using C2.Business.Model;
using System.Collections.Generic;
using System.Linq;

namespace C2.Business.Schedule
{
    /// <summary>
    /// 最小处理单位，三元组类
    /// </summary> 
    public class Triple
    {
        public List<ModelElement> DataElements { get; }
        public ModelElement OperateElement { get; }
        public ModelElement ResultElement { get; }
        public string TripleName { get; }

        public Triple(List<ModelElement> dataElements, ModelElement operateElement, ModelElement resultElement)
        {
            this.DataElements = dataElements;
            this.OperateElement = operateElement;
            this.ResultElement = resultElement;
            this.TripleName =  string.Format("{0}----{1}----{2}",string.Join("&&", dataElements.Select(c => c.Description).ToArray()), operateElement.Description, resultElement.Description);
        }
    }
}
