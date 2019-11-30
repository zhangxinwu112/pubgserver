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
    public class SearchRoomDao : CommonDao
    {
        ILog Logger = log4net.LogManager.GetLogger("server.DAO.SearchRoomDao");
        public void SearchAllRoom(PubgSession session, string body, string  userId)
        {
            Logger.InfoFormat("查询用户下的所有房间：{0}", userId);
            string sql = "select * from room where userId = @userId ORDER BY id DESC";
            List<Room> result = MySqlExecuteTools.GetObjectResult<Room>(sql,
                new MySqlParameter[] { new MySqlParameter("@userId", userId)});
            DataResult dataResult = new DataResult();
            dataResult.result = 0;
            dataResult.data = result;
            session.Send(GetSendData(dataResult, body));
        }

        public void SearchSingleRoom(PubgSession session, string body, string roomId)
        {
            Logger.InfoFormat("查询单房间下的对：{0}", roomId);
            string sql = "select * from grounp where roomId = @roomId";
            List<Grounp> result = MySqlExecuteTools.GetObjectResult<Grounp>(sql,
                new MySqlParameter[] { new MySqlParameter("@roomId", roomId) });
            DataResult dataResult = new DataResult();
            dataResult.result = 0;
            dataResult.data = result;
            session.Send(GetSendData(dataResult, body));
        }

        public void SearchSingleGrounp(PubgSession session, string body, string grounpId)
        {
            Logger.InfoFormat("查询group下的user：{0}", grounpId);
            string sql = "select * from grounp_user where grounp_id = @grounp_id";
            List<Grounp_User> result = MySqlExecuteTools.GetObjectResult<Grounp_User>(sql,
                new MySqlParameter[] { new MySqlParameter("@grounp_id", grounpId) });
            DataResult dataResult = new DataResult();
            dataResult.result = 0;
            dataResult.data = GetUserList(result);
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
