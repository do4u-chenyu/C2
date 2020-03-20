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
        private string savePath;
        //private bool selected;
        private bool dirty;//字段表示模型是否被修改
<<<<<<< HEAD:Citta_T1/Business/ModelDocument.cs
        private Point mapOrigin = new Point(0,0);
=======
        private int elementCount = 0;
        private List<ModelRelation> modelRelations;
        private Point mapOrigin = new Point(-600,-300);

>>>>>>> de7954ca04dc817595676b868292ea382e2690f3:Citta_T1/Business/Model/ModelDocument.cs
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

        public ModelDocument(string modelTitle, string userName)
        {
            this.modelTitle = modelTitle;
            this.userName = userName;
            this.modelElements = new List<ModelElement>();
            this.modelRelations = new List<ModelRelation>();
            this.savePath = Directory.GetCurrentDirectory() + "\\cittaModelDocument\\" + userName + "\\" + modelTitle + "\\";
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
                break;
            }   
        }

        public void Load()
        {
            if (File.Exists(savePath + modelTitle +".xml"))
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

    }
}
