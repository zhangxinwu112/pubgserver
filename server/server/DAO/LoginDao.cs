using log4net;
using mysql;
using MySql.Data.MySqlClient;
using server;
using server.Model;
using server.Tool;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server.DAO
{
    public  class LoginDao: CommonDao
    {
         ILog Logger =  log4net.LogManager.GetLogger("server.DAO.LoginDao");
        public void CheckLogin(PubgSession session, string body, string username, string password)
        {

            Logger.InfoFormat("用户登陆验证：{0}", username);
            string sql = "select * from user where telephone = @username and password = @password";
            List<UserName> result = MySqlExecuteTools.GetObjectResult<UserName> (sql, 
                new MySqlParameter[] { new MySqlParameter("@username", username), new MySqlParameter("@password", password) });
          
            DataResult dataResult = new DataResult();
            if(result.Count==0)
            {
                dataResult.result = 1;
                dataResult.resean = "账号或密码有误，请重试!";
            }
            else
            {
                bool isLogin = CheckIsLogin(result[0].id.ToString());
                if(isLogin)
                {
                    dataResult.result = 1;
                    dataResult.resean = "该账号已在线，不能重复登录，请重试!";
                }
                else
                {
                    dataResult.result = 0;

                    UserName userName = result[0];
                    sql = "select * from room where userId = @userId";
                     int count = MySqlExecuteTools.GetCountResult(sql, new MySqlParameter[] { new MySqlParameter("@userId", userName.id)});
                    if(count>0)
                    {
                        userName.isLeader = true;
                    }
                    dataResult.data = userName;
                }
               
            }
            session.Send(GetSendData(dataResult, body));
        }

        /// <summary>
        /// 查看session中是否有登录的用户
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        private bool CheckIsLogin(string userId)
        {
            ConcurrentDictionary<PubgSession, SessionItem> dic = PubgSession.mOnLineConnections;
            foreach (SessionItem sessionItem in dic.Values)
            {
                if(sessionItem.isLogin && sessionItem.userId.Trim().Equals(userId.Trim()))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
