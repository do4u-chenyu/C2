using Citta_T1.Business.Model;
using Citta_T1.Controls.Move.Dt;
using Citta_T1.Controls.Move.Op;
using Citta_T1.Controls.Move.Rs;
using Citta_T1.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Citta_T1.Core.UndoRedo.Command
{
    class ElementAddCommand : BaseCommand
    {
        private ModelElement me;
        private List<Tuple<int, int, int>> relations;
        private ModelElement ele;
        private ElementStatus status;
        private Dictionary<string, string> opOptDict;
        private Dictionary<int, Point> eleWorldCordDict;
        public ElementAddCommand(ModelElement element)
        {
            this.me = element;
            this.eleWorldCordDict = ControlUtil.SaveElesWorldCord(new List<ModelElement> { this.me });
        }
        public ElementAddCommand(ModelElement me, List<Tuple<int, int, int>> relations = null, ModelElement connectedEle = null)
        {
            this.me = me;
            this.relations = relations;
            this.ele = connectedEle;
            if (this.ele != null && this.ele.Type == ElementType.Operator)
            {
                this.status = connectedEle.Status;
                this.opOptDict = new Dictionary<string, string>((connectedEle.InnerControl as MoveOpControl).Option.OptionDict);
            }
            this.eleWorldCordDict = ControlUtil.SaveElesWorldCord(new List<ModelElement> { this.me, this.ele });
        }
        public override bool _Redo()
        {
            return DoAdd();
        }

        public override bool _Undo()
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
            Global.GetCanvasPanel().Invalidate(false);
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
                    (me.InnerControl as MoveRsControl).UndoRedoAddElement(this.eleWorldCordDict, this.me, this.relations, this.ele, this.status, this.opOptDict);
                    break;
                default:
                    break;
            }
            Global.GetCanvasPanel().Invalidate(false);
            return true;
        }
    }
}