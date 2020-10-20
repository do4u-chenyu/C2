using System;

namespace C2.Core.UndoRedo.Command
{

    class RelationDeleteCommand : BaseCommand
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
        public override bool _Redo()
        {
            Global.GetCanvasPanel().DeleteRelationByCtrID(this.startID, this.endID, this.pinIndex);
            Global.GetCanvasPanel().Invalidate(false);
            return true;
        }

        public override bool _Undo()
        {
            Global.GetCanvasPanel().AddNewRelationByCtrID(this.startID, this.endID, this.pinIndex);
            return true;
        }
    }
}
