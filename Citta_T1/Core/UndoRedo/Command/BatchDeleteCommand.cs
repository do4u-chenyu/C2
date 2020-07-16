using Citta_T1.Business.Model;
using Citta_T1.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;


namespace Citta_T1.Core.UndoRedo.Command
{
    class BatchDeleteCommand : BaseCommand
    {
        private List<ModelElement> mes;
        private List<Tuple<int, int, int>> mrs;
        private Dictionary<int, Point> eleWorldCordDict;
        public BatchDeleteCommand(List<ModelElement> mes, List<Tuple<int, int, int>> mrs)
        {
            this.mes = mes;
            this.mrs = mrs;
            this.eleWorldCordDict = ControlUtil.SaveElesWorldCord(mes);
        }

        public override bool _Redo()
        {
            return DoDelete();
        }

        public override bool _Undo()
        {
            // 正好和ElementAddComman操作相反
            return DoAdd();
        }


        private bool DoDelete()
        {
            Global.GetCanvasPanel().UndoRedoDelSelectedEles(this.mes, this.mrs);
            Global.GetFlowControl().InterruptSelectFrame();
            return true;
        }
        private bool DoAdd()
        {
            Global.GetCanvasPanel().UndoRedoAddSelectedEles(this.eleWorldCordDict, this.mes, this.mrs);
            Global.GetFlowControl().InterruptSelectFrame();
            return true;
        }
    }
}
