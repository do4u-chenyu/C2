using System;
using System.Windows.Forms;
using Citta_T1.Core;
using Citta_T1.Globalization;
using Citta_T1.Model.Styles;

namespace Citta_T1.Dialogs.Components
{
    class ThemeFolderNode : TreeNode
    {
        private ChartThemeFolder _ThemeFolder;

        public ThemeFolderNode(ChartThemeFolder themeFolder)
        {
            ThemeFolder = themeFolder;
        }

        public ChartThemeFolder ThemeFolder
        {
            get { return _ThemeFolder; }
            set
            {
                if (_ThemeFolder != value)
                {
                    _ThemeFolder = value;
                    OnThemeFolderChanged();
                }
            }
        }

        private void OnThemeFolderChanged()
        {
            if (ThemeFolder != null)
            {
                Text = Lang._(ThemeFolder.Name);
                ThemeFolder.NameChanged += new EventHandler(Theme_NameChanged);
            }
        }

        private void Theme_NameChanged(object sender, EventArgs e)
        {
            if (ThemeFolder != null)
            {
                Text = Lang._(ThemeFolder.Name);
            }
        }
    }
}
