namespace C2.Core.UndoRedo
{

    public class BaseCommand
    {
        // 对应redo操作
        public bool Redo()
        {
            Global.GetTopToolBarControl().InterruptSelectFrame();
            Global.GetTopToolBarControl().ResetStatus();
            bool retCode = this._Redo();
            return retCode;
        }
        // 对应undo操作
        public bool Undo()
        {
            Global.GetTopToolBarControl().InterruptSelectFrame();
            Global.GetTopToolBarControl().ResetStatus();
            bool retCode = this._Undo();
            return retCode;
        }
        public virtual bool _Redo()
        {
            return true;
        }
        public virtual bool _Undo()
        {
            return true;
        }
    }
}
