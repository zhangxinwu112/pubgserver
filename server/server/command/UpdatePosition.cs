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
    /// 更新位置
    /// </summary>
   public class UpdatePosition:CommandBase<PubgSession, StringRequestInfo>
    {
        public override void ExecuteCommand(PubgSession session, StringRequestInfo requestInfo)
        {
          
            SessionItem sessionItem = null;
            PubgSession.mOnLineConnections.TryGetValue(session, out sessionItem);
            if(sessionItem!=null)
            {
                GPSItem gpsItem = Utils.CollectionsConvert.ToObject<GPSItem>( requestInfo.Body.ToString());
                sessionItem.gpsItem = gpsItem;
            }
            Console.WriteLine("收到客户端位置更新：" + session.RemoteEndPoint);


        }
    }
}
