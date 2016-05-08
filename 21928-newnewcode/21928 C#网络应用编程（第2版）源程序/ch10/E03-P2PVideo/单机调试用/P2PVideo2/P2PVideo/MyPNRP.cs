using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.PeerToPeer;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace P2PVideo
{
    class MyPNRP
    {   
       
        static PeerNameRegistration peerNameRegistration;
        static List<String> strPeerInfos  = new List<string>();
        public static int port;
        /// <summary>
        /// 将PNRP Name 注册到Cloud中
        /// </summary>
        public static bool registerPeer(String strMyPeerName)
        {

            try
            {
                PeerName peerName = new PeerName(strMyPeerName, PeerNameType.Unsecured);
                // 以PeerName创建PeerNameRegistration实例
                peerNameRegistration = new PeerNameRegistration(peerName, port);
                //    peerNameRegistration.PeerName = peerName;
                //IPEndPointCollection ipEndPoints = new IPEndPointCollection();
                peerNameRegistration.UseAutoEndPointSelection = true;
                // 设定PNRP Peer Name的其他描述信息
                peerNameRegistration.Comment = "PNRP Peer Name的其他描述信息";
                // 设定用PeerNameRegistration的Data描述信息
                peerNameRegistration.Data = System.Text.Encoding.UTF8.GetBytes(String.Format("描述信息,注册时间{0}", DateTime.Now.ToString()));
                // 设定用户端或Peer端点目前参与的所有连接本机的PNRP群
                // peerNameRegistration.Cloud = Cloud.AllLinkLocal;
                // 将PNRP Peer Name注册至PNRP Cloud中
                //  strAllMyPeerName = peerName.ToString();
                peerNameRegistration.Start();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// 解析对等名称
        /// </summary>
        /// <param name="myPeerName"></param>
        public static PeerNameRecordCollection ResolverPeer(String myPeerName)
        {
            Thread.Sleep(500);
            strPeerInfos.Clear();
            // 建立PeerName实例
            PeerName peerName = new PeerName("0." + myPeerName);
            // 建立PeerNameResolver实例
            PeerNameResolver resolver = new PeerNameResolver();
            // 对PNRP Peer Name进行解析
            PeerNameRecordCollection pmrcs = resolver.Resolve(peerName);
            return pmrcs;
        }
    }
}
