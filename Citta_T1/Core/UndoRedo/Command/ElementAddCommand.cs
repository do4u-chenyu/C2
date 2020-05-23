using Citta_T1.Business.Model;
using Citta_T1.Controls.Move;

namespace Citta_T1.Core.UndoRedo.Command
{
    class ElementAddCommand : ICommand
    {
        //private ModelDocument document;
        private ModelElement element;
        public ElementAddCommand(ModelElement element)
        {
            this.element = element;        
        }
        public bool Do()
        {
            return DoAdd();
        }

        public bool Rollback()
        {
            return DoDelete();
        }

        private bool DoDelete()
        {
            switch (element.Type)
            {
                case ElementType.DataSource:
                    (element.GetControl as MoveDtControl).UndoRedoDelete();
                    break;
                case ElementType.Operator:
                    (element.GetControl as MoveOpControl).UndoRedoDelete();
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
                    (element.GetControl as MoveDtControl).UndoRedoAdd();
                    break;
                case ElementType.Operator:
                    (element.GetControl as MoveOpControl).UndoRedoAdd();
                    break;
                case ElementType.Result:
                default:
                    break;
            }
            return true;
        }
    }
}