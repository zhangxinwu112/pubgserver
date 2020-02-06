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
    public class SearchGrounpDao : RoomDao
    {
        ILog Logger = log4net.LogManager.GetLogger("server.DAO.SearchGrounpDao");
        private JoinRoomDao joinRoomDao = new JoinRoomDao();
        public void SearchAllGrounp(PubgSession session, string body, string  userId)
        {
            Logger.InfoFormat("查询用户下的所有队：{0}", userId);
            List<Grounp> result = null;
            if (!userId.Equals("0"))
            {
                string sql = "select * from grounp where userId = @userId ORDER BY id DESC";
                result = MySqlExecuteTools.GetObjectResult<Grounp>(sql,new MySqlParameter[] { new MySqlParameter("@userId", userId) });
            }
            else
            {
                string sql = "select * from grounp  ORDER BY id DESC";
                result = MySqlExecuteTools.GetObjectResult<Grounp>(sql, null);
            }
            
            DataResult dataResult = new DataResult();
            dataResult.result = 0;
            dataResult.data = result;
            session.Send(GetSendData(dataResult, body));
        }

        public void SearchSingleGrounp(PubgSession session, string body, string grounpId)
        {
            Logger.InfoFormat("查询单队下的房间：{0}", grounpId);
           
            DataResult dataResult = new DataResult();
            dataResult.result = 0;
            dataResult.data = SearchSingleRoomCommon(grounpId);
            session.Send(GetSendData(dataResult, body));
        }

        public void SearchSingleRoom(PubgSession session, string body, string roomId)
        {
            Logger.InfoFormat("查询room下的user：{0}", roomId);
            DataResult dataResult = new DataResult();
            dataResult.result = 0;
            dataResult.data = GetUserList(SearchSingleGrounpCommon(roomId));
            session.Send(GetSendData(dataResult, body));
        }


        private List<UserName> GetUserList(List<Room_User> groupList)
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

       public Grounp GetGrounpById(string grounpId)
        {
            string sql = "select * from grounp where id = @grounpId ORDER BY id DESC";
            List<Grounp> result = MySqlExecuteTools.GetObjectResult<Grounp>(sql, new MySqlParameter[] { new MySqlParameter("@grounpId", grounpId) });

            if(result.Count==1)
            {
                return result[0];
            }

            return null;
        }

        public List<int> SearchGrounpListByUser(int userId)
        {
            string sql = "select * from grounp where userId = @userId and runState =0 and fenceLon>0  ORDER BY id DESC";
            List<Grounp> list  = MySqlExecuteTools.GetObjectResult<Grounp>(sql, new MySqlParameter[] { new MySqlParameter("@userId", userId) });
            return  list.Select(a => a.id).ToList<int>();
        }

        public void UpdateFenceScope(int frequency)
        {
            string sql = "select * from grounp where runState =0 and fenceLon>0 and fenceRadius>=0 ORDER BY id DESC";
            List<Grounp> list = MySqlExecuteTools.GetObjectResult<Grounp>(sql, null );

            list.ForEach((item) => {

                if(item.fenceRadius>0)
                {
                   int everyCount = item.fenceTotalRadius / (item.playerTime * 60 / frequency);
                   sql = "update  grounp set fenceRadius = '" + (item.fenceRadius- everyCount) + "' where id = @grounpId;";
                   MySqlExecuteTools.GetCountResult(sql, new MySqlParameter[] { new MySqlParameter("@grounpId", item.id) });
                }
                else
                {
                    sql = "update  grounp set fenceRadius = '" + item.fenceTotalRadius + "',runState = -1  where id = @grounpId;";
                    MySqlExecuteTools.GetCountResult(sql, new MySqlParameter[] { new MySqlParameter("@grounpId", item.id) });
                }

            });

            joinRoomDao.GetAllRoom();

        }
    }


}
