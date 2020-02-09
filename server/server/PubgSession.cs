using server.Model;
using server.Tool;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server
{
   public   class PubgSession:AppSession<PubgSession>
    {
        public static ConcurrentDictionary<PubgSession, SessionItem> mOnLineConnections = new ConcurrentDictionary<PubgSession, SessionItem>();


        protected override void OnSessionStarted()
        {
            this.Send("ECHO:Welcome to SuperSocket pubg Server"+ " \r\n");
            SessionItem item = new SessionItem();
            item.createTimeStamp= TimeUtils.GetCurrentTimestamp();
            mOnLineConnections.TryAdd(this, item);

            Console.WriteLine("有新的客户端连接。"+RemoteEndPoint);
            Console.WriteLine("当前客户端数量：" + mOnLineConnections.Count);
           // Console.WriteLine("启用日志：" + Logger.IsErrorEnabled);
           
            Logger.InfoFormat("新的客户端连接：{0}", RemoteEndPoint);
        }

        protected override void HandleUnknownRequest(StringRequestInfo requestInfo)
        {
            this.Send("ECHO:UnknowRequest" + " \r\n");
        }

        protected override void HandleException(Exception e)
        {
           // this.Send("Application error: {0}", e.Message);
        }

        protected override void OnSessionClosed(CloseReason reason)
        {
            //add you logics which will be executed after the session is closed
            base.OnSessionClosed(reason);
            Console.WriteLine("客户端主动断开了");
            SessionItem sessionItem = null;
            mOnLineConnections.TryRemove(this, out sessionItem);
            string content = "客户端主动断开了连接。";
            if (sessionItem!=null && sessionItem.gpsItem!=null && !string.IsNullOrEmpty(sessionItem.gpsItem.userName))
            {
                content += sessionItem.gpsItem.userName;
                Console.WriteLine(content + ":" + RemoteEndPoint);
                Logger.InfoFormat("客户端主动断开：{0}", RemoteEndPoint);
            }
           
        }
    }
}
