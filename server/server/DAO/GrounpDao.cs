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
    public class GrounpDao:CommonDao
    {
        private ILog Logger = log4net.LogManager.GetLogger("server.DAO.GrounpDao");
        public void Update(PubgSession session, string body, Model.Grounp group)
        {
            string sql = "update grounp set name = '" + group.name + "', checkCode = '" + group.checkCode + "' where id = @id;";
            int result = MySqlExecuteTools.GetCountResult(sql, new MySqlParameter[] { new MySqlParameter("@id", group.id) });
            DataResult dataResult = new DataResult();
            if (result > 0)
            {
                dataResult.result = 0;
                dataResult.data = GetGrounpById(group.id);
            }
            else
            {
                dataResult.result = 1;
                dataResult.resean = "更新失败";
            }
            session.Send(GetSendData(dataResult, body));
        }

        private Grounp GetGrounpById(int grounpId)
        {
            string sql = "select * from grounp where id = @id";
            List<Grounp> result = MySqlExecuteTools.GetObjectResult<Grounp>(sql,
                new MySqlParameter[] { new MySqlParameter("@id", grounpId) });

            if(result.Count==1)
            {
                return result[0];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 通过group获取用户列表
        /// </summary>
        /// <param name="grounpId"></param>
        private  void GetUserByGrounp(PubgSession session, string body,int grounpId)
        {
            DataResult dataResult = new DataResult();

            string sql = "select * from grounp_user where grounp_id = @grounpId";
            List<Grounp_User> Grounp_User_List = MySqlExecuteTools.GetObjectResult<Grounp_User>(sql, new MySqlParameter[] { new MySqlParameter("@grounpId", grounpId) });
            List<string> ids = new List<string>();
            Grounp_User_List.ForEach((item) => {

                ids.Add(item.id.ToString());
            });

            string str = StrUtil.ConnetString(ids, ",");

            string userSql = "select * from user where id in  = ('" + str + "')";
            List<UserName> User_List = MySqlExecuteTools.GetObjectResult<UserName>(sql, null);
            dataResult.result = 0;
            dataResult.data = User_List;

            session.Send(GetSendData(dataResult, body));
        }

    }

    
    
}
