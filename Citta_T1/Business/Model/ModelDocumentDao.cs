using C2.Business.Schedule;
using C2.Controls.Move;
using C2.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace C2.Business.Model
{
    public class ModelDocumentDao
    {
        ModelDocument _CurrentDocument;

        public List<ModelDocument> ModelDocuments { get; set; }
        public ModelDocument CurrentDocument {
            get { return _CurrentDocument; }
            set
            {
                if (ModelDocuments == null)
                    ModelDocuments = new List<ModelDocument>() { value };
                else
                {
                    _CurrentDocument = value;
                    ModelDocuments.Clear();
                    ModelDocuments.Add(value);
                }
            }
        }
        private readonly string userInfoPath = Path.Combine(Global.WorkspaceDirectory, "UserInformation.xml");
        public ModelDocumentDao()
        {
            ModelDocuments = new List<ModelDocument>();
        }
        public ModelDocumentDao(ModelDocument doc)
        {
            ModelDocuments = new List<ModelDocument>();
            ModelDocuments.Add(doc);
            CurrentDocument = doc;
        }
        public string SaveCurrentDocument()
        {
            CurrentDocument.Save();
            CurrentDocument.Modified = false;
            return CurrentDocument.Name;
        }


        public ModelDocument LoadDocument(string modelTitle, string userName)
        {

            ModelDocument md = new ModelDocument(modelTitle, userName);
            md.Load();
            md.Hide();
            md.ReCountDocumentMaxElementID();
            CurrentDocument = md;
            ModelDocuments.Add(md);
            Global.GetCanvasPanel().FrameWrapper.InitFrame();
            return md;

        }

        public ModelElement AddDocumentOperator(MoveBaseControl ct)
        {
            ct.ID = this.CurrentDocument.ElementCount++;
            ModelElement e = ModelElement.CreateModelElement(ct);
            CurrentDocument.AddModelElement(e);
            return e;
        }



        public List<ModelElement> DeleteCurrentDocument()
        {
            if (CurrentDocument == null)
                throw new NullReferenceException();
            this.ModelDocuments.Remove(CurrentDocument);
            return CurrentDocument.ModelElements;
        }


        public ModelDocument GetManagerRelateModel(TaskManager manager)
        {
            return ModelDocuments.Find(md => md.TaskManager == manager);
        }

        // 特定Datasource在当前所有打开模型中的引用次数
        public int CountDataSourceUsage(string ffp)
        {
            int count = 0;
            foreach (ModelElement me in Global.GetCurrentDocument().ModelElements)
                if (me.Type == ElementType.DataSource && me.FullFilePath == ffp)
                    count++;
            return count;
        }
        #region 文档保存
        public void SaveEndDocuments(string userName)
        {

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(userInfoPath);
            var node = xDoc.SelectSingleNode("login");
            XmlNodeList bodyNodes = xDoc.GetElementsByTagName("user");
            foreach (XmlNode xn in bodyNodes)
            {
                if (xn.SelectSingleNode("name") != null && xn.SelectSingleNode("name").InnerText == userName)
                {
                    XmlNodeList childNodes = xn.SelectNodes("modeltitle");
                    foreach (XmlNode xmlNode in childNodes)
                        xn.RemoveChild(xmlNode);
                    string[] saveTitle = LoadAllModelTitle(userName);
                    if (saveTitle.Length == 0)
                        return;
                    foreach (ModelDocument mb in this.ModelDocuments)
                    {
                        if (!saveTitle._Contains(mb.Name))
                            continue;
                        XmlElement childElement = xDoc.CreateElement("modeltitle");
                        childElement.InnerText = mb.Name;
                        xn.AppendChild(childElement);
                    }
                    //关闭界面，用户只留下一个未保存的文档，则加载时随机打开一个文档
                    if (this.ModelDocuments.Count == 1 && !saveTitle._Contains(this.ModelDocuments[0].Name))
                    {
                        XmlElement childElement = xDoc.CreateElement("modeltitle");
                        childElement.InnerText = saveTitle[0];
                        xn.AppendChild(childElement);
                    }
                    xDoc.Save(userInfoPath);
                    return;
                }
            }


        }
        public string[] LoadAllModelTitle(string userName)
        {
            string[] modelTitles = new string[0];
            try
            {
                DirectoryInfo userDir = new DirectoryInfo(Path.Combine(Global.WorkspaceDirectory, userName));
                DirectoryInfo[] dir = userDir.GetDirectories();
                modelTitles = Array.ConvertAll(dir, value => Convert.ToString(value));
            }
            catch { }
            return modelTitles;
        }
        public ModelDocument FindModelDocument(string modelTitle)
        {
            return this.ModelDocuments.Find(md => md.Name == modelTitle);
        }
        #endregion
        #region 被干掉的方法
        //public string RemarkDescription => CurrentDocument == null ? String.Empty : CurrentDocument.RemarkDescription;



        //public string[] LoadSaveModelTitle(string userName)
        //{
        //    List<string> modelTitleList = new List<string>();
        //    XmlDocument xDoc = new XmlDocument();
        //    xDoc.Load(userInfoPath);

        //    XmlNodeList userNode = xDoc.GetElementsByTagName("user");
        //    foreach (XmlNode xn in userNode)
        //    {
        //        if (xn.SelectSingleNode("name") != null && xn.SelectSingleNode("name").InnerText == userName)
        //        {
        //            XmlNodeList childNodes = xn.SelectNodes("modeltitle");
        //            foreach (XmlNode xn2 in childNodes)
        //            {
        //                string modelTitle = xn2.InnerText;
        //                if (Directory.Exists(System.IO.Path.Combine(Global.WorkspaceDirectory, userName, modelTitle)))
        //                    modelTitleList.Add(modelTitle);
        //            }
        //            if (modelTitleList.Count > 0)
        //                return modelTitleList.Distinct().ToArray();
        //        }
        //    }
        //    return LoadAllModelTitle(userName);
        //}
        //public void AddBlankDocument(string modelTitle, string userName)
        //{
        //    ModelDocument modelDocument = new ModelDocument(modelTitle, userName);
        //    ModelDocuments.ForEach(md => md.Hide());
        //    ModelDocuments.Add(modelDocument);
        //    CurrentDocument = modelDocument;
        //    Global.GetCanvasPanel().FrameWrapper.InitFrame();
        //}
        //public string[] SaveAllDocuments()
        //{
        //    List<string> titles = new List<string>();
        //    foreach (ModelDocument md in ModelDocuments)
        //    {
        //        // 不Dirty的大文档, 不重复保存,减少硬盘写
        //        if (!md.Dirty && md.ModelElements.Count > 10)
        //            continue;

        //        md.Save();
        //        md.Dirty = false;
        //        titles.Add(md.ModelTitle);
        //    }
        //    return titles.ToArray();
        //}
        //public void SwitchDocument(string modelTitle)
        //{
        //    CurrentDocument = FindModelDocument(modelTitle);
        //    foreach (ModelDocument md in ModelDocuments)
        //    {
        //        if (md.ModelTitle == modelTitle)
        //            md.Show();
        //        else
        //            md.Hide();
        //    }
        //    Global.GetCanvasPanel().FrameWrapper.InitFrame();
        //}
        //public void UpdateRemark(RemarkControl remarkControl)
        //{
        //    if (this.CurrentDocument != null)
        //        this.CurrentDocument.RemarkDescription = remarkControl.RemarkDescription;
        //}
        //public bool WithoutDocumentLogin(string userName)
        //{
        //    //新用户登录
        //    string userDir = Path.Combine(Global.WorkspaceDirectory, userName);
        //    if (!Directory.Exists(userDir))
        //        return true;
        //    //非新用户但无模型文档
        //    DirectoryInfo di = new DirectoryInfo(userDir);
        //    DirectoryInfo[] directoryInfos = di.GetDirectories();
        //    return (directoryInfos.Length == 0);
        //}
        //// 统计当前用户有多少元素
        //public int CountAllModelElements()
        //{
        //    return this.ModelDocuments.Sum(md => md.ModelElements.Count);
        //}
        #endregion
    }
}
