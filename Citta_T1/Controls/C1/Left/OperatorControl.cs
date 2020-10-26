using C2.Business.Model;
using C2.Utils;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace C2.Controls.Left
{
    public partial class OperatorControl : UserControl
    {
        public OperatorControl()
        {
            InitializeComponent();
            InitializeToolTip();
            
            //this.differOpPictureBox.Image = SetImgcolor(global::C2.Properties.Resources.differOp);
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
            this.toolTip1.SetToolTip(this.leftPanelOpWordFilter, HelpUtil.KeyWordOperatorHelpInfo);
            this.toolTip1.SetToolTip(this.leftPanelOpDataStandar, HelpUtil.DataFormatOperatorHelpInfo);
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
