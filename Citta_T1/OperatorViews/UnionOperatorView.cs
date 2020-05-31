using Citta_T1.Business.Model;
using Citta_T1.Business.Option;
using Citta_T1.Controls.Move.Op;
using Citta_T1.Core;
using Citta_T1.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Citta_T1.OperatorViews
{
    public partial class UnionOperatorView : Form
    {
        private MoveOpControl opControl;
        private string dataPath0;
        private string dataPath1;
        private string[] columnName0;
        private string[] columnName1;
        private string oldOptionDict;
        private List<string> selectColumn;
        private List<bool> oldCheckedItems = new List<bool>();
        private List<string> oldColumnName;
        private OptionInfoCheck optionInfoCheck;
        public UnionOperatorView(MoveOpControl opControl)
        {
            InitializeComponent();
            this.optionInfoCheck = new OptionInfoCheck();
            oldColumnName = new List<string>();
            this.opControl = opControl;          
            this.columnName0 = new string[] { };
            this.columnName1 = new string[] { };
            selectColumn = new List<string>();
            InitOptionInfo();
            LoadOption();
            this.oldOptionDict = string.Join(",", this.opControl.Option.OptionDict.ToList());
            this.oldCheckedItems.Add(this.noRepetition.Checked);
            this.oldCheckedItems.Add(this.repetition.Checked);
    

            SetTextBoxName(this.dataSource0);
            SetTextBoxName(this.dataSource1);
            this.comboBox1.Leave += new System.EventHandler(optionInfoCheck.Control_Leave);
            this.comboBox1.KeyUp += new System.Windows.Forms.KeyEventHandler(optionInfoCheck.Control_KeyUp);
            this.comboBox2.Leave += new System.EventHandler(optionInfoCheck.Control_Leave);
            this.comboBox2.KeyUp += new System.Windows.Forms.KeyEventHandler(optionInfoCheck.Control_KeyUp);
            this.textBoxEx1.Leave += new System.EventHandler(optionInfoCheck.IsIllegalCharacter);
            this.textBoxEx1.KeyUp += new System.Windows.Forms.KeyEventHandler(optionInfoCheck.IsIllegalCharacter);
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
                this.opControl.FirstDataSourceColumns = this.columnName0.ToList();
            }
            if (dataInfo.ContainsKey("dataPath1") && dataInfo.ContainsKey("encoding1"))
            {
                this.dataPath1 = dataInfo["dataPath1"];
                this.dataSource1.Text = Path.GetFileNameWithoutExtension(dataInfo["dataPath1"]);
                this.toolTip2.SetToolTip(this.dataSource1, this.dataSource1.Text);
                this.columnName1 = SetOption(this.dataPath1, this.dataSource1.Text, dataInfo["encoding1"], dataInfo["separator1"].ToCharArray());
                this.opControl.SecondDataSourceColumns = this.columnName1.ToList();
            }
           if(this.opControl.Option.GetOption("outname") != String.Empty)
           {
                this.oldColumnName = this.opControl.Option.GetOption("outname").Split('\t').ToList();
            }
           

            foreach (string name in this.columnName0)
                this.comboBox1.Items.Add(name);

            foreach (string name in this.columnName1)
                this.comboBox2.Items.Add(name);
        }

        private string[] SetOption(string path, string dataName, string encoding, char[] separator)
        {

            BcpInfo bcpInfo = new BcpInfo(path, dataName, ElementType.Empty, OpUtil.EncodingEnum(encoding), separator);
            return bcpInfo.ColumnArray;
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
                textBox.Text = ConvertUtil.GB2312.GetString(ConvertUtil.GB2312.GetBytes(dataName), 0, maxLength) + "...";
            }
        }
        #endregion
        #region 配置信息的保存与加载
        private void InitNewFactorControl(int count)
        {
            for (int line = 0; line < count; line++)
            {
                this.tableLayoutPanel1.RowCount++;
                this.tableLayoutPanel1.Height = this.tableLayoutPanel1.RowCount * 40;
                this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40));
                CreateLine(line);
            }
        }
        private void SaveOption()
        {
            this.opControl.Option.OptionDict.Clear();
            this.opControl.Option.SetOption("columnname0", String.Join("\t", this.opControl.FirstDataSourceColumns));
            this.opControl.Option.SetOption("columnname1", String.Join("\t", this.opControl.SecondDataSourceColumns));
            string index01 = this.comboBox1.Tag == null ? this.comboBox1.SelectedIndex.ToString() : this.comboBox1.Tag.ToString();
            string index02 = this.comboBox2.Tag == null ? this.comboBox2.SelectedIndex.ToString() : this.comboBox2.Tag.ToString();
            string factor1 = index01 + "," + index02 + "," + this.textBoxEx1.Text;
            this.opControl.Option.SetOption("factor1", factor1);
            this.selectColumn.Add(OutColumnName(this.comboBox1.Text, this.textBoxEx1.Text));
            if (this.tableLayoutPanel1.RowCount > 0)
            {
                for (int i = 0; i < this.tableLayoutPanel1.RowCount; i++)
                {
                    Control control1 = (Control)this.tableLayoutPanel1.Controls[i * 5 + 0];
                    Control control2 = (Control)this.tableLayoutPanel1.Controls[i * 5 + 1];
                    Control control3 = (Control)this.tableLayoutPanel1.Controls[i * 5 + 2];
                    string index1 = (control1 as ComboBox).Tag == null ? (control1 as ComboBox).SelectedIndex.ToString() : (control1 as ComboBox).Tag.ToString();
                    string index2 = (control2 as ComboBox).Tag == null ? (control2 as ComboBox).SelectedIndex.ToString() : (control2 as ComboBox).Tag.ToString();
                   
                    string factor = index1 + "," + index2 + "," + control3.Text;
                    this.opControl.Option.SetOption("factor" + (i + 2).ToString(), factor);
                    this.selectColumn.Add(OutColumnName((control1 as ComboBox).Text, control3.Text));
                }
            }
            this.opControl.Option.SetOption("outname", String.Join("\t", this.selectColumn));
            this.opControl.Option.SetOption("noRepetition", this.noRepetition.Checked.ToString());
            this.opControl.Option.SetOption("repetition", this.repetition.Checked.ToString());
            if (this.oldOptionDict == string.Join(",", this.opControl.Option.OptionDict.ToList()) && this.opControl.Status != ElementStatus.Null && this.opControl.Status != ElementStatus.Warn)
                return;
            else
                this.opControl.Status = ElementStatus.Ready;

        }
        private string OutColumnName(string name,string alias)
        {
            return alias == "别名" ? name : alias;
        }
        private void LoadOption()
        {
            int count = this.opControl.Option.KeysCount("factor");
            string factor1 = this.opControl.Option.GetOption("factor1");
            if (this.opControl.Option.GetOption("noRepetition") != String.Empty)
                this.noRepetition.Checked = Convert.ToBoolean(this.opControl.Option.GetOption("noRepetition"));
            if (this.opControl.Option.GetOption("repetition") != String.Empty)
                this.repetition.Checked = Convert.ToBoolean(this.opControl.Option.GetOption("repetition"));
            if (factor1 != "")
            {
                string[] factorList = factor1.Split(',');
                int[] Nums = Array.ConvertAll<string, int>(factorList.Take(factorList.Length - 1).ToArray(), int.Parse);
                bool case0 = Global.GetOptionDao().IsClearOption(this.opControl, this.columnName0, "factor1", Nums[0]);
                bool case1 = Global.GetOptionDao().IsClearOption(this.opControl, this.columnName1, "factor1", Nums[1]);
                if (!case0 && !case1)
                {
                    this.comboBox1.Text = this.comboBox1.Items[Nums[0]].ToString();
                    this.comboBox2.Text = this.comboBox2.Items[Nums[1]].ToString();
                    this.textBoxEx1.Text = factorList[2];
                    this.comboBox1.Tag = Nums[0].ToString();
                    this.comboBox2.Tag = Nums[1].ToString();
                }
            }
            if (count > 1)
                InitNewFactorControl(count - 1);
            else
            {
                this.opControl.Option.SetOption("columnname0", String.Join("\t", this.opControl.FirstDataSourceColumns));
                this.opControl.Option.SetOption("columnname1", String.Join("\t", this.opControl.SecondDataSourceColumns));
                return;
            }
            for (int i = 2; i < (count + 1); i++)
            {
                string name = "factor" + i.ToString();
                string factor = this.opControl.Option.GetOption(name);
                if (factor == "") continue;

                string[] factorList = factor.Split(',');
                int[] Nums = Array.ConvertAll<string, int>(factorList.Take(factorList.Length - 1).ToArray(), int.Parse);
                bool case0 = Global.GetOptionDao().IsClearOption(this.opControl, this.columnName0, name, Nums[0]);
                bool case1 = Global.GetOptionDao().IsClearOption(this.opControl, this.columnName1, name, Nums[1]);
                if (case0 || case1) continue;

                Control control1 = (Control)this.tableLayoutPanel1.Controls[(i - 2) * 5 + 0];
                control1.Text = (control1 as ComboBox).Items[Nums[0]].ToString();
                Control control2 = (Control)this.tableLayoutPanel1.Controls[(i - 2) * 5 + 1];
                control2.Text = (control2 as ComboBox).Items[Nums[1]].ToString();
                Control control3 = (Control)this.tableLayoutPanel1.Controls[(i - 2) * 5 + 2];
                control3.Text = factorList[2];
                control1.Tag = Nums[0].ToString();
                control2.Tag = Nums[1].ToString();
            }
            this.opControl.Option.SetOption("columnname0", String.Join("\t", this.opControl.FirstDataSourceColumns));
            this.opControl.Option.SetOption("columnname1", String.Join("\t", this.opControl.SecondDataSourceColumns));
        }


        #endregion
        #region 添加取消
        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            bool empty = IsOptionReay();
            if (empty) return;
            SaveOption();
            //判断取并条件是否有完全重复的

            var duplicateValues = this.opControl.Option.OptionDict.Where(x => x.Key.Contains("factor")).GroupBy(x => x.Value).Where(x => x.Count() > 1);
            foreach (var  item in duplicateValues)
            {
                MessageBox.Show("取并集条件存在完全重复选项,请重新选择并集条件");
                return;
            }

            this.DialogResult = DialogResult.OK;
            //内容修改，引起文档dirty
           
            if (this.oldOptionDict != string.Join(",", this.opControl.Option.OptionDict.ToList()))
                Global.GetMainForm().SetDocumentDirty();
            //生成结果控件,创建relation,bcp结果文件
            ModelElement resultElement = Global.GetCurrentDocument().SearchResultElementByOpID(this.opControl.ID);
            if (resultElement == ModelElement.Empty)
            {
                MoveRsControlFactory.GetInstance().CreateNewMoveRsControl(this.opControl, this.selectColumn);
                return;
            }
            //输出变化，重写BCP文件
            if (resultElement != null && !this.oldColumnName.SequenceEqual(this.selectColumn))
                Global.GetOptionDao().DoOutputCompare(this.oldColumnName, this.selectColumn, this.opControl.ID);
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }
        private bool IsOptionReay()
        {
            bool empty = false;
            List<string> types = new List<string>();
            types.Add(this.comboBox1.GetType().Name);
            types.Add(this.textBoxEx1.GetType().Name);
            foreach (Control ctl in this.tableLayoutPanel2.Controls)
            {
                if (types.Contains(ctl.GetType().Name) && ctl.Text == String.Empty)
                {
                    MessageBox.Show("请填写过滤条件");
                    empty = true;
                    return empty;
                }
            }
            foreach (Control ctl in this.tableLayoutPanel1.Controls)
            {
                if (types.Contains(ctl.GetType().Name) && ctl.Text == String.Empty)
                {
                    MessageBox.Show("请填写过滤条件");
                    empty = true;
                    return empty;
                }
            }
            return empty;
        }
        #endregion
        private void CreateLine(int addLine)
        {
            // 添加控件

            ComboBox dataBox = new ComboBox();
            dataBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            dataBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            dataBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            dataBox.Font = new Font("微软雅黑", 8f, FontStyle.Regular);
            dataBox.Items.AddRange(this.columnName0);
            dataBox.Leave += new System.EventHandler(optionInfoCheck.Control_Leave);
            dataBox.KeyUp += new System.Windows.Forms.KeyEventHandler(optionInfoCheck.Control_KeyUp);
            dataBox.SelectionChangeCommitted += new System.EventHandler(Global.GetOptionDao().GetSelectedItemIndex);
            this.tableLayoutPanel1.Controls.Add(dataBox, 0, addLine);

            ComboBox filterBox = new ComboBox();
            filterBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            filterBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            filterBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            filterBox.Font = new Font("微软雅黑", 8f, FontStyle.Regular);
            filterBox.Items.AddRange(this.columnName1);
            filterBox.Leave += new System.EventHandler(optionInfoCheck.Control_Leave);
            filterBox.KeyUp += new System.Windows.Forms.KeyEventHandler(optionInfoCheck.Control_KeyUp);
            filterBox.SelectionChangeCommitted += new System.EventHandler(Global.GetOptionDao().GetSelectedItemIndex);
            this.tableLayoutPanel1.Controls.Add(filterBox, 1, addLine);

            TextBox textBox = new TextBox();
            textBox.Text = "别名";
            textBox.Font = new Font("微软雅黑",9f,FontStyle.Regular);
            textBox.ForeColor = SystemColors.ActiveCaption;
            textBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            textBox.Enter += TextBoxEx1_Enter;
            textBox.Leave += TextBoxEx1_Leave;
            textBox.Leave += new System.EventHandler(optionInfoCheck.IsIllegalCharacter);
            textBox.KeyUp += new System.Windows.Forms.KeyEventHandler(optionInfoCheck.IsIllegalCharacter);
            this.tableLayoutPanel1.Controls.Add(textBox, 2, addLine);

            Button addButton1 = new Button();
            addButton1.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            addButton1.FlatAppearance.BorderSize = 0;
            addButton1.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            addButton1.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            addButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            addButton1.BackColor = System.Drawing.SystemColors.Control;
            addButton1.BackgroundImage = global::Citta_T1.Properties.Resources.add;
            addButton1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            addButton1.Click += new System.EventHandler(this.AddButton1_Click);
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
            delButton1.Click += new System.EventHandler(this.DelButton1_Click);
            delButton1.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            delButton1.Name = addLine.ToString();
            this.tableLayoutPanel1.Controls.Add(delButton1, 4, addLine);
        }

        private void AddButton1_Click(object sender, EventArgs e)
        {
            Button tmp = (Button)sender;
            int addLine;
            if (this.tableLayoutPanel1.RowCount == 0)
            {
                this.tableLayoutPanel1.RowCount++;
                this.tableLayoutPanel1.Height = this.tableLayoutPanel1.RowCount * 40;
                this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40));
                addLine = 0;
                CreateLine(addLine);
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
                CreateLine(addLine);
            }

        }

        private void DelButton1_Click(object sender, EventArgs e)
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
        private void GroupBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(this.BackColor);
        }

        private void TextBoxEx1_Enter(object sender, EventArgs e)
        {
            TextBox TextBoxEx = sender as TextBox;
            if (TextBoxEx.Text == "别名")
            {
                TextBoxEx.Text = String.Empty;
            }
            TextBoxEx.ForeColor = Color.Black;
        }

        private void TextBoxEx1_Leave(object sender, EventArgs e)
        {
            TextBox TextBoxEx = sender as TextBox;
            if (TextBoxEx.Text == String.Empty)
            {
                TextBoxEx.Text = "别名";
                TextBoxEx.ForeColor = SystemColors.ActiveCaption;
            }           
        }

        private void DataSource1_MouseClick(object sender, MouseEventArgs e)
        {
            this.dataSource1.Text = Path.GetFileNameWithoutExtension(this.dataPath1);
        }

        private void DataSource1_LostFocus(object sender, EventArgs e)
        {
            SetTextBoxName(this.dataSource1);
        }

        private void DataSource0_MouseClick(object sender, MouseEventArgs e)
        {
            this.dataSource0.Text = Path.GetFileNameWithoutExtension(this.dataPath0);
        }

        private void DataSource0_LostFocus(object sender, EventArgs e)
        {
            SetTextBoxName(this.dataSource0);
        }
      

    }
}
