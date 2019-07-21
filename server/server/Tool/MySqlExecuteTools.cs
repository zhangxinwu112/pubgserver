using mysql;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace server.Tool
{
    /// <summary>
    /// dgv.DataSource=MySqlHelper.GetDataSet(MySqlHelper.Conn, CommandType.Text, "select * from wp_posts", null).Tables[0].DefaultView;
    /// </summary>
    public static  class MySqlExecuteTools
    {
       
        /// <summary>
        /// 获取查询的结果数量
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public static int  GetCountResult(string sql, params MySqlParameter[] commandParameters)
        {
            MySqlCommand cmd = new MySqlCommand(sql, MySQLHelp.Instance.GetSqlConn);
           
            if(commandParameters!=null)
            {
                foreach (MySqlParameter parm in commandParameters)
                    cmd.Parameters.Add(parm);
            }
           
            MySqlDataReader reader = cmd.ExecuteReader();
            int result = 0;
            while (reader.Read())
            {
                if (reader.HasRows)
                {
                    result = result + 1;
                    
                }
                
            }
            reader.Close();
            return result;
        }


        public static List<T> GetObjectResult<T>(string sql, params MySqlParameter[] commandParameters) where T:new()
        {
            MySqlCommand cmd = new MySqlCommand(sql, MySQLHelp.Instance.GetSqlConn);

            List<T> result = new List<T>();
            if(commandParameters!=null)
            {
                foreach (MySqlParameter parm in commandParameters)
                    cmd.Parameters.Add(parm);
            }
           
            MySqlDataReader reader = cmd.ExecuteReader();
            int filedCount  =reader.FieldCount;
           
           
            while (reader.Read())
            {
                if (reader.HasRows)
                {

                    T t = new T();
                    
                    for(int i=0;i< filedCount;i++)
                    {
                        string propertyName = reader.GetName(i);
                        FieldInfo info = t.GetType().GetField(propertyName);
                        if(info!=null&&reader.GetValue(i)!=null)
                        {
                            info.SetValue(t, reader.GetValue(i));
                        }
                        
                    }
                    result.Add(t);

                }

            }
            reader.Close();
            return result;
        }


        public static int AddOrUpdate(string sql)
        {
            MySqlCommand cmd = new MySqlCommand(sql, MySQLHelp.Instance.GetSqlConn);
            int result = cmd.ExecuteNonQuery();
            return result;
        }



    }
}
