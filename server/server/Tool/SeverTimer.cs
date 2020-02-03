using log4net;
using server.DAO;
using server.Model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace server.Tool
{
   
    public class SeverTimer:IEventListener
    {

        ILog Logger = log4net.LogManager.GetLogger("server.Tool.SeverTimer");
        private int IntervalSendPostion = 1000 * 5;
        private System.Threading.Timer tmrsendPostion = null;


        private int IntervalCheckConnect = 1000 * 10;
        private System.Threading.Timer tmrCheckConnect = null;


        public void Init()
        {
            tmrsendPostion = new System.Threading.Timer(SendPositionCallBack, null, IntervalSendPostion, IntervalSendPostion);

            tmrCheckConnect = new System.Threading.Timer(CheckConnectCallBack, null, IntervalCheckConnect, IntervalCheckConnect);

            EventMgr.Instance.AddListener(this, EventName.UPATE_GROUNP_USER);
            //获取
            JoinRoomDao joinRoomDao = new JoinRoomDao();
            joinRoomDao.SendUpdateGrounData();


        }

        /// <summary>
        /// 定时发送经纬度数据
        /// </summary>
        /// <param name="state"></param>
        private void SendPositionCallBack(object state)
        {
            try
            {
                tmrsendPostion.Change(Timeout.Infinite, Timeout.Infinite);
                ConcurrentDictionary<PubgSession, SessionItem> dic = PubgSession.mOnLineConnections;
                //List<GPSItem> list = new List<GPSItem>();
                //foreach (PubgSession session in dic.Keys)
                //{
                //    SessionItem sessionItem = null;
                //    dic.TryGetValue(session, out sessionItem);
                //    if(sessionItem!=null && !string.IsNullOrEmpty(sessionItem.gpsItem.userName))
                //    {
                //        list.Add(sessionItem.gpsItem);
                //    }
                //}
                //if(list.Count>0)
                //{
                //

                SetGrounpDic();
                foreach (PubgSession session in dic.Keys)
                {
                    SessionItem sessionItem = null;
                    dic.TryGetValue(session, out sessionItem);
                    if (sessionItem != null && !string.IsNullOrEmpty(sessionItem.gpsItem.userName))
                    {
                       int grounpid =  GetGrounpByUser(sessionItem.gpsItem.userId);
                        List<GPSItem> list = null;
                        gpsDic.TryGetValue(grounpid.ToString(), out list);
                        if(list!=null && list.Count>0)
                        {
                            string resultJson = Utils.CollectionsConvert.ToJSON(list);

                            string data = "ShowPosition" + Constant.START_SPLIT + resultJson + "\r\n";
                            session.Send(data);
                        }
                       
                    }
                }
                
                //}
              
            }
            catch(Exception e)
            {
                Logger.InfoFormat("更新位置异常：：{0}", e.Message);
                tmrsendPostion.Dispose();
                tmrsendPostion = new System.Threading.Timer(SendPositionCallBack, null, IntervalSendPostion, IntervalSendPostion);
                Console.WriteLine("位置更新异常信息：" + e.Message);
            }
            finally
            {
                tmrsendPostion.Change(IntervalSendPostion, IntervalSendPostion);
            }
        }

        private int connectTime = 30;
        /// <summary>
        /// 定时检查连接
        /// </summary>
        /// <param name="state"></param>
        private void CheckConnectCallBack(object state)
        {
            try
            {
                tmrCheckConnect.Change(Timeout.Infinite, Timeout.Infinite);

                ConcurrentDictionary<PubgSession, SessionItem> dic = PubgSession.mOnLineConnections;
                if(dic==null || dic.Count==0)
                {
                    Console.WriteLine("当前服务器连接数量：" + dic.Count);
                    tmrCheckConnect.Change(IntervalCheckConnect, IntervalCheckConnect);
                    return;
                }
                foreach(PubgSession session in dic.Keys)
                {
                    SessionItem sessionItem = null;
                    dic.TryGetValue(session, out sessionItem);
                    long currentTime = TimeUtils.GetCurrentTimestamp();

                    if (sessionItem!=null)
                    {
                        if ((sessionItem.heartTimeStamp > 0 && (currentTime - sessionItem.heartTimeStamp > connectTime) ||
                            sessionItem.heartTimeStamp == -1 && (currentTime - sessionItem.createTimeStamp > connectTime)))
                        {
                            dic.TryRemove(session, out sessionItem);
                            Console.WriteLine(sessionItem.gpsItem.userName + ":" + "连接超时，客户端被强制中断。");
                        }
                        
                    }
                }

                Console.WriteLine("当前服务器连接数量：" + dic.Count);
            }
            catch(Exception e)
            {
                Logger.InfoFormat("连接异常：：{0}", e.Message);
                tmrCheckConnect.Dispose();
                tmrCheckConnect = new System.Threading.Timer(CheckConnectCallBack, null, IntervalCheckConnect, IntervalCheckConnect);
               
                Console.WriteLine("连接异常信息：" +  e.Message);
            }
            finally
            {
                tmrCheckConnect.Change(IntervalCheckConnect, IntervalCheckConnect);
            }
        }

        ~SeverTimer() // 析构函数
        {
          
            StopTimer();
        }

        private void StopTimer()
        {
            if (tmrsendPostion != null)
            {
                tmrsendPostion.Dispose();
               
            }
            if(tmrCheckConnect!=null)
            {
                tmrCheckConnect.Dispose();
            }
        }

        public bool HandleEvent(string eventName, IDictionary<string, object> dictionary)
        {

            List<Room_User> list = dictionary["data"] as List<Room_User>;
            DoGroundData(list);
            return true;
        }

        //key:grounpID  value:userList
        private Dictionary<string, List<int>> grounpUserDic = new Dictionary<string, List<int>>();
        private void DoGroundData(List<Room_User> room_User_list)
        {
            grounpUserDic.Clear();
            //去除重复的
            var resultlist = room_User_list.GroupBy(p => p.room_id).Select(g => g.First()).ToList();

            resultlist.ForEach((item) => {

                int grounpId = item.room_id;

                var query = from s in room_User_list
                            where s.room_id == grounpId
                            select s.user_id;
                grounpUserDic.Add(grounpId.ToString(), query.ToList<int>());

            });
            if(grounpUserDic.Count>0)
            {
                Console.WriteLine(Utils.CollectionsConvert.ToJSON(grounpUserDic));
            }
           
        }


        private Dictionary<string, List<GPSItem>> gpsDic = new Dictionary<string, List<GPSItem>>();
        /// <summary>
        /// set grounpid,
        /// </summary>
        private void SetGrounpDic()
        {
            gpsDic.Clear();
            foreach (string gronpId in grounpUserDic.Keys)
            {
                List<int> userids = grounpUserDic[gronpId];
                List<GPSItem> gpsList = GetSingleGPSByUser(userids);
                if(gpsList.Count>0)
                {
                    gpsDic.Add(gronpId, gpsList);
                }
            }


        }

        /// <summary>
        /// 获取同一grounp的用户的gpsList数据
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
                    if(sessionItem.gpsItem!=null && sessionItem.gpsItem.userId== item)
                    {
                        gpsList.Add(sessionItem.gpsItem);
                    }
                }

            });
            return gpsList;
        }

        /// <summary>
        /// 通过userid查询所在的grounp
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private int GetGrounpByUser(int userId)
        {
            foreach(string grounpid in grounpUserDic.Keys)
            {
                List<int> list = grounpUserDic[grounpid];
               if (list.Contains(userId))
                {
                    return Convert.ToInt16(grounpid);
                }
            }

            return -1;
        }

    }

    
   
}
