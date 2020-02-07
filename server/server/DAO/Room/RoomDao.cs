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
        /// 通过room查询user
        /// </summary>
        /// <param name="grounpId"></param>
        /// <returns></returns>
        protected List<Room_User> SearchSingleGrounpCommon(string roomId)
        {
     
            string sql = "select * from room_user where room_id = @room_id";
            List<Room_User> result = MySqlExecuteTools.GetObjectResult<Room_User>(sql,
                new MySqlParameter[] { new MySqlParameter("@room_id", roomId) });
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
    }
}
