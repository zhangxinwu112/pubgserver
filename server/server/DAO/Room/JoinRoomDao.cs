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

        private readonly int maxNum = 5;
        public void JoinRoom(PubgSession session, string body, string grounpId,string userId)
        {
            Logger.InfoFormat("加入房间：{0},{1}", grounpId, userId);
            string sql = "select * from grounp_user where user_id = @user_id";
            List<Grounp_User> grounp_UserList = MySqlExecuteTools.GetObjectResult<Grounp_User>(sql,
               new MySqlParameter[] { new MySqlParameter("@user_id", userId) });
            DataResult dataResult = new DataResult();
            if (grounp_UserList.Count>0)
            {
                dataResult.result = 1;
                dataResult.resean = "您已经加入分队，不能重复。";
                session.Send(GetSendData(dataResult, body));
                return;
            }

            grounp_UserList = SearchSingleGrounpCommon(grounpId);
           
            if (grounp_UserList.Count> maxNum)
            {
                dataResult.result = 1;
                dataResult.resean = "分队人数加入已满，请重试。";
            }
            else
            {
                 sql = "insert into grounp_user(grounp_id,user_id) " +
                   "values('" + grounpId + "','" + userId + "')";
                    MySqlExecuteTools.AddOrUpdate(sql);
                dataResult.result = 0;
            }
            session.Send(GetSendData(dataResult, body));
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
            string sql = "select * from grounp_user where user_id = @user_id";
            List<Grounp_User> grounp_UserList = MySqlExecuteTools.GetObjectResult<Grounp_User>(sql,
               new MySqlParameter[] { new MySqlParameter("@user_id", userId) });

            grounp_UserList.ForEach((item) => {

                sql = "delete from grounp_user  where id = @id";
                MySqlExecuteTools.GetCountResult(sql, new MySqlParameter[] { new MySqlParameter("@id", item.id) });

            });

            DataResult dataResult = new DataResult();
            dataResult.result = 0;
            //查询能否删除
            session.Send(GetSendData(dataResult, body));
        }
    }
}
