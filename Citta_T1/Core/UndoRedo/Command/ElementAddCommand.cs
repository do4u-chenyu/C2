using Citta_T1.Business.Model;
using Citta_T1.Controls.Move.Dt;
using Citta_T1.Controls.Move.Op;
using Citta_T1.Controls.Move.Rs;
using System;
using System.Collections.Generic;

namespace Citta_T1.Core.UndoRedo.Command
{
    class ElementAddCommand : ICommand
    {
        private readonly ModelElement me;
        private readonly List<Tuple<int, int, int>> relations;
        private readonly ModelElement ele;
        private readonly ElementStatus status;
        private readonly Dictionary<string, string> opOptDict;
        public ElementAddCommand(ModelElement element)
        {
            this.me = element;
        }
        public ElementAddCommand(ModelElement me, List<Tuple<int, int, int>> relations = null, ModelElement connectedEle = null)
        {
            this.me = me;
            this.relations = relations;
            this.ele = connectedEle;
            if (this.ele != null && this.ele.Type == ElementType.Operator)
            {
                this.status = connectedEle.Status;
                this.opOptDict = (connectedEle.InnerControl as MoveOpControl).Option.OptionDict;
            }
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
            switch (me.Type)
            {
                case ElementType.DataSource:
                    (me.InnerControl as MoveDtControl).UndoRedoDeleteElement(this.me, this.relations, this.ele);
                    break;
                case ElementType.Operator:
                    (me.InnerControl as MoveOpControl).UndoRedoDeleteElement(this.me, this.relations, this.ele);
                    break;
                case ElementType.Result:
                    (me.InnerControl as MoveRsControl).UndoRedoDeleteElement(this.me, this.relations, this.ele);
                    break;
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
                    (me.InnerControl as MoveDtControl).UndoRedoAddElement(this.me, this.relations, this.ele);
                    break;
                case ElementType.Operator:
                    (me.InnerControl as MoveOpControl).UndoRedoAddElement(this.me, this.relations, this.ele);
                    break;
                case ElementType.Result:
                    (me.InnerControl as MoveRsControl).UndoRedoAddElement(this.me, this.relations, this.ele, this.status, this.opOptDict);
                    break;
                default:
                    break;
            }
            return true;
        }
    }
}