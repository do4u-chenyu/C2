using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
using C2.Core;
using C2.Utils;

namespace C2.Business.Model
{
    public class ExportModel
    {
        private string dataPath;                 //创建存储数据的_data文件夹
        private string finallyName;
        private string tmpModelPath;        //临时模型的路径（文件夹）
        private string xmlFullPath;             //模型文件（.bmd .xml）原始路径
        private string newModelName;     //重命名后的模型名称

        public ExportModel()
        {
            dataPath = string.Empty;
            finallyName = string.Empty;
            tmpModelPath = string.Empty;
            xmlFullPath = string.Empty;
            newModelName = string.Empty;
        }

        private static ExportModel ExportModelInstance;
        public static ExportModel GetInstance()
        {
            if (ExportModelInstance == null)
            {
                ExportModelInstance = new ExportModel();
            }
            return ExportModelInstance;
        }

        #region 导出业务视图
        public bool ExportC2Model(string oldFullPath, string exportFullPath, string password = "")
        {
            xmlFullPath = oldFullPath;
            newModelName = Path.GetFileNameWithoutExtension(exportFullPath);

            //模型关联文件复制
            if (!CopyModelAndDataFiles(Global.TempDirectory, true))
                return false;

            //生成压缩包
            string error = ZipUtil.CreateZip(tmpModelPath, exportFullPath, password);
            if (!string.IsNullOrEmpty(error))
            {
                HelpUtil.ShowMessageBox(error);
                return false;
            }

            return true;
        }

        private bool C2CopylAllDatas(string tmpXmlFullPath)
        {
            bool copySuccess = true;
            List<string> dataSourceNames = new List<string>();
            Dictionary<string, string> allPaths = new Dictionary<string, string>();
            List<string> xmlPaths = new List<string>();//存放c1模型xml的路径

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(tmpXmlFullPath);
            XmlNode chart = xDoc.DocumentElement.SelectSingleNode("//chart");//仅拷贝业务拓展视图相关数据
            XmlNodeList widgets = chart.SelectNodes("//widget");  //每个节点都有一个widgets

            foreach (XmlNode widget in widgets)
            {
                XmlNodeList datas = widget.SelectNodes("data_items/data_item|op_items/op_item/data_item|chart_items/chart_item|attach_items/attach_item|op_items/op_item/result_item|result_items/result_item");
                if (!CopyDataSource(datas, allPaths, dataSourceNames))
                    return !copySuccess;

                XmlNodeList opItems = widget.SelectNodes("op_items/op_item");
                foreach (XmlNode opItem in opItems)
                {
                    switch (opItem.SelectSingleNode("subtype").InnerText)
                    {
                        case "CustomOperator":
                            if (!CopyCustomOperatorFile(opItem.SelectNodes("."), allPaths, dataSourceNames))
                                return !copySuccess;
                            break;
                        case "PythonOperator":
                            if (!CopyPythonOperatorFiles(opItem.SelectNodes("."), allPaths, dataSourceNames))
                                return !copySuccess;
                            break;
                        case "model":
                            xmlPaths.Add(opItem.SelectSingleNode("path").InnerText);
                            break;
                        default:
                            break;
                    }
                }

                if ((widget as XmlElement).GetAttribute("type") == "PICTURE")
                    CopyPic(widget.SelectNodes("."), allPaths, dataSourceNames);
            }

            foreach (string xmlPath in xmlPaths)
            {
                string desPath = Path.Combine(tmpModelPath, Path.GetFileNameWithoutExtension(xmlPath));
                string tmpXmlPath = Path.Combine(desPath, Path.GetFileName(xmlPath));
                FileUtil.CreateDirectory(desPath);
                File.Copy(xmlPath, tmpXmlPath, true);

                if (!CopyXmlDataSource(tmpXmlPath, allPaths, dataSourceNames))
                    return !copySuccess;
            }

            xDoc.Save(tmpXmlFullPath);
            return copySuccess;
        }

        private bool CopyDataSource(XmlNodeList nodes, Dictionary<string, string> allPaths, List<string> dataSourceNames)
        {
            bool copySuccess = true;
            foreach (XmlElement xmlNode in nodes)
            {
                string path = xmlNode.GetAttribute("path");
                if (string.IsNullOrEmpty(path))
                    continue;
                // 相同数据源，直接使用已经命名好的数据源，防止已修改的同名文件
                if (allPaths.ContainsKey(path))
                {
                    xmlNode.SetAttribute("path", allPaths[path]);
                    continue;
                }

                if (!CopyFileRewriteXml(xmlNode, path, dataSourceNames))
                    return !copySuccess;

                allPaths[path] = xmlNode.GetAttribute("path");
            }
            return copySuccess;
        }

        private bool CopyPic(XmlNodeList nodes, Dictionary<string, string> allPaths, List<string> dataSourceNames)
        {
            bool copySuccess = true;
            foreach (XmlElement xmlNode in nodes)
            {
                string path = xmlNode.GetAttribute("image_url");
                if (string.IsNullOrEmpty(path) || string.IsNullOrEmpty(Path.GetDirectoryName(path))) //内置图片不用copy
                    continue;
                // 相同数据源，直接使用已经命名好的数据源，防止已修改的同名文件
                if (allPaths.ContainsKey(path))
                {
                    xmlNode.SetAttribute("image_url", allPaths[path]);
                    continue;
                }

                if (!CopyFileRewriteXml(xmlNode, path, dataSourceNames, "image_url"))
                    return !copySuccess;

                allPaths[path] = xmlNode.GetAttribute("image_url");
            }
            return copySuccess;
        }

        private bool CopyFileRewriteXml(XmlNode xmlNode, String path, List<string> dataSourceNames, string nodeName = "path")
        {
            bool copySuccess = true;
            string pathName = Path.GetFileName(path);

            // 导出模型文档再次导出
            if (string.Equals(path, Path.Combine(this.dataPath, pathName)))
                return copySuccess;
            if (!File.Exists(path))
            {
                HelpUtil.ShowMessageBox(path + "文件不存在，无法完成模型导出。");
                return !copySuccess;
            }

            // _data中包含同名文件，新添加的文件要重命名并修改Xml对应路径中文件名
            if (dataSourceNames.Contains(pathName))
            {
                pathName = GetNewName(pathName, dataSourceNames);
                string newPath = Path.Combine(Path.GetDirectoryName(path), pathName);
                (xmlNode as XmlElement).SetAttribute(nodeName, (xmlNode as XmlElement).GetAttribute(nodeName).Replace(path, newPath));
            }
            File.Copy(path, Path.Combine(this.dataPath, pathName), true);
            dataSourceNames.Add(pathName);
            finallyName = pathName;
            return copySuccess;
        }

        #endregion

        public bool Export(string fullXmlFilePath, string modelNewName, string exportFilePath)
        {
            // 模型文档不存在返回
            if (!File.Exists(fullXmlFilePath))
            {
                HelpUtil.ShowMessageBox("模型文档不存在，可能已被删除");
                return false;
            }
            xmlFullPath = fullXmlFilePath;
            newModelName = modelNewName;

            // 准备要导出的模型文档
            if (!CopyModelAndDataFiles(exportFilePath, false))
            {
                FileUtil.DeleteDirectory(tmpModelPath);
                return false;
            }

            return true;
        }

        private bool CopyModelAndDataFiles(string exportFilePath, bool isC2Model)
        {
            FileUtil.DeleteDirectory(Global.TempDirectory);

            this.tmpModelPath = Path.Combine(exportFilePath, newModelName); //待压缩的文件先放入公共临时文件夹
            this.dataPath = Path.Combine(tmpModelPath, "_datas");// 创建存储数据的_data文件夹
            FileUtil.CreateDirectory(this.tmpModelPath);
            FileUtil.CreateDirectory(this.dataPath);

            string destFileName;
            string tmpXmlFullPath = string.Empty;
            string[] filePaths = Directory.GetFiles(Path.GetDirectoryName(xmlFullPath), "*.*");
            foreach (string file in filePaths)
            {
                if (!file.Equals(xmlFullPath))
                    continue;

                destFileName = isC2Model ? newModelName + ".bmd" : newModelName + ".xml";
                tmpXmlFullPath = Path.Combine(tmpModelPath, destFileName);
                File.Copy(file, Path.Combine(tmpModelPath, destFileName), true);
            }

            if (string.IsNullOrEmpty(tmpXmlFullPath))//不存在xml文件
                return false;

            if (isC2Model)
                return C2CopylAllDatas(tmpXmlFullPath);
            else
                return C1CopyAllDatas(tmpXmlFullPath);
        }

        private bool CopyXmlDataSource(string xmlPath, Dictionary<string, string> allPaths, List<string> dataSourceNames)
        {
            bool copySuccess = true;

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(xmlPath);
            XmlNode rootNode = xDoc.SelectSingleNode("ModelDocument");
            // 数据源、结果
            XmlNodeList nodes = rootNode.SelectNodes("//ModelElement[type='DataSource']|//ModelElement[type='Result']");
            // dataSourceNames存放拷贝到_data目录中的文件名称
            if (!CopyDataSourceOperatorFile(nodes, allPaths, dataSourceNames))
                return !copySuccess;

            // AI、多源算子
            XmlNodeList customNodes = rootNode.SelectNodes("//ModelElement[subtype='CustomOperator1']|//ModelElement[subtype='CustomOperator2']");
            if (!CopyCustomOperatorFile(customNodes, allPaths, dataSourceNames))
                return !copySuccess;

            // Python算子
            /*
             * 遍历XML所有python算子的cmd节点
             * 取出cmd节点的路径
             * 是已经处理过相同路径，跳过
             * 不是处理过的路径，文件拷贝
             */
            XmlNodeList pythonNodes = rootNode.SelectNodes("//ModelElement[subtype='PythonOperator']");
            if (!CopyPythonOperatorFiles(pythonNodes, allPaths, dataSourceNames))
                return !copySuccess;

            //// Python、AI、多源算子连接的结果算子路径赋值
            //XmlNodeList resultNodes = rootNode.SelectNodes("//ModelElement[type='Result']");
            //foreach (XmlNode node in resultNodes)
            //{
            //    if (node.SelectSingleNode("path") == null)
            //        throw new ArgumentNullException("message: The path of the result operator is empty"); ;
            //    string path = node.SelectSingleNode("path").InnerText;
            //    if (allPaths.ContainsKey(path))
            //        node.SelectSingleNode("path").InnerText = allPaths[path];
            //}
            xDoc.Save(xmlPath);
            return copySuccess;
        }

        private bool C1CopyAllDatas(string tmpXmlFullPath)
        {
            List<string> dataSourceNames = new List<string>();
            Dictionary<string, string> allPaths = new Dictionary<string, string>();

            return CopyXmlDataSource(tmpXmlFullPath, allPaths, dataSourceNames);
        }
        private bool CopyCustomOperatorFile(XmlNodeList nodes, Dictionary<string, string> allPaths, List<string> dataSourceNames)
        {
            bool copySuccess = true;
            foreach (XmlNode xmlNode in nodes)
            {
                if (xmlNode.SelectSingleNode("option/path") == null)
                    continue;
                string path = xmlNode.SelectSingleNode("option/path").InnerText;

                // 相同数据源，直接使用已经命名好的数据源
                if (allPaths.ContainsKey(path))
                {
                    xmlNode.SelectSingleNode("option/path").InnerText = allPaths[path];
                    continue;
                }

                if (!CopyFileTo_dataFolder(xmlNode, path, dataSourceNames, "option/path"))
                    return !copySuccess;

                allPaths[path] = xmlNode.SelectSingleNode("option/path").InnerText;
            }
            return copySuccess;
        }
        private bool CopyPythonOperatorFiles(XmlNodeList pythonNodes, Dictionary<string, string> allPaths, List<string> dataSourceNames)
        {
            bool copySuccess = true;
            Regex reg0 = new Regex(Global.regPath);
            foreach (XmlNode pythonNode in pythonNodes)
            {
                XmlNode optionNode = pythonNode.SelectSingleNode("option");
                if (optionNode == null)
                    continue;

                XmlNode bcNode = optionNode.SelectSingleNode("browseChosen");
                if (bcNode != null)
                {
                    if (allPaths.ContainsKey(bcNode.InnerText))
                        bcNode.InnerText = allPaths[bcNode.InnerText];
                    else if (!CopyFileTo_dataFolder(optionNode, bcNode.InnerText, dataSourceNames, "browseChosen"))
                        return !copySuccess;
                }
                XmlNode cmdNode = optionNode.SelectSingleNode("cmd");
                if (cmdNode == null || string.IsNullOrEmpty(cmdNode.InnerText))
                    continue;
                string outputParamPath = string.Empty;
                if (optionNode.SelectSingleNode("outputParamPath") != null)
                    outputParamPath = optionNode.SelectSingleNode("outputParamPath").InnerText;

                string[] cmd = optionNode.SelectSingleNode("cmd").InnerText.Split(OpUtil.Blank);
                List<string> paths = new List<string>();
                foreach (string item in cmd)
                {
                    bool factor0 = reg0.IsMatch(item);
                    bool factor1 = !item.ToLower().Contains("python.exe");
                    bool factor2 = string.IsNullOrEmpty(outputParamPath) || !item.Contains(outputParamPath);
                    if (factor0 && factor1 && factor2)
                        paths.Add(item);
                }
                foreach (string path in paths)
                {
                    if (allPaths.ContainsKey(path))
                    {
                        // 相同数据源，直接使用已经命名好的数据源
                        cmdNode.InnerText = cmdNode.InnerText.Replace(path, allPaths[path]);
                        continue;
                    }
                    // 拷贝文件到_data目录
                    if (!CopyFileTo_dataFolder(optionNode, path, dataSourceNames, "cmd"))
                        return !copySuccess;
                    // 修改cmd中路径的文件名
                    string newPath = Path.Combine(Path.GetDirectoryName(path), this.finallyName);
                    // 修改其他节点中对应路径的文件名
                    ModifySubNodePath(optionNode, path, newPath);
                    allPaths[path] = newPath;
                }
            }
            return copySuccess;
        }

        private bool CopyDataSourceOperatorFile(XmlNodeList nodes, Dictionary<string, string> allPaths, List<string> dataSourceNames)
        {
            bool copySuccess = true;
            foreach (XmlNode xmlNode in nodes)
            {
                if (xmlNode.SelectSingleNode("path") == null)
                    continue;

                string path = xmlNode.SelectSingleNode("path").InnerText;
                // 相同数据源，直接使用已经命名好的数据源
                if (allPaths.ContainsKey(path))
                {
                    xmlNode.SelectSingleNode("path").InnerText = allPaths[path];
                    continue;
                }

                if (!CopyFileTo_dataFolder(xmlNode, path, dataSourceNames))
                    return !copySuccess;

                allPaths[path] = xmlNode.SelectSingleNode("path").InnerText;
            }
            return copySuccess;
        }
        private void ModifySubNodePath(XmlNode node, string path, string newPath)
        {
            XmlNode pyFullPath = node.SelectSingleNode("pyFullPath");
            if (pyFullPath != null && string.Equals(pyFullPath.InnerText, path))
            {
                pyFullPath.InnerText = newPath;
            }
            XmlNode pyParam = node.SelectSingleNode("pyParam");
            if (pyParam != null && pyParam.InnerText.Contains(path))
            {
                pyParam.InnerText = pyParam.InnerText.Replace(path, newPath);
            }
        }

        private bool CopyFileTo_dataFolder(XmlNode xmlNode, String path, List<string> dataSourceNames, string nodeName = "path")
        {
            bool copySuccess = true;
            string pathName = Path.GetFileName(path);

            // 导出模型文档再次导出
            if (string.IsNullOrEmpty(path) || string.Equals(path, Path.Combine(this.dataPath, pathName)))
                return copySuccess;

            if (!File.Exists(path))
            {
                HelpUtil.ShowMessageBox(path + "文件不存在，无法完成模型导出。");
                return !copySuccess;
            }

            // _data中包含同名文件，新添加的文件要重命名并修改Xml对应路径中文件名
            if (dataSourceNames.Contains(pathName))
            {
                pathName = GetNewName(pathName, dataSourceNames);
                string newPath = Path.Combine(Path.GetDirectoryName(path), pathName);
                xmlNode.SelectSingleNode(nodeName).InnerText = xmlNode.SelectSingleNode(nodeName).InnerText.Replace(path, newPath);
            }
            File.Copy(path, Path.Combine(this.dataPath, pathName), true);
            dataSourceNames.Add(pathName);
            finallyName = pathName;
            return copySuccess;
        }

        private string GetNewName(string pathName, List<string> dataSourceNames)
        {
            while (dataSourceNames.Contains(pathName))
            {
                pathName = "副本-" + pathName;
            }
            return pathName;
        }

    }
}
