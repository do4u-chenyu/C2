using C2.Controls;
using C2.IAOLab.WebEngine.GisMap;
using C2.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
        public List<DataItem> DataItems;
        public SelectMapDialog(List<DataItem> dataItems)
        {
            InitializeComponent();
            WebUrl = Path.Combine(Application.StartupPath, "IAOLab\\WebEngine\\Html", "StartMap.html");
            DataItems = dataItems;
            foreach (DataItem dataItem in DataItems)
            {
                this.datasourceComboBox.Items.Add(dataItem.FileName);
            }
        }


        protected override bool OnOKButtonClick()
        {
            //生成html
            WebUrl = GenGisMapHtml.GetInstance().TransDataToHtml();

            return base.OnOKButtonClick();
        }
    }
}
