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
using server.Business;

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
            //管理员
            if(userType.Equals("1"))
            {

                Grounp grounp = result.Where((item) => item.userId == int.Parse(userId)).FirstOrDefault<Grounp>();
                result.Remove(grounp);
                result.Insert(0, grounp);
            }

            else
            {
                //当前的grounp显示top
                Grounp p = GetGrounpByPlayer(int.Parse( userId));
                if(p!=null)
                {
                    Grounp grounp = result.Where((item) => item.id == p.id).FirstOrDefault<Grounp>();
                    result.Remove(grounp);
                    result.Insert(0, grounp);

                }
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

       public static  Grounp GetGrounpById(string grounpId)
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

        public int CheckEnterButton(string usreId,string userType)
        {
            string sql = "";
           
            int runState = -1;
            //管理员
            if (userType.Equals("1"))
            {
                sql = "select * from grounp where userId = @userId";
                Grounp  p = MySqlExecuteTools.GetObjectResult<Grounp>(sql, new MySqlParameter[] { new MySqlParameter("@userId", usreId) }).FirstOrDefault<Grounp>();

               
                if (p == null)
                {
                    return -1;
                }
                runState = p.runState;

            }
            else
            {
                Grounp p = GetGrounpByPlayer(int.Parse(usreId));
                if (p == null)
                {
                    return -1;
                }
                runState = p.runState;
            }

            return runState;
        }

       
    }


}
