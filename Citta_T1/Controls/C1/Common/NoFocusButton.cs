using System.Windows.Forms;

namespace C2.Controls.Common
{
    // 普通的Button由于Focus效果导致算子重命名时在某些情况下会出现不期望的聚焦框
    // 这重写ShowFocusCues方法,去掉所有情况下的聚焦框
    public class NoFocusButton : Button
    {
        public NoFocusButton()
        {
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
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
    }
}
