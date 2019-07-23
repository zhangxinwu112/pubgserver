using MySql.Data.MySqlClient;
using server.Model;
using server.Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  server
{
   public  class Register
    {
        public void RegisterUser(PubgSession session, string body, string telephone, string password, string nick, string icon, string checkCode)
        {
            string sql = "select * from user where telephone = @telephone";
            int result = MySqlExecuteTools.GetCountResult(sql,
                new MySqlParameter[] { new MySqlParameter("@telephone", telephone) });

            DataResult dataResult = new DataResult();
            if (result > 0)
            {
                dataResult.result = 1;
                dataResult.resean = "手机号码已注册，请重试";
            }
            else
            {
                dataResult.result = 0;
                sql = "insert into user(password,nick ,telephone,image) " +
                    "values('" + password + "','" + nick + "','" + telephone + "','" + icon + "')";
                MySqlExecuteTools.AddOrUpdate(sql);
            }

            string resultJson = Utils.CollectionsConvert.ToJSON(dataResult);
            string sendata = "Post" + Constant.START_SPLIT + body + Constant.END_SPLIT + resultJson + "\r\n";
            session.Send(sendata);
        }
    }
}
