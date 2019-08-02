using log4net;
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
        private int IntervalSendPostion = 1000 * 5;
        private System.Threading.Timer tmrsendPostion = null;


        private int IntervalCheckConnect = 1000 * 10;
        private System.Threading.Timer tmrCheckConnect = null;


        public void Init()
        {
            tmrsendPostion = new System.Threading.Timer(SendPositionCallBack, null, IntervalSendPostion, IntervalSendPostion);

            tmrCheckConnect = new System.Threading.Timer(CheckConnectCallBack, null, IntervalCheckConnect, IntervalCheckConnect);


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
                List<GPSItem> list = new List<GPSItem>();
                foreach (PubgSession session in dic.Keys)
                {

                    SessionItem sessionItem = null;
                    dic.TryGetValue(session, out sessionItem);
                    if(sessionItem!=null && !string.IsNullOrEmpty(sessionItem.gpsItem.userName))
                    {
                        list.Add(sessionItem.gpsItem);
                    }
                }
                if(list.Count>0)
                {
                    string resultJson = Utils.CollectionsConvert.ToJSON(list);
                    foreach (PubgSession session in dic.Keys)
                    {
                        SessionItem sessionItem = null;
                        dic.TryGetValue(session, out sessionItem);
                        if (sessionItem != null && !string.IsNullOrEmpty(sessionItem.gpsItem.userName))
                        {

                            string data = "ShowPostion" + Constant.START_SPLIT + resultJson + "\r\n";
                            session.Send(data);
                        }
                    }
                
                }
              
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
    }
   
}
