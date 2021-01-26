﻿using C2.Dialogs.IAOLab;
using C2.Globalization;
using C2.Utils;
using System;
using System.IO;
using System.Windows.Forms;
namespace C2.Controls.Left
{
    public partial class IAOButton : UserControl
    {
        private WifiLocation baseForm0;
        private ApkTool baseForm1;
        private coordinateConversion baseForm2;
        public IAOButton(string ffp)
        {
            InitializeComponent();
            txtButton.Name = ffp;
            txtButton.Text = ffp;
          
            //this.leftPictureBox.Image = global::C2.Properties.Resources.Apk;
            this.ContextMenuStrip = contextMenuStrip1;
            switch (ffp)
            {
                case "APK":
                    this.txtButton.Text = Lang._("APK");
                    this.leftPictureBox.Image = global::C2.Properties.Resources.Apk;
                    toolTip1.SetToolTip(this.rightPictureBox, "对Apk进行解析并获取Apk的图标，安装名称，包名，入口函数名和大小");
                    
                    ApkToolForm();
                    break;
                case "BaseStation":
                    this.txtButton.Text = Lang._("BaseStation");
                    this.leftPictureBox.Image = global::C2.Properties.Resources.BaseStation;
                    toolTip1.SetToolTip(this.rightPictureBox, "根据基站号进行定位，获取基站的经纬度，覆盖范围和详细地址,需要网络");                 
                    BaseStationForm();
                    break;
                case "Wifi":
                    this.txtButton.Text = Lang._("Wifi");
                    this.leftPictureBox.Image = global::C2.Properties.Resources.Wifi;
                    toolTip1.SetToolTip(this.rightPictureBox, "根据WIFI热点的MAC进行定位，获取WIFI的经纬度，覆盖范围和详细地址,需要网络");
                    baseForm0 = new WifiLocation();
                    break;
                case "Card":
                    this.txtButton.Text = Lang._("Card");
                    this.leftPictureBox.Image = global::C2.Properties.Resources.Card;
                    toolTip1.SetToolTip(this.rightPictureBox, "根据银行卡号获取银行卡的卡种，开户行和其他信息,需要网络");
                    BankToolForm();
                    break;
                case "Tude":
                    this.txtButton.Text = Lang._("Tude");
                    this.leftPictureBox.Image = global::C2.Properties.Resources.Tude;
                    toolTip1.SetToolTip(this.rightPictureBox, "经纬度坐标系转换;计算两经纬度坐标之间的距离");
                    GPSTransformForm();
                    break;
                case "Ip":
                    this.txtButton.Text = Lang._("Ip");
                    this.leftPictureBox.Image = global::C2.Properties.Resources.Ip;
                    toolTip1.SetToolTip(this.rightPictureBox, "IP和整形IP之间的转换，绝对时间和真实时间之间的转换");
                    TimeAndIPTransformForm();
                    break;
            }
            toolTip1.SetToolTip(this.txtButton, this.txtButton.Text);
            if (baseForm0 != null)
                baseForm0.FormType = ffp;
            if (baseForm2 != null)
                baseForm2.FormType = ffp;
        }
        #region 定义6种弹窗
        private void ApkToolForm()
        {
            baseForm1 = new ApkTool();
        }
        private void BaseStationForm()
        {
            baseForm0 = new WifiLocation()
            {
                Text = "基站查询",
                InputLable = "请在下方输入基站号码",
                Tip = HelpUtil.BaseStationHelpInfo,
                
            };

        }
        private void BankToolForm()
        {
            baseForm0 = new WifiLocation()
            {
                Text = "银行卡归属地查询",
                InputLable = "请在下方输入银行卡",
                Tip = HelpUtil.BankToolHelpInfo
            };

        }
        private void GPSTransformForm()
        {
            baseForm2 = new coordinateConversion()
            {
                Tab0Tip = HelpUtil.GPSTransformHelpInfo,
                Tib1Tip = HelpUtil.GPSDistanceHelpInfo
            };
    
        }
        private void TimeAndIPTransformForm()
        {
            baseForm2 = new coordinateConversion()
            {
                Tab0Tip = HelpUtil.IPTransformHelpInfo,
                Tib1Tip = HelpUtil.TimeTransformHelpInfo
            };
            baseForm2.ReLayoutForm();
  
        }
        #endregion
        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (baseForm1 != null)
            {
                string tmpPath = Path.Combine(Path.GetTempPath(), "ApkTool");
                FileUtil.DeleteDirectory(tmpPath);
                FileUtil.CreateDirectory(tmpPath);
            }
            OpenToolForm();
        }

        private void TxtButton_MouseDown(object sender, MouseEventArgs e)
        {
            if(baseForm1 != null)
            {
                string tmpPath = Path.Combine(Path.GetTempPath(), "ApkTool");
                FileUtil.DeleteDirectory(tmpPath);
                FileUtil.CreateDirectory(tmpPath);
            }
            if (e.Button == MouseButtons.Left && e.Clicks == 2)
                OpenToolForm();
        }
        private void OpenToolForm()
        {
            if (baseForm0 != null)
            {
                baseForm0.ShowDialog();
                return;
            }
            if (baseForm1 != null)
            {
                baseForm1.ShowDialog();
                return;
            }
            if (baseForm2 != null)
                baseForm2.ShowDialog();
        }

        private void RightPictureBox_MouseHover(object sender, EventArgs e)
        {
            toolTip1.InitialDelay = 100;
            toolTip1.ReshowDelay = 500;//指针从一个控件移向另一个控件时，经过多久才会显示下一个提示框
            toolTip1.ShowAlways = true;//是否显示提示框
            //  设置伴随的对象.
            
        }
    }
}
