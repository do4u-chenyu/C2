using C2.Controls.Move.Op;
using C2.Core;
using C2.Dialogs.Base;
using C2.Utils;
using System;
using System.IO;
using System.Windows.Forms;

namespace C2.OperatorViews
{
    public partial class PreprocessingOperatorView : C1BaseOperatorView
    {
        private string fileSamplePath;
        private string dirSamplePath;

        public PreprocessingOperatorView(MoveOpControl opControl) : base(opControl)
        {
            InitializeComponent();
            InitializeDataSource();//初始化配置内容
            LoadOption();
            fileSamplePath = Path.Combine(Global.TemplatesPath, "数据预处理文件模板.txt");
            dirSamplePath = Path.Combine(Global.TemplatesPath, "数据预处理文件夹模板.txt");
        }


    #region 配置信息的保存与加载
    protected override void SaveOption()
        {
            this.opControl.Option.Clear();
            //输入输出字段待定
            //this.opControl.Option.SetOption("outfield0", comboBox0.Tag == null ? this.comboBox0.SelectedIndex.ToString() : comboBox0.Tag.ToString());
            
            this.opControl.Option.SetOption("columnname0", this.nowColumnsName0);
            this.opControl.Option.SetOption("pretype", Convert.ToInt32((ConvertBoolToString(this.checkBox1.Checked) + ConvertBoolToString(this.checkBox2.Checked) + ConvertBoolToString(this.checkBox3.Checked)), 2));

            //更新子图所有节点状态
            UpdateSubGraphStatus();
        }

        private void LoadOption()
        {
            if (string.IsNullOrEmpty(this.opControl.Option.GetOption("pretype")))
                return;
            string pretype = DecimalToBinary(int.Parse(this.opControl.Option.GetOption("pretype")));
            this.checkBox1.Checked = pretype[0].ToString() == "1" ;
            this.checkBox2.Checked = pretype[1].ToString() == "1";
            this.checkBox3.Checked = pretype[2].ToString() == "1";
        }
        #endregion

        private string DecimalToBinary(int decimalNum)
        {
            string binaryNum = Convert.ToString(decimalNum, 2);
            if (binaryNum.Length < 3)
            {
                for (int i = 0; i < 3 - binaryNum.Length; i++)
                {
                    binaryNum = '0' + binaryNum;
                }
            }
            return binaryNum;
        }

        private string ConvertBoolToString(bool flag)
        {
            return flag ? "1" : "0";
        }

        protected override void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }

        private void DirSampleLabel_Click(object sender, EventArgs e)
        {
            SaveFileToLocal(dirSamplePath);
        }

        private void FileSampleLabel_Click(object sender, EventArgs e)
        {
            SaveFileToLocal(fileSamplePath);
        }

        private void SaveFileToLocal(string sourcePath)
        {
            SaveFileDialog dialog = new SaveFileDialog
            {
                Filter = "文本文件|*.txt",
                FileName = Path.GetFileName(sourcePath)
            };

            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            using (GuarderUtil.WaitCursor)
            {
                if (!File.Exists(dialog.FileName))
                {
                    StreamWriter sw = new StreamWriter(dialog.FileName);
                    sw.Close();
                }
                File.Copy(sourcePath, dialog.FileName, true);
            }
            HelpUtil.ShowMessageBox("保存完毕。");
        }
    }
}
