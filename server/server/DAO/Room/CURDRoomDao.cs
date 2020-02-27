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
    public class CURDRoomDao : RoomDao
    {
        ILog Logger = log4net.LogManager.GetLogger("server.DAO.CURDRoomDao");
        public void CreateEditRoom(PubgSession session, string body,string grounpId, string gamePassword,string roomId, 
            string roomName,string checkCode,string userId)
        {
            Logger.InfoFormat("创建编辑房间：{0},{1},{2},{3}", grounpId, roomId, roomName, checkCode);
            DataResult dataResult = new DataResult();
            string sql = "select * from grounp where id = @id and checkCode = @checkCode";
            int result =  MySqlExecuteTools.GetCountResult(sql, new MySqlParameter[] { new MySqlParameter("@id", grounpId) , new MySqlParameter("@@checkCode", gamePassword) });
            if(result==0)
            {
                dataResult.result = 1;
                dataResult.resean = "游戏密码错误，操作失败";
                session.Send(GetSendData(dataResult, body));
                return;
            }
            //ADD
            if (roomId.Equals("-1"))
            {
                //每个用户只能创建一个房间

                sql = "select * from room where userId = @userId";

                int roomCount = MySqlExecuteTools.GetCountResult(sql, new MySqlParameter[] { new MySqlParameter("@userId", userId) });
                if(roomCount>=1)
                {
                    dataResult.result = 1;
                    dataResult.resean = "创建失败，每个队长只能创建一个战队";
                    session.Send(GetSendData(dataResult, body));
                    return;
                }
                //不能加入其他队还进行队的创建

                sql = "select * from room_user where user_id = @userId";

                roomCount = MySqlExecuteTools.GetCountResult(sql, new MySqlParameter[] { new MySqlParameter("@userId", userId) });
                if(roomCount>0)
                {
                    dataResult.result = 1;
                    dataResult.resean = "创建失败，您已经加入战队，无法再次创建";
                    session.Send(GetSendData(dataResult, body));
                    return;
                }

                //创建房间
                sql = "insert into room(grounpId,name,checkCode,userId) " +
                   "values('" + grounpId + "','" + roomName + "','" + checkCode + "','" + userId + "')";
                 long newRoomId =   MySqlExecuteTools.GetAddID(sql);

                //用户加入到该房间
                sql = "insert into room_user(room_id,user_id) " + "values('" + newRoomId + "','" + userId + "')";
                MySqlExecuteTools.AddOrUpdate(sql);
      
            }
            //更新
            else
            {
               sql = "update room set name = '" + roomName + "', checkCode = '" +  checkCode + "' where id = @roomid;";
                MySqlExecuteTools.GetCountResult(sql, new MySqlParameter[] { new MySqlParameter("@roomid", roomId) });
            }
           
            dataResult.result = 0;
            session.Send(GetSendData(dataResult, body));
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="session"></param>
        /// <param name="body"></param>
        /// <param name="id">roomid</param>
        /// <param name="userId">用户id</param>
        public void DeleteRoom(PubgSession session, string body, string roomId,string userId)
        {
            DataResult dataResult = new DataResult();

            //判断当前用户是具有删除的权限

            string sql = "select * from room where id = @room_id and userId = @userId";

            int roomCount = MySqlExecuteTools.GetCountResult(sql,
            new MySqlParameter[] { new MySqlParameter("@room_id", roomId), new MySqlParameter("@userId", userId) });

            if(roomCount==0)
            {
                dataResult.result = 1;
                dataResult.resean = "操作错误，无删除权限";
                session.Send(GetSendData(dataResult, body));
                return;
            }


            sql = "select * from room_user where room_id = @room_id and user_id <>  @userId";
            int countResult = MySqlExecuteTools.GetCountResult(sql,
            new MySqlParameter[] { new MySqlParameter("@room_id", roomId), new MySqlParameter("@userId", userId) });

            if(countResult > 0)
            {
                dataResult.result = 1;
                dataResult.resean = "战队下有玩家用户，无法进行删除";
                session.Send(GetSendData(dataResult, body));
                return;
            }
           
            //删除队长创建的room
            sql = "delete from room  where id = @id";
            MySqlExecuteTools.GetCountResult(sql, new MySqlParameter[] { new MySqlParameter("@id", roomId) });

            //删除room_user

            sql = "delete from room_user  where user_id = @user_id";
            MySqlExecuteTools.GetCountResult(sql, new MySqlParameter[] { new MySqlParameter("@user_id", userId) });

            dataResult.result = 0;
            //查询能否删除
            session.Send(GetSendData(dataResult, body));
         
        }

       
    }


    


}
