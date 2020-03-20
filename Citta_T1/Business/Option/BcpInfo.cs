using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Citta_T1.Business.Model;
namespace Citta_T1.Business.Option
{
    class BcpInfo
    {
        private string fullBcpPath;  //BCP文件全路径
        private string fileName;     //BCP文件名
        private string name;         //对应的数据源名称
        private ElementType type;    //对应的类型:数据源 或 Result
        private List<ColumnInfo> columnInfos;
    }
}
