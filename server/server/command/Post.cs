using server.Tool;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Command;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
           
            string methodkey = requestInfo.Parameters.ToList<string>()[0];
            string[] strs = methodkey.Split(Constant.METHOD_SPLIT.ToCharArray()[0]);
            string className = strs[0];
            string method = strs[1];
            Assembly asm = Assembly.GetExecutingAssembly();
            Object obj = asm.CreateInstance(className, true);

            MethodInfo mi = obj.GetType().GetMethod(method);
            if (mi != null)
            {
                object[] parameters = new object[requestInfo.Parameters.Length + 1];
                parameters[0] = session;
                for (int i = 1; i < parameters.Length; i++)
                {
                    parameters[i] = requestInfo.Parameters[i - 1];
                }
                mi.Invoke(obj, parameters);
            }

        }
    }
}
