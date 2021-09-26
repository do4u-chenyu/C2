using C2.Controls;
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

namespace C2.Business.CastleBravo.WebShellTool
{
    partial class AddWebShell : StandardDialog
    {
        public WebShellTaskInfo WebShellTask;

        private string taskId;

        public AddWebShell()
        {
            InitializeComponent();
            this.typeCombo.SelectedIndex = 0;
        }

        public AddWebShell(string id) : this()
        {
            taskId = id;
        }

        public AddWebShell(WebShellTaskInfo webShellTaskInfo) : this()
        {
            WebShellTask = webShellTaskInfo;
            UpdateSetting();
        }

        private void UpdateSetting()
        {
            taskId = WebShellTask.TaskID;
            this.NameTextBox.Text = WebShellTask.TaskName;
            this.urlTextBox.Text = WebShellTask.TaskUrl;
            this.pwdTextBox.Text = WebShellTask.TaskPwd;
            this.remarkTextBox.Text = WebShellTask.TaskRemark;
            this.advancedTextBox.Text = WebShellTask.TaskAdvanced;
        }

        protected override bool OnOKButtonClick()
        {
            //TODO 判断必填是否有值
            WebShellTask = new WebShellTaskInfo(taskId, 
                                                this.NameTextBox.Text, 
                                                urlTextBox.Text, 
                                                pwdTextBox.Text,
                                                (WebShellTaskType)Enum.Parse(typeof(WebShellTaskType), typeCombo.Text),
                                                remarkTextBox.Text,
                                                advancedTextBox.Text);

            return base.OnOKButtonClick();
        }
    }
}
