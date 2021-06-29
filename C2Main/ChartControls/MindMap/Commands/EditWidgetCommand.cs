﻿using System;
using System.Collections.Generic;
using System.Text;
using C2.Model.Widgets;
using System.Windows.Forms;

namespace C2.Controls.MapViews
{
    class EditWidgetCommand : Command
    {
        private Widget OriginalWidget;
        private Widget NewValue;
        private Widget OldCopy;

        public EditWidgetCommand(Widget oriWidget, Widget newValue)
        {
            if (oriWidget == null || newValue == null)
                throw new ArgumentNullException();

            OriginalWidget = oriWidget;
            NewValue = newValue;
        }

        public override string Name
        {
            get { return "Edit Widget"; }
        }

        public override bool Rollback()
        {
            if (OldCopy != null && OriginalWidget != null)
            {
                OldCopy.CopyTo(OriginalWidget);
                return true;
            }
            else
            {
                return false;
            }
        }
        public override bool Redo()
        {
            return Execute();
        }
        public override bool Execute()
        {
            if (OriginalWidget == null || NewValue == null)
                return false;

            OldCopy = OriginalWidget.Clone() as Widget;
            NewValue.CopyTo(OriginalWidget);
            return true;
        }
    }
}
