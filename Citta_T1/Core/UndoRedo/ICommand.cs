using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Citta_T1.Core.UndoRedo
{
    // 正在施工中
    interface ICommand
    {
        bool Undo();
        bool Redo();
    }
}
