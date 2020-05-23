using Citta_T1.Business.Model;
using Citta_T1.Controls.Move;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Citta_T1.Core.UndoRedo.Command
{
    class ElementDeleteCommand : ICommand
    {
        private readonly ModelElement element;
        public ElementDeleteCommand(ModelElement element)
        {
            this.element = element;
        }
        public bool Do()
        {
            return DoDelete();
        }

        public bool Rollback()
        {
            // 正好和ElementAddComman操作相反
            return DoAdd();
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
