using C2.Dialogs.IAOLab;
using C2.Globalization;
using C2.IAOLab.Plugins;
using C2.Utils;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
namespace C2.Controls.Left
{
    public partial class IAOButton : UserControl
    {
        private WifiLocation baseForm0;
        private ApkTool baseForm1;
        private coordinateConversion baseForm2;
        private DialogResult dllDialogResult;
        private Form dllForm;
        private string description;
        public string ControlName { set => this.txtButton.Text = value; }
        public string Description { set=> this.description = value; }
        public Image LeftPicture { set => this.leftPictureBox.Image = value; }
        public Form DLLForm { set => this.dllForm = value; }

        public IAOButton()
  
        {
            InitializeComponent();
            this.ContextMenuStrip = contextMenuStrip1;
            toolTip1.SetToolTip(this.rightPictureBox, this.description);
        }
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
                    toolTip1.SetToolTip(this.rightPictureBox, HelpUtil.ApkToolFormHelpInfo);
                    
                    ApkToolForm();
                    break;
                case "BaseStation":
                    this.txtButton.Text = Lang._("BaseStation");
                    this.leftPictureBox.Image = global::C2.Properties.Resources.BaseStation;
                    toolTip1.SetToolTip(this.rightPictureBox, HelpUtil.BaseStationFormHelpInfo);                 
                    BaseStationForm();
                    break;
                case "Wifi":
                    this.txtButton.Text = Lang._("Wifi");
                    this.leftPictureBox.Image = global::C2.Properties.Resources.Wifi;
                    toolTip1.SetToolTip(this.rightPictureBox, HelpUtil.WifiLocationFormHelpInfo);
                    baseForm0 = new WifiLocation();
                    break;
                case "Card":
                    this.txtButton.Text = Lang._("Card");
                    this.leftPictureBox.Image = global::C2.Properties.Resources.Card;
                    toolTip1.SetToolTip(this.rightPictureBox, HelpUtil.BankToolFormHelpInfo);
                    BankToolForm();
                    break;
                case "Tude":
                    this.txtButton.Text = Lang._("Tude");
                    this.leftPictureBox.Image = global::C2.Properties.Resources.Tude;
                    toolTip1.SetToolTip(this.rightPictureBox, HelpUtil.GPSTransformFormHelpInfo);
                    GPSTransformForm();
                    break;
                case "Ip":
                    this.txtButton.Text = Lang._("Ip");
                    this.leftPictureBox.Image = global::C2.Properties.Resources.Ip;
                    toolTip1.SetToolTip(this.rightPictureBox, HelpUtil.TimeAndIPTransformFormHelpInfo);
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
                Text = "银行卡信息查询",
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
            
            OpenToolForm();
        }

        private void TxtButton_MouseDown(object sender, MouseEventArgs e)
        {
            
            if (e.Button == MouseButtons.Left && e.Clicks == 2)
                OpenToolForm();
        }
        private void OpenToolForm()
        {
            if (dllForm != null)
            {
                dllForm.ShowDialog();
                return;
            }
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
