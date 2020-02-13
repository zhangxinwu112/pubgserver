using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server.Model
{
    public class Room
    {
        public int id;

        public int grounpId;

        public string  name;

        public string checkCode;

        public int userCount = 0;

        public bool isCurrentUser = false;

        public int userId;

        public int runState;

    }
}
