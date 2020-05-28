using Citta_T1.Controls.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Citta_T1.OperatorViews.Validate
{
    class OperatorViewValidate
    {

        private static OperatorViewValidate instance = null;
        private OperatorViewValidate()
        { }

        public static OperatorViewValidate GetInstance()
        {
            if (instance == null)
                instance = new OperatorViewValidate();
            return instance;
        }

        public bool ComCheckBoxListValidating(ComCheckBoxList ccbl, string message = "请给算子选择对应的输出字段")
        {
            if (ccbl.GetItemCheckIndex().Count == 0)
            {
                MessageBox.Show(message);
                return false;
            }
            return true;
        }

        public bool ComBoxValidating(ComboBox cb, string message = "请选择字段")
        {
            if (String.IsNullOrWhiteSpace(cb.Text))
            {
                MessageBox.Show(message);
                return false;
            }
            return true;
        }
    }
}
