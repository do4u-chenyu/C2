//*******************************************************************
//
//      文件名（File Name）：          frmGameOver.cs
//
//      功能描述（Description）：      2048游戏结束界面    
//
//      数据表（Tables）：             nothing
//                            
//      作者（Author）：               MH
//
//      日期（Create Date）：          2014.6.5
//
//
//*******************************************************************
using System;
using System.Windows.Forms;

namespace _2048
{
    public partial class GameOver : Form
    {
        public GameOver()
        {
            InitializeComponent();
        }

        private void btnAgain_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

    }
}
