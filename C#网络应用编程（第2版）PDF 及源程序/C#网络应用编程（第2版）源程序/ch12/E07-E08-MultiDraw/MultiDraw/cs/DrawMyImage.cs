using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.Serialization;

namespace MultiDraw
{
    class DrawMyImage : TrackRectangle
    {
        private Bitmap originalBitmap;

        public override DrawObject Clone()
		{
			DrawMyImage w = new DrawMyImage();
            w.objRectangle = this.objRectangle;
            w.originalBitmap = (Bitmap)this.originalBitmap.Clone();
            AddOtherFields(w);
			return w;
		}

        public DrawMyImage()
        {
        }

        public DrawMyImage(int x, int y, int width, int height, Bitmap bitmap, int id)
        {
            this.objRectangle = new Rectangle(x, y, width, height);
            this.originalBitmap = new Bitmap(bitmap);
            this.id = id;
        }

		public override void Draw(Graphics g)
		{
            if (originalBitmap == null)
            {
                Pen p = new Pen(Color.Black, -1f);
                g.DrawRectangle(p, objRectangle);
            }
            else
            {
                //将bitmap设置为对其默认的透明色透明
                originalBitmap.MakeTransparent();
                g.DrawImage(originalBitmap, objRectangle, 0, 0, originalBitmap.Width, originalBitmap.Height, GraphicsUnit.Pixel);
            }
        }


        #region 序列化与反序列化

        public override void LoadSerializdInfo(SerializationInfo info, int objectIndex)
        {
            base.LoadSerializdInfo(info, objectIndex);
            info.AddValue("originalBitmap" + objectIndex, originalBitmap, typeof(Bitmap));
        }

        public override void SaveDeserializdInfo(SerializationInfo info, int objectIndex)
        {
            base.SaveDeserializdInfo(info, objectIndex);
            originalBitmap = (Bitmap)info.GetValue("originalBitmap" + objectIndex, typeof(Bitmap));
        }

        #endregion

    }
}
