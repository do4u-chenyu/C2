using System.Collections.Generic;
using System.Linq;

namespace C2.Core.UndoRedo
{
    // 固定大小的操作堆栈
    // 要自行判断堆栈是否为空
    class FixedCommandStack
    {
        private int capacity;
        private LinkedList<BaseCommand> fixedStack;

        public FixedCommandStack(int capacity = 100)
        {
            this.capacity = capacity;
            fixedStack = new LinkedList<BaseCommand>();
        }

        public int Push(BaseCommand cmd)
        {
            if (fixedStack.Count >= capacity)
                fixedStack.RemoveLast();

            fixedStack.AddFirst(cmd);
            return fixedStack.Count;
        }

        public BaseCommand Top()
        {
            return fixedStack.First();
        }

        public bool IsEmpty()
        {
            return fixedStack.Count <= 0;
        }

        public BaseCommand Pop()
        {
            BaseCommand cmd = Top();
            fixedStack.RemoveFirst();
            return cmd;
        }

        public void Clear()
        {
            this.fixedStack.Clear();
        }
    }
}
