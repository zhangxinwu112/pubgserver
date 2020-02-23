﻿using mysql;
using server.Test;
using server.Tool;
using ServerFramework.Tool;
using SuperSocket.SocketBase;
using SuperSocket.SocketEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using server.Restful;
using System.Configuration;
using server.DAO;

namespace server
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine(ConfigurationManager.AppSettings["ip"]);
            MySQLHelp.Instance.Connect();
            //TestSql.TestLogin("admin", "1234563");

            RestServiceInit RestServiceInit = new RestServiceInit();
            RestServiceInit.Init();


            //ScoreDao scoreDao = new ScoreDao();
            //scoreDao.SearchScore(36,false);


            ServerInit serverInit = new ServerInit();
            serverInit.Init();


        }
    }
}
