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
    /// 创建删除修改
    /// </summary>
    public class EditRoomDao : RoomDao
    {
        ILog Logger = log4net.LogManager.GetLogger("server.DAO.EditRoomDao");

        private readonly int gounpCount = 10;
        public void AddRoom(PubgSession session, string body, string roomName,string userId,string area="shanxi")
        {
            Logger.InfoFormat("创建队：{0}", roomName);
            string sql = "select * from room where name = @name and userId = @userId";
            List<Room> result = MySqlExecuteTools.GetObjectResult<Room>(sql,
               // new MySqlParameter[] { new MySqlParameter("@name", roomName), new MySqlParameter("@area", room.area.Trim())});
               new MySqlParameter[] { new MySqlParameter("@name", roomName), new MySqlParameter("@userId", userId) });
            DataResult dataResult = new DataResult();
            if (result.Count >0)
            {
                dataResult.result = 1;
                dataResult.resean = "队名称已存在，请检查后重试。";
            }
            else
            {
                //创建房间
                sql = "insert into room(name,area,userId) " +
                    "values('" + roomName + "','" + area + "','" + userId + "')";
                long roomid = MySqlExecuteTools.GetAddID(sql);
                if(roomid!=-1)
                {
                    //创建分队
                    CreateGrounp(gounpCount, roomid);

                    dataResult.result = 0;
                    dataResult.data = null;
                }
                else
                {
                    dataResult.result = 1;
                    dataResult.data ="房间队失败，请重试！";
                }
              
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
        public void DeleteRoom(PubgSession session, string body, string  roomId)
        {

            DataResult dataResult = new DataResult();
            //查询能否删除

            List<Grounp> grounpList = SearchSingleRoomCommon(roomId);
            grounpList.ForEach((item) => {

                List<Grounp_User>  grounpUserList = SearchSingleGrounpCommon(item.id.ToString());
                if(grounpUserList.Count>0)
                {
                    dataResult.result = 1;
                    dataResult.resean = "该房间下存在用户，无法进行删除。";
                    session.Send(GetSendData(dataResult, body));
                    return;
                }

            });
            //开始删除
           string sql = "delete  from room where id = @roomId";
           MySqlExecuteTools.GetCountResult(sql, new MySqlParameter[] { new MySqlParameter("@roomId", roomId) });
            dataResult.result = 0;
            //删除队友的信息
            DeleteGrounp(roomId);
            session.Send(GetSendData(dataResult, body));
        }

        /// <summary>
        /// 更新房间
        /// </summary>
        /// <param name="session"></param>
        /// <param name="body"></param>
        /// <param name="room"></param>
        public void UpdateRoom(PubgSession session, string body, string roomName, string grounpName, string checkCode, string roomId, string grounpId)
        {
            //string sql = "update room set name = '" + roomName + "', area = '" + room.area + "' where id = @id;";
            //更新房间
            string sql = "update room set name = '" + roomName + "' where id = @roomId;";
            MySqlExecuteTools.GetCountResult(sql, new MySqlParameter[] { new MySqlParameter("@roomId", roomId) });

            //更新队友的信息
            sql = "update  grounp set name = '" + grounpName + "', checkCode = '" + checkCode + "' where id = @grounpId;";
            MySqlExecuteTools.GetCountResult(sql, new MySqlParameter[] { new MySqlParameter("@grounpId", grounpId) });

            //更新分队信息
            DataResult dataResult = new DataResult();
            dataResult.result = 0;
            session.Send(GetSendData(dataResult, body));

        }


        /// <summary>
        /// 查询所有的房间
        /// </summary>
        /// <returns></returns>
            private List<Room> GetAllRoom(int userId)
        {
            string sql = "select * from room where userId = @userId";
            List<Room> result = MySqlExecuteTools.GetObjectResult<Room>(sql, 
                new MySqlParameter[] { new MySqlParameter("@userId", userId) });
            return result;
        }

        /// <summary>
        /// 创建分队
        /// </summary>
        /// <param name="num">创建分队色数量</param>
        /// <param name="roomid">房间id</param>
        private void CreateGrounp(int num,long roomid,string defaultCheckCode="123456")
        {
            for(int i=0;i<num;i++)
            {
               string  sql = "insert into grounp(roomId,code,name,checkCode) " +
                   "values('" + roomid + "','" + (i+1) + "','" + "房间"+ (i+1) + "','" + defaultCheckCode + "')";
                MySqlExecuteTools.AddOrUpdate(sql);
            }
        }

        private  void DeleteGrounp(string roomId)
        {
            string sql = "select * from grounp where roomId = @roomId";
            List<Grounp> result = MySqlExecuteTools.GetObjectResult<Grounp>(sql,
                new MySqlParameter[] { new MySqlParameter("@roomId", roomId) });

            result.ForEach((item) => {

                int grounid = item.id;
                // 删除队信息
                string deleteGronpSql = "delete  from grounp  where id = @grounid";
                MySqlExecuteTools.GetCountResult(deleteGronpSql, new MySqlParameter[] { new MySqlParameter("@grounid", grounid) });

                //删除队和用户的关联表
                string delete_grounp_userSql = "delete  from  grounp_user  where grounp_id = @grounp_id";
                MySqlExecuteTools.GetCountResult(delete_grounp_userSql, new MySqlParameter[] { new MySqlParameter("@grounp_id", grounid) });
            });
        }

       


    }
}
