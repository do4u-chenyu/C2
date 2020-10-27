using C2.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2.Model
{
    public class DataItem
    {
        string filePath;
        string fileName;
        char fileSep;
        OpUtil.Encoding fileEncoding;
        OpUtil.ExtType fileType;
        public DataItem()
        {

        }
        public DataItem(string filePath,string fileName,char fileSep, OpUtil.Encoding fileEncoding, OpUtil.ExtType fileType)
        {
            this.filePath = filePath;
            this.fileName = fileName;
            this.fileSep = fileSep;
            this.fileEncoding = fileEncoding;
            this.fileType = fileType;
        }
        public string FilePath { get => filePath; set => filePath = value; }
        public string FileName { get => fileName; set => fileName = value; }
        public char FileSep { get => fileSep; set => fileSep = value; }
        public OpUtil.Encoding FileEncoding { get => fileEncoding; set => fileEncoding = value; }
        public OpUtil.ExtType FileType { get => fileType; set => fileType = value; }
    }
}
