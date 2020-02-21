using server.DAO;
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
    // 入队，退队
   public  class PublishTipsMessage
    {

        private const string Show_Message = "ShowMessage";
        private UserRoomDao roomUser = new UserRoomDao();
        private PublishPlayerState publishPlayerState = new PublishPlayerState();
        /// <summary>
        /// 用户入队，退队
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="isJoin"></param>
        public void JoinAndExitLeader(string userName,int userId,string grounpName,bool isJoin =true)
        {
            List<int> userList = roomUser.GetUserListBySingleUser(userId, false);
            string message = userName + "加入队:" + grounpName;
            if(!isJoin)
            {
                message = userName + "退出队:" + grounpName;
            }
            userList.ForEach((_userID) => {

                SendMessage(_userID, message);
            });
        }


        private void SendMessage(int userId,string message)
        {
            ConcurrentDictionary<PubgSession, SessionItem> dic = PubgSession.mOnLineConnections;

            foreach (PubgSession session in dic.Keys)
            {
                SessionItem sessionItem = null;
                dic.TryGetValue(session, out sessionItem);
                if (sessionItem != null && sessionItem.userId.Equals(userId.ToString()))
                {
                    //推送的时候刷洗状态
                    publishPlayerState.SendSingleUserMessage(userId);


                    string data = Show_Message + Constant.START_SPLIT + message + "\r\n";
                    session.Send(data);
                }

            }
        }

    }
}
