using Citta_T1.Business.Model;
using Citta_T1.Controls.Move.Dt;
using Citta_T1.Controls.Move.Op;
using Citta_T1.Controls.Move.Rs;
using System.Drawing;


namespace Citta_T1.Core.UndoRedo.Command
{
    class ElementMoveCommand : ICommand
    {
        Point oldLocation;
        private readonly ModelElement element;
        public ElementMoveCommand(ModelElement me, Point oldLocation)
        {
            element = me;
            this.oldLocation = oldLocation;//有bug,放大缩小时移动会有问题，这地方应该存储世界坐标系坐标
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