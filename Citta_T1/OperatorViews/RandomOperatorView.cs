using Citta_T1.Business.Model;
using Citta_T1.Business.Option;
using Citta_T1.Controls.Move.Dt;
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

    public partial class RandomOperatorView : Form
    {
        private MoveOpControl opControl;
        private string dataPath;
        private string oldRandomNum;
        private List<int> oldOutList;
        private string oldOptionDict;
        private string[] columnName;
        private List<string> selectColumn;
        private List<string> oldColumnName;
        private static LogUtil log = LogUtil.GetInstance("RandomOperatorView");
        private OptionInfoCheck optionInfoCheck;
        public RandomOperatorView(MoveOpControl opControl)
        {
            InitializeComponent();
            this.optionInfoCheck = new OptionInfoCheck();
            this.dataPath = String.Empty;
            this.oldOutList = new List<int>();
            oldColumnName = new List<string>();
            this.opControl = opControl;
            InitOptionInfo();
            LoadOption();
            this.oldRandomNum = this.randomNumBox.Text;
            this.oldOptionDict = string.Join(",", this.opControl.Option.OptionDict.ToList());

            SetTextBoxName(this.dataInfoBox);
        }
        #region 初始化配置
        private void InitOptionInfo()
        {
            int startID = -1;
            string encoding = String.Empty;
            char separator = OpUtil.DefaultSeparator;
            List<ModelRelation> modelRelations = Global.GetCurrentDocument().ModelRelations;
            List<ModelElement> modelElements = Global.GetCurrentDocument().ModelElements;
            foreach (ModelRelation mr in modelRelations)
            {
                if (mr.EndID == this.opControl.ID)
                {
                    startID = mr.StartID;
                    break;
                }
            }
            foreach (ModelElement me in modelElements)
            {
                if (me.ID == startID)
                {
                    this.dataPath = me.FullFilePath;
                    separator = me.Separator;
                    //设置数据信息选项
                    this.dataInfoBox.Text = Path.GetFileNameWithoutExtension(this.dataPath);
                    this.toolTip1.SetToolTip(this.dataInfoBox, this.dataInfoBox.Text);
                    encoding = me.Encoding.ToString();
                    break;
                }
            }
            if (this.dataPath != String.Empty)
                SetOption(this.dataPath, this.dataInfoBox.Text, encoding, separator);

        }
        private void SetOption(string path, string dataName, string encoding, char separator)
        {

            BcpInfo bcpInfo = new BcpInfo(path, dataName, ElementType.Empty, OpUtil.EncodingEnum(encoding), separator);
            this.columnName = bcpInfo.ColumnArray;
            foreach (string name in columnName)
            {
                this.outList.AddItems(name);
            }

            this.opControl.FirstDataSourceColumns = this.columnName;
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
        #region 配置信息的保存与加载
        private void SaveOption()
        {
            this.opControl.Option.SetOption("randomnum", this.randomNumBox.Text);
            List<int> checkIndexs = this.outList.GetItemCheckIndex();
            List<int> outIndexs = new List<int>(this.oldOutList);
            Global.GetOptionDao().UpdateOutputCheckIndexs(checkIndexs, outIndexs);
            string outField = string.Join("\t", outIndexs);
            this.opControl.Option.SetOption("outfield", outField);

            ElementStatus oldStatus = this.opControl.Status;
            if (this.oldOptionDict != string.Join(",", this.opControl.Option.OptionDict.ToList()))
                this.opControl.Status = ElementStatus.Ready;

            if (oldStatus == ElementStatus.Done && this.opControl.Status == ElementStatus.Ready)
                Global.GetCurrentDocument().DegradeChildrenStatus(this.opControl.ID);

        }

        private void LoadOption()
        {
            if (!string.IsNullOrEmpty( this.opControl.Option.GetOption("randomnum")))
                this.randomNumBox.Text = this.opControl.Option.GetOption("randomnum");
            if (!Global.GetOptionDao().IsCleanOption(this.opControl, this.columnName, "outfield"))
            {
                string[] checkIndexs = this.opControl.Option.GetOptionSplit("outfield");
                int[] indexs = Array.ConvertAll<string, int>(checkIndexs, int.Parse);
                this.oldOutList = indexs.ToList();
                this.outList.LoadItemCheckIndex(indexs);
                foreach (int index in indexs)
                    this.oldColumnName.Add(this.outList.Items[index].ToString());
            }

        }
        #endregion
        #region 添加取消
        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            //未设置字段警告
            if (this.randomNumBox.Text == String.Empty)
            {
                MessageBox.Show("随机条数字段不能为空,请输入一个整数");
                return;
            }
            if (this.outList.GetItemCheckIndex().Count == 0)
            {
                MessageBox.Show("请选择算子要输出的字段");
                return;
            }
            this.DialogResult = DialogResult.OK;
            if (this.dataInfoBox.Text == String.Empty) return;
            SaveOption();
            //内容修改，引起文档dirty
            if (this.oldRandomNum != this.randomNumBox.Text)
                Global.GetMainForm().SetDocumentDirty();
            else if (String.Join(",", this.oldOutList) != this.opControl.Option.GetOption("outfield"))
                Global.GetMainForm().SetDocumentDirty();
            //生成结果控件,创建relation,bcp结果文件
            this.selectColumn = this.outList.GetItemCheckText();
            ModelElement resultElement = Global.GetCurrentDocument().SearchResultElementByOpID(this.opControl.ID);
            if (resultElement == ModelElement.Empty)
            {
                MoveRsControlFactory.GetInstance().CreateNewMoveRsControl(this.opControl, this.selectColumn);
                return;
            }
            // 对应的结果文件置脏
            BCPBuffer.GetInstance().SetDirty(resultElement.FullFilePath);
            //输出变化，重写BCP文件
            List<string> outName = new List<string>();
            foreach (string index in this.opControl.Option.GetOptionSplit("outfield"))
            { outName.Add(this.columnName[Convert.ToInt32(index)]); }
            if (String.Join(",", this.oldOutList) != this.opControl.Option.GetOption("outfield"))
                Global.GetOptionDao().DoOutputCompare(this.oldColumnName, outName, this.opControl.ID);
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
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

        private void RandomNumBox_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void RandomNumBox_Leave(object sender, EventArgs e)
        {
            ConvertUtil.ControlTextTryParseInt(randomNumBox, "\"{0}\" 不是数字, 请输入一个整数.");
        }
    }
}
