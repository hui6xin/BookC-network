using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SAccessControl
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            //DirectoryInfo di = new DirectoryInfo(Path.GetDirectoryName(Application.StartupPath));

            //System.Security.AccessControl.DirectorySecurity dirSecurity = di.GetAccessControl();
            //dirSecurity.AddAccessRule(new FileSystemAccessRule("Everyone", FileSystemRights.FullControl, AccessControlType.Allow));
            //dirSecurity.AddAccessRule(new FileSystemAccessRule("Users", FileSystemRights.FullControl, AccessControlType.Allow));
            //di.SetAccessControl(dirSecurity);

            ////给Excel文件添加"Everyone,Users"用户组的完全控制权限
            //FileInfo fi = new FileInfo(excelPath);
            //System.Security.AccessControl.FileSecurity fileSecurity = fi.GetAccessControl();
            //fileSecurity.AddAccessRule(new FileSystemAccessRule("Everyone", FileSystemRights.FullControl, AccessControlType.Allow));
            //fileSecurity.AddAccessRule(new FileSystemAccessRule("Users", FileSystemRights.FullControl, AccessControlType.Allow));
            //fi.SetAccessControl(fileSecurity);
        }
    }
}
