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
    public delegate void OpenToolFormDelegate();
    public partial class IAOButton : UserControl
    {

        private Form baseForm;
        private OpenToolFormDelegate openToolForm;

        public OpenToolFormDelegate ShowDialogDelegate { get => openToolForm; set => openToolForm = value; }

        public void SetToolTip(string desc)
        {
            toolTip1.SetToolTip(this.rightPictureBox, desc);
        }

        public void SetIcon(Image icon)
        {
            this.leftPictureBox.Image = icon;
        }

        public IAOButton(string ffp)
        {
            InitializeComponent();
            this.ContextMenuStrip = contextMenuStrip1;
            this.txtButton.Text = Lang._(ffp);
            switch (ffp)
            {
                case "APK":
                    this.leftPictureBox.Image = global::C2.Properties.Resources.Apk;
                    toolTip1.SetToolTip(this.rightPictureBox, HelpUtil.ApkToolFormHelpInfo);
                    ApkToolForm();
                    break;
                case "BaseStation":
                    this.leftPictureBox.Image = global::C2.Properties.Resources.BaseStation;
                    toolTip1.SetToolTip(this.rightPictureBox, HelpUtil.BaseStationFormHelpInfo);
                    BaseStationForm(ffp);
                    break;
                case "Wifi":
                    this.leftPictureBox.Image = global::C2.Properties.Resources.Wifi;
                    toolTip1.SetToolTip(this.rightPictureBox, HelpUtil.WifiLocationFormHelpInfo);
                    baseForm = new WifiLocation() { FormType = ffp };
                    break;
                case "Card":
                    this.leftPictureBox.Image = global::C2.Properties.Resources.Card;
                    toolTip1.SetToolTip(this.rightPictureBox, HelpUtil.BankToolFormHelpInfo);
                    BankToolForm(ffp);
                    break;
                case "Tude":
                    this.leftPictureBox.Image = global::C2.Properties.Resources.Tude;
                    toolTip1.SetToolTip(this.rightPictureBox, HelpUtil.GPSTransformFormHelpInfo);
                    GPSTransformForm(ffp);
                    break;
                case "Ip":
                    this.leftPictureBox.Image = global::C2.Properties.Resources.Ip;
                    toolTip1.SetToolTip(this.rightPictureBox, HelpUtil.TimeAndIPTransformFormHelpInfo);
                    TimeAndIPTransformForm(ffp);
                    break;
                case "BigAPK":
                    this.leftPictureBox.Image = global::C2.Properties.Resources.BigAPK;
                    toolTip1.SetToolTip(this.rightPictureBox, HelpUtil.BigAPKFormHelpInfo);
                    BigAPKForm();
                    break;
            }
            //toolTip1.SetToolTip(this.txtButton, this.txtButton.Text);
        }
        #region 定义6种弹窗
        private void ApkToolForm()
        {
            baseForm = new ApkTool();
        }
        private void BaseStationForm(string formType)
        {
            baseForm = new WifiLocation()
            {
                Text = "基站查询",
                InputLable = "请在下方输入基站号码",
                Tip = HelpUtil.BaseStationHelpInfo,
                FormType = formType
            };

        }
        private void BankToolForm(string formType)
        {
            baseForm = new WifiLocation()
            {
                Text = "银行卡信息查询",
                InputLable = "请在下方输入银行卡",
                Tip = HelpUtil.BankToolHelpInfo,
                FormType = formType
            };

        }
        private void GPSTransformForm(string formType)
        {
            baseForm = new CoordinateConversion()
            {
                Tab0Tip = HelpUtil.GPSTransformHelpInfo,
                Tib1Tip = HelpUtil.GPSDistanceHelpInfo,
                FormType = formType
            };

        }
        private void TimeAndIPTransformForm(string formType)
        {
            baseForm = new CoordinateConversion()
            {
                Tab0Tip = HelpUtil.IPTransformHelpInfo,
                Tib1Tip = HelpUtil.TimeTransformHelpInfo,
                FormType = formType
            };
            (baseForm as CoordinateConversion).ReLayoutForm();

        }

        private void BigAPKForm()
        {
            baseForm = new BigAPKForm();
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
            if (openToolForm != null)
            {
                openToolForm();
                return;
            }
            if (baseForm != null)
                baseForm.ShowDialog();


        }


    }
}
