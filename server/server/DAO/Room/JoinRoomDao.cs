using server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using server.Tool;
using MySql.Data.MySqlClient;

namespace server.DAO
{
    /// <summary>
    /// 创建删除修改
    /// </summary>
    public class JoinRoomDao : RoomDao
    {
        ILog Logger = log4net.LogManager.GetLogger("server.DAO.JoinRoomDao");
        private readonly int maxNum = 50;
        public void JoinRoom(PubgSession session, string body, string checkCode,string roomId,string userId)
        {
            Logger.InfoFormat("加入房间：{0},{1}", roomId, userId);
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

            grounp_UserList = SearchSingleGrounpCommon(roomId);
           
            if (grounp_UserList.Count> maxNum)
            {
                dataResult.result = 1;
                dataResult.resean = "房间人数加入已满，请重试。";
            }
            else
            {
                 sql = "insert into room_user(room_id,user_id) " +
                   "values('" + roomId + "','" + userId + "')";
                    MySqlExecuteTools.AddOrUpdate(sql);
                dataResult.result = 0;
            }
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
        public void ExitRoom(PubgSession session, string body, string grounpId, string userId)
        {
            string sql = "select * from room_user where user_id = @user_id";
            List<Room_User> grounp_UserList = MySqlExecuteTools.GetObjectResult<Room_User>(sql,
               new MySqlParameter[] { new MySqlParameter("@user_id", userId) });

            grounp_UserList.ForEach((item) => {

                sql = "delete from room_user  where id = @id";
                MySqlExecuteTools.GetCountResult(sql, new MySqlParameter[] { new MySqlParameter("@id", item.id) });

            });

            DataResult dataResult = new DataResult();
            dataResult.result = 0;
            //查询能否删除
            session.Send(GetSendData(dataResult, body));
            GetRoomUserData();
        }

        /// <summary>
        /// 查询能否进入房间
        /// </summary>
        public void SearchEnterRoomState(PubgSession session, string body, string userId)
        {
            DataResult dataResult = new DataResult();

            string sql = "select * from room_user where user_id = @user_id";
            List<Room_User> grounp_UserList = MySqlExecuteTools.GetObjectResult<Room_User>(sql,
               new MySqlParameter[] { new MySqlParameter("@user_id", userId) });

            dataResult.result = 0;
            //没有加入房间
            if (grounp_UserList.Count==0)
            {
                dataResult.data = false;
            }

            if(grounp_UserList.Count==1)
            {
                //int grounp_id = grounp_UserList[0].room_id;

                //sql = "select * from room_user where room_id = @room_id";
                //List<Room_User> _grounp_UserList = MySqlExecuteTools.GetObjectResult<Room_User>(sql,
                //   new MySqlParameter[] { new MySqlParameter("@room_id", grounp_id) });
                //if(_grounp_UserList.Count< maxNum)
                //{
                //    dataResult.data = false;
                //}
                //else
                //{
                 dataResult.data = true;
               // }
            }

            session.Send(GetSendData(dataResult, body));
        }

        public void CheckEnterButton(PubgSession session, string body, string checkCode, string userId)
        {
            DataResult dataResult = new DataResult();
            string sql = "select * from room_user where user_id = @user_id";
            List<Room_User> grounp_UserList = MySqlExecuteTools.GetObjectResult<Room_User>(sql,
              new MySqlParameter[] { new MySqlParameter("@user_id", userId) });

            if(grounp_UserList.Count==1)
            {
                //sql = "select * from room where id = @id  and checkCode = @checkCode";
                //List<Room> grounps = MySqlExecuteTools.GetObjectResult<Room>(sql,
                //    new MySqlParameter[] { new MySqlParameter("@id", grounp_UserList[0].room_id), new MySqlParameter("@checkCode", checkCode) });
                //if(grounps.Count==1)
                //{
                dataResult.result = 0;
                session.Send(GetSendData(dataResult, body));
                return;
                //}
            }
            dataResult.result = 1;
            session.Send(GetSendData(dataResult, body));
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
    }


    


}
