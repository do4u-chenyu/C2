namespace Citta_T1.Core.UndoRedo
{

    class BaseCommand
    {
        // 对应redo操作
        public bool Redo()
        {
            Global.GetFlowControl().InterruptSelectFrame();
            Global.GetFlowControl().ResetStatus();
            bool retCode = this._Redo();
            return retCode;
        }
        // 对应undo操作
        public bool Undo()
        {
            Global.GetFlowControl().InterruptSelectFrame();
            Global.GetFlowControl().ResetStatus();
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
