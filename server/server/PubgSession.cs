using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server
{
   public   class PubgSession:AppSession<PubgSession>
    {
        protected override void OnSessionStarted()
        {
            this.Send("ECHO:Welcome to SuperSocket pubg Server"+Environment.NewLine);
        }

        protected override void HandleUnknownRequest(StringRequestInfo requestInfo)
        {
            this.Send("ECHO:UnknowRequest" + Environment.NewLine);
        }

        protected override void HandleException(Exception e)
        {
           // this.Send("Application error: {0}", e.Message);
        }

        protected override void OnSessionClosed(CloseReason reason)
        {
            //add you logics which will be executed after the session is closed
            base.OnSessionClosed(reason);
        }
    }
}
