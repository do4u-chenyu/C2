namespace C2.Core.UndoRedo
{
    class UndoRedoEvent
    {
        // Undo堆栈不空=>为空时发生,用来通知界面更新UI状态
        public delegate void UndoStackEmptyEventHandler();
        // Undo堆栈空=>不为空时发生,用来通知界面更新UI状态
        public delegate void UndoStackNotEmptyEventHandler();

        // redo堆栈不空=>为空时发生,用来通知界面更新UI状态
        public delegate void RedoStackEmptyEventHandler();
        // redo堆栈空=>不为空时发生,用来通知界面更新UI状态
        public delegate void RedoStackNotEmptyEventHandler();
    }
}