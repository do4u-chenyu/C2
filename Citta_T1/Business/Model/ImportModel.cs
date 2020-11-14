using C2.Core;
using ICSharpCode.SharpZipLib.Zip;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;

namespace C2.Business.Model
{
    public class ImportModel
    {
        public ImportModel()
        {
            modelFilePath = string.Empty;

        }
        private static ImportModel ImportModelInstance;
        public static ImportModel GetInstance()
        {
            if (ImportModelInstance == null)
            {
                ImportModelInstance = new ImportModel();
            }
            return ImportModelInstance;
        }
        public void ImportIaoFile(string userName)
        {

            //获取导入模型路径
            OpenFileDialog fd = new OpenFileDialog
            {
                Filter = "模型文件(*.iao)|*.iao",
                Title = "导入模型",
                AddExtension = true
            };
            if (fd.ShowDialog() == DialogResult.OK)
            {
                string fullFilePath = fd.FileName;
                UnZipIaoFile(fullFilePath, userName);
            }

        }
        private void UnZipIaoFile(string fullFilePath, string userName)
        {
            ;
            if (!File.Exists(fullFilePath))
                return;
            if (HasUnZipIaoFile(fullFilePath, userName))
            {
                // 脚本、数据源存储路径
                string dirs = Path.Combine(this.modelDir, "_data");
                // 修改XML文件中数据源路径
                RenameFile(dirs, this.modelFilePath);
                // 将导入模型添加到左侧模型面板
                MyModelControlAddItem(Path.GetFileNameWithoutExtension(this.modelFilePath));
                MessageBox.Show("模型导入成功。");
            }

        }
        private string modelFilePath;
        private string modelDir;
        private bool HasUnZipIaoFile(string zipFilePath, string userName)
        {
            /*
             * 是否存在同名模型文档
             * 存在，选择覆盖或取消导入
             */
            bool hasUnZip = true;
            string fileName = string.Empty;
            string modelName = string.Empty;
            string modelPath = string.Empty;
            DialogResult result;
            using (ZipInputStream s = new ZipInputStream(File.OpenRead(zipFilePath)))
            {
                ZipEntry theEntry;
                while ((theEntry = s.GetNextEntry()) != null)
                {
                    if (!Path.GetFileName(theEntry.Name).EndsWith(".xml"))
                        continue;
                    fileName = Path.GetFileName(theEntry.Name);
                    modelName = Path.GetFileNameWithoutExtension(theEntry.Name);
                    modelPath = Path.Combine(Global.WorkspaceDirectory, userName, modelName);
                    break;
                }
            }

            // 未找到xml文件
            if (string.IsNullOrEmpty(fileName))
                return !hasUnZip;
            this.modelDir = Path.Combine(Global.WorkspaceDirectory, userName, modelName);
            this.modelFilePath = Path.Combine(this.modelDir, fileName);
            // 是否包含同名模型文档
            if (!IsSameModelTitle(modelName))
            {
                //解压文件   
                Utils.ZipUtil.UnZipFile(zipFilePath);
                return hasUnZip;
            }

            result = MessageBox.Show("模型文件:" + modelName + "已存在，是否覆盖该模型文档", "导入模型", MessageBoxButtons.OKCancel);
            if (result == DialogResult.Cancel)
                return !hasUnZip;

            if (Global.GetTaskBar().ContainModel(modelName))
            {
                MessageBox.Show("模型文件:" + modelName + "已打开，请关闭该文档并重新进行导入", "关闭模型文档");
                return !hasUnZip;
            }
            // 删除原始模型文件、解压新文件                    
            if (Directory.Exists(modelPath))
                Directory.Delete(modelPath, true);
            Utils.ZipUtil.UnZipFile(zipFilePath);
            return hasUnZip;

        }

        private void RenameFile(string dirs, string newModelFilePath)
        {
            Dictionary<string, string> dataSourcePath = new Dictionary<string, string>();
            if (Directory.Exists(dirs))
            {
                DirectoryInfo dir0 = new DirectoryInfo(dirs);
                FileInfo[] fsinfos0 = dir0.GetFiles();
                foreach (FileInfo fsinfo in fsinfos0)
                {
                    // 建立数据源、脚本名称与路径对应字典
                    dataSourcePath[fsinfo.Name] = fsinfo.FullName;
                }
            }

            DirectoryInfo dir1 = new DirectoryInfo(Path.GetDirectoryName(newModelFilePath));
            FileInfo[] fsinfos1 = dir1.GetFiles();
            foreach (FileInfo fsinfo in fsinfos1)
            {
                // 建立结果文件名称与路径对应字典
                if (!fsinfo.Name.EndsWith(".xml"))
                    dataSourcePath[fsinfo.Name] = fsinfo.FullName;
            }
            if (dataSourcePath.Count == 0)
                return;

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(newModelFilePath);
            XmlNode rootNode = xDoc.SelectSingleNode("ModelDocument");
            // 数据源
            XmlNodeList nodes = rootNode.SelectNodes("//ModelElement[type='DataSource']");
            ReWriteNodePath(nodes, dataSourcePath);
            // 结果算子
            XmlNodeList resultNodes = rootNode.SelectNodes("//ModelElement[type='Result']");
            ReWriteNodePath(resultNodes, dataSourcePath);
            // AI、多源算子
            XmlNodeList customNodes0 = rootNode.SelectNodes("//ModelElement[subtype='CustomOperator1']");
            XmlNodeList customNodes1 = rootNode.SelectNodes("//ModelElement[subtype='CustomOperator2']");
            foreach (XmlNode node in customNodes0)
            {
                XmlNodeList optionNode = node.SelectNodes("option");
                if (optionNode == null)
                    continue;

                ReWriteNodePath(optionNode, dataSourcePath);
            }
            foreach (XmlNode node in customNodes1)
            {
                XmlNodeList optionNode = node.SelectNodes("option");
                if (optionNode == null)
                    continue;

                ReWriteNodePath(optionNode, dataSourcePath);
            }
            // python算子
            XmlNodeList pythonNodes = rootNode.SelectNodes("//ModelElement[subtype='PythonOperator']");

            foreach (XmlNode node in pythonNodes)
            {
                XmlNode optionNode = node.SelectSingleNode("option");
                if (optionNode == null)
                    continue;
                XmlNode oppNode = optionNode.SelectSingleNode("outputParamPath");
                ReWriteNodePath(oppNode, dataSourcePath);
                XmlNode bcNode = optionNode.SelectSingleNode("browseChosen");
                ReWriteNodePath(bcNode, dataSourcePath);
                XmlNode ppNode = optionNode.SelectSingleNode("pyFullPath");
                ReWriteNodePath(ppNode, dataSourcePath);
                XmlNode cmdNode = optionNode.SelectSingleNode("cmd");
                ReWriteCmdNode(cmdNode, dataSourcePath);
                XmlNode pyParamNode = optionNode.SelectSingleNode("pyParam");
                ReWriteCmdNode(pyParamNode, dataSourcePath);
            }
            xDoc.Save(newModelFilePath);
        }
        private void ReWriteCmdNode(XmlNode cmdNode, Dictionary<string, string> dataSourcePath)
        {
            if (cmdNode == null || string.IsNullOrEmpty(cmdNode.InnerText))
                return;
            Regex reg0 = new Regex(Global.regPath);
            string[] cmd = cmdNode.InnerText.Split(' ');
            Dictionary<string, string> paths = new Dictionary<string, string>();
            foreach (string item in cmd)
            {
                if (reg0.IsMatch(item))
                    paths[Path.GetFileName(item)] = item;
            }
            foreach (string fileName in paths.Keys)
            {
                if (dataSourcePath.ContainsKey(fileName))
                    cmdNode.InnerText = cmdNode.InnerText.Replace(paths[fileName], dataSourcePath[fileName]);
            }
        }
        private void ReWriteNodePath(XmlNodeList nodes, Dictionary<string, string> dataSourcePath)
        {
            foreach (XmlNode xmlNode in nodes)
            {
                if (xmlNode.SelectSingleNode("path") == null)
                    continue;
                string name = Path.GetFileName(xmlNode.SelectSingleNode("path").InnerText);
                if (dataSourcePath.ContainsKey(name))
                    xmlNode.SelectSingleNode("path").InnerText = dataSourcePath[name];

            }
        }
        private void ReWriteNodePath(XmlNode node, Dictionary<string, string> dataSourcePath)
        {
            if (node == null || string.IsNullOrEmpty(node.InnerText))
                return;
            string name = Path.GetFileName(node.InnerText);
            if (dataSourcePath.ContainsKey(name))
                node.InnerText = dataSourcePath[name];
        }
        private void MyModelControlAddItem(string modelTitle)
        {
            if (Global.GetMyModelControl().ContainModel(modelTitle))
                return;

            Global.GetMyModelControl().AddModel(modelTitle);
            // 菜单项可以打开
            Global.GetMyModelControl().EnableClosedDocumentMenu(modelTitle);
        }
        private bool IsSameModelTitle(string modelTitle)
        {
            return (Global.GetMyModelControl().ContainModel(modelTitle) || Global.GetTaskBar().ContainModel(modelTitle));
        }

    }
}
