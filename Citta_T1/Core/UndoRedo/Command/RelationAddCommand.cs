using Citta_T1.Controls.Move;
using System;

namespace Citta_T1.Core.UndoRedo.Command
{
    class RelationAddCommand : ICommand
    {
        private readonly MoveBaseControl startC;
        private readonly MoveBaseControl endC;
        public RelationAddCommand(MoveBaseControl startC, MoveBaseControl endC)
        {
            this.startC = startC;
            this.endC = endC;
        }
        public bool Redo()
        {
            throw new NotImplementedException();
        }

        public bool Undo()
        {
            throw new NotImplementedException();
        }
    }
}
