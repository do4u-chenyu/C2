using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace C2.Business.CastleBravo.WebShellTool
{
    public partial class WebShellDetailsForm : Form
    {
        private readonly WebShellClient webShell;

        private string browserDicectory = string.Empty;
        private string commandDirectory = string.Empty;
        readonly WebShellTaskConfig webShellTask = new WebShellTaskConfig();
        private string PageData = string.Empty;
        private WSFile seleFile;
        public WebShellDetailsForm()
        {
            InitializeComponent();
        }
        public WebShellDetailsForm(WebShellTaskConfig info) : this()
        {
            
            webShellTask = info;
            webShell = new WebShellClient(info.Url, info.Password, info.ClientVersion,info.DatabaseConfig);
            UpdateBaseInfo(webShell.PHPInfo());
        }

        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string loginInfo = webShell;
            if (tabControl1.SelectedTab.Text == "文件管理")
                UpdateFileManager(webShell.PathBrowser());
            if (tabControl1.SelectedTab.Text == "基础信息")
                UpdateBaseInfo(webShell.PHPInfo());
            if (tabControl1.SelectedTab.Text == "虚拟终端")
                UpdateCmd(webShell.ShellStart());
            if (tabControl1.SelectedTab.Text == "数据库管理")
                ShowDatabase(webShell.DatabeseInfo());
        }
        private void ShowDatabase(string dbResult) 
        {
            if (dbResult == "->||<-" || string.IsNullOrEmpty(dbResult)) 
            {
                this.messageLog.Text = webShell.FetchLog() + "\n数据库连接失败";
                return;
            }
            string[] dbResults = dbResult.Split('|');
            foreach (string result in dbResults) 
            {
                string name = result.Replace("\t\r\n",string.Empty).Replace("\t\t",string.Empty);//返回报文为sql命令行显示模式，包含有大量连续分隔符，需要去除
                if (name == String.Empty)
                    continue;
                TreeNode[] broNodes = this.treeView2.Nodes.Find(name, false);
                if (broNodes.Length == 0)
                    this.treeView2.Nodes.Add(new TreeNode
                    {
                        Tag = name + "/",
                        Name = name,
                        Text = name,
                        ImageIndex = 4,
                        SelectedImageIndex = 4
                    });
            }
            this.messageLog.Text = webShell.FetchLog();
        }
        private void UpdateDatabase(string dbResult, string selectNode) 
        {
            TreeNode[] broNodes = this.treeView2.Nodes.Find(selectNode, false);
            if (string.IsNullOrEmpty(dbResult))
                return;
            string[] dbResults = dbResult.Split('|');
            for(int i =1; i< dbResults.Length; i++)
            {
                string name = dbResults[i].Replace("\t\r\n", string.Empty).Replace("\t\t", string.Empty).TrimEnd('\t');
                if (name == String.Empty || broNodes[0].Nodes.Find(name,true).Length > 0)//跳过同名表
                    continue;
                broNodes[0].Nodes.Add(new TreeNode
                {
                    Tag = name + "/",
                    Name = name,
                    Text = name,
                    ImageIndex = 4,
                    SelectedImageIndex = 4
                });
            }
            this.messageLog.Text = webShell.FetchLog();
        }
        private void ReadTable(string tableData) 
        {
            //var bytes = src.GetBytes(tableData);
            //bytes = Encoding.Convert(src, dst, bytes);
            //dst.GetString(bytes);
            string[] lines = tableData.Split(new string[] { "\t\r\n" }, StringSplitOptions.None);
            dataGridView1.Columns.Clear();
            int i = 0;
            if (lines.Length == 0)
                return;
            string[] rowName = lines[0].Split('|');
            while (i < rowName.Length - 1)//添加第一行
            {
                dataGridView1.Columns.Add(i.ToString(), rowName[i]);
                i++;
            }
            for (int j = 1; j < lines.Length - 1; j++)//最后一行为\t 从第二行开始
            {
                string[] data = lines[j].Split(new string[] { "\t|\t" }, StringSplitOptions.None);
                int index = dataGridView1.Rows.Add();
                for (int k = 0; k < data.Length; k++)
                {
                    if (k == data.Length - 1)
                        dataGridView1.Rows[index].Cells[k].Value = data[k].Replace("\t|", string.Empty);
                    else
                        dataGridView1.Rows[index].Cells[k].Value = data[k].Replace("\t", string.Empty);
                }
               
            }
        }
        private void TreeView2_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode node = treeView2.SelectedNode;
            string selectNode = node.Text;
            if (node.Level == 0)
                UpdateDatabase(webShell.DatabeseInfo(webShellTask.DatabaseConfig, selectNode + "\t", String.Format("show tables from {0}", selectNode)), selectNode);
            if (node.Level == 1)
            {
                ReadTable(webShell.DatabeseInfo(webShellTask.DatabaseConfig, node.Parent.Text, String.Format("select * from {0}", selectNode)));
                this.messageLog.Text = webShell.FetchLog();
            }
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
            if (selectedFile.Type == WebShellFileType.Directory)
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

        private void 浏览ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.fileManagerListView.SelectedItems.Count == 0)
                return;
            seleFile = this.fileManagerListView.SelectedItems[0].Tag as WSFile;
            PageData = browserDicectory + "/" + seleFile.FileName;
            if (seleFile.Type == WebShellFileType.File)
            {
                new DetailsPageForm(webShell.DetailInfo(PageData)).ShowDialog();
                this.messageLog.Text = webShell.FetchLog();
            }
        }

        private void 下载ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.fileManagerListView.SelectedItems.Count == 0)
                return;
            seleFile = this.fileManagerListView.SelectedItems[0].Tag as WSFile;
            byte[] download = webShell.DownloadFile(PageData);
            download = download.Skip(3).Take(download.Length - 6).ToArray();

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                //Filter = "ext files (*.txt)|*.txt|All files(*.*)|*>**",
                Filter = "All files(*.*)|*>**",
                FileName = seleFile.FileName
            };

            if (saveFileDialog.ShowDialog() != DialogResult.OK)
                return;

            try
            {
                using (var fs = new FileStream(saveFileDialog.FileName, FileMode.Create, FileAccess.Write))
                    fs.Write(download, 0, download.Length);

                this.messageLog.Text = "下载文件成功" + Environment.NewLine + webShell.FetchLog();
            }
            catch (Exception ex)
            {
                this.messageLog.Text = "保存文件时发生异常:" + ex.Message + Environment.NewLine + webShell.FetchLog();
            }  
        }
    }
}
