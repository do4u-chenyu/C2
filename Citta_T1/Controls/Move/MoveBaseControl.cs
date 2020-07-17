using Citta_T1.Business.Model;
using Citta_T1.Core;
using Citta_T1.Core.UndoRedo;
using Citta_T1.Core.UndoRedo.Command;
using Citta_T1.Utils;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Citta_T1.Controls.Move
{
    public partial class MoveBaseControl : UserControl
    {
        private static LogUtil log = LogUtil.GetInstance("MoveDtContorl");
        public ElementType Type { get; set; }
        public int ID { get; set; }
        public string Description { get => this.textBox.Text; set => this.textBox.Text = value; }
        public OpUtil.Encoding Encoding { get; set; }
        public char Separator { get; set; }
        public virtual ElementStatus Status { get; set; }
        public string FullFilePath { get; set; }
        public Point WorldCord { get; set; }
        public OpUtil.ExtType ExtType
        {
            get
            {
                if (String.IsNullOrWhiteSpace(FullFilePath))
                    return OpUtil.ExtType.Unknow;
                if (FullFilePath.EndsWith(".xlsx", true, System.Globalization.CultureInfo.CurrentCulture))
                    return OpUtil.ExtType.Excel;
                if (FullFilePath.EndsWith(".xls", true, System.Globalization.CultureInfo.CurrentCulture))
                    return OpUtil.ExtType.Excel;
                if (FullFilePath.EndsWith(".txt", true, System.Globalization.CultureInfo.CurrentCulture))
                    return OpUtil.ExtType.Text;
                if (FullFilePath.EndsWith(".bcp", true, System.Globalization.CultureInfo.CurrentCulture))
                    return OpUtil.ExtType.Text;
                if (FullFilePath.EndsWith(".csv", true, System.Globalization.CultureInfo.CurrentCulture))
                    return OpUtil.ExtType.Text;
                return OpUtil.ExtType.Unknow;
            }
        }

        protected Size changeStatus;
        protected Size normalStatus;

        protected string oldTextString;
        protected Bitmap staticImage;
        // 缩放等级
        public int sizeLevel;

        // 画布上的缩放倍率
        protected float factor = Global.Factor;
        // 出度矩形框
        protected Rectangle rectOut;
        // 算子上一次位置
        protected Point oldControlPosition;
        // 移动时的鼠标偏移量
        protected Point mouseOffset;


        protected int pinWidth = 8;
        protected int pinHeight = 8;
        public Rectangle RectOut { get => rectOut; set => rectOut = value; }

        public MoveBaseControl()
        {
            InitializeComponent();

        }

        // 单元素拖拽
        public void ChangeLoc(float dx, float dy)
        {
            int left = this.Left + Convert.ToInt32(dx);
            int top = this.Top + Convert.ToInt32(dy);
            this.Location = new Point(left, top);
        }

        protected void RenameMenuItem_Click(object sender, EventArgs e)
        {
            if (Global.GetFlowControl().SelectDrag || Global.GetFlowControl().SelectFrame)
                return;
            this.textBox.ReadOnly = false;
            this.oldTextString = this.textBox.Text;
            this.txtButton.Visible = false;
            this.textBox.Visible = true;
            this.textBox.Focus();//获取焦点
            this.textBox.Select(this.textBox.TextLength, 0);
        }

        protected void DrawRoundRect(int x, int y, int width, int height, int radius)
        {
            if (this.staticImage != null)
            {   // bitmap是重型资源,需要强制释放
                this.staticImage.Dispose();
                this.staticImage = null;
            }
            this.staticImage = new Bitmap(this.Width, this.Height);
            Graphics g = Graphics.FromImage(staticImage);
            g.Clear(Color.White);

            //去掉圆角的锯齿
            g.SmoothingMode = SmoothingMode.HighQuality;//去掉锯齿
            g.CompositingQuality = CompositingQuality.HighQuality;//合成图像的质量
            g.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit;//去掉文字的锯齿


            g.DrawLine(MyPens.DarkGray, new PointF(x + radius, y), new PointF(x + width - radius, y));
            g.DrawLine(MyPens.DarkGray, new PointF(x + radius, y + height), new PointF(x + width - radius, y + height));
            g.DrawLine(MyPens.DarkGray, new PointF(x, y + radius), new PointF(x, y + height - radius));
            g.DrawLine(MyPens.DarkGray, new PointF(x + width, y + radius), new PointF(x + width, y + height - radius));
            g.DrawArc(MyPens.DarkGray, new Rectangle(x, y, radius * 2, radius * 2), 180, 90);
            g.DrawArc(MyPens.DarkGray, new Rectangle(x + width - radius * 2, y, radius * 2, radius * 2), 270, 90);
            g.DrawArc(MyPens.DarkGray, new Rectangle(x, y + height - radius * 2, radius * 2, radius * 2), 90, 90);
            g.DrawArc(MyPens.DarkGray, new Rectangle(x + width - radius * 2, y + height - radius * 2, radius * 2, radius * 2), 0, 90);

            g.Dispose();

            this.BackgroundImage = this.staticImage;
        }

        protected void SetControlsBySize(float f, Control control)
        {
            control.Width = Convert.ToInt32(control.Width * f);
            control.Height = Convert.ToInt32(control.Height * f);
            control.Left = Convert.ToInt32(control.Left * f);
            control.Top = Convert.ToInt32(control.Top * f);
            control.Font = new Font(control.Font.Name, control.Font.Size * f, control.Font.Style, control.Font.Unit);
            
            
            //遍历窗体中的控件，重新设置控件的值
            foreach (Control con in control.Controls)
                SetControlsBySize(f, con);
        }

        protected Rectangle SetRectBySize(float f, Rectangle rect)
        {
            rect.Width = Convert.ToInt32(rect.Width * f);
            rect.Height = Convert.ToInt32(rect.Height * f);
            rect.X = Convert.ToInt32(rect.Left * f);
            rect.Y = Convert.ToInt32(rect.Top * f);
            return rect;
        }

        public void ChangeSize(int sizeL)
        {
            if (sizeL > sizeLevel)
            {
                while (sizeL > sizeLevel)
                {
                    ChangeSize(true);
                    sizeLevel += 1;
                }
            }
            else
            {
                while (sizeL < sizeLevel)
                {
                    ChangeSize(false);
                    sizeLevel -= 1;
                }
            }
        }

        protected virtual void ChangeSize(bool zoomUp, float factor = Global.Factor)
        {

        }

        private void LeftPicture_MouseEnter(object sender, EventArgs e)
        {
            this.helpToolTip.SetToolTip(this.leftPictureBox, String.Format("元素ID: {0}", this.ID.ToString()));
        }



        protected void UpdateRound(int x, int y, int width, int height, int radius)
        {
            Graphics g = Graphics.FromImage(staticImage);

            g.SmoothingMode = SmoothingMode.HighQuality;//去掉锯齿
            g.CompositingQuality = CompositingQuality.HighQuality;//合成图像的质量
            g.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit;//去掉文字的锯齿
            g.DrawLine(MyPens.GreenDash2f, new PointF(x + radius, y), new PointF(x + width - radius, y));
            g.DrawLine(MyPens.GreenDash2f, new PointF(x + radius, y + height), new PointF(x + width - radius, y + height));
            g.DrawLine(MyPens.GreenDash2f, new PointF(x, y + radius), new PointF(x, y + height - radius));
            g.DrawLine(MyPens.GreenDash2f, new PointF(x + width, y + radius), new PointF(x + width, y + height - radius));
            g.DrawArc(MyPens.GreenDash2f, new Rectangle(x, y, radius * 2, radius * 2), 180, 90);
            g.DrawArc(MyPens.GreenDash2f, new Rectangle(x + width - radius * 2, y, radius * 2, radius * 2), 270, 90);
            g.DrawArc(MyPens.GreenDash2f, new Rectangle(x, y + height - radius * 2, radius * 2, radius * 2), 90, 90);
            g.DrawArc(MyPens.GreenDash2f, new Rectangle(x + width - radius * 2, y + height - radius * 2, radius * 2, radius * 2), 0, 90);

            g.Dispose();
            this.BackgroundImage = this.staticImage;
        }

        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Global.GetFlowControl().SelectDrag || Global.GetFlowControl().SelectFrame)
                return;
            // 按下回车键
            if (e.KeyChar == 13)
            {
                FinishTextChange();
            }
        }

        private void TextBox_Leave(object sender, EventArgs e)
        {
            if (Global.GetFlowControl().SelectDrag || Global.GetFlowControl().SelectFrame)
                return;
            FinishTextChange();
        }

        public void FinishTextChange()
        {
            if (this.textBox.Text.Trim().Length == 0)
                this.textBox.Text = this.oldTextString;
            this.textBox.ReadOnly = true;
            this.textBox.Visible = false;
            this.txtButton.Visible = true;
            if (this.oldTextString == this.textBox.Text)
                return;

            SetOpControlName(this.textBox.Text);
            // 构造重命名命令类,压入undo栈
            ModelElement element = Global.GetCurrentDocument().SearchElementByID(ID);
            if (element != ModelElement.Empty)
            {
                BaseCommand renameCommand = new ElementRenameCommand(element, oldTextString);
                UndoRedoManager.GetInstance().PushCommand(Global.GetCurrentDocument(), renameCommand);
            }

            this.oldTextString = this.textBox.Text;
            Global.GetMainForm().SetDocumentDirty();
            Global.GetCurrentDocument().UpdateAllLines();
            Global.GetCanvasPanel().Invalidate(false);
        }

        public Point UndoRedoMoveLocation(Point location)
        {
            // TODO 需要处理坐标原点变化的情况
            oldControlPosition = this.Location;
            this.Location = Global.GetCurrentDocument().WorldMap.WorldToScreen(location);
            Global.GetNaviViewControl().UpdateNaviView();
            Global.GetMainForm().SetDocumentDirty();
            return Global.GetCurrentDocument().WorldMap.ScreenToWorld(oldControlPosition, true);
        }

        public string UndoRedoChangeTextName(string des)
        {
            oldTextString = Description;
            Description = des;
            SetOpControlName(Description);
            Global.GetCurrentDocument().UpdateAllLines();
            Global.GetCanvasPanel().Invalidate(false);
            return oldTextString;
        }



        protected void SetOpControlName(string name)
        {
            this.Description = name;
            int maxLength = 24;
            name = ConvertUtil.SubstringByte(name, 0, maxLength);
            int sumCount = Regex.Matches(name, "[\u4E00-\u9FA5]").Count;
            int sumCountDigit = Regex.Matches(name, "[a-zA-Z0-9_-]").Count;
            int txtWidth = ConvertUtil.CountTextWidth(sumCount, sumCountDigit);
            this.txtButton.Text = name;
            if (ConvertUtil.GB2312.GetBytes(this.Description).Length > maxLength)
            {
                txtWidth += 11;
                this.txtButton.Text = name + "...";
            }
            changeStatus.Width = normalStatus.Width + txtWidth;
            ResizeControl(txtWidth, changeStatus);
            this.helpToolTip.SetToolTip(this.txtButton, this.Description);
        }

        protected virtual void ResizeControl(int txtWidth, Size controlSize)
        {
        }
        public virtual PointF GetEndPinLoc(int pinIndex)
        {
            return new PointF(0, 0);
        }
        public virtual PointF GetStartPinLoc(int pinIndex)
        {
            return new PointF(
                this.Location.X + this.rectOut.Location.X + this.rectOut.Width / 2,
                this.Location.Y + this.rectOut.Location.Y + this.rectOut.Height / 2);
        }
        public virtual PointF RevisePointLoc(PointF p)
        {
            return p;
        }
        public virtual void UndoRedoDeleteElement(ModelElement me, List<Tuple<int, int, int>> relations=null, ModelElement rsEle = null)
        {
            /*
             * 1. 删自身
             * 2. 删与之相连的关系
             * 3. 删与之相连的结果控件
             * 4. 改变其他控件的Pin状态
             */
            CanvasPanel cp = Global.GetCanvasPanel();
            if (relations != null)
            {
                foreach (Tuple<int, int, int> rel in relations)
                    cp.DeleteRelationByCtrID(rel.Item1, rel.Item2, rel.Item3);
            }
            cp.DeleteEle(me);
            if (rsEle != null)
                cp.DeleteEle(rsEle);
        }
        public virtual void UndoRedoAddElement(Dictionary<int, Point> eleWorldCordDict, ModelElement me, List<Tuple<int, int, int>> relations = null, ModelElement rsEle = null)
        {
            /*
             * 1. 恢复自身
             * 2. 恢复与之相连的关系
             * 3. 恢复与之相连的结果控件
             * 4. 改变其他控件的Pin状态
             */
            CanvasPanel cp = Global.GetCanvasPanel();
            cp.AddEleWhenUndoRedo(me);
            if (rsEle != null)
                cp.AddEleWhenUndoRedo(rsEle);
            if (relations != null)
            {
                foreach (Tuple<int, int, int> rel in relations)
                    cp.AddNewRelationByCtrID(rel.Item1, rel.Item2, rel.Item3);
            }
            ControlUtil.UpdateElesWorldCord(eleWorldCordDict);
        }
    }
}
