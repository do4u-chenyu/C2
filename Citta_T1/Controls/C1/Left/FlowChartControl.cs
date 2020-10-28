using C2.Business.Model;
using C2.Utils;
using System.Windows.Forms;

namespace C2.Controls.Left
{
    public partial class FlowChartControl : UserControl
    {
        public FlowChartControl()
        {
            InitializeComponent();
            
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

        private void customOPButton1_Click(object sender, System.EventArgs e)
        {

        }
    }
}
