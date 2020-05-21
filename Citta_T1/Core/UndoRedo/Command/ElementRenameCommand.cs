using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Citta_T1.Business.Model;

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
            return true;
        }

        public bool Rollback()
        {
            return true;
        }
    }
}
