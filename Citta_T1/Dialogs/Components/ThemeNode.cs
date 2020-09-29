﻿using System;
using System.Windows.Forms;
using Citta_T1.Model.Styles;

namespace Citta_T1.Dialogs.Components
{
    class ThemeNode : TreeNode
    {
        ChartTheme _Theme;

        public ThemeNode(ChartTheme theme)
        {
            Theme = theme;
        }

        public ChartTheme Theme
        {
            get { return _Theme; }
            set
            {
                if (_Theme != value)
                {
                    _Theme = value;
                    OnThemeChanged();
                }
            }
        }

        private void OnThemeChanged()
        {
            if (Theme != null)
            {
                Text = Theme.Name;
                Theme.NameChanged += new EventHandler(Theme_NameChanged);
            }
        }

        private void Theme_NameChanged(object sender, EventArgs e)
        {
            if (Theme != null)
            {
                Text = Theme.Name;
            }
        }
    }
}
