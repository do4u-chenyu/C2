using C2.Business.Model;
using System.Drawing;


namespace C2.Core.UndoRedo.Command
{
    class ElementMoveCommand : BaseCommand
    {
        Point oldLocation; // 每次回滚时保存当前位置
        private readonly ModelElement element;
        public ElementMoveCommand(ModelElement me, Point oldLocation)
        {
            element = me;
            this.oldLocation = oldLocation;
        }

        public override bool _Redo()
        {
            return Move();
        }

        public override bool _Undo()
        {
            return Move();
        }


        private bool Move()
        {
            oldLocation = element.InnerControl.UndoRedoMoveLocation(oldLocation);
            return true;
        }
    }
}