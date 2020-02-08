using MySql.Data.MySqlClient;
using server.Model;
using server.Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server.DAO
{
   public  class RoomDao :CommonDao
    {

        /// <summary>
        /// 通过grounp查询room
        /// </summary>
        /// <param name="grounpId"></param>
        /// <returns></returns>
        protected List<Room> SearchRoomListByGrounp(string grounpId,string  userId="-1")
        {
            
            string sql = "select * from room where grounpId = @grounpId ORDER BY id ASC";
            List<Room> result = MySqlExecuteTools.GetObjectResult<Room>(sql,
                new MySqlParameter[] { new MySqlParameter("@grounpId", grounpId) });

            result.ForEach((item) => {

                sql = "select * from room_user where room_id = @room_id ";
                int  count = MySqlExecuteTools.GetCountResult(sql,
                    new MySqlParameter[] { new MySqlParameter("@room_id", item.id) });
                item.userCount = count;

                if(!userId.Equals("-1"))
                {
                    sql = "select * from room_user where room_id = @room_id  and user_id = @user_id";

                    count = MySqlExecuteTools.GetCountResult(sql,
                    new MySqlParameter[] { new MySqlParameter("@room_id", item.id), new MySqlParameter("@user_id", userId) });
                    if(count>0)
                    {
                        item.isCurrentUser = true;
                    }
                }
              
            });

            return result;

        }

        /// <summary>
        /// 通过room查询user,如果存在userid，将其排除
        /// </summary>
        /// <param name="grounpId"></param>
        /// <returns></returns>
        public List<Room_User> SearchSingleGrounpCommon(string roomId,int userID=-1)
        {
            List<Room_User> result = null;
            if (userID!=-1)
            {
                string sql = "select * from room_user where room_id = @room_id and user_id <> @userId";
                result = MySqlExecuteTools.GetObjectResult<Room_User>(sql,
                new MySqlParameter[] { new MySqlParameter("@room_id", roomId), new MySqlParameter("@userId", userID) });
            }
            else
            {
                string sql = "select * from room_user where room_id = @room_id";
                result = MySqlExecuteTools.GetObjectResult<Room_User>(sql,
                new MySqlParameter[] { new MySqlParameter("@room_id", roomId)});
            }
           
            
            return result;
        }


        protected List<Room_User> SearchAllRoomUser()
        {
            string sql = "select * from room_user";
            List<Room_User> result = MySqlExecuteTools.GetObjectResult<Room_User>(sql,null);
            return result;
        }

        protected List<Room> SearchAllRoom()
        {
            string sql = "select * from room";
            List<Room> result = MySqlExecuteTools.GetObjectResult<Room>(sql, null);
            return result;
        }

        /// <summary>
        /// room查询管理员
        /// </summary>
        /// <param name="roomId"></param>
        /// <returns></returns>
        public int GetGrounpAdminByRoom(int roomId)
        {
           
            string sql = "select userId from room r join grounp p on p.id = r.grounpId and r.id = @roomid;";
            int userId = -1;
                
             object result = MySqlExecuteTools.GetSingleFieldResult(sql,
                new MySqlParameter[] { new MySqlParameter("@roomid", roomId) }).FirstOrDefault<object>();

            if(result!=null)
            {
                userId = (int)result;
            }
            return userId;


        }
        /// <summary>
        /// 通过admin获取roomUserList
        /// </summary>
        /// <returns></returns>
        public List<int>  GetRoomUserListByAdmin(int userID)
        {
            string sql = " select ru.user_id from room r join grounp p join room_user ru on p.id = r.grounpId and r.id = ru.room_id and p.userId = @userId";
          
             List<object> dataResult = MySqlExecuteTools.GetSingleFieldResult(sql,
               new MySqlParameter[] { new MySqlParameter("@userId", userID) });
            List<int> result = new List<int>();

            dataResult.ForEach((item) => {

                result.Add((int)item);
            });

            return result;



        }
    }
}
