
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
    public class UserDao : CommonDao
    {
        public void RegisterUser(PubgSession session, string body, string telephone, string password,
            string name, string icon, string checkCode, string userType)
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
                    "values('" + password + "','" + name + "','" + telephone + "','" + icon + "','" + type + "')";
                long newuserId = MySqlExecuteTools.GetAddID(sql);

                //玩家,增加生命信息
                if (type == 0)
                {
                    sql = "insert into life(userId) " +
                   "values('" + newuserId + "')";
                    MySqlExecuteTools.AddOrUpdate(sql);
                }
                //管理员，增加一条grounp数据
                if(type == 1)
                {
                    sql = "insert into grounp(name,userId) " +
                   "values('" + name + "','" + newuserId + "')";
                    MySqlExecuteTools.AddOrUpdate(sql);
                }
            }

            session.Send(GetSendData(dataResult, body));
        }

        public Life GetLifeById(int userId)
        {
            string sql = "select * from life where userId = @userId";
            Life life = MySqlExecuteTools.GetObjectResult<Life>(sql,
                new MySqlParameter[] { new MySqlParameter("@userId", userId) }).FirstOrDefault<Life>();

            return life;
        }

        /// <summary>
        /// 增加或减少命值
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="SubTractValue"></param>
        /// <param name="isSub">时候减少</param>
        public void SetLifeValue(string userId, int SubTractValue, bool isSub = true)
        {

            string sql = "select * from life where userId = @userId";
            List<Life> lifes = MySqlExecuteTools.GetObjectResult<Life>(sql,new MySqlParameter[] { new MySqlParameter("@userId", userId) });
            if(lifes.Count>0)
            {
                Life life = MySqlExecuteTools.GetObjectResult<Life>(sql,
                new MySqlParameter[] { new MySqlParameter("@userId", userId) }).FirstOrDefault<Life>();
               

                if (life != null)
                {
                    if (isSub)
                    {
                        int subValue = life.lifeValue - SubTractValue;
                        if (subValue < 0)
                        {
                            subValue = 0;
                        }
                        sql = "update  life set lifeValue  =" + (subValue) + " where id = @id";

                    }
                    else
                    {
                        sql = "update  life set lifeValue  =" + (life.lifeValue + SubTractValue) + " where id = @id;";
                    }

                    MySqlExecuteTools.GetCountResult(sql, new MySqlParameter[] { new MySqlParameter("@id", life.id) });
                }

            }
            else
            {
                Console.WriteLine(sql + " :is null");
            }
           
        }

        public void UpdateLifeValue(int lifeValue,int bulletCountValue,string userId)
        {
         
          string sql =   "update life set lifeValue = '" + lifeValue + "', bulletCount = '" + bulletCountValue  + "' where userId = "+ userId;
          MySqlExecuteTools.AddOrUpdate(sql);



        }
    }


  
}
