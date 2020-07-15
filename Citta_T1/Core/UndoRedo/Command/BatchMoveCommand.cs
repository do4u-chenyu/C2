using Citta_T1.Business.Model.World;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public BatchMoveCommand(Dictionary<int, Point> idPtsDict, WorldMap wm)
        {
            this.idPtsDict = idPtsDict;          // 世界坐标
            this.worldMap = wm;                  // 世界坐标系
        }
        public override bool _Redo()
        {
            if (this.worldMap.MapOrigin.IsEmpty)
                return DoMove();
            else
                return DoMove(this.worldMap);
        }

        public override bool _Undo()
        {
            if (this.worldMap.MapOrigin.IsEmpty)
                return DoMove();
            else
                return DoMove(this.worldMap);
        }
        private bool DoMove(WorldMap wm)
        {
            // TODO 有问题
            Global.GetCanvasPanel().UndoRedoMoveEles(this.idPtsDict, wm);
            Global.GetNaviViewControl().UpdateNaviView();
            return true;
        }
        private bool DoMove()
        {
            Global.GetCanvasPanel().UndoRedoMoveEles(this.idPtsDict);
            Global.GetNaviViewControl().UpdateNaviView();
            return true;
        }
    }
}
