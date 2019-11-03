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
    }
}
