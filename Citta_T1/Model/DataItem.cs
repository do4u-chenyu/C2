using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2.Model
{
    class DataItem
    {
        string filePath;
        string fileName;
        string fileSep;
        string fileEncoding;
        string fileType;
        public DataItem()
        {

        }
        public DataItem(string filePath,string fileName,string fileSep,string fileEncoding,string fileType)
        {
            this.filePath = filePath;
            this.fileName = fileName;
            this.fileSep = fileSep;
            this.fileEncoding = fileEncoding;
            this.fileType = fileType;
        }
        public string FilePath { get => filePath; set => filePath = value; }
        public string FileName { get => fileName; set => fileName = value; }
        public string FileSep { get => fileSep; set => fileSep = value; }
        public string FileEncoding { get => fileEncoding; set => fileEncoding = value; }
        public string FileType { get => fileType; set => fileType = value; }
    }
}
