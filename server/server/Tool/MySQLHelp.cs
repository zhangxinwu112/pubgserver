using System;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;
using ServerFramework.Tool.Singleton;

namespace mysql
{
    public class MySQLHelp : Singleton<MySQLHelp>
    {
        public const string ConnectionString = "datasource=127.0.0.1;port=3306;Charset=gb2312;database=cs;user=root;pwd=root";

        public MySQLHelp()
        {
           // Connect();
        }

        private MySqlConnection SqlConn = null;
        public MySqlConnection GetSqlConn
        {
            get
            {
                return SqlConn;

            }
        }
        /// <summary>
        /// 连接
        /// </summary>
        public void Connect()
        {
            MySqlConnection SqlConn = new MySqlConnection(ConnectionString);
            try
            {
                SqlConn.Open();
                Console.WriteLine("数据库已接成功" );
                this.SqlConn = SqlConn;
            }
            catch(Exception e)
            {
                Console.WriteLine("数据库异常：" + e.Message);
                SqlConn = null;
            }
        }
        //关闭数据库连接
        public void CloseConnection(MySqlConnection SqlConn)
        {
            if (SqlConn != null)
                SqlConn.Close();
            else
            {
                Console.WriteLine("MySqlConnection不能为空");
            }
        }
        //判定安全字符串
        public bool IsSafeStr(string str)
        {
            return !Regex.IsMatch(str, @"[-|;|,|\/|\(|\)|\[|\]|\}|\{|%|@|\*|!|\']");
        }
    }
}
