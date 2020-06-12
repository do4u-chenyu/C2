using Citta_T1.Business.Model;
using Citta_T1.Controls.Move.Dt;
using Citta_T1.Controls.Move.Op;
using System;
using System.Collections.Generic;

namespace Citta_T1.Core.UndoRedo.Command
{
    class ElementDeleteCommand : ICommand
    {
        private readonly ModelElement me;
        private readonly List<Tuple<int, int, int>> relations;
        private readonly ModelElement rsEle;
        public ElementDeleteCommand(ModelElement element)
        {
            this.me = element;
        }
        public ElementDeleteCommand(ModelElement ele, List<Tuple<int, int, int>> relations=null, ModelElement rsEle = null)
        {
            this.me = ele;
            this.relations = relations;
            this.rsEle = rsEle;
        }
        public bool Redo()
        {
            return DoDelete();
        }

        public bool Undo()
        {
            // 正好和ElementAddComman操作相反
            return DoAdd();
        }


        private bool DoDelete()
        {
            switch (me.Type)
            {
                case ElementType.DataSource:
                    (me.InnerControl as MoveDtControl).UndoRedoDeleteElement(this.relations);
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
                    (me.InnerControl as MoveDtControl).UndoRedoAddElement(me, this.relations);
                    break;
                case ElementType.Operator:
                    (me.InnerControl as MoveOpControl).UndoRedoAddElement(me, this.relations, this.rsEle);
                    break;
                case ElementType.Result:
                default:
                    break;
            }
            return true;
        }
    }
}
