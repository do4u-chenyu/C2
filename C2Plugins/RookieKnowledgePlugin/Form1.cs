using C2.IAOLab.Plugins;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace RookieKnowledgePlugin
{
    public partial class Form1 : Form, IPlugin
    {
        static List<string> kidFolder = new List<string>();
        int i = 0;
        int j = 0;
        public Form1()
        {
            InitializeComponent();
            WriteNodes();
        }
        public void WriteNodes() 
        {
            //string folderPath = Path.Combine(Application.StartupPath,"RookieKnowledgePlugin","cookbook");
            string folderPath = @"D:\work\C2\test";
            TreeNode treeNodeRoot1 = new TreeNode();
            treeNodeRoot1.Text = "首页";
            treeView1.Nodes.Add(treeNodeRoot1);
            WriteNodes(treeView1, folderPath);
            TreeNode treeNodeRoot2 = new TreeNode();
            treeNodeRoot2.Text = "首页";
            treeView2.Nodes.Add(treeNodeRoot2);
        }
        public string GetPluginDescription()
        {
            return "适合新手的知识宝典，来自于老鸟们多年实战中的经验总结。";
        }

        public Image GetPluginImage()
        {
            return this.Icon.ToBitmap();
        }

        public string GetPluginName()
        {
            return "技能宝典";
        }

        public string GetPluginVersion()
        {
            return "0.0.3";
        }

        public DialogResult ShowFormDialog()
        {
            return this.ShowDialog();
        }
        public void WriteLastBNodes(TreeView treeView, string folderPath, int i,int j) 
        {
            if (Directory.Exists(folderPath))
            {
                DirectoryInfo dir = new DirectoryInfo(folderPath);
                //检索表示当前目录的文件和子目录
                FileSystemInfo[] fsInfos = dir.GetFileSystemInfos();
                if (fsInfos.Length != 0)
                {
                    foreach (FileSystemInfo fsInfo in fsInfos)
                    {

                        TreeNode treeNode = new TreeNode
                        {
                            Name = fsInfo.FullName,
                            Text = fsInfo.Name.Substring(2, fsInfo.Name.Length - 2)
                        };
                        treeView.Nodes[i].Nodes[j].Nodes.Add(treeNode);
                    }

                }
            }
        }
        public void WriteChildNodes(TreeView treeView, string folderPath,int i) 
        {
            if (Directory.Exists(folderPath))
            {
                DirectoryInfo dir = new DirectoryInfo(folderPath);
                //检索表示当前目录的文件和子目录
                FileSystemInfo[] fsInfos = dir.GetFileSystemInfos();
                if (fsInfos.Length != 0)
                {
                    foreach (FileSystemInfo fsInfo in fsInfos)
                    {
                        j++;
                        if (FileOrFolder(fsInfo.FullName.ToString()))
                        {
                            TreeNode treeNode = new TreeNode
                            {
                                Name = fsInfo.FullName,
                                Text = fsInfo.Name.Substring(2, fsInfo.Name.Length - 2)
                            };
                            treeView.Nodes[i].Nodes.Add(treeNode);
                            WriteLastBNodes(treeView1, fsInfo.FullName, i,j);
                        }
                        else
                        {
                            TreeNode treeNode = new TreeNode
                            {
                                Name = fsInfo.FullName,
                                Text = fsInfo.Name.Substring(2, fsInfo.Name.Length - 2)
                            };
                            treeView.Nodes[i].Nodes.Add(treeNode);
                        }
                    }

                }
            }
        }
        public void WriteNodes(TreeView treeView, string folderPath)
        {
            if (Directory.Exists(folderPath))
            {
                DirectoryInfo dir = new DirectoryInfo(folderPath);
                //检索表示当前目录的文件和子目录
                FileSystemInfo[] fsInfos = dir.GetFileSystemInfos();
                if (fsInfos.Length != 0)
                {
                    foreach (FileSystemInfo fsInfo in fsInfos)
                    {
                        i++;
                        if (FileOrFolder(fsInfo.FullName.ToString()))
                        {
                            kidFolder.Add(fsInfo.FullName);
                            TreeNode treeNode = new TreeNode
                            {
                                Name = fsInfo.FullName,
                                Text = fsInfo.Name.Substring(2, fsInfo.Name.Length - 2)
                            };
                            treeView.Nodes.Add(treeNode);
                            WriteChildNodes(treeView1, fsInfo.FullName,i);
                        }
                        else 
                        {
                            TreeNode treeNode = new TreeNode
                            {
                                Name = fsInfo.FullName,
                                Text = fsInfo.Name.Substring(2, fsInfo.Name.Length - 2)
                            };
                            treeView.Nodes.Add(treeNode);
                        }
                    }
                }
            }
        }
        public bool FileOrFolder(string path)
        {
            if (Directory.Exists(path))
                return true;
            if (File.Exists(path))
                return false;
            return true;
        }

        private void TreeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            //string path = treeView1.SelectedNode.Name;
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                string path = treeView1.SelectedNode.Name;
                StringBuilder text = new StringBuilder();
                using (StreamReader sr = new StreamReader(path))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        line = line + @"\n\r";
                        text.Append(line);
                        textEditorControlEx1.Text = text.ToString();
                    }
                }
            }
            catch 
            { 
            }
        }
    }
}
