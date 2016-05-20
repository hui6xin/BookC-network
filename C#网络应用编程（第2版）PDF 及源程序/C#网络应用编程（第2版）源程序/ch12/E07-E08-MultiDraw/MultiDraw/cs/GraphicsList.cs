using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Reflection;
using System.Diagnostics;

namespace MultiDraw
{
    [Serializable]
    public class GraphicsList:ISerializable
    {

        private List<DrawObject> _graphicsList;

        /// <summary>
        /// 索引
        /// </summary>
        public IEnumerable<DrawObject> Selection
        {
            get
            {
                foreach (DrawObject o in _graphicsList)
                {
                    if (o.Selected)
                    {
                        yield return o;
                    }
                }
            }
        }
        /// <summary>清除</summary>
        public void Clear()
        {
            _graphicsList.Clear();
        }

        /// <summary>
        /// 对象个数
        /// </summary>
        public int Count
        {
            get
            {
                return _graphicsList.Count;
            }
        }

        public DrawObject this[int index]
        {
            get
            {
                if (index < 0 || index >= _graphicsList.Count)
                {
                    return null;
                }
                return (DrawObject)_graphicsList[index];
            }
        }

        public GraphicsList()
        {
            _graphicsList = new List<DrawObject>();
        }

        /// <summary>
        /// 画图
        /// </summary>
        public void Draw(Graphics g)
        {
            DrawObject w;
            for (int i = 0; i < _graphicsList.Count; i++)
            {
                w = _graphicsList[i];

                if (w.IntersectsWith(Rectangle.Round(g.ClipBounds)))
                {
                    w.Draw(g);
                }

                if (w.Selected == true)
                {
                    w.DrawTracker(g);
                }
            }
        }

        /// <summary>
        /// 选择的对象个数
        /// </summary>
        public int SelectionCount
        {
            get
            {
                int n = 0;

                foreach (DrawObject w in _graphicsList)
                {
                    if (w.Selected) n++;
                }
                return n;
            }
        }

        /// <summary>选择的对象</summary>
        public DrawObject GetSelectedObject(int index)
        {
            int n = -1;
            foreach (DrawObject w in _graphicsList)
            {
                if (w.Selected)
                {
                    n++;
                    if (n == index)
                        return w;
                }
            }
            return null;
        }

        /// <summary>
        /// 添加图形对象
        /// </summary>
        public void Add(DrawObject w)
        {
            _graphicsList.Add(w);
        }

        /// <summary>删除指定对象</summary>
        public void Remove(int objID)
        {
            for (int i = _graphicsList.Count - 1; i >= 0; i--)
            {
                if (_graphicsList[i].ID == objID)
                {
                    _graphicsList.RemoveAt(i);
                }
            }
        }

        /// <summary>删除选中的图形</summary>
        public void DeleteSelection()
        {
            int n = _graphicsList.Count;
            for (int i = n - 1; i >= 0; i--)
            {
                if (_graphicsList[i].Selected)
                {
                    _graphicsList.RemoveAt(i);
                }
            }
            CC.palette.Refresh();
        }

        /// <summary>设置矩形框内选择的对象</summary>
        public void SelectInRectangle(Rectangle rectangle)
        {
            foreach (DrawObject w in _graphicsList)
            {
                if (w.IntersectsWith(rectangle))
                    w.Selected = true;
            }
        }

        /// <summary>全部选择</summary>
        public void SelectAll()
        {
            foreach (DrawObject w in _graphicsList)
            {
                w.Selected = true;
            }
            CC.palette.Refresh();
        }

        /// <summary>全部取消选择</summary>
        public void UnselectAll()
        {
            foreach (DrawObject w in _graphicsList)
            {
                w.Selected = false;
            }
            CC.palette.Refresh();
        }

        #region 序列化与反序列化

        //实现的ISerializable接口，序列化时系统会自动调用它
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("GraphicsCount", _graphicsList.Count);
            for (int i = 0; i < _graphicsList.Count; i++)
            {
                DrawObject w = _graphicsList[i];
                info.AddValue("ObjectType" + i, w.GetType().FullName);
                //调用扩充类中对应的方法
                w.LoadSerializdInfo(info, i);
            }
        }

        //反序列化时系统自动调用的方法（固定参数类型的特殊构造函数）
        protected GraphicsList(SerializationInfo info, StreamingContext context)
        {
            _graphicsList = new List<DrawObject>();
            int n = info.GetInt32("GraphicsCount");
            for (int i = 0; i < n; i++)
            {
                string objectType = info.GetString("ObjectType" + i);
                DrawObject w = (DrawObject)Assembly.GetExecutingAssembly().CreateInstance(objectType);
                w.SaveDeserializdInfo(info, i);
                _graphicsList.Add(w);
            }
        }
        #endregion


       
    }
}
