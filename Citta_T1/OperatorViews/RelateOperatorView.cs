using Citta_T1.Business.Model;
using Citta_T1.Business.Option;
using Citta_T1.Controls.Move.Op;
using Citta_T1.Core;
using Citta_T1.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Citta_T1.OperatorViews
{
    public partial class RelateOperatorView : Form
    {
        private MoveOpControl opControl;
        private string dataPath0;
        private string dataPath1;
        private string[] columnName0;
        private string[] columnName1;
        private List<int> oldOutList0;
        private List<int> oldOutList1;
        private string oldOptionDict;
        private List<string> selectColumn;
        private List<string> oldColumnName0;
        private List<string> oldColumnName1;
        private List<string> outColumnName0;
        private List<string> outColumnName1;
        private OptionInfoCheck optionInfoCheck;

        public RelateOperatorView(MoveOpControl opControl)
        {
            InitializeComponent();
            this.optionInfoCheck = new OptionInfoCheck();
            this.outColumnName0 = new List<string>();
            this.outColumnName1= new List<string>();
            this.oldOutList0 = new List<int>();
            this.oldOutList1 = new List<int>();
            oldColumnName0 = new List<string>();
            oldColumnName1 = new List<string>();
            dataPath0 = "";
            dataPath1 = "";
            this.opControl = opControl;
            this.oldOptionDict = string.Join(",", this.opControl.Option.OptionDict.ToList());
            InitOptionInfo();
            LoadOption();
           
            SetTextBoxName(this.dataSource0);
            SetTextBoxName(this.dataSource1);
            this.comboBox1.Leave += new System.EventHandler(optionInfoCheck.Control_Leave);
            this.comboBox1.KeyUp += new System.Windows.Forms.KeyEventHandler(optionInfoCheck.Control_KeyUp);
            this.comboBox2.Leave += new System.EventHandler(optionInfoCheck.Control_Leave);
            this.comboBox2.KeyUp += new System.Windows.Forms.KeyEventHandler(optionInfoCheck.Control_KeyUp);
            //selectindex会在某些不确定情况触发，这种情况是不期望的
            this.comboBox1.SelectionChangeCommitted += new System.EventHandler(Global.GetOptionDao().GetSelectedItemIndex);
            this.comboBox2.SelectionChangeCommitted += new System.EventHandler(Global.GetOptionDao().GetSelectedItemIndex);

        }
        #region 配置初始化
        private void InitOptionInfo()
        {
            //获取两个数据源表头字段
            Dictionary<string, string> dataInfo = Global.GetOptionDao().GetDataSourceInfo(this.opControl.ID, false);
            if (dataInfo.ContainsKey("dataPath0") && dataInfo.ContainsKey("encoding0"))
            {
                this.dataPath0 = dataInfo["dataPath0"];
                this.dataSource0.Text = Path.GetFileNameWithoutExtension(this.dataPath0);
                this.toolTip1.SetToolTip(this.dataSource0, this.dataSource0.Text);
                this.columnName0 = SetOption(this.dataPath0, this.dataSource0.Text, dataInfo["encoding0"], dataInfo["separator0"].ToCharArray());
            }
            if (dataInfo.ContainsKey("dataPath1") && dataInfo.ContainsKey("encoding1"))
            {
                this.dataPath1 = dataInfo["dataPath1"];
                this.dataSource1.Text = Path.GetFileNameWithoutExtension(dataInfo["dataPath1"]);
                this.toolTip2.SetToolTip(this.dataSource1, this.dataSource1.Text);
                this.columnName1 = SetOption(this.dataPath1, this.dataSource1.Text, dataInfo["encoding1"], dataInfo["separator1"].ToCharArray());
            }

            this.opControl.DoubleDataSourceColumns["0"] = this.columnName0.ToList();
            this.opControl.DoubleDataSourceColumns["1"] = this.columnName1.ToList();
          

            foreach (string name in this.columnName0)
            {
                this.comboBox1.Items.Add(name);
                this.OutList0.AddItems(name);
            }
            foreach (string name in this.columnName1)
            {
                this.comboBox2.Items.Add(name);
                this.OutList1.AddItems(name);
            }
                
        }

        private string[] SetOption(string path, string dataName, string encoding, char[] separator)
        {

            BcpInfo bcpInfo = new BcpInfo(path, dataName, ElementType.Empty, EnType(encoding));
            string column = bcpInfo.columnLine;
            string[] columnName = column.Split(separator);
            this.opControl.SingleDataSourceColumns = column;
            return columnName;
        }
        private DSUtil.Encoding EnType(string type)
        { return (DSUtil.Encoding)Enum.Parse(typeof(DSUtil.Encoding), type); }
        public void SetTextBoxName(TextBox textBox)
        {
            string dataName = textBox.Text;
            int maxLength = 18;
            MatchCollection chs = Regex.Matches(dataName, "[\u4E00-\u9FA5]");
            int sumcount = chs.Count * 2;
            int sumcountDigit = Regex.Matches(dataName, "[a-zA-Z0-9]").Count;

            //防止截取字符串时中文乱码
            foreach (Match mc in chs)
            {
                if (dataName.IndexOf(mc.ToString()) == maxLength)
                {
                    maxLength -= 1;
                    break;
                }
            }

            if (sumcount + sumcountDigit > maxLength)
            {
                textBox.Text = System.Text.Encoding.GetEncoding("GB2312").GetString(System.Text.Encoding.GetEncoding("GB2312").GetBytes(dataName), 0, maxLength) + "...";
            }
        }
        #endregion
        #region 保存加载
        private void InitNewFactorControl(int count)
        {
            for (int line = 0; line < count; line++)
            {
                this.tableLayoutPanel1.RowCount++;
                this.tableLayoutPanel1.Height = this.tableLayoutPanel1.RowCount * 40;
                this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40));
                createLine(line);
            }
        }
        private void LoadOption()
        {
            int count = this.opControl.Option.KeysCount("factor");
            string factor1 = this.opControl.Option.GetOption("factor1");
            if (this.opControl.Option.GetOption("outfield0") != "" && Global.GetOptionDao().IsDoubleDataSourceChange(this.opControl, this.columnName0, null, "outfield0"))
            {
                string[] checkIndexs = this.opControl.Option.GetOption("outfield0").Split(',');
                int[] indexs = Array.ConvertAll<string, int>(checkIndexs, int.Parse);
                this.oldOutList0 = indexs.ToList();
                this.OutList0.LoadItemCheckIndex(indexs);
                foreach (int index in indexs)
                    this.oldColumnName0.Add(this.OutList0.Items[index].ToString());
            }
            if (this.opControl.Option.GetOption("outfield1") != "" && Global.GetOptionDao().IsDoubleDataSourceChange(this.opControl, this.columnName1, null, "outfield1"))
            {
                string[] checkIndexs = this.opControl.Option.GetOption("outfield1").Split(',');
                int[] indexs = Array.ConvertAll<string, int>(checkIndexs, int.Parse);
                this.oldOutList1 = indexs.ToList();
                this.OutList1.LoadItemCheckIndex(indexs);              
                foreach (int index in indexs)
                    this.oldColumnName1.Add(this.OutList1.Items[index].ToString());
            }
            if (factor1 != "")
            {
                string[] factorList = factor1.Split(',');
                int[] Nums = Array.ConvertAll<string, int>(factorList, int.Parse);
                List<int> fieldColumn = new List<int>(Nums);
                if (Global.GetOptionDao().IsDoubleDataSourceChange(this.opControl, this.columnName0, this.columnName1, "factor1", fieldColumn))
                {
                    this.comboBox1.Text = this.comboBox1.Items[Nums[0]].ToString();
                    this.comboBox2.Text = this.comboBox2.Items[Nums[1]].ToString();
                    this.comboBox1.Tag = Nums[0].ToString();
                    this.comboBox2.Tag = Nums[1].ToString();
                }

            }
            if (count > 1)
                InitNewFactorControl(count - 1);
            else
            {
                this.opControl.Option.SetOption("columnname0", String.Join("\t", this.opControl.DoubleDataSourceColumns["0"]));
                this.opControl.Option.SetOption("columnname1", String.Join("\t", this.opControl.DoubleDataSourceColumns["1"]));
                return;
            }
            for (int i = 2; i < (count + 1); i++)
            {
                string factor = this.opControl.Option.GetOption("factor" + i.ToString());
                if (factor == "") continue;
                string[] factorList = factor.Split(',');
                int[] Nums = Array.ConvertAll<string, int>(factorList, int.Parse);
                List<int> fieldColumn = new List<int>(Nums.Skip(1));
                if (!Global.GetOptionDao().IsDoubleDataSourceChange(this.opControl, this.columnName0, this.columnName1, "factor" + i.ToString(), fieldColumn)) continue;

                Control control1 = (Control)this.tableLayoutPanel1.Controls[(i - 2) * 6 + 0];
                control1.Text = (control1 as ComboBox).Items[Nums[0]].ToString();
                Control control2 = (Control)this.tableLayoutPanel1.Controls[(i - 2) * 6 + 1];
                control2.Text = (control2 as ComboBox).Items[Nums[1]].ToString();
                Control control3 = (Control)this.tableLayoutPanel1.Controls[(i - 2) * 6 + 3];
                control3.Text = (control3 as ComboBox).Items[Nums[2]].ToString();
                control1.Tag = Nums[0].ToString();
                control2.Tag = Nums[1].ToString();
                control3.Tag = Nums[2].ToString();
            }
            this.opControl.Option.SetOption("columnname0", String.Join("\t", this.opControl.DoubleDataSourceColumns["0"]));
            this.opControl.Option.SetOption("columnname1", String.Join("\t", this.opControl.DoubleDataSourceColumns["1"]));
        }
        private void SaveOption()
        {
            this.opControl.Option.OptionDict.Clear();
            this.opControl.Option.SetOption("columnname0", String.Join("\t", this.opControl.DoubleDataSourceColumns["0"]));
            this.opControl.Option.SetOption("columnname1", String.Join("\t", this.opControl.DoubleDataSourceColumns["1"]));

            List<int> checkIndexs0 = this.OutList0.GetItemCheckIndex();
            List<int> outIndexs0 = new List<int>(this.oldOutList0);
          
            Global.GetOptionDao().UpdateOutputCheckIndexs(checkIndexs0, outIndexs0);
            foreach (int index in outIndexs0)
                this.outColumnName0.Add(this.OutList0.Items[index].ToString());
            string outField0 = string.Join(",", outIndexs0);
            this.opControl.Option.SetOption("outfield0", outField0);

            List<int> checkIndexs1 = this.OutList1.GetItemCheckIndex();
            List<int> outIndexs1 = new List<int>(this.oldOutList1);
            Global.GetOptionDao().UpdateOutputCheckIndexs(checkIndexs1, outIndexs1);
            foreach (int index in outIndexs1)
                this.outColumnName1.Add(this.OutList1.Items[index].ToString());
            string outField1 = string.Join(",", outIndexs1);
            this.opControl.Option.SetOption("outfield1", outField1);

            string index01 = this.comboBox1.Tag == null ? this.comboBox1.SelectedIndex.ToString() : this.comboBox1.Tag.ToString();
            string index02 = this.comboBox2.Tag == null ? this.comboBox2.SelectedIndex.ToString() : this.comboBox2.Tag.ToString();
            string factor1 = index01 + "," + index02;
            this.opControl.Option.SetOption("factor1", factor1);
            if (this.tableLayoutPanel1.RowCount > 0)
            {
                for (int i = 0; i < this.tableLayoutPanel1.RowCount; i++)
                {
                    Control control1 = (Control)this.tableLayoutPanel1.Controls[i * 6 + 0];
                    Control control2 = (Control)this.tableLayoutPanel1.Controls[i * 6 + 1];
                    Control control3 = (Control)this.tableLayoutPanel1.Controls[i * 6 + 3];
                    string index1 = (control1 as ComboBox).Tag == null ? (control1 as ComboBox).SelectedIndex.ToString() : (control1 as ComboBox).Tag.ToString();
                    string index2 = (control2 as ComboBox).Tag == null ? (control2 as ComboBox).SelectedIndex.ToString() : (control2 as ComboBox).Tag.ToString();
                    string index3 = (control3 as ComboBox).Tag == null ? (control3 as ComboBox).SelectedIndex.ToString() : (control3 as ComboBox).Tag.ToString();
                    string factor = index1 + "," + index2 + "," + index3;
              
                    this.opControl.Option.SetOption("factor" + (i + 2).ToString(), factor);
                }
            }
          
            if (this.oldOptionDict == string.Join(",", this.opControl.Option.OptionDict.ToList()) && this.opControl.Status != ElementStatus.Null)
                return;
            else
                this.opControl.Status = ElementStatus.Ready;

        }
        #endregion
        #region 添加取消
        private void confirmButton_Click(object sender, EventArgs e)
        {
            bool empty = IsOptionReay();
            if (empty) return;
            SaveOption();
            this.DialogResult = DialogResult.OK;
            //内容修改，引起文档dirty
            if (this.oldOptionDict != string.Join(",", this.opControl.Option.OptionDict.ToList()))
                Global.GetMainForm().SetDocumentDirty();
            //生成结果控件,创建relation,bcp结果文件
           
            ModelElement resultElement = Global.GetCurrentDocument().SearchResultElement(this.opControl.ID);
            if (resultElement == ModelElement.Empty)
            {
                this.selectColumn = this.OutList0.GetItemCheckText().Concat(this.OutList1.GetItemCheckText()).ToList();
                Global.GetCreateMoveRsControl().CreateResultControl(this.opControl, this.selectColumn);
                return;
            }

            // 对应的结果文件置脏
            BCPBuffer.GetInstance().SetDirty(resultElement.GetFullFilePath());

            //输出变化，重写BCP文件
            List<string> oldName = this.oldColumnName0.Concat(this.oldColumnName1).ToList();
            if (!oldName.SequenceEqual(this.outColumnName0.Concat(this.outColumnName1)))
                Global.GetOptionDao().IsModifyDoubleOut(this.oldColumnName0,this.outColumnName0,this.oldColumnName1,this.outColumnName1, this.opControl.ID);
            
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }
        private bool IsOptionReay()
        {
            bool empty = false;
            List<string> types = new List<string>();
            types.Add(this.comboBox1.GetType().Name);
            types.Add(this.OutList0.GetType().Name);
            foreach (Control ctl in this.tableLayoutPanel2.Controls)
            {
                if (types.Contains(ctl.GetType().Name) && ctl.Text == "")
                {
                    MessageBox.Show("请填写连接条件");
                    empty = true;
                    return empty;
                }
            }
            foreach (Control ctl in this.tableLayoutPanel1.Controls)
            {
                if (types.Contains(ctl.GetType().Name) && ctl.Text == "")
                {
                    MessageBox.Show("请填写连接条件");
                    empty = true;
                    return empty;
                }
            }
            if (this.OutList0.GetItemCheckIndex().Count == 0 || this.OutList1.GetItemCheckIndex().Count == 0)
            {
                MessageBox.Show("请填写输出字段");
                empty = true;
                return empty;
            }
            return empty;
        }
        #endregion


        private void createLine(int addLine)
        {
            // 添加控件
            ComboBox regBox = new ComboBox();
            regBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            regBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            regBox.Font = new Font("微软雅黑", 8f, FontStyle.Regular);
            regBox.Anchor = AnchorStyles.None;
            regBox.Items.AddRange(new object[] {
            "AND",
            "OR"});
            regBox.Leave += new System.EventHandler(optionInfoCheck.Control_Leave);
            regBox.KeyUp += new System.Windows.Forms.KeyEventHandler(optionInfoCheck.Control_KeyUp);
            regBox.SelectionChangeCommitted += new System.EventHandler(Global.GetOptionDao().GetSelectedItemIndex);
            this.tableLayoutPanel1.Controls.Add(regBox, 0, addLine);

            ComboBox dataBox = new ComboBox();
            dataBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            dataBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            dataBox.Font = new Font("微软雅黑", 8f, FontStyle.Regular);
            dataBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            dataBox.Items.AddRange(this.columnName0);
            dataBox.Leave += new System.EventHandler(optionInfoCheck.Control_Leave);
            dataBox.KeyUp += new System.Windows.Forms.KeyEventHandler(optionInfoCheck.Control_KeyUp);
            dataBox.SelectionChangeCommitted += new System.EventHandler(Global.GetOptionDao().GetSelectedItemIndex);
            this.tableLayoutPanel1.Controls.Add(dataBox, 1, addLine);

            Label label = new Label();
            label.Font = new Font("微软雅黑", 8f, FontStyle.Regular);
            label.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            label.Text = "等于=";
            label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.tableLayoutPanel1.Controls.Add(label, 2, addLine);

            ComboBox data2box = new ComboBox();
            data2box.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            data2box.AutoCompleteSource = AutoCompleteSource.ListItems;
            data2box.Font = new Font("微软雅黑", 8f, FontStyle.Regular);
            data2box.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            data2box.Items.AddRange(this.columnName1);
            data2box.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            data2box.Leave += new System.EventHandler(optionInfoCheck.Control_Leave);
            data2box.KeyUp += new System.Windows.Forms.KeyEventHandler(optionInfoCheck.Control_KeyUp);
            data2box.SelectionChangeCommitted += new System.EventHandler(Global.GetOptionDao().GetSelectedItemIndex);
            this.tableLayoutPanel1.Controls.Add(data2box, 3, addLine);

            Button addButton1 = new Button();
            addButton1.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            addButton1.FlatAppearance.BorderSize = 0;
            addButton1.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            addButton1.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            addButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            addButton1.BackColor = System.Drawing.SystemColors.Control;
            addButton1.BackgroundImage = global::Citta_T1.Properties.Resources.add;
            addButton1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            addButton1.Click += new System.EventHandler(this.add_Click);
            addButton1.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            addButton1.Name = addLine.ToString();
            addButton1.UseVisualStyleBackColor = true;
            this.tableLayoutPanel1.Controls.Add(addButton1, 4, addLine);

            Button delButton1 = new Button();
            delButton1.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            delButton1.FlatAppearance.BorderSize = 0;
            delButton1.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            delButton1.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            delButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            delButton1.BackColor = System.Drawing.SystemColors.Control;
            delButton1.UseVisualStyleBackColor = true;
            delButton1.BackgroundImage = global::Citta_T1.Properties.Resources.div;
            delButton1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            delButton1.Click += new System.EventHandler(this.del_Click);
            delButton1.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            delButton1.Name = addLine.ToString();
            this.tableLayoutPanel1.Controls.Add(delButton1, 5, addLine);
        }

        private void add_Click(object sender, EventArgs e)
        {
            Button tmp = (Button)sender;
            int addLine;
            if (this.tableLayoutPanel1.RowCount == 0)
            {
                this.tableLayoutPanel1.RowCount++;
                this.tableLayoutPanel1.Height = this.tableLayoutPanel1.RowCount * 40;
                this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40));
                addLine = 0;
                createLine(addLine);
            }
            else
            {
                if (tmp.Name == "button1")
                    addLine = 0;
                else
                    addLine = int.Parse(tmp.Name) + 1;

                this.tableLayoutPanel1.RowCount++;
                this.tableLayoutPanel1.Height = this.tableLayoutPanel1.RowCount * 40;

                this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40));
                for (int k = this.tableLayoutPanel1.RowCount - 2; k >= addLine; k--)
                {
                    Control ctlNext = this.tableLayoutPanel1.GetControlFromPosition(0, k);
                    this.tableLayoutPanel1.SetCellPosition(ctlNext, new TableLayoutPanelCellPosition(0, k + 1));
                    Control ctlNext1 = this.tableLayoutPanel1.GetControlFromPosition(1, k);
                    this.tableLayoutPanel1.SetCellPosition(ctlNext1, new TableLayoutPanelCellPosition(1, k + 1));
                    Control ctlNext2 = this.tableLayoutPanel1.GetControlFromPosition(2, k);
                    this.tableLayoutPanel1.SetCellPosition(ctlNext2, new TableLayoutPanelCellPosition(2, k + 1));
                    Control ctlNext3 = this.tableLayoutPanel1.GetControlFromPosition(3, k);
                    this.tableLayoutPanel1.SetCellPosition(ctlNext3, new TableLayoutPanelCellPosition(3, k + 1));
                    Control ctlNext4 = this.tableLayoutPanel1.GetControlFromPosition(4, k);
                    ctlNext4.Name = (k + 1).ToString();
                    this.tableLayoutPanel1.SetCellPosition(ctlNext4, new TableLayoutPanelCellPosition(4, k + 1));
                    Control ctlNext5 = this.tableLayoutPanel1.GetControlFromPosition(5, k);
                    ctlNext5.Name = (k + 1).ToString();
                    this.tableLayoutPanel1.SetCellPosition(ctlNext5, new TableLayoutPanelCellPosition(5, k + 1));
                }
                createLine(addLine);
            }

        }

        private void del_Click(object sender, EventArgs e)
        {
            Button tmp = (Button)sender;
            int delLine = int.Parse(tmp.Name);

            for (int i = 0; i < this.tableLayoutPanel1.RowCount; i++)
            {
                Control bt1 = this.tableLayoutPanel1.Controls[(i * 6) + 5];
                if (bt1.Name == tmp.Name)
                {
                    for (int j = (i * 6) + 5; j >= (i * 6); j--)
                    {
                        this.tableLayoutPanel1.Controls.RemoveAt(j);
                    }
                    break;
                }

            }

            for (int k = delLine; k < this.tableLayoutPanel1.RowCount - 1; k++)
            {
                Control ctlNext = this.tableLayoutPanel1.GetControlFromPosition(0, k + 1);
                this.tableLayoutPanel1.SetCellPosition(ctlNext, new TableLayoutPanelCellPosition(0, k));
                Control ctlNext1 = this.tableLayoutPanel1.GetControlFromPosition(1, k + 1);
                this.tableLayoutPanel1.SetCellPosition(ctlNext1, new TableLayoutPanelCellPosition(1, k));
                Control ctlNext2 = this.tableLayoutPanel1.GetControlFromPosition(2, k + 1);
                this.tableLayoutPanel1.SetCellPosition(ctlNext2, new TableLayoutPanelCellPosition(2, k));
                Control ctlNext3 = this.tableLayoutPanel1.GetControlFromPosition(3, k + 1);
                this.tableLayoutPanel1.SetCellPosition(ctlNext3, new TableLayoutPanelCellPosition(3, k));
                Control ctlNext4 = this.tableLayoutPanel1.GetControlFromPosition(4, k + 1);
                ctlNext4.Name = k.ToString();
                this.tableLayoutPanel1.SetCellPosition(ctlNext4, new TableLayoutPanelCellPosition(4, k));
                Control ctlNext5 = this.tableLayoutPanel1.GetControlFromPosition(5, k + 1);
                ctlNext5.Name = k.ToString();
                this.tableLayoutPanel1.SetCellPosition(ctlNext5, new TableLayoutPanelCellPosition(5, k));
            }
            this.tableLayoutPanel1.RowStyles.RemoveAt(this.tableLayoutPanel1.RowCount - 1);
            this.tableLayoutPanel1.RowCount = this.tableLayoutPanel1.RowCount - 1;

            this.tableLayoutPanel1.Height = this.tableLayoutPanel1.RowCount * 40;

        }


        private void DataInfoBox2_LostFocus(object sender, EventArgs e)
        {
            SetTextBoxName(this.dataSource1);
        }

        private void dataSource0_MouseClick(object sender, MouseEventArgs e)
        {
            this.dataSource0.Text = Path.GetFileNameWithoutExtension(this.dataPath0);
        }

        private void DataInfoBox_LostFocus(object sender, EventArgs e)
        {
            SetTextBoxName(this.dataSource0);
        }

        private void dataSource1_MouseClick(object sender, MouseEventArgs e)
        {
            this.dataSource1.Text = Path.GetFileNameWithoutExtension(this.dataPath1);
        }
    }
}
