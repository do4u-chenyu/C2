using Citta_T1.Business.Model;
using Citta_T1.Controls;
using Citta_T1.Controls.Move;
using Citta_T1.Controls.Move.Op;
using System;
using System.Windows.Forms;

namespace Citta_T1.Core.UndoRedo.Command
{
    class RelationAddCommand : ICommand
    {
        private readonly int startID;
        private readonly int endID;
        private readonly int pinIndex;
        public RelationAddCommand(int startID, int endID, int pinIndex)
        {
            this.startID = startID;
            this.endID = endID;
            this.pinIndex = pinIndex;
        }
        public bool Undo()
        {
            Global.GetCanvasPanel().DeleteRelationByCtrID(this.startID, this.endID, this.pinIndex);
            Global.GetCanvasPanel().Invalidate(false);
            return true;
        }

        public bool Redo()
        {
            Global.GetCanvasPanel().AddNewRelationByCtrID(this.startID, this.endID, this.pinIndex);
            return true;
        }
    }
}
