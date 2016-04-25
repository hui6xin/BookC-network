using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
namespace SnapLibrary
{
    internal class UnmanagedApi
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        internal static extern bool RemoveDirectory(string name);
    }
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        ///// <summary>
        ///// ActiveX 组件快照类，用于网页拍照，将网页保存成图片
        ///// AcitveX 必须实现 IViewObject 接口
        ///// 作者:随飞
        ///// </summary>
        //public class Snapshot
        //{
        //    /// <summary>
        //    /// 取快照
        //    /// </summary>
        //    /// <param name="pUnknown">Com 对象</param>
        //    /// <param name="bmpRect">图象大小</param>
        //    /// <returns></returns>
        //    public Bitmap TakeSnapshot(object pUnknown, Rectangle bmpRect)
        //    {
        //        if (pUnknown == null)
        //            return null;
        //        //必须为com对象
        //        if (!Marshal.IsComObject(pUnknown))
        //            return null;
        //        //IViewObject 接口
        //        SnapLibrary.UnsafeNativeMethods.IViewObject ViewObject = null;
        //        IntPtr pViewObject = IntPtr.Zero;
        //        //内存图
        //        Bitmap pPicture = new Bitmap(bmpRect.Width, bmpRect.Height);
        //        Graphics hDrawDC = Graphics.FromImage(pPicture);
        //        //获取接口
        //        object hret = Marshal.QueryInterface(Marshal.GetIUnknownForObject(pUnknown),
        //          ref UnsafeNativeMethods.IID_IViewObject, out pViewObject);
        //        try
        //        {
        //            ViewObject = Marshal.GetTypedObjectForIUnknown(pViewObject, typeof(SnapLibrary.UnsafeNativeMethods.IViewObject)) as SnapLibrary.UnsafeNativeMethods.IViewObject;
        //            //调用Draw方法
        //            ViewObject.Draw((int)DVASPECT.DVASPECT_CONTENT,
        //              -1,
        //              IntPtr.Zero,
        //              null,
        //              IntPtr.Zero,
        //              hDrawDC.GetHdc(),
        //              new NativeMethods.COMRECT(bmpRect),
        //              null,
        //              IntPtr.Zero,
        //              0);
        //            Marshal.Release(pViewObject);
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine(ex.Message);
        //            throw ex;
        //        }
        //        //释放
        //        hDrawDC.Dispose();
        //        return pPicture;
        //    }
        //}
    }
}
