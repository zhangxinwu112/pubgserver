using log4net;
using MySql.Data.MySqlClient;
using server.Model;
using server.Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace server.DAO
{
    public class LifeDao
    {

        ILog Logger = log4net.LogManager.GetLogger("server.DAO.LifeDao");

        private int GetRoomIdByUser(int userId)
        {
            string sql = "select room_id from room_user where user_id = @user_id";
            List<object> result = MySqlExecuteTools.GetSingleFieldResult(sql, new MySqlParameter[] { new MySqlParameter("@user_id", userId) });
            Console.WriteLine(sql);
            if (result.Count > 0)
            {
                int roomId = int.Parse(result[0].ToString());

                return roomId;
            }
            return -1;

        }

        public string GetRoomLifeInfoByUser(string userId)
        {
            //只考虑玩家
            int roomId = GetRoomIdByUser(int.Parse(userId));

            string sql = "select user_id from room_user where room_id = @roomId";
            //获取用户列表
            List<object> list = MySqlExecuteTools.GetSingleFieldResult(sql, new MySqlParameter[] { new MySqlParameter("@roomId", roomId) });


            if (list.Count > 0)
            {
                List<string> resultStr = new List<string>();
                list.ForEach((id) =>
                {

                    resultStr.Add(id.ToString());
                });
                string result = StrUtil.ConnetString(resultStr, ",");
                sql = "select * from life where userId in  (" + result + ")";
                List<Life> lifes = MySqlExecuteTools.GetObjectResult<Life>(sql, null);
                lifes.ForEach((life) =>
                {

                    life.userName = MySqlExecuteTools.GetSingleFieldResult("select name from user  where id = @id", new MySqlParameter[] { new MySqlParameter("@id", life.userId) })[0].ToString();


                });

                return Utils.CollectionsConvert.ToJSON(lifes);
            }
            else
            {
                return "";
            }



        }

        public string GetPlayerInfoByUser(string userId)
        {
            string sql = "select * from life where userId =@userId";
            List<Life> lifes = MySqlExecuteTools.GetObjectResult<Life>(sql, new MySqlParameter[] { new MySqlParameter("@userId", userId) });
            if(lifes!=null && lifes.Count>0)
            {
                Life life = lifes[0];
                life.userName = MySqlExecuteTools.GetSingleFieldResult("select name from user  where id = @id",
               new MySqlParameter[] { new MySqlParameter("@id", life.userId) })[0].ToString();

                string json = Utils.CollectionsConvert.ToJSON(life);
                Logger.Debug(json);
                return json; ;

              
            }
            Logger.Debug("GetPlayerInfoByUser back is null");
            return "";

        }
        public string AddPlayerLife(string userId, string addLifeValue, string currentUser)
        {
            string sql = "select * from life  where userId = @userId";
            Life currrentlife =  MySqlExecuteTools.GetObjectResult<Life>(sql,
               new MySqlParameter[] { new MySqlParameter("@userId", currentUser) })[0];
            if(currrentlife.lifeValue< int.Parse(addLifeValue))
            {
                return "1";
            }
            Life addlife = MySqlExecuteTools.GetObjectResult<Life>(sql,
               new MySqlParameter[] { new MySqlParameter("@userId", userId) })[0];


            sql = "update life set lifeValue = '" + (addlife.lifeValue + int.Parse(addLifeValue)) +  "' where userId = "+ userId;
            MySqlExecuteTools.GetCountResult(sql);

            sql = "update life set lifeValue = '" + (currrentlife.lifeValue - int.Parse(addLifeValue)) + "' where userId = "+ currentUser;
            MySqlExecuteTools.AddOrUpdate(sql);


            return "0";
        }
    }
}
