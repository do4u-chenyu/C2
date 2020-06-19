using Citta_T1.Business.Model;
using Citta_T1.Controls.Move.Dt;
using Citta_T1.Controls.Move.Op;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Citta_T1.Core.UndoRedo.Command
{
    class BatchDeleteCommand : ICommand
    {
        private List<ModelElement> mes;
        private List<Tuple<int, int, int>> mrs;
        public BatchDeleteCommand(List<ModelElement> mes, List<Tuple<int, int, int>> mrs)
        {
            this.mes = mes;
            this.mrs = mrs;
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
            Global.GetCanvasPanel().UndoRedoDelSelectedEleAndRel(this.mes, this.mrs);
            Global.GetFlowControl().InterruptSelectFrame();
            return true;
        }
        private bool DoAdd()
        {
            Global.GetCanvasPanel().UndoRedoAddSelectedEleAndRel(this.mes, this.mrs);
            Global.GetFlowControl().InterruptSelectFrame();
            return true;
        }
    }
}
