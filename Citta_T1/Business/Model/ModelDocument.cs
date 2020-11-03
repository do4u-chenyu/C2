using C2.Business.Model.World;
using C2.Business.Schedule;
using C2.Controls.Interface;
using C2.Controls.Move.Op;
using C2.Core;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;

namespace C2.Business.Model
{
    /*
     * 一个文档对应一个模型
     */
    public class ModelDocument
    {

        /*
         * 传入参数为模型文档名称，当前用户名
         */
        public string Name { get; }   // 文档标题
        public bool Dirty { get; set; }     // 字段表示模型是否被修改

        public int ElementCount { get; set; }
        public string SavePath { get; }
        public List<ModelRelation> ModelRelations { get; } // 所有线关系
        public List<ModelElement> ModelElements { get; }   // 所有元素

        public string RemarkDescription { get; set; }      // 备注描述
        public TaskManager TaskManager { get; }


        public string UserPath { get; set; }
        public bool RemarkVisible { get; set; }
        public bool FlowControlVisible { get; set; }
        public bool OperatorControlVisible { get; set; }
        // 当前模型,以ID为key的图描述
        public Dictionary<int, List<int>> ModelGraphDict { get; }

        public WorldMap WorldMap { get; }
        private static LogUtil log = LogUtil.GetInstance("ModelDocument");



        public ModelDocument(string modelTitle, string userName)
        {
            this.Name = modelTitle;
            this.ModelElements = new List<ModelElement>();
            this.ModelRelations = new List<ModelRelation>();
            this.ModelGraphDict = new Dictionary<int, List<int>>();
            this.RemarkDescription = String.Empty;
            this.RemarkVisible = false;
            this.FlowControlVisible = true;
            this.OperatorControlVisible = true;
            this.UserPath = Path.Combine(Global.WorkspaceDirectory, userName);
            this.SavePath = Path.Combine(this.UserPath, modelTitle);
            this.ElementCount = 0;
            this.TaskManager = new TaskManager();
            this.WorldMap = new WorldMap();
        }
        /*
         * 保存功能
         */
        public void Save()
        {
            DocumentSaveLoad dSaveLoad = new DocumentSaveLoad(this);
            dSaveLoad.WriteXml();
        }
        public void AddModelElement(ModelElement modelElement)
        {
            this.ModelElements.Add(modelElement);
        }

        public void AddModelRelation(ModelRelation mr)
        {
            this.ModelRelations.Add(mr);
            this.AddEdge(mr);
        }
        public void RemoveModelRelation(ModelRelation mr)
        {
            this.ModelRelations.Remove(mr);
            this.RemoveEdge(mr);
        }
        private void AddEdge(ModelRelation mr)
        {
            if (!this.ModelGraphDict.ContainsKey(mr.StartID))
                this.ModelGraphDict[mr.StartID] = new List<int>() { mr.EndID };
            else
                this.ModelGraphDict[mr.StartID].Add(mr.EndID);
        }
        private void RemoveEdge(ModelRelation mr)
        {
            if (this.ModelGraphDict.ContainsKey(mr.StartID))
                this.ModelGraphDict[mr.StartID].Remove(mr.EndID);
        }

        public void DeleteModelElement(Control control)
        {
            this.ModelElements.Remove(this.ModelElements.Find(me => me.InnerControl == control));
        }

        public void DeleteModelElement(ModelElement me)
        {
            this.ModelElements.Remove(me);
        }

        public void StatusChangeWhenDeleteControl(int ID)
        {
            foreach (ModelRelation mr in this.ModelRelations)
            {
                if (mr.StartID != ID) continue;
                StatusChangeWhenDeleteLine(mr.EndID);
            }
        }
        public void StatusChangeWhenDeleteLine(int ID)
        {
            ModelElement me = SearchElementByID(ID);
            if (me == ModelElement.Empty)
                return;
            me.Status = ElementStatus.Null;
            if (me.InnerControl is MoveOpControl)
                (me.InnerControl as MoveOpControl).EnableOption = false;
            DegradeChildrenStatus(me.ID);
        }
        public void DegradeChildrenStatus(int opID)
        {
            foreach (ModelRelation mr in this.ModelRelations)
            {
                if (mr.StartID != opID) continue;
                foreach (ModelElement me in this.ModelElements)
                {
                    if (me.ID != mr.EndID) continue;
                    DegradeStatus(me);  // 算子状态降级
                    DegradeChildrenStatus(mr.EndID);
                }
            }
        }
        // 算子状态降级规则:
        // Op算子  : Done状态降为Ready; Ready状态保持不变
        // 其他算子: 全部降为Null状态
        private void DegradeStatus(ModelElement me)
        {
            switch (me.Status)
            {
                case ElementStatus.Done:
                case ElementStatus.Ready:
                    me.Status = me.Type == ElementType.Operator ? ElementStatus.Ready : ElementStatus.Null;
                    break;
                case ElementStatus.Null:
                case ElementStatus.Runnnig:
                case ElementStatus.Stop:
                case ElementStatus.Suspend:
                default:
                    me.Status = ElementStatus.Null;
                    break;
            }
        }
        public void SetChildrenStatusNull(int ID)
        {
            foreach (ModelRelation mr in this.ModelRelations)
            {
                if (mr.StartID != ID) continue;
                foreach (ModelElement me in this.ModelElements)
                {
                    if (me.ID != mr.EndID) continue;
                    me.Status = ElementStatus.Null;
                    SetChildrenStatusNull(mr.EndID);
                }
            }
        }


        public void Load()
        {
            if (File.Exists(Path.Combine(SavePath, Name + ".xml")))
            {
                DocumentSaveLoad dSaveLoad = new DocumentSaveLoad(this);
                dSaveLoad.ReadXml();
            }
        }


        public void Show()
        {
            foreach (ModelElement el1 in this.ModelElements)
            {
                el1.Show();
                (el1.InnerControl as IMoveControl).ControlNoSelect();
            }

        }

        public void Hide()
        {
            this.ModelElements.ForEach(me => me.Hide());
        }

        public void Enable()
        {
            this.ModelElements.ForEach(me => me.Enable());
        }
        public void EnableRs()
        {
            this.ModelElements.FindAll(me => me.Type == ElementType.Result).ForEach(rs => rs.Enable());
        }
        public void UnEnable()
        {
            this.ModelElements.ForEach(me => me.UnEnable());
        }

        public int ReCountDocumentMaxElementID()
        {
            if (this.ModelElements.Count == 0)
                return 0;
            ElementCount = 1 + Math.Max(ElementCount, ModelElements.Max(me => me.ID));
            return ElementCount;
        }

        public void UpdateAllLines()
        {
            foreach (ModelRelation mr in this.ModelRelations)
            {
                try
                {
                    ModelElement sEle = SearchElementByID(mr.StartID);
                    ModelElement eEle = SearchElementByID(mr.EndID);
                    if (sEle == ModelElement.Empty || eEle == ModelElement.Empty)
                        continue;
                    // 坐标更新
                    mr.StartP = sEle.InnerControl.GetStartPinLoc(0);
                    mr.EndP = eEle.InnerControl.GetEndPinLoc(mr.EndPin);
                    // 引脚更新
                    (sEle.InnerControl as IMoveControl).OutPinInit("lineExit");
                    (eEle.InnerControl as IMoveControl).RectInAdd(mr.EndPin);
                    mr.UpdatePoints();
                }
                catch (Exception ex)
                {
                    log.Error("ModelDocument UpdateAllLines 出错: " + ex.ToString());
                }
            }
        }

        public ModelElement SearchElementByID(int ID)
        {
            return this.ModelElements.Find(me => me.ID == ID) ?? ModelElement.Empty;
        }

        // 寻找隶属于同一个二元算子的两个关系
        public List<ModelRelation> SearchBrotherRelations(ModelRelation modelRelation)
        {
            return this.ModelRelations.FindAll(me => me.EndID == modelRelation.EndID);
        }
        public ModelElement SearchResultElementByOpID(int OpID)
        {
            // 找到OpID开头的Relation
            ModelRelation mr = this.ModelRelations.Find(c => c.StartID == OpID);
            return mr == null ? ModelElement.Empty : SearchElementByID(mr.EndID);
        }

        public bool IsDuplicatedRelation(ModelRelation mr)
        {   // 关系终结于同一个元素
            return this.ModelRelations.Exists(c => c.EndID == mr.EndID && c.EndPin == mr.EndPin);
        }
        //修改xml中所有RS的path, 用newPathPrefix替换oldPathPrefix
        public static bool ModifyRsPath(string xmlPath, string oldPathPrefix, string newPathPrefix)
        {
            bool ret = true;
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlPath);
                XmlNodeList nodes = xmlDoc.GetElementsByTagName("ModelElement");
                foreach (XmlNode childNode in nodes)
                {
                    XmlNode pathNode = childNode.SelectSingleNode("path");
                    if (pathNode == null || String.IsNullOrEmpty(pathNode.InnerText))
                        continue;

                    if (pathNode.InnerText.StartsWith(oldPathPrefix))
                        pathNode.InnerText = pathNode.InnerText.Replace(oldPathPrefix, newPathPrefix);
                }
                xmlDoc.Save(xmlPath);
            }
            catch (Exception e)
            {
                log.Error("ModelDocument ModifyRSPath 出错: " + e.ToString());
                ret = false;
            }
            return ret;
        }
    }
}
