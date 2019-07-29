using server.Model;
using server.Tool;
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
    /// 心跳
    /// </summary>
   public class HeartBeat : CommandBase<PubgSession, StringRequestInfo>
    {
        public override void ExecuteCommand(PubgSession session, StringRequestInfo requestInfo)
        {
            SessionItem sessionItem = null;
            PubgSession.mOnLineConnections.TryGetValue(session, out sessionItem);
            if (sessionItem!=null)
            {
              
                sessionItem.heartTimeStamp = TimeUtils.GetCurrentTimestamp();
            }
            


        }
    }
}
