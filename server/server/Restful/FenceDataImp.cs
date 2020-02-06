using MySql.Data.MySqlClient;
using server.DAO;
using server.Model;
using server.Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using System.Threading.Tasks;

namespace Restful
{
   
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Single, IncludeExceptionDetailInFaults = true)]
     [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class FenceDataImp : IFenceData
    {
        private JoinRoomDao joinRoomDao = new JoinRoomDao();
        public int Save(string json)
        {

            string[] strs = json.Split('|');

            string grounpId = strs[0];

            string fenceLon = strs[1];

            string fenceLat = strs[2];

            string fenceRadius = strs[3];

            string sql = "update  grounp set fenceLon = '" + fenceLon + "', fenceLat = '" + fenceLat +
                "', fenceRadius = '" + fenceRadius + "' where id = @grounpId;";
            int count = MySqlExecuteTools.GetCountResult(sql, new MySqlParameter[] { new MySqlParameter("@grounpId", grounpId) });
            // Console.WriteLine(count);
            joinRoomDao.GetAllRoom();
            return 0;
        }

        /// <summary>
        /// 设置grounp运行状态
        /// </summary>
        /// <param name="grounpId"></param>
        /// <returns></returns>
        public string UpdateState(string grounpId)
        {
            string sql = "select * from grounp where id = @grounpId";
            List<Grounp> result = MySqlExecuteTools.GetObjectResult<Grounp>(sql,
                new MySqlParameter[] { new MySqlParameter("@grounpId", grounpId) });


            if(result.Count==1)
            {
                if(result[0].fenceLat<=0)
                {
                    return "电子围栏尚未设置，无法启动游戏！";
                }
            }
            else
            {
                return "非法错误";
            }

            sql = "update  grounp set runState =0  where id = @grounpId;";
            MySqlExecuteTools.GetCountResult(sql, new MySqlParameter[] { new MySqlParameter("@grounpId", grounpId) });

            return "0";
        }
    }
}
