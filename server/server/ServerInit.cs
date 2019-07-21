using SuperSocket.SocketBase;
using SuperSocket.SocketEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server
{
    public class ServerInit
    {
        private IBootstrap bootstrap;
        public void Init()
        {
            //Console.WriteLine("Press any key to start the server!");

            //Console.ReadKey();
            //Console.WriteLine();

            bootstrap = BootstrapFactory.CreateBootstrap();

            if (!bootstrap.Initialize())
            {
                Console.WriteLine("服务器未初始化");
                Console.ReadKey();
                return;
            }

            var result = bootstrap.Start();

            Console.WriteLine("服务器启动结果{0}", result);

            if (result == StartResult.Failed)
            {
                Console.WriteLine("失败!");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("按Q键退出服务器");

            while (Console.ReadKey().KeyChar != 'q')
            {
                Console.WriteLine();
                continue;
            }

            Console.WriteLine();

            //Stop the appServer
            bootstrap.Stop();

            Console.WriteLine("服务器正常停止!");
            Console.ReadKey();
        }

        ~ServerInit()
        {
            if(bootstrap!=null)
            {
                bootstrap.Stop();
            }
            
        }
    }

    
}
