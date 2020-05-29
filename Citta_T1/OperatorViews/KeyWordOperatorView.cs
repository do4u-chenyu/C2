using Citta_T1.Controls.Move.Op;
using Citta_T1.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            Dictionary<string, string> dataInfoDic = Global.GetOptionDao().GetDataSourceInfo(opControl.ID, false);
            if (AccessOptionCheck(dataInfoDic.Keys.ToList()))
                return;
            dataSourcePath = dataInfoDic["dataPath0"];
            dataSourceBox.Text = Path.GetFileNameWithoutExtension(dataSourcePath);
            dataSourceTip.SetToolTip(dataSourceBox, dataSourceBox.Text);
            keyWordPath = dataInfoDic["dataPath1"];
            keyWordBox.Text = Path.GetFileNameWithoutExtension(dataInfoDic["dataPath1"]);
            keyWordTip.SetToolTip(keyWordBox, keyWordBox.Text);           
        }
        private bool AccessOptionCheck(List<string> dataInfoKeys)
        {
            List<string> keyCheck = new List<string> { "dataPath0", "encoding0", "dataPath1", "encoding1" };
            return !keyCheck.ToList().Except(dataInfoKeys.ToList()).Any();
        }
        #endregion

    }
}
