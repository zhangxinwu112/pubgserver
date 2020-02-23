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
    public class ScoreDao
    {

        ILog Logger = log4net.LogManager.GetLogger("server.DAO.ScoreDao");
        public  void SaveScoreResetLife(int grounpId)
        {
            string sql = "select  l.* from room r join room_user ru on r.id = ru.room_id join life l on ru.user_id = l.userId where r.grounpId = @grounpId";

            List<Life> lifeList = MySqlExecuteTools.GetObjectResult<Life>(sql,
                new MySqlParameter[] { new MySqlParameter("@grounpId", grounpId) });

            lifeList.ForEach((life) =>
            {
                //查询roomid
              
                int roomId = GetRoomIdByUser(life.userId);

                //查询userName
                sql = "select name from user where id = @user_id";
                string userName = MySqlExecuteTools.GetSingleFieldResult(sql, new MySqlParameter[] { new MySqlParameter("@user_id", life.userId) })[0].ToString();
                if(life.lifeValue<0)
                {
                    life.lifeValue = 0;
                }
                //插入值
                sql = "insert into score(createTime,bulletCount,lifeValue,fightScore,roomId,grounpId,userId,userName) " +
                 "values('" + TimeUtils.GetCurrentTimestamp() + "','" + life.bulletCount + "','" + life.lifeValue + "'" +
                 ",'" + life.fightScore + "','" + roomId + "','" + grounpId + "','" + life.userId + "','" + userName + "')";
                MySqlExecuteTools.GetAddID(sql);


                //复位life的生命值

                sql = "update life set bulletCount = 80，lifeValue=50，fightScore=35  where id = @id;";
                MySqlExecuteTools.GetCountResult(sql, new MySqlParameter[] { new MySqlParameter("@id", life.id) });


            });

        }

        /// <summary>
        /// 查询成绩
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="currentScore">0，为当前</param>
        ///  <param name="isPlayerOrder">是否为玩家排行</param>
        public string SearchScore(int userId,bool isPlayerOrder = true, int currentScore = 0)
        {
            //只考虑玩家
            int roomId = GetRoomIdByUser(userId);
            string sql = "";
            //玩家的排行
            if (isPlayerOrder)
            {
                sql = "select * from score where roomId = @roomId";
                List<Score>  scoreLsit = MySqlExecuteTools.GetObjectResult<Score>(sql, new MySqlParameter[] { new MySqlParameter("@roomId", roomId) });
                string json = Utils.CollectionsConvert.ToJSON(scoreLsit);
                return json;
            }
            //room的排行
            else
            {
                 sql = "select grounpId from room where id = @roomId";
                int gournpId = (int)(MySqlExecuteTools.GetSingleFieldResult(sql, new MySqlParameter[] { new MySqlParameter("@roomId", roomId) })[0]);

                sql = "select * from room where grounpId = @grounpId";
                //获取分组的
                List<Room> rooms = MySqlExecuteTools.GetObjectResult<Room>(sql, new MySqlParameter[] { new MySqlParameter("@grounpId", gournpId) });
                List<Score> scores = new List<Score>();
                rooms.ForEach((room) => {
                    //求平均值
                    sql = " select avg(bulletCount),avg(lifeValue),avg(fightScore),createTime from score   where roomId = " + room.id;
                     List<object>  result =  (List<object>)(MySqlExecuteTools.GetMuchFieldResult(sql, new MySqlParameter[] { new MySqlParameter("@roomId", room.id) })[0]);
                    int bulletCount = Convert.ToInt16(result[0]);
                    Console.WriteLine(bulletCount);
                    int lifeValue = Convert.ToInt16(result[1]);
                    int fightScore = Convert.ToInt16(result[2]);
                    int createTime = int.Parse(result[3].ToString());
                    Score score = new Score();
                    score.bulletCount = bulletCount;
                    score.lifeValue = lifeValue;
                    score.fightScore = fightScore;
                    score.userName = room.name;
                    score.createTime = createTime;
                    scores.Add(score);
                });

                 return Utils.CollectionsConvert.ToJSON(scores);
            }
            
        }

        private int GetRoomIdByUser(int userId)
        {
            string sql = "select room_id from room_user where user_id = @user_id";
            List<object> result = MySqlExecuteTools.GetSingleFieldResult(sql, new MySqlParameter[] { new MySqlParameter("@user_id", userId) });
            if(result.Count>0)
            {
                int roomId = int.Parse(result[0].ToString());

                return roomId;
            }
            return -1;
           
        }
    }
}
