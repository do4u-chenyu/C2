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
            //TODO 判断必填是否有值
            if (filePathTextBox.Text.IsNullOrEmpty() || scanFieldTextBox.Text.IsNullOrEmpty())
            {
                HelpUtil.ShowMessageBox("【IP】 和 【端口】 不能为空。");
                return false;
            }
            return base.OnOKButtonClick();
        }

        
    }
}
