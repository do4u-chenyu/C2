using Citta_T1.Business.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Citta_T1.Business.Schedule
{

    /// <summary>
    /// 并发的处理类
    /// </summary> 
    class Triple
    {
        private List<ModelElement> dataElements;
        private ModelElement operateElement;
        private ModelElement resultElement;
        private string tripleName;

        //是否算过，算过说明结果算子已经在之前某次运算中算过
        //暂定，结果节点有个状态，当它连接的算子节点算完了，算子节点和结果节点，同步状态done
        private bool isOperated;

        public Triple(List<ModelElement> dataElements, ModelElement operateElement, ModelElement resultElement)
        {
            this.DataElements = dataElements;
            this.OperateElement = operateElement;
            this.ResultElement = resultElement;
            this.isOperated = resultElement.Status.Equals("Done") ? true : false;
            this.tripleName = string.Join("", dataElements.Select(c => c.Description).ToArray()) + "-" + operateElement.Description + "-" + resultElement.Description;
        }

        public bool IsOperated { get => isOperated; set => isOperated = value; }
        public List<ModelElement> DataElements { get => dataElements; set => dataElements = value; }
        public ModelElement OperateElement { get => operateElement; set => operateElement = value; }
        public ModelElement ResultElement { get => resultElement; set => resultElement = value; }
        public string TripleName { get => tripleName;}
    }
}
