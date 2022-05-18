﻿using C2.IAOLab.Plugins;
using C2.Log;
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace RookieKnowledgePlugin
{
    public partial class RookieForm : Form, IPlugin
    {
        private TreeNode linuxRoot;
        private TreeNode pythonRoot;
        private String tempPath;

        public RookieForm()
        {
            InitializeComponent();
            InitializeTempPath();
            
        }
        private void InitializeTrees()
        {
            if (pythonRoot == null)
                InitializePythonTree();
            if (linuxRoot == null)
                InitializeLinuxTree();

            // GetZipInfo(Properties.Resources.cookbook);
            // GetZipInfo 从项目的资源文件读取zip内容
            // WriteNodes（*，*）可以去掉，在GetZipInfo里获得目录名，创建节点就好

            // string folderPath = @"C:\Users\iao\Desktop\work\C2\test";
        }

        private void InitializeTempPath()
        {
            tempPath = Path.Combine(Path.GetTempPath(), "C2", "plugins", "RookieKnowledgePlugin");
            try
            {
                Directory.Delete(tempPath,true);
                Directory.CreateDirectory(tempPath);
            }
            catch { }
        }

        private void InitializeLinuxTree()
        {
            UnZip(Properties.Resources.Linux, tempPath);
            linuxRoot = new TreeNode
            {
                Text = "首页",
                Name = "首页"
            };
            linuxTreeView.Nodes.Add(linuxRoot);
            WriteNodes(linuxRoot, Path.Combine(tempPath, "Linux"));
            linuxRoot.ExpandAll();
        }

        private void InitializePythonTree()
        {
            UnZip(Properties.Resources.Python, tempPath);
            pythonRoot = new TreeNode
            {
                Text = "首页",
                Name = "首页"
            };
            pythonTreeView.Nodes.Add(pythonRoot);
            WriteNodes(pythonRoot, Path.Combine(tempPath, "Python"));
            pythonRoot.ExpandAll();
        }
        private void UnZip(byte[] zipBuffer, String path)
        {
            FastZip fastZip = new FastZip();
            fastZip.ExtractZip(new MemoryStream(zipBuffer),
                path,
                FastZip.Overwrite.Always,
                null,
                String.Empty,
                String.Empty,
                false,
                false);
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

        private void WriteNodes(TreeNode root, String path)
        {
            if (!Directory.Exists(path))
                return;

            DirectoryInfo pathInfo = new DirectoryInfo(path);
            foreach (FileSystemInfo fsi in pathInfo.GetFileSystemInfos())
            {
                if (!fsi.Exists)
                    continue;

                TreeNode node = new TreeNode
                {
                    Name = fsi.FullName,
                    Text = Regex.Replace(fsi.Name, @"^\d+_\s*", ""),
                };
                root.Nodes.Add(node);
                if (Directory.Exists(node.Name))
                    WriteNodes(node, node.Name);
            }
        }

        private void LinuxTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Name == "首页")
            {
                linuxTextBox.SetTextAndRefresh("Linux 首页");
            }
            else
                TreeView_AfterSelect(linuxTextBox, e.Node.Name);
        }

        private void PythonTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Name == "首页")
            {
                pythonTextBox.SetTextAndRefresh("Python 首页");
            }
            else
                TreeView_AfterSelect(pythonTextBox, e.Node.Name);
        }


        private void TreeView_AfterSelect(ICSharpCode.TextEditor.TextEditorControlEx tc, String name)
        {
            try
            {
                using (StreamReader sr = new StreamReader(name))
                {
                    tc.SetTextAndRefresh(sr.ReadToEnd());
                }
            }
            catch { tc.SetTextAndRefresh(String.Empty); }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitializeTrees();
        }

        private void LinuxFilterTB_TextChanged(object sender, EventArgs e)
        {
            VisibleTreeNode(linuxRoot, this.linuxFilterTB.Text);
        }

        private void PythonFilterTB_TextChanged(object sender, EventArgs e)
        {
            VisibleTreeNode(pythonRoot, this.pythonFilterTB.Text);
        }

        private void VisibleTreeNode(TreeNode root, String filter)
        {
            root.TreeView.SuspendLayout();
            if (String.IsNullOrEmpty(filter))
            {
                root.ExpandAll();
                return;
            }

            root.Collapse();
            FindNodes(root, filter);
            root.TreeView.ResumeLayout(false);
        }

        private void FindNodes(TreeNode root, String filter)
        {
            if (root.Text.Contains(filter) && root.GetNodeCount(true) == 0)
            {
                Expand(root); 
            }
               
            foreach (TreeNode node in root.Nodes)
            {
                FindNodes(node, filter);
            }
        }

        private void Expand(TreeNode node)
        {
            while (node != null && !node.IsExpanded)
            {
                node.Expand();
                node = node.Parent;
            }
        }

    }
}
