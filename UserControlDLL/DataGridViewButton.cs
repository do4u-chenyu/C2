using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UserControlDLL
{
    public partial class DataGridViewButton : DataGridView
    {
        private Array _ShowButtonColumns;   //有按钮的列名称

        /// <summary>
        /// 设置要显示按钮的列
        /// </summary>
        /// <param name="ShowButtonColumns"></param>
        public void SetParam(Array ShowButtonColumns)
        {
            _ShowButtonColumns = ShowButtonColumns;
        }

        public DataGridViewButton()
        {
            InitializeComponent();
            this.Controls.Add(button1);
        }

        /// <summary>
        /// 数组中是否有与指定值相等的元素
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="ShowButtonColumns"></param>
        /// <returns></returns>
        private bool IsShowButtonColumn(string columnName, Array ShowButtonColumns)
        {
            if (string.IsNullOrEmpty(columnName) || ShowButtonColumns == null || ShowButtonColumns.Length < 1) return false;

            foreach (string astr in ShowButtonColumns)
                if (astr == columnName) return true;

            return false;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        private void DataGridViewButton_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (IsShowButtonColumn(this.Columns[this.CurrentCell.ColumnIndex].Name, _ShowButtonColumns))
            {

                Point p = new Point();

                if (this.button1.Height != this.Rows[this.CurrentCell.RowIndex].Height)
                {
                    this.button1.Height = this.Rows[this.CurrentCell.RowIndex].Height;
                }
                #region 获取X轴的位置


                if (this.RowHeadersVisible)
                {
                    //判断该类是否包含行标题,如果该列包含行标题，按钮的横坐标位置等于当前位置加上行标题
                    p.X += this.RowHeadersWidth;
                }
                //FirstDisplayedCell表示左上角第一个单元格
                for (int i = this.FirstDisplayedCell.ColumnIndex; i <= this.CurrentCell.ColumnIndex; i++)
                {
                    if (this.Columns[i].Visible)
                    {
                        //当前位置=单元格的宽度加上分隔符发宽度
                        p.X += this.Columns[i].Width + this.Columns[i].DividerWidth;
                    }
                }

                p.X -= this.FirstDisplayedScrollingColumnHiddenWidth;
                p.X -= this.button1.Width;
                #endregion

                #region 获取Y轴位置

                if (this.ColumnHeadersVisible)
                {
                    //如果列表题可见，按钮的初始纵坐标位置等于当前位置加上列标题
                    p.Y += this.ColumnHeadersHeight;
                }

                //获取或设置某一列的索引，该列是显示在 DataGridView 上的第一列
                for (int i = this.FirstDisplayedScrollingRowIndex; i < this.CurrentCell.RowIndex; i++)
                {
                    if (this.Rows[i].Visible)
                    {
                        p.Y += this.Rows[i].Height + this.Rows[i].DividerHeight;
                    }
                }

                #endregion

                this.button1.Location = p;
                this.button1.Visible = true;
            }
            else
            {
                this.button1.Visible = false;
            }
        }

        private void DataGridViewButton_Scroll(object sender, ScrollEventArgs e)
        {
            this.button1.Visible = false;
        }

        //定义按钮的单击事件
        public delegate void ButtonClick();
        public event ButtonClick ButtonSelectClick;

        private void button1_Click(object sender, EventArgs e)
        {
            this.ButtonSelectClick.DynamicInvoke(null);
        }
    }
}
