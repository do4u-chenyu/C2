using Citta_T1.Business.Model;
using Citta_T1.Business.Option;
using Citta_T1.Controls.Move.Op;
using Citta_T1.Core;
using Citta_T1.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Citta_T1.OperatorViews.Base
{
    public partial class BaseOperatorView : Form
    {
        protected MoveOpControl opControl;          // 对应的OP算子 
        protected string dataSourceFFP0;            // 左表数据源路径
        protected string dataSourceFFP1;            // 右表数据源路径
        protected string[] nowColumnsName0;         // 当前左表(pin0)数据源表头字段(columnName)
        protected string[] nowColumnsName1;         // 当前右表(pin1)数据源表头字段
        protected List<string> oldColumnsName0;     // 上一次左表(pin0)数据源表头字段
        protected List<string> oldColumnsName1;     // 上一次右表(pin1数据源表头字段
        protected List<int> oldOutList0;            // 上一次用户选择的左表输出字段的索引
        protected List<int> oldOutList1;            // 上一次用户选择的右表输出字段的索引
        protected List<string> selectedColumns;     // 本次配置用户选择的输出字段名称
        protected string oldOptionDictStr;          // 旧配置字典的字符串表述

        protected OptionInfoCheck optionInfoCheck;  // 用户配置信息通用检查

        public BaseOperatorView()
        {
            InitializeComponent();
            this.opControl = null;
            oldOptionDictStr = String.Empty;
            dataSourceFFP0 = String.Empty;
            dataSourceFFP1 = String.Empty;
            nowColumnsName0 = new string[0];
            nowColumnsName1 = new string[0];
            oldColumnsName0 = new List<string>();
            oldColumnsName1 = new List<string>();
            oldOutList0 = new List<int>();
            oldOutList1 = new List<int>();
            selectedColumns = new List<string>();
            optionInfoCheck = new OptionInfoCheck();
        }
        public BaseOperatorView(MoveOpControl opControl) : this()
        {
            this.opControl = opControl;
            oldOptionDictStr = opControl.Option.ToString();
        }

        protected void InitDataSource()
        {
            Dictionary<string, string> dataInfo = Global.GetOptionDao().GetDataSourceInfoDict(this.opControl.ID);
            
            if (dataInfo.ContainsKey("dataPath0") && dataInfo.ContainsKey("encoding0"))
            {
                this.dataSourceFFP0     = dataInfo["dataPath0"];
                this.dataSourceTB0.Text = dataInfo["description0"];
                BcpInfo bcpInfo = new BcpInfo(dataSourceFFP0, OpUtil.EncodingEnum(dataInfo["encoding0"]), dataInfo["separator0"].ToCharArray());
                opControl.FirstDataSourceColumns = bcpInfo.ColumnArray;
                this.nowColumnsName0 = bcpInfo.ColumnArray;
            }
            
            
            if (dataInfo.ContainsKey("dataPath1") && dataInfo.ContainsKey("encoding1"))
            {
                this.dataSourceFFP1     = dataInfo["dataPath1"];
                this.dataSourceTB1.Text = dataInfo["description1"]; ;
                BcpInfo bcpInfo = new BcpInfo(dataSourceFFP1, OpUtil.EncodingEnum(dataInfo["encoding1"]), dataInfo["separator1"].ToCharArray());
                opControl.SecondDataSourceColumns = bcpInfo.ColumnArray;
                this.nowColumnsName1 = bcpInfo.ColumnArray;
            }
        }


        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }

        protected virtual void ConfirmButton_Click(object sender, EventArgs e)
        {

        }

        protected void SetTextBoxName(TextBox textBox)
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

        private void DataSourceTB1_MouseHover(object sender, EventArgs e)
        {
            this.toolTip1.SetToolTip(dataSourceTB1, this.dataSourceFFP1);
        }

        private void DataSourceTB0_MouseHover(object sender, EventArgs e)
        {
            this.toolTip1.SetToolTip(dataSourceTB0, this.dataSourceFFP0);
        }
    }
}
