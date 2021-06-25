﻿using System;
using System.Collections.Generic;
using System.Text;

namespace C2.Core
{
    delegate void ShortcutKeyEventHandler();

    class ShortcutKeyAction
    {
        public ShortcutKeyEventHandler Handler { get; private set; }

        public ShortcutKey Key { get; private set; }

        public ShortcutKeyAction(ShortcutKey key, ShortcutKeyEventHandler handler)
        {
            Key = key;
            Handler = handler;
        }
    }
}
