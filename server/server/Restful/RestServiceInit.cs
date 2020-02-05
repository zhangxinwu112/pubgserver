﻿using Restful;
using server.Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace server.Restful
{
    public class RestServiceInit
    {

        public void Init()
        {
            string ipAdress = CommonUtils.GetLocalIP();

            try
            {

                string url = "http://" + ipAdress + ":8899/";
                Console.WriteLine(url);
                FenceDataImp service = new FenceDataImp();
                WebServiceHost _serviceHost = new WebServiceHost(service, new Uri(url));
             
                _serviceHost.Open();
                Console.WriteLine("Web服务已开启...");
               
            }
            catch (Exception ex)
            {
                Console.WriteLine("Web服务开启失败：{0}\r\n{1}", ex.Message, ex.StackTrace);
            }

        }
    }
}