using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using C2.Controls;
using C2.Utils;
using C2.Core;

namespace C2.Business.CastleBravo.WebShellTool
{
    partial class InfoCollectionSet : StandardDialog
    {
        public InfoCollectionSet()
        {
            InitializeComponent();
        }

        public InfoCollectionSet(InfoCollectionSetting setting)
        {
            InitializeComponent();
            InitializeWebShell(setting);
        }

        private void InitializeWebShell(InfoCollectionSetting setting)
        {
            if (setting == InfoCollectionSetting.Empty)
            {
                this.useSetComboBox.SelectedIndex = 0;
            }
            else
            {

                this.useSetComboBox.SelectedIndex = setting.Enable ? 1 : 0;
                this.remoteAddrTextBox.Text = setting.RemoteAddr;
                this.localAddrTextBox.Text = setting.LocalAddr;
                this.remoteAddr.Checked = setting.AddrType;// TRUE表示选中远程地址，FALSE表是选中本地地址
                this.localAddr.Checked = !setting.AddrType;

            }

        }
        protected override bool OnOKButtonClick()
        {
            //TODO 判断必填是否有值
            if (localAddrTextBox.Text.IsNullOrEmpty() || remoteAddrTextBox.Text.IsNullOrEmpty())
            {
                HelpUtil.ShowMessageBox("脚本和字典所在的【远程地址】或【本地地址】不能为空。");
                return false;
            }
            return base.OnOKButtonClick();
        }
        public new InfoCollectionSetting ShowDialog()
        {
            return base.ShowDialog() == System.Windows.Forms.DialogResult.OK ?
                new InfoCollectionSetting(
                    useSetComboBox.SelectedIndex == 1,   // 0 否 1是
                    remoteAddrTextBox.Text.Trim(),
                    localAddrTextBox.Text.Trim(),
                    remoteAddr.Checked) : InfoCollectionSetting.Empty;
        }

        private void PortTextBox_TextChanged(object sender, EventArgs e)
        {

        }
       
    }
    public class InfoCollectionSetting
    {
        public static InfoCollectionSetting Empty = new InfoCollectionSetting();

        public bool Enable;
        public string RemoteAddr;
        public string LocalAddr;
        public bool AddrType;

        public InfoCollectionSetting()
        { }
        public InfoCollectionSetting(bool enable, string remoteAddr, string localAddr,bool addrType)
        {
            Enable = enable;
            RemoteAddr = remoteAddr;
            LocalAddr = localAddr;
            AddrType = addrType;

        }
    }
}
