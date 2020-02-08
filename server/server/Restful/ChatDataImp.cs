using Restful;
using server.DAO;
using server.Model;
using server.Tool;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server.Restful
{
    public class ChatDataImp : IChatData
    {


        private RoomDao roomDao = new RoomDao();
        public string GetMessage(string userId)
        {
            // throw new NotImplementedException();

            return "";
        }

        /// <summary>
        /// roomid|content
        /// </summary>
        /// <param name="json"></param>
        public void SendMeesage(string json)
        {
            
  
            //string[] strs = json.Split('|');
            //string roomId = strs[0];

            //string content = strs[1];
            //List<Room_User> roomUsers = roomDao.SearchSingleGrounpCommon(roomId);
            //ConcurrentDictionary<PubgSession, SessionItem> dic = PubgSession.mOnLineConnections;
            //if (dic == null || dic.Count == 0)
            //{
            //    return;
            //}
            //roomUsers.ForEach((item) =>
            //{
            //    foreach (PubgSession session in dic.Keys)
            //    {
            //        SessionItem sessionItem = null;
            //        dic.TryGetValue(session, out sessionItem);
            //        if(sessionItem!=null && sessionItem.gpsItem!=null&& sessionItem.gpsItem.userId== item.user_id)
            //        {
            //            string sendMessageContent = content +  "    "+TimeUtils.GetCurrentFormatTime();
            //            string data = "SendMessage" + Constant.START_SPLIT + sendMessageContent + "\r\n";
            //            session.Send(data);
            //        }

            //    }


            //});

        }
    }
}
