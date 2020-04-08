using Citta_T1.Business.Schedule;
using Citta_T1.Controls.Interface;
using Citta_T1.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        private string remarkDescription;

        private string savePath;
        private bool dirty;//字段表示模型是否被修改

        private int elementCount = 0;
        
        private Point mapOrigin = new Point(-600,-300);

        private Manager manager;


        /*
         * 传入参数为模型文档名称，当前用户名
         */
        public string ModelTitle {get => this.modelTitle;}
        public bool Dirty { get => dirty; set => dirty = value; }

        public int ElementCount { get => this.elementCount; set => this.elementCount = value; }
        public string SavePath { get => savePath; set => savePath = value; }
        internal List<ModelRelation> ModelRelations { get => this.modelRelations; set => this.modelRelations = value; }
        internal List<ModelElement> ModelElements { get => this.modelElements; set => this.modelElements = value; }


        public Point MapOrigin { get => mapOrigin; set => mapOrigin = value; }
        public string RemarkDescription { get => remarkDescription; set => remarkDescription = value; }
        public Manager Manager { get => manager; set => manager = value; }

        public ModelDocument(string modelTitle, string userName)
        {
            this.modelTitle = modelTitle;
            this.userName = userName;
            this.modelElements = new List<ModelElement>();
            this.modelRelations = new List<ModelRelation>();
            this.remarkDescription = "";
            this.savePath = Directory.GetCurrentDirectory() + "\\cittaModelDocument\\" + userName + "\\" + modelTitle + "\\";

            this.manager = new Manager();
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
            this.modelElements.Add(modelElement);
            dirty = true;
        }
        public void AddModelRelation(ModelRelation modelRelation)
        {
            this.modelRelations.Add(modelRelation);
            dirty = true;
        }

        public void DeleteModelElement(Control control)
        {
           
            foreach (ModelElement me in this.modelElements)
            {
                if (!me.GetControl.Equals(control))
                    continue;
                this.modelElements.Remove(me);
                //删除与控件连接的关系
                DeleteModelRelation(me.ID);
                return;
            }   
        }
        public void DeleteModelRelation(int ID)
        {
            List<ModelRelation> relations = new List<ModelRelation>();
            foreach (ModelRelation mr in this.ModelRelations)
            {
                if (mr.StartID == ID || mr.EndID == ID)
                    relations.Add(mr);
            }
            //后续所有算子状态变为null
            StateChange(ID);
            foreach (ModelRelation mr in relations) 
                this.ModelRelations.Remove(mr);

        }
        private void StateChange(int ID)
        {
            foreach (ModelRelation mr in this.ModelRelations)
            {
                if (mr.StartID == ID)
                {
                    foreach (ModelElement me in this.ModelElements)
                    {
                        if (me.ID == mr.EndID)
                        {
                            if(me.Type==ElementType.Operator)
                                me.Status = ElementStatus.Null;
                            StateChange(mr.EndID);
                        }
                           
                    }
                }
            }
        }
        public void Load()
        {
            if (File.Exists(savePath + modelTitle +".xml"))
            {
                DocumentSaveLoad dSaveLoad = new DocumentSaveLoad(this);
                dSaveLoad.ReadXml();            }          
        }

        public void Show()
        {
            foreach (ModelElement el1 in this.modelElements)
            {
                el1.Show();
            }
        }

        public void Hide()
        {
            foreach (ModelElement el1 in this.modelElements)
            {
                el1.Hide();
            }
        }

        public void ResetCount()
        {
            int num = 0;
            foreach (ModelElement me in this.modelElements)
            {
                if (me.ID > num)
                    num = me.ID;
            }
            this.elementCount = num;   
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
        private LogUtil log = LogUtil.GetInstance("CanvasPanel");
        public void UpdateAllLines()
        {
            log.Info("划线更新");
            for (int i = 0;i < this.modelRelations.Count();i++)
            {
                ModelRelation mr = this.modelRelations[i];
                // 0 被RemarkControl占用了
                ModelElement sEle = this.modelElements[mr.StartID - 1];
                ModelElement eEle = this.modelElements[mr.EndID - 1];
                // 坐标更新
                mr.StartP = (sEle.GetControl as IMoveControl).GetStartPinLoc(0);
                mr.EndP = (eEle.GetControl as IMoveControl).GetEndPinLoc(mr.EndPin);
                mr.UpdatePoints();
                // 控件线绑定
                (sEle.GetControl as IMoveControl).BindStartLine(0, i);
                (eEle.GetControl as IMoveControl).BindEndLine(mr.EndPin, i);
            }
        }

    }
}
