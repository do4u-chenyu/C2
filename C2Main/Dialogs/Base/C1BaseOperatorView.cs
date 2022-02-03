﻿using C2.Business.Model;
using C2.Business.Option;
using C2.Controls.Move.Op;
using C2.Core;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace C2.Dialogs.Base
{
    public partial class C1BaseOperatorView : BaseOperatorView
    {
        protected MoveOpControl opControl;          // 对应的OP算子 
        
        
        protected List<string> oldOutName0;         // 上一次（左输出）输出表头字段
        protected List<string> oldOutName1;         // 上一次（右输出）输出表头字段
        protected List<int> oldOutList0;            // 上一次用户选择的左表输出字段的索引
        protected List<int> oldOutList1;            // 上一次用户选择的右表输出字段的索引
        protected List<string> selectedColumns;     // 本次配置用户选择的输出字段名称
        protected string oldOptionDictStr;          // 旧配置字典的字符串表述
        protected int ColumnCount { get => this.tableLayoutPanel1.ColumnCount; }       // 有增减条件的表格步长


        protected Dictionary<string, string> dataInfo; // 加载左右表数据源基本信息: FFP, Description, EXTType, encoding, sep等

        public C1BaseOperatorView()
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
            InitializeComponent();
        }

        public C1BaseOperatorView(MoveOpControl opControl) : this()
        {
            this.opControl = opControl;
            oldOptionDictStr = opControl.Option.ToString();
            comboBoxes = new List<ComboBox>() { this.comboBox0, this.comboBox1 };
        }

        // 初始化左右表数据源
        protected virtual void InitializeDataSource()
        {
            dataInfo = Global.GetOptionDao().GetDataSourceInfoDict(this.opControl.ID);
            // 左表
            if (dataInfo.ContainsKey("dataPath0") && dataInfo.ContainsKey("encoding0"))
            {
                this.dataSourceFFP0 = dataInfo["dataPath0"];
                this.dataSourceTB0.Text = dataInfo["description0"];
                BcpInfo bcpInfo = new BcpInfo(dataSourceFFP0, OpUtil.EncodingEnum(dataInfo["encoding0"]), dataInfo["separator0"].ToCharArray());
                opControl.FirstDataSourceColumns = bcpInfo.ColumnArray;
                if (!(bcpInfo.ColumnArray.Length == 1 && bcpInfo.ColumnArray[0] == string.Empty))//排除表头为空的情况
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
                if (!(bcpInfo.ColumnArray.Length == 1 && bcpInfo.ColumnArray[0] == string.Empty))//排除表头为空的情况
                    this.nowColumnsName1 = bcpInfo.ColumnArray;
                SetTextBoxName(this.dataSourceTB1); // 一元算子,TB1是不可见,赋值了也没事,统一逻辑后可以减少重复代码
            }

        }

        protected virtual void CancelButton_Click(object sender, EventArgs e)
        {
            /*
             * 外部Xml文件修改等情况，检查并处理异常配置内容
             */
            opControl.Option.OptionValidating();
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
            if (IsIllegalFieldName()) return;
            if (IsDuplicateSelect()) return;//数据标准化窗口
            SaveOption();
            this.DialogResult = DialogResult.OK;

            if (this.oldOptionDictStr != this.opControl.Option.ToString())
                Global.GetMainForm().SetDocumentDirty();
            // 生成结果控件,relation,bcp结果文件
            ModelElement resultElement = Global.GetCurrentModelDocument().SearchResultElementByOpID(this.opControl.ID);
            if (resultElement == ModelElement.Empty)
            {
                MoveRsControlFactory.GetInstance().CreateNewMoveRsControl(this.opControl, this.selectedColumns);
                return;
            }
            // 对应的结果文件置脏
            BCPBuffer.GetInstance().SetDirty(resultElement.FullFilePath);
            // 判断结果算子有没有异常删除，或者结果算子存储的路径与当前工作路径不一致
            string path = resultElement.FullFilePath;
            string filePath = Path.Combine(Global.GetCurrentModelDocument().SavePath, Path.GetFileName(path));
            if (filePath != path)
                resultElement.FullFilePath = filePath;
            if (Directory.Exists(Global.GetCurrentModelDocument().SavePath) && !File.Exists(filePath))
                File.Create(filePath);

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

        protected bool IsIllegalFieldName()
        {
            bool isIllegal = true;
            foreach (ComboBox cbb in this.comboBoxes)
            {
                if (String.IsNullOrEmpty(cbb.Text))
                    continue;
                if (!cbb.Items.Contains(cbb.Text))
                {
                    cbb.Text = String.Empty;
                    HelpUtil.ShowMessageBox("未输入正确字段名，请从下拉列表中选择正确字段名");
                    return isIllegal;
                }
                if (IsIllegalCharacter(cbb))
                {
                    HelpUtil.ShowMessageBox("字段名中包含分隔符TAB，请检查与算子相连数据源的分隔符选择是否正确");
                    return isIllegal;
                }
            }
            return !isIllegal;
        }
        //请重新输入过滤条件
        protected bool IsIllegalCharacter(Control control)
        {
            if (control.Text.Contains('\t'))
            {
                control.Text = String.Empty;
                return true;
            }
            return false;
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
                Global.GetCurrentModelDocument().DegradeChildrenStatus(this.opControl.ID);
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
            //e.Graphics.Clear(this.BackColor);
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
            return textBox;
        }

        protected ComboBox NewColumnsName1ComboBox()
        {
            ComboBox combox = NewComboBox();
            combox.Items.AddRange(this.nowColumnsName1);
            combox.SelectionChangeCommitted += new EventHandler(this.GetRightSelectedItemIndex);
            combox.TextUpdate += new System.EventHandler(RightComboBox_TextUpdate);
            combox.DropDownClosed += new System.EventHandler(RightComboBox_ClosedEvent);
            return combox;
        }

        protected ComboBox NewColumnsName0ComboBox()
        {
            ComboBox combox = NewComboBox();
            combox.Items.AddRange(this.nowColumnsName0);
            combox.SelectionChangeCommitted += new EventHandler(this.GetLeftSelectedItemIndex);
            combox.TextUpdate += new System.EventHandler(LeftComboBox_TextUpdate);
            combox.DropDownClosed += new System.EventHandler(LeftComboBox_ClosedEvent);
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
            this.tableLayoutPanel1.SuspendLayout();
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
            this.tableLayoutPanel1.ResumeLayout(true);
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
            this.tableLayoutPanel1.SuspendLayout();
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

            this.tableLayoutPanel1.ResumeLayout(true);
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
