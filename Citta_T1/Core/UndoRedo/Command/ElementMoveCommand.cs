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
            switch (element.Type)
            {
                case ElementType.DataSource:
                    oldLocation = (element.GetControl as MoveDtControl).UndoRedoMoveLocation(oldLocation);
                    break;
                case ElementType.Operator:
                    oldLocation = (element.GetControl as MoveOpControl).UndoRedoMoveLocation(oldLocation);
                    break;
                case ElementType.Result:
                    oldLocation = (element.GetControl as MoveRsControl).UndoRedoMoveLocation(oldLocation);
                    break;
                default:
                    break;
            }
            return true;
        }
    }
}