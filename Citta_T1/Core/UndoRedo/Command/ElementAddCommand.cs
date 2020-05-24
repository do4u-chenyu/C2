using Citta_T1.Business.Model;
using Citta_T1.Controls.Move.Dt;
using Citta_T1.Controls.Move.Op;

namespace Citta_T1.Core.UndoRedo.Command
{
    class ElementAddCommand : ICommand
    {
        //private ModelDocument document;
        private readonly ModelElement element;
        public ElementAddCommand(ModelElement element)
        {
            this.element = element;        
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
            switch (element.Type)
            {
                case ElementType.DataSource:
                    (element.GetControl as MoveDtControl).UndoRedoDeleteElement();
                    break;
                case ElementType.Operator:
                    (element.GetControl as MoveOpControl).UndoRedoDeleteElement();
                    break;
                case ElementType.Result:
                default:
                    break;
            }
            return true;
        }
        private bool DoAdd()
        {
            switch (element.Type)
            {
                case ElementType.DataSource:
                    (element.GetControl as MoveDtControl).UndoRedoAddElement();
                    break;
                case ElementType.Operator:
                    (element.GetControl as MoveOpControl).UndoRedoAddElement();
                    break;
                case ElementType.Result:
                default:
                    break;
            }
            return true;
        }
    }
}