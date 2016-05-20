using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace MultiDraw
{
    class ToolImage : ToolObject
    {
        public override void OnMouseDown(Palette palette, MouseEventArgs e)
        {
            base.OnMouseDown(palette, e);
            DrawMyImage w = new DrawMyImage(e.X, e.Y, 15, 15, CC.bitmap, CC.ID);
            AddNewObject(palette, w);
            isNewObjectAdded = true;
        }

        public override void OnMouseUp(Palette palette, MouseEventArgs e)
        {
            if (isNewObjectAdded == false)
            {
                return;
            }
            base.OnMouseUp(palette, e);
            int index = CC.myService.FindObjectIndex(CC.ID);
            DrawMyImage w = (DrawMyImage)palette.graphics[index];
            if (CC.userState != UserState.SingleUser)
            {
                GraphicsList myGraphicsList = new GraphicsList();
                myGraphicsList.Add(w.Clone());
                //���л�
                try
                {
                    using (MemoryStream stream = new MemoryStream())
                    {
                        IFormatter formatter = new BinaryFormatter();
                        formatter.Serialize(stream, myGraphicsList);
                        byte[] bytes = stream.GetBuffer();
                        //��ţ����л�����ֽ���
                        CC.me.SendToServer(string.Format("DrawMyImage,{0}", bytes.Length));
                        CC.me.SendToServer(bytes);
                    }
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message, "���л�ʧ��");
                }
                palette.graphics.Remove(CC.ID);
            }
        }
    }
}
