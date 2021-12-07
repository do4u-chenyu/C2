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
        public WebConfigScan()
        {
            InitializeComponent();
        }

        protected override bool OnOKButtonClick()
        {
            //判断必填是否有值
            if (filePathTextBox.Text.IsNullOrEmpty() || scanFieldTextBox.Text.IsNullOrEmpty())
            {
                HelpUtil.ShowMessageBox("【配置文件路径】 和 【扫描字段】 不能为空。");
                return false;
            }
            if (scanFieldTextBox.Text.Equals("账号字段,密码字段"))
            {
                HelpUtil.ShowMessageBox(" 【扫描字段】 不能使用默认值。");
                return false;
            }
            return base.OnOKButtonClick();
        }

        
    }
}
