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

namespace Citta_T1.Business
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
        private Point mapOrigin = new Point(-600,-300);
        /*
         * 传入参数为模型文档名称，当前用户名
         */
        public string ModelDocumentTitle {get => this.modelTitle;}
        public bool Dirty { get => dirty; set => dirty = value; }
        public Point MapOrigin { get => mapOrigin; set => mapOrigin = value; }
        public ModelDocument(string modelTitle, string userName)
        {
            this.modelTitle = modelTitle;
            this.userName = userName;
            this.modelElements = new List<ModelElement>();
            this.savePath = Directory.GetCurrentDirectory() + "\\cittaModelDocument\\" + userName + "\\" + modelTitle + "\\";
        }
        /*
         * 保存功能
         */
        public void Save()
        {
            DocumentSaveLoad dSaveLoad = new DocumentSaveLoad(savePath, modelTitle);
            dSaveLoad.WriteXml(modelElements);
        }
        public void AddModelElement(ModelElement modelElement)
        {
            this.modelElements.Add(modelElement);
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
                DocumentSaveLoad dSaveLoad = new DocumentSaveLoad(savePath, modelTitle);
                this.modelElements = dSaveLoad.ReadXml();
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
        
        public Point ScreenToWorld(Point Ps, Point Pm)
        {
            Point Pw = new Point();
            Pw.X = Ps.X - Pm.X;
            Pw.Y = Ps.Y - Pm.Y;
            return Pw;
        }

        public List<ModelElement> ModelElements()
        { return this.modelElements; }
    }
}
