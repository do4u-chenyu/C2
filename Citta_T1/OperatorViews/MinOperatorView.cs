using Citta_T1.Business.Model;
using Citta_T1.Business.Option;
using Citta_T1.Controls.Move.Op;
using Citta_T1.Core;
using Citta_T1.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Citta_T1.OperatorViews
{
    public partial class MinOperatorView : Form
    {
        private MoveOpControl opControl;
        private string dataPath;
        private List<int> oldOutList;
        private string[] columnName;
        private string oldOptionDict;
        private List<string> oldColumnName;
        private static LogUtil log = LogUtil.GetInstance("MinOperatorView");
        private OptionInfoCheck optionInfoCheck;
        public MinOperatorView(MoveOpControl opControl)
        {
            InitializeComponent();
            this.optionInfoCheck = new OptionInfoCheck();
            dataPath = String.Empty;
            this.columnName = new string[] { };
            this.oldColumnName = new List<string>();
            this.oldOutList = new List<int>();
            this.opControl = opControl;
            InitOptionInfo();
            LoadOption();

            this.oldOptionDict = string.Join(",", this.opControl.Option.OptionDict.ToList());
            this.minValueBox.Leave += new System.EventHandler(optionInfoCheck.Control_Leave);
            this.minValueBox.KeyUp += new System.Windows.Forms.KeyEventHandler(optionInfoCheck.Control_KeyUp);
            this.minValueBox.SelectionChangeCommitted += new System.EventHandler(Global.GetOptionDao().GetSelectedItemIndex);
            SetTextBoxName(this.dataInfoBox);
        }
        #region 添加取消
        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            //未设置字段警告
            if (String.IsNullOrWhiteSpace(this.minValueBox.Text))
            {
                MessageBox.Show("请选择最小值字段!");
                return;
            }
            if (this.outList.GetItemCheckIndex().Count == 0)
            {
                MessageBox.Show("请选择输出字段!");
                return;
            }
            this.DialogResult = DialogResult.OK;
            if (String.IsNullOrWhiteSpace(dataInfoBox.Text)) return;
            SaveOption();
            //内容修改，引起文档dirty
            if (this.oldOptionDict != string.Join(",", this.opControl.Option.OptionDict.ToList()))
                Global.GetMainForm().SetDocumentDirty();
            //生成结果控件,创建relation,bcp结果文件
            ModelElement resultElement = Global.GetCurrentDocument().SearchResultElementByOpID(this.opControl.ID);
            if (resultElement == ModelElement.Empty)
            {
                MoveRsControlFactory.GetInstance().CreateNewMoveRsControl(this.opControl, this.outList.GetItemCheckText());
                return;
            }

            // 对应的结果文件置脏
            BCPBuffer.GetInstance().SetDirty(resultElement.FullFilePath);

            //输出变化，重写BCP文件
            List<string> outName = new List<string>();
            foreach (string index in this.opControl.Option.GetOption("outfield").Split(','))
            { outName.Add(this.columnName[Convert.ToInt32(index)]); }
            if (String.Join(",", this.oldOutList) != this.opControl.Option.GetOption("outfield"))
                Global.GetOptionDao().DoOutputCompare(this.oldColumnName, outName, this.opControl.ID);
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }
        #endregion
        #region 配置信息的保存与加载
        private void SaveOption()
        {
            List<int> checkIndexs = this.outList.GetItemCheckIndex();
            List<int> outIndexs = new List<int>(this.oldOutList);
            List<int> removeIndex = new List<int>();
            Global.GetOptionDao().UpdateOutputCheckIndexs(checkIndexs, outIndexs);
            string outField = string.Join(",", outIndexs);
            this.opControl.Option.SetOption("outfield", outField);
            this.opControl.Option.SetOption("minfield", this.minValueBox.Tag == null ? this.minValueBox.SelectedIndex.ToString() : this.minValueBox.Tag.ToString());

            ElementStatus oldStatus = this.opControl.Status;
            if (this.oldOptionDict != string.Join(",", this.opControl.Option.OptionDict.ToList()))
                this.opControl.Status = ElementStatus.Ready;

            if (oldStatus == ElementStatus.Done && this.opControl.Status == ElementStatus.Ready)
                Global.GetCurrentDocument().DegradeChildrenStatus(this.opControl.ID);


        }

        private void LoadOption()
        {
            if (!Global.GetOptionDao().IsCleanOption(this.opControl, this.columnName, "minfield"))
            {
                int index = Convert.ToInt32(this.opControl.Option.GetOption("minfield"));
                this.minValueBox.Text = this.minValueBox.Items[index].ToString();
                this.minValueBox.Tag = index.ToString();
            }
            if (!Global.GetOptionDao().IsCleanOption(this.opControl, this.columnName, "outfield"))
            {

                string[] checkIndexs = this.opControl.Option.GetOption("outfield").Split(',');
                int[] indexs = Array.ConvertAll<string, int>(checkIndexs, int.Parse);
                this.oldOutList = indexs.ToList();
                this.outList.LoadItemCheckIndex(indexs);
                foreach (int index in indexs)
                    this.oldColumnName.Add(this.outList.Items[index].ToString());
            }

        }
        #endregion
        #region 初始化配置
        private void InitOptionInfo()
        {
            Dictionary<string, string> dataInfo = Global.GetOptionDao().GetDataSourceInfo(this.opControl.ID);
            if (dataInfo.ContainsKey("dataPath0") && dataInfo.ContainsKey("encoding0"))
            {
                this.dataPath = dataInfo["dataPath0"];
                this.dataInfoBox.Text = Path.GetFileNameWithoutExtension(this.dataPath);
                this.toolTip1.SetToolTip(this.dataInfoBox, this.dataInfoBox.Text);
                SetOption(this.dataPath, this.dataInfoBox.Text, dataInfo["encoding0"], dataInfo["separator0"].ToCharArray());
            }
        }
        private void SetOption(string path, string dataName, string encoding, char[] separator)
        {
            BcpInfo bcpInfo = new BcpInfo(path, dataName, ElementType.Empty, OpUtil.EncodingEnum(encoding), separator);
            this.columnName = bcpInfo.ColumnArray;
            foreach (string name in columnName)
            {
                this.outList.AddItems(name);
                this.minValueBox.Items.Add(name);
            }

            this.opControl.FirstDataSourceColumns = this.columnName.ToList();
            this.opControl.Option.SetOption("columnname0", String.Join("\t", this.columnName));
        }


        private void SetTextBoxName(TextBox textBox)
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

        private void DataInfoBox_MouseClick(object sender, MouseEventArgs e)
        {
            this.dataInfoBox.Text = Path.GetFileNameWithoutExtension(this.dataPath);
        }

        private void DataInfoBox_LostFocus(object sender, EventArgs e)
        {
            SetTextBoxName(this.dataInfoBox);
        }


    }
}
