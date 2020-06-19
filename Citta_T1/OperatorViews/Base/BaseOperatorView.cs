using Citta_T1.Business.Model;
using Citta_T1.Business.Option;
using Citta_T1.Controls.Move.Op;
using Citta_T1.Core;
using Citta_T1.Utils;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Citta_T1.OperatorViews.Base
{
    public enum DataType
    {
         String,
         Int,
         Bool
    }
    public class CheckOption
    {
        private readonly Dictionary<string, DataType> dataTypes;        
        private readonly Dictionary<string,int> itemCounts0;
        private readonly Dictionary<string, int[]> itemCounts1;
        private readonly MoveOpControl opControl;
        private int maxIndex0;
        private int maxIndex1;
        public CheckOption(MoveOpControl opControl)
        {
            this.dataTypes = new Dictionary<string, DataType>();
            this.itemCounts0 = new Dictionary<string, int>();
            this.itemCounts1 = new Dictionary<string, int[]>();
            this.opControl = opControl;
            
        }
        public CheckOption Add(string prefix, DataType dataType)
        {
            this.dataTypes[prefix] = dataType;
            return this;
        }
        public CheckOption Add(string prefix, DataType dataType, int count)
        {
            this.dataTypes[prefix] = dataType;
            this.itemCounts0[prefix] = count;
            return this;
        }
        public CheckOption Add(string prefix, DataType dataType, int[] countList)
        {
            this.dataTypes[prefix] = dataType;
            this.itemCounts1[prefix] = countList;
            return this;
        }
        public void DealAbnormalOption(OperatorOption option)
        {
            maxIndex0 = opControl.Option.GetOptionSplit("columname0").Length - 1;
            maxIndex1 = opControl.Option.GetOptionSplit("columname1").Length - 1;
            // 判断表头信息是否存在
            // 不存在，清空所有配置
            if (WithoutInputColumns(option))
            {
                option.Clear();
                this.opControl.Status = ElementStatus.Null;
                return;
            }
          
            foreach (string prefix in dataTypes.Keys)
            {
               
                // 配置项丢失检查
                if (String.IsNullOrEmpty(option.GetOption(prefix)))
                    opControl.Status = ElementStatus.Null;

                // 数据类型检测与异常类型处理
                if (dataTypes[prefix] == DataType.Int)
                    CheckAndDealIntType(option, prefix);

                // 索引值超限判断
                if (string.IsNullOrEmpty(option.GetOption(prefix)))
                    continue;

                string[] items = option.GetOptionSplit(prefix);
                string[] Keys = itemCounts0.Keys.ToArray();
                
                if (Keys.Contains(prefix) && Array.ConvertAll(items, int.Parse).Max() > maxIndex0)
                {
                    opControl.Option[prefix] = String.Empty;
                    opControl.Status = ElementStatus.Null;
                }
                else if (itemCounts1.Keys.Contains(prefix))
                {
                    DealOutOfRangeIndex(itemCounts1, option);
                }

            }

        }
        private bool WithoutInputColumns(OperatorOption option)
        {
            bool hasInput0 = String.IsNullOrEmpty(option.GetOption("columname0"));
            bool hasInput1 = String.IsNullOrEmpty(option.GetOption("columname1"));
            bool binaryInput = this.opControl.IsBinaryDimension();

            if ( hasInput1 && binaryInput || hasInput0)
                return true;
            return false;
        }
        private void CheckAndDealIntType(OperatorOption option, string key)
        {
            if (string.IsNullOrEmpty(option[key]))
                return;
            if (key.Contains("outfield") && IsNotAllInt(option.GetOptionSplit(key)))
            {
                option[key] = String.Empty;
                opControl.Status = ElementStatus.Null;
            }
            else if (key.Equals("factor1"))
                DealNotIntChange(option);
            else if (key.Equals("factorI"))
            { }
            else if(!ConvertUtil.IsInt(option[key]))
            {
                opControl.Option[key] = String.Empty;
                opControl.Status = ElementStatus.Null;
            }

        }
        private void DealNotIntChange(OperatorOption option)
        {

            List<ElementSubType> mixTypes = new List<ElementSubType>()
            {
                ElementSubType.FilterOperator,
                ElementSubType.DataFormatOperator,
                ElementSubType.UnionOperator
            };
            ElementSubType ctlType = OpUtil.SEType(opControl.SubTypeName);
            List<string> factors = option.Keys.FindAll(x => x.Contains("factor"));

            foreach (string factor in factors)
            {
                if (string.IsNullOrEmpty(option[factor]))
                    continue;
                
                string[] items = option.GetOptionSplit(factor);
                int maxIndex = items.Length - 1;
                string[] indexs = mixTypes.Contains(ctlType) ? items.Take(maxIndex).ToArray() : items;

                if (IsNotAllInt(indexs))
                {
                    opControl.Option[factor] = String.Empty;
                    opControl.Status = ElementStatus.Null;
                }
            }
        }
        public void DealOutOfRangeIndex(Dictionary<string, int[]> itemCounts1, OperatorOption option)
        {
            int count0 = itemCounts1["factor1"].Length;
            int count1 = itemCounts1["factorI"].Length;

            List<string> factors = option.Keys.FindAll(x => x.Contains("factor"));
            foreach (string factor in factors)
            {
                if (factor.Equals("factor1"))
                    IsOutOfIndex(option.GetOptionSplit(factor), itemCounts1["factor1"], maxIndex0);
                else
                    IsOutOfIndex(option.GetOptionSplit(factor), itemCounts1["factorI"], maxIndex1);
            }
         

        }
        private bool IsOutOfIndex(String[] itemList, int[] maxIndexs, int columnCount)
        {
            int count = maxIndexs.Length;
            //索引数目小于正常数目，异常：直接返回
            if (itemList.Length < count) return true;
            if (IsNotAllInt(itemList.Take(count).ToArray())) return true;
            for (int i = 0; i < count; i++)
            {
                if (maxIndexs[i] == -1 && Convert.ToInt32(itemList[i]) > maxIndex0)
                {
                    return true;
                }
                else if (maxIndexs[i] == -2 && Convert.ToInt32(itemList[i]) > maxIndex1)
                    return true;
                else if (maxIndexs[i] < Convert.ToInt32(itemList[i]))
                    return true;
            }
            return false;

        }
        private bool IsNotAllInt(string[] indexs)
        {
            foreach (string index in indexs)
            {
                if (!ConvertUtil.IsInt(index))
                    return true;
            }
            return false;
        }


    }



    public partial class BaseOperatorView : Form
    {
        protected MoveOpControl opControl;          // 对应的OP算子 
        protected string dataSourceFFP0;            // 左表数据源路径
        protected string dataSourceFFP1;            // 右表数据源路径
        protected string[] nowColumnsName0;         // 当前左表(pin0)数据源表头字段(columnName)
        protected string[] nowColumnsName1;         // 当前右表(pin1)数据源表头字段
        protected List<string> oldOutName0;         // 上一次（左输出）输出表头字段
        protected List<string> oldOutName1;         // 上一次（右输出）输出表头字段
        protected List<int> oldOutList0;            // 上一次用户选择的左表输出字段的索引
        protected List<int> oldOutList1;            // 上一次用户选择的右表输出字段的索引
        protected List<string> selectedColumns;     // 本次配置用户选择的输出字段名称
        protected string oldOptionDictStr;          // 旧配置字典的字符串表述
        protected int ColumnCount { get => this.tableLayoutPanel1.ColumnCount; }       // 有增减条件的表格步长

        protected Dictionary<string, string> dataInfo; // 加载左右表数据源基本信息: FFP, Description, EXTType, encoding, sep等
        protected CheckOption checkOptions;
        public BaseOperatorView()
        {
            this.opControl = null;
            oldOptionDictStr = String.Empty;
            dataSourceFFP0 = String.Empty;
            dataSourceFFP1 = String.Empty;
            nowColumnsName0 = new string[0];
            nowColumnsName1 = new string[0];
            oldOutName0 = new List<string>();
            oldOutName1 = new List<string>();
            oldOutList0 = new List<int>();
            oldOutList1 = new List<int>();
            selectedColumns = new List<string>();
            dataInfo = new Dictionary<string, string>();
            checkOptions = new CheckOption(this.opControl);
            InitializeComponent();
        }
        public BaseOperatorView(MoveOpControl opControl) : this()
        {
            this.opControl = opControl;
            oldOptionDictStr = opControl.Option.ToString();
        }
        // 初始化左右表数据源
        protected void InitDataSource()
        {
            dataInfo = Global.GetOptionDao().GetDataSourceInfoDict(this.opControl.ID);
            // 左表
            if (dataInfo.ContainsKey("dataPath0") && dataInfo.ContainsKey("encoding0"))
            {
                this.dataSourceFFP0 = dataInfo["dataPath0"];
                this.dataSourceTB0.Text = dataInfo["description0"];
                BcpInfo bcpInfo = new BcpInfo(dataSourceFFP0, OpUtil.EncodingEnum(dataInfo["encoding0"]), dataInfo["separator0"].ToCharArray());
                opControl.FirstDataSourceColumns = bcpInfo.ColumnArray;
                if (!(bcpInfo.ColumnArray.Length == 1 && bcpInfo.ColumnArray[0] == ""))//排除表头为空的情况
                    this.nowColumnsName0 = bcpInfo.ColumnArray;
                SetTextBoxName(this.dataSourceTB0);
            }
            // 右表
            if (dataInfo.ContainsKey("dataPath1") && dataInfo.ContainsKey("encoding1"))
            {
                this.dataSourceFFP1 = dataInfo["dataPath1"];
                this.dataSourceTB1.Text = dataInfo["description1"]; ;
                BcpInfo bcpInfo = new BcpInfo(dataSourceFFP1, OpUtil.EncodingEnum(dataInfo["encoding1"]), dataInfo["separator1"].ToCharArray());
                opControl.SecondDataSourceColumns = bcpInfo.ColumnArray;
                if (!(bcpInfo.ColumnArray.Length == 1 && bcpInfo.ColumnArray[0] == ""))//排除表头为空的情况
                    this.nowColumnsName1 = bcpInfo.ColumnArray;
                SetTextBoxName(this.dataSourceTB1); // 一元算子,TB1是不可见,赋值了也没事,统一逻辑后可以减少重复代码
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            /*
             * 外部Xml文件修改等情况，检查并处理异常配置内容
             */
           // checkOptions.DealAbnormalOption();
            this.DialogResult = DialogResult.Cancel;
            Close();
        }

        // 是否配置完毕
        protected virtual bool IsOptionNotReady()
        {
          
            return false;
        }
        protected virtual void SaveOption()
        {
        }
        // 判断标准化字段是否重复选择
        protected virtual bool IsDuplicateSelect()
        {
            return false;
        }
        protected virtual void ConfirmButton_Click(object sender, EventArgs e)
        {
            
            if (IsOptionNotReady()) return;
            if (IsDuplicateSelect()) return;//数据标准化窗口
            SaveOption();
            this.DialogResult = DialogResult.OK;

            if (this.oldOptionDictStr != this.opControl.Option.ToString())
                Global.GetMainForm().SetDocumentDirty();
            // 生成结果控件,relation,bcp结果文件
            ModelElement resultElement = Global.GetCurrentDocument().SearchResultElementByOpID(this.opControl.ID);
            if (resultElement == ModelElement.Empty)
            {
                MoveRsControlFactory.GetInstance().CreateNewMoveRsControl(this.opControl, this.selectedColumns);
                return;
            }
            // 对应的结果文件置脏
            BCPBuffer.GetInstance().SetDirty(resultElement.FullFilePath);
            // 输出字段变化，重写BCP文件
            // 单输出算子时oldOutName1为0数组,不影响逻辑
            List<string> oldOutNames = this.oldOutName0.Concat(this.oldOutName1).ToList();
            Global.GetOptionDao().DoOutputCompare(oldOutNames, this.selectedColumns, this.opControl.ID);
        }

        protected void SetTextBoxName(TextBox textBox)
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

        private void DataSourceTB1_MouseHover(object sender, EventArgs e)
        {
            this.toolTip1.SetToolTip(dataSourceTB1, this.dataSourceFFP1);
        }

        private void DataSourceTB0_MouseHover(object sender, EventArgs e)
        {
            this.toolTip1.SetToolTip(dataSourceTB0, this.dataSourceFFP0);
        }

        protected void Control_Leave(object sender, EventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if (comboBox.Items.Count == 0 || String.IsNullOrEmpty(comboBox.Text)) return;
            if (!comboBox.Items.Contains(comboBox.Text))
            {
                comboBox.Text = String.Empty;
                MessageBox.Show("未输入正确列名，请从下拉列表中选择正确列名");
            }
        }
        protected void Control_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Control_Leave(sender, e);
        }

        protected void IsIllegalCharacter(object sender, EventArgs e)
        {
            if ((sender as TextBox).Text.Contains("\\t"))
            {
                (sender as TextBox).Text = String.Empty;
                MessageBox.Show("输入非法字TAB键，请重新输入过滤条件");
            }
        }

        protected void GetSelectedItemIndex(object sender, EventArgs e)
        {
            (sender as ComboBox).Tag = (sender as ComboBox).SelectedIndex.ToString();
        }

        //更新后续子图所有节点状态
        protected void UpdateSubGraphStatus()
        {
            ElementStatus oldStatus = this.opControl.Status;
            if (this.oldOptionDictStr == this.opControl.Option.ToString() 
                && oldStatus == ElementStatus.Done)
                return;
             this.opControl.Status = ElementStatus.Ready;

            if (oldStatus == ElementStatus.Done && this.opControl.Status == ElementStatus.Ready)
                Global.GetCurrentDocument().DegradeChildrenStatus(this.opControl.ID);
        }

        protected void InitNewFactorControl(int count)
        {
            for (int line = 0; line < count; line++)
            {
                this.tableLayoutPanel1.RowCount++;
                this.tableLayoutPanel1.Height = this.tableLayoutPanel1.RowCount * 40;
                this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
                CreateLine(line);
            }
        }

        protected virtual void CreateLine(int addLine)
        { 
        
        }

        protected void GroupBox_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(this.BackColor);
        }

        private Button NewButton(string name)
        {
            Button delButton = new Button();
            delButton.FlatAppearance.BorderColor = SystemColors.Control;
            delButton.FlatAppearance.BorderSize = 0;
            delButton.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            delButton.FlatAppearance.MouseOverBackColor = SystemColors.Control;
            delButton.FlatStyle = FlatStyle.Flat;
            delButton.BackColor = SystemColors.Control;
            delButton.UseVisualStyleBackColor = true;
            delButton.BackgroundImageLayout = ImageLayout.Center;
            delButton.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            delButton.Name = name;
            return delButton;
        }


        protected Button NewDelButton(string name)
        {
            Button delButton = NewButton(name);
            delButton.BackgroundImage = Properties.Resources.div;
            delButton.Click += new EventHandler(this.Del_Click);
            return delButton;
        }


        protected Button NewAddButton(string name)
        {
            Button addButton = NewButton(name);
            addButton.BackgroundImage = Properties.Resources.add;
            addButton.Click += new EventHandler(this.Add_Click);
            return addButton;
        }

        protected TextBox NewAliasTextBox()
        {
            TextBox textBox = new TextBox
            {
                Anchor = AnchorStyles.Left | AnchorStyles.Right,
                Text = "别名",
                Font = new Font("微软雅黑", 9f, FontStyle.Regular),
                ForeColor = SystemColors.ActiveCaption
            };
            textBox.Enter += AliasTextBox_Enter;
            textBox.Leave += AliasTextBox_Leave;
            textBox.Leave += new EventHandler(this.IsIllegalCharacter);
            textBox.KeyUp += new KeyEventHandler(this.IsIllegalCharacter);
            return textBox;
        }

        protected ComboBox NewComboBox()
        {
            ComboBox combox = new ComboBox
            {
                AutoCompleteMode = AutoCompleteMode.SuggestAppend,
                AutoCompleteSource = AutoCompleteSource.ListItems,
                Anchor = AnchorStyles.Left | AnchorStyles.Right,
                Font = new Font("微软雅黑", 8f, FontStyle.Regular)
            };
            combox.Leave += new EventHandler(this.Control_Leave);
            combox.KeyUp += new KeyEventHandler(this.Control_KeyUp);
            combox.SelectionChangeCommitted += new EventHandler(this.GetSelectedItemIndex);
            return combox;
        }

        protected ComboBox NewAndORComboBox()
        {
            ComboBox combox = NewComboBox();
            combox.Anchor = AnchorStyles.None;
            combox.Items.AddRange(new object[] {
            "AND",
            "OR"});
            return combox;
        }

        protected ComboBox NewColumnsName1ComboBox()
        {
            ComboBox combox = NewComboBox();
            combox.Items.AddRange(this.nowColumnsName1);
            return combox;
        }

        protected ComboBox NewColumnsName0ComboBox()
        {
            ComboBox combox = NewComboBox();
            combox.Items.AddRange(this.nowColumnsName0);
            return combox;
        }

        protected void AliasTextBox_Enter(object sender, EventArgs e)
        {
            TextBox TextBoxEx = sender as TextBox;
            if (TextBoxEx.Text == "别名")
            {
                TextBoxEx.Text = String.Empty;
            }
            TextBoxEx.ForeColor = Color.Black;
        }

        protected void AliasTextBox_Leave(object sender, EventArgs e)
        {
            TextBox TextBoxEx = sender as TextBox;
            if (TextBoxEx.Text == String.Empty)
            {
                TextBoxEx.Text = "别名";
                TextBoxEx.ForeColor = SystemColors.ActiveCaption;
            }
        }

        protected void Add_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            int lineNumber = 0;

            this.tableLayoutPanel1.RowCount++;
            this.tableLayoutPanel1.Height = this.tableLayoutPanel1.RowCount * 40;
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));

            if (this.tableLayoutPanel1.RowCount > 1)
            {
                lineNumber = button.Name == "button1" ? 0 : int.Parse(button.Name) + 1;
                AddTableLayoutPanelControls(lineNumber);
            }
            CreateLine(lineNumber);
        }


        protected virtual void AddTableLayoutPanelControls(int lineNumber)
        {
            for (int k = this.tableLayoutPanel1.RowCount - 2; k >= lineNumber; k--)
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
        }

        protected void Del_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            int lineNumber = int.Parse(button.Name);

            for (int i = 0; i < this.tableLayoutPanel1.RowCount; i++)
            {
                int buttonPosition = (i * ColumnCount) + ColumnCount - 1;
                Control bt1 = this.tableLayoutPanel1.Controls[buttonPosition];
                if (bt1.Name == button.Name)
                {
                    for (int j = buttonPosition; j >= (i * ColumnCount); j--)
                    {
                        this.tableLayoutPanel1.Controls.RemoveAt(j);
                    }
                    break;
                }

            }

            MoveTableLayoutPanelControls(lineNumber);

            this.tableLayoutPanel1.RowStyles.RemoveAt(this.tableLayoutPanel1.RowCount - 1);
            this.tableLayoutPanel1.RowCount -= 1;

            this.tableLayoutPanel1.Height = this.tableLayoutPanel1.RowCount * 40;
        }

        protected virtual void MoveTableLayoutPanelControls(int lineNumber)
        {
            for (int k = lineNumber; k < this.tableLayoutPanel1.RowCount - 1; k++)
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
        }
    }
}
