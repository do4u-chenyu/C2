using Citta_T1.Business.Model.World;
using System.Collections.Generic;
using System.Drawing;


namespace Citta_T1.Core.UndoRedo.Command
{
    class BatchMoveCommand : BaseCommand
    {
        private Dictionary<int, Point> idPtsDict;
        private WorldMap worldMap;

        public BatchMoveCommand(Dictionary<int, Point> idPtsDict)
        {
            this.idPtsDict = idPtsDict;
            this.worldMap = new WorldMap();
        }
        public override bool _Redo()
        {
            return DoMove();
        }

        public override bool _Undo()
        {
            return DoMove();
        }
        private bool DoMove()
        {
            Global.GetCanvasPanel().UndoRedoMoveEles(this.idPtsDict);
            Global.GetNaviViewControl().UpdateNaviView();
            return true;
        }
    }
}
