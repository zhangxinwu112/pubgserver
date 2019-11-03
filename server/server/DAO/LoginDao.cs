using log4net;
using mysql;
using MySql.Data.MySqlClient;
using server;
using server.Model;
using server.Tool;
using System;
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
                dataResult.result = 0;
                dataResult.data = result[0];
            }
            session.Send(GetSendData(dataResult, body));
        }
    }
}
