using server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using server.Tool;
using MySql.Data.MySqlClient;
using server.Business;

namespace server.DAO
{
    /// <summary>
    /// 
    /// </summary>
    public class UserRoomDao : CommonDao    {
        ILog Logger = log4net.LogManager.GetLogger("server.DAO.UserRoomDao");

        private PublishPlayerState publishPlayerState = new PublishPlayerState();
        //====================================================================
        //玩家： 没有启动的时候不刷新
        //启动的时候刷新
        //队长的话， 不断刷新
        //管理员刷新，变化推送。
       //======================================================================
        public string SetUserRoomState(string userID)
        {
            //是否有房间创建
            string sql = "select * from room where userId = @userId";
            List<Room> roomList = MySqlExecuteTools.GetObjectResult<Room>(sql,
                new MySqlParameter[] { new MySqlParameter("@userId",  int.Parse(userID))});
            //普通玩家
            if(roomList.Count == 0)
            {
                //更新玩家状态
                UpdateRoomUserState(userID);

                //该队中其他玩家，不包括玩家
                List<int> userList = GetUserListBySingleUser(int.Parse(userID));
                if(userList.Count>0)
                {
                    publishPlayerState.PublishPlayerList(userList);
                }

                //推送给队长
                int adminUser =  FindLeaderByPlayer(int.Parse(userID));
                publishPlayerState.SendSingleUserMessage(adminUser, PublishPlayerState.Update_Command);
                return "0";

            }
            //队长玩家
            else
            {
                Room room = roomList.FirstOrDefault<Room>();

                sql = "select * from room_user where room_id = @room_id";
                List<Room_User> room_UserList = MySqlExecuteTools.GetObjectResult<Room_User>(sql,
                    new MySqlParameter[] { new MySqlParameter("@room_id", room.id) });
                //房间里没有其他玩家加入
                if(room_UserList.Count==1)
                {
                    return "-2";
                }

                bool isFinished = CheckOtherPlayerState(room_UserList,  int.Parse( userID));
                //完成
                if(isFinished)
                {
                    //更新玩家状态
                    UpdateRoomUserState(userID);

                    //更新room状态
                    UpdateRoomState(userID, room);

                    //推送给队长自己
                    publishPlayerState.PubilshLoaderSelf(int.Parse(userID));
                    //推送管理员
                    publishPlayerState.PublishAdmin(room);
                    //推送给其他玩家
                    List<int> userIdList = GetOtherByLeader(userID);
                    if(userIdList!=null && userIdList.Count>0)
                    {
                        publishPlayerState.PublishPlayerList(userIdList);
                    }
                   
                    return "0";
                }
                // 未完成
                else
                {
                    return "-1";
                }

               
            }
        }

        /// <summary>
        /// 检查队长之外其他玩家的状态是否完成
        /// </summary>
        /// <returns></returns>
        private bool CheckOtherPlayerState(List<Room_User> Room_UserList,int userId)
        {
           var list  =  Room_UserList.Where((item) => item.user_id != userId && item.runState == -1);
            if(list.Count()>0)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 更新user的状态
        /// </summary>
        private void UpdateRoomUserState(string userID)
        {
            string sql = "update room_user set runState = 0  where user_id = " + userID;
            MySqlExecuteTools.AddOrUpdate(sql);
        }

        /// <summary>
        /// 更新room状态
        /// </summary>
        /// <param name="userID"></param>
        private void UpdateRoomState(string userID,Room room)
        {
            //更新room状态
           string sql = "update room set runState = 0 " + " where userId = " + userID;
           MySqlExecuteTools.AddOrUpdate(sql);
           //推送给管理员
          

        }

        /// <summary>
        /// 通过队长查询其他同队的用户
        /// </summary>
        /// <returns></returns>
        public List<int> GetOtherByLeader(string leaderUserId)
        {
            string sql = "select ru.user_id from room r join room_user ru on r.userId =  " + leaderUserId +
                " and ru.user_id <>"+ leaderUserId + " and r.id = ru.room_id";

            List<object> dataResult = MySqlExecuteTools.GetSingleFieldResult(sql,null);
            if(dataResult==null)
            {
                Console.WriteLine(sql +"查询结果为空");
                return null;
            }
            List<int> userIdList = new List<int>();

            dataResult.ForEach((item) => {

                userIdList.Add((int)item);
            });

            return userIdList;
          
        }


        /// <summary>
        /// /// <summary>
        /// 通过一个玩家获取同队的其他玩家,
        /// </summary>
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="isState">并且状态准备好</param>
        /// <returns></returns>
        public List<int> GetUserListBySingleUser(int userId,bool isState = true)
        {
            string sql = " select ru.user_id from room_user ru  " +
                "left join room r on r.id = ru.room_id   and ru.user_id = "+ userId + " where  ru.runState=0";

            if(!isState)
            {
               sql = " select ru.user_id from room_user ru  " +"left join room r on r.id = ru.room_id   and ru.user_id = " + userId ;
            }

            List<object> dataResult = MySqlExecuteTools.GetSingleFieldResult(sql, null);
            if(dataResult==null)
            {
                return null;
            }
            List<int> userIdList = new List<int>();

            dataResult.ForEach((item) => {

                userIdList.Add((int)item);
            });

            return userIdList;
        }


        public int FindLeaderByPlayer(int userId)
        {
            string sql = "select r.userId from room_user ru  left join room r on r.id = ru.room_id where ru.user_id =  " + userId;
            List<object> dataResult = MySqlExecuteTools.GetSingleFieldResult(sql, null);

            List<int> userIdList = new List<int>();

            dataResult.ForEach((item) => {

                userIdList.Add((int)item);
            });
            return userIdList[0];
        }
       
    }


  
}
