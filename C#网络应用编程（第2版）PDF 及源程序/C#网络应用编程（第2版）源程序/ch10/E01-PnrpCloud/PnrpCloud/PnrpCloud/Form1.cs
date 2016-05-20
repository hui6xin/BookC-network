using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.PeerToPeer;

namespace PnrpCloud
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ShowCloudList();
        }

        /// <summary>
        /// 显示群信息
        /// </summary>
        public void ShowCloudList()
        { 
            CloudCollection clouds = Cloud.GetAvailableClouds();
            textBoxCloud.Text = String.Format("发现{0}个群\r\n\r\n", clouds.Count);
           
            String strSomeCloudInfo = "";
            int index = 0;
            foreach (Cloud someCloud in clouds)   
            {
                index++;
                strSomeCloudInfo += String.Format("第{0}个群\r\n",index);
                strSomeCloudInfo += String.Format("群名称:{0}\r\n", someCloud.Name);
                strSomeCloudInfo += String.Format("群编号:{0}\r\n", someCloud.ScopeId);
                strSomeCloudInfo += String.Format("群范围:{0}\r\n\r\n", someCloud.Scope);
                textBoxCloud.Text += strSomeCloudInfo;
                strSomeCloudInfo = ""; 
            }   
        }
    }
}
