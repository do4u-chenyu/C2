using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C2.Controls.C1.Left
{
    public partial class SearchToolkitControl : BaseLeftInnerPanel
    {
        public SearchToolkitControl()
        {
            InitializeComponent();
        }

        private void AddTaskLabel_Click(object sender, EventArgs e)
        {
            AddInnerButton(new SearchToolkitButton());
        }
    }
}
