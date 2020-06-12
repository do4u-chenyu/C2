using System;

namespace Citta_T1.Core.UndoRedo.Command
{

    class RelationDeleteCommand : ICommand
    {
        private readonly int startID;
        private readonly int endID;
        private readonly int pinIndex;
        public RelationDeleteCommand(int startID, int endID, int pinIndex)
        {
            this.startID = startID;
            this.endID = endID;
            this.pinIndex = pinIndex;
        }
        public bool Redo()
        {
            Global.GetCanvasPanel().DeleteRelationByCtrID(this.startID, this.endID, this.pinIndex);
            Global.GetCanvasPanel().Invalidate(false);
            return true;
        }

        public bool Undo()
        {
            Global.GetCanvasPanel().AddNewRelationByCtrID(this.startID, this.endID, this.pinIndex);
            return true;
        }
    }
}
