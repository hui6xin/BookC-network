using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Net.PeerToPeer;
using System.Net;
using System.Diagnostics;
using System.Net.Sockets;

namespace ListNeighber
{
    public partial class FormListNeighber : Form
    {
        static String strMyPeerName = "myApplicationName";
        static String strAllMyPeerName = "";
        static int port;
        static PeerNameRegistration peerNameRegistration;
        public FormListNeighber()
        {
            InitializeComponent();
        }
        public static void getPort()
        {
            while (true)
            {
                port = new Random().Next(50000, 51000);
                try
                {
                    TcpListener listener = new TcpListener(IPAddress.Any, port);
                    listener.Start();
                }
                catch
                {
                    continue;
                }
                break;
            }
        }
        /// <summary>
        /// 将PNRP Name 注册到Cloud中
        /// </summary>
        public static void registerPeer()
        {
            getPort();
            PeerName peerName = new PeerName(strMyPeerName, PeerNameType.Unsecured);
            // 以PeerName创建PeerNameRegistration实例
            peerNameRegistration = new PeerNameRegistration(peerName, port);
            // 设定PNRP Peer Name的其他描述信息
            peerNameRegistration.Comment = "PNRP Peer Name的其他描述信息";
            // 设定用PeerNameRegistration的Data描述信息
            peerNameRegistration.Data = System.Text.Encoding.UTF8.GetBytes(String.Format("描述信息,注册时间{0}",DateTime.Now.ToString()));
            // 设定用户端或Peer端点目前参与的所有连接本机的PNRP群
            // peerNameRegistration. = Cloud.AllLinkLocal;
            // 将PNRP Peer Name注册至PNRP Cloud中
            strAllMyPeerName = peerName.ToString();
            peerNameRegistration.Start();
        }
        /// <summary>
        /// 解析对等名称
        /// </summary>
        /// <param name="myPeerName"></param>
        public static void ResolverPeer(String myPeerName)
        {
            // 建立PeerName实例
            PeerName peerName = new PeerName("0." + myPeerName);
            // 建立PeerNameResolver实例
            PeerNameResolver resolver = new PeerNameResolver();
            // 对PNRP Peer Name进行解析
            PeerNameRecordCollection pmrcs = resolver.Resolve(peerName);

            foreach (PeerNameRecord pmrc in pmrcs)
            {
                foreach (IPEndPoint endpoint in pmrc.EndPointCollection)
                {
                    Console.WriteLine(endpoint);
                }
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            registerPeer();
            ResoveName("0."+strMyPeerName);
        }
        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            ResoveName("0." + strMyPeerName);
        }
        /// <summary>
        /// 解析名称
        /// </summary>
        private void ResoveName(String strPeerName)
        {
            listView1.Items.Clear();
            PeerName myPeer = new PeerName(strPeerName);
            PeerNameResolver myRes = new PeerNameResolver();
            PeerNameRecordCollection recColl = myRes.Resolve(myPeer);
            foreach (PeerNameRecord record in recColl)
            {
                foreach (IPEndPoint endP in record.EndPointCollection)
                {
                    if (endP.AddressFamily.Equals(AddressFamily.InterNetwork))
                    {
                        ListViewItem item1 = new ListViewItem();
                        item1.SubItems.Add(record.PeerName.ToString());
                        item1.SubItems.Add(endP.ToString());
                        item1.SubItems.Add(Encoding.UTF8.GetString(record.Data));
                        item1.SubItems.Add(record.PeerName.PeerHostName);
                        listView1.Items.Add(item1);
                    }
                }
            }
        }
    }
}
