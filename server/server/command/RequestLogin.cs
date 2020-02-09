using server.Model;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Command;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server.command
{
    /// <summary>
    /// 登录成功后
    /// </summary>
   public class RequestLogin : CommandBase<PubgSession, StringRequestInfo>
    {
        public override void ExecuteCommand(PubgSession session, StringRequestInfo requestInfo)
        {
          
            SessionItem sessionItem = null;
            PubgSession.mOnLineConnections.TryGetValue(session, out sessionItem);
            if(sessionItem!=null)
            {
                if(requestInfo.Body.ToString().Equals("-1"))
                {
                    sessionItem.telephone = requestInfo.Body.ToString();
                    sessionItem.isLogin = false;
                }
                else
                {
                    sessionItem.telephone = requestInfo.Body.ToString();
                    sessionItem.isLogin = true;
                }
                
            }
            Console.WriteLine("收到客户端位置更新：" + session.RemoteEndPoint);


        }
    }
}
