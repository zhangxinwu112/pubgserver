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
using log4net;

namespace server.Tool
{
    /// <summary>
    /// dgv.DataSource=MySqlHelper.GetDataSet(MySqlHelper.Conn, CommandType.Text, "select * from wp_posts", null).Tables[0].DefaultView;
    /// </summary>
    public static  class MySqlExecuteTools
    {

       private static  ILog Logger = log4net.LogManager.GetLogger("server.Tool.MySqlExecuteTools");
        /// <summary>
        /// 获取查询的结果数量
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public static int  GetCountResult(string sql, params MySqlParameter[] commandParameters)
        {
            MySqlDataReader reader = null;
            int result = 0;
            try
            {
                MySqlCommand cmd = new MySqlCommand(sql, MySQLHelp.Instance.GetSqlConn);
              // cmd.CommandTimeout = 10;
                if (commandParameters != null)
                {
                    foreach (MySqlParameter parm in commandParameters)
                        cmd.Parameters.Add(parm);
                }

                reader = cmd.ExecuteReader();


                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        result = result + 1;

                    }

                }
            }
            catch(Exception e)
            {
                Logger.InfoFormat("excepstion1：{0}", e.Message);
                ReConnect();
               
            }
            finally
            {
                if(reader!=null)
                {
                    reader.Close();
                }
               
            }
            return result;


        }

        private static void ReConnect()
        {
            MySQLHelp.Instance.CloseConnection();
            MySQLHelp.Instance.Connect();

        }


        public static List<T> GetObjectResult<T>(string sql, params MySqlParameter[] commandParameters) where T:new()
        {
            List<T> result = new List<T>();
            MySqlDataReader reader = null;
            try
            {
                MySqlCommand cmd = new MySqlCommand(sql, MySQLHelp.Instance.GetSqlConn);
               // cmd.CommandTimeout = 10;
                
                if (commandParameters != null)
                {
                    foreach (MySqlParameter parm in commandParameters)
                        cmd.Parameters.Add(parm);
                }

                reader = cmd.ExecuteReader();
                int filedCount = reader.FieldCount;


                while (reader.Read())
                {
                    if (reader.HasRows)
                    {

                        T t = new T();

                        for (int i = 0; i < filedCount; i++)
                        {
                            string propertyName = reader.GetName(i);
                            FieldInfo info = t.GetType().GetField(propertyName);
                            if (info != null && reader.GetValue(i) != null)
                            {
                                info.SetValue(t, reader.GetValue(i));
                            }

                        }
                        result.Add(t);

                    }

                }
            }
            catch (Exception e)
            {
                Logger.InfoFormat("excepstion2：{0}", e.Message);
                ReConnect();
              
            }
            finally
            { 
            
                if(reader!=null)
                {
                    reader.Close();
                    
                }
            }
            return result;

        }

        /// <summary>
        /// 获取单字段的值
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public static List<object> GetSingleFieldResult(string sql, params MySqlParameter[] commandParameters)
        {

            MySqlDataReader reader = null;
            try
            {
                MySqlCommand cmd = new MySqlCommand(sql, MySQLHelp.Instance.GetSqlConn);
                // cmd.CommandTimeout = 10;

                if (commandParameters != null)
                {
                    foreach (MySqlParameter parm in commandParameters)
                        cmd.Parameters.Add(parm);
                }

                reader = cmd.ExecuteReader();
                int filedCount = reader.FieldCount;


                List<object> list = new List<object>();
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {

                    
                        for (int i = 0; i < filedCount; i++)
                        {
                            list.Add( reader.GetValue(i));
                          
                        }
                    
                    }

                }

                return list;



            }
            catch (Exception e)
            {
                Logger.InfoFormat("excepstion3：{0}", e.Message);
                ReConnect();
              
            }
            finally
            {

                if (reader != null)
                {
                    reader.Close();

                }
            }
            return null;

        }


        public static object AddOrUpdate(string sql)
        {
            MySqlCommand cmd = null;
            object result = 0;
            try
            {

                cmd = new MySqlCommand(sql, MySQLHelp.Instance.GetSqlConn);
                // cmd.CommandTimeout = 10;
                //返回操作的数据库id
                result = cmd.ExecuteScalar();
              
              
            }
            catch(Exception e)
            {
                Logger.InfoFormat("excepstion4：{0}", e.Message);
                ReConnect();
                
            }
            finally
            {
               
            }
            return result;

        }

        /// <summary>
        /// 插入数据库并获取新增的数据id
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static long GetAddID(string sql)
        {
            long result = -1;
            MySqlDataReader reader = null;
            try
            {

                MySqlCommand cmd = new MySqlCommand(sql, MySQLHelp.Instance.GetSqlConn);
                reader = cmd.ExecuteReader();

                result = cmd.LastInsertedId;
                Console.WriteLine(result.ToString());
                //while (reader.Read())
                //{
                //    if (reader.HasRows)
                //    {
                //        return reader.GetInt32(0);
                //    }
                //}
            }
            catch (Exception e)
            {
                Logger.InfoFormat("excepstion5：{0}", e.Message);
                ReConnect();
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();

                }
            }

            return result;

        }

    }
}
