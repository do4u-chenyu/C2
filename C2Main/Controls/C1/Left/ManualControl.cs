using C2.Business.Model;
using C2.Core;
using C2.Dialogs;
using C2.Model.MindMaps;
using C2.Utils;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace C2.Controls.Left
{
    public partial class ManualControl : UserControl
    {
        protected string desc;
        public ManualControl()
        {
            InitializeComponent();
            InitializeOther();
        }

        private void InitializeOther()
        {
            startPoint = new Point(ButtonLeftX, -ButtonBottomOffsetY);
            startPoint.Y -= 12;
            desc = "战术手册";
        }

        private static readonly int ButtonLeftX = 18;
        private static readonly int ButtonBottomOffsetY = 23;
        private static readonly int ButtonGapY = 50;
        private Point startPoint;
        public void AddButton(string modelName)
        {
            MindMapModelButton mb = new MindMapModelButton(modelName);
            // 获得当前要添加的model button的初始位置
            LayoutButton(mb);
            //this.Controls.Add(mb);
            this.PaintPanel.Controls.Add(mb);
        }

        private void LayoutButton(Control ct)
        {
            if (this.PaintPanel.Controls.Count > 0)
            {
                this.startPoint = this.PaintPanel.Controls[this.PaintPanel.Controls.Count - 1].Location;
            }
            else
            {
                this.PaintPanel.VerticalScroll.Value = 0;
                startPoint = new Point(ButtonLeftX, -ButtonBottomOffsetY);
            }
            this.startPoint.Y += ButtonGapY;
            ct.Location = this.startPoint;
        }

        public bool ContainModel(string modelTitle)
        {
            foreach (Control ct in this.PaintPanel.Controls)
            {
                if ((ct is MindMapModelButton) && (ct as MindMapModelButton).ModelTitle == modelTitle)
                    return true;
            }
            return false;
        }

        private string FindFFP(string modelTitle)
        {
            foreach (Control ct in this.PaintPanel.Controls)
            {
                if ((ct is MindMapModelButton) && (ct as MindMapModelButton).ModelTitle == modelTitle)
                    return (ct as MindMapModelButton).FullFilePath;
            }
            return string.Empty;
        }

        public bool TryOpen(string modelTitle)
        {
            if (!ContainModel(modelTitle))
                return false;
            string ffp = FindFFP(modelTitle);
            Global.GetMainForm().OpenDocument(ffp);
            return true;
        }

        public void RemoveButton(MindMapModelButton modelButton)
        {
            // panel左上角坐标随着滑动条改变而改变，以下就是将panel左上角坐标校验
            if (this.PaintPanel.Controls.Count > 0)
                this.startPoint.Y = this.PaintPanel.Controls[0].Location.Y - this.PaintPanel.Controls[0].Height - ButtonBottomOffsetY;

            int idx = this.PaintPanel.Controls.IndexOf(modelButton);
            // 先暂停布局,然后调整button位置,最后恢复布局,可以避免闪烁
            using (new GuarderUtil.LayoutGuarder(PaintPanel))
            {
                ReLayoutMindMapButtons(idx); // 重新布局
                this.PaintPanel.Controls.Remove(modelButton); // 删除控件
            }
        }

        private void ReLayoutMindMapButtons(int index)
        {
            for (int i = index + 1; i < this.PaintPanel.Controls.Count; i++)
            {
                Control ct = this.PaintPanel.Controls[i];
                ct.Location = new Point(ct.Location.X, ct.Location.Y - ButtonGapY);
            }
        }

        private void ManualControl_MouseDown(object sender, MouseEventArgs e)
        {
            // 强制编辑控件失去焦点,触发重命名控件的Leave事件 
            Global.GetMainForm().BlankButtonFocus();
        }

        private void ManualControl_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen p = new Pen(Color.FromArgb(195, 195, 195), 1);  //画笔 1宽度.
            p.DashStyle = System.Drawing.Drawing2D.DashStyle.Custom;
            p.DashPattern = new float[] { 4, 4 };
            g.DrawLine(p, 0, 30, 200, 30);//x1,y1,x2,y2

        }
        public void AddButton_Click(object sender, EventArgs e)
        {
            ZipDialog zipDialog = new ImportZipDialog();
            if (zipDialog.ShowDialog() == DialogResult.OK)
            {
                string fullFilePath = zipDialog.ModelPath;
                string filename = fullFilePath.Substring(fullFilePath.LastIndexOf("\\") + 1, fullFilePath.Length - fullFilePath.LastIndexOf("\\") - 1).Replace(".c2", string.Empty);
                string password = zipDialog.Password;
                if (Path.GetExtension(fullFilePath) == ".doc" || Path.GetExtension(fullFilePath) == ".docx")
                {
                    ImportWordFile.GetInstance().Import(fullFilePath);
                    return;
                }
                if (Path.GetExtension(fullFilePath) == ".xmind")
                {
                    ImportXmindFile.GetInstance().LoadXml(fullFilePath);
                    return;
                }
                if (ImportModel.GetInstance().UnZipC2File(fullFilePath, Global.GetMainForm().UserName, password))
                    HelpUtil.ShowMessageBox(String.Format("[{0}]导入[{1}仓库]成功", filename, desc));
            }
        }
    }
}
