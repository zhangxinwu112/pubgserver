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

        private readonly int maxNum = 2;
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

        /// <summary>
        /// 查询grounp is full
        /// </summary>
        public void SearchEnterButtonState(PubgSession session, string body, string userId)
        {
            DataResult dataResult = new DataResult();

            string sql = "select * from grounp_user where user_id = @user_id";
            List<Grounp_User> grounp_UserList = MySqlExecuteTools.GetObjectResult<Grounp_User>(sql,
               new MySqlParameter[] { new MySqlParameter("@user_id", userId) });

            dataResult.result = 0;
            //没有加入房间
            if (grounp_UserList.Count==0)
            {
                dataResult.data = false;
            }

            if(grounp_UserList.Count==1)
            {
                int grounp_id = grounp_UserList[0].grounp_id;

                sql = "select * from grounp_user where grounp_id = @grounp_id";
                List<Grounp_User> _grounp_UserList = MySqlExecuteTools.GetObjectResult<Grounp_User>(sql,
                   new MySqlParameter[] { new MySqlParameter("@grounp_id", grounp_id) });
                if(_grounp_UserList.Count< maxNum)
                {
                    dataResult.data = false;
                }
                else
                {
                    dataResult.data = true;
                }
            }

            session.Send(GetSendData(dataResult, body));
        }

        public void CheckEnterButton(PubgSession session, string body, string checkCode, string userId)
        {
            DataResult dataResult = new DataResult();
            string sql = "select * from grounp_user where user_id = @user_id";
            List<Grounp_User> grounp_UserList = MySqlExecuteTools.GetObjectResult<Grounp_User>(sql,
              new MySqlParameter[] { new MySqlParameter("@user_id", userId) });

            if(grounp_UserList.Count==1)
            {
                sql = "select * from grounp where id = @id  and checkCode = @checkCode";
                List<Grounp> grounps = MySqlExecuteTools.GetObjectResult<Grounp>(sql,
                    new MySqlParameter[] { new MySqlParameter("@id", grounp_UserList[0].grounp_id), new MySqlParameter("@checkCode", checkCode) });
                if(grounps.Count==1)
                {
                    dataResult.result = 0;
                    session.Send(GetSendData(dataResult, body));
                    return;
                }
            }
            dataResult.result = 1;
            session.Send(GetSendData(dataResult, body));
        }
    }


}
