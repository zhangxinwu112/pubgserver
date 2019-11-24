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
    ///查询房间的关联数据
    /// </summary>
    public class SearchRoomDao : CommonDao
    {
        ILog Logger = log4net.LogManager.GetLogger("server.DAO.SearchRoomDao");
        public void SearchAllRoom(PubgSession session, string body, string  userId)
        {
            Logger.InfoFormat("查询用户下的所有房间：{0}", userId);
            string sql = "select * from room where userId = @userId";
            List<Room> result = MySqlExecuteTools.GetObjectResult<Room>(sql,
                new MySqlParameter[] { new MySqlParameter("@userId", userId)});
            DataResult dataResult = new DataResult();
            dataResult.result = 0;
            dataResult.data = result;
            session.Send(GetSendData(dataResult, body));
        }
    }


}
