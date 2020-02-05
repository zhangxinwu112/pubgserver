using server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using server.Tool;
using MySql.Data.MySqlClient;

namespace server.DAO
{
    /// <summary>
    /// 创建删除修改队
    /// </summary>
    public class EditGrounpDao : RoomDao
    {
        ILog Logger = log4net.LogManager.GetLogger("server.DAO.EditGrounpDao");

        private readonly int roomCount = 5;

        private readonly int createGrounpCount = 2;

        private JoinRoomDao joinRoomDao = new JoinRoomDao();
        public void AddGrounp(PubgSession session, string body, string grounpName,string playerTime, string userId,string area="shanxi")
        {
            Logger.InfoFormat("创建队：{0}", grounpName);
            DataResult dataResult = new DataResult();
            string sql = "select * from grounp where userId = @userId";
            List<Grounp> result = MySqlExecuteTools.GetObjectResult<Grounp>(sql,
              new MySqlParameter[] { new MySqlParameter("@userId", userId) });

            if(result.Count>= createGrounpCount)
            {
                dataResult.result = 1;
                dataResult.resean = "您的权限最多创建"+ createGrounpCount + "个分队，请检查后重试。";
                session.Send(GetSendData(dataResult, body));

                return;
            }

            sql = "select * from grounp where name = @name and userId = @userId";
             result = MySqlExecuteTools.GetObjectResult<Grounp>(sql,
               // new MySqlParameter[] { new MySqlParameter("@name", roomName), new MySqlParameter("@area", room.area.Trim())});
               new MySqlParameter[] { new MySqlParameter("@name", grounpName), new MySqlParameter("@userId", userId) });
          
            if (result.Count >0)
            {
                dataResult.result = 1;
                dataResult.resean = "队名称已存在，请检查后重试。";
            }
            else
            {
                //创建房间
                sql = "insert into grounp(name,runState,playerTime,area,userId) " +
                    "values('" + grounpName + "','-1','" + playerTime + "','" + area + "','" + userId + "')";
                long roomid = MySqlExecuteTools.GetAddID(sql);
                if(roomid!=-1)
                {
                    //创建房间
                    CreateRoom(roomCount, roomid);

                    dataResult.result = 0;
                    dataResult.data = null;
                }
                else
                {
                    dataResult.result = 1;
                    dataResult.data ="创建失败，请重试！";
                }
              
            }
            if(dataResult.result == 0)
            {
                joinRoomDao.GetAllRoom();
            }
            session.Send(GetSendData(dataResult, body));
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="session"></param>
        /// <param name="body"></param>
        /// <param name="id">roomid</param>
        /// <param name="userId">用户id</param>
        public void DeleteGrounp(PubgSession session, string body, string  grounpId)
        {

            DataResult dataResult = new DataResult();
            //查询能否删除

            List<Room> roomList = SearchSingleRoomCommon(grounpId);
            if(roomList==null || roomList.Count==0)
            {
                dataResult.result = 1;
                dataResult.resean = "非法操作，无法进行删除。";
                session.Send(GetSendData(dataResult, body));
                return;
            }
     
            foreach(Room item in roomList)
            {
                List<Room_User> roomUserList = SearchSingleGrounpCommon(item.id.ToString());
                if (roomUserList.Count > 0)
                {
                    dataResult.result = 1;
                    dataResult.resean = "该房间下存在用户，无法进行删除。";
                    session.Send(GetSendData(dataResult, body));

                    return;
                }
            }
            
            //开始删除队信息
           string sql = "delete  from grounp where id = @grounpId";
           MySqlExecuteTools.GetCountResult(sql, new MySqlParameter[] { new MySqlParameter("@grounpId", grounpId) });
           dataResult.result = 0;
            //删除房间的相关数据
            DeleteRoom(grounpId);
            if (dataResult.result == 0)
            {
                joinRoomDao.GetAllRoom();
            }
            session.Send(GetSendData(dataResult, body));
        }

        /// <summary>
        /// 更新房间
        /// </summary>
        /// <param name="session"></param>
        /// <param name="body"></param>
        /// <param name="room"></param>
        public void UpdateGrounp(PubgSession session, string body, string _grounpName, string playerTime,string _roomName, string checkCode, string _grounpId, string _roomId)
        {
            //string sql = "update room set name = '" + roomName + "', area = '" + room.area + "' where id = @id;";
            //更新队
            string sql = "update grounp set name = '" + _grounpName + "', playerTime = '" + playerTime + "' where id = @grounpId;";
            MySqlExecuteTools.GetCountResult(sql, new MySqlParameter[] { new MySqlParameter("@grounpId", _grounpId) });

            //更新房间的信息
            sql = "update  room set name = '" + _roomName + "', checkCode = '" + checkCode + "' where id = @roomId;";
            MySqlExecuteTools.GetCountResult(sql, new MySqlParameter[] { new MySqlParameter("@roomId", _roomId) });

            //更新分队信息
            DataResult dataResult = new DataResult();
            dataResult.result = 0;
            session.Send(GetSendData(dataResult, body));

        }


        /// <summary>
        /// 查询所有的队
        /// </summary>
        /// <returns></returns>
            private List<Grounp> GetAllRoom(int userId)
        {
            string sql = "select * from gronp where userId = @userId";
            List<Grounp> result = MySqlExecuteTools.GetObjectResult<Grounp>(sql, 
                new MySqlParameter[] { new MySqlParameter("@userId", userId) });
            return result;
        }

        /// <summary>
        /// 创建房间
        /// </summary>
        /// <param name="num">创建分队色数量</param>
        /// <param name="roomid">房间id</param>
        private void CreateRoom(int num,long roomid,string defaultCheckCode="123456")
        {
            for(int i=0;i<num;i++)
            {
               string  sql = "insert into room(grounpId,code,name,checkCode) " +
                   "values('" + roomid + "','" + (i+1) + "','" + "房间"+ (i+1) + "','" + defaultCheckCode + "')";
                MySqlExecuteTools.AddOrUpdate(sql);
            }
        }

        private  void DeleteRoom(string grounpId)
        {
            string sql = "select * from room where grounpId = @grounpId";
            List<Room> result = MySqlExecuteTools.GetObjectResult<Room>(sql,
                new MySqlParameter[] { new MySqlParameter("@grounpId", grounpId) });

            result.ForEach((item) => {

                int roomId = item.id;
                // 删除房间信息
                string deleteGronpSql = "delete  from room  where id = @roomId";
                MySqlExecuteTools.GetCountResult(deleteGronpSql, new MySqlParameter[] { new MySqlParameter("@roomId", roomId) });

                //删除房间和用户的关联表
                string delete_grounp_userSql = "delete  from  room_user  where room_id = @roomId";
                MySqlExecuteTools.GetCountResult(delete_grounp_userSql, new MySqlParameter[] { new MySqlParameter("@roomId", roomId) });
            });
        }

       


    }
}
