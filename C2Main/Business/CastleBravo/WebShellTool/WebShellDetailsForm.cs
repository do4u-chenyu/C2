using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace C2.Business.CastleBravo.WebShellTool
{
    public partial class WebShellDetailsForm : Form
    {
        private WebShellClient webShell;

        private string currentShowPath;
        private string currentCmdPath;

        public WebShellDetailsForm()
        {
            InitializeComponent();
        }

        public WebShellDetailsForm(WebShellTaskConfig info) : this()
        {
            webShell = new WebShellClient(info.Url, info.Password, info.ClientVersion);
            currentShowPath = string.Empty;
            currentCmdPath = string.Empty;
            UpdateBaseInfo(webShell.PHPInfo());
        }

        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.Text == "文件管理")
                UpdateFileManager(webShell.CurrentPathBrowse());
            if (tabControl1.SelectedTab.Text == "基础信息")
                UpdateBaseInfo(webShell.PHPInfo());
            if (tabControl1.SelectedTab.Text == "虚拟终端")
                UpdateCmd(webShell.CurrentCmdExcute());        
        }

        private void UpdateCmd(Tuple<string, string> excuteResult)
        {
            currentCmdPath = excuteResult.Item1;
            string nextExcutePath = currentCmdPath.StartsWith("/") ? string.Format("[{0}]$", currentCmdPath) : string.Format("{0}>", currentCmdPath);
            string output = excuteResult.Item2;

            this.outputTextBox.Text = this.outputTextBox.Text + "\r\n" + output + "\r\n\r\n" + nextExcutePath;
            this.outputTextBox.Focus();//获取焦点
            this.outputTextBox.Select(this.outputTextBox.TextLength, 0);//光标定位到文本最后
            this.outputTextBox.ScrollToCaret();//滚动到光标处

            this.cmdTextBox.Text = string.Empty;

            this.messageLog.Text = webShell.PayloadLog;
            webShell.Clear();
        }

        private void UpdateBaseInfo(string result)
        {

            this.baseInfoWebBrowser.DocumentText = result;

            this.messageLog.Text = webShell.PayloadLog;
            webShell.Clear();
        }

        private void FilePathTb_KeyDown(object sender, KeyEventArgs e)
        {
            //TODO 暂时不支持修改，要判断返回情况
            //if (e.KeyCode == Keys.Enter)
            //{
            //    UpdateFileManager(webShell.PathBrowse(new List<string>() { this.filePathTb.Text }));
            //}
        }

        private void FileManagerListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.fileManagerListView.SelectedItems.Count == 0)
                return;

            WSFile selectedFile = (WSFile)this.fileManagerListView.SelectedItems[0].Tag;
            if (selectedFile.Type != WebShellFileType.Directory)
                return;

            //TODO linux和windows拼接不一样，用path.combine拼不了linux路径？
            UpdateFileManager(webShell.PathBrowse(new List<string>() { currentShowPath + "/" + selectedFile.FileName })); 
        }

        private void UpdateFileManager(Tuple<string, List<WSFile>, List<string>> pathFiles)
        {
            string path = pathFiles.Item1;
            List<WSFile> files = pathFiles.Item2;
            List<string> broPaths = pathFiles.Item3;

            //更新textbox显示路径
            currentShowPath = this.filePathTb.Text = path;

            //更新右侧listview
            this.fileManagerListView.Items.Clear();
            foreach (WSFile file in files)
            {
                ListViewItem lvi = new ListViewItem
                {
                    Tag = file,
                    Text = file.FileName,
                    ImageIndex = file.Type == WebShellFileType.Directory ? 0 : 1
                };
                lvi.SubItems.Add(file.CreateTime);
                lvi.SubItems.Add(file.FileSize);
                lvi.SubItems.Add(file.LastMod);

                this.fileManagerListView.Items.Add(lvi);
            }

            //更新左侧treeview
            CreateBroNodes(broPaths);//生成兄弟节点，这里仅针对window
            CreateSelfAndChildrenNodes(path, files);//生成自己和孩子节点

            this.messageLog.Text = webShell.PayloadLog;
            webShell.Clear();
        }

        private void CreateBroNodes(List<string> broPaths)
        {
            foreach (string broPath in broPaths)
            {
                if (string.IsNullOrEmpty(broPath))
                    continue;

                string broName = broPath + ":";
                TreeNode bro = new TreeNode();
                TreeNode[] broNodes = this.treeView1.Nodes.Find(broName, false);
                if (broNodes.Length == 0)
                {
                    bro.Name = broName;
                    bro.Text = broName;
                    bro.Tag = broName + "/";
                    bro.ImageIndex = 4;
                    bro.SelectedImageIndex = 4;
                    this.treeView1.Nodes.Add(bro);
                }
            }
        }

        private void CreateSelfAndChildrenNodes(string path, List<WSFile> files)
        {
            //先遍历生成path的所有节点，再以path最后一个节点扩展其子孩子
            List<string> pathNodes = path.Split('/').ToList();
            if (pathNodes.Count == 0)
                return;
            string rootNode = string.IsNullOrEmpty(pathNodes[0]) ? "/" : pathNodes[0];

            TreeNode[] nodes = this.treeView1.Nodes.Find(rootNode, false);
            TreeNode root = new TreeNode();
            TreeNode tmpNode;
            if (nodes.Length == 0)
            {
                root.Name = rootNode;
                root.Text = rootNode;
                root.Tag = rootNode;
                root.ImageIndex = 4;
                root.SelectedImageIndex = 4;
                this.treeView1.Nodes.Add(root);
                tmpNode = root;
            }
            else
                tmpNode = nodes[0];

            foreach (string dir in pathNodes.Skip(1))
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
                    dirNode.Tag = tmpNode.Tag + file.FileName + "/";
                    dirNode.ImageIndex = 0;
                    tmpNode.Nodes.Add(dirNode);
                }
            }

            root.ExpandAll();
        }
        private void TreeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            { 
                UpdateFileManager(webShell.PathBrowse(new List<string>() { e.Node.Tag.ToString() == "/" ? e.Node.Tag.ToString() : e.Node.Tag.ToString().TrimEnd('/') }));
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

        private void ExcuteBtn_Click(object sender, EventArgs e)
        {
            this.outputTextBox.Text += this.cmdTextBox.Text;
            UpdateCmd(webShell.CmdExcute(currentCmdPath, this.cmdTextBox.Text));
        }

        private void CmdTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                UpdateCmd(webShell.CmdExcute(currentCmdPath, this.cmdTextBox.Text));
            }
        }
    }
}
