using System.Drawing;
using System.Windows.Forms;
using Citta_T1.Utils;
using Citta_T1.Business.Model;


namespace Citta_T1.Controls.Left
{
    public partial class OperatorControl : UserControl
    {
        private Point mouseOffset; //记录鼠标指针的坐标
        public OperatorControl()
        {
            InitializeComponent();
            InitializeToolTip();
        }

        private void InitializeToolTip()
        {
            this.toolTip1.SetToolTip(this.leftPanelOpRelate, HelpUtil.RelateOperatorHelpInfo);
            this.toolTip1.SetToolTip(this.leftPanelOpCollide, HelpUtil.CollideOperatorHelpInfo);
            this.toolTip1.SetToolTip(this.lefPanelOpUnion, HelpUtil.UnionOperatorHelpInfo);
            this.toolTip1.SetToolTip(this.leftPanelOpDiffer, HelpUtil.DifferOperatorHelpInfo);
            this.toolTip1.SetToolTip(this.leftPanelOpRandom, HelpUtil.RandomOperatorHelpInfo);
            this.toolTip1.SetToolTip(this.leftPanelOpFilter, HelpUtil.FilterOperatorHelpInfo);
            this.toolTip1.SetToolTip(this.leftPanelOpMax, HelpUtil.MaxOperatorHelpInfo);
            this.toolTip1.SetToolTip(this.leftPanelOpMin, HelpUtil.MinOperatorHelpInfo);
            this.toolTip1.SetToolTip(this.leftPanelOpAvg, HelpUtil.AvgOperatorHelpInfo);
            this.toolTip1.SetToolTip(this.leftPanelOpFreq, HelpUtil.FreqOperatorHelpInfo);
            this.toolTip1.SetToolTip(this.leftPanelOpSort, HelpUtil.SortOperatorHelpInfo);
            this.toolTip1.SetToolTip(this.leftPanelOpGroup, HelpUtil.GroupOperatorHelpInfo);
        }


        private void LeftPaneOp_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                DataObject dragDropData = new DataObject();
                dragDropData.SetData("Type", ElementType.Operator);
                dragDropData.SetData("Path", "");
                dragDropData.SetData("Text", (sender as Button).Text);
                (sender as Button).DoDragDrop(dragDropData, DragDropEffects.Copy | DragDropEffects.Move);
            }
        }

        private void button1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseOffset.X = e.X;
                mouseOffset.Y = e.Y;
            }
        }

        protected override void OnGiveFeedback(GiveFeedbackEventArgs e) 
        {
            base.OnGiveFeedback(e);
            e.UseDefaultCursors = false;
            Bitmap map = new Bitmap(this.pictureBox1.Image);
            

            Bitmap myNewCursor = new Bitmap(map.Width * 2 , map.Height * 2 );
            Graphics g = Graphics.FromImage(myNewCursor);
            g.Clear(Color.FromArgb(0, 0, 0, 0));
            g.DrawImage(map, map.Width, map.Height, map.Width,map.Height);
            
            Cursor.Current = new Cursor(myNewCursor.GetHicon());

            g.Dispose();
            myNewCursor.Dispose();
            myNewCursor = null;
            map.Dispose();
            map = null;

        }
    }
}
