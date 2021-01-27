using C2.Core;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;

namespace C2.Business.Model
{
    public class ExportModel
    {
        private string DataPath;         //创建存储数据的_data文件夹 this.DataPath = Path.Combine(TmpModelPath, "_datas");
        private string FinallyName;
        private string TmpModelPath;     //临时模型的路径（文件夹）TmpModelPath = Path.Combine(Path.GetDirectoryName(this.XmlFullPath), NewModelName);
        private string XmlFullPath;  //模型文件（.bmd .xml）原始路径
        private string NewModelName;     //重命名后的模型名称
        public ExportModel()
        {
            this.DataPath = string.Empty;
            this.FinallyName = string.Empty;
            this.TmpModelPath = string.Empty;
            this.XmlFullPath = string.Empty;
            this.NewModelName = string.Empty;
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
        public bool ExportC2Model(string oldFullPath, string exportFullPath, string password="")
        {
            this.XmlFullPath = oldFullPath;
            this.NewModelName = Path.GetFileNameWithoutExtension(exportFullPath);

            //模型关联文件复制
            if (!CopyModelAndDataFiles(Path.GetDirectoryName(this.XmlFullPath), true))
            {
                if (Directory.Exists(TmpModelPath))
                    Directory.Delete(TmpModelPath, true);
                return false;
            }

            //生成压缩包
            try
            {
                ZipUtil.CreateZip(TmpModelPath, exportFullPath, password);
            }
            catch(Exception e)
            {
                HelpUtil.ShowMessageBox(e.Message);
                if (Directory.Exists(TmpModelPath))
                    Directory.Delete(TmpModelPath, true);
                return false;
            }

            //删除临时文件夹
            if (Directory.Exists(TmpModelPath))
                Directory.Delete(TmpModelPath, true);

            return true;
        }

        private bool C2CopyDataSourceFiles()
        {
            bool copySuccess = true;
            List<string> dataSourceNames = new List<string>();
            Dictionary<string, string> allPaths = new Dictionary<string, string>();

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(this.XmlFullPath);

            XmlNode chart = xDoc.DocumentElement.SelectSingleNode("//chart");//仅拷贝业务拓展视图相关数据
            XmlNodeList widgets = chart.SelectNodes("//widget");  //每个节点都有一个widgets

            List<string> xmlPaths = new List<string>();//存放c1模型xml的路径

            foreach (XmlNode widget in widgets)
            {
                XmlNodeList datas = widget.SelectNodes("data_items/data_item|op_items/op_item/data_item|chart_items/chart_item|attach_items/attach_item");
                CopyDataSource(datas, allPaths, dataSourceNames);

                //模型的结果同步到业务视图的结果算子，也需要修改路径
                XmlNodeList results = widget.SelectNodes("op_items/op_item/result_item|result_items/result_item");
                foreach(XmlNode result in results)
                {
                    if ((result as XmlElement).GetAttribute("result_type") == "ModelOp")
                        CopyDataSource(result.SelectNodes("."), allPaths, dataSourceNames);
                }

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
            }

            //需要根据this.XmlFullPath找到当前xml,且存放路径也要改变
            foreach(string xmlPath in xmlPaths)
            {
                string desPath = Path.Combine(TmpModelPath, Path.GetFileNameWithoutExtension(xmlPath));
                
                //先拷贝一份文件夹到临时文件夹，模型算子的文件夹里存放xml及结果文件，其中结果文件路径在bmd也存储了一份，由上面bmd直接复制
                if (Directory.Exists(desPath))
                    continue;
                Directory.CreateDirectory(desPath);
                File.Copy(xmlPath, Path.Combine(desPath, Path.GetFileName(xmlPath)), true);

                CopyDataSourceFilesPerXml(xmlPath, allPaths, dataSourceNames, true);
            }

            xDoc.Save(this.XmlFullPath);
            return copySuccess;
        }

        private bool CopyDataSource(XmlNodeList nodes, Dictionary<string, string> allPaths, List<string> dataSourceNames)
        {
            bool copySuccess = true;
            foreach (XmlElement xmlNode in nodes)
            {
                if (xmlNode.GetAttribute("path") == null)
                    continue;

                string path = xmlNode.GetAttribute("path");
                // 相同数据源，直接使用已经命名好的数据源
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

        private bool CopyFileRewriteXml(XmlNode xmlNode, String path, List<string> dataSourceNames, string nodeName = "path")
        {
            bool copySuccess = true;
            string pathName = Path.GetFileName(path);

            // 导出模型文档再次导出
            if (string.IsNullOrEmpty(path) || string.Equals(path, Path.Combine(this.DataPath, pathName)))
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
            File.Copy(path, Path.Combine(this.DataPath, pathName), true);
            dataSourceNames.Add(pathName);
            FinallyName = pathName;
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
            this.XmlFullPath = fullXmlFilePath;
            this.NewModelName = modelNewName;
            // 准备要导出的模型文档
            if (!CopyModelAndDataFiles(exportFilePath))
            {
                if (Directory.Exists(TmpModelPath))
                    Directory.Delete(TmpModelPath, true);
                return false;
            }
                

            return true;
        }

        public void GenExportIAO()
        {
            //TODO C1模型导出功能先隐去，后续应该可以删除
            // 导出Iao模型
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.AddExtension = true;
            saveFileDialog1.Filter = "模型文件(*.iao)|*.iao"; //文件类型
            saveFileDialog1.Title = "导出模型";//标题
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string fileName = saveFileDialog1.FileName;
                ZipUtil.CreateZip(TmpModelPath, fileName);
                HelpUtil.ShowMessageBox("模型导出成功,存储路径：" + fileName);
            }

            // 清场
            if (Directory.Exists(TmpModelPath))
                Directory.Delete(TmpModelPath, true);
        }

        private bool CopyModelAndDataFiles(string exportFilePath, bool isC2Model = false)
        {
            //string modelName = Path.GetFileNameWithoutExtension(this.XmlFullPath);

            string modelPath = Path.GetDirectoryName(this.XmlFullPath);
            TmpModelPath = Path.Combine(exportFilePath, isC2Model ? string.Format("{0}_{1}", NewModelName, DateTime.Now.ToString("hhmmss")) : NewModelName);//业务视图临时文件夹可能与模型算子文件夹同名，加时间戳做区分
            Directory.CreateDirectory(TmpModelPath);
            string[] filePaths = Directory.GetFiles(modelPath, "*.*");
            foreach (string file in filePaths)
            {
                //xml文件重命名
                string sourceFileName = Path.GetFileName(file);
                string destFileName;
                if (!isC2Model && sourceFileName == Path.GetFileNameWithoutExtension(this.XmlFullPath) + ".xml")
                    destFileName = NewModelName + ".xml";
                else if (isC2Model && sourceFileName == Path.GetFileNameWithoutExtension(this.XmlFullPath) + ".bmd")
                    destFileName = NewModelName + ".bmd";
                else
                    destFileName = sourceFileName;
                
                File.Copy(file, Path.Combine(TmpModelPath, destFileName), true);
            }

            // 创建存储数据的_data文件夹
            this.DataPath = Path.Combine(TmpModelPath, "_datas");
            Directory.CreateDirectory(DataPath);
            if (!isC2Model)
                return CopyDataSourceFiles();
            else
                return C2CopyDataSourceFiles();
        }

        private bool CopyDataSourceFilesPerXml(string xmlPath, Dictionary<string, string> allPaths, List<string> dataSourceNames, bool isC2Model=false)
        {
            bool copySuccess = true;

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(xmlPath);
            XmlNode rootNode = xDoc.SelectSingleNode("ModelDocument");
            // 数据源
            XmlNodeList nodes = rootNode.SelectNodes("//ModelElement[type='DataSource']");
            // dataSourceNames存放拷贝到_data目录中的文件名称
            if (!CopyDataSourceOperatorFile(nodes, allPaths, dataSourceNames))
                return !copySuccess;

            //结果也要存一份
            if (isC2Model)
            {
                XmlNodeList results = rootNode.SelectNodes("//ModelElement[type='Result']");
                if (!CopyDataSourceOperatorFile(results, allPaths, dataSourceNames))
                    return !copySuccess;
            }
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

            // Python、AI、多源算子连接的结果算子路径赋值
            XmlNodeList resultNodes = rootNode.SelectNodes("//ModelElement[type='Result']");
            foreach (XmlNode node in resultNodes)
            {
                if (node.SelectSingleNode("path") == null)
                    throw new ArgumentNullException("message: The path of the result operator is empty"); ;
                string path = node.SelectSingleNode("path").InnerText;
                if (allPaths.ContainsKey(path))
                    node.SelectSingleNode("path").InnerText = allPaths[path];
            }
            xDoc.Save(xmlPath);
            return copySuccess;
        }

        private bool CopyDataSourceFiles()
        {
            List<string> dataSourceNames = new List<string>();
            Dictionary<string, string> allPaths = new Dictionary<string, string>();

            return CopyDataSourceFilesPerXml(this.XmlFullPath, allPaths, dataSourceNames);
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
                    else if(!CopyFileTo_dataFolder(optionNode, bcNode.InnerText, dataSourceNames, "browseChosen"))
                         return !copySuccess;
                }
                XmlNode cmdNode = optionNode.SelectSingleNode("cmd");
                if (cmdNode == null || string.IsNullOrEmpty(cmdNode.InnerText))
                    continue;
                string outputParamPath = string.Empty;
                if (optionNode.SelectSingleNode("outputParamPath") != null)
                    outputParamPath = optionNode.SelectSingleNode("outputParamPath").InnerText;

                string[] cmd = optionNode.SelectSingleNode("cmd").InnerText.Split(' ');
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
                    string newPath = Path.Combine(Path.GetDirectoryName(path), this.FinallyName);
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
            if (string.IsNullOrEmpty(path) || string.Equals(path, Path.Combine(this.DataPath, pathName)))
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
            File.Copy(path, Path.Combine(this.DataPath, pathName), true);
            dataSourceNames.Add(pathName);
            FinallyName = pathName;
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
