using server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using server.Tool;
using MySql.Data.MySqlClient;
using Utils;

namespace server.DAO
{
    /// <summary>
    ///查询房间的关联数据
    /// </summary>
    public class SearchRoomDao : RoomDao
    {
        ILog Logger = log4net.LogManager.GetLogger("server.DAO.SearchRoomDao");
        public void SearchAllRoom(PubgSession session, string body, string  userId)
        {
            Logger.InfoFormat("查询用户下的所有房间：{0}", userId);
            List<Room> result = null;
            if (!userId.Equals("0"))
            {
                string sql = "select * from room where userId = @userId ORDER BY id DESC";
                result = MySqlExecuteTools.GetObjectResult<Room>(sql,new MySqlParameter[] { new MySqlParameter("@userId", userId) });
            }
            else
            {
                string sql = "select * from room  ORDER BY id DESC";
                result = MySqlExecuteTools.GetObjectResult<Room>(sql, null);
            }
            
            DataResult dataResult = new DataResult();
            dataResult.result = 0;
            dataResult.data = result;
            session.Send(GetSendData(dataResult, body));
        }

        public void SearchSingleRoom(PubgSession session, string body, string roomId)
        {
            Logger.InfoFormat("查询单房间下的对：{0}", roomId);
           
            DataResult dataResult = new DataResult();
            dataResult.result = 0;
            dataResult.data = SearchSingleRoomCommon(roomId);
            session.Send(GetSendData(dataResult, body));
        }

        public void SearchSingleGrounp(PubgSession session, string body, string grounpId)
        {
            Logger.InfoFormat("查询group下的user：{0}", grounpId);
            DataResult dataResult = new DataResult();
            dataResult.result = 0;
            dataResult.data = GetUserList(SearchSingleGrounpCommon(grounpId));
            session.Send(GetSendData(dataResult, body));
        }


        private List<UserName> GetUserList(List<Grounp_User> groupList)
        {
            List<string> ids = new List<string>();
            groupList.ForEach((item) => {
                ids.Add(item.user_id.ToString());

            });

            string result =  StrUtil.ConnetString(ids, ",");
            string sql = "select * from user where id in  ("+ result+")";
            List<UserName> resultData = MySqlExecuteTools.GetObjectResult<UserName>(sql,null);

            return resultData;


        }
    }


}
