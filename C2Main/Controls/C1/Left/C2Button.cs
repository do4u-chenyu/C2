﻿using C2.Core;
using C2.Dialogs;
using C2.Utils;
using System;
using System.IO;
using System.Windows.Forms;

namespace C2.Controls.Left
{
    public partial class C2Button : UserControl
    {
        protected string desc;

        public C2Button(string c2Title)
        {
            InitializeComponent();
            InitializeOther(c2Title);
        }

        private void InitializeOther(string modelTitle)
        {
            this.textButton.Text = modelTitle;
            this.toolTip1.SetToolTip(this.textButton, ModelTitle);
            FullFilePath = Path.Combine(Global.BusinessViewPath, ModelTitle, ModelTitle + ".bmd");
            this.desc = "分析笔记";
        }

        public string ModelTitle => this.textButton.Text;

        public string FullFilePath { get; set; }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Global.GetMainForm().OpenDocument(FullFilePath);
        }

        private void ExplorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileUtil.ExploreDirectory(FullFilePath);
        }

        private void CopyFilePathToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileUtil.TryClipboardSetText(FullFilePath);
        }

 
        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 容错处理, 标题栏中文档未关闭时,不能删除
            if (Global.GetTaskBar().ContainModel(ModelTitle))
                return;
            // 删除前用对话框确认
            DialogResult rs = MessageBox.Show(String.Format("删除{1}: {0}, 继续删除请点击 \"确定\"", ModelTitle, desc),
                    "删除 " + ModelTitle,
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Information);

            if (rs != DialogResult.OK)
                return;

            FileUtil.DeleteDirectory(Path.GetDirectoryName(FullFilePath));
            Global.GetMindMapControl().RemoveButton(this);
        }


        private void TextButton_MouseDown(object sender, MouseEventArgs e)
        {
            // 鼠标左键双击触发
            if (e.Button != MouseButtons.Left || e.Clicks != 2)
                return;
            // 双击打开对应模型
            Global.GetMainForm().OpenDocument(FullFilePath);
        }


        private void ExportModelButton_Click(object sender, EventArgs e)
        {
            // 模型文档不存在返回
            if (!File.Exists(this.FullFilePath))
            {
                HelpUtil.ShowMessageBox("文档不存在，可能已被删除");
                return;
            }

            ZipDialog zipDialog = new ExportZipDialog(ModelTitle);
            if (zipDialog.ShowDialog() == DialogResult.OK)
            {
                string exportFullPath = zipDialog.ModelPath;
                string password = zipDialog.Password;
                if (Business.Model.ExportModel.GetInstance().ExportC2Model(this.FullFilePath, exportFullPath, password))
                    HelpUtil.ShowMessageBox("导出成功,存储路径：" + exportFullPath);
                FileUtil.DeleteDirectory(Path.Combine(Global.TempDirectory));
            }
        }

        protected virtual void ContextMenuStrip_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            bool val = !Global.GetMainForm().OpenedMindMapDocuments().Contains(ModelTitle);
            this.OpenToolStripMenuItem.Enabled = val;
            this.DeleteToolStripMenuItem.Enabled = val;
        }

        private void MindMapModelButton_Load(object sender, EventArgs e)
        {
            // 模型全路径浮动提示信息
            String helpInfo = FullFilePath;
            this.toolTip1.SetToolTip(this.rightPictureBox, helpInfo);
        }
    }
}