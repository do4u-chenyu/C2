using C2.Business.Model;
using C2.Utils;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace C2.Controls.Left
{
    public partial class OperatorControl : UserControl
    {
        public OperatorControl()
        {
            InitializeComponent();
            InitializeComponent();
            InitializeToolTip();
            this.Padding = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
        }

        // 圆角
        private int _Radius = 30;  // 圆角弧度

        /// <summary>圆角弧度(0为不要圆角)</summary>
        [Browsable(true)]
        [Description("圆角弧度(0为不要圆角)")]
        public int _setRoundRadius
        {
            get
            {
                return _Radius;
            }
            set
            {
                if (value < 0) { _Radius = 0; }
                else { _Radius = value; }
                base.Refresh();
            }
        }

        // 圆角代码
        public void Round(System.Drawing.Region region)
        {
            // 已经是.net提供给我们的最容易的改窗体的属性了(以前要自己调API)
            System.Drawing.Drawing2D.GraphicsPath oPath = new System.Drawing.Drawing2D.GraphicsPath();
            int x = 0;
            int y = 0;
            int thisWidth = this.Width;
            int thisHeight = this.Height;
            int angle = _Radius;
            if (angle > 0)
            {
                System.Drawing.Graphics g = CreateGraphics();
                oPath.AddArc(x, y, angle, angle, 180, 90);                                 // 左上角
                oPath.AddArc(thisWidth - angle, y, angle, angle, 270, 90);                 // 右上角
                oPath.AddArc(thisWidth - angle, thisHeight - angle, angle, angle, 0, 90);  // 右下角
                oPath.AddArc(x, thisHeight - angle, angle, angle, 90, 90);                 // 左下角
                oPath.CloseAllFigures();
                Region = new System.Drawing.Region(oPath);
            }
            
            else
            {
                oPath.AddLine(x + angle, y, thisWidth - angle, y);                         // 顶端
                oPath.AddLine(thisWidth, y + angle, thisWidth, thisHeight - angle);        // 右边
                oPath.AddLine(thisWidth - angle, thisHeight, x + angle, thisHeight);       // 底边
                oPath.AddLine(x, y + angle, x, thisHeight - angle);                        // 左边
                oPath.CloseAllFigures();
                Region = new System.Drawing.Region(oPath);
            }
        }

        public OperatorControl(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs pe)
        {
            base.OnPaint(pe);
            Round(this.Region);  // 圆角
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

        private Bitmap SetImgcolor(Bitmap img)
        {
            Color initColor = img.GetPixel(0, 0);
            for (int i = 0; i < img.Height; i++)
            {
                for (int j = 0; j < img.Width; j++)
                {
                    Color c = img.GetPixel(j, i);
                    if (c.Equals(initColor))
                        img.SetPixel(j, i, Color.FromArgb(0, 0, 0, 0));
                }
            }
            img.Save("1.png");
            return img;
        }

        private void relateOpPictureBox_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void customOPButton2_Click(object sender, EventArgs e)
        {

        }
    }
}
