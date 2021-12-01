using C2.Controls;

namespace C2.Forms
{
    public partial class JSForm : BaseForm
    {
        public JSForm()
        {
            InitializeComponent();
        }

        public override bool IsNeedShowBottomViewPanel()
        {
            return false;
        }
    }
}
