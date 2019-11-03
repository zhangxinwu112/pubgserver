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
    public class RoomDao: CommonDao
    {
        ILog Logger = log4net.LogManager.GetLogger("server.DAO.RoomDao");
        public void Add(PubgSession session, string body, Model.Room room)
        {
            Logger.InfoFormat("创建房间：{0}", room.name);
            string sql = "select * from room where name = @name and area = @area";
            List<Room> result = MySqlExecuteTools.GetObjectResult<Room>(sql,
                new MySqlParameter[] { new MySqlParameter("@name", room.name.Trim()), new MySqlParameter("@area", room.area.Trim())});
            DataResult dataResult = new DataResult();
            if (result.Count >0)
            {
                dataResult.result = 1;
                dataResult.resean = "房间名称已存在，请检查后重试。";
            }
            else
            {
                
                //创建房间
                sql = "insert into room(name,area,userId) " +
                    "values('" + room.name + "','" + room.area + "','" + room.userId + "')";
                int roomid = MySqlExecuteTools.GetAddID(sql);
                if(roomid!=-1)
                {
                    //创建分队
                    CreateGrounp(5, roomid);

                    dataResult.result = 0;
                    dataResult.data = GetAllRoom(room.userId);
                }
                else
                {
                    dataResult.result = 1;
                    dataResult.data ="房间创建失败，请重试！";
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
        public void DeleteRoom(PubgSession session, string body, int  id,int userId)
        {
            string sql = "delete  from room where id = @id";

             int result = MySqlExecuteTools.GetCountResult(sql, new MySqlParameter[] { new MySqlParameter("@id", id) });

            DataResult dataResult = new DataResult();
            if(result>0)
            {
              
                dataResult.result = 0;
                //删除队友的信息
                DeleteGrounp(id);


                dataResult.data = GetAllRoom(userId);
            }
            else
            {
                dataResult.result = 0;
                dataResult.data = "删除失败";
            }

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
        private void CreateGrounp(int num,int roomid,string defaultCheckCode="123456")
        {
            for(int i=0;i<num;i++)
            {
               string  sql = "insert into grounp(roomId,code,name,checkCode) " +
                   "values('" + roomid + "','" + (num+1) + "','" + "房间"+ (num+1) + "','" + defaultCheckCode + "')";
                MySqlExecuteTools.AddOrUpdate(sql);
            }
        }

        private  void DeleteGrounp(int roomId)
        {
            string sql = "select * from grounp where roomId = @roomId";
            List<Grounp> result = MySqlExecuteTools.GetObjectResult<Grounp>(sql,
                new MySqlParameter[] { new MySqlParameter("@roomId", roomId) });

            result.ForEach((item) => {

                int grounid = item.id;
                // 删除队信息
                string deleteGronpSql = "delete  from grounp  where id = @id";
                MySqlExecuteTools.GetCountResult(deleteGronpSql, new MySqlParameter[] { new MySqlParameter("@id", grounid) });

                //删除队和用户的关联表
                string delete_grounp_userSql = "delete  from  grounp_user  where grounp_id = @grounp_id";
                MySqlExecuteTools.GetCountResult(delete_grounp_userSql, new MySqlParameter[] { new MySqlParameter("@grounp_id", grounid) });



            });
        }
    }
}
