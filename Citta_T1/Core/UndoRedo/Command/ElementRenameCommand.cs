using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Citta_T1.Business.Model;
using Citta_T1.Controls.Move;

namespace Citta_T1.Core.UndoRedo.Command
{
    class ElementRenameCommand : ICommand
    {
        private string oldName;
        private ModelElement element;
        public ElementRenameCommand(ModelElement me, string oldName)
        {
            this.oldName = oldName;
            this.element = me;
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
