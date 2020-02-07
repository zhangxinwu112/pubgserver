using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server.Tool
{
   public  class TimeUtils
    {

        public static long GetCurrentTimestamp()
        {
            TimeSpan ts = DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            long TotalSeconds = Convert.ToInt64(ts.TotalSeconds);
            return TotalSeconds;
        }

        public static string GetCurrentFormatTime()
        {
            return DateTime.Now.ToString("hh:mm:ss");
        }
    }
}
