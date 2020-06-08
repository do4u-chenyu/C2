using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Citta_T1.Controls
{
    class ClipBoardWrapper
    {
        private List<Control> clipBoardCts;
        public ClipBoardWrapper()
        {
            clipBoardCts = new List<Control>();
        }
        
        public List<Control> ClipBoardCts { get => clipBoardCts; set => clipBoardCts = value; }
        
    }
}
