using System;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace FtpClientExample
{
    public partial class FormMain : Form
    {
        private const int ftpPort = 21;  //控制连接服务器端口
        private string ftpUriString;  //要访问的资源
        private NetworkCredential networkCredential;  //身份验证信息
        private string currentDir = "/"; //客户端当前工作目录
        public FormMain()
        {
            InitializeComponent();
            //为简单起见，此处假设服务器配置在本机，并假定了有效用户名和密码
            IPAddress[] ips = Dns.GetHostAddresses("");
            textBoxServer.Text = ips[0].ToString();
            textBoxUserName.Text = "mytestName";
            textBoxPassword.Text = "12345";
        }
        /// <summary>登录</summary>
        private void buttonLogin_Click(object sender, EventArgs e)
        {
            if (textBoxServer.Text.Length == 0)
            {
                return;
            }
            //拼接要访问的资源URI
            ftpUriString = "ftp://" + textBoxServer.Text;
            networkCredential = new NetworkCredential(textBoxUserName.Text, textBoxPassword.Text);
            if (ShowFtpFileAndDirectory() == true)
            {
                buttonLogin.Enabled = false;
            }
        }
        /// <summary>
        /// 上传文件
        /// </summary>
        private void buttonUpload_Click(object sender, EventArgs e)
        {
            //选取要上传的文件
            OpenFileDialog f = new OpenFileDialog();
            if (f.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            FileInfo fileInfo = new FileInfo(f.FileName);
            string uri = GetUriString(fileInfo.Name);
            FtpWebRequest request = CreateFtpWebRequest(uri, WebRequestMethods.Ftp.UploadFile);
            request.ContentLength = fileInfo.Length;
            int buffLength = 8196;
            byte[] buff = new byte[buffLength];
            FileStream fs = fileInfo.OpenRead();
            try
            {
                Stream responseStream = request.GetRequestStream();
                int contentLen = fs.Read(buff, 0, buffLength);
                while (contentLen != 0)
                {
                    responseStream.Write(buff, 0, contentLen);
                    contentLen = fs.Read(buff, 0, buffLength);
                }
                responseStream.Close();
                fs.Close();
                FtpWebResponse response = GetFtpResponse(request);
                if (response == null)
                {
                    return;
                }
                ShowFtpFileAndDirectory();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "上传失败");
            }
        }
        /// <summary>
        /// 下载文件
        /// </summary>
        private void buttonDownload_Click(object sender, EventArgs e)
        {
            string fileName = GetSelectedFile();
            if (fileName.Length == 0)
            {
                MessageBox.Show("请先选择要下载的文件");
                return;
            }
            string filePath = Application.StartupPath + "\\DownLoad";
            if (Directory.Exists(filePath) == false)
            {
                Directory.CreateDirectory(filePath);
            }
            Stream responseStream = null;
            FileStream fileStream = null;
            StreamReader reader = null;
            try
            {
                string uri = GetUriString(fileName);
                FtpWebRequest request = CreateFtpWebRequest(uri, WebRequestMethods.Ftp.DownloadFile);
                FtpWebResponse response = GetFtpResponse(request);
                if (response == null)
                {
                    return;
                }
                responseStream = response.GetResponseStream();
                string path = filePath + "\\" + fileName;
                fileStream = File.Create(path);
                byte[] buffer = new byte[8196];
                int bytesRead;
                while (true)
                {
                    bytesRead = responseStream.Read(buffer, 0, buffer.Length);
                    if (bytesRead == 0)
                    {
                        break;
                    }
                    fileStream.Write(buffer, 0, bytesRead);
                }
                MessageBox.Show("下载完毕");
            }
            catch (UriFormatException err)
            {
                MessageBox.Show(err.Message);
            }
            catch (WebException err)
            {
                MessageBox.Show(err.Message);
            }
            catch (IOException err)
            {
                MessageBox.Show(err.Message);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                else if (responseStream != null)
                {
                    responseStream.Close();
                }
                if (fileStream != null)
                {
                    fileStream.Close();
                }
            }
        }       
        /// <summary>
        /// 选项发生变化时触发
        /// </summary>
        private void listBoxFtp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxFtp.SelectedIndex > 0)
            {
                string fileName = GetSelectedFile();
                textBoxSelectedFile.Text = fileName;                
            }
        }
        /// <summary>
        /// 双击listBoxFtp时触发
        /// </summary>
        private void listBoxFtp_DoubleClick(object sender, EventArgs e)
        {
            //返回上层目录
            if (listBoxFtp.SelectedIndex == 0)
            {
                if (currentDir == "/")
                {
                    MessageBox.Show("该目录已经是最顶层！", "",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                int index = currentDir.LastIndexOf("/");
                if (index == 0)
                {
                    currentDir = "/";
                }
                else
                {
                    currentDir = currentDir.Substring(0, index);
                }
                ShowFtpFileAndDirectory();
            }
            else if (listBoxFtp.SelectedIndex > 0 && listBoxFtp.SelectedItem.ToString().Contains("[目录]"))
            {
                if (currentDir == "/")
                {
                    currentDir = "/" + listBoxFtp.SelectedItem.ToString().Substring(4);
                }
                else
                {
                    currentDir = currentDir + "/" + listBoxFtp.SelectedItem.ToString().Substring(4);
                }
                ShowFtpFileAndDirectory();
            }
        }
        /// <summary>
        /// 从服务器获取指定路径下文件及子目录列表,并显示
        /// </summary>
        /// <returns>操作是否成功</returns>
        private bool ShowFtpFileAndDirectory()
        {
            listBoxFtp.Items.Clear();
            string uri = string.Empty;
            if (currentDir == "/")
            {
                uri = ftpUriString;
            }
            else
            {
                uri = ftpUriString + currentDir;
            }
            FtpWebRequest request = CreateFtpWebRequest(uri, WebRequestMethods.Ftp.ListDirectoryDetails);
            //获取服务器端响应
            FtpWebResponse response = GetFtpResponse(request);
            if (response == null)
                return false;
            listBoxInfo.Items.Add("服务器返回：" + response.StatusDescription);
            //读取网络流信息
            StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.Default);
            string s = sr.ReadToEnd();
            string[] ftpDir = s.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            //在listBoxInfo中显示服务器响应的原信息
            listBoxInfo.Items.AddRange(ftpDir);
            listBoxInfo.Items.Add("服务器返回：" + response.StatusDescription);
            //添加单击能返回上层目录的项
            listBoxFtp.Items.Add("返回上层目录");
            int len = 0;
            for (int i = 0; i < ftpDir.Length; i++)
            {
                if (ftpDir[i].EndsWith("."))
                {
                    len = ftpDir[i].Length - 2;
                    break;
                }
            }
            for (int i = 0; i < ftpDir.Length; i++)
            {
                s = ftpDir[i];
                int index = s.LastIndexOf('\t');
                if (index == -1)
                {
                    if (len < s.Length)
                        index = len;
                    else
                        continue;
                }
                string name = s.Substring(index + 1);
                if (name == "." || name == "..")
                    continue;
                //判断是否为目录，在项前进行表示
                if (s[0] == 'd' || (s.ToLower()).Contains("<dir>"))
                {
                    listBoxFtp.Items.Add("[目录]" + name);
                }
            }
            for (int i = 0; i < ftpDir.Length; i++)
            {
                s = ftpDir[i];
                int index = s.LastIndexOf('\t');
                if (index == -1)
                {
                    if (len < s.Length)
                        index = len;
                    else
                        continue;
                }
                string name = s.Substring(index + 1);
                if (name == "." || name == "..")
                    continue;
                //判断是否为文件，在项前进行表示
                if (!(s[0] == 'd' || (s.ToLower()).Contains("<dir>")))
                {
                    listBoxFtp.Items.Add("[文件]" + name);
                }
            }
            return true;
        }
        /// <summary>
        /// 创建FtpWebRequest对象
        /// </summary>
        /// <param name="uri">资源标识</param>
        /// <param name="requestMethod">要发送到服务器的命令</param>
        /// <returns>FtpWebRequest对象</returns>
        private FtpWebRequest CreateFtpWebRequest(string uri, string requestMethod)
        {
            FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(uri);
            request.Credentials = networkCredential;
            request.KeepAlive = true;
            request.UseBinary = true;
            request.Method = requestMethod;
            return request;
        }
        /// <summary>
        /// 获得服务器端响应信息
        /// </summary>
        /// <param name="request">FtpWebRequest对象</param>
        /// <returns>FtpWebResponse对象</returns>
        private FtpWebResponse GetFtpResponse(FtpWebRequest request)
        {
            FtpWebResponse response = null;
            try
            {
                response = (FtpWebResponse)request.GetResponse();
                return response;
            }
            catch (WebException err)
            {
                listBoxInfo.Items.Add("出现异常，FTP返回状态：" + err.Status.ToString());
                FtpWebResponse e = (FtpWebResponse)err.Response;
                listBoxInfo.Items.Add("Status Code :" + e.StatusCode);
                listBoxInfo.Items.Add("Status Description :" + e.StatusDescription);
                return null;
            }
            catch (Exception err)
            {
                listBoxInfo.Items.Add(err.Message);
                return null;
            }
        }
        /// <summary>
        /// 获取在listBoxFtp中所选择文件的文件名
        /// </summary>
        /// <returns>所选择文件的文件名</returns>
        private string GetSelectedFile()
        {
            string fileName = "";
            if (!(listBoxFtp.SelectedIndex == -1 ||
                listBoxFtp.SelectedItem.ToString().Substring(0, 4) == "[目录]"))
            {
                fileName = listBoxFtp.SelectedItem.ToString().Substring(4).Trim();
            }            
            return fileName;
        }
        private string GetUriString(string fileName)
        {
            string uri = string.Empty;
            if (currentDir.EndsWith("/"))
            {
                uri = ftpUriString + currentDir + fileName;
            }
            else
            {
                uri = ftpUriString + currentDir + "/" + fileName;
            }
            return uri;
        }   
    }
}