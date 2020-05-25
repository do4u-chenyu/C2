
using Citta_T1.Business.Model;
using Citta_T1.Controls.Move.Dt;
using Citta_T1.Controls.Move.Op;
using Citta_T1.Controls.Move.Rs;

namespace Citta_T1.Core.UndoRedo.Command
{
    class ElementRenameCommand : ICommand
    {
        private string oldName;
        private readonly ModelElement element;
        public ElementRenameCommand(ModelElement me, string oldName)
        {
            this.oldName = oldName;
            this.element = me;
        }
        public bool Redo()
        {
            return DoCommand();
        }


        public bool Undo()
        {
            return DoCommand();
        }


        private bool DoCommand()
        {
            switch (element.Type)
            {
                case ElementType.DataSource:
                    oldName = (element.GetControl as MoveDtControl).UndoRedoChangeTextName(oldName);
                    break;
                case ElementType.Operator:
                    oldName = (element.GetControl as MoveOpControl).UndoRedoChangeTextName(oldName);
                    break;
                case ElementType.Result:
                    oldName = (element.GetControl as MoveRsControl).UndoRedoChangeTextName(oldName);
                    break;
                default:
                    break;
            }
            return true;
        }
    }
}
