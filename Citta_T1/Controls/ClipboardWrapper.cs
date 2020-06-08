using Citta_T1.Business.Model;
using Citta_T1.Controls.Interface;
using Citta_T1.Core;
using Citta_T1.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Windows.Forms;

namespace Citta_T1.Controls
{
    class ClipBoardWrapper
    {
        private static LogUtil log = LogUtil.GetInstance("CanvasPanel");
        private List<Control> clipBoardCts;
        public ClipBoardWrapper()
        {
            clipBoardCts = new List<Control>();
        }
        
        public List<Control> ClipBoardCts { get => clipBoardCts; set => clipBoardCts = value; }
        public void ClipBoardPaste()
        {
        }
    }
}
