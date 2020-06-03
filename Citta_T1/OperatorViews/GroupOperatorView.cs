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
    public partial class GroupOperatorView : Form
    {
        private MoveOpControl opControl;
        private string dataPath;
        private string[] columnName;
        private string oldOptionDict;
        private List<string> selectColumn;
        private List<int> groupColumn;
        private List<bool> oldCheckedItems;
        private List<int> outList;
        private int[] oldOutList;
        private List<string> oldOutName;
        private OptionInfoCheck optionInfoCheck;
        public GroupOperatorView(MoveOpControl opControl)
        {
            InitializeComponent();
            this.optionInfoCheck = new OptionInfoCheck();
            this.selectColumn = new List<string>();
            this.groupColumn = new List<int>();
            this.oldCheckedItems = new List<bool>();
            this.oldOutList = new int[] { };
            this.oldOutName = new List<string>();
            this.opControl = opControl;
            this.oldOptionDict = string.Join(",", this.opControl.Option.OptionDict.ToList());
            dataPath = String.Empty;
            InitOptionInfo();
            LoadOption();
            this.oldCheckedItems.Add(this.noRepetition.Checked);
            this.oldCheckedItems.Add(this.repetition.Checked);
            this.oldCheckedItems.Add(this.ascendingOrder.Checked);
            this.oldCheckedItems.Add(this.descendingOrder.Checked);
            this.oldCheckedItems.Add(this.sortByString.Checked);
            this.oldCheckedItems.Add(this.sortByNum.Checked);
            this.comboBox1.Leave += new System.EventHandler(optionInfoCheck.Control_Leave);
            this.comboBox1.KeyUp += new System.Windows.Forms.KeyEventHandler(optionInfoCheck.Control_KeyUp);
            SetTextBoxName(this.dataInfo);
            //selectindex会在某些不确定情况触发，这种情况是不期望的
            
            this.comboBox1.SelectionChangeCommitted += new System.EventHandler(Global.GetOptionDao().GetSelectedItemIndex);
        }
        #region 初始化配置
        private void InitOptionInfo()
        {
            Dictionary<string, string> dataInfo = Global.GetOptionDao().GetDataSourceInfo(this.opControl.ID);
            if (dataInfo.ContainsKey("dataPath0") && dataInfo.ContainsKey("encoding0"))
            {
                this.dataPath = dataInfo["dataPath0"];
                this.dataInfo.Text = Path.GetFileNameWithoutExtension(this.dataPath);
                this.toolTip1.SetToolTip(this.dataInfo, this.dataInfo.Text);
                SetOption(this.dataPath, this.dataInfo.Text, dataInfo["encoding0"], dataInfo["separator0"].ToCharArray());
            }
  
        }

        private void SetOption(string path, string dataName, string encoding, char[] separator)
        {
            
            BcpInfo bcpInfo = new BcpInfo(path, dataName, ElementType.Empty, OpUtil.EncodingEnum(encoding), separator);
            this.columnName = bcpInfo.ColumnArray;
            foreach (string name in this.columnName)
                this.comboBox1.Items.Add(name);
               
            this.opControl.FirstDataSourceColumns =this.columnName.ToList();
        }
       
        public void SetTextBoxName(TextBox textBox)
        {
            string dataName = textBox.Text;
            int maxLength = 18;
            MatchCollection chs = Regex.Matches(dataName, "[\u4E00-\u9FA5]");
            int sumcount = chs.Count * 2;
            int sumcountDigit = Regex.Matches(dataName, "[a-zA-Z0-9]").Count;

            //防止截取字符串时中文乱码
            foreach (System.Text.RegularExpressions.Match mc in chs)
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

        private void LoadOption()
        {

            if (!String.IsNullOrEmpty( this.opControl.Option.GetOption("noRepetition")))
                this.noRepetition.Checked = Convert.ToBoolean(this.opControl.Option.GetOption("noRepetition"));
            if (!String.IsNullOrEmpty(this.opControl.Option.GetOption("repetition")))
                this.repetition.Checked = Convert.ToBoolean(this.opControl.Option.GetOption("repetition"));
            if (!String.IsNullOrEmpty( this.opControl.Option.GetOption("ascendingOrder")))
                this.ascendingOrder.Checked = Convert.ToBoolean(this.opControl.Option.GetOption("ascendingOrder"));
            if (!String.IsNullOrEmpty(this.opControl.Option.GetOption("descendingOrder")))
                this.descendingOrder.Checked = Convert.ToBoolean(this.opControl.Option.GetOption("descendingOrder"));

            if (this.opControl.Option.GetOption("sortByNum") != "")
                this.sortByNum.Checked = Convert.ToBoolean(this.opControl.Option.GetOption("sortByNum"));
            if (this.opControl.Option.GetOption("sortByString") != "")
                this.sortByString.Checked = Convert.ToBoolean(this.opControl.Option.GetOption("sortByString"));

            if (!Global.GetOptionDao().IsCleanOption(this.opControl, this.columnName, "outfield"))
            {
                this.oldOutList = Array.ConvertAll<string, int>(this.opControl.Option.GetOption("outfield").Split(','), int.Parse);
                foreach (int index in this.oldOutList)
                    this.oldOutName.Add(this.columnName[index]);
            }
            int count = this.opControl.Option.KeysCount("factor");
            string factor1 = this.opControl.Option.GetOption("factor1");
            if (!Global.GetOptionDao().IsCleanOption(this.opControl, this.columnName, "factor1"))
            {
               
                int index = Convert.ToInt32(factor1);
                this.comboBox1.Text = this.comboBox1.Items[index].ToString();
                this.comboBox1.Tag = index.ToString();



            }
            if (count > 1)
                InitNewFactorControl(count - 1);
            else
            {
                this.opControl.Option.SetOption("columnname0", string.Join("\t", this.opControl.FirstDataSourceColumns));
                return;
            }


            for (int i = 2; i < (count + 1); i++)
            {
                string name = "factor" + i.ToString();
                if (Global.GetOptionDao().IsCleanOption(this.opControl, this.columnName, name)) continue;
  
                int index = Convert.ToInt32(this.opControl.Option.GetOption(name));
                Control control1 = (Control)this.tableLayoutPanel1.Controls[(i - 2) * 3 + 0];
                control1.Text = (control1 as ComboBox).Items[index].ToString();
                control1.Tag = index.ToString();
            }
            this.opControl.Option.SetOption("columnname0", string.Join("\t", this.opControl.FirstDataSourceColumns));

        }
        private void SaveOption()
        {
            this.opControl.Option.OptionDict.Clear();
            this.opControl.Option.SetOption("columnname0", String.Join("\t",this.opControl.FirstDataSourceColumns));
            string factor1 = comboBox1.Tag == null ? comboBox1.SelectedIndex.ToString() : comboBox1.Tag.ToString();
            this.opControl.Option.SetOption("factor1", factor1);
            this.groupColumn.Add(this.comboBox1.SelectedIndex);
            
            if (this.tableLayoutPanel1.RowCount > 0)
            {
                for (int i = 0; i < this.tableLayoutPanel1.RowCount; i++)
                {
                    Control control1 = (Control)this.tableLayoutPanel1.Controls[i * 3 + 0];
                    string factor = (control1 as ComboBox).Tag == null ? (control1 as ComboBox).SelectedIndex.ToString() : (control1 as ComboBox).Tag.ToString();
                    this.groupColumn.Add(Convert.ToInt32(factor));
                    this.opControl.Option.SetOption("factor" + (i + 2).ToString(), factor);
                }
            }
            this.opControl.Option.SetOption("noRepetition", this.noRepetition.Checked.ToString());
            this.opControl.Option.SetOption("repetition", this.repetition.Checked.ToString());
            this.opControl.Option.SetOption("ascendingOrder", this.ascendingOrder.Checked.ToString());
            this.opControl.Option.SetOption("descendingOrder", this.descendingOrder.Checked.ToString());
            this.opControl.Option.SetOption("sortByString", this.sortByString.Checked.ToString());
            this.opControl.Option.SetOption("sortByNum", this.sortByNum.Checked.ToString());
            this.outList = new List<int>(this.groupColumn);
            int[] columnIndex = Enumerable.Range(0, this.columnName.Length).ToArray();
            foreach (int index in columnIndex)
            {
                if (!this.groupColumn.Contains(index))
                    this.outList.Add(index);
            }
            this.opControl.Option.SetOption("outfield", string.Join(",", this.outList));

            ElementStatus oldStatus = this.opControl.Status;
            if (this.oldOptionDict != string.Join(",", this.opControl.Option.OptionDict.ToList()))
                this.opControl.Status = ElementStatus.Ready;

            if (oldStatus == ElementStatus.Done && this.opControl.Status == ElementStatus.Ready)
                Global.GetCurrentDocument().DegradeChildrenStatus(this.opControl.ID);

        }
        #endregion
        #region 添加取消
        private void ConfirmButton_Click(object sender, EventArgs e)
        {

            bool empty = IsOptionReay();
            if (empty) return;
            //判断分组字段是否重复选择
            if( IsDuplicateSelect()) return;
            SaveOption();
            this.DialogResult = DialogResult.OK;
            //内容修改，引起文档dirty
            if (this.oldOptionDict != string.Join(",", this.opControl.Option.OptionDict.ToList()))
                Global.GetMainForm().SetDocumentDirty();
            //生成结果控件,创建relation,bcp结果文件
            foreach (int index in this.outList)
                this.selectColumn.Add(this.columnName[index]);
            ModelElement resultElement = Global.GetCurrentDocument().SearchResultElementByOpID(this.opControl.ID);
            if (resultElement == ModelElement.Empty)
            {
                MoveRsControlFactory.GetInstance().CreateNewMoveRsControl(this.opControl, this.selectColumn);
                return;
            }

            // 对应的结果文件置脏
            BCPBuffer.GetInstance().SetDirty(resultElement.FullFilePath);
            //输出变化，重写BCP文件
            if (!this.oldOutName.SequenceEqual(this.selectColumn))
                Global.GetOptionDao().DoOutputCompare(this.oldOutName, this.selectColumn, this.opControl.ID);
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
            foreach (Control ctl in this.tableLayoutPanel2.Controls)
            {
                if (types.Contains(ctl.GetType().Name) && ctl.Text == "")
                {
                    MessageBox.Show("请填写过滤条件!");
                    empty = true;
                    return empty;
                }
            }
            foreach (Control ctl in this.tableLayoutPanel1.Controls)
            {
                if (types.Contains(ctl.GetType().Name) && ctl.Text == "")
                {
                    MessageBox.Show("请填写过滤条件!");
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
            dataBox.Font = new Font("微软雅黑", 8f, FontStyle.Regular);
            dataBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            dataBox.Items.AddRange(this.columnName);
            dataBox.Leave += new System.EventHandler(optionInfoCheck.Control_Leave);
            dataBox.KeyUp += new System.Windows.Forms.KeyEventHandler(optionInfoCheck.Control_KeyUp);        
            dataBox.SelectionChangeCommitted += new System.EventHandler(Global.GetOptionDao().GetSelectedItemIndex);
            this.tableLayoutPanel1.Controls.Add(dataBox, 0, addLine);

           
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
            this.tableLayoutPanel1.Controls.Add(addButton1, 1, addLine);

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
            this.tableLayoutPanel1.Controls.Add(delButton1, 2, addLine);
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
                    Control ctlNext1 = this.tableLayoutPanel1.GetControlFromPosition(1, k);
                    ctlNext1.Name = (k + 1).ToString();
                    this.tableLayoutPanel1.SetCellPosition(ctlNext1, new TableLayoutPanelCellPosition(1, k + 1));
                    Control ctlNext2 = this.tableLayoutPanel1.GetControlFromPosition(2, k);
                    ctlNext2.Name = (k + 1).ToString();
                    this.tableLayoutPanel1.SetCellPosition(ctlNext2, new TableLayoutPanelCellPosition(2, k + 1));
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
                Control bt1 = this.tableLayoutPanel1.Controls[(i * 3) + 2];
                if (bt1.Name == tmp.Name)
                {
                    for (int j = (i * 3) + 2; j >= (i * 3); j--)
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
                ctlNext1.Name = k.ToString();
                this.tableLayoutPanel1.SetCellPosition(ctlNext1, new TableLayoutPanelCellPosition(1, k));
                Control ctlNext2 = this.tableLayoutPanel1.GetControlFromPosition(2, k + 1);
                ctlNext2.Name = k.ToString();
                this.tableLayoutPanel1.SetCellPosition(ctlNext2, new TableLayoutPanelCellPosition(2, k));
            }
            this.tableLayoutPanel1.RowStyles.RemoveAt(this.tableLayoutPanel1.RowCount - 1);
            this.tableLayoutPanel1.RowCount = this.tableLayoutPanel1.RowCount - 1;

            this.tableLayoutPanel1.Height = this.tableLayoutPanel1.RowCount * 40;

        }
        private void GroupBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(this.BackColor);
        }

        private void GroupBox2_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(this.BackColor);
        }

        private void DataInfo_MouseClick(object sender, MouseEventArgs e)
        {
            this.dataInfo.Text = Path.GetFileNameWithoutExtension(this.dataPath);
        }

        private void DataInfo_LostFocus(object sender, EventArgs e)
        {
            SetTextBoxName(this.dataInfo);
        }
        #region 分组字段重复选择判断
        private bool IsDuplicateSelect()
        {
            bool ret = false;
            Dictionary<int,string> selectedIndex = new Dictionary<int, string>();
    
            string index0 = this.comboBox1.Tag == null ? this.comboBox1.SelectedIndex.ToString() : this.comboBox1.Tag.ToString();
            selectedIndex[0] = index0;
      
            if (this.tableLayoutPanel1.RowCount > 0)
            {
                for (int i = 0; i < this.tableLayoutPanel1.RowCount; i++)
                {
                    Control control1 = (Control)this.tableLayoutPanel1.Controls[i * 3 + 0];
                    string index1 = (control1 as ComboBox).Tag == null ? (control1 as ComboBox).SelectedIndex.ToString() : (control1 as ComboBox).Tag.ToString();
                    selectedIndex[i+1] = index1;
                }
            }
            var duplicateValues = selectedIndex.GroupBy(x => x.Value).Where(x => x.Count() > 1);
            List<int> indexs = new List<int>();
            foreach (var item in duplicateValues)
                indexs.Add(Convert.ToInt32(item.Key));
            if (indexs != null&& indexs.Count()>0)
            {
                string name = "";
                foreach (int num in indexs)
                    name += this.columnName[num];
                MessageBox.Show("分组字段" +  name + "重复选择，请保持每个字段只被选择一次");
                ret = true;
            }
            return ret;
        }
        #endregion

        private void GroupBox3_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(this.BackColor);
        }
    }
}
