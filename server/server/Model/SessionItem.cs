using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server.Model
{
   public  class SessionItem
    {

        public GPSItem gpsItem = new GPSItem();

        public long heartTimeStamp =-1;

        public long createTimeStamp = -1;

        public bool isLogin = false;

        public string telephone = "";

    }
}
