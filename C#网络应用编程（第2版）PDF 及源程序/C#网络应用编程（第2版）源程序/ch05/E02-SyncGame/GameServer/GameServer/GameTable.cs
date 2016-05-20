//-------------------GameTable.cs-----------------//
using System;
using System.Timers;
using System.Windows.Forms;
namespace GameServer
{
    class GameTable
    {
        private const int None = -1;       //无棋子
        private const int Black = 0;       //黑色棋子
        private const int White = 1;       //白色棋子
        public Player[] gamePlayer;        //保存同一桌的玩家信息
        private int[,] grid = new int[15, 15];       //15*15的方格
        private System.Timers.Timer timer;       //用于定时产生棋子
        private int NextdotColor = 0;            //应该产生黑棋子还是白棋子
        private ListBox listbox;
        Random rnd = new Random();
        Service service;
        public GameTable(ListBox listbox)
        {
            gamePlayer = new Player[2];
            gamePlayer[0] = new Player();
            gamePlayer[1] = new Player();
            timer = new System.Timers.Timer();
            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
            timer.Enabled = false;
            this.listbox = listbox;
            service = new Service(listbox);
            ResetGrid();
        }
        /// <summary>重置棋盘</summary>
        public void ResetGrid()
        {
            for (int i = 0; i <= grid.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= grid.GetUpperBound(1); j++)
                {
                    grid[i, j] = None;
                }
            }
            gamePlayer[0].grade = 0;
            gamePlayer[1].grade = 0;
        }
        /// <summary>启动Timer</summary>
        public void StartTimer()
        {
            timer.Start();
        }
        /// <summary>停止Timer</summary>
        public void StopTimer()
        {
            timer.Stop();
        }
        /// <summary>设置时间间隔</summary>
        /// <param name="interval">时间间隔</param>
        public void SetTimerLevel(int interval)
        {
            timer.Interval = interval;
        }
        /// <summary>达到时间间隔时处理事件</summary>
        private void timer_Elapsed(object sender, EventArgs e)
        {
            int x, y;
            //随机产生一个格内没有棋子的单元格位置
            do
            {
                x = rnd.Next(15);  //产生一个小于15的非负整数
                y = rnd.Next(15);
            } while (grid[x, y] != None);
            //放置棋子:x坐标,y坐标,颜色
            SetDot(x, y, NextdotColor);
            //设定下次分发的旗子颜色
            NextdotColor = (NextdotColor + 1) % 2;
        }
        /// <summary>发送产生的棋子信息</summary>
        /// <param name="i">指定棋盘的第几行</param>
        /// <param name="j">指定棋盘的第几列</param>
        /// <param name="dotColor">棋子颜色</param>
        private void SetDot(int i, int j, int dotColor)
        {
            //向两个用户发送产生的棋子信息，并判断是否有相邻棋子
            //发送格式：SetDot,行,列,颜色
            grid[i, j] = dotColor;
            service.SendToBoth(this, string.Format("SetDot,{0},{1},{2}", i, j, dotColor));
            /*----------以下判断当前行是否有相邻点----------*/
            int k1, k2;   //k1:循环初值，k2:循环终值
            if (i == 0)
            {
                //如果是首行，只需要判断下边的点
                k1 = k2 = 1;
            }
            else if (i == grid.GetUpperBound(0))
            {
                //如果是最后一行，只需要判断上边的点
                k1 = k2 = grid.GetUpperBound(0) - 1;
            }
            else
            {
                //如果是中间的行，上下两边的点都要判断
                k1 = i - 1; k2 = i + 1;
            }
            for (int x = k1; x <= k2; x += 2)
            {
                if (grid[x, j] == dotColor)
                {
                    ShowWin(dotColor);
                }
            }
            /*-------------以下判断当前列是否有相邻点------------------*/
            if (j == 0)
            {
                k1 = k2 = 1;
            }
            else if (j == grid.GetUpperBound(1))
            {
                k1 = k2 = grid.GetUpperBound(1) - 1;
            }
            else
            {
                k1 = j - 1; k2 = j + 1;
            }
            for (int y = k1; y <= k2; y += 2)
            {
                if (grid[i, y] == dotColor)
                {
                    ShowWin(dotColor);
                }
            }
        }
        /// <summary>出现相邻点的颜色为dotColor</summary>
        /// <param name="dotColor">相邻点的颜色</param>
        private void ShowWin(int dotColor)
        {
            timer.Enabled = false;
            gamePlayer[0].started = false;
            gamePlayer[1].started = false;
            this.ResetGrid();
            //发送格式：Win,相邻点的颜色,黑方成绩,白方成绩
            service.SendToBoth(this, string.Format("Win,{0},{1},{2}",
                dotColor, gamePlayer[0].grade, gamePlayer[1].grade));
        }
        /// <summary>消去棋子的信息</summary>
        /// <param name="i">指定棋盘的第几行</param>
        /// <param name="j">指定棋盘的第几列</param>
        /// <param name="color">指定棋子颜色</param>
        public void UnsetDot(int i, int j, int color)
        {
            //向两个用户发送消去棋子的信息
            //格式：UnsetDot,行,列,黑方成绩,白方成绩
            grid[i, j] = None;
            gamePlayer[color].grade++;
            string str = string.Format("UnsetDot,{0},{1},{2},{3}",
                i, j, gamePlayer[0].grade, gamePlayer[1].grade);
            service.SendToBoth(this, str);
        }
    }
}
