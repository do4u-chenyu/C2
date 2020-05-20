using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Citta_T1.Core.UndoRedo
{
    class UndoRedoManager
    {
     
        private FixedCommandStack undoStack;
        private FixedCommandStack redoStack;

        private static UndoRedoManager Manager = null;
        private static int UndoRedoManagerCapacity = 100;

        public event UndoRedoEvent.UndoStackEmptyEventHandler UndoStackEmpty;
        public event UndoRedoEvent.UndoStackNotEmptyEventHandler UndoStackNotEmpty;

        public event UndoRedoEvent.RedoStackEmptyEventHandler RedoStackEmpty;
        public event UndoRedoEvent.RedoStackNotEmptyEventHandler RedoStackNotEmpty;

        private UndoRedoManager(int capacity = 100)
        {
            undoStack = new FixedCommandStack(capacity);
            redoStack = new FixedCommandStack(capacity);
        }

        public UndoRedoManager GetInstance()
        {
            if (Manager == null)
                Manager = new UndoRedoManager(UndoRedoManagerCapacity);
            return Manager;
        }

        // 普通执行命令
        public void DoCommand(ICommand cmd)
        {
            cmd.Do();

            if (undoStack.IsEmpty())
                UndoStackNotEmpty?.Invoke();
            undoStack.Push(cmd);
            // 一旦有了新命令,redo栈就可以清空了
            redoStack.Clear();
            RedoStackEmpty?.Invoke();
        }

        public void Undo()
        {
            // 防止cmd为空
            if (undoStack.IsEmpty())
                return;
               
            ICommand cmd = undoStack.Pop();
            if (undoStack.IsEmpty())
                UndoStackEmpty?.Invoke();

            if (redoStack.IsEmpty())
                RedoStackNotEmpty?.Invoke();
            redoStack.Push(cmd);

            // 回滚操作
            cmd.Rollback();
        }

        public void Redo()
        {
            if (redoStack.IsEmpty())
                return;

            ICommand cmd = redoStack.Pop();

            if (redoStack.IsEmpty())
                RedoStackEmpty?.Invoke();

            if (undoStack.IsEmpty())
                UndoStackNotEmpty?.Invoke();
            undoStack.Push(cmd);

            // 重新执行命令
            cmd.Do();

        }



    }
}
