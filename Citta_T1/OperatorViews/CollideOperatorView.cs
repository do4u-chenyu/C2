using Citta_T1.Business.Option;
using System;
using System.Windows.Forms;
using Citta_T1.Controls.Move;
using Citta_T1.Utils;
using System.Collections.Generic;
using System.IO;
using Citta_T1.Business.Model;
using System.Linq;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Runtime.CompilerServices;
using Citta_T1.Core;

namespace Citta_T1.OperatorViews
{
   
    public partial class CollideOperatorView : Form
    {
        
        private MoveOpControl opControl;
        private string dataPath0;
        private string dataPath1;
        private string[] columnName0;
        private string[] columnName1;
        private string oldOptionDict;
        private List<int> oldOutList;
        private List<string> selectColumn;
        private List<string> oldColumnName;
        private static LogUtil log = LogUtil.GetInstance("CollideOperatorView");


        public CollideOperatorView(MoveOpControl opControl)
        {
            InitializeComponent();
            this.oldOutList = new List<int>();
            oldColumnName = new List<string>();
            this.opControl = opControl;
            this.columnName0 = new string[] { };
            this.columnName1 = new string[] { };         
            this.oldOptionDict = string.Join(",", this.opControl.Option.OptionDict.ToList());
            InitOptionInfo();            
            LoadOption();
           

            SetTextBoxName(this.dataSource0);
            SetTextBoxName(this.dataSource1);

            //selectindex会在某些不确定情况触发，这种情况是不期望的
            this.comboBox1.SelectionChangeCommitted += new System.EventHandler(Global.GetOptionDao().GetSelectedItemIndex);
            this.comboBox2.SelectionChangeCommitted += new System.EventHandler(Global.GetOptionDao().GetSelectedItemIndex);

        }
        #region 初始化配置
        private void InitOptionInfo()
        {
            Dictionary<string, string> dataInfo = Global.GetOptionDao().GetDataSourceInfo(this.opControl.ID, false);
            if (dataInfo.ContainsKey("dataPath0") && dataInfo.ContainsKey("encoding0"))
            {
                this.dataPath0 = dataInfo["dataPath0"];
                this.dataSource0.Text = Path.GetFileNameWithoutExtension(this.dataPath0);
                this.toolTip1.SetToolTip(this.dataSource0, this.dataSource0.Text);
                this.columnName0 = SetOption(this.dataPath0, this.dataSource0.Text, dataInfo["encoding0"], dataInfo["separator0"].ToCharArray());
                this.opControl.DoubleDataSourceColumns["0"] = this.columnName0.ToList();
                
            }
            if (dataInfo.ContainsKey("dataPath1") && dataInfo.ContainsKey("encoding1"))
            {
                this.dataPath1 = dataInfo["dataPath1"];
                this.dataSource1.Text = Path.GetFileNameWithoutExtension(dataInfo["dataPath1"]);
                this.toolTip2.SetToolTip(this.dataSource1, this.dataSource1.Text);
                this.columnName1 = SetOption(this.dataPath1, this.dataSource1.Text, dataInfo["encoding1"], dataInfo["separator1"].ToArray());
                this.opControl.DoubleDataSourceColumns["1"] = this.columnName1.ToList();
                
            }
           
            foreach (string name in this.columnName0)
            {
                this.comboBox1.Items.Add(name);
                this.OutList.AddItems(name);
            }            

            foreach (string name in this.columnName1)
                this.comboBox2.Items.Add(name);
        }

        private string[] SetOption(string path, string dataName, string encoding, char[] separator)
        {

            BcpInfo bcpInfo = new BcpInfo(path, dataName, ElementType.Null, EnType(encoding));
            string column = bcpInfo.columnLine;
            string[] columnName = column.Split(separator);
            return columnName;
        }

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
            this.selectColumn = this.OutList.GetItemCheckText();
            ModelElement hasResutl = Global.GetCurrentDocument().SearchResultOperator(this.opControl.ID);
            if (hasResutl == null)
            { 
                Global.GetOptionDao().CreateResultControl(this.opControl, this.selectColumn);
                return;
            }

            //输出变化，重写BCP文件
            List<string> outName =new List<string>();
            foreach (string index in this.opControl.Option.GetOption("outfield").Split(','))
            { outName.Add(this.columnName0[Convert.ToInt32(index)]); }
            if (hasResutl != null && String.Join(",", this.oldOutList)!= this.opControl.Option.GetOption("outfield"))
                Global.GetOptionDao().IsModifyOut(this.oldColumnName, outName, this.opControl.ID);
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
            types.Add(this.OutList.GetType().Name);
            foreach (Control ctl in this.tableLayoutPanel2.Controls)
            {
                if (types.Contains(ctl.GetType().Name) && ctl.Text == "")
                {
                    MessageBox.Show("请填写碰撞条件!");
                    empty = true;
                    return empty;
                }
            }
            foreach (Control ctl in this.tableLayoutPanel1.Controls)
            {
                if (types.Contains(ctl.GetType().Name) && ctl.Text == "")
                {
                    MessageBox.Show("请填写碰撞条件!");
                    empty = true;
                    return empty;
                }
            }
            if (this.OutList.GetItemCheckIndex().Count == 0)
            {
                MessageBox.Show("请填写输出字段!");
                empty = true;
                return empty;
            }
            return empty;
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
            if (this.opControl.Option.GetOption("outfield") != "" && Global.GetOptionDao().IsDoubleDataSourceChange(this.opControl, this.columnName0, null, "outfield"))
            {
                string[] checkIndexs = this.opControl.Option.GetOption("outfield").Split(',');
                int[] indexs = Array.ConvertAll<string, int>(checkIndexs, int.Parse);
                this.oldOutList = indexs.ToList();
                this.OutList.LoadItemCheckIndex(indexs);
                foreach (int index in indexs)
                    this.oldColumnName.Add(this.OutList.Items[index].ToString());

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
                }
                   
            }
            if (count - 1 > 0)
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

                Control control1 = (Control)this.tableLayoutPanel1.Controls[(i - 2) * 5 + 0];
                control1.Text = (control1 as ComboBox).Items[Nums[0]].ToString();
                Control control2 = (Control)this.tableLayoutPanel1.Controls[(i - 2) * 5 + 1];
                control2.Text = (control2 as ComboBox).Items[Nums[1]].ToString();
                Control control3 = (Control)this.tableLayoutPanel1.Controls[(i - 2) * 5 + 2];
                control3.Text = (control3 as ComboBox).Items[Nums[2]].ToString();
            }
            this.opControl.Option.SetOption("columnname0", String.Join("\t", this.opControl.DoubleDataSourceColumns["0"]));
            this.opControl.Option.SetOption("columnname1", String.Join("\t", this.opControl.DoubleDataSourceColumns["1"]));
        }
        
        private void SaveOption()
        {
            this.opControl.Option.OptionDict.Clear();
            this.opControl.Option.SetOption("columnname0", String.Join("\t", this.opControl.DoubleDataSourceColumns["0"]));
            this.opControl.Option.SetOption("columnname1", String.Join("\t", this.opControl.DoubleDataSourceColumns["1"]));
            List<int> checkIndexs = this.OutList.GetItemCheckIndex();
            List<int> outIndexs = new List<int>(this.oldOutList);
            Global.GetOptionDao().UpdateOutputCheckIndexs(checkIndexs, outIndexs);

            string outField = string.Join(",", outIndexs);
            this.opControl.Option.SetOption("outfield", outField);

            string index00 = comboBox1.Tag == null ? comboBox1.SelectedIndex.ToString() : comboBox1.Tag.ToString();
            string index11 = comboBox2.Tag == null ? comboBox2.SelectedIndex.ToString() : comboBox2.Tag.ToString();
            string factor1 = index00 + "," + index11;
            this.opControl.Option.SetOption("factor1", factor1);
            if (this.tableLayoutPanel1.RowCount > 0)
            {
                for (int i = 0; i < this.tableLayoutPanel1.RowCount; i++)
                {
                    Control control1 = (Control)this.tableLayoutPanel1.Controls[i * 5 + 0];
                    Control control2 = (Control)this.tableLayoutPanel1.Controls[i * 5 + 1];
                    Control control3 = (Control)this.tableLayoutPanel1.Controls[i * 5 + 2];
                    string index1= (control1 as ComboBox).Tag==null? (control1 as ComboBox).SelectedIndex.ToString(): (control1 as ComboBox).Tag.ToString();
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


        private void createLine(int addLine)
        {
            // 添加控件
            ComboBox regBox = new ComboBox();
            regBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            regBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            regBox.Anchor = AnchorStyles.None;
            regBox.Font = new Font("微软雅黑", 8f, FontStyle.Regular);
            regBox.Items.AddRange(new object[] {
            "AND",
            "OR"});
            regBox.Leave += new System.EventHandler(Global.GetOptionDao().Control_Leave);
            regBox.KeyUp += new System.Windows.Forms.KeyEventHandler(Global.GetOptionDao().Control_KeyUp); 
            regBox.SelectionChangeCommitted += new System.EventHandler(Global.GetOptionDao().GetSelectedItemIndex);
            this.tableLayoutPanel1.Controls.Add(regBox, 0, addLine);


            ComboBox dataBox = new ComboBox();
            dataBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            dataBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            dataBox.Font = new Font("微软雅黑", 8f, FontStyle.Regular);
            dataBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            dataBox.Items.AddRange(this.columnName0);
            dataBox.Leave += new System.EventHandler(Global.GetOptionDao().Control_Leave);
            dataBox.KeyUp += new System.Windows.Forms.KeyEventHandler(Global.GetOptionDao().Control_KeyUp);
            dataBox.SelectionChangeCommitted += new System.EventHandler(Global.GetOptionDao().GetSelectedItemIndex);
            this.tableLayoutPanel1.Controls.Add(dataBox, 1, addLine);

            ComboBox filterBox = new ComboBox();
            filterBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            filterBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            filterBox.Font = new Font("微软雅黑", 8f, FontStyle.Regular);
            filterBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            filterBox.Items.AddRange(this.columnName1);
            filterBox.Leave += new System.EventHandler(Global.GetOptionDao().Control_Leave);
            filterBox.KeyUp += new System.Windows.Forms.KeyEventHandler(Global.GetOptionDao().Control_KeyUp);
            filterBox.SelectionChangeCommitted += new System.EventHandler(Global.GetOptionDao().GetSelectedItemIndex);
            this.tableLayoutPanel1.Controls.Add(filterBox, 2, addLine);

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
            this.tableLayoutPanel1.Controls.Add(addButton1, 3, addLine);

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
            this.tableLayoutPanel1.Controls.Add(delButton1, 4, addLine);
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
                    ctlNext3.Name = (k + 1).ToString();
                    this.tableLayoutPanel1.SetCellPosition(ctlNext3, new TableLayoutPanelCellPosition(3, k + 1));
                    Control ctlNext4 = this.tableLayoutPanel1.GetControlFromPosition(4, k);
                    ctlNext4.Name = (k + 1).ToString();
                    this.tableLayoutPanel1.SetCellPosition(ctlNext4, new TableLayoutPanelCellPosition(4, k + 1));
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
                Control bt1 = this.tableLayoutPanel1.Controls[(i * 5) + 4];
                if (bt1.Name == tmp.Name)
                {
                    for (int j = (i * 5) + 4; j >= (i * 5); j--)
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
                ctlNext3.Name = k.ToString();
                this.tableLayoutPanel1.SetCellPosition(ctlNext3, new TableLayoutPanelCellPosition(3, k));
                Control ctlNext4 = this.tableLayoutPanel1.GetControlFromPosition(4, k + 1);
                ctlNext4.Name = k.ToString();
                this.tableLayoutPanel1.SetCellPosition(ctlNext4, new TableLayoutPanelCellPosition(4, k));
            }
            this.tableLayoutPanel1.RowStyles.RemoveAt(this.tableLayoutPanel1.RowCount - 1);
            this.tableLayoutPanel1.RowCount = this.tableLayoutPanel1.RowCount - 1;

            this.tableLayoutPanel1.Height = this.tableLayoutPanel1.RowCount * 40;

        }


        private DSUtil.Encoding EnType(string type)
        { return (DSUtil.Encoding)Enum.Parse(typeof(DSUtil.Encoding), type); }

        private void dataSource1_MouseClick(object sender, MouseEventArgs e)
        {
            this.dataSource1.Text = Path.GetFileNameWithoutExtension(this.dataPath1);
        }

        private void dataSource1_LostFocus(object sender, EventArgs e)
        {
            SetTextBoxName(this.dataSource1);
        }

        private void dataSource0_MouseClick(object sender, MouseEventArgs e)
        {
            this.dataSource0.Text = Path.GetFileNameWithoutExtension(this.dataPath0);
        }

        private void dataSource0_LostFocus(object sender, EventArgs e)
        {
            SetTextBoxName(this.dataSource0);
        }

        private void comboBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Global.GetOptionDao().IsIllegalInputName(this.comboBox1, this.columnName0, this.comboBox1.Text);
        }

        private void comboBox1_Leave(object sender, EventArgs e)
        {
            Global.GetOptionDao().IsIllegalInputName(this.comboBox1, this.columnName0, this.comboBox1.Text);
        }

        private void comboBox2_Leave(object sender, EventArgs e)
        {
            Global.GetOptionDao().IsIllegalInputName((sender as ComboBox), this.columnName1, (sender as ComboBox).Text);
        }
        
        private void comboBox2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Global.GetOptionDao().IsIllegalInputName((sender as ComboBox), this.columnName1, (sender as ComboBox).Text);
        }
       
    }
}
