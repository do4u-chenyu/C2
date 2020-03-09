using System;
using System.Collections;
using System.Collections.Generic;
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
        private ModelElement mlElement;
        /*
         * 传入参数为模型文档名称，当前用户名
         */
        public string ModelDocumentTitle {get => this.modelTitle;}
        public bool Dirty { get => dirty; set => dirty = value; }

        public ModelDocument(string modelTitle, string userName)
        {
            this.modelTitle = modelTitle;
            this.userName = userName;
            modelElements = new List<ModelElement>();
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
            modelElements.Add(modelElement);
            dirty = true;
        }
        
        public void DeleteModelElement(Control control)
        {
            
            foreach (ModelElement me in this.modelElements)
            {
                if (me.GetControl.Equals(control))
                    mlElement = me;
                
            }
            this.modelElements.Remove(mlElement);
        }
        public List<ModelElement> Load()
        {
            if (File.Exists(savePath + modelTitle +".xml"))//-------------------------------
            {
                DocumentSaveLoad dSaveLoad = new DocumentSaveLoad(savePath, modelTitle);
                this.modelElements = dSaveLoad.ReadXml();
            }          
            return this.modelElements;
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
                Console.WriteLine("隐藏===");
            }
        }
        public List<ModelElement> CurrentDocumentElement()
        { return this.modelElements; }
    }
}
