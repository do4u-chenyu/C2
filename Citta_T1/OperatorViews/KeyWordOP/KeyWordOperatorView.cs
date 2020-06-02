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
    public partial class KeyWordOperatorView : Form
    {
        private const int colIndexDefault = 0;
        private static LogUtil log = LogUtil.GetInstance("CanvasPanel");
        private string dataSourcePath, dataSourceEncode, keyWordPath, keyWordEncode;
        private MoveOpControl opControl;
        private string[] dataSrcColName, keyWordColName;       

        public KeyWordOperatorView(MoveOpControl opControl)
        {
            this.opControl = opControl;
            InitializeComponent();
            InitOptionInfo();
            
        }

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }
        #region 加载连接数据
        private void InitOptionInfo()
        {           
            Dictionary<string, string> dataInfoDic = Global.GetOptionDao().GetDataSourceInfo(opControl.ID);


            _ = dataInfoDic.TryGetValue("dataPath0", out dataSourcePath);
            _ = dataInfoDic.TryGetValue("dataPath1", out keyWordPath);
            _ = dataInfoDic.TryGetValue("encoding0", out dataSourceEncode);
            _ = dataInfoDic.TryGetValue("encoding1", out keyWordEncode);

            dataSourceBox.Text = SetTextBoxName(dataSourcePath);
            dataSourceTip.SetToolTip(dataSourceBox, dataSourceBox.Text);

            keyWordBox.Text = SetTextBoxName(keyWordPath);
            keyWordTip.SetToolTip(keyWordBox, keyWordBox.Text);
            dataSrcColName = SetOption(this.dataSourcePath, 
                                       this.dataSourceBox.Text, 
                                       dataInfoDic["encoding0"], 
                                       dataInfoDic["separator0"].ToCharArray());
            keyWordColName = SetOption(this.keyWordPath,
                                       this.keyWordBox.Text,
                                       dataInfoDic["encoding1"],
                                       dataInfoDic["separator1"].ToCharArray());
            this.opControl.FirstDataSourceColumns = this.dataSrcColName.ToList();
            this.opControl.SecondDataSourceColumns = this.keyWordColName.ToList();
            this.dataColumnBox.Items.AddRange(dataSrcColName);
            this.keyWordColBox.Items.AddRange(keyWordColName);
            this.outList.Items.AddRange(dataSrcColName);
            this.dataColumnBox.SelectedIndex = colIndexDefault;
            this.keyWordColBox.SelectedIndex = colIndexDefault;
            this.conditionSelectBox.SelectedIndex = colIndexDefault;
        }
        private string[] SetOption(string path, string dataName, string encoding, char[] separator)
        {
            BcpInfo bcpInfo = new BcpInfo(path, dataName, ElementType.Empty, OpUtil.EncodingEnum(encoding), separator);
            this.opControl.FirstDataSourceColumns = bcpInfo.ColumnArray.ToList();
            return bcpInfo.ColumnArray;
        }
        private string SetTextBoxName(string filePath)
        {
            String fileName = Path.GetFileNameWithoutExtension(filePath);
            int maxLength = 18;
            MatchCollection chs = Regex.Matches(fileName, "[\u4E00-\u9FA5]");
            int sumcount = chs.Count * 2;
            int sumcountDigit = Regex.Matches(fileName, "[a-zA-Z0-9]").Count;

            //防止截取字符串时中文乱码
            foreach (Match mc in chs)
            {
                if (fileName.IndexOf(mc.ToString()) == maxLength)
                {
                    maxLength -= 1;
                    break;
                }
            }
            if (sumcount + sumcountDigit <= maxLength)
            {
                return fileName;
            }
            return ConvertUtil.SubstringByte(fileName, colIndexDefault, maxLength);
        }
        #endregion
        #region 检查
        private bool IsOptionReady()
        {
            bool empty = false;
            
            if (this.outList.GetItemCheckIndex().Count == colIndexDefault)
            {
                MessageBox.Show("请选择输出字段!");
                empty = true;
                return empty;
            }
            return empty;
        }
        #endregion
        #region 配置信息的保存与修改

        #endregion
    }
}
