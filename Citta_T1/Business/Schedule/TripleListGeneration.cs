using Citta_T1.Business.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Citta_T1.Utils;

namespace Citta_T1.Business.Schedule
{
    /// <summary>
    /// 根据前台保存模型生成一个三元组列表
    /// </summary>

    class TripleListGeneration
    {
        private List<Triple> currentModelTripleList;
        private ModelDocument currentModel;
        private String state;
        private ModelElement stopElement;
        public TripleListGeneration(ModelDocument currentModel, String state)
        {
            //“运行”构造方法
            this.currentModel = currentModel;
            this.state = state;
        }

        public TripleListGeneration(ModelDocument currentModel, String state, ModelElement stopElement)
        {
            //“运行到此”构造方法
            this.currentModel = currentModel;
            this.state = state;
            this.stopElement = stopElement;
        }

        public void GenerateList()
        {
            List<ModelElement> endElement = null;

            if (this.state == "button")
            {
                //从“运行”按钮进入,找到模型的最后一个元素
                //TODO
                //结束元素可能有多个,需要判断每个元素的出度
                //endElement.Add(currentModel.ModelElements().Last());
            }
            else if (this.state == "select")
            {
                //从“运行到此”右键选项进入
                //endElement.Add(this.stopElement);
            }

            //从endElement出发，遍历，把三元组存入list

        }

        public void SearchNewTriple(ModelElement a)
        {

        }


        public List<Triple> CurrentModelTripleList { get => currentModelTripleList; set => currentModelTripleList = value; }
    }
}
