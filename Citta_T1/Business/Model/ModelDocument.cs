using Citta_T1.Business.Schedule;
using Citta_T1.Controls.Interface;
using Citta_T1.Controls.Move;
using Citta_T1.Controls.Move.Op;
using Citta_T1.Core;
using Citta_T1.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;

namespace Citta_T1.Business.Model
{
    /*
     * 一个文档对应一个模型
     */
    class ModelDocument
    {
        private string userName;//用户名
        private string modelTitle;

        private List<ModelElement> modelElements;     
        private List<ModelRelation> modelRelations;
        private Dictionary<int, List<int>> modelLineDict;  // 边字典 node -> List<node>
        private string remarkDescription;  // 备注描述信息
        private bool remarkVisible;        // 备注控件是否可见

        private string savePath;
        private bool dirty;//字段表示模型是否被修改

        private int elementCount = 0;
        
        private Point mapOrigin = new Point(-600, -300);
        private int sizeL;
        private float screenFactor;

        private Manager manager;
        private string userPath;



        /*
         * 传入参数为模型文档名称，当前用户名
         */
        public string ModelTitle {get => this.modelTitle;}
        public bool Dirty { get => dirty; set => dirty = value; }

        public int ElementCount { get => this.elementCount; set => this.elementCount = value; }
        public string SavePath { get => savePath; set => savePath = value; }
        public List<ModelRelation> ModelRelations { get => this.modelRelations; set => this.modelRelations = value; }
        public List<ModelElement> ModelElements { get => this.modelElements; set => this.modelElements = value; }


        public Point MapOrigin { get => mapOrigin; set => mapOrigin = value; }
        public string RemarkDescription { get => remarkDescription; set => remarkDescription = value; }
        public Manager Manager { get => manager; set => manager = value; }
        public int SizeL { get => this.sizeL; set => this.sizeL = value; }
        public float ScreenFactor { get => this.screenFactor; set => this.screenFactor = value; }
        public string UserPath { get => userPath; set => userPath = value; }
        public bool RemarkVisible { get => remarkVisible; set => remarkVisible = value; }
        public Dictionary<int, List<int>> ModelLineDict { get => modelLineDict; set => modelLineDict = value; }

        private static LogUtil log = LogUtil.GetInstance("ModelDocument");

        public ModelDocument(string modelTitle, string userName)
        {
            this.modelTitle = modelTitle;
            this.userName = userName;
            this.modelElements = new List<ModelElement>();
            this.modelRelations = new List<ModelRelation>();
            this.modelLineDict = new Dictionary<int, List<int>>();
            this.remarkDescription = "";
            this.remarkVisible = false;
            this.userPath = Path.Combine(Global.WorkspaceDirectory, userName);
            this.savePath = Path.Combine(this.userPath, modelTitle);

            this.manager = new Manager();
            this.sizeL = 0;
            this.screenFactor = 1;


            // lineCounter应该为`this,modelRelations`的最大值
            //this.lineCounter = this.modelRelations.Count == 0 ? -1 :   
        }
        /*
         * 保存功能
         */
        public void Save()
        {
            DocumentSaveLoad dSaveLoad = new DocumentSaveLoad(this);
            dSaveLoad.WriteXml();
    
        }

        //private int GetMaxLineID(List<ModelRelation> mrs)
        //{
        //    int maxID = -1;
        //    foreach (ModelRelation mr in mrs)
        //    {
        //        maxID = Math.Max(maxID, mr.)
        //    }
        //}
        public void AddModelElement(ModelElement modelElement)
        {
            this.modelElements.Add(modelElement);
        }
        public void RemoveModelElement(ModelElement modelElement)
        {
            this.modelElements.Remove(modelElement);
        }
        public void AddModelRelation(ModelRelation mr, bool setDirty = true)
        {
            this.modelRelations.Add(mr);
            this.AddEdge(mr);
            if (setDirty)
                Global.GetMainForm().SetDocumentDirty();
        }
        public void RemoveModelRelation(ModelRelation mr)
        {
            this.modelRelations.Remove(mr);
            this.RemoveEdge(mr);
        }
        private void AddEdge(ModelRelation mr)
        {
            if (!this.modelLineDict.ContainsKey(mr.StartID))
                this.modelLineDict[mr.StartID] = new List<int>() { mr.EndID };
            else
                this.modelLineDict[mr.StartID].Add(mr.EndID);
        }
        private void RemoveEdge(ModelRelation mr)
        {
            if (this.modelLineDict.ContainsKey(mr.StartID))
                this.modelLineDict[mr.StartID].Remove(mr.EndID);
        }
        public void DeleteModelElement(Control control)
        {
            List<ModelElement> modelElements = new List<ModelElement>(this.modelElements);
            foreach (ModelElement me in modelElements)
            {
                if (!me.GetControl.Equals(control))
                    continue;
                this.modelElements.Remove(me);
                return;
            }   
        }

        public void DeleteModelElement(ModelElement me)
        {
            this.modelElements.Remove(me);
        }

        public void StateChangeByDeleteControl(int ID)
        {

            foreach (ModelRelation mr in this.ModelRelations)
            {
                if (mr.StartID != ID) continue;
                foreach (ModelElement me in this.ModelElements)
                {
                    if (me.ID != mr.EndID) continue; 
                    me.Status = ElementStatus.Null;
                    (me.GetControl as MoveOpControl).EnableOpenOption = false;
                    //存在链路，后续链路中算子状态变化
                    AllStateChange(me.ID);

                }
            }
        }
        public void StateChangeByDeletLine(int ID)
        {

            foreach (ModelElement me in this.ModelElements)
            {
                if (me.ID != ID) continue;
                me.Status = ElementStatus.Null;
                (me.GetControl as MoveOpControl).EnableOpenOption = false;
                //存在链路，后续链路中算子状态变化
                AllStateChange(me.ID);
            }
        }
        public void AllStateChange(int operatorID)
        {
            foreach (ModelRelation mr in this.ModelRelations)
            {
                if (mr.StartID != operatorID) continue;
                foreach (ModelElement me in this.ModelElements)
                {
                    if (me.ID != mr.EndID) continue;
                    me.Status = ModifyStatus(me, me.Status);
                    AllStateChange(mr.EndID);
                }
            }
        }
        private ElementStatus ModifyStatus(ModelElement me, ElementStatus status)
        {
            if (me.Type == ElementType.Operator && status == ElementStatus.Done || status == ElementStatus.Ready)
                return ElementStatus.Ready;
            else 
                return ElementStatus.Null;

        }
        public void StateChangeByOut(int ID)
        {
           
            foreach (ModelRelation mr in this.ModelRelations)
            {              
                if (mr.StartID != ID)  continue;
                foreach (ModelElement me in this.ModelElements)
                {

                    if (me.ID != mr.EndID) continue;
                    me.Status = ElementStatus.Null;
                    StateChangeByOut(mr.EndID);

                }
            }
        }
       

        public void Load()
        {
            if (File.Exists(Path.Combine(savePath, modelTitle +".xml")))
            {
                DocumentSaveLoad dSaveLoad = new DocumentSaveLoad(this);
                dSaveLoad.ReadXml();        
            }
        }


        public void Show()
        {
            foreach (ModelElement el1 in this.modelElements)
            {
                el1.Show();
                (el1.GetControl as IMoveControl).ControlNoSelect();
            }
                
        }

        public void Hide()
        {
            foreach (ModelElement el1 in this.modelElements)
                el1.Hide();
        }

        public int ReCountDocumentMaxElementID()
        {
            if (this.modelElements.Count == 0)
                return 0;
            foreach (ModelElement me in this.modelElements)
            {
                if (me.ID > ElementCount)
                    ElementCount = me.ID;
            }
            ElementCount += 1;
            return ElementCount;
        }

        
        public Point ScreenToWorld(Point Ps, Point Pm)
        {
            Point Pw = new Point();
            Pw.X = Ps.X - Pm.X;
            Pw.Y = Ps.Y - Pm.Y;
            return Pw;
        }

        public PointF ScreenToWorldF(PointF Ps, Point Pm)
        {
            PointF Pw = new PointF();
            Pw.X = Ps.X - Pm.X;
            Pw.Y = Ps.Y - Pm.Y;
            return Pw;
        }


        //  Pw = Ps / Factor - Pm
        public Point ScreenToWorld(Point Ps)
        {
            Point Pw = new Point    
            {
                X = Convert.ToInt32(Ps.X / ScreenFactor - MapOrigin.X),
                Y = Convert.ToInt32(Ps.Y / ScreenFactor - MapOrigin.Y)
            };
            return Pw;
        }

        // Ps = (Pw + Pm) * Factor
        public Point WorldToScreen(Point Pw)
        {
            Point Ps = new Point
            {
                X = Convert.ToInt32((Pw.X + MapOrigin.X) * ScreenFactor),
                Y = Convert.ToInt32((Pw.Y + MapOrigin.Y) * ScreenFactor)
            };
            return Ps;
        }
       
        public void UpdateAllLines()
        {
            for (int i = 0;i < this.modelRelations.Count();i++)
            {
                try
                {
                    ModelRelation mr = this.modelRelations[i];

                    ModelElement sEle = SearchElementByID(mr.StartID);
                    ModelElement eEle = SearchElementByID(mr.EndID);
                    // 坐标更新
                    mr.StartP = (sEle.GetControl as IMoveControl).GetStartPinLoc(0);
                    mr.EndP = (eEle.GetControl as IMoveControl).GetEndPinLoc(mr.EndPin);
                    // 引脚更新
                    (sEle.GetControl as IMoveControl).OutPinInit("lineExit");
                    (eEle.GetControl as IMoveControl).rectInAdd(mr.EndPin);
                    mr.UpdatePoints();
                }
                catch (IndexOutOfRangeException)
                {
                    log.Error("索引越界");
                }
                catch (Exception ex)
                {
                    log.Error("ModelDocument UpdateAllLines 出错: " + ex.ToString());
                }
            }
        }
        
        public ModelElement SearchElementByID(int ID)
        {
 
            foreach (ModelElement me in this.ModelElements)
            {
                if (me.ID == ID)
                    return me;
            }
            return ModelElement.Empty;
        }
        public List<ModelRelation> SearchRelationByID(int ID,bool startID = true)
        {
            List<ModelRelation> relations = new List<ModelRelation>();
            foreach (ModelRelation mr in this.ModelRelations)
            {
                if (mr.StartID == ID && startID)
                    relations.Add(mr);
                else if (mr.EndID == ID && !startID)
                    relations.Add(mr);
            }
            return relations;
        }
        public ModelElement SearchResultOperator(int ID)
        {
            foreach (ModelRelation mr in this.ModelRelations)
            {
                if (mr.StartID != ID) continue;
                ModelElement modelElement = SearchElementByID(mr.EndID);
                if (modelElement != ModelElement.Empty && modelElement.Type == ElementType.Result)
                {
                    modelElement.Status = modelElement.Status;
                    return modelElement;
                   
                }
                   
            }
            return null; 
        }
      
        public bool IsDuplicatedRelation(ModelRelation mr)
        {
            foreach (ModelRelation _mr in this.modelRelations)
            {
                if (_mr.EndID == mr.EndID && _mr.EndPin == mr.EndPin)
                {
                    return true;
                }
            }
            return false;
        }
        //修改xml内容

        public static bool ModifyRSPath(string xmlPath, string oldPathPrefix, string newPathPrefix) 
        {
            bool ret = false;
            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                
                xmlDoc.Load(xmlPath);
                XmlNodeList nodes = xmlDoc.GetElementsByTagName("ModelElement");
                foreach (XmlNode childNode in nodes)
                {
                    XmlNode pathNode = childNode.SelectSingleNode("path");
                    if (pathNode != null && !String.IsNullOrEmpty(pathNode.InnerText) && pathNode.InnerText.StartsWith(oldPathPrefix))
                        pathNode.InnerText = pathNode.InnerText.Replace(oldPathPrefix, newPathPrefix);

                }
                xmlDoc.Save(xmlPath);
                ret = true;
            }
            catch (Exception e) 
            {
                log.Info("ModelDocument ModifyRSPath 出错: " + e.ToString());
                ret = false;
            }
            return ret;
        }
}
}
