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
        private Point worldMapOrigin;

        public BatchMoveCommand(Dictionary<int, Point> idPtsDict)
        {
            this.idPtsDict = idPtsDict;
            this.worldMapOrigin = Point.Empty;
        }
        public BatchMoveCommand(Dictionary<int, Point> idPtsDict, Point worldMapOrigin)
        {
            this.idPtsDict = idPtsDict;
            this.worldMapOrigin = worldMapOrigin;
        }
        public bool Redo()
        {
            if (this.worldMapOrigin.IsEmpty)
                return DoMove();
            else
                return DoMove(new Point(0, 0));
        }

        public bool Undo()
        {
            if (this.worldMapOrigin.IsEmpty)
                return DoMove();
            else
                return DoMove(this.worldMapOrigin);
        }
        private bool DoMove(Point wmo)
        {
            Global.GetCanvasPanel().UndoRedoMoveEles(this.idPtsDict, wmo);
            Global.GetFlowControl().InterruptSelectFrame();
            Global.GetNaviViewControl().UpdateNaviView();
            return true;
        }
        private bool DoMove()
        {
            Global.GetCanvasPanel().UndoRedoMoveEles(this.idPtsDict);
            Global.GetFlowControl().InterruptSelectFrame();
            Global.GetNaviViewControl().UpdateNaviView();
            return true;
        }
    }
}
