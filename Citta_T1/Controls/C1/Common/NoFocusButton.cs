using System.Drawing;
using System.Windows.Forms;

namespace C2.Controls.Common
{
    // 普通的Button由于Focus效果导致算子重命名时在某些情况下会出现不期望的聚焦框
    // 这重写ShowFocusCues方法,去掉所有情况下的聚焦框
    public class NoFocusButton : Button
    {
        private string _Text;
        public new string Text 
        { 
            get => AutoEllipsis ? _Text : base.Text; 
            set 
            {
                if (AutoEllipsis)
                {
                    _Text = value;
                    base.Text = string.Empty;
                }    
                else
                    base.Text = value;
            }
        }
        public NoFocusButton()
        {
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 1;
            this.Paint += NoFocusButton_Paint;
        }

        /// <summary>
        /// 取消捕获焦点后的聚焦框
        /// </summary>
        protected override bool ShowFocusCues
        {
            get
            {
                return false;
            }
        }

        private void NoFocusButton_Paint(object sender, PaintEventArgs e)
        {
            if (!AutoEllipsis)
                return;
            Button B = (Button)sender;
            Size S = TextRenderer.MeasureText(Text, B.Font);
            TextRenderer.DrawText(e.Graphics, Text, B.Font, new Rectangle(1, B.ClientRectangle.Top + (B.ClientRectangle.Height - S.Height) / 2,
                B.ClientRectangle.Width - 1, B.ClientRectangle.Height), B.ForeColor, Color.Transparent, TextFormatFlags.EndEllipsis | TextFormatFlags.HorizontalCenter);
        }
    }
}
