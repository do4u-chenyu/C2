using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using C2.Dialogs.Base;
using C2.Model.Widgets;

namespace C2.Dialogs.C2OperatorViews
{
    public partial class C2FilterOperatorView : C2BaseOperatorView
    {
        public C2FilterOperatorView(OperatorWidget operatorWidget) : base(operatorWidget)
        {
            InitializeComponent();
        }
    }
}
