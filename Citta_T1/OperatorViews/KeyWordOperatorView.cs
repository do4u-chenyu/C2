﻿using Citta_T1.Controls.Move.Op;
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
        private string dataSourcePath;
        private string keyWordPath;
        private MoveOpControl opControl;

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
            if (AccessOptionCheck(dataInfoDic.Keys.ToList()))
                return;
            
            dataSourcePath = dataInfoDic["dataPath0"];
            dataSourceBox.Text = SetTextBoxName(dataSourcePath);
            dataSourceTip.SetToolTip(dataSourceBox, dataSourceBox.Text);

            keyWordPath = dataInfoDic["dataPath1"];
            keyWordBox.Text = SetTextBoxName(keyWordPath);
            keyWordTip.SetToolTip(keyWordBox, keyWordBox.Text);           
        }
        private bool AccessOptionCheck(List<string> dataInfoKeys)
        {
            List<string> keyCheck = new List<string> { "dataPath0", "encoding0", "dataPath1", "encoding1" };
            return !keyCheck.ToList().Except(dataInfoKeys.ToList()).Any();
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
            return ConvertUtil.SubstringByte(fileName, 0, maxLength);
        }
        #endregion

    }
}
