using Citta_T1.Business.Model;
using Citta_T1.Controls.Move.Dt;
using Citta_T1.Controls.Move.Op;
using Citta_T1.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Citta_T1.Core.UndoRedo.Command
{
    class ElementDeleteCommand : BaseCommand
    {
        private readonly ModelElement me;
        private readonly List<Tuple<int, int, int>> relations;
        private readonly ModelElement ele;
        private Dictionary<int, Point> eleWorldCordDict;
        public ElementDeleteCommand(ModelElement element)
        {
            this.me = element;
        }

        public ElementDeleteCommand(ModelElement ele, List<Tuple<int, int, int>> relations=null, ModelElement connectedEle = null)
        {
            this.me = ele;
            this.relations = relations;
            this.ele = connectedEle;
            this.eleWorldCordDict = ControlUtil.SaveElesWorldCord(new List<ModelElement> { ele, connectedEle });
        }
        public override bool _Redo()
        {
            return DoDelete();
        }

        public override bool _Undo()
        {
            // 正好和ElementAddComman操作相反
            return DoAdd();
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
                    (me.InnerControl as MoveDtControl).UndoRedoAddElement(this.eleWorldCordDict, this.me, this.relations, this.ele);
                    break;
                case ElementType.Operator:
                    (me.InnerControl as MoveOpControl).UndoRedoAddElement(this.eleWorldCordDict, this.me, this.relations, this.ele);
                    break;
                case ElementType.Result:
                default:
                    break;
            }
            return true;
        }
    }
}
