using C2.Controls;
using C2.Core;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C2.Business.CastleBravo.WebShellTool.SettingsDialog
{
    partial class WebConfigScan : StandardDialog
    {
        private string Payload { get; set; } = string.Empty;
        public WebConfigScan()
        {
            InitializeComponent();
        }

        protected override bool OnOKButtonClick()
        {
            //判断必填是否有值
            if (filePathTextBox.Text.IsNullOrEmpty())
            {
                HelpUtil.ShowMessageBox("【配置文件路径】 不能为空。");
                return false;
            }
           
            Payload = string.Format(ClientSetting.InfoPayloadDict[InfoType.MysqlConfigField],
                                          "{0}",
                                          ST.EncodeBase64(filePathTextBox.Text.Trim()));
            return base.OnOKButtonClick();
        }
        public new string ShowDialog()
        {
            return base.ShowDialog() == System.Windows.Forms.DialogResult.OK ? Payload : string.Empty;
        }

    }
}
