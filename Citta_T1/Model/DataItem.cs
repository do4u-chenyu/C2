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
        public DataItem(string filePath,string fileName,string fileSep,string fileEncoding)
        {
            this.filePath = filePath;
            this.fileName = fileName;
            this.fileSep = fileSep;
            this.fileEncoding = fileEncoding;
        }
        public string FilePath { get => filePath; set => filePath = value; }
        public string FileName { get => fileName; set => fileName = value; }
        public string FileSep { get => fileSep; set => fileSep = value; }
        public string FileEncoding { get => fileEncoding; set => fileEncoding = value; }
    }
}
