using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Citta_T1.Controls.Interface
{
    interface IMoveControl
    {
        void UpdateLineWhenMoving();
        void SaveStartLines(int line_index);
        void SaveEndLines(int line_index);
    }
}
