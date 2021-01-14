using C2.Core;
using C2.Utils;
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

        #region C2业务视图导入
        public bool UnZipC2File(string fullFilePath, string userName, string password="")
        {
            if (!File.Exists(fullFilePath))
                return false;
            if (!HasUnZipC2File(fullFilePath, userName, password))
                return false;
            // 脚本、数据源存储路径
            string dirs = Path.Combine(this.modelDir, "_datas");
            // 修改XML文件中数据源路径
            RenameBmd(dirs, this.modelFilePath);

            // 将导入模型添加到左侧模型面板
            MindMapControlAddItem(Path.GetFileNameWithoutExtension(this.modelFilePath));
            return true;
        }
        public void MindMapControlAddItem(string modelTitle)
        {
            if (Global.GetMindMapModelControl().ContainModel(modelTitle))
                return;

            Global.GetMindMapModelControl().AddMindMapModel(modelTitle);
        }

        public void RenameBmd(string dirs, string newModelFilePath)
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
                if (!fsinfo.Name.EndsWith(".bmd"))
                    dataSourcePath[fsinfo.Name] = fsinfo.FullName;
            }
            if (dataSourcePath.Count == 0)
                return;

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(newModelFilePath);

            XmlNode chart = xDoc.DocumentElement.SelectSingleNode("//chart");//仅拷贝业务拓展视图相关数据
            XmlNodeList widgets = chart.SelectNodes("//widget");  //每个节点都有一个widgets

            foreach (XmlNode widget in widgets)
            {
                XmlElement widget_e = (XmlElement)widget;
                if (widget_e.Name != "widget")
                    continue;

                XmlNodeList datas = widget_e.SelectNodes("//data_item|//chart_item|//attach_item|//result_item");
                ReWriteC2NodePath(datas, dataSourcePath);

                XmlNodeList opItems = widget_e.SelectNodes("op_items/op_item");
                foreach (XmlElement opItem in opItems)
                {
                    switch (opItem.SelectSingleNode("subtype").InnerText)
                    {
                        case "CustomOperator":
                            XmlNodeList optionNode = opItem.SelectNodes("option");
                            if (optionNode == null)
                                continue;
                            ReWriteNodePath(optionNode, dataSourcePath);
                            break;
                        case "PythonOperator":
                            XmlNode optionNode2 = opItem.SelectSingleNode("option");
                            if (optionNode2 == null)
                                continue;
                            XmlNode oppNode = optionNode2.SelectSingleNode("outputParamPath");
                            ReWriteNodePath(oppNode, dataSourcePath);
                            XmlNode bcNode = optionNode2.SelectSingleNode("browseChosen");
                            ReWriteNodePath(bcNode, dataSourcePath);
                            XmlNode ppNode = optionNode2.SelectSingleNode("pyFullPath");
                            ReWriteNodePath(ppNode, dataSourcePath);
                            XmlNode cmdNode = optionNode2.SelectSingleNode("cmd");
                            ReWriteCmdNode(cmdNode, dataSourcePath);
                            XmlNode pyParamNode = optionNode2.SelectSingleNode("pyParam");
                            ReWriteCmdNode(pyParamNode, dataSourcePath);
                            break;
                        default:
                            break;
                    }
                }

            }


            xDoc.Save(newModelFilePath);
        }

        private void ReWriteC2NodePath(XmlNodeList nodes, Dictionary<string, string> dataSourcePath)
        {
            foreach (XmlElement xmlNode in nodes)
            {
                if (xmlNode.GetAttribute("path") == null)
                    continue;
                string name = Path.GetFileName(xmlNode.GetAttribute("path"));
                if (dataSourcePath.ContainsKey(name))
                    xmlNode.SetAttribute("path", dataSourcePath[name]);
            }
        }

        public bool HasUnZipC2File(string zipFilePath, string userName, string password)
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
            ZipInputStream s = null;
            try
            {
                using (s = new ZipInputStream(File.OpenRead(zipFilePath)))
                {
                    ZipEntry theEntry;
                    while ((theEntry = s.GetNextEntry()) != null)
                    {
                        if (!Path.GetFileName(theEntry.Name).EndsWith(".bmd"))
                            continue;
                        fileName = Path.GetFileName(theEntry.Name);
                        modelName = Path.GetFileNameWithoutExtension(theEntry.Name);
                        modelPath = Path.Combine(Global.WorkspaceDirectory, userName, "业务视图", modelName);
                        break;
                    }
                }
            }
            catch
            {
                MessageBox.Show("文件内容可能破损:" + zipFilePath);
                return !hasUnZip;
            }
            finally
            {
                if (s != null)
                    s.Close();
            }


            // 未找到bmd文件
            if (string.IsNullOrEmpty(fileName))
                return !hasUnZip;
            this.modelDir = Path.Combine(Global.WorkspaceDirectory, userName, "业务视图", modelName);
            this.modelFilePath = Path.Combine(this.modelDir, fileName);

            // 是否包含同名模型文档
            if (!IsSameMindMapTitle(modelName))
            {
                //解压文件   
                if(!Utils.ZipUtil.UnZipFile(zipFilePath, "c2", password))
                    return !hasUnZip;
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
            if(!Utils.ZipUtil.UnZipFile(zipFilePath, "c2", password))
                return !hasUnZip;

            return hasUnZip;
        }

        public bool IsSameMindMapTitle(string modelTitle)
        {
            return (Global.GetMindMapModelControl().ContainModel(modelTitle) || Global.GetTaskBar().ContainModel(modelTitle));
        }
        #endregion

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
                UnZipIaoFile(fullFilePath, userName,true);
            }

        }
        public void UnZipIaoFile(string fullFilePath, string userName,bool judge)
        {
            if (!File.Exists(fullFilePath))
                return;
            if (HasUnZipIaoFile(fullFilePath, userName))
            {
                // 脚本、数据源存储路径
                string dirs = Path.Combine(this.modelDir, "_datas");
                // 修改XML文件中数据源路径
                RenameFile(dirs, this.modelFilePath);
                if (judge == true)
                {
                    // 将导入模型添加到左侧模型面板
                    MyModelControlAddItem(Path.GetFileNameWithoutExtension(this.modelFilePath));
                    HelpUtil.ShowMessageBox("模型导入成功");
                }
                    
            }
        }
        public string modelFilePath;
        public string modelDir;
        public bool HasUnZipIaoFile(string zipFilePath, string userName)
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
            ZipInputStream s = null;
            try
            {
                using (s = new ZipInputStream(File.OpenRead(zipFilePath)))
                {
                    ZipEntry theEntry;
                    while ((theEntry = s.GetNextEntry()) != null)
                    {
                        if (!Path.GetFileName(theEntry.Name).EndsWith(".xml"))
                            continue;
                        fileName = Path.GetFileName(theEntry.Name);
                        modelName = Path.GetFileNameWithoutExtension(theEntry.Name);
                        modelPath = Path.Combine(Global.WorkspaceDirectory, userName, "模型市场", modelName);
                        break;
                    }
                }
            }
            catch
            {
                MessageBox.Show("文件内容可能破损:" + zipFilePath);
                return !hasUnZip;
            }
            finally 
            {
                if (s != null)
                    s.Close();
            }
           

            // 未找到xml文件
            if (string.IsNullOrEmpty(fileName))
                return !hasUnZip;
            this.modelDir = Path.Combine(Global.WorkspaceDirectory, userName, "模型市场", modelName);
            this.modelFilePath = Path.Combine(this.modelDir, fileName);
            // 是否包含同名模型文档
            if (!IsSameModelTitle(modelName))
            {
                //解压文件   
                Utils.ZipUtil.UnZipFile(zipFilePath,"iao");
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
            Utils.ZipUtil.UnZipFile(zipFilePath,"iao");
            return hasUnZip;

        }

        public void RenameFile(string dirs, string newModelFilePath)
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
        public void MyModelControlAddItem(string modelTitle)
        {
            if (Global.GetMyModelControl().ContainModel(modelTitle))
                return;

            Global.GetMyModelControl().AddModel(modelTitle);
            // 菜单项可以打开
            Global.GetMyModelControl().EnableClosedDocumentMenu(modelTitle);
        }
        public bool IsSameModelTitle(string modelTitle)
        {
            return (Global.GetMyModelControl().ContainModel(modelTitle) || Global.GetTaskBar().ContainModel(modelTitle));
        }

    }
}
