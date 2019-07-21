using mysql;
using MySql.Data.MySqlClient;
using server;
using server.Model;
using server.Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server
{
    public  class Code
    {
        public void CheckCode(PubgSession session,string body,string code,string 
            deviceUniqueIdentifier,string plat,string system)
        {
            string sql = "select * from code where name = @name";
            List<CodeModel> result = MySqlExecuteTools.GetObjectResult<CodeModel>(sql,
                new MySqlParameter[] { new MySqlParameter("@name", code) });
            DataResult dataResult = new DataResult();
            //不存在
            if(result.Count==0)
            {
                dataResult.result = 1;
                dataResult.resean = "激活码有误，请重试!";
            }

            else
            {
                CheckCountOrDateTime(result[0], dataResult, deviceUniqueIdentifier, plat, system);
               
            }

            string resultJson = Utils.CollectionsConvert.ToJSON(dataResult);
            string sendata = "Post"+ Constant.START_SPLIT + body + Constant.END_SPLIT + resultJson + Environment.NewLine;
            session.Send(sendata);
        }

        /// <summary>
        /// 次数和到期时间判断
        /// </summary>
        /// <param name="codeModel"></param>
        /// <param name="dataResult"></param>
        private void CheckCountOrDateTime(CodeModel codeModel, DataResult dataResult, 
           string  deviceUniqueIdentifier, string plat, string system)
        {
            //次数
            if(codeModel.ctype==0)
            {
                //0或者-1无限制
                if(codeModel.count<=0)
                {
                    dataResult.result = 0;
                }
                else
                {

                    bool flag = IsExistCodeMachine(codeModel.name, deviceUniqueIdentifier);
                    if(flag)
                    {
                        dataResult.result = 0;
                    }
                    else
                    {
                        if (GetCodeMachineCount(codeModel.name) < codeModel.count)
                        {
                            dataResult.result = 0;
                            SaveMachineCode(codeModel.id, deviceUniqueIdentifier, plat, system);
                        }
                        else
                        {
                            dataResult.result = 1;
                            dataResult.resean = "激活码无效，请重试。";
                        }
                    }
                }
            }
            //时间戳
            else
            {
                TimeSpan ts = DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0);
                long TotalSeconds = Convert.ToInt64(ts.TotalSeconds);
                if(TotalSeconds> codeModel.expiretime)
                {
                    dataResult.result = 1;
                    dataResult.resean = "激活码过期，请重试。";
                }
                else
                {
                    dataResult.result = 0;
                }
            }

        }
        /// <summary>
        /// code存在
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private int GetCodeMachineCount(string code)
        {
            string sql = "select * from machine where codeid = @code";

            int result = MySqlExecuteTools.GetCountResult(sql,
                new MySqlParameter[] { new MySqlParameter("@code", code) });

            return result;
        }

        /// <summary>
        /// 机器码和code存在在
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private bool IsExistCodeMachine(string code,string deviceUniqueIdentifier)
        {
            string sql = "select * from machine where codeid = @code and deviceUniqueIdentifier = @deviceUniqueIdentifier";

            int result = MySqlExecuteTools.GetCountResult(sql,
                new MySqlParameter[] { new MySqlParameter("@code", code),
                    new MySqlParameter("@deviceUniqueIdentifier", deviceUniqueIdentifier) });

            if(result>0)
            {
                return true;
            }

            return false;
        }

        private void SaveMachineCode(int codeid, string deviceUniqueIdentifier, string plat, string system)
        {
            string sql = "select * from machine where codeid = @code and deviceUniqueIdentifier = @deviceUniqueIdentifier";

            int result = MySqlExecuteTools.GetCountResult(sql,
                new MySqlParameter[] { new MySqlParameter("@code", codeid),
                    new MySqlParameter("@deviceUniqueIdentifier", deviceUniqueIdentifier) });

            //保存
            if(result==0)
            {
                sql = "insert into machine(codeid,deviceUniqueIdentifier,plat,system) " +
                    "values('" + codeid+ "','" + deviceUniqueIdentifier + "','" + plat + "','" + system + "')";
                MySqlExecuteTools.AddOrUpdate(sql);
            }
        }
    }

    

    

}
