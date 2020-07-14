namespace Citta_T1.Core.UndoRedo
{

    class BaseCommand
    {
        // 对应redo操作
        public bool Redo()
        {
            Global.GetFlowControl().InterruptSelectFrame();
            return this._Redo();
        }
        // 对应undo操作
        public bool Undo()
        {
            Global.GetFlowControl().InterruptSelectFrame();
            return this._Undo();
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
