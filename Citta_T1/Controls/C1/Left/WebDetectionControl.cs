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
    public partial class WebDetectionControl : BaseLeftInnerPanel
    {
        public WebDetectionControl()
        {
            InitializeComponent();
            this.titleLabel.Text = "网站侦察兵";
            this.addLabel.Text = "+新建任务";
        }

        public override void AddButton()
        {
            //TODO 打开不同配置窗口，确定后新加
            this.handleButton = new WebDetectionButton();
            base.AddButton();
        }




        private class WebDetectionButton : BaseLeftInnerButton
        {
            public WebDetectionButton()
            {
                this.leftPictureBox.Image = global::C2.Properties.Resources.提示;
            }
        }
    }
}
