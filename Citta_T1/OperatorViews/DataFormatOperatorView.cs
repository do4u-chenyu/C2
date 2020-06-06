using Citta_T1.Business.Model;
using Citta_T1.Business.Option;
using Citta_T1.Controls.Move.Op;
using Citta_T1.Core;
using Citta_T1.OperatorViews.Base;
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
    public partial class DataFormatOperatorView : BaseOperatorView
    {
        private List<string> oldOutName;
        private List<int> formatColumn;
        private List<bool> oldCheckedItems;


        public DataFormatOperatorView(MoveOpControl opControl) : base(opControl)
        {
            InitializeComponent();

            this.formatColumn = new List<int>();
            this.oldCheckedItems = new List<bool>();
            this.oldOutName = new List<string>();

            InitOptionInfo();
            LoadOption();
            this.comboBox1.Leave += new System.EventHandler(optionInfoCheck.Control_Leave);
            this.comboBox1.KeyUp += new System.Windows.Forms.KeyEventHandler(optionInfoCheck.Control_KeyUp);
            this.textBox1.Leave += new System.EventHandler(optionInfoCheck.IsIllegalCharacter);
            this.textBox1.KeyUp += new System.Windows.Forms.KeyEventHandler(optionInfoCheck.IsIllegalCharacter);
            SetTextBoxName(this.dataSourceTB0);
            //selectindex会在某些不确定情况触发，这种情况是不期望的

            this.comboBox1.SelectionChangeCommitted += new System.EventHandler(Global.GetOptionDao().GetSelectedItemIndex);
        }

        #region 初始化配置
        private void InitOptionInfo()
        {
            Dictionary<string, string> dataInfo = Global.GetOptionDao().GetDataSourceInfo(this.opControl.ID);
            if (dataInfo.ContainsKey("dataPath0") && dataInfo.ContainsKey("encoding0"))
            {
                this.dataSourceFFP0 = dataInfo["dataPath0"];
                this.dataSourceTB0.Text = Path.GetFileNameWithoutExtension(this.dataSourceFFP0);
                this.toolTip1.SetToolTip(this.dataSourceTB0, this.dataSourceTB0.Text);
                SetOption(this.dataSourceFFP0, this.dataSourceTB0.Text, dataInfo["encoding0"], dataInfo["separator0"].ToCharArray());
            }
            if (this.opControl.Option.GetOption("outname") != String.Empty)
            {
                this.oldOutName = this.opControl.Option.GetOptionSplit("outname").ToList();
            }
        }

        private void SetOption(string path, string dataName, string encoding, char[] separator)
        {
            BcpInfo bcpInfo = new BcpInfo(path, dataName, ElementType.Empty, OpUtil.EncodingEnum(encoding), separator);
            this.nowColumnsName0 = bcpInfo.ColumnArray;
            foreach (string name in this.nowColumnsName0)
                this.comboBox1.Items.Add(name);
            this.opControl.FirstDataSourceColumns = this.nowColumnsName0;
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

        private void LoadOption()
        {
            int count = this.opControl.Option.KeysCount("factor");
            string factor1 = this.opControl.Option.GetOption("factor1");
            if (factor1 != "")
            {
                string[] factorList = factor1.Split('\t');
                int[] Nums = Array.ConvertAll<string, int>(factorList.Take(factorList.Length - 1).ToArray(), int.Parse);
                bool case0 = Global.GetOptionDao().IsCleanOption(this.opControl, this.nowColumnsName0, "factor1", Nums[0]);
                if (!case0)
                {
                    this.comboBox1.Text = this.comboBox1.Items[Nums[0]].ToString();
                    this.comboBox1.Tag = Nums[0].ToString();
                    this.textBox1.Text = factorList[1];
                }
            }
            if (count > 1)
                InitNewFactorControl(count - 1);
            else
            {
                this.opControl.Option.SetOption("columnname", String.Join("\t", this.opControl.FirstDataSourceColumns));
                return;
            }
            for (int i = 2; i < (count + 1); i++)
            {
                string name = "factor" + i.ToString();
                string factor = this.opControl.Option.GetOption(name);
                if (factor == "") continue;

                string[] factorList = factor.Split('\t');
                int[] Nums = Array.ConvertAll<string, int>(factorList.Take(factorList.Length - 1).ToArray(), int.Parse);
                bool case0 = Global.GetOptionDao().IsCleanOption(this.opControl, this.nowColumnsName0, name, Nums[0]);
                if (case0) continue;

                Control control1 = this.tableLayoutPanel1.GetControlFromPosition(1, i-2);  
                Control control2 = this.tableLayoutPanel1.GetControlFromPosition(2, i-2);
                control1.Text = (control1 as ComboBox).Items[Nums[0]].ToString();
                control1.Tag = Nums[0].ToString();
                control2.Text = factorList[1];
            }
            this.opControl.Option.SetOption("columnname", String.Join("\t", this.opControl.FirstDataSourceColumns));

        }
        private void SaveOption()
        {
            this.opControl.Option.OptionDict.Clear();
            this.opControl.Option.SetOption("columnname", String.Join("\t", this.opControl.FirstDataSourceColumns));
            string index1 = comboBox1.Tag == null ? comboBox1.SelectedIndex.ToString() : comboBox1.Tag.ToString();
            string factor1 = index1 + "\t" + this.textBox1.Text;
            this.opControl.Option.SetOption("factor1", factor1);
            this.selectedColumns.Add(OutColumnName(this.comboBox1.Text, this.textBox1.Text));

            if (this.tableLayoutPanel1.RowCount > 0)
            {
                for (int i = 0; i < this.tableLayoutPanel1.RowCount; i++)
                {
                    Control control1 = this.tableLayoutPanel1.GetControlFromPosition(1, i);
                    Control control2 = this.tableLayoutPanel1.GetControlFromPosition(2, i);
                    //Control control1 = (Control)this.tableLayoutPanel1.Controls[i * 5 + 1];
                    //Control control2 = (Control)this.tableLayoutPanel1.Controls[i * 5 + 2];
                    string tmpIndex = (control1 as ComboBox).Tag == null ? (control1 as ComboBox).SelectedIndex.ToString() : (control1 as ComboBox).Tag.ToString();

                    string factor = tmpIndex + "\t" + control2.Text;
                    this.opControl.Option.SetOption("factor" + (i + 2).ToString(), factor);
                    this.selectedColumns.Add(OutColumnName((control1 as ComboBox).Text, control2.Text));
                }
            }
            this.opControl.Option.SetOption("outname", String.Join("\t", this.selectedColumns));

            ElementStatus oldStatus = this.opControl.Status;
            if (this.oldOptionDictStr != string.Join(",", this.opControl.Option.OptionDict.ToList()))
                this.opControl.Status = ElementStatus.Ready;

            if (oldStatus == ElementStatus.Done && this.opControl.Status == ElementStatus.Ready)
                Global.GetCurrentDocument().DegradeChildrenStatus(this.opControl.ID);

        }

        private string OutColumnName(string name, string alias)
        {
            return alias == "别名" ? name : alias;
        }
        #endregion

        #region 添加取消
        protected override void ConfirmButton_Click(object sender, EventArgs e)
        {

            bool empty = IsOptionReay();
            if (empty) return;
            //判断标准化字段是否重复选择
            if (IsDuplicateSelect()) return;
            SaveOption();
            this.DialogResult = DialogResult.OK;
            //内容修改，引起文档dirty
            if (this.oldOptionDictStr != string.Join(",", this.opControl.Option.OptionDict.ToList()))
                Global.GetMainForm().SetDocumentDirty();
            //生成结果控件,创建relation,bcp结果文件
            ModelElement resultElement = Global.GetCurrentDocument().SearchResultElementByOpID(this.opControl.ID);
            if (resultElement == ModelElement.Empty)
            {
                MoveRsControlFactory.GetInstance().CreateNewMoveRsControl(this.opControl, this.selectedColumns);
                return;
            }

            // 对应的结果文件置脏
            BCPBuffer.GetInstance().SetDirty(resultElement.FullFilePath);
            //输出变化，重写BCP文件
            if (!this.oldOutName.SequenceEqual(this.selectedColumns))
                Global.GetOptionDao().DoOutputCompare(this.oldOutName, this.selectedColumns, this.opControl.ID);
        }

        private bool IsOptionReay()
        {
            bool empty = false;
            List<string> types = new List<string>();
            types.Add(this.comboBox1.GetType().Name);
            foreach (Control ctl in this.tableLayoutPanel2.Controls)
            {
                if (types.Contains(ctl.GetType().Name) && ctl.Text == "")
                {
                    MessageBox.Show("请选择字段");
                    empty = true;
                    return empty;
                }
            }
            foreach (Control ctl in this.tableLayoutPanel1.Controls)
            {
                if (types.Contains(ctl.GetType().Name) && ctl.Text == "")
                {
                    MessageBox.Show("请选择字段");
                    empty = true;
                    return empty;
                }
            }
            return empty;
        }
        #endregion

        #region 分组字段重复选择判断
        private bool IsDuplicateSelect()
        {
            bool repetition = false;
            string index01 = this.comboBox1.Tag == null ? this.comboBox1.SelectedIndex.ToString() : this.comboBox1.Tag.ToString();
            string factor1 = index01 + "," + this.textBox1.Text;
            Dictionary<string, string> factors = new Dictionary<string, string>();
            factors["factor1"] = factor1;
            if (this.tableLayoutPanel1.RowCount > 0)
            {
                for (int i = 0; i < this.tableLayoutPanel1.RowCount; i++)
                {
                    ComboBox control1 = (ComboBox)this.tableLayoutPanel1.Controls[i * 5 + 1];
                    Control control2 = (Control)this.tableLayoutPanel1.Controls[i * 5 + 2];
                    string index1 = control1.Tag == null ? control1.SelectedIndex.ToString() : control1.Tag.ToString();
                    string factor = index1 + "," + control2.Text;
                    factors["factor" + (i + 2).ToString()] = factor;

                }
            }
            var duplicateValues = factors.Where(x => x.Key.Contains("factor")).GroupBy(x => x.Value).Where(x => x.Count() > 1);
            foreach (var item in duplicateValues)
            {
                MessageBox.Show("数据标准化存在完全重复选项,请重新选择并集条件");
                repetition = true;
            }
            return repetition;
        }
        #endregion
        
        private void CreateLine(int addLine)
        {
            // 添加控件
            Label label = new Label();
            label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            label.AutoSize = true;
            label.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            label.Text = (addLine+2).ToString();
            this.tableLayoutPanel1.Controls.Add(label, 0, addLine);


            ComboBox dataBox = new ComboBox();
            dataBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            dataBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            dataBox.Font = new Font("微软雅黑", 8f, FontStyle.Regular);
            dataBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            dataBox.Items.AddRange(this.nowColumnsName0);
            dataBox.Leave += new System.EventHandler(optionInfoCheck.Control_Leave);
            dataBox.KeyUp += new System.Windows.Forms.KeyEventHandler(optionInfoCheck.Control_KeyUp);
            dataBox.SelectionChangeCommitted += new System.EventHandler(Global.GetOptionDao().GetSelectedItemIndex);
            this.tableLayoutPanel1.Controls.Add(dataBox, 1, addLine);

            TextBox textBox = new TextBox();
            textBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            textBox.Text = "别名";
            textBox.Font = new Font("微软雅黑", 9f, FontStyle.Regular);
            textBox.ForeColor = SystemColors.ActiveCaption;
            textBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            textBox.Enter += TextBox1_Enter;
            textBox.Leave += TextBox1_Leave;
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
            addButton1.Click += new System.EventHandler(this.Add_Click);
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
            delButton1.Click += new System.EventHandler(this.Del_Click);
            delButton1.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            delButton1.Name = addLine.ToString();
            this.tableLayoutPanel1.Controls.Add(delButton1, 4, addLine);
        }

        private void Add_Click(object sender, EventArgs e)
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
                    ctlNext.Text = (k + 3).ToString();
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

        private void Del_Click(object sender, EventArgs e)
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
                ctlNext.Text = (k+2).ToString();
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

        private void TextBox1_Enter(object sender, EventArgs e)
        {
            TextBox TextBoxEx = sender as TextBox;
            if (TextBoxEx.Text == "别名")
            {
                TextBoxEx.Text = String.Empty;
            }
            TextBoxEx.ForeColor = Color.Black;
        }

        private void TextBox1_Leave(object sender, EventArgs e)
        {
            TextBox TextBoxEx = sender as TextBox;
            if (TextBoxEx.Text == String.Empty)
            {
                TextBoxEx.Text = "别名";
                TextBoxEx.ForeColor = SystemColors.ActiveCaption;
            }
        }

    }
}
