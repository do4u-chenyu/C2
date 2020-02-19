using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Citta_T1
{
    public class Data
    {
        public string dataName;
        public string filePath;
        public string content;
        public Data(string n, string fp, string c)
        {
            this.dataName = n;
            this.filePath = fp;
            this.content = c; 
        }
    }

}
