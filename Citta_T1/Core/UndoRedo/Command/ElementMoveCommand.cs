using Citta_T1.Business.Model;
using Citta_T1.Controls.Move.Dt;
using Citta_T1.Controls.Move.Op;
using Citta_T1.Controls.Move.Rs;
using System.Drawing;


namespace Citta_T1.Core.UndoRedo.Command
{
    class ElementMoveCommand : ICommand
    {
        Point oldLocation; // 每次回滚时保存当前位置
        private readonly ModelElement element;
        public ElementMoveCommand(ModelElement me, Point oldLocation)
        {
            element = me;
            this.oldLocation = oldLocation;
        }

        public bool Redo()
        {
            return Move();
        }

        public bool Undo()
        {
            return Move();
        }


        private bool Move()
        {
            oldLocation = element.InnerControl.UndoRedoMoveLocation(oldLocation);
            return true;
        }
    }
}