using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HttpMultiThreadDownloadExample
{
    /// <summary>    
    /// 下载数据参数    
    /// </summary>
    public class DownloadEventArgs : EventArgs
    {
        int bytesReceived;
        int totalBytes;
        byte[] receivedData;
        /// <summary>    
        /// 已接收的字节数    
        /// </summary>    
        public int BytesReceived
        {
            get { return bytesReceived; }
            set { bytesReceived = value; }
        }
        /// <summary>
        /// 总字节数
        /// </summary>
        public int TotalBytes
        {
            get { return totalBytes; }
            set { totalBytes = value; }
        }
        /// <summary>    
        /// 当前缓冲区接收的数据    
        /// </summary>    
        public byte[] ReceivedData
        {
            get { return receivedData; }
            set { receivedData = value; }
        }
    }
}
