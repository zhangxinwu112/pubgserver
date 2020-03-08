using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server.Model
{
    public class UserName
    {
        public int id;

        public string telephone;

        public string name;

        public string image;

       // 0 超级管理员
       //1 普通管理员
      // 2 道具
      // 3 普通玩家
        public int type;

        //如果是玩家是否准备好
        public int runState = -1;

        //是否为队长
        public bool isLeader = false;
    }
}
