using server.Model;
using server.Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server.DAO
{
    public class CommonDao
    {

        protected  string GetSendData(DataResult dataResult,string body)
        {
            string resultJson = Utils.CollectionsConvert.ToJSON(dataResult);
            string sendata = "Post" + Constant.START_SPLIT + body + Constant.END_SPLIT + resultJson + "\r\n";
            return sendata;
        }

        protected Grounp GetGrounpByPlayer(int player)
        {
            string sql = "select p.* from grounp p join room r on p.id =r.grounpId join room_user ru on r.id = ru.room_id where ru.user_id=" + player;
            List<Grounp> ps = MySqlExecuteTools.GetObjectResult<Grounp>(sql, null);
            if (ps.Count > 0)
            {
                return ps.FirstOrDefault<Grounp>();
            }
            return null;
        }
    }
}
