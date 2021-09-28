using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace C2.Business.CastleBravo.WebShellTool
{
    public partial class WebShellDetails : Form
    {
        private WebShellTaskInfo webShellTaskInfo;
        private WebShell webShell;

        private string currentShowPath;

        public WebShellDetails()
        {
            InitializeComponent();
            currentShowPath = string.Empty;
        }

        public WebShellDetails(WebShellTaskInfo taskInfo) : this()
        {
            webShellTaskInfo = taskInfo;
            webShell = new WebShell(taskInfo.TaskUrl, taskInfo.TaskPwd);
        }

        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.Text == "文件管理")
                UpdateFileManager(webShell.CurrentPathBrowse());
        }

        private void FilePathTb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                UpdateFileManager(webShell.PathBrowse(this.filePathTb.Text));
            }
        }

        private void FileManagerListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.fileManagerListView.SelectedItems.Count == 0)
                return;

            WSFile selectedFile = (WSFile)this.fileManagerListView.SelectedItems[0].Tag;
            if (selectedFile.Type != WebShellFileType.Directory)
                return;

            //TODO linux和windows拼接不一样，用path.combine拼不了linux路径？
            UpdateFileManager(webShell.PathBrowse(currentShowPath + "/" + selectedFile.FileName)); 
        }

        private void UpdateFileManager(Tuple<string, List<WSFile>> pathFiles)
        {
            string path = pathFiles.Item1;
            List<WSFile> files = pathFiles.Item2;

            //更新textbox显示路径
            currentShowPath = this.filePathTb.Text = path;

            //更新右侧listview
            this.fileManagerListView.Items.Clear();
            foreach (WSFile file in files)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Tag = file;
                lvi.Text = file.FileName;
                lvi.ImageIndex = file.Type == WebShellFileType.Directory ? 0 : 1;
                lvi.SubItems.Add(file.CreateTime);
                lvi.SubItems.Add(file.FileSize);
                lvi.SubItems.Add(file.LastMod);

                this.fileManagerListView.Items.Add(lvi);
            }

            //更新左侧treeview
            /*
             * 更新逻辑【先清空吧，后面再调整】
             * 1、首先肯定能拿到path
             * 2、对path进行解析，用 / 切分，展示为树的一条完整分支
             * 3、该路径下的所有文件夹，添加到子孩子
             */
            //this.treeView1.Nodes.Clear();

            TreeNode[] nodes = this.treeView1.Nodes.Find("/", false);
            TreeNode root = new TreeNode();
            TreeNode tmpNode;
            if (nodes.Length == 0)
            {
                root.Name = "/";
                root.Text = "/";
                root.Tag = "/";
                root.ImageIndex = 4;
                root.SelectedImageIndex = 4;
                this.treeView1.Nodes.Add(root);
                tmpNode = root;
            }
            else
                tmpNode = nodes[0];

            foreach (string dir in path.Trim('/').Split('/'))
            {
                if (string.IsNullOrEmpty(dir))
                    break;

                TreeNode[] nodes2 = tmpNode.Nodes.Find(dir, false);

                TreeNode dirNode = new TreeNode();
                if (nodes2.Length == 0)
                {
                    dirNode.Name = dir;
                    dirNode.Text = dir;
                    dirNode.Tag = tmpNode.Tag + dir + "/";
                    dirNode.ImageIndex = 0;
                    tmpNode.Nodes.Add(dirNode);
                    tmpNode = dirNode;
                }
                else
                    tmpNode = nodes2[0];
            }

            foreach (WSFile file in files)
            {
                if (file.Type != WebShellFileType.Directory)
                    continue;

                TreeNode[] nodes3 = tmpNode.Nodes.Find(file.FileName, false);
                TreeNode dirNode = new TreeNode();
                
                if (nodes3.Length == 0)
                {
                    dirNode.Name = file.FileName;
                    dirNode.Text = file.FileName;
                    dirNode.Tag = tmpNode.Tag +  file.FileName + "/";
                    dirNode.ImageIndex = 0;
                    tmpNode.Nodes.Add(dirNode);
                }
            }

            root.ExpandAll();

            this.messageLog.Text = string.Join("\r\n",webShell.PayloadLog);
            webShell.PayloadLog.Clear();
        }

        private void TreeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                
                UpdateFileManager(webShell.PathBrowse(e.Node.Tag.ToString() == "/" ? e.Node.Tag.ToString() : e.Node.Tag.ToString().TrimEnd('/')));
            }
        }

        //曲线救国之双击不展开
        public int m_MouseClicks = 0;

        private void TreeView1_MouseDown(object sender, MouseEventArgs e)
        {
            this.m_MouseClicks = e.Clicks;
        }

        private void TreeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (this.m_MouseClicks > 1)
            {
                //如果是鼠标双击则禁止结点展开
                e.Cancel = true;
            }
            else
            {
                //如果是鼠标单击则允许结点展开
                e.Cancel = false;
            }
        }

        private void TreeView1_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            if (this.m_MouseClicks > 1)
            {
                //如果是鼠标双击则禁止结点折叠
                e.Cancel = true;
            }
            else
            {
                //如果是鼠标单击则允许结点折叠
                e.Cancel = false;
            }
        }
    }
}
