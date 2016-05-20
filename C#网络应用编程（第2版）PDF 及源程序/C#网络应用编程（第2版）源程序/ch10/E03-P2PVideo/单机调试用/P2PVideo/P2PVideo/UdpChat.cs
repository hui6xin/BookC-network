using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;
namespace P2PVideo
{
class UdpChat
{
    private UdpClient udpClient;
    //接收端口
    private int port = 8001;
    MainForm myMainForm;
    public UdpChat(MainForm form)
    {
        myMainForm = form;
        //创建一个线程接收远程主机发来的信息
        Thread myThread = new Thread(ReceiveData);
        myThread.IsBackground = true;
        myThread.Start();
    }
    /// <summary>
    /// 接收线程
    /// </summary>
    private void ReceiveData()
    {
        //在本机指定的端口接收
        udpClient = new UdpClient(port);
        IPEndPoint remote = null;
        String receiveMessage = "";
        //接收从远程主机发送过来的信息；
        while (true)
        {
            try
            {
                //关闭udpClient时此句会产生异常
                byte[] bytes = udpClient.Receive(ref remote);
                receiveMessage =  Encoding.UTF8.GetString(bytes, 0, bytes.Length);
                myMainForm.SetRichTextBoxReceive(String.Format("【{0}】说：{1}",remote.ToString().Split(':')[0],receiveMessage));
            }
            catch
            {
                //退出循环，结束线程
                break;
            }
        }
    }
    /// <summary>
    /// 发送数据到远程主机
    /// </summary>
    public void sendData(String strRemoteIP,String strRemotePort,String strSendMessage)
    {
        UdpClient myUdpClient = new UdpClient();
        IPAddress remoteIP = IPAddress.Parse(strRemoteIP);
        IPEndPoint iep = new IPEndPoint(remoteIP, 8002);
        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(strSendMessage);
        try
        {
            myUdpClient.Send(bytes, bytes.Length, iep);
            myMainForm.ClearRichTextBox();
            myUdpClient.Close();
        }
        catch (Exception err)
        {
            Console.WriteLine(err.Message);
        }
        finally
        {
            myUdpClient.Close();
        }
    }
    private void Close()
    {
        udpClient.Close(); 
    }
}
}
