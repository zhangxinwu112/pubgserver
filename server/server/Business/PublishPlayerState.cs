﻿using server.DAO;
using server.Model;
using server.Tool;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server.Business
{
   public  class PublishPlayerState
    {

        public static readonly string Update_Command = "UpdatePlayerState";

        public static readonly string Game_Over = "GameOver";


        private RoomDao roomDao = new RoomDao();
        /// <summary>
        /// 推送管理员
        /// </summary>
        public void PublishAdmin(Room room)
        {
           
            int AdminUser = roomDao.GetGrounpAdminByRoom(room.id);
            SendMessage(AdminUser);
        }
        //推送队长自己
        public void PubilshLoaderSelf(int leaderUser)
        {
            SendMessage(leaderUser);
        }

        private void SendMessage(int userId)
        {
            ConcurrentDictionary<PubgSession, SessionItem> dic = PubgSession.mOnLineConnections;

            foreach (PubgSession session in dic.Keys)
            {
                SessionItem sessionItem = null;
                dic.TryGetValue(session, out sessionItem);
                if (sessionItem != null && sessionItem.userId.Equals( userId.ToString()))
                {
                    string data = Update_Command + Constant.START_SPLIT + "admin" + "\r\n";
                    session.Send(data);
                }

            }
        }


       
        /// <summary>
        /// 通过队长，推送给他玩家
        /// </summary>
        public void PublishPlayerList(List<int> userList)
        {
            //推送给所有队员
            userList.ForEach((userId) =>
            {

                SendSingleUserMessage(userId, Update_Command);
            });
        }

        public void SendSingleUserMessage(int userId,string CommandName)
        {
            ConcurrentDictionary<PubgSession, SessionItem> dic = PubgSession.mOnLineConnections;
            foreach (PubgSession session in dic.Keys)
            {
                SessionItem sessionItem = null;
                dic.TryGetValue(session, out sessionItem);
                if (sessionItem != null && sessionItem.userId.Equals(userId.ToString()))
                {

                    string data = CommandName + Constant.START_SPLIT + "player" + "\r\n";
                    session.Send(data);
                }

            }
        }

        public void SendUserListByAdmin(int adminUserId,string CommandName)
        {
            ConcurrentDictionary<PubgSession, SessionItem> dic = PubgSession.mOnLineConnections;
            List<int> userIdList = roomDao.GetRoomUserListByAdmin(adminUserId);

            userIdList.ForEach((userId) =>
            {
                foreach (PubgSession session in dic.Keys)
                {
                    SessionItem sessionItem = null;
                    dic.TryGetValue(session, out sessionItem);
                    if (sessionItem != null && sessionItem.userId.Equals(userId.ToString()))
                    {

                        string data = CommandName + Constant.START_SPLIT + "player" + "\r\n";
                        session.Send(data);
                    }

                }

            });
        }
    }
}
