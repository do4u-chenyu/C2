using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Citta_T1.Core.UndoRedo
{
    
    interface ICommand
    {
        // 对应redo操作
        bool Redo();
        // 对应undo操作
        bool Undo();
    }
}
