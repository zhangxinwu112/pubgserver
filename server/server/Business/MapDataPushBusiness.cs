using log4net;
using server.DAO;
using server.Model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server.Business
{
    /// <summary>
    /// 处理地图推送相关的业务逻辑
    /// </summary>
    public  class MapDataPushBusiness:IEventListener
    {
        private ILog Logger = log4net.LogManager.GetLogger("server.Business.MapDataPushBusiness");
        private JoinRoomDao joinRoomDao;
        private SearchGrounpDao searchGrounpDao;
  
        public void Init()
        {
            EventMgr.Instance.AddListener(this, EventName.UPATE_ROOM_USER);
            EventMgr.Instance.AddListener(this, EventName.ALL_ROOM_DATA);
            //获取
            searchGrounpDao = new SearchGrounpDao();
            joinRoomDao = new JoinRoomDao();
            joinRoomDao.GetRoomUserData();
            joinRoomDao.GetAllRoom();
        }

       

        public bool HandleEvent(string eventName, IDictionary<string, object> dictionary)
        {
            //获取用户和room的数据
            if(eventName.Equals(EventName.UPATE_ROOM_USER))
            {
                List<Room_User> roomUserList = dictionary["data"] as List<Room_User>;
                DoRoomData(roomUserList);
            }else if(eventName.Equals(EventName.ALL_ROOM_DATA))
            {
                List<Room> roomList = dictionary["data"] as List<Room>;
                CreateGroupRoomDic(roomList);
            }
           
            return true;
        }
        #region 处理同一grounp推送经纬的数据的逻辑
        //key:roomID  value:userList
        private Dictionary<string, List<int>> roomUserDic = new Dictionary<string, List<int>>();
        /// <summary>
        /// 形成roomid，List<userid>
        /// </summary>
        /// <param name="room_User_list"></param>
        private void DoRoomData(List<Room_User> room_User_list)
        {
            roomUserDic.Clear();
            //去除重复的
            var roomlist = room_User_list.GroupBy(p => p.room_id).Select(g => g.First()).ToList();

            roomlist.ForEach((item) => {

                int roomId = item.room_id;

                var query = from s in room_User_list
                            where s.room_id == roomId
                            select s.user_id;
                roomUserDic.Add(roomId.ToString(), query.ToList<int>());

            });
            if (roomUserDic.Count > 0)
            {
                Console.WriteLine(Utils.CollectionsConvert.ToJSON(roomUserDic));
            }

        }
         private Dictionary<string, List<GPSItem>> gpsDic = new Dictionary<string, List<GPSItem>>();

     
        public List<GPSItem> GetGpsListByRoomId(string roomId)
        {
            List<GPSItem> gpsList = null;
            gpsDic.TryGetValue(roomId.ToString(), out gpsList);

            if (gpsList != null && gpsList.Count > 0)
            {
                //移除没有进入游戏地图的用户
                for (int i = gpsList.Count - 1; i >= 0; i--)
                {
                    if (string.IsNullOrEmpty(gpsList[i].userName))
                    {
                        gpsList.RemoveAt(i);
                    }
                }
            }

            return gpsList;
        }
        /// <summary>
        /// set grounpid,
        /// </summary>
        public void CreateGPSRoomDic()
        {
            gpsDic.Clear();
            foreach (string roomId in roomUserDic.Keys)
            {
                List<int> userids = roomUserDic[roomId];
                List<GPSItem> gpsList = GetSingleGPSByUser(userids);
                if (gpsList.Count > 0)
                {
                    gpsDic.Add(roomId, gpsList);
                }
            }


        }

        /// <summary>
        /// 获取同一room的用户的gpsList数据
        /// </summary>
        /// <param name="userids"></param>
        /// <returns></returns>
        private List<GPSItem> GetSingleGPSByUser(List<int> userids)
        {
            List<GPSItem> gpsList = new List<GPSItem>();
            userids.ForEach((item) => {

                ConcurrentDictionary<PubgSession, SessionItem> dic = PubgSession.mOnLineConnections;
                foreach (SessionItem sessionItem in dic.Values)
                {
                    if (sessionItem.gpsItem != null && sessionItem.gpsItem.userId == item)
                    {
                        gpsList.Add(sessionItem.gpsItem);
                    }
                }

            });
            return gpsList;
        }

        /// <summary>
        /// 通过userid查询所在的room
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int GetRoomByUser(int userId,int userType = 0)
        {
            if(userType==1)
            {
                return -1;
            }
            foreach (string roomId in roomUserDic.Keys)
            {
                List<int> list = roomUserDic[roomId];
                if (list.Contains(userId))
                {
                    return Convert.ToInt16(roomId);
                }
            }

            return -1;
        }

        public List<GPSItem> GetGpsListByUser(GPSItem userGpsItem)
        {
            
            //玩家
            if (userGpsItem.userType == 0)
            {
                int roomId = GetRoomByUser(userGpsItem.userId);
                List<GPSItem> gpsList = GetGpsListByRoomId(roomId.ToString());
                return gpsList;
            }
            //管理员
            else if (userGpsItem.userType == 1)
            
            {
                //查询所有用户的grounp
                List<int> grounpIDs = searchGrounpDao.SearchGrounpListByUser(userGpsItem.userId);
                List<GPSItem> result = new List<GPSItem>();
                grounpIDs.ForEach((item) => {
                    int[] roomIds =  GetRoomListByGrounpId(item);
                   
                    for(int k=0;k< roomIds.Length;k++)
                    {
                        List<GPSItem> gpsList = GetGpsListByRoomId(roomIds[k].ToString());
                        if(gpsList!=null)
                        {
                            result.AddRange(gpsList);
                        }
                       
                    }
                });
                return result;
            }
            
            return null;
        }

     
        #endregion


        #region 创建 room和grounp的关系
        private Dictionary<int, Grounp> roomGrounp = new Dictionary<int, Grounp>();
        private List<Room> roomList = null;
        private void CreateGroupRoomDic(List<Room> roomList)
        {
            this.roomList = roomList;
            roomGrounp.Clear();
            roomList.ForEach((item) => {

                Grounp p = searchGrounpDao.GetGrounpById(item.grounpId.ToString());
                roomGrounp.Add(item.id, p);
            });
            // Console.WriteLine(Utils.CollectionsConvert.ToJSON(roomGrounp));
           // Logger.Debug(Utils.CollectionsConvert.ToJSON(roomGrounp));

        }

        public Grounp GetGrounpByRoomId(int roomID)
        {
            Grounp grounp = null;
            roomGrounp.TryGetValue(roomID, out grounp);

            return grounp;
        }

        public int[] GetRoomListByGrounpId(int grounpID)
        {
            var result = this.roomList.Where(a => a.grounpId == grounpID);

            int[] roomIds = result.Select(a => a.id).ToArray();

            return roomIds;
        }
        #endregion
    }
}
