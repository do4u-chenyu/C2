using Citta_T1.Business.Model;
using Citta_T1.Core;
using Citta_T1.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;

namespace Citta_T1.Business.Model
{
    public class ExportModel
    {
        private string dataPath;
        private string finallyName;
        private string newModelPath;
        private string FullXmlFilePath;
        public ExportModel()
        {
            this.dataPath = string.Empty;
            this.finallyName = string.Empty;
            this.newModelPath = string.Empty;
            this.FullXmlFilePath = string.Empty;
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
        public void Export(string FullXmlFilePath)
        {
            // 模型文档不存在返回
            if (!File.Exists(FullXmlFilePath))
            {
                MessageBox.Show("模型文档不存在，可能已被删除");
                return;
            }
            this.FullXmlFilePath = FullXmlFilePath;
            // 准备要导出的模型文档
            if (!CopyModelAndDataFiles())
                return;

            // 导出Iao模型
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.AddExtension = true;
            saveFileDialog1.Filter = "模型文件(*.iao)|*.iao"; //文件类型
            saveFileDialog1.Title = "导出模型";//标题
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string fileName = saveFileDialog1.FileName;
                Utils.ZipUtil.CreateZip(newModelPath, fileName);
                MessageBox.Show("模型导出成功,存储路径：" + fileName);

            }

            // 清场
            if (Directory.Exists(newModelPath))
                Directory.Delete(newModelPath, true);


        }
        private bool CopyModelAndDataFiles()
        {

            string modelPath = Path.GetDirectoryName(this.FullXmlFilePath);
            string modelName = Path.GetFileNameWithoutExtension(this.FullXmlFilePath);
            newModelPath = Path.Combine(modelPath, modelName);
            Directory.CreateDirectory(newModelPath);
            string[] filePaths = Directory.GetFiles(modelPath, "*.*");
            foreach (string file in filePaths)
                File.Copy(file, Path.Combine(newModelPath, Path.GetFileName(file)), true);
            // 创建存储数据的_data文件夹
            this.dataPath = Path.Combine(newModelPath, "_data");
            Directory.CreateDirectory(dataPath);
            return CopyDataSourceFiles();
        }
        private bool CopyDataSourceFiles()
        {
            bool copySuccess = true;
            List<string> dataSourceNames = new List<string>();
            Dictionary<string, string> allPaths = new Dictionary<string, string>();

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(this.FullXmlFilePath);
            XmlNode rootNode = xDoc.SelectSingleNode("ModelDocument");
            // 数据源
            XmlNodeList nodes = rootNode.SelectNodes("//ModelElement[type='DataSource']");
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
            xDoc.Save(this.FullXmlFilePath);
            return copySuccess;
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
                    if (!CopyFileTo_dataFolder(optionNode, bcNode.InnerText, dataSourceNames, "browseChosen"))
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
                MessageBox.Show(path + "文件不存在，无法完成模型导出。");
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
