using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using C2.Dialogs.WebsiteFeatureDetection;
using C2.Utils;
using System.IO;
using C2.Core;

namespace C2.Controls.C1.Left
{
    public partial class WebsiteFeatureDetectionControl : BaseLeftInnerPanel
    {

        public WebsiteFeatureDetectionControl()
        {
            InitializeComponent();
        }

        public override void AddTask()
        {
            var dialog = new AddWFDTask();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                AddInnerButton(new WebsiteFeatureDetectionButton(dialog.TaskName, dialog.FilePath));
            }
        }




        private class WebsiteFeatureDetectionButton : BaseLeftInnerButton
        {
            public string FilePath;


            public WebsiteFeatureDetectionButton()
            {
                InitButtonMenu();
                InitButtonType();

            }
            public WebsiteFeatureDetectionButton(string name, string filePath) : this()
            {
                this.ButtonText = name;
                this.FilePath = filePath;
                this.toolTip.SetToolTip(this.rightPictureBox, FilePath);
            }

            private void InitButtonType()
            {
                this.leftPictureBox.Image = global::C2.Properties.Resources.数据;
                this.rightPictureBox.Image = global::C2.Properties.Resources.提示;
            }

            private void InitButtonMenu()
            {
                ToolStripMenuItem RemoveToolStripMenuItem = new ToolStripMenuItem();
                RemoveToolStripMenuItem.Name = "ReviewToolStripMenuItem";
                RemoveToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
                RemoveToolStripMenuItem.Text = "删除任务";
                RemoveToolStripMenuItem.ToolTipText = "从面板中移除任务,同时删除本地结果文件";
                RemoveToolStripMenuItem.Click += new System.EventHandler(RemoveToolStripMenuItem_Click);

                ToolStripMenuItem OpenDatasourceToolStripMenuItem = new ToolStripMenuItem();
                OpenDatasourceToolStripMenuItem.Name = "OpenDatasourceToolStripMenuItem";
                OpenDatasourceToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
                OpenDatasourceToolStripMenuItem.Text = "打开源文件";
                OpenDatasourceToolStripMenuItem.ToolTipText = "从本地文本编辑器中打开文件";
                OpenDatasourceToolStripMenuItem.Click += new System.EventHandler(OpenDatasourceToolStripMenuItem_Click);

                ToolStripMenuItem ResultToolStripMenuItem = new ToolStripMenuItem();
                ResultToolStripMenuItem.Name = "ResultToolStripMenuItem";
                ResultToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
                ResultToolStripMenuItem.Text = "查看结果";
                ResultToolStripMenuItem.ToolTipText = "查看任务返回结果";
                ResultToolStripMenuItem.Click += new System.EventHandler(ResultToolStripMenuItem_Click);

                this.contextMenuStrip.Items.AddRange(new ToolStripItem[] {
                    OpenDatasourceToolStripMenuItem,
                    ResultToolStripMenuItem,
                    RemoveToolStripMenuItem
                 });

            }

            private void RemoveToolStripMenuItem_Click(object sender, EventArgs e)
            {
                DialogResult rs = MessageBox.Show(
                    String.Format("删除任务 {0}, 继续删除请点击 \"确定\"", ButtonText),
                    "删除",MessageBoxButtons.OKCancel,MessageBoxIcon.Information);

                if (rs != DialogResult.OK)
                    return;

                //TODO 删除结果文件
                //从左侧面板中移除
                Global.GetWebsiteFeatureDetectionControl().RemoveButton(this);
            }
            private void OpenDatasourceToolStripMenuItem_Click(object sender, EventArgs e)
            {
                ProcessUtil.ProcessOpen(this.FilePath);
            }
            private void ResultToolStripMenuItem_Click(object sender, EventArgs e)
            {

            }

        }


    }
}
