using Citta_T1.Business.Model;
using Citta_T1.Business.Model.World;
using Citta_T1.Controls.Move.Dt;
using Citta_T1.Controls.Move.Op;
using System;
using System.Collections.Generic;

namespace Citta_T1.Core.UndoRedo.Command
{
    class ElementDeleteCommand : BaseCommand
    {
        private readonly ModelElement me;
        private readonly List<Tuple<int, int, int>> relations;
        private readonly ModelElement ele;
        public ElementDeleteCommand(WorldMap wm, ModelElement element)
        {
            this.me = element;
            this.me.WorldCord = wm.ScreenToWorld(this.me.Location, true);
        }

        public ElementDeleteCommand(WorldMap wm, ModelElement ele, List<Tuple<int, int, int>> relations=null, ModelElement connectedEle = null)
        {
            this.me = ele;
            this.relations = relations;
            this.ele = connectedEle;
            this.me.WorldCord = wm.ScreenToWorld(this.me.Location, true);
            if (this.ele != null)
                this.ele.WorldCord = wm.ScreenToWorld(this.ele.Location, true);
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
                    (me.InnerControl as MoveDtControl).UndoRedoAddElement(this.me, this.relations, this.ele);
                    break;
                case ElementType.Operator:
                    (me.InnerControl as MoveOpControl).UndoRedoAddElement(this.me, this.relations, this.ele);
                    break;
                case ElementType.Result:
                default:
                    break;
            }
            return true;
        }
    }
}
