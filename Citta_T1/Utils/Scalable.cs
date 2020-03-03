using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Citta_T1.Utils
{
    interface Scalable
    {
        void SetTag(Control cons);
        void SetControlsBySize(float fx, float fy, Control cons);
    }
}
