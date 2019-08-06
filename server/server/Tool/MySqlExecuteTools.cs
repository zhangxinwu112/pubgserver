﻿using mysql;
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
                cmd.CommandTimeout = 10;
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
                Logger.InfoFormat("excepstion：{0}", e.Message);
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


        public static List<T> GetObjectResult<T>(string sql, params MySqlParameter[] commandParameters) where T:new()
        {
            List<T> result = new List<T>();
            MySqlDataReader reader = null;
            try
            {
                MySqlCommand cmd = new MySqlCommand(sql, MySQLHelp.Instance.GetSqlConn);
                cmd.CommandTimeout = 10;
                
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
                Logger.InfoFormat("excepstion：{0}", e.Message);
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


        public static int AddOrUpdate(string sql)
        {
            int result = 0;
            try
            {

                MySqlCommand cmd = new MySqlCommand(sql, MySQLHelp.Instance.GetSqlConn);
                cmd.CommandTimeout = 10;
                result = cmd.ExecuteNonQuery();
            }
            catch(Exception e)
            {
                Logger.InfoFormat("excepstion：{0}", e.Message);
            }
            finally
            {
               
            }

            return result;

        }



    }
}
