using Citta_T1.Business.Model;
using Citta_T1.Controls;
using Citta_T1.Controls.Move;
using Citta_T1.Controls.Move.Op;
using System;
using System.Windows.Forms;

namespace Citta_T1.Core.UndoRedo.Command
{
    class RelationAddCommand : ICommand
    {
        private readonly int startID;
        private readonly int endID;
        private readonly ElementType sEleType;
        public RelationAddCommand(int startID, int endID, ElementType sEleType)
        {
            this.startID = startID;
            this.endID = endID;
            this.sEleType = sEleType;
        }
        public bool Undo()
        {
            CanvasPanel cp = Global.GetCanvasPanel();
            foreach (ModelRelation mr in Global.GetCurrentDocument().ModelRelations)
            {
                if (mr.StartID == this.startID && mr.EndID == this.endID)
                {
                    cp.DeleteSelectedLine(mr);
                    break;
                }
            }
            cp.Invalidate(false);
            return true;
            // throw new NotImplementedException();
        }

        public bool Redo()
        {
            ModelDocument doc = Global.GetCurrentDocument();
            MoveBaseControl startC = doc.SearchElementByID(this.startID).InnerControl;
            MoveBaseControl endC = doc.SearchElementByID(this.endID).InnerControl;
            Global.GetCanvasPanel().AddNewRelation(startC, endC, this.sEleType);
            return true;
        }
    }
}
