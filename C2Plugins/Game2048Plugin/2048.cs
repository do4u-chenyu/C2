//*******************************************************************
//
//      文件名（File Name）：          frmMain.cs
//
//      功能描述（Description）：      2048游戏交互逻辑
//
//      数据表（Tables）：             nothing
//                            
//      作者（Author）：               MH
//
//      日期（Create Date）：          2014.6.5
//
//
//*******************************************************************
using C2.IAOLab.Plugins;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace _2048
{
    public partial class frmMain : Form, IPlugin
    {
        public frmMain()
        {
            InitializeComponent();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    PictureBox pb = new PictureBox();
                    pb.Location = new Point(75 * j + 6, 75 * i + 6);//使方格间有间距
                    pb.Size = new Size(70, 70);
                    pb.BorderStyle = BorderStyle.FixedSingle;
                    pb.SizeMode = PictureBoxSizeMode.StretchImage;
                    pb.BackColor = Color.FromArgb(204,192,180);
                    pboxes[i,j] = pb; //标记号的转换,二维的转一维
                    plBoard.Controls.Add(pb);
                }
            }
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    blocknumber[i,j] = BLANK_STATE;
                }
            }
            plBoard.SendToBack();
        }


        int BLANK_STATE = -1;//无图片的状态
        PictureBox[,] pboxes = new PictureBox[4,4];
        int[,] blocknumber = new int[4,4];//存储相应方块的ID号
        System.Windows.Forms.Timer[] tmrs = new System.Windows.Forms.Timer[16];//16个计时器
        Point[] blankblock = new Point[16];//空白的PictureBox
        bool KeyboardEnable = false;//设置键盘能不能用
        int offset = 25;//每次移动的偏移量
        Bitmap two = _2048.Properties.Resources._2;
        Bitmap four = _2048.Properties.Resources._4;
        Bitmap eight = _2048.Properties.Resources._8;
        Bitmap sixteen = _2048.Properties.Resources._16;
        Bitmap thirty_two = _2048.Properties.Resources._32;
        Bitmap sixty_four = _2048.Properties.Resources._64;
        Bitmap one_hundred_twenty_eight = _2048.Properties.Resources._128;
        Bitmap two_hundred_fifty_six = _2048.Properties.Resources._256;
        Bitmap five_hundred_twelve = _2048.Properties.Resources._512;
        Bitmap one_thousand_twenty_four = _2048.Properties.Resources._1024;
        Bitmap two_thousand_forty_eight = _2048.Properties.Resources._2048;


        //产生与数字对应的图片
        private Bitmap MatchImage(int n)
        {
            switch (n)
            {
                case 2:
                    return two;
                case 4:
                    return four;
                case 8:
                    return eight;
                case 16:
                    return sixteen;
                case 32:
                    return thirty_two;
                case 64:
                    return sixty_four;
                case 128:
                    return one_hundred_twenty_eight;
                case 256:
                    return two_hundred_fifty_six;
                case 512:
                    return five_hundred_twelve;
                case 1024:
                    return one_thousand_twenty_four;
                case 2048:
                    return two_thousand_forty_eight;
                default:
                    return null;
            }
        }

        //重写方法
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (KeyboardEnable)
            {
                switch (keyData)
                {
                    case Keys.Up:
                        Key_Up();
                        OpenAllTimerAndBeginWatch();
                        break;
                    case Keys.Down:
                        Key_Down();
                        OpenAllTimerAndBeginWatch();
                        break;
                    case Keys.Left:
                        Key_Left();
                        OpenAllTimerAndBeginWatch();
                            break;
                    case Keys.Right:
                        Key_Right();
                        OpenAllTimerAndBeginWatch();
                        break;
                }
            }
            return base.ProcessDialogKey(keyData);
        }

        /// <summary>
        /// 产生一个新的PictureBox形成动画效果，使用了匿名方法访问局部变量
        /// </summary>
        /// <param name="n_start">起始的PictureBox</param>
        /// <param name="n_end">终点的PictureBox</param>
        /// <param name="direction">移动的方向，0为上，1为下，2为左，3为右</param>
        /// <param name="Isempty">终点是否为空</param>

        private void MoveAnimation(Point n_start,Point n_end,int direction,bool Isempty)
        {
            if(direction==0||direction==1)//坐标相等不产生动画
            {
                if (n_start.X == n_end.X)
                    return;
            }
            if(direction==2||direction==3)
            {
                if (n_start.Y == n_end.Y)
                    return;
            }
            PictureBox newpic = new PictureBox();
            newpic.Size = pboxes[n_start.X,n_start.Y].Size;
            newpic.Location = pboxes[n_start.X, n_start.Y].Location;
            newpic.SizeMode = PictureBoxSizeMode.StretchImage;
            newpic.Image = pboxes[n_start.X, n_start.Y].Image;
            pboxes[n_start.X, n_start.Y].Image = null;//让图片为空
            this.plBoard.Controls.Add(newpic);
            newpic.BringToFront();
            tmrs[n_start.X * 4 + n_start.Y] = new System.Windows.Forms.Timer();
            tmrs[n_start.X * 4 + n_start.Y].Interval = 18;

            if(direction==0)//往上运动
            {
                if(Isempty==true)
                {
                    tmrs[n_start.X * 4 + n_start.Y].Tick += delegate(object sender, EventArgs e)
                    {
                        if (newpic.Top <= pboxes[n_end.X,n_end.Y].Top)
                        {
                            pboxes[n_end.X, n_end.Y].Image = newpic.Image;
                            newpic.Dispose();
                            tmrs[n_start.X * 4 + n_start.Y].Stop();
                            tmrs[n_start.X * 4 + n_start.Y].Dispose();
                            tmrs[n_start.X * 4 + n_start.Y] = null;
                        }
                        else
                            newpic.Top -= offset;
                    };
                    blocknumber[n_end.X, n_end.Y] = blocknumber[n_start.X, n_start.Y];
                    blocknumber[n_start.X, n_start.Y] = BLANK_STATE;
                }
                else
                {
                    int old = blocknumber[n_start.X, n_start.Y];
                    tmrs[n_start.X * 4 + n_start.Y].Tick += delegate(object sender, EventArgs e)
                    {
                        if (newpic.Top <= pboxes[n_end.X, n_end.Y].Top)
                        {
                            pboxes[n_end.X, n_end.Y].Image = MatchImage(old * 2);
                            newpic.Dispose();
                            tmrs[n_start.X * 4 + n_start.Y].Stop();
                            tmrs[n_start.X * 4 + n_start.Y].Dispose();
                            tmrs[n_start.X * 4 + n_start.Y] = null;
                        }
                        else
                            newpic.Top -= offset;
                    };
                    blocknumber[n_end.X, n_end.Y] = blocknumber[n_start.X, n_start.Y] * 2;
                    blocknumber[n_start.X, n_start.Y] = BLANK_STATE;
                }
            }
            else if(direction==1)//往下运动
            {
                if(Isempty==true)
                {
                    tmrs[n_start.X * 4 + n_start.Y].Tick += delegate(object sender, EventArgs e)
                    {
                        if (newpic.Top >= pboxes[n_end.X, n_end.Y].Top)
                        {
                            pboxes[n_end.X, n_end.Y].Image = newpic.Image;
                            newpic.Dispose();
                            tmrs[n_start.X * 4 + n_start.Y].Stop();
                            tmrs[n_start.X * 4 + n_start.Y].Dispose();
                            tmrs[n_start.X * 4 + n_start.Y] = null;
                        }
                        else
                            newpic.Top += offset;
                    };
                    blocknumber[n_end.X, n_end.Y] = blocknumber[n_start.X, n_start.Y];
                    blocknumber[n_start.X, n_start.Y] = BLANK_STATE;
                }
                else
                {
                    int old = blocknumber[n_start.X, n_start.Y];
                    tmrs[n_start.X * 4 + n_start.Y].Tick += delegate(object sender, EventArgs e)
                    {
                        if (newpic.Top >= pboxes[n_end.X, n_end.Y].Top)
                        {
                            pboxes[n_end.X, n_end.Y].Image = MatchImage(old * 2);
                            newpic.Dispose();
                            tmrs[n_start.X * 4 + n_start.Y].Stop();
                            tmrs[n_start.X * 4 + n_start.Y].Dispose();
                            tmrs[n_start.X * 4 + n_start.Y] = null;
                        }
                        else
                            newpic.Top += offset;
                    };
                    blocknumber[n_end.X, n_end.Y] = blocknumber[n_start.X, n_start.Y] * 2;
                    blocknumber[n_start.X, n_start.Y] = BLANK_STATE;
                }
            }
            else if(direction==2)//往左运动
            {
                if (Isempty == true)
                {
                    tmrs[n_start.X * 4 + n_start.Y].Tick += delegate(object sender, EventArgs e)
                    {
                        if (newpic.Left <= pboxes[n_end.X, n_end.Y].Left)
                        {
                            pboxes[n_end.X, n_end.Y].Image = newpic.Image;
                            newpic.Dispose();
                            tmrs[n_start.X * 4 + n_start.Y].Stop();
                            tmrs[n_start.X * 4 + n_start.Y].Dispose();
                            tmrs[n_start.X * 4 + n_start.Y] = null;
                        }
                        else
                            newpic.Left -= offset;
                    };
                    blocknumber[n_end.X, n_end.Y] = blocknumber[n_start.X, n_start.Y];
                    blocknumber[n_start.X, n_start.Y] = BLANK_STATE;
                }
                else
                {
                    int old = blocknumber[n_start.X, n_start.Y];
                    tmrs[n_start.X * 4 + n_start.Y].Tick += delegate(object sender, EventArgs e)
                    {
                        if (newpic.Left <= pboxes[n_end.X, n_end.Y].Left)
                        {
                            pboxes[n_end.X, n_end.Y].Image = MatchImage(old * 2);
                            newpic.Dispose();
                            tmrs[n_start.X * 4 + n_start.Y].Stop();
                            tmrs[n_start.X * 4 + n_start.Y].Dispose();
                            tmrs[n_start.X * 4 + n_start.Y] = null;
                        }
                        else
                            newpic.Left -= offset;
                    };
                    blocknumber[n_end.X, n_end.Y] = blocknumber[n_start.X, n_start.Y] * 2;
                    blocknumber[n_start.X, n_start.Y] = BLANK_STATE;
                }
            }
            else//往右运动
            {
                if (Isempty == true)
                {
                    tmrs[n_start.X * 4 + n_start.Y].Tick += delegate(object sender, EventArgs e)
                    {
                        if (newpic.Left >= pboxes[n_end.X, n_end.Y].Left)
                        {
                            pboxes[n_end.X, n_end.Y].Image = newpic.Image;
                            newpic.Dispose();
                            tmrs[n_start.X * 4 + n_start.Y].Stop();
                            tmrs[n_start.X * 4 + n_start.Y].Dispose();
                            tmrs[n_start.X * 4 + n_start.Y] = null;
                        }
                        else
                            newpic.Left += offset;
                    };
                    blocknumber[n_end.X, n_end.Y] = blocknumber[n_start.X, n_start.Y];
                    blocknumber[n_start.X, n_start.Y] = BLANK_STATE;
                }
                else
                {
                    int old = blocknumber[n_start.X, n_start.Y];
                    tmrs[n_start.X * 4 + n_start.Y].Tick += delegate(object sender, EventArgs e)
                    {
                        if (newpic.Left >= pboxes[n_end.X, n_end.Y].Left)
                        {
                            pboxes[n_end.X, n_end.Y].Image = MatchImage(old * 2);
                            newpic.Dispose();
                            tmrs[n_start.X * 4 + n_start.Y].Stop();
                            tmrs[n_start.X * 4 + n_start.Y].Dispose();
                            tmrs[n_start.X * 4 + n_start.Y] = null;
                        }
                        else
                            newpic.Left += offset;
                    };
                    blocknumber[n_end.X, n_end.Y] = blocknumber[n_start.X, n_start.Y] * 2;
                    blocknumber[n_start.X, n_start.Y] = BLANK_STATE;
                }
            }
        }


        /// <summary>
        /// 开启所有的计时器，如果全为空，直接返回
        /// </summary>
        private void OpenAllTimerAndBeginWatch()
        {
            bool allnull = true;
            for (int i = 0; i < 16; i++)
            {
                if (tmrs[i] != null)
                    allnull = false;
            }
            if (allnull)
                return;
            
            KeyboardEnable = false;
            tmrWatch.Start();
            for(int i=0;i<16;i++)
            {
                if (tmrs[i] != null)
                    tmrs[i].Start();
            }
        }


        #region  上下左右四个方向的判断

        /// <summary>
        /// 往上运动
        /// </summary>
        private void Key_Up()
        {
            int empty_num = 0;
            for (int j = 0; j < 4; j++)
            {
                empty_num = CountEmptyNum(j, 1);
                if (empty_num == 4)
                    continue;
                else if (empty_num == 3)
                {
                    if(blocknumber[0,j]==BLANK_STATE)
                    {
                        int index = FindWhichBlockNotEmpty(j, 1);
                        MoveAnimation(new Point(index, j), new Point(0, j), 0, true);
                    }
                }
                else if(empty_num==2)
                {
                    Point p = FindWhichTwoBlockEmpty(j, 1);
                    List<int> nums = new List<int>() { 0, 1, 2, 3 };
                    nums.Remove(p.X);
                    nums.Remove(p.Y);
                    int a = nums[0];
                    int b = nums[1];
                    if(blocknumber[a,j]==blocknumber[b,j])
                    {
                        MoveAnimation(new Point(a, j), new Point(0, j), 0, true);
                        MoveAnimation(new Point(b, j), new Point(0, j), 0, false);
                    }
                    else
                    {
                        MoveAnimation(new Point(a, j), new Point(0, j), 0, true);
                        MoveAnimation(new Point(b, j), new Point(1, j), 0, true);
                    }
                }
                else if(empty_num==1)
                {
                    int index = FindWhichBlockEmpty(j, 1);
                    List<int> nums = new List<int>() { 0, 1, 2, 3 };
                    nums.Remove(index);
                    int a = nums[0];
                    int b = nums[1];
                    int c = nums[2];
                    if(blocknumber[a,j]==blocknumber[b,j])
                    {
                        MoveAnimation(new Point(a, j), new Point(0, j), 0, true);
                        MoveAnimation(new Point(b, j), new Point(0, j), 0, false);
                        MoveAnimation(new Point(c, j), new Point(1, j), 0, true);
                    }
                    else
                    {
                        if(blocknumber[b,j]==blocknumber[c,j])
                        {
                            MoveAnimation(new Point(a, j), new Point(0, j), 0, true);
                            MoveAnimation(new Point(b, j), new Point(1, j), 0, true);
                            MoveAnimation(new Point(c, j), new Point(1, j), 0, false);
                        }
                        else
                        {
                            MoveAnimation(new Point(a, j), new Point(0, j), 0, true);
                            MoveAnimation(new Point(b, j), new Point(1, j), 0, true);
                            MoveAnimation(new Point(c, j), new Point(2, j), 0, true);
                        }
                    }
                }
                else
                {
                    if(blocknumber[0,j]==blocknumber[1,j])
                    {
                        if(blocknumber[2,j]==blocknumber[3,j])
                        {
                            MoveAnimation(new Point(1, j), new Point(0, j), 0, false);
                            MoveAnimation(new Point(2, j), new Point(1, j), 0, true);
                            MoveAnimation(new Point(3, j), new Point(1, j), 0, false);
                        }
                        else
                        {
                            MoveAnimation(new Point(1, j), new Point(0, j), 0, false);
                            MoveAnimation(new Point(2, j), new Point(1, j), 0, true);
                            MoveAnimation(new Point(3, j), new Point(2, j), 0, true);
                        }
                    }
                    else
                    {
                        if(blocknumber[1,j]==blocknumber[2,j])
                        {
                            MoveAnimation(new Point(2, j), new Point(1, j), 0, false);
                            MoveAnimation(new Point(3, j), new Point(2, j), 0, true);
                        }
                        else
                        {
                            if(blocknumber[2,j]==blocknumber[3,j])
                            {
                                MoveAnimation(new Point(3, j), new Point(2, j), 0, false);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 往下运动
        /// </summary>
        private void Key_Down()
        {
            int empty_num = 0;
            for (int j = 0; j < 4; j++)
            {
                empty_num = CountEmptyNum(j, 1);
                if (empty_num == 4)
                    continue;
                else if (empty_num == 3)
                {
                    if (blocknumber[3, j] == BLANK_STATE)
                    {
                        int index = FindWhichBlockNotEmpty(j, 1);
                        MoveAnimation(new Point(index, j), new Point(3, j), 1, true);
                    }
                }
                else if (empty_num == 2)
                {
                    Point p = FindWhichTwoBlockEmpty(j, 1);
                    List<int> nums = new List<int>() { 0, 1, 2, 3 };
                    nums.Remove(p.X);
                    nums.Remove(p.Y);
                    int a = nums[0];
                    int b = nums[1];
                    if (blocknumber[a, j] == blocknumber[b, j])
                    {
                        MoveAnimation(new Point(b, j), new Point(3, j), 1, true);
                        MoveAnimation(new Point(a, j), new Point(3, j), 1, false);
                    }
                    else
                    {
                        MoveAnimation(new Point(b, j), new Point(3, j), 1, true);
                        MoveAnimation(new Point(a, j), new Point(2, j), 1, true);
                    }
                }
                else if (empty_num == 1)
                {
                    int index = FindWhichBlockEmpty(j, 1);
                    List<int> nums = new List<int>() { 0, 1, 2, 3 };
                    nums.Remove(index);
                    int a = nums[0];
                    int b = nums[1];
                    int c = nums[2];
                    if (blocknumber[b, j] == blocknumber[c, j])
                    {
                        MoveAnimation(new Point(c, j), new Point(3, j), 1, true);
                        MoveAnimation(new Point(b, j), new Point(3, j), 1, false);
                        MoveAnimation(new Point(a, j), new Point(2, j), 1, true);
                    }
                    else
                    {
                        if (blocknumber[a, j] == blocknumber[b, j])
                        {
                            MoveAnimation(new Point(c, j), new Point(3, j), 1, true);
                            MoveAnimation(new Point(b, j), new Point(2, j), 1, true);
                            MoveAnimation(new Point(a, j), new Point(2, j), 1, false);
                        }
                        else
                        {
                            MoveAnimation(new Point(c, j), new Point(3, j), 1, true);
                            MoveAnimation(new Point(b, j), new Point(2, j), 1, true);
                            MoveAnimation(new Point(a, j), new Point(1, j), 1, true);
                        }
                    }
                }
                else
                {
                    if (blocknumber[2, j] == blocknumber[3, j])
                    {
                        if (blocknumber[1, j] == blocknumber[0, j])
                        {
                            MoveAnimation(new Point(2, j), new Point(3, j), 1, false);
                            MoveAnimation(new Point(1, j), new Point(2, j), 1, true);
                            MoveAnimation(new Point(0, j), new Point(2, j), 1, false);
                        }
                        else
                        {
                            MoveAnimation(new Point(2, j), new Point(3, j), 1, false);
                            MoveAnimation(new Point(1, j), new Point(2, j), 1, true);
                            MoveAnimation(new Point(0, j), new Point(1, j), 1, true);
                        }
                    }
                    else
                    {
                        if (blocknumber[1, j] == blocknumber[2, j])
                        {
                            MoveAnimation(new Point(1, j), new Point(2, j), 1, false);
                            MoveAnimation(new Point(0, j), new Point(1, j), 1, true);
                        }
                        else
                        {
                            if (blocknumber[1, j] == blocknumber[0, j])
                            {
                                MoveAnimation(new Point(0, j), new Point(1, j), 1, false);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 向左运动
        /// </summary>
        private void Key_Left()
        {
            int empty_num = 0;
            for (int i = 0; i < 4; i++)
            {
                empty_num = CountEmptyNum(i, 0);
                if (empty_num == 4)
                    continue;
                else if (empty_num == 3)
                {
                    if (blocknumber[i, 0] == BLANK_STATE)
                    {
                        int index = FindWhichBlockNotEmpty(i, 0);
                        MoveAnimation(new Point(i, index), new Point(i, 0), 2, true);
                    }
                }
                else if(empty_num==2)
                {
                    Point p = FindWhichTwoBlockEmpty(i, 0);
                    List<int> nums = new List<int>() { 0, 1, 2, 3 };
                    nums.Remove(p.X);
                    nums.Remove(p.Y);
                    int a = nums[0];
                    int b = nums[1];
                    if(blocknumber[i,a]==blocknumber[i,b])
                    {
                        MoveAnimation(new Point(i, a), new Point(i, 0), 2, true);
                        MoveAnimation(new Point(i, b), new Point(i, 0), 2, false);
                    }
                    else
                    {
                        MoveAnimation(new Point(i, a), new Point(i, 0), 2, true);
                        MoveAnimation(new Point(i, b), new Point(i, 1), 2, true);
                    }
                }
                else if(empty_num==1)
                {
                    int index = FindWhichBlockEmpty(i, 0);
                    List<int> nums = new List<int>() { 0, 1, 2, 3 };
                    nums.Remove(index);
                    int a = nums[0];
                    int b = nums[1];
                    int c = nums[2];
                    if(blocknumber[i,a]==blocknumber[i,b])
                    {
                        MoveAnimation(new Point(i, a), new Point(i, 0), 2, true);
                        MoveAnimation(new Point(i, b), new Point(i, 0), 2, false);
                        MoveAnimation(new Point(i, c), new Point(i, 1), 2, true);
                    }
                    else
                    {
                        if(blocknumber[i,b]==blocknumber[i,c])
                        {
                            MoveAnimation(new Point(i, a), new Point(i, 0), 2, true);
                            MoveAnimation(new Point(i, b), new Point(i, 1), 2, true);
                            MoveAnimation(new Point(i, c), new Point(i, 1), 2, false);
                        }
                        else
                        {
                            MoveAnimation(new Point(i, a), new Point(i, 0), 2, true);
                            MoveAnimation(new Point(i, b), new Point(i, 1), 2, true);
                            MoveAnimation(new Point(i, c), new Point(i, 2), 2, true);
                        }
                    }
                }
                else
                {
                    if(blocknumber[i,0]==blocknumber[i,1])
                    {
                        if(blocknumber[i,2]==blocknumber[i,3])
                        {
                            MoveAnimation(new Point(i, 1), new Point(i, 0), 2, false);
                            MoveAnimation(new Point(i, 2), new Point(i, 1), 2, true);
                            MoveAnimation(new Point(i, 3), new Point(i, 1), 2, false);

                        }
                        else
                        {
                            MoveAnimation(new Point(i, 1), new Point(i, 0), 2, false);
                            MoveAnimation(new Point(i, 2), new Point(i, 1), 2, true);
                            MoveAnimation(new Point(i, 3), new Point(i, 2), 2, true);
                        }
                    }
                    else
                    {
                        if (blocknumber[i, 1] == blocknumber[i, 2])
                        {
                            MoveAnimation(new Point(i, 2), new Point(i, 1), 2, false);
                            MoveAnimation(new Point(i, 3), new Point(i, 2), 2, true);
                        }
                        else
                        {
                            if(blocknumber[i,2]==blocknumber[i,3])
                            {
                                MoveAnimation(new Point(i, 3), new Point(i, 2), 2, false);
                            }
                        }
                    }
                }
            }
        }


        /// <summary>
        /// 向右运动
        /// </summary>
        private void Key_Right()
        {
            int empty_num = 0;
            for (int i = 0; i < 4; i++)
            {
                empty_num = CountEmptyNum(i, 0);
                if (empty_num == 4)
                    continue;
                else if (empty_num == 3)
                {
                    if (blocknumber[i, 3] == BLANK_STATE)
                    {
                        int index = FindWhichBlockNotEmpty(i, 0);
                        MoveAnimation(new Point(i, index), new Point(i, 3), 3, true);
                    }
                }
                else if (empty_num == 2)
                {
                    Point p = FindWhichTwoBlockEmpty(i, 0);
                    List<int> nums = new List<int>() { 0, 1, 2, 3 };
                    nums.Remove(p.X);
                    nums.Remove(p.Y);
                    int a = nums[0];
                    int b = nums[1];
                    if (blocknumber[i, a] == blocknumber[i, b])
                    {
                        MoveAnimation(new Point(i, b), new Point(i, 3), 3, true);
                        MoveAnimation(new Point(i, a), new Point(i, 3), 3, false);
                    }
                    else
                    {
                        MoveAnimation(new Point(i, b), new Point(i, 3), 3, true);
                        MoveAnimation(new Point(i, a), new Point(i, 2), 3, true);
                    }
                }
                else if (empty_num == 1)
                {
                    int index = FindWhichBlockEmpty(i, 0);
                    List<int> nums = new List<int>() { 0, 1, 2, 3 };
                    nums.Remove(index);
                    int a = nums[0];
                    int b = nums[1];
                    int c = nums[2];
                    if (blocknumber[i, b] == blocknumber[i, c])
                    {
                        MoveAnimation(new Point(i, c), new Point(i, 3), 3, true);
                        MoveAnimation(new Point(i, b), new Point(i, 3), 3, false);
                        MoveAnimation(new Point(i, a), new Point(i, 2), 3, true);
                    }
                    else
                    {
                        if (blocknumber[i, a] == blocknumber[i, b])
                        {
                            MoveAnimation(new Point(i, c), new Point(i, 3), 3, true);
                            MoveAnimation(new Point(i, b), new Point(i, 2), 3, true);
                            MoveAnimation(new Point(i, a), new Point(i, 2), 3, false);
                        }
                        else
                        {
                            MoveAnimation(new Point(i, c), new Point(i, 3), 3, true);
                            MoveAnimation(new Point(i, b), new Point(i, 2), 3, true);
                            MoveAnimation(new Point(i, a), new Point(i, 1), 3, true);
                        }
                    }
                }
                else
                {
                    if (blocknumber[i, 2] == blocknumber[i, 3])
                    {
                        if (blocknumber[i, 0] == blocknumber[i, 1])
                        {
                            MoveAnimation(new Point(i, 2), new Point(i, 3), 3, false);
                            MoveAnimation(new Point(i, 1), new Point(i, 2), 3, true);
                            MoveAnimation(new Point(i, 0), new Point(i, 2), 3, false);

                        }
                        else
                        {
                            MoveAnimation(new Point(i, 2), new Point(i, 3), 3, false);
                            MoveAnimation(new Point(i, 1), new Point(i, 2), 3, true);
                            MoveAnimation(new Point(i, 0), new Point(i, 1), 3, true);
                        }
                    }
                    else
                    {
                        if (blocknumber[i, 1] == blocknumber[i, 2])
                        {
                            MoveAnimation(new Point(i, 1), new Point(i, 2), 3, false);
                            MoveAnimation(new Point(i, 0), new Point(i, 1), 3, true);
                        }
                        else
                        {
                            if (blocknumber[i, 0] == blocknumber[i, 1])
                            {
                                MoveAnimation(new Point(i, 0), new Point(i, 1), 3, false);
                            }
                        }
                    }
                }
            }
        }

        #endregion


        /// <summary>
        /// 计算某一行或某一列的空的空格数
        /// </summary>
        /// <param name="n">行或列的标识</param>
        /// <param name="dir">0表示行的空的空格数，1表示列的空的空格数</param>
        /// <returns></returns>
        private int CountEmptyNum(int n,int dir)
        {
            int empty_num=0;
            if(dir==0)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (blocknumber[n, i] == BLANK_STATE)
                        empty_num++;
                }
            }
            else
            {
                for (int i = 0; i < 4; i++)
                {
                    if (blocknumber[i, n] == BLANK_STATE)
                        empty_num++;
                }
            }
            return empty_num;
        }

        

        /// <summary>
        /// 找到某一行或某一列那个方格是非空的
        /// </summary>
        /// <param name="n">行或列的标识</param>
        /// <param name="dir">0表示行,1表示列</param>
        /// <returns></returns>
        private int FindWhichBlockNotEmpty(int n,int dir)
        {
            int index = 0;
            if(dir==0)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (blocknumber[n, i] != BLANK_STATE)
                    {
                        index = i;
                        break;
                    }
                }
            }
            else
            {
                for (int i = 0; i < 4; i++)
                {
                    if (blocknumber[i, n] != BLANK_STATE)
                    {
                        index = i;
                        break;
                    }
                }
            }
            return index;
        }


        /// <summary>
        /// 找到某一行或某一列两个为空的方块，one为小的数，two为大的数
        /// </summary>
        /// <param name="n"></param>
        /// <param name="dir">0表示行,1表示列</param>
        /// <returns></returns>
        private Point FindWhichTwoBlockEmpty(int n,int dir)
        {
            int one = 0;//第一个方块
            int two = 0;//第二个方块
            int num = 0;
            if(dir==0)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (blocknumber[n, i] == BLANK_STATE)
                    {
                        if (num == 0)
                        {
                            one = i;
                            num++;
                        }
                        else if (num == 1)
                        {
                            two = i;
                            break;
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < 4; i++)
                {
                    if(blocknumber[i,n]==BLANK_STATE)
                    {
                        if (num == 0)
                        {
                            one = i;
                            num++;
                        }
                        else if (num == 1)
                        {
                            two = i;
                            break;
                        }
                    }
                }
            }
            return new Point(one, two);
        }


        /// <summary>
        /// 找到某一行或某一列中为空的一个方块
        /// </summary>
        /// <param name="n"></param>
        /// <param name="dir">0表示行,1表示列</param>
        /// <returns></returns>
        private int FindWhichBlockEmpty(int n,int dir)
        {
            int index = 0;
            if(dir==0)
            {
                for (int i = 0; i < 4; i++)
                {
                    if(blocknumber[n,i]==BLANK_STATE)
                    {
                        index = i;
                        break;
                    }
                }
            }
            else
            {
                for (int i = 0; i < 4; i++)
                {
                    if(blocknumber[i,n]==BLANK_STATE)
                    {
                        index = i;
                        break;
                    }
                }
            }
            return index;
        }


        /// <summary>
        /// 利用随机数产生内部标识以及对应的图片
        /// </summary>
        private void CreateImage()
        {
            int sum = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4;j++ )
                {
                    if (blocknumber[i, j] == BLANK_STATE)
                    {
                        blankblock[sum] = new Point(i, j);
                        sum++;
                    }
                }
            }
            Random r = new Random();
            int index = r.Next(0, sum);
            int number=r.Next(0,2);
            if(number==0)
            {
                blocknumber[blankblock[index].X, blankblock[index].Y] = 2;
                pboxes[blankblock[index].X, blankblock[index].Y].Image = MatchImage(2);
            }
            else if(number==1)
            {
                blocknumber[blankblock[index].X, blankblock[index].Y] = 4;
                pboxes[blankblock[index].X, blankblock[index].Y].Image = MatchImage(4);
            }
            pboxes[blankblock[index].X, blankblock[index].Y].Refresh();
        }


        /// <summary>
        /// 判断游戏结果
        /// </summary>
        private void JudegResult()
        {
            bool Iswin = false;  //判断游戏是否玩出
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (blocknumber[i,j] == 2048)
                    {
                        Iswin = true;
                    }
                }
            }
            if(Iswin)
            {
                MessageBox.Show("You win!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Clear();
                return;//直接返回
            }
            IsGameEnd();
        }

        private void IsGameEnd()
        {
            bool requirement_one = true; //判断游戏是否结束,游戏结束需要两个条件
            bool requirement_two = true;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (blocknumber[i, j] == BLANK_STATE)
                    {
                        requirement_one = false;
                    }
                }
            }
            if (requirement_one == true)
            {
                for (int i = 0; i < 4; i++) //左右方向的判断
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (blocknumber[i,j] == blocknumber[i,j + 1])
                        {
                            requirement_two = false;
                        }
                    }
                }
                for (int j = 0; j < 4; j++)//上下方向的判断
                {
                    for (int i = 0; i < 3; i++)
                    {
                        if (blocknumber[i,j] == blocknumber[i+1,j])
                        {
                            requirement_two = false;
                        }
                    }
                }
                if(requirement_two)
                {
                    GameOver frm = new GameOver();
                    Thread.Sleep(1000);
                    frm.ShowDialog();
                    if(frm.DialogResult==DialogResult.OK)
                    {
                        Clear();
                        CreateImage();
                        CreateImage();
                    }
                }
            }

        }

        private void Clear()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    blocknumber[i,j] = BLANK_STATE;
                    pboxes[i, j].Image = null;
                }
            }
        }

        
        /// <summary>
        /// 监视动画是否完成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmrWatch_Tick(object sender, EventArgs e)
        {
            bool isfinished = true;
            for (int i = 0; i < 16; i++)
            {
                if(tmrs[i]!=null)
                {
                    isfinished = false;
                }
            }
            if(isfinished)
            {
                CreateImage();
                KeyboardEnable = true;
                tmrWatch.Stop();
                JudegResult();
            }
        }

        private void 结束游戏ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void 关于游戏ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("上上下下左左右右BABA，Show Me The Money，Kirov Reporting!");
        }


        private void 新游戏ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StartNewGame();
        }

        private void StartNewGame()
        {
            Clear();
            KeyboardEnable = true;
            CreateImage();
            CreateImage();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            StartNewGame();
        }

        public string GetPluginVersion()
        {
            return "0.1";
        }

        public string GetPluginName()
        {
            return "2048";
        }
        public Image GetPluginImage()
        {
            return _2048.Properties.Resources._2048_LOGO.ToBitmap();
        }

        public string GetPluginDescription()
        {
            return "2048小游戏，按上下左右消除相同的数字，用作插件系统的教学示例。";
        }

        public DialogResult ShowFormDialog()
        {
            return this.ShowDialog();
        }
    }
}
