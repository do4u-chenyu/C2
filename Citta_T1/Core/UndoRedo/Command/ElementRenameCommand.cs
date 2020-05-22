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
            switch (element.Type)
            {
                case ElementType.DataSource:
                    oldName = (element.GetControl as MoveDtControl).ChangeTextName(oldName);
                    break;
                case ElementType.Operator:
                    oldName = (element.GetControl as MoveOpControl).ChangeTextName(oldName);
                    break;
                case ElementType.Result:
                    oldName = (element.GetControl as MoveRsControl).ChangeTextName(oldName);
                    break;
                default:
                    break;
            }
            return true;
        }

        public bool Rollback()
        {
            switch (element.Type)
            {
                case ElementType.DataSource:
                    oldName = (element.GetControl as MoveDtControl).ChangeTextName(oldName);
                    break;
                case ElementType.Operator:
                    oldName = (element.GetControl as MoveOpControl).ChangeTextName(oldName);
                    break;
                case ElementType.Result:
                    oldName = (element.GetControl as MoveRsControl).ChangeTextName(oldName);
                    break;
                default:
                    break;
            }
            return true;
        }
    }
}
