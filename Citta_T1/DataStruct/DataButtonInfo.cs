using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Citta_T1.data_struct
{
    class DataButtonInfo: DataObject
    {
        bool isData;
        string index;
        string text;

        public DataButtonInfo(bool id, string i, string t)
        {
            this.index = i;
            this.isData = id;
            this.text = t;
        }
    }
}
