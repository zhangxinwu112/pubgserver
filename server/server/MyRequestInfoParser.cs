using server.Tool;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server
{
    public class MyRequestInfoParser : IRequestInfoParser<StringRequestInfo>
    {
        public StringRequestInfo ParseRequestInfo(string source)
        {
            int pos = source.IndexOf(Constant.START_SPLIT.ToCharArray()[0]);

            if (pos <= 0)
                return null;

            string body = source.Substring(pos + 1);

            string key = source.Substring(0, pos);
            return new StringRequestInfo(source.Substring(0, pos), body,
                body.Split(new string[] { Constant.END_SPLIT }, StringSplitOptions.RemoveEmptyEntries));
        }
    }
}
