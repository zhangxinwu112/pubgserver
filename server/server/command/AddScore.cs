﻿using SuperSocket.SocketBase;
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
    /// 充值
    /// </summary>
   public class AddScore : CommandBase<PubgSession, StringRequestInfo>
    {
        public override void ExecuteCommand(PubgSession session, StringRequestInfo requestInfo)
        {
           // session.Send(requestInfo.Parameters.Select(p => Convert.ToInt32(p)).Sum().ToString());
        }
    }
}
