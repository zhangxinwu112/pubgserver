using log4net;
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
    public class Grounp_User_Dao:CommonDao
    {
        private  ILog Logger = log4net.LogManager.GetLogger("server.DAO.Grounp_User_Dao");

        public void Add(PubgSession session, string body, int grounpId, int userId)
        {
            bool  result  = CheckGrounpCount(grounpId);
            DataResult dataResult = new DataResult();

            if (result)
            {
                string sql = "insert into grounp_user(grounp_id,user_id) " +
                 "values('" + grounpId + "','" + userId + "')";
                int count = MySqlExecuteTools.AddOrUpdate(sql);
                if (count > 0)
                {
                    dataResult.result = 0;
                    dataResult.resean = "组队成功";
                }
                else
                {
                    dataResult.result = 1;
                    dataResult.resean = "操作失败，请重试.";
                }
            }
            else
            {
                dataResult.result = 1;
                dataResult.resean = "该队用户已满，请重试.";

            }
            session.Send(GetSendData(dataResult, body));
        }

        private bool CheckGrounpCount(int grounpId,int num=5)
        {
            string sql = "select * from grounp_user where grounp_id = @grounpId";
             int count = MySqlExecuteTools.GetCountResult(sql, new MySqlParameter[] { new MySqlParameter("@grounpId", grounpId) });
            if(count< num+1)
            {
                return true;
            }

            return false;


        }
    }
}
