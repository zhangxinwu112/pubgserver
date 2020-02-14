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
        public void SearchAllGrounp(PubgSession session, string body, string keyName,string userId,string userType)
        {
            Logger.InfoFormat("查询所有的游戏：{0}", keyName);
            List<Grounp> result = null;
            if(keyName.Equals("-1"))
            {
                string sql = "select * from grounp  ORDER BY id DESC";
                result = MySqlExecuteTools.GetObjectResult<Grounp>(sql, null);
            }
            else
            {
                string sql = "select * from grounp where name like '%"+ keyName +"%' ORDER BY id DESC";
                result = MySqlExecuteTools.GetObjectResult<Grounp>(sql, null);
            }
                
            result.ForEach((item) => {

                if(item.fenceLat>0)
                {
                    item.isDefence = true;
                }

            });
            if(userType.Equals("1"))
            {

                Grounp grounp = result.Where((item) => item.userId == int.Parse(userId)).FirstOrDefault<Grounp>();
                result.Remove(grounp);
                result.Insert(0, grounp);
            }
            DataResult dataResult = new DataResult();
            dataResult.result = 0;
            dataResult.data = result;
            session.Send(GetSendData(dataResult, body));
        }

        public void SearchSingleGrounp(PubgSession session, string body, string grounpId,string userId)
        {
            Logger.InfoFormat("查询单队下的房间：{0}", grounpId);
           
            DataResult dataResult = new DataResult();
            dataResult.result = 0;
            dataResult.data = SearchRoomListByGrounp(grounpId, userId);
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


        private List<UserName> GetUserList(List<Room_User> roomUserList)
        {
            List<string> ids = new List<string>();
            roomUserList.ForEach((item) => {
                ids.Add(item.user_id.ToString());

            });

            string result =  StrUtil.ConnetString(ids, ",");
            string sql = "select * from user where id in  ("+ result+")";
            List<UserName> resultData = MySqlExecuteTools.GetObjectResult<UserName>(sql,null);
            resultData.ForEach((user)=> {

                int runState = roomUserList.Where((item) => item.user_id == user.id).FirstOrDefault<Room_User>().runState;
                user.runState = runState;
            });

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
            string sql = "select * from grounp where runState =0 and fenceLon>0 ORDER BY id DESC";
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
                    sql = "update  grounp set fenceRadius = 2000,fenceTotalRadius=2000,runState = -1,fenceLon=-1,fenceLat=-1 where id = @grounpId;";
                    MySqlExecuteTools.GetCountResult(sql, new MySqlParameter[] { new MySqlParameter("@grounpId", item.id) });
                }

            });

            joinRoomDao.GetAllRoom();

        }
    }


}
