using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsForms5
{
    class GameTable
    {
        private const int None = -1;
        private const int Black = 0;
        private const int White = 1;
        public Player[] gamePlayer;
        private int[,] grid = new int[15, 15];
        private System.Timers.Timer timer;
        private int NextdotColor = 0;
        private ListBox listbox;
        Random rnd = new Random();
        Service service;
        public GameTable(ListBox listbox)
        {
            gamePlayer = new Player[2];
            gamePlayer[0] = new Player();
            gamePlayer[1] = new Player();
            timer = new System.Timers.Timer();
            timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);
            timer.Enabled = false;
            this.listbox = listbox;
            service = new Service(listbox);
            ResetGrid();
        }
        public void ResetGrid()
        {
            for (int i = 0; i <= grid.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= grid.GetUpperBound(1); i++)
                {
                    grid[i, j] = None;
                }
            }
            gamePlayer[0].grade = 0;
            gamePlayer[1].grade = 0;
        }
        public void StartTimer()
        {
            timer.Start();
        }
        public void StopTimer()
        {
            timer.Stop();
        }
        public void SetTimerLevel(int interval)
        {
            timer.Interval = interval;
        }
        private void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            int x, y;
            do
            {
                x = rnd.Next(15);
                y = rnd.Next(15);
            } while (grid[x, y] != None);
            SetDot(x, y, NextdotColor);
            NextdotColor = (NextdotColor + 1) % 2;
        }
        private void SetDot(int i, int j, int dotColor)
        {
            //seng to users ,and judge if some dot nearby
            grid[i, j] = dotColor;
            service.SendToBoth(this,string .Format("SetDot,{0,{1,{2}}",i,j,dotColor));
            /*-------------------------一下判断当前航是否有相邻点----------*/
            int k1, k2=0;//k1:循环初值，k2:循环终值
            if (i == 0)
            {
                //如果是首行，只需要判断下边的点
                k1 = k2 = i;
            }
            else if (i == grid.GetUpperBound(0))
            {
                //如果是终行，只需要判断上边的点
                k1 = k1 = grid.GetUpperBound(0) - 1;
            }
            else
            {
                //如果是中间的行，上下都要判断
                k1 = i - 1;
                k2 = i + 1;
            }
            for (int x = k1; x <= k2; x += 2)
            {
                if (grid[x, j] == dotColor)
                {
                    ShowWin(dotColor);
                }
            }
            /*------以下判断当前列是否有相邻点-----*/
            if (j== 0)
            {
                //如果是首行，只需要判断下边的点
                k1 = k2 = j;
            }
            else if (j == grid.GetUpperBound(1))
            {
                //如果是终行，只需要判断上边的点
                k1 = k1 = grid.GetUpperBound(1) - 1;
            }
            else
            {
                //如果是中间的行，上下都要判断
                k1 = j - 1;
                k2 = j + 1;
            }
            for (int y = k1; y <= k2; y += 2)
            {
                if (grid[i, y] == dotColor)
                {
                    ShowWin(dotColor);
                }
            }
        }
        /// <summary>
        /// 出现相邻点的颜色为dotcolor
        /// </summary>
        /// <param name="dotcolor">相邻点的dotcolor</param>
        private void ShowWin(int dotcolor)
        {
            timer.Enabled = false;
            gamePlayer[0].started = false;
            gamePlayer[1].started = false;
            this.ResetGrid();
            //发送格式：win 相邻点的颜色，黑色成绩，白色成绩
            service.SendToBoth(this,string.Format("Win,{0},{1},{2}",dotcolor,gamePlayer[0].grade,gamePlayer[1].grade));
        }
        /// <summary>
        /// 消去棋子的信息
        /// </summary>
        /// <param name="i">行号</param>
        /// <param name="j">列号</param>
        /// <param name="color">棋子颜色</param>
        public void UnsetDot(int i,int j,int color)
        {
        }
    }
}
