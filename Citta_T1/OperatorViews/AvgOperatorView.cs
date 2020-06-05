using Citta_T1.Business.Model;
using Citta_T1.Business.Option;
using Citta_T1.Controls.Move.Op;
using Citta_T1.Core;
using Citta_T1.OperatorViews.Base;
using Citta_T1.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Citta_T1.OperatorViews
{
    public partial class AvgOperatorView : Form
    {
        private OptionViewInfo optionViewInfo;
        private string oldAvg;
        private string selectedIndex;

       

        public AvgOperatorView(MoveOpControl opControl)
        {
            InitializeComponent();
            optionViewInfo = new OptionViewInfo();
            optionViewInfo.OpControl = opControl;

            InitOptionInfor();
            LoadOption();
           
            this.oldAvg = this.AvgComBox.Text;
            optionViewInfo.OldOptionDictStr = optionViewInfo.OpControl.Option.ToString();

            SetTextBoxName(this.DataInfo);
        }
        #region 初始化配置
        private void InitOptionInfor()
        {
            int startID = -1;
            string encoding = String.Empty;
            char separator = OpUtil.DefaultSeparator;
            List<ModelRelation> modelRelations = Global.GetCurrentDocument().ModelRelations;
            List<ModelElement> modelElements = Global.GetCurrentDocument().ModelElements;
            foreach (ModelRelation mr in modelRelations)
            {
                if (mr.EndID == optionViewInfo.OpControl.ID)
                {
                    startID = mr.StartID;
                    break;
                }
            }
            foreach (ModelElement me in modelElements)
            {
                if (me.ID == startID)
                {
                    separator = me.Separator;
                    optionViewInfo.DataPath0 = me.FullFilePath;
                    //设置数据信息选项
                    this.DataInfo.Text = Path.GetFileNameWithoutExtension(this.optionViewInfo.DataPath0);
                    this.toolTip1.SetToolTip(this.DataInfo, this.DataInfo.Text);
                    encoding = me.Encoding.ToString();
                    break;
                }
            }
            if (!String.IsNullOrEmpty(optionViewInfo.DataPath0))
                SetOption(this.optionViewInfo.DataPath0, this.DataInfo.Text, encoding, separator);

        }
        private void SetOption(string path, string dataName, string encoding, char separator)
        {
            BcpInfo bcpInfo = new BcpInfo(path, dataName, ElementType.Empty, OpUtil.EncodingEnum(encoding), separator);
            optionViewInfo.NowColumnName0 = bcpInfo.ColumnArray;
            foreach (string name in optionViewInfo.NowColumnName0)
                this.AvgComBox.Items.Add(name);
            optionViewInfo.OpControl.FirstDataSourceColumns = optionViewInfo.NowColumnName0;
            optionViewInfo.OpControl.Option.SetOption("columnname0", String.Join("\t", optionViewInfo.NowColumnName0));
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

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            //未设置字段警告
            if (this.DataInfo.Text == "") return;
            if (this.AvgComBox.Text == "")
            {
                MessageBox.Show("请选择平均值字段!");
                return;
            }
            this.DialogResult = DialogResult.OK;
            SaveOption();

            //情况1：内容修改
            //       引起文档dirty
            //情况2：内容不修改
            //        返回
            if (optionViewInfo.OldOptionDictStr != string.Join(",", optionViewInfo.OpControl.Option.OptionDict.ToList()))
                Global.GetMainForm().SetDocumentDirty();
            else
                return;
            //生成结果控件,创建relation,bcp结果文件
            optionViewInfo.SelectedColumns.Add(this.AvgComBox.SelectedItem.ToString());
            ModelElement resultElement = Global.GetCurrentDocument().SearchResultElementByOpID(optionViewInfo.OpControl.ID);
            if (resultElement == ModelElement.Empty)
            {
                MoveRsControlFactory.GetInstance().CreateNewMoveRsControl(optionViewInfo.OpControl, optionViewInfo.SelectedColumns);
                return;
            }
            // 对应的结果文件置脏
            BCPBuffer.GetInstance().SetDirty(resultElement.FullFilePath);

            //输出变化，重写BCP文件
            List<string> oldColumn = new List<string>();
            oldColumn.Add(this.oldAvg);
            if (this.oldAvg != this.AvgComBox.Text)
                Global.GetOptionDao().DoOutputCompare(oldColumn, optionViewInfo.SelectedColumns, optionViewInfo.OpControl.ID);

        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }
        #region 配置信息的保存与加载
        private void SaveOption()
        {

            optionViewInfo.OpControl.Option.SetOption("avgfield", this.selectedIndex == null ? this.AvgComBox.SelectedIndex.ToString() : this.selectedIndex);
            optionViewInfo.OpControl.Option.SetOption("outfield", this.AvgComBox.SelectedIndex.ToString());

            ElementStatus oldStatus = optionViewInfo.OpControl.Status;
            if (optionViewInfo.OldOptionDictStr != string.Join(",", optionViewInfo.OpControl.Option.OptionDict.ToList()))
                optionViewInfo.OpControl.Status = ElementStatus.Ready;

            if (oldStatus == ElementStatus.Done && optionViewInfo.OpControl.Status == ElementStatus.Ready)
                Global.GetCurrentDocument().DegradeChildrenStatus(optionViewInfo.OpControl.ID);
        }

        private void LoadOption()
        {
            if (!Global.GetOptionDao().IsCleanOption(optionViewInfo.OpControl, optionViewInfo.NowColumnName0, "avgfield"))
            {
                int index = Convert.ToInt32(optionViewInfo.OpControl.Option.GetOption("avgfield"));
                this.AvgComBox.Text = this.AvgComBox.Items[index].ToString();
                this.selectedIndex = index.ToString();
            }
            
        }
        #endregion
        private void DataInfo_MouseClick(object sender, MouseEventArgs e)
        {
            this.DataInfo.Text = Path.GetFileNameWithoutExtension(optionViewInfo.DataPath0);
        }

        private void DataInfo_LostFocus(object sender, EventArgs e)
        {
            SetTextBoxName(this.DataInfo);
        }

        private void AvgComBox_Leave(object sender, EventArgs e)
        {
            this.optionViewInfo.OptionInfoCheck.IsIllegalInputName(this.AvgComBox, optionViewInfo.NowColumnName0, this.AvgComBox.Text);
        }

        private void AvgComBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                this.optionViewInfo.OptionInfoCheck.IsIllegalInputName(this.AvgComBox, optionViewInfo.NowColumnName0, this.AvgComBox.Text);
        }

        private void AvgComBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.selectedIndex = this.AvgComBox.SelectedIndex.ToString();
        }
    }
}
