using System.Windows.Forms;
using Citta_T1.Business.Model;
using Citta_T1.Utils;

namespace Citta_T1.Controls.Left
{
    public partial class FlowChartControl : UserControl
    {
        public FlowChartControl()
        {
            InitializeComponent();
            this.toolTip1.SetToolTip(this.customOPButton1, HelpUtil.CustomOperator1HelpInfo);
            this.toolTip1.SetToolTip(this.customOPButton2, HelpUtil.CustomOperator2HelpInfo);
            this.toolTip1.SetToolTip(this.pythonOPButton, HelpUtil.PythonOperatorHelpInfo);
        }

        private void FlowChartControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && sender is Button)
            {
                DataObject dragDropData = new DataObject();
                dragDropData.SetData("Type", ElementType.Operator);
                dragDropData.SetData("Path", string.Empty);
                dragDropData.SetData("Text", (sender as Button).Text);
                (sender as Button).DoDragDrop(dragDropData, DragDropEffects.Copy | DragDropEffects.Move);
            }
        }
    }
}
