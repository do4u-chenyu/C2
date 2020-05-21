using Citta_T1.Business.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Citta_T1.Core.UndoRedo
{
    class UndoRedoStack
    {
        private FixedCommandStack undoStack;
        private FixedCommandStack redoStack;

        public FixedCommandStack UndoStack { get => undoStack; }
        public FixedCommandStack RedoStack { get => redoStack; }

        public UndoRedoStack(int capacity = 100)
        {
            undoStack = new FixedCommandStack(capacity);
            redoStack = new FixedCommandStack(capacity);
        }
    }
    class UndoRedoManager
    {
        // 每个文档对应一个撤回/重做栈
        private Dictionary<ModelDocument, UndoRedoStack> undoRedoDict;

        private static UndoRedoManager Manager = null;

        public event UndoRedoEvent.UndoStackEmptyEventHandler UndoStackEmpty;
        public event UndoRedoEvent.UndoStackNotEmptyEventHandler UndoStackNotEmpty;

        public event UndoRedoEvent.RedoStackEmptyEventHandler RedoStackEmpty;
        public event UndoRedoEvent.RedoStackNotEmptyEventHandler RedoStackNotEmpty;

        private UndoRedoManager()
        {
            undoRedoDict = new Dictionary<ModelDocument, UndoRedoStack>();
        }

        public static UndoRedoManager GetInstance()
        {
            if (Manager == null)
                Manager = new UndoRedoManager();
            return Manager;
        }

        // 后面文档切换时,更新 TopToolBarControl用
        public bool IsUndoStackEmpty(ModelDocument md)
        {
            return undoRedoDict.ContainsKey(md) && undoRedoDict[md].UndoStack.IsEmpty();
        }
        public bool IsRedoStackEmpty(ModelDocument md)
        {
            return undoRedoDict.ContainsKey(md) && undoRedoDict[md].RedoStack.IsEmpty();
        }

        // 普通执行命令
        public void DoCommand(ModelDocument md, ICommand cmd)
        {
            if (cmd == null)
                return;

            cmd.Do();

            if (!undoRedoDict.ContainsKey(md))
                undoRedoDict[md] = new UndoRedoStack();

            if (undoRedoDict[md].UndoStack.IsEmpty())
                UndoStackNotEmpty?.Invoke();
            undoRedoDict[md].UndoStack.Push(cmd);
            // 一旦有了新命令,redo栈就可以清空了
            undoRedoDict[md].RedoStack.Clear();
            RedoStackEmpty?.Invoke();
        }

        public void Undo(ModelDocument md)
        {
            // 防止cmd为空
            if (!undoRedoDict.ContainsKey(md) || undoRedoDict[md].UndoStack.IsEmpty())
                return;

            // 回滚操作
            ICommand cmd = undoRedoDict[md].UndoStack.Pop();
            cmd.Rollback();

            if (undoRedoDict[md].UndoStack.IsEmpty())
                UndoStackEmpty?.Invoke();

            if (undoRedoDict[md].RedoStack.IsEmpty())
                RedoStackNotEmpty?.Invoke();
            undoRedoDict[md].RedoStack.Push(cmd);
        }

        public void Redo(ModelDocument md)
        {
            if (!undoRedoDict.ContainsKey(md) || undoRedoDict[md].RedoStack.IsEmpty())
                return;

            ICommand cmd = undoRedoDict[md].RedoStack.Pop();

            // 重新执行命令
            cmd.Do();

            if (undoRedoDict[md].RedoStack.IsEmpty())
                RedoStackEmpty?.Invoke();

            if (undoRedoDict[md].UndoStack.IsEmpty())
                UndoStackNotEmpty?.Invoke();
            undoRedoDict[md].UndoStack.Push(cmd);


        }

        public void Remove(ModelDocument md)
        {
            if (!undoRedoDict.ContainsKey(md))
                return;

            undoRedoDict[md].RedoStack.Clear();
            undoRedoDict[md].UndoStack.Clear();
            undoRedoDict.Remove(md);    
        }
    }
}
