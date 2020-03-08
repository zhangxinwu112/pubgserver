using server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using server.Tool;
using MySql.Data.MySqlClient;
using server.Business;

namespace server.DAO
{
    /// <summary>
    /// 创建删除修改
    /// </summary>
    public class JoinRoomDao : RoomDao
    {
        ILog Logger = log4net.LogManager.GetLogger("server.DAO.JoinRoomDao");
        //private readonly int maxNum = 50;

        private PublishTipsMessage publishTipsMessage = new PublishTipsMessage();
        public void JoinRoom(PubgSession session, string body, string checkCode,string grounpId,
            string roomId,string userId,string userName)
        {
            Logger.InfoFormat("加入队：{0},{1}", roomId, userId);
            string sql = "select * from room_user where user_id = @user_id";
            List<Room_User> grounp_UserList = MySqlExecuteTools.GetObjectResult<Room_User>(sql,
               new MySqlParameter[] { new MySqlParameter("@user_id", userId) });
            DataResult dataResult = new DataResult();
            if (grounp_UserList.Count>0)
            {
                dataResult.result = 1;
                dataResult.resean = "您已经加入房间，不能重复。";
                session.Send(GetSendData(dataResult, body));
                return;
            }

            //校验checkcode是否正确

           sql = "select * from room where id = @roomId and checkCode = @checkCode";

           int countResult =  MySqlExecuteTools.GetCountResult(sql, new MySqlParameter[] { new MySqlParameter("@roomId", roomId), new MySqlParameter("@checkCode", checkCode) });

            if(countResult==0)
            {
                dataResult.result = 1;
                dataResult.resean = "进入房间的密码不正确，请重试。";
                session.Send(GetSendData(dataResult, body));
                return;
            }


            Grounp p = SearchGrounpDao.GetGrounpById(grounpId);

            if(p!=null && p.runState == 0)
            {
                dataResult.result = 1;
                dataResult.resean = "游戏运行中，无法加入战队。";
                session.Send(GetSendData(dataResult, body));
                return;
            }
            sql = "select * from room where id = @roomId";

             List<Room> roomList = MySqlExecuteTools.GetObjectResult<Room>(sql, new MySqlParameter[] { new MySqlParameter("@roomId", roomId) });
            if(roomList.Count==1 && roomList[0].runState == 0)
            {
                dataResult.result = 1;
                dataResult.resean = "该战队已经准备就绪，无法加入，请重试。";
                session.Send(GetSendData(dataResult, body));
                return;
            }

            //grounp_UserList = SearchSingleGrounpCommon(roomId);

            //if (grounp_UserList.Count> maxNum)
            //{
            //    dataResult.result = 1;
            //    dataResult.resean = "房间人数加入已满，请重试。";
            //}
            //else
            //{
            sql = "insert into room_user(room_id,user_id) " + "values('" + roomId + "','" + userId + "')";
            MySqlExecuteTools.AddOrUpdate(sql);
            dataResult.result = 0;

            //推送数据
            sql = "select name from room where id=" + roomId;
            string rommName = MySqlExecuteTools.GetSingleFieldResult(sql, null)[0].ToString();
            publishTipsMessage.JoinAndExitLeader(userName, int.Parse(userId), rommName, true);


           // }
            session.Send(GetSendData(dataResult, body));
            GetRoomUserData();
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="session"></param>
        /// <param name="body"></param>
        /// <param name="id">roomid</param>
        /// <param name="userId">用户id</param>
        public void ExitRoom(PubgSession session, string body, string roomId, string userId,string userName)
        {
            string sql = "select * from room_user where user_id = @user_id";
            List<Room_User> grounp_UserList = MySqlExecuteTools.GetObjectResult<Room_User>(sql,
               new MySqlParameter[] { new MySqlParameter("@user_id", userId) });
            DataResult dataResult = new DataResult();
            if (grounp_UserList.Count==0)
            {

                dataResult.result = 1;
                dataResult.resean = "非法操作";
                session.Send(GetSendData(dataResult, body));
                return;
            }


            Grounp p = GetGrounpByPlayer(int.Parse(userId));
            if (p != null && p.runState == 0)
            {
                dataResult.result = 1;
                dataResult.resean = "游戏运行中，无法退出战队。";
                session.Send(GetSendData(dataResult, body));
                return;
            }


            // 删除之前提示
            sql = "select name from room where id=" + roomId;
            string rommName = MySqlExecuteTools.GetSingleFieldResult(sql, null)[0].ToString();
            publishTipsMessage.JoinAndExitLeader(userName, int.Parse(userId), rommName, false);



            sql = "delete from room_user  where id = @id";
            MySqlExecuteTools.GetCountResult(sql, new MySqlParameter[] { new MySqlParameter("@id", grounp_UserList[0].id) });

            dataResult.result = 0;
            session.Send(GetSendData(dataResult, body));

            //刷新缓存数据
            GetRoomUserData();
        }
        /// <summary>
        /// 获取最新的grounp_user信息进行grounp信息
        /// </summary>
        public void GetRoomUserData()
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("data", SearchAllRoomUser());
            EventMgr.Instance.SendEvent(EventName.UPATE_ROOM_USER, dic);
        }

        public void GetAllRoom()
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("data", SearchAllRoom());
            EventMgr.Instance.SendEvent(EventName.ALL_ROOM_DATA, dic);
        }

        public int IsEditRoom(string roomId, string userId)
        {
            string sql = "select *  from room where id = @id and userId = @userId";
            List<Room> roomList = MySqlExecuteTools.GetObjectResult<Room>(sql,
             new MySqlParameter[] { new MySqlParameter("@id", roomId), new MySqlParameter("@userId", userId) });
            if(roomList.Count>0)
            {
                return 0;
            }

            return -1;
        }
    }


    


}
