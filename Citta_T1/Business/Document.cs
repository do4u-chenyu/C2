using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Citta_T1.Business
{
    class Document
    {
        private string directory;
        private string modelTitle;
        private List<Control> controls;
        private bool selected;
        private bool dirty;
        public Document(string modelTitle)
        {
            this.directory = @"D:\Citta\User1";
            this.modelTitle = modelTitle;
            controls = new List<Control>();
            selected = false;
        }
    }
}
