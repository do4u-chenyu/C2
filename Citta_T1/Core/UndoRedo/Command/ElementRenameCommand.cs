
using Citta_T1.Business.Model;

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
            oldName = element.InnerControl.UndoRedoChangeTextName(oldName);
            return true;
        }
    }
}
