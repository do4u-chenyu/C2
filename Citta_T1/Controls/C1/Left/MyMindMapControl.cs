using C2.Business.Model;
using C2.Core;
using C2.Dialogs;
using C2.Utils;
using System.Drawing;
using System.Windows.Forms;

namespace C2.Controls.Left
{
    public partial class MyMindMapControl : UserControl
    {
        public MyMindMapControl()
        {
            InitializeComponent();
            startPoint = new Point(ButtonLeftX, -ButtonBottomOffsetY);
            startPoint.Y -= 12;
        }

        private static readonly int ButtonLeftX = 18;
        private static readonly int ButtonBottomOffsetY = 23;
        private static readonly int ButtonGapY = 50;
        private Point startPoint;
        public void AddMindMapModel(string modelName)
        {
            MindMapModelButton mb = new MindMapModelButton(modelName);
            // 获得当前要添加的model button的初始位置
            LayoutModelButtonLocation(mb);
            //this.Controls.Add(mb);
            this.MindMapPaintPanel.Controls.Add(mb);
        }

        private void LayoutModelButtonLocation(Control ct)
        {
            if (this.MindMapPaintPanel.Controls.Count > 0)
            {
                this.startPoint = this.MindMapPaintPanel.Controls[this.MindMapPaintPanel.Controls.Count - 1].Location;
            }
            else
            {
                this.MindMapPaintPanel.VerticalScroll.Value = 0;
                startPoint = new Point(ButtonLeftX, -ButtonBottomOffsetY);
            }
            this.startPoint.Y += ButtonGapY;
            ct.Location = this.startPoint;
        }

        public bool ContainModel(string modelTitle)
        {
            foreach (Control ct in this.MindMapPaintPanel.Controls)
            {
                if ((ct is MindMapModelButton) && (ct as MindMapModelButton).ModelTitle == modelTitle)
                    return true;
            }
            return false;
        }

        public void RemoveMindMapButton(MindMapModelButton modelButton)
        {
            // panel左上角坐标随着滑动条改变而改变，以下就是将panel左上角坐标校验
            if (this.MindMapPaintPanel.Controls.Count > 0)
                this.startPoint.Y = this.MindMapPaintPanel.Controls[0].Location.Y - this.MindMapPaintPanel.Controls[0].Height - ButtonBottomOffsetY;

            // 先暂停布局,然后调整button位置,最后恢复布局,可以避免闪烁
            using (new GuarderUtil.LayoutGuarder(MindMapPaintPanel))
            {
                int idx = this.MindMapPaintPanel.Controls.IndexOf(modelButton);
                ReLayoutMindMapButtons(idx); // 重新布局
                this.MindMapPaintPanel.Controls.Remove(modelButton); // 删除控件
            }
        }

        private void ReLayoutMindMapButtons(int index)
        {
            for (int i = index + 1; i < this.MindMapPaintPanel.Controls.Count; i++)
            {
                Control ct = this.MindMapPaintPanel.Controls[i];
                ct.Location = new Point(ct.Location.X, ct.Location.Y - ButtonGapY);
            }
        }

        private void MindMapModelControl_MouseDown(object sender, MouseEventArgs e)
        {
            // 强制编辑控件失去焦点,触发重命名控件的Leave事件 
            Global.GetMainForm().BlankButtonFocus();
        }

        private void MindMapModelControl_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen p = new Pen(Color.FromArgb(195, 195, 195), 1);  //画笔 1宽度.
            p.DashStyle = System.Drawing.Drawing2D.DashStyle.Custom;
            p.DashPattern = new float[] { 4, 4 };
            g.DrawLine(p, 0, 30, 200, 30);//x1,y1,x2,y2

        }

        private void AddMindMapButton_Click(object sender, System.EventArgs e)
        {
            ZipDialog zipDialog = new ImportZipDialog();
            if (zipDialog.ShowDialog() == DialogResult.OK)
            {
                string fullFilePath = zipDialog.ModelPath;
                string password = zipDialog.Password;
                if (ImportModel.GetInstance().UnZipC2File(fullFilePath, Global.GetMainForm().UserName, password))
                    HelpUtil.ShowMessageBox("模型导入成功");
            }
        }
    }
}
