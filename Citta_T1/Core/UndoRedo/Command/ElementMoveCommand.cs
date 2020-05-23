using Citta_T1.Business.Model;
using Citta_T1.Controls.Move;
using System;
using System.Drawing;


namespace Citta_T1.Core.UndoRedo.Command
{
    class ElementMoveCommand : ICommand
    {
        Point oldLocation;
        private ModelElement element;
        public ElementMoveCommand(ModelElement me, Point oldLocation)
        {
            element = me;
            this.oldLocation = oldLocation;
        }

        public bool Do()
        {
            return DoCommand();
        }

        public bool Rollback()
        {
            return DoCommand();
        }


        private bool DoCommand()
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