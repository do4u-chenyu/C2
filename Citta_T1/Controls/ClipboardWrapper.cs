
using C2.Controls.Move.Dt;
using C2.Controls.Move.Op;
using C2.Core;
using C2.Utils;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace C2.Controls
{
    class ClipBoardWrapper
    {
        private List<Control> clipBoardCts;
        public ClipBoardWrapper()
        {
            clipBoardCts = new List<Control>();
        }
        
        public List<Control> ClipBoardCts { get => clipBoardCts; set => clipBoardCts = value; }
        public void ClipBoardPaste()
        {
            foreach(Control ct in clipBoardCts)
            {
                string description = ct.Controls["textBox"].Text;
                int sizeLevel = Global.GetCurrentDocument().WorldMap.SizeLevel;  
                Point location = new Point(ct.Left + ct.Width/2,ct.Top + ct.Height/2);
                if (ct.Name.Equals("MoveOpControl"))
                {
                    string subTypeName = (ct as MoveOpControl).SubTypeName;
                    Global.GetCanvasPanel().AddNewOperator(sizeLevel, description, subTypeName, location);
                }
                if (ct.Name.Equals("MoveDtControl"))
                {
                    string path = (ct as MoveDtControl).FullFilePath;
                    char separator = (ct as MoveDtControl).Separator;
                    OpUtil.Encoding encoding = (ct as MoveDtControl).Encoding;
                    OpUtil.ExtType extType = (ct as MoveDtControl).ExtType;
                    Global.GetCanvasPanel().AddNewDataSource(path, sizeLevel, description,
                        location, separator, extType, encoding);
                }
            }
            clipBoardCts.Clear();
            //Global.GetCanvasPanel().FrameWrapper.InitFrame();
            //Global.GetFlowControl().ResetStatus();
        }
    }
}
