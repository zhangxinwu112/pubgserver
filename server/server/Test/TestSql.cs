using mysql;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server.Test
{
    public class TestSql
    {

        public static void TestLogin(string username, string password)
        {
       
            MySqlCommand cmd = new MySqlCommand("select * from user where username = @username and password = @password", MySQLHelp.Instance.GetSqlConn);
            cmd.Parameters.AddWithValue("username", username);
            cmd.Parameters.AddWithValue("password", password);
            MySqlDataReader  reader = cmd.ExecuteReader();
            int result = 0;
            while (reader.Read())
            {
                if (reader.HasRows)
                {
                    result = result + 1;
                }
            }
          Console.WriteLine("count={0}", result);

            Console.ReadKey();
            Console.WriteLine();
        }
    }
}
