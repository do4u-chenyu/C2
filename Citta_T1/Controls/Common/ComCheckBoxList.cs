using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Citta_T1.Controls.Common
{
    /// <summary>
    /// 带下拉框的用户控件
    /// </summary>
    public partial class ComCheckBoxList : UserControl
    {
        private TextBox tbSelectedValue;
        private ButtonS btnSelect;//下拉箭头
        private LabelS lbGrip;//此LABEL用于设置可以拖动下拉窗体变化

        private CheckedListBox checkListBox;
        private CheckedListBox checkListBoxTmp;
        private Label lbSelectAll;//全选
        private Label lbSelectNo;//取消

        private Form frmCheckList;

        private TextBox textBox1;
        private Panel pnlBack;
        private Panel pnlCheck;

        private System.Drawing.Point DragOffset; //用于记录窗体大小变化的位置

        //单击列表项状态更改事件
        public delegate void CheckBoxListItemClick(object sender, ItemCheckEventArgs e);
        public event CheckBoxListItemClick ItemClick;

        public ComCheckBoxList()
        {
            InitializeComponent();
            this.Name = "comBoxCheckBoxList";
            this.Layout += new LayoutEventHandler(ComCheckBoxList_Layout);

            //生成控件
            tbSelectedValue = new TextBox();
            tbSelectedValue.ReadOnly = true;
            tbSelectedValue.BorderStyle = BorderStyle.None;
            tbSelectedValue.BackColor = Color.White;

            //下拉箭头
            this.btnSelect = new ButtonS();
            btnSelect.FlatStyle = FlatStyle.Flat;
            btnSelect.Click += new EventHandler(btnSelect_Click);

            //全选
            this.lbSelectAll = new Label();
            lbSelectAll.BackColor = Color.Transparent;
            lbSelectAll.Font = new Font("微软雅黑", 8f, FontStyle.Regular);
            lbSelectAll.Text = "全选";
            lbSelectAll.Size = new Size(40, 20);
            lbSelectAll.ForeColor = Color.Blue;
            lbSelectAll.Cursor = Cursors.Hand;
            lbSelectAll.TextAlign = ContentAlignment.MiddleCenter;
            lbSelectAll.Click += new EventHandler(lbSelectAll_Click);

            //取消
            lbSelectNo = new Label();
            lbSelectNo.Font = new Font("微软雅黑", 8f, FontStyle.Regular);
            lbSelectNo.BackColor = Color.Transparent;
            lbSelectNo.Text = "取消";
            lbSelectNo.Size = new Size(40, 20);
            lbSelectNo.ForeColor = Color.Blue;
            lbSelectNo.Cursor = Cursors.Hand;
            lbSelectNo.TextAlign = ContentAlignment.MiddleCenter;
            lbSelectNo.Click += new EventHandler(lbSelectNo_Click);

            //生成checkboxlist
            this.checkListBox = new CheckedListBox();
            checkListBox.Font = new Font("微软雅黑", 8f, FontStyle.Regular);
            checkListBox.BorderStyle = BorderStyle.None;
            checkListBox.Location = new Point(0, 25);
            checkListBox.CheckOnClick = true;
            checkListBox.ScrollAlwaysVisible = true;
            //checkListBox.LostFocus += new EventHandler(checkListBox_LostFocus);
            checkListBox.ItemCheck += new ItemCheckEventHandler(checkListBox_ItemCheck);

            //生成checkListBoxTmp
            this.checkListBoxTmp = new CheckedListBox();
            checkListBoxTmp.Font = new Font("微软雅黑", 8f, FontStyle.Regular);
            checkListBoxTmp.BorderStyle = BorderStyle.None;
            checkListBoxTmp.Location = new Point(0, 25);
            checkListBoxTmp.CheckOnClick = true;
            checkListBoxTmp.ScrollAlwaysVisible = true;
            //checkListBoxTmp.LostFocus += new EventHandler(checkListBox_LostFocus);
            checkListBoxTmp.ItemCheck += new ItemCheckEventHandler(checkListBoxTmp_ItemCheck);
            checkListBoxTmp.Hide();

            //窗体
            frmCheckList = new Form();
            frmCheckList.FormBorderStyle = FormBorderStyle.None;
            frmCheckList.StartPosition = FormStartPosition.Manual;
            frmCheckList.BackColor = SystemColors.Control;
            frmCheckList.ShowInTaskbar = false;
            frmCheckList.Activated += new System.EventHandler(this.frmCheckList_Activated);
            frmCheckList.Deactivate += new System.EventHandler(this.frmCheckList_Deactivate);


            //可拖动窗体大小变化的LABEL
            lbGrip = new LabelS();
            lbGrip.Size = new Size(9, 18);
            lbGrip.BackColor = Color.Transparent;
            lbGrip.Cursor = Cursors.SizeNWSE;
            //lbGrip.MouseDown+=new MouseEventHandler(lbGrip_MouseDown);
            //lbGrip.MouseMove+=new MouseEventHandler(lbGrip_MouseMove);

            //panel
            pnlBack = new Panel();
            pnlBack.BorderStyle = BorderStyle.Fixed3D;
            pnlBack.BackColor = Color.White;
            pnlBack.AutoScroll = false;
            //pnlBack.LostFocus += new EventHandler(checkListBox_LostFocus);
            // 
            // textBox1
            // 
            textBox1 = new TextBox();
            textBox1.Size = new System.Drawing.Size(50, 25);
            textBox1.LostFocus += new EventHandler(textBox1_LostFocus); //失去焦点后发生事件
            textBox1.GotFocus += new EventHandler(textBox1_GotFocus);  //获取焦点前发生事件
            textBox1.TextChanged += new EventHandler(textBox1_TextChanged);

            //
            pnlCheck = new Panel();
            pnlCheck.BorderStyle = BorderStyle.FixedSingle;
            pnlCheck.BackColor = Color.White; ;


            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);

            pnlBack.Controls.Add(tbSelectedValue);
            pnlBack.Controls.Add(btnSelect);

            this.Controls.Add(pnlBack);


            pnlCheck.Controls.Add(checkListBox);
            pnlCheck.Controls.Add(checkListBoxTmp);
            pnlCheck.Controls.Add(textBox1);
            pnlCheck.Controls.Add(lbSelectAll);
            pnlCheck.Controls.Add(lbSelectNo);
            pnlCheck.Controls.Add(lbGrip);
            this.frmCheckList.Controls.Add(pnlCheck);


        }

        private void frmCheckList_Deactivate(object sender, EventArgs e)
        {
            if (!this.btnSelect.RectangleToScreen(this.btnSelect.ClientRectangle).Contains(Cursor.Position))
            {
                frmCheckList.Hide();
            }
        }

        private void frmCheckList_Activated(object sender, EventArgs e)
        {
            checkListBox.Focus();
        }

        private void checkListBoxTmp_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (ItemClick != null)
            {
                ItemClick(sender, e);
            }


            if (GetItemText(this.checkListBoxTmp.Items[e.Index]) == "未找到结果")
            {
                e.NewValue = CheckState.Indeterminate;
                return;
            }



            //获取选中的数量
            int nCount = this.checkListBox.CheckedItems.Count;

            //找到改变状态的checkbox
            for (int i = 0; i < checkListBox.Items.Count; i++)
            {
                if (GetItemText(checkListBox.Items[i]) == GetItemText(this.checkListBoxTmp.Items[e.Index]))
                {
                    checkListBox.SetItemChecked(i, e.NewValue == CheckState.Checked ? true : false);
                }
            }

            if (this.checkListBoxTmp.CheckedItems.Contains(this.checkListBoxTmp.Items[e.Index]))
            {

                if (e.NewValue != CheckState.Checked)
                {
                    nCount--;
                }
            }
            else
            {
                if (e.NewValue == CheckState.Checked)
                {
                    nCount++;
                }
            }

            tbSelectedValue.Text = "已选择" + nCount.ToString() + "项";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (this.textBox1.Text == "")
            {
                checkListBoxTmp.Hide();
                checkListBox.Show();
                return;
            }

            checkListBox.Hide();
            checkListBoxTmp.Show();
            int count = 0;
            this.checkListBoxTmp.Items.Clear();
            checkListBoxTmp.ItemCheck -= checkListBoxTmp_ItemCheck;

            for (int i = 0; i < checkListBox.Items.Count; i++)
            {
                if (GetItemText(checkListBox.Items[i]).Contains(this.textBox1.Text))
                {
                    checkListBoxTmp.Items.Add(GetItemText(checkListBox.Items[i]), checkListBox.GetItemChecked(i) ? CheckState.Checked : CheckState.Unchecked);
                    count++;
                }
            }

            if (count == 0)
            {
                checkListBoxTmp.Items.Add("未找到结果", CheckState.Indeterminate);
            }

            checkListBoxTmp.ItemCheck += checkListBoxTmp_ItemCheck;
        }

        private void textBox1_GotFocus(object sender, EventArgs e)
        {

        }

        private void textBox1_LostFocus(object sender, EventArgs e)
        {

        }

        private void ReloationGrip()
        {
            textBox1.Width = this.frmCheckList.Width - 20;

            lbGrip.Top = this.frmCheckList.Height - lbGrip.Height - 1;
            lbGrip.Left = this.frmCheckList.Width - lbGrip.Width - 1;

            lbSelectAll.Left = 5;
            lbSelectAll.Top = frmCheckList.Height - lbSelectAll.Height;

            lbSelectNo.Left = 50;
            lbSelectNo.Top = frmCheckList.Height - lbSelectNo.Height;


        }

        #region 事件


        //布局
        private void ComCheckBoxList_Layout(object sender, LayoutEventArgs e)
        {
            this.Height = tbSelectedValue.Height + 6;
            this.pnlBack.Size = new Size(this.Width, this.Height - 2);

            //设置按钮的位置
            this.btnSelect.Size = new Size(16, this.Height - 6);
            btnSelect.Location = new Point(this.Width - this.btnSelect.Width - 4, 0);

            this.tbSelectedValue.Location = new Point(2, 2);
            this.tbSelectedValue.Width = this.Width - btnSelect.Width - 4;

            checkListBox.Height = 150;

            //设置窗体
            this.frmCheckList.Size = new Size(this.Width, this.checkListBox.Height + this.textBox1.Height);
            this.pnlCheck.Size = frmCheckList.Size;


            this.checkListBox.Width = this.frmCheckList.Width;
            this.checkListBox.Height = this.frmCheckList.Height - lbSelectNo.Height - textBox1.Height;
            checkListBoxTmp.Size = checkListBox.Size;

            ReloationGrip();


        }
        /// <summary>
        /// 单价下拉框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnSelect_Click(object sender, EventArgs e)
        {
            if (this.frmCheckList.Visible == false)
            {
                Rectangle rec = this.RectangleToScreen(this.ClientRectangle);
                this.frmCheckList.Location = new Point(rec.X, rec.Y + this.pnlBack.Height);
                this.frmCheckList.Show();
                this.textBox1.Text = "";
                this.frmCheckList.BringToFront();

                ReloationGrip();
            }
            else
                this.frmCheckList.Hide();
        }

        //全选事件
        private void lbSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkListBox.Items.Count; i++)
            {
                checkListBox.SetItemChecked(i, true);
            }
            tbSelectedValue.Text = "已选择" + checkListBox.Items.Count.ToString() + "项";
        }
        //取消
        private void lbSelectNo_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkListBox.Items.Count; i++)
            {
                checkListBox.SetItemChecked(i, false);
            }
            tbSelectedValue.Text = "已选择0项";
        }

        private void checkListBox_LostFocus(object sender, EventArgs e)
        {
            //如果鼠标位置在下拉框按钮的以为地方，则隐藏下拉框
            if (!this.btnSelect.RectangleToScreen(this.btnSelect.ClientRectangle).Contains(Cursor.Position))
            {
                frmCheckList.Hide();
            }
        }

        private void checkListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (ItemClick != null)
            {
                ItemClick(sender, e);
            }
            //获取选中的数量
            int nCount = this.checkListBox.CheckedItems.Count;
            if (this.checkListBox.CheckedItems.Contains(this.checkListBox.Items[e.Index]))
            {

                if (e.NewValue != CheckState.Checked)
                {
                    nCount--;
                }
            }
            else
            {
                if (e.NewValue == CheckState.Checked)
                {
                    nCount++;
                }
            }
            tbSelectedValue.Text = "已选择" + nCount.ToString() + "项";

        }
        #region 获取与加载被选中项的索引
        public List<int> GetItemCheckIndex()
        {
            List<int> checkIndexs = new List<int>();
            for (int i = 0; i < checkListBox.Items.Count; i++)
            {
                if (checkListBox.GetItemChecked(i))
                    checkIndexs.Add(i);
            }
            return checkIndexs;
        }
        public List<string> GetItemCheckText()
        {
            List<string> checkTexts = new List<string>();
            foreach (string selectItem in checkListBox.CheckedItems)
                checkTexts.Add(selectItem);
            return checkTexts;
        }
        public void LoadItemCheckIndex(int[] checkIndexs)
        {
            foreach (int index in checkIndexs)
                checkListBox.SetItemChecked(index, true);
            if (checkIndexs.Count() > 0)
                tbSelectedValue.Text = "已选择" + checkIndexs.Count().ToString() + "项";
        }
        #endregion

        /// <summary>
        /// 鼠标按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbGrip_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int offsetX = System.Math.Abs(Cursor.Position.X - frmCheckList.RectangleToScreen(this.frmCheckList.ClientRectangle).Right);
                int offsetY = System.Math.Abs(Cursor.Position.Y - frmCheckList.RectangleToScreen(this.frmCheckList.ClientRectangle).Bottom);
                this.DragOffset = new Point(offsetX, offsetY);
            }
        }

        /// <summary>
        /// 鼠标移动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbGrip_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //获取拉伸长度
                int curWidth = Cursor.Position.X - frmCheckList.Location.X;
                int curHeight = Cursor.Position.Y - frmCheckList.Location.Y;
                if (curWidth < this.Width)
                {
                    curWidth = this.Width;
                }

                if (curHeight < checkListBox.Height)
                {
                    curHeight = checkListBox.Height;
                }

                this.frmCheckList.Size = new Size(this.Width, curHeight);
                this.pnlCheck.Size = frmCheckList.Size;
                this.checkListBox.Height = (this.frmCheckList.Height - lbGrip.Height) < 50 ? 50 : this.frmCheckList.Height - lbGrip.Height;

                ReloationGrip();
                SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
                SetStyle(ControlStyles.ResizeRedraw, true);
                SetStyle(ControlStyles.UserPaint, true);
                SetStyle(ControlStyles.AllPaintingInWmPaint, true);



            }
        }

        #endregion

        /// <summary>
        /// 设置数据源
        /// </summary>
        public object DataSource
        {
            set
            {
                this.checkListBox.DataSource = value;
            }
            get
            {
                return checkListBox.DataSource;
            }
        }
        /// <summary>
        /// 设置值
        /// </summary>
        public string ValueMember
        {
            set
            {
                checkListBox.ValueMember = value;
            }
        }
        /// <summary>
        /// 设置显示名称
        /// </summary>
        public string DisplayMember
        {
            set
            {
                checkListBox.DisplayMember = value;
            }
        }

        /// <summary>
        /// 添加项
        /// </summary>
        public int AddItems(object value)
        {
            checkListBox.Items.Add(value);
            return checkListBox.Items.Count;
        }

        /// <summary>
        /// 选项集合
        /// </summary>
        public CheckedListBox.ObjectCollection Items
        {
            get
            {
                return checkListBox.Items;
            }
        }

        /// <summary>
        /// 获取选中项的文本
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string GetItemText(object item)
        {
            return checkListBox.GetItemText(item);
        }
    }

    /// <summary>
    /// 重写LABEL
    /// </summary>
    public class LabelS : Label
    {
        public LabelS()
        {
            //控件绘制的时候减少闪烁
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            System.Windows.Forms.ControlPaint.DrawSizeGrip(e.Graphics, Color.Black, 1, 0, this.Size.Width, this.Size.Height);
        }
    }

    /// <summary>
    /// 重写BUTTON
    /// </summary>
    public class ButtonS : Button
    {
        public ButtonS()
        {
            //防止重绘控件出现闪烁
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        }
        ButtonState state;
        //当按钮被按下
        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            state = ButtonState.Pushed;
            base.OnMouseDown(mevent);
        }

        //当按钮被释放
        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            state = ButtonState.Normal;
            base.OnMouseUp(mevent);
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            System.Windows.Forms.ControlPaint.DrawComboButton(pevent.Graphics, 0, 0, this.Width, this.Height, state);
        }
    }


}
