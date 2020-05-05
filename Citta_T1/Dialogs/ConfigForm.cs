using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Citta_T1.Utils;

namespace Citta_T1.Dialogs
{
    public partial class ConfigForm : Form
    {
        private static LogUtil log = LogUtil.GetInstance("ConfigForm");
        private static int AliasColumnIndex = 1;
        private static int CheckBoxColumnIndex = 2;
        private static Regex PythonAliasRegex = new Regex(@"(python\d{1,3}(\.\d{1,3})?)", RegexOptions.IgnoreCase);
        public ConfigForm()
        {
            InitializeComponent();
        }

        private void TabControl_Selected(object sender, TabControlEventArgs e)
        {
            log.Info(e.TabPageIndex.ToString());
        }

        private void UserModelOkButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void UserModelCancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AboutOkButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AboutCancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void PythonBrowseButton_Click(object sender, EventArgs e)
        {
            DialogResult dr = pythonOpenFileDialog.ShowDialog();
            if (dr != DialogResult.OK)
                return;
            string pythonFFP = pythonOpenFileDialog.FileName;
            if (!CheckPythonInterpreter(pythonFFP))
            {
                //TODO 弹出对话框
                MessageBox.Show(String.Format("Python解释器导入错误: {0}", pythonFFP), 
                    "Python解释器导入错误", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Information);
                return;
            }

            AddPythonInterpreter(pythonFFP);

        }

        private void PythonConfigCancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AddPythonInterpreter(string pythonFFP)
        {
            // 默认别名采用 python 解释器的可执行文件名,但一般都是python.exe,看不出区别
            // 用正则表达式从路径中提取带版本号的python解释器名称,失败时采用默认命名
            string aliasDefault = System.IO.Path.GetFileNameWithoutExtension(pythonFFP);
            MatchCollection mats = PythonAliasRegex.Matches(pythonFFP);
            foreach(Match mat in mats)
            {   // 提取成功
                if (mat.Success && mat.Groups.Count > 1 && mat.Groups[1].Success)
                {   // 提取最长的一个匹配
                    string matchValue = mat.Groups[1].Value;
                    if (matchValue.Length > aliasDefault.Length)
                        aliasDefault = matchValue;
                }       
            }        
            this.pythonFFPTextBox.Text = pythonFFP;
            // 第一行 默认选中
            if (this.dataGridView.Rows.Count == 0)
            {
                this.dataGridView.Rows.Add(new Object[] { pythonFFP, aliasDefault, true });
                this.chosenPythonLable.Text = aliasDefault;
            }
            else
                this.dataGridView.Rows.Add(new Object[] { pythonFFP, aliasDefault, false });
        }

        private void SavePythonConfig()
        { 
        }

        private bool CheckPythonInterpreter(string pythonFFP)
        {
            return true;
        }

        private void PythonConfigOkButton_Click(object sender, EventArgs e)
        {
            SavePythonConfig();
            Close();
        }

        private void ConfigForm_Load(object sender, EventArgs e)
        {
            UserModelTabPage_Load();
            PythonConfigTabPage_Load();
        }

        private void UserModelTabPage_Load()
        {
            this.userModelTextBox.Text = Global.WorkspaceDirectory;
        }

        private void PythonConfigTabPage_Load()
        { 
        
        }


        private void DataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            // 别名不能为空
            if (e.ColumnIndex == 1 && String.IsNullOrEmpty(e.FormattedValue.ToString().Trim()))
            {
                dataGridView.Rows[e.RowIndex].ErrorText = "别名不能为空";
                e.Cancel = true;
            }
        }

        private void DataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView.Rows[e.RowIndex].ErrorText = String.Empty;
        }

        private void DataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == -1 || e.RowIndex == -1) return;
            for (int i = 0; i < dataGridView.Rows.Count; i++)
            {
                if (i == e.RowIndex)
                    continue;
                (dataGridView.Rows[i].Cells[CheckBoxColumnIndex] as DataGridViewCheckBoxCell).Value = false;
            }
            this.chosenPythonLable.Text = dataGridView.Rows[e.RowIndex].Cells[AliasColumnIndex].Value.ToString();
        }
    }
}
