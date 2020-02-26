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
    public class ScoreDao
    {

        ILog Logger = log4net.LogManager.GetLogger("server.DAO.ScoreDao");
        public void SaveScoreResetLife(int grounpId)
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
                if (life.lifeValue < 0)
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
        public string SearchScore(int userId, string userType, bool isPlayerOrder = true, int currentScore = 0)
        {
            //只考虑玩家
            int roomId = GetRoomIdByUser(userId);
            string sql = "";
            //玩家的排行
            if (isPlayerOrder)
            {
                return SearchScoreByRoomId(roomId);
            }
            //room的排行
            else
            {
                int gournpId = -1;
                //玩家
                if (userType.Equals("0"))
                {
                    sql = "select grounpId from room where id = @roomId";

                    List<object> list = (MySqlExecuteTools.GetSingleFieldResult(sql, new MySqlParameter[] { new MySqlParameter("@roomId", roomId) }));
                    if (list == null || list.Count == 0)
                    {
                        return "";
                    }
                    gournpId = (int)(list[0]);
                }
                else
                {
                    sql = "select id from grounp where userId = @userId";
                    gournpId = (int)(MySqlExecuteTools.GetSingleFieldResult(sql, new MySqlParameter[] { new MySqlParameter("@userId", userId) })[0]);
                }


                sql = "select * from room where grounpId = @grounpId";
                //获取分组的
                List<Room> rooms = MySqlExecuteTools.GetObjectResult<Room>(sql, new MySqlParameter[] { new MySqlParameter("@grounpId", gournpId) });
                List<Score> scores = new List<Score>();
                rooms.ForEach((room) =>
                {
                    //求平均值
                    sql = " select avg(bulletCount),avg(lifeValue),avg(fightScore),createTime from score   where roomId = " + room.id;
                    List<object> result = (List<object>)(MySqlExecuteTools.GetMuchFieldResult(sql, new MySqlParameter[] { new MySqlParameter("@roomId", room.id) })[0]);
                    int bulletCount = 0;
                    if (result[0] != DBNull.Value)
                    {
                        bulletCount = Convert.ToInt16(result[0]);
                    }

                    int lifeValue = 0;
                    if (result[1] != DBNull.Value)
                    {
                        lifeValue = Convert.ToInt16(result[1]);
                    }

                    int fightScore = 0;

                    if (result[2] != DBNull.Value)
                    {
                        fightScore = Convert.ToInt16(result[2]);
                    }
                    int createTime = 0;
                    if (result[3] != DBNull.Value)
                    {
                        createTime = int.Parse(result[3].ToString());
                    }

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

        public string SearchScoreByRoomId(int roomId)
        {
            string sql = "select count(*) from room_user where room_id = @roomId";

            List<object> list = MySqlExecuteTools.GetSingleFieldResult(sql, new MySqlParameter[] { new MySqlParameter("@roomId", roomId) });
            //查出数量room下的玩家人数
            if (list.Count > 0)
            {
                int count = int.Parse((MySqlExecuteTools.GetSingleFieldResult(sql, new MySqlParameter[] { new MySqlParameter("@roomId", roomId) })[0].ToString()));


                sql = "select * from score where roomId = @roomId  order by createTime desc limit 0," + count;
                List<Score> scoreLsit = MySqlExecuteTools.GetObjectResult<Score>(sql, new MySqlParameter[] { new MySqlParameter("@roomId", roomId) });
                string result = Utils.CollectionsConvert.ToJSON(scoreLsit);
                return result;
            }
            return "";

        }

        private int GetRoomIdByUser(int userId)
        {
            string sql = "select room_id from room_user where user_id = @user_id";
            List<object> result = MySqlExecuteTools.GetSingleFieldResult(sql, new MySqlParameter[] { new MySqlParameter("@user_id", userId) });
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
    }
}
