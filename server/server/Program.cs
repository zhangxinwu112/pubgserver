using ServerFramework.Tool;
using SuperSocket.SocketBase;
using SuperSocket.SocketEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server
{
    class Program
    {
        static void Main(string[] args)
        {
            MySQLHelp.Instance.Connect();
             ServerInit serverInit = new ServerInit();
            serverInit.Init();

        }
    }
}
