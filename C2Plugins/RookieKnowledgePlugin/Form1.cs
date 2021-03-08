using C2.IAOLab.Plugins;
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace RookieKnowledgePlugin
{
    public partial class Form1 : Form, IPlugin
    {
        static List<string> kidFolder = new List<string>();
        int i = 0;
        int j = 0;

        private TreeNode linuxRoot;
        private TreeNode pythonRoot;

        public Form1()
        {
            InitializeComponent();
            InitializeTrees();
        }
        private void InitializeTrees()
        {

            InitializePythonTree();
            InitializeLinuxTree();

            // GetZipInfo(Properties.Resources.cookbook);
            // GetZipInfo 从项目的资源文件读取zip内容
            // WriteNodes（*，*）可以去掉，在GetZipInfo里获得目录名，创建节点就好

           // string folderPath = @"C:\Users\iao\Desktop\work\C2\test";
        }
        
        
        private void InitializeLinuxTree()
        {
            linuxRoot = new TreeNode();
            linuxRoot.Text = "首页";
            linuxTreeView.Nodes.Add(linuxRoot);
        }

        private void InitializePythonTree()
        {
            pythonRoot = new TreeNode();
            pythonRoot.Text = "首页";
            pythonTreeView.Nodes.Add(pythonRoot);
        }
        private void GetZipInfo(byte[] zipFile)
        {
            Stream stream = new MemoryStream(zipFile);
            ZipInputStream s = null;
            try
            {
                using (s = new ZipInputStream(stream))
                {
                    ZipEntry theEntry;
                    while ((theEntry = s.GetNextEntry()) != null)
                    {
                        string name = theEntry.Name;

                    }
                }
            }
            catch
            {

            }
            finally
            {
                if (s != null)
                    s.Close();
            }
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
        public void WriteLastBNodes(TreeView treeView, string folderPath, int i, int j)
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
        public void WriteChildNodes(TreeView treeView, string folderPath, int i)
        {
            if (!Directory.Exists(folderPath))
                return;
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
                        WriteLastBNodes(linuxTreeView, fsInfo.FullName, i, j);
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
        public void WriteNodes(TreeView treeView, string folderPath)
        {
            if (!Directory.Exists(folderPath))
                return;
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
                        WriteChildNodes(linuxTreeView, fsInfo.FullName, i);
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
        private bool FileOrFolder(string path)
        {
            if (Directory.Exists(path))
                return true;
            if (File.Exists(path))
                return false;
            return true;
        }



        private void LinuxTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            String path = linuxTreeView.SelectedNode.Name;
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    textEditorControlEx1.Text = sr.ReadToEnd();
                }
            }
            catch { textEditorControlEx1.Text = String.Empty; }
        }
    }
}
