using System;

namespace Citta_T1.Core.UndoRedo.Command
{

    class RelationDeleteCommand : ICommand
    {
        private readonly int startID;
        private readonly int endID;
        public RelationDeleteCommand(int startID, int endID)
        {
            this.startID = startID;
            this.endID = endID;
        }
        public bool Redo()
        {
            Global.GetCanvasPanel().DeleteRelationByCtrID(this.startID, this.endID);
            Global.GetCanvasPanel().Invalidate(false);
            return true;
        }

        public bool Undo()
        {
            Global.GetCanvasPanel().AddNewRelationByCtrID(this.startID, this.endID);
            return true;
        }
    }
}
