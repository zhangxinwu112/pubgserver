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

namespace server
{
    public  class Login
    {
        public void CheckLogin(PubgSession session, string body, string username, string password)
        {

            string sql = "select * from user where telephone = @username and password = @password";
            int result = MySqlExecuteTools.GetCountResult(sql, 
                new MySqlParameter[] { new MySqlParameter("@username", username), new MySqlParameter("@password", password) });
          
            DataResult dataResult = new DataResult();
            if(result==0)
            {
                dataResult.result = 1;
                dataResult.resean = "账号或密码有误，请重试!";
            }
            else
            {
                dataResult.result = 0;
            }

            string resultJson = Utils.CollectionsConvert.ToJSON(dataResult);
            string sendata = "Post"+ Constant.START_SPLIT + body + Constant.END_SPLIT + resultJson + Environment.NewLine;
            session.Send(sendata);
        }
    }
}
