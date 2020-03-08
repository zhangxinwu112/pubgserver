using log4net;
using MySql.Data.MySqlClient;
using server;
using server.Business;
using server.DAO;
using server.Model;
using server.Tool;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using System.Threading.Tasks;

namespace Restful
{

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Single, IncludeExceptionDetailInFaults = true)]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]

    public class ServiceImp : IService
    {
        ILog Logger = log4net.LogManager.GetLogger("Restful.ServiceImp");

        private JoinRoomDao joinRoomDao = new JoinRoomDao();
        private UserDao userDao = new UserDao();
        private UserRoomDao userRoomDao = new UserRoomDao();

        private SearchGrounpDao searchGrounpDao = new SearchGrounpDao();

       private  PublishPlayerState publishPlayerState = new PublishPlayerState();

        private ScoreDao scoreDao = new ScoreDao();
        public int Save(string json)
        {

            string[] strs = json.Split('|');

            string grounpId = strs[0];

            string fenceLon = strs[1];

            string fenceLat = strs[2];

            string fenceRadius = strs[3];

            string sql = "update  grounp set fenceLon = '" + fenceLon + "', fenceLat = '" + fenceLat +
                "', fenceRadius = '" + fenceRadius + "', fenceTotalRadius = '" + fenceRadius + "' where id = @grounpId;";
            int count = MySqlExecuteTools.GetCountResult(sql, new MySqlParameter[] { new MySqlParameter("@grounpId", grounpId) });
            // Console.WriteLine(count);
            joinRoomDao.GetAllRoom();
            return 0;
        }

        public string SearchState(string grounpId)
        {
            // throw new NotImplementedException();
            string sql = "select * from grounp where id = @grounpId  and runState = 0";
            int count = MySqlExecuteTools.GetCountResult(sql, new MySqlParameter[] { new MySqlParameter("@grounpId", grounpId) });
            if (count > 0)
            {
                return "0";
            }
            else
            {
                return "1";
            }


        }

        /// <summary>
        /// 设置grounp运行状态
        /// </summary>
        /// <param name="grounpId"></param>
        /// <returns></returns>
        public string UpdateState(string grounpId)
        {
            string sql = "select * from grounp where id = @grounpId";
            List<Grounp> result = MySqlExecuteTools.GetObjectResult<Grounp>(sql,
                new MySqlParameter[] { new MySqlParameter("@grounpId", grounpId) });

            if (result.Count == 1)
            {
                if (result[0].fenceLat <= 0)
                {
                    return "电子围栏尚未设置，无法启动游戏！";
                }
                //查询grounp下room 的状态
                sql = "select * from room where grounpId = @grounpId and  runState = -1";
                int resultCount = MySqlExecuteTools.GetCountResult(sql, new MySqlParameter[] { new MySqlParameter("@grounpId", grounpId) });
                if(resultCount>0)
                {
                    
                    return "该游戏的队员尚未准备就绪，无法启动游戏";
                    
                }


            }
            else
            {
                return "非法操作";
            }

            sql = "update  grounp set runState =0  where id = @grounpId;";
            MySqlExecuteTools.GetCountResult(sql, new MySqlParameter[] { new MySqlParameter("@grounpId", grounpId) });

            //推送给该组的其他玩家更新状态

            publishPlayerState.SendUserListByAdmin(result[0].userId, PublishPlayerState.Update_Command);

            return "0";
        }



        #region chat
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
        public string SendMeesage(string json)
        {
            string[] strs = json.Split('|');
            string roomId = strs[0];

            string content = strs[1];

            string userName = strs[2];

            string userType = strs[3];

            string userID = strs[4];

            ConcurrentDictionary<PubgSession, SessionItem> dic = PubgSession.mOnLineConnections;
            if (dic == null || dic.Count == 0)
            {
                return "failture";
            }

            //玩家的信息
            if (userType.Equals("0"))
            {
                //获取该房间下的所有用户
                List<Room_User> roomUsers = roomDao.SearchSingleGrounpCommon(roomId, int.Parse(userID));

                //获取管理员
                int AdminUser = roomDao.GetGrounpAdminByRoom(int.Parse(roomId));

                //推送给所有队员
                roomUsers.ForEach((item) =>
                {
                    foreach (PubgSession session in dic.Keys)
                    {
                        SessionItem sessionItem = null;
                        dic.TryGetValue(session, out sessionItem);
                        if (sessionItem != null && sessionItem.gpsItem != null && sessionItem.gpsItem.userId == item.user_id)
                        {

                            StartSendChatMessage(content, userName, session);
                        }

                    }

                });
                //推送给该对的管理员

                foreach (PubgSession session in dic.Keys)
                {
                    SessionItem sessionItem = null;
                    dic.TryGetValue(session, out sessionItem);
                    if (sessionItem != null && sessionItem.gpsItem != null && sessionItem.gpsItem.userId == AdminUser)
                    {

                        StartSendChatMessage(content, userName, session);
                    }

                }
            }
            //管理员
            else
            {
                List<int> userIdList = roomDao.GetRoomUserListByAdmin(int.Parse(userID));

                userIdList.ForEach((userId) =>
                {
                    foreach (PubgSession session in dic.Keys)
                    {
                        SessionItem sessionItem = null;
                        dic.TryGetValue(session, out sessionItem);
                        if (sessionItem != null && sessionItem.gpsItem != null && (
                        sessionItem.gpsItem.userId == userId))
                        {
                            StartSendChatMessage(content, "系统管理员", session);
                        }

                    }

                });
            }
            return "success";

        }

        public void StartSendChatMessage(string content, string userName, PubgSession session)
        {
            Dictionary<string, string> _dic = new Dictionary<string, string>();
            _dic.Add("time", TimeUtils.GetCurrentFormatTime());
            _dic.Add("content", content);
            _dic.Add("name", userName);
            string resultJson = Utils.CollectionsConvert.ToJSON(_dic);
            string data = "SendMessage" + Constant.START_SPLIT + resultJson + "\r\n";
            session.Send(data);
        }


        #endregion

        public string ShowDebug(string info)
        {
            Console.WriteLine("调试信息:{0}",info);
            return info;
        }

        /// <summary>
        /// 减命
        /// </summary>
        private readonly int SubTractValue = 1;
        public string SubTractLife(string userId)
        {
            userDao.SetLifeValue(userId, SubTractValue);
            return "";
        }

        /// <summary>
        /// 加命
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public string AddLife(string json)
        {
            string[] strs = json.Split('|');
            string addValue = strs[0];
            string userId = strs[1];
            userDao.SetLifeValue(userId, int.Parse(addValue),false);
            return "";
        }

        /// <summary>
        /// 设置玩家准备好了状态
        /// </summary>
        /// <param name="userId"></param>
        public string SetPlayerState(string userId)
        {
            return  userRoomDao.SetUserRoomState(userId);
        }

        public  int CheckEnterButton(string json)
        {
            string[] strs = json.Split('|');

            string userId = strs[0];

            string userType = strs[1];

            return searchGrounpDao.CheckEnterButton(userId, userType);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="currrent"></param>
        /// <param name="currrentUser">0为玩家排行，1为room排行</param>
        /// <returns></returns>
        public string SearchScore(string userId, string userType,string currrentUser)
        {
            if(currrentUser.Equals("0"))
            {
                return  scoreDao.SearchScore(int.Parse(userId), userType, true);
            }
            else
            {
                return scoreDao.SearchScore(int.Parse(userId), userType, false);
            }
        }

        public string GetRoomList(string adminUserId)
        {
           List<Room> roomList =   joinRoomDao.GetRoomListByAdmin(int.Parse(adminUserId));
            return Utils.CollectionsConvert.ToJSON(roomList);
        }

        //public string SearchScoreByRoom(string roomId)
        //{
        //    return scoreDao.SearchScoreByRoomId(int.Parse(roomId));
        //}

        public int IsEditRoom(string roomId, string userId)
        {
            return  joinRoomDao.IsEditRoom(roomId, userId);
        }

        public string GetRoomUserTreeData(string userId)
        {
            return searchGrounpDao.GetRoomUserTreeData(userId);
        }

        public string GetRoomLifeInfoByUser(string userId)
        {
            return scoreDao.GetRoomLifeInfoByUser(userId);
          
        }

        public string GetPlayerInfoByUser(string userId)
        {
            Logger.Debug("userid:="+userId);
            return scoreDao.GetPlayerInfoByUser(userId);
        }

        public string GetRemainTime(string userId,string userType)
        {
            userId = userId.Trim();
            userType = userType.Trim();
            Console.WriteLine("userId={0},userType={1}", userId, userType);
            string sql = "";
            //管理员
            if (userType.Equals("1"))
            {
                sql = "select * from grounp where userId = @userId  and runState = 0";
                List<Grounp> pList = MySqlExecuteTools.GetObjectResult<Grounp>(sql, new MySqlParameter[] { new MySqlParameter("@userId", userId) });
                if (pList.Count > 0)
                {
                    return  pList[0].remainTime.ToString();
                }
            }
            //玩家
            else
            {
                sql = "select p.remainTime from grounp p JOIN room r on p.id = r.grounpId join room_user ru on r.id = ru.room_id where ru.user_id =" + userId;
                List<object> list = MySqlExecuteTools.GetSingleFieldResult(sql, null);
                if(list!=null && list.Count>0)
                {
                    return list[0].ToString();
                }
            }
            

            return "";
            
        }

        public string GetLeaderAuthority(string userId)
        {
            string sql = "select * from room where userId = @userId";
            List<Room> roomList = MySqlExecuteTools.GetObjectResult<Room>(sql, new MySqlParameter[] { new MySqlParameter("@userId", userId) });
            if(roomList!=null && roomList.Count>0)
            {
                return "1";
            }
            else
            {
                return "0";
            }

        }
    }


}
