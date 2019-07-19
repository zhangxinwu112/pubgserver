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
    /// Post
    /// </summary>
   public class Post : CommandBase<PubgSession, StringRequestInfo>
    {
        public override void ExecuteCommand(PubgSession session, StringRequestInfo requestInfo)
        {
            List<string> parametors = requestInfo.Parameters.ToList<string>();
            parametors.RemoveAt(0);
           
        }
    }
}
