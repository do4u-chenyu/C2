using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Citta_T1.Core.UndoRedo.Command
{
    class BatchMoveCommand : ICommand
    {
        private Dictionary<int, Point> idPtsDict;

        public BatchMoveCommand(Dictionary<int, Point> idPtsDict)
        {
            this.idPtsDict = idPtsDict;
        }
        public bool Redo()
        {
            return DoDelete();
        }

        public bool Undo()
        {
            // 正好和ElementAddComman操作相反
            return DoAdd();
        }


        private bool DoDelete()
        {
            Global.GetCanvasPanel().UndoRedoMoveEles(this.idPtsDict);
            Global.GetFlowControl().InterruptSelectFrame();
            return true;
        }
        private bool DoAdd()
        {
            Global.GetCanvasPanel().UndoRedoMoveEles(this.idPtsDict);
            Global.GetFlowControl().InterruptSelectFrame();
            return true;
        }
    }
}
