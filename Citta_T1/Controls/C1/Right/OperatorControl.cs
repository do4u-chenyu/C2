using C2.Business.Model;
using C2.Utils;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace C2.Controls.Right
{
    public partial class OperatorControl : UserControl
    {
        public OperatorControl()
        {
            InitializeComponent();
            InitializeToolTip();
        }

        // 圆角
        private int radius = 20;  // 圆角弧度

        [Browsable(true), DefaultValue(20)]
        [Description("圆角弧度(0为不要圆角)")]
        public int RoundRadius
        {
            get
            {
                return radius;
            }
            set
            {
                radius = Math.Max(0, value);
                base.Refresh();
            }
        }
        // 圆角代码
        public void CustomBorderRound()
        {
            // 已经是.net提供给我们的最容易的改窗体的属性了(以前要自己调API)
            System.Drawing.Drawing2D.GraphicsPath oPath = new System.Drawing.Drawing2D.GraphicsPath();
            int thisWidth = this.Width;
            int thisHeight = this.Height;
            int angle = radius;
            if (angle > 0)
            {
                oPath.AddArc(0, 0, angle, angle, 180, 90);                                 // 左上角
                oPath.AddArc(thisWidth - angle, 0, angle, angle, 270, 90);                 // 右上角
                oPath.AddArc(thisWidth - angle, thisHeight - angle, angle, angle, 0, 90);  // 右下角
                oPath.AddArc(0, thisHeight - angle, angle, angle, 90, 90);                 // 左下角
            }
            else
            {
                oPath.AddLine(0, 0, thisWidth, 0);                         // 顶端
                oPath.AddLine(thisWidth, 0, thisWidth, thisHeight);        // 右边
                oPath.AddLine(thisWidth, thisHeight, 0, thisHeight);       // 底边
                oPath.AddLine(0, 0, 0, thisHeight);                       // 左边
            }
            oPath.CloseAllFigures();
            Region = new Region(oPath);
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            CustomBorderRound();  // 圆角
            base.OnPaint(pe);
        }
        protected override void OnResize(EventArgs eventargs)
        {
            base.OnResize(eventargs);
            base.Refresh();
        }
        private void InitializeToolTip()
        {
            this.toolTip1.SetToolTip(this.leftPanelOpRelate, HelpUtil.RelateOperatorHelpInfo);
            this.toolTip1.SetToolTip(this.leftPanelOpCollide, HelpUtil.CollideOperatorHelpInfo);
            this.toolTip1.SetToolTip(this.leftPanelOpUnion, HelpUtil.UnionOperatorHelpInfo);
            this.toolTip1.SetToolTip(this.leftPanelOpDiffer, HelpUtil.DifferOperatorHelpInfo);
            this.toolTip1.SetToolTip(this.leftPanelOpRandom, HelpUtil.RandomOperatorHelpInfo);
            this.toolTip1.SetToolTip(this.leftPanelOpFilter, HelpUtil.FilterOperatorHelpInfo);
            this.toolTip1.SetToolTip(this.leftPanelOpMax, HelpUtil.MaxOperatorHelpInfo);
            this.toolTip1.SetToolTip(this.leftPanelOpMin, HelpUtil.MinOperatorHelpInfo);
            this.toolTip1.SetToolTip(this.leftPanelOpAvg, HelpUtil.AvgOperatorHelpInfo);
            this.toolTip1.SetToolTip(this.leftPanelOpFreq, HelpUtil.FreqOperatorHelpInfo);
            this.toolTip1.SetToolTip(this.leftPanelOpSort, HelpUtil.SortOperatorHelpInfo);
            this.toolTip1.SetToolTip(this.leftPanelOpGroup, HelpUtil.GroupOperatorHelpInfo);
            this.toolTip1.SetToolTip(this.leftPanelOpWordFilter, HelpUtil.KeyWordOperatorHelpInfo);
            this.toolTip1.SetToolTip(this.leftPanelOpDataStandar, HelpUtil.DataFormatOperatorHelpInfo);
            this.toolTip1.SetToolTip(this.customOPButton1, HelpUtil.CustomOperator1HelpInfo);
            this.toolTip1.SetToolTip(this.customOPButton2, HelpUtil.CustomOperator2HelpInfo);
            this.toolTip1.SetToolTip(this.pythonOPButton, HelpUtil.PythonOperatorHelpInfo);
        }


        private void LeftPaneOp_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && sender is Button)
            {
                DataObject dragDropData = new DataObject();
                dragDropData.SetData("Type", ElementType.Operator);
                dragDropData.SetData("Path", String.Empty);
                dragDropData.SetData("Text", (sender as Button).Text);
                (sender as Button).DoDragDrop(dragDropData, DragDropEffects.Copy | DragDropEffects.Move);
            }
        }
    }
}
