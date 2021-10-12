using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace C2.Business.CastleBravo.WebShellTool
{
    public partial class WebShellDetailsForm : Form
    {
        private readonly WebShellClient webShell;

        private string browserDicectory = string.Empty;
        private string commandDirectory = string.Empty;

        public WebShellDetailsForm()
        {
            InitializeComponent();
        }

        public WebShellDetailsForm(WebShellTaskConfig info) : this()
        {
            webShell = new WebShellClient(info.Url, info.Password, info.ClientVersion);
            UpdateBaseInfo(webShell.PHPInfo());
        }

        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.Text == "文件管理")
                UpdateFileManager(webShell.PathBrowser());
            if (tabControl1.SelectedTab.Text == "基础信息")
                UpdateBaseInfo(webShell.PHPInfo());
            if (tabControl1.SelectedTab.Text == "虚拟终端")
                UpdateCmd(webShell.ShellStart());        
        }

        private void UpdateCmd(Tuple<string, string> excuteResult)
        {
            commandDirectory = excuteResult.Item1;
            string nextExcutePath = commandDirectory.StartsWith("/") ? string.Format("[{0}]$", commandDirectory) : string.Format("{0}>", commandDirectory);
            string output = excuteResult.Item2;

            this.outputTextBox.Text = this.outputTextBox.Text + "\r\n" + output + "\r\n\r\n" + nextExcutePath;
            this.outputTextBox.Focus();//获取焦点
            this.outputTextBox.Select(this.outputTextBox.TextLength, 0);//光标定位到文本最后
            this.outputTextBox.ScrollToCaret();//滚动到光标处

            this.cmdTextBox.Text = string.Empty;
            this.messageLog.Text = webShell.FetchLog();
        }

        private void UpdateBaseInfo(string result)
        {
            this.baseInfoWebBrowser.DocumentText = result;
            this.messageLog.Text = webShell.FetchLog();
        }

        private void FileManagerListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.fileManagerListView.SelectedItems.Count == 0)
                return;

            WSFile selectedFile = this.fileManagerListView.SelectedItems[0].Tag as WSFile;
            if (selectedFile.Type != WebShellFileType.Directory)
                return;

            //TODO linux和windows拼接不一样，用path.combine拼不了linux路径？
            UpdateFileManager(webShell.PathBrowser(browserDicectory + "/" + selectedFile.FileName)); 
        }

        private void UpdateFileManager(Tuple<string, List<WSFile>, List<string>> pathFiles)
        {
            string path = pathFiles.Item1.Replace("\\","/");
            List<WSFile> files = pathFiles.Item2;
            List<string> broPaths = pathFiles.Item3;

            //更新textbox显示路径
            browserDicectory = this.filePathTb.Text = path;

            //更新右侧listview
            this.fileManagerListView.Items.Clear();
            foreach (WSFile file in files)
            {
                ListViewItem lvi = new ListViewItem
                {
                    Tag = file,
                    Text = file.FileName,
                    ImageIndex = (int)file.Type - 1
                };
                lvi.SubItems.Add(file.CreateTime);
                lvi.SubItems.Add(file.FileSize);
                lvi.SubItems.Add(file.LastMod);

                this.fileManagerListView.Items.Add(lvi);
            }

            //更新左侧treeview
            CreateBroNodes(broPaths);//生成兄弟节点，这里仅针对 windows
            CreateSelfAndChildrenNodes(path, files);//生成自己和孩子节点

            this.messageLog.Text = webShell.FetchLog();
        }

        private void CreateBroNodes(List<string> broPaths)
        {
            foreach (string bro in broPaths)
            {
                if (string.IsNullOrEmpty(bro))
                    continue;

                string name = bro + ":";

                TreeNode[] broNodes = this.treeView1.Nodes.Find(name, false);
                if (broNodes.Length == 0)
                    this.treeView1.Nodes.Add(new TreeNode
                    {
                        Tag  = name + "/",
                        Name = name,
                        Text = name,
                        ImageIndex = 4,
                        SelectedImageIndex = 4
                    });
            }
        }

        private void CreateSelfAndChildrenNodes(string content, List<WSFile> files)
        {
            //先遍历生成path的所有节点，再以path最后一个节点扩展其子孩子
            string[] pathNodes = content.Split('/');
            if (pathNodes.Length == 0)
                return;
            string name = string.IsNullOrEmpty(pathNodes[0]) ? "/" : pathNodes[0];

            TreeNode[] nodes = this.treeView1.Nodes.Find(name, false);
            TreeNode self = new TreeNode();
            TreeNode cursorNode = self;
            if (nodes.Length == 0)
            {
                self.Tag  = name;
                self.Name = name;
                self.Text = name;
                self.ImageIndex = 4;
                self.SelectedImageIndex = 4;
                this.treeView1.Nodes.Add(self);
            }
            else
                cursorNode = nodes[0];

            foreach (string dir in pathNodes.Skip(1))
            {
                if (string.IsNullOrEmpty(dir))
                    break;

                TreeNode[] subNodes = cursorNode.Nodes.Find(dir, false);

                if (subNodes.Length == 0)
                {
                    TreeNode child = new TreeNode
                    {
                        Tag  = cursorNode.Tag + dir + "/",
                        Name = dir,
                        Text = dir,
                        ImageIndex = 0
                    };
                    cursorNode.Nodes.Add(child);
                    cursorNode = child;
                }
                else
                    cursorNode = subNodes[0];
            }

            foreach (WSFile file in files)
            {
                if (file.Type != WebShellFileType.Directory)
                    continue;

                TreeNode[] subNodes = cursorNode.Nodes.Find(file.FileName, false);
                if (subNodes.Length == 0)
                    cursorNode.Nodes.Add(new TreeNode()
                    {
                        Tag  = cursorNode.Tag + file.FileName + "/",
                        Name = file.FileName,
                        Text = file.FileName,
                        ImageIndex = 0
                    });
            }

            self.ExpandAll();
        }
        private void TreeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                UpdateFileManager(webShell.PathBrowser(e.Node.Tag.ToString() == "/" ? "/" : e.Node.Tag.ToString().TrimEnd('/')));
        }

        //曲线救国之双击不展开
        public int m_MouseClicks = 0;

        private void TreeView1_MouseDown(object sender, MouseEventArgs e)
        {
            this.m_MouseClicks = e.Clicks;
        }

        private void TreeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            e.Cancel = this.m_MouseClicks > 1;
        }

        private void TreeView1_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            e.Cancel = this.m_MouseClicks > 1;
        }

        private void ExcuteBtn_Click(object sender, EventArgs e)
        {
            this.outputTextBox.Text += this.cmdTextBox.Text;
            UpdateCmd(webShell.Excute(commandDirectory, this.cmdTextBox.Text));
        }

        private void CmdTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                UpdateCmd(webShell.Excute(commandDirectory, this.cmdTextBox.Text));
        }
    }
}
