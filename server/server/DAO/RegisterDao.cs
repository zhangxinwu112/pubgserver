﻿
using MySql.Data.MySqlClient;
using server.Model;
using server.Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  server.DAO
{
   public  class RegisterDao: CommonDao
    {
        public void RegisterUser(PubgSession session, string body, string telephone, string password, 
            string name, string icon, string checkCode,string userType)
        {
           // Logger.InfoFormat("新的客户端断开：{0}", session.RemoteEndPoint);


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
                int type = Convert.ToInt32(userType);
                dataResult.result = 0;
                sql = "insert into user(password,name ,telephone,image,type) " +
                    "values('" + password + "','" + name + "','" + telephone + "','" + icon + "','"+ type + "')";
                MySqlExecuteTools.AddOrUpdate(sql);
            }

            session.Send(GetSendData(dataResult, body));
        }
    }
}
