using MySql.Data.MySqlClient;
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
            return 0;
        }

        public int Update(string grounpId)
        {
            string sql = "update  grounp set runState =0  where id = @grounpId;";
            int count = MySqlExecuteTools.GetCountResult(sql, new MySqlParameter[] { new MySqlParameter("@grounpId", grounpId) });

            return 0;
        }
    }
}
