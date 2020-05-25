using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Citta_T1.Core.UndoRedo.Command
{
    // 测试Command,仅作测试UndoRedoManager功能使用
    class TestCommand : ICommand
    {
        public bool Redo()
        {
            System.Console.WriteLine("TestCommand: Do");
            return true;
        }

        public bool Undo()
        {
            System.Console.WriteLine("TestCommand: Rollback");
            return true;
        }
    }
}