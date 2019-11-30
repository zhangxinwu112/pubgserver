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
        /// 通过room查询grounp
        /// </summary>
        /// <param name="roomId"></param>
        /// <returns></returns>
        protected List<Grounp> SearchSingleRoomCommon(string roomId)
        {
            
            string sql = "select * from grounp where roomId = @roomId ORDER BY id ASC";
            List<Grounp> result = MySqlExecuteTools.GetObjectResult<Grounp>(sql,
                new MySqlParameter[] { new MySqlParameter("@roomId", roomId) });

            return result;

        }

        /// <summary>
        /// 通过group查询user
        /// </summary>
        /// <param name="grounpId"></param>
        /// <returns></returns>
        protected List<Grounp_User> SearchSingleGrounpCommon(string grounpId)
        {
     
            string sql = "select * from grounp_user where grounp_id = @grounp_id";
            List<Grounp_User> result = MySqlExecuteTools.GetObjectResult<Grounp_User>(sql,
                new MySqlParameter[] { new MySqlParameter("@grounp_id", grounpId) });
            return result;
        }
    }
}
