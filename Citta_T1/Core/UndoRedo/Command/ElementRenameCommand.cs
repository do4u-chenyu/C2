
using Citta_T1.Business.Model;

namespace Citta_T1.Core.UndoRedo.Command
{
    class ElementRenameCommand : BaseCommand
    {
        private string oldName;
        private readonly ModelElement element;
        public ElementRenameCommand(ModelElement me, string oldName)
        {
            this.oldName = oldName;
            this.element = me;
        }
        public override bool _Redo()
        {
            return DoCommand();
        }


        public override bool _Undo()
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
