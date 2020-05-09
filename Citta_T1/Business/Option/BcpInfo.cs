using System.Collections.Generic;
using Citta_T1.Utils;
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
        private DSUtil.Encoding encoding;     //BCP文件对应的编码
        public string columnLine;
        public string separator;

        public BcpInfo(string fullBcpPath, string name, ElementType type, DSUtil.Encoding encoding)
        {
            this.fullBcpPath = fullBcpPath;
            fileName = System.IO.Path.GetFileName(this.fullBcpPath);
            this.name = name;
            this.type = type;
            this.encoding = encoding;
            InitColumnInfo();

        }
        // 根据第一行初始化列信息
        private void InitColumnInfo()
        {
            this.columnLine = BCPBuffer.GetInstance().GetCacheColumnLine(this.fullBcpPath, encoding);

        }
    }
}
