﻿using C2.Business.Model;
using System.Collections.Generic;

namespace C2.Core.UndoRedo
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
    public class UndoRedoManager
    {
        // 每个文档对应一个撤回/重做栈
        private Dictionary<ModelDocument, UndoRedoStack> undoRedoDict;

        public event UndoRedoEvent.UndoStackEmptyEventHandler UndoStackEmpty;
        public event UndoRedoEvent.UndoStackNotEmptyEventHandler UndoStackNotEmpty;

        public event UndoRedoEvent.RedoStackEmptyEventHandler RedoStackEmpty;
        public event UndoRedoEvent.RedoStackNotEmptyEventHandler RedoStackNotEmpty;

        public  UndoRedoManager()
        {
            undoRedoDict = new Dictionary<ModelDocument, UndoRedoStack>();
        }

        public static UndoRedoManager GetInstance()
        {
            return Global.GetCanvasForm().UndoRedoManager;
        }

        // 后面文档切换时,更新 TopToolBarControl用
        public bool IsUndoStackEmpty(ModelDocument md)
        {
            return !undoRedoDict.ContainsKey(md) || undoRedoDict[md].UndoStack.IsEmpty();
        }
        public bool IsRedoStackEmpty(ModelDocument md)
        {
            return !undoRedoDict.ContainsKey(md) || undoRedoDict[md].RedoStack.IsEmpty();
        }

        // 普通执行命令
        public void PushCommand(ModelDocument md, BaseCommand cmd)
        {
            if (md == null || cmd == null)
                return;

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
            if (md == null || !undoRedoDict.ContainsKey(md) || undoRedoDict[md].UndoStack.IsEmpty())
                return;

            // 回滚操作
            BaseCommand cmd = undoRedoDict[md].UndoStack.Pop();
            cmd.Undo();

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

            BaseCommand cmd = undoRedoDict[md].RedoStack.Pop();

            // 重新执行命令
            cmd.Redo();

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
