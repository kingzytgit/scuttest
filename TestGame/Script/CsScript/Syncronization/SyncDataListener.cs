#define DEBUG
//#define LOCAL_TEST

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Text;
using System.Threading.Tasks;
using ZyGames.Framework.Common.Log;
using System.Net.Sockets;
using System.Diagnostics;
using TestGameServer.Common;

namespace Syncronization
{
    class SyncDataListener
    {
#if DEBUG
        protected static int listenPort = 11000;
#else
        protected static int listenPort = new Random(System.DateTime.Now.Millisecond).Next(10000, 20000);
#endif
        public void StartListener(object obj)
        {
            try
            {
#if LOCAL_TEST
                IPAddress localIP = IPAddress.Parse("127.0.0.1");
#else
                GameLog.Info("**请确保服务器只有一个网卡**，不然只有手动指定服务器外网IP");
                IPAddress localIP = Dns.GetHostEntry(Dns.GetHostName()).AddressList[1];
#endif
                IPEndPoint localEP = new IPEndPoint(localIP, listenPort);
                GameLog.Info("listening BEGIN. " + localIP.ToString() + ":" + listenPort.ToString());
                UdpClient listener = new UdpClient(localEP);
                try
                {
                    IPEndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);
                    while (true)
                    {
                        byte[] data = listener.Receive(ref remoteEP);
                        string info = Encoding.UTF8.GetString(data);
                        GameLog.Info(info);

                        if(info == "end")
                        {
                            return;
                        }
                    }
                }
                catch (ThreadInterruptedException)
                {
                    GameLog.Info("listening Interrupted.");
                }
                catch (Exception e)
                {
                    GameLog.Error(e.ToString());
                }
                finally
                {
                    listener.Close();
                    GameLog.Info("listening Close.");
                }
            }
            catch (Exception e)
            {
                GameLog.Error(e.ToString());
            }
        }
        public int GetListenPort()
        {
            return listenPort;
        }

        public void Run()
        {
            ThreadPool.QueueUserWorkItem(StartListener);
        }
        public static void TestClient(string data)
        {
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
#if LOCAL_TEST
            IPAddress serverIP = IPAddress.Parse("127.0.0.1");
#else
            IPAddress serverIP = IPAddress.Parse("112.124.97.58");
#endif
            IPEndPoint ep = new IPEndPoint(serverIP, listenPort);
            for (int i = 0; i < 10; ++i)
            {
                string info = i.ToString() + " : " + data;
                byte[] sendbuf = Encoding.UTF8.GetBytes(info);
                s.SendTo(sendbuf, ep);
                GameLog.Info(info);
            }

            s.SendTo(Encoding.UTF8.GetBytes("end"), ep);
            s.SendTo(Encoding.UTF8.GetBytes("end"), ep);
            GameLog.Info("end");
        }

        static void Main(string[] args)
        {
            SyncDataListener.TestClient("kkkkk疯狂jfk");
            System.Console.ReadKey();
        }
    }
}

