using System;
using C2.Controls;
using C2.Utils;
using C2.Core;

namespace C2.Business.CastleBravo.WebShellTool
{
    partial class InfoCollectionSet : StandardDialog
    {

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
                this.addrTextBox.Text = setting.Addr;

            }

        }
        protected override bool OnOKButtonClick()
        {
            //TODO 判断必填是否有值
            if (addrTextBox.Text.IsNullOrEmpty())
            {
                HelpUtil.ShowMessageBox("【地址】不能为空。");
                return false;
            }
            return base.OnOKButtonClick();
        }
        public new InfoCollectionSetting ShowDialog()
        {
            return base.ShowDialog() == System.Windows.Forms.DialogResult.OK && this.useSetComboBox.SelectedIndex == 1 ?
                new InfoCollectionSetting(
                    useSetComboBox.SelectedIndex == 1,   // 0 否 1是
                    addrTextBox.Text.Trim()) : InfoCollectionSetting.Empty;
        }


    }
    public class InfoCollectionSetting
    {
        public static InfoCollectionSetting Empty = new InfoCollectionSetting();

        public bool Enable;
        public string Addr;

        public InfoCollectionSetting()
        { }
        public InfoCollectionSetting(bool enable, string addr)
        {
            Enable = enable;
            Addr = addr;

        }
    }
}
