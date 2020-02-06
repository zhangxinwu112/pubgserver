using log4net;
using server.Business;
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
   
    public class SeverTimer
    {

        ILog Logger = log4net.LogManager.GetLogger("server.Tool.SeverTimer");
        //推送地图
        private int IntervalSendPostion = 1000 * 5;
        private System.Threading.Timer tmrsendPostion = null;

        //检查在线连接
        private int IntervalCheckConnect = 1000 * 10;
        private System.Threading.Timer tmrCheckConnect = null;


        //检查电子围栏
        private int IntervalCheckFence = 1000 * 30;
        private System.Threading.Timer tmrCheckCFence = null;

        private MapDataPushBusiness mapDataPushBusiness;
        public void Init()
        {
            tmrsendPostion = new System.Threading.Timer(SendMapDataCallBack, null, IntervalSendPostion, IntervalSendPostion);

            tmrCheckConnect = new System.Threading.Timer(CheckConnectCallBack, null, IntervalCheckConnect, IntervalCheckConnect);

            tmrCheckCFence = new System.Threading.Timer(CheckFenceCallBack, null, IntervalCheckFence, IntervalCheckFence);

            mapDataPushBusiness = new MapDataPushBusiness();

            mapDataPushBusiness.Init();

        }

        /// <summary>
        /// 定时发送经纬度数据
        /// </summary>
        /// <param name="state"></param>
        private void SendMapDataCallBack(object state)
        {
            try
            {
                tmrsendPostion.Change(Timeout.Infinite, Timeout.Infinite);
                ConcurrentDictionary<PubgSession, SessionItem> dic = PubgSession.mOnLineConnections;
              
                mapDataPushBusiness.CreateGPSRoomDic();
                foreach (PubgSession session in dic.Keys)
                {
                    SessionItem sessionItem = null;
                    dic.TryGetValue(session, out sessionItem);
                    if (sessionItem != null && !string.IsNullOrEmpty(sessionItem.gpsItem.userName))
                    {
                      
                        List<GPSItem> gpsList = mapDataPushBusiness.GetGpsListByUser(sessionItem.gpsItem);
                        int roomId =  mapDataPushBusiness.GetRoomByUser(sessionItem.gpsItem.userId, sessionItem.gpsItem.userType);
                        Dictionary<string, object> dataDic = new Dictionary<string, object>();
                        Grounp grounp = mapDataPushBusiness.GetGrounpByRoomId(roomId);

                        dataDic.Add("grounp", grounp);
                        dataDic.Add("gpsData", gpsList);
                        string  resultJson = Utils.CollectionsConvert.ToJSON(dataDic);
                        Logger.Debug(resultJson);
                        string data = "ShowPosition" + Constant.START_SPLIT + resultJson + "\r\n";
                        session.Send(data);
                    }
                }
                
            }
            catch(Exception e)
            {
                Logger.InfoFormat("更新位置异常：：{0}", e.Message);
                tmrsendPostion.Dispose();
                tmrsendPostion = new System.Threading.Timer(SendMapDataCallBack, null, IntervalSendPostion, IntervalSendPostion);
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



        private void CheckFenceCallBack(object state)
        {
            try
            {
                
                tmrCheckCFence.Change(Timeout.Infinite, Timeout.Infinite);
                mapDataPushBusiness.UpdateFenceScope(IntervalCheckFence/1000);

            }
            catch (Exception e)
            {
                Logger.InfoFormat("电子围栏定时异常：：{0}", e.Message);
                tmrCheckConnect.Dispose();
                tmrCheckCFence = new System.Threading.Timer(CheckFenceCallBack, null, IntervalCheckFence, IntervalCheckFence);

                Console.WriteLine("电子围栏定时异常：" + e.Message);
            }
            finally
            {
                tmrCheckCFence.Change(IntervalCheckFence, IntervalCheckFence);
            }
        }

        private void StopTimer()
        {
            if (tmrsendPostion != null)
            {
                tmrsendPostion.Dispose();

            }
            if (tmrCheckConnect != null)
            {
                tmrCheckConnect.Dispose();
            }
        }

        ~SeverTimer() // 析构函数
        {

            StopTimer();
        }

    }

    
   
}
