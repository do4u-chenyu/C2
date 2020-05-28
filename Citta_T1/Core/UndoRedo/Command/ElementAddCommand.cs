using Citta_T1.Business.Model;
using Citta_T1.Controls.Move.Dt;
using Citta_T1.Controls.Move.Op;

namespace Citta_T1.Core.UndoRedo.Command
{
    class ElementAddCommand : ICommand
    {
        private readonly ModelElement me;
        public ElementAddCommand(ModelElement element)
        {
            this.me = element;        
        }
        public bool Redo()
        {
            return DoAdd();
        }

        public bool Undo()
        {
            return DoDelete();
        }

        private bool DoDelete()
        {
            switch (me.Type)
            {
                case ElementType.DataSource:
                    (me.InnerControl as MoveDtControl).UndoRedoDeleteElement();
                    break;
                case ElementType.Operator:
                    (me.InnerControl as MoveOpControl).UndoRedoDeleteElement();
                    break;
                case ElementType.Result:
                default:
                    break;
            }
            return true;
        }
        private bool DoAdd()
        {
            switch (me.Type)
            {
                case ElementType.DataSource:
                    (me.InnerControl as MoveDtControl).UndoRedoAddElement(me);
                    break;
                case ElementType.Operator:
                    (me.InnerControl as MoveOpControl).UndoRedoAddElement(me);
                    break;
                case ElementType.Result:
                default:
                    break;
            }
            return true;
        }
    }
}