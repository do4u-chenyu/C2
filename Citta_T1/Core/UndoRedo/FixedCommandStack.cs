using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Citta_T1.Core.UndoRedo
{
    // 固定大小的操作堆栈
    // 要自行判断堆栈是否为空
    class FixedCommandStack
    {
        private int capacity;
        private LinkedList<ICommand> fixedStack;

        public FixedCommandStack(int capacity = 100)
        {
            this.capacity = capacity;
            fixedStack = new LinkedList<ICommand>();
        }

        public int Push(ICommand cmd)
        {
            if (fixedStack.Count >= capacity)
                fixedStack.RemoveLast();
           
            fixedStack.AddFirst(cmd);
            return fixedStack.Count;
        }

        public ICommand Top()
        {
            return fixedStack.First();
        }

        public bool IsEmpty()
        {
            return fixedStack.Count <= 0;
        }

        public ICommand Pop()
        {
            ICommand cmd = Top();
            fixedStack.RemoveFirst();
            return cmd;
        }

        public void Clear()
        {
            this.fixedStack.Clear();
        }
    }
}
