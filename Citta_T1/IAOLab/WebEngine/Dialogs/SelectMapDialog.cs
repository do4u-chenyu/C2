using C2.Controls;
using C2.IAOLab.WebEngine.GisMap;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C2.IAOLab.WebEngine.Dialogs
{
    partial class SelectMapDialog : StandardDialog
    {
        public string WebUrl;
        public SelectMapDialog()
        {
            InitializeComponent();
        }

        protected override bool OnOKButtonClick()
        {
            //生成html
            WebUrl = GenGisMapHtml.GetInstance().TransDataToHtml();

            return base.OnOKButtonClick();
        }
    }
}
