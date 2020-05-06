using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Citta_T1.Utils;

namespace Citta_T1.Dialogs
{
    public partial class ConfigForm : Form
    {
        private static LogUtil log = LogUtil.GetInstance("ConfigForm");
        private static readonly int PythonFFPColumnIndex = 0;
        private static readonly int AliasColumnIndex = 1;
        private static readonly int CheckBoxColumnIndex = 2;
        private static readonly Regex PythonAliasRegex = new Regex(@"(python\d{1,3}(\.\d{1,3})?)", RegexOptions.IgnoreCase);
        private static readonly char[] IllegalCharacter = { ';', '?', '<', '>', '/', '|', '#', '!' };
        public ConfigForm()
        {
            InitializeComponent();
        }

        private void TabControl_Selected(object sender, TabControlEventArgs e)
        {
            //log.Info(e.TabPageIndex.ToString());
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
            string pythonFFP = pythonOpenFileDialog.FileName.Trim();
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

        private void AddPythonInterpreter(string pythonFFP, string alias, bool ifCheck)
        {
            this.dataGridView.Rows.Add(new Object[] { pythonFFP, alias, ifCheck });
            if (ifCheck)
            {
                this.pythonFFPTextBox.Text = pythonFFP;
                this.chosenPythonLable.Text = alias;
            }       
        }

        private void SavePythonConfig()
        {
            // <add key="python" value="C:\PythonFake\Python37\python.exe|Python37|true;C:\PythonFake\Python37\python.exe|Python37|false;" />
            StringBuilder sb = new StringBuilder();
            foreach (DataGridViewRow row in this.dataGridView.Rows)
            {
                string pythonFFP = row.Cells[PythonFFPColumnIndex].Value.ToString().Trim();
                string alias = row.Cells[AliasColumnIndex].Value.ToString().Trim();
                bool ifCheck = (bool)row.Cells[CheckBoxColumnIndex].Value;
                sb.Append(pythonFFP).Append('|')
                  .Append(alias).Append('|')
                  .Append(ifCheck).Append(';');
            }
            ConfigUtil.TrySetAppSettingsByKey("python", sb.ToString());
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
            // <add key="python" value="C:\PythonFake\Python37\python.exe|Python37|true;C:\PythonFake\Python37\python.exe|Python37|false;" />
            string value = ConfigUtil.TryGetAppSettingsByKey("python");
            foreach (string pItem in value.Split(';'))
            {
                string[] oneConfig = pItem.Split('|');
                // 格式不对, 忽略
                if (oneConfig.Length != 3)
                    continue;
                string pythonFFP = oneConfig[0].Trim();
                string alias = oneConfig[1].Trim();
                bool ifCheck = StringTryParseBool(oneConfig[2]);
                AddPythonInterpreter(pythonFFP, alias, ifCheck);
            }    
        }

        private bool StringTryParseBool(string value)
        {
            try 
            {
                return bool.Parse(value);
            }
            catch
            {
                return false;
            }
        }


        private void DataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex != 1)
                return;

            string value = e.FormattedValue.ToString().Trim();
            // 别名不能为空
            if (String.IsNullOrEmpty(value))
            {
                dataGridView.Rows[e.RowIndex].ErrorText = "别名不能为空";
                e.Cancel = true;
                return;
            }
            // 特殊字符判断
            if (value.IndexOfAny(IllegalCharacter) != -1)
            {  
                dataGridView.Rows[e.RowIndex].ErrorText = "别名不能含有特殊字符 " + String.Join(" ", IllegalCharacter);
                e.Cancel = true;
                return;
            }
        }

        private void DataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dataGridView.Rows[e.RowIndex];
            row.ErrorText = String.Empty;
            // 如果是当前选中的行的别名被修改, 更新chosen
            if (e.ColumnIndex == AliasColumnIndex && (bool)row.Cells[CheckBoxColumnIndex].Value)
                this.chosenPythonLable.Text = row.Cells[AliasColumnIndex].Value.ToString().Trim();
        }

        private void DataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != CheckBoxColumnIndex || e.RowIndex == -1) return;
            // 所有第三列的checkbox只允许互斥单选
            for (int i = 0; i < dataGridView.Rows.Count; i++)
                if (i != e.RowIndex)
                    (dataGridView.Rows[i].Cells[CheckBoxColumnIndex] as DataGridViewCheckBoxCell).Value = false;
            for (int i = 0; i < dataGridView.Rows.Count; i++)
            {
                // 全部都没选中时, chosen清空，  选定的当前列, 需要用 EditedFormattedValue,此时Value尚未提交
                DataGridViewCell cell = dataGridView.Rows[i].Cells[CheckBoxColumnIndex];
                if (i == e.RowIndex ? (bool)cell.EditedFormattedValue : (bool)cell.Value) 
                {
                    this.chosenPythonLable.Text = dataGridView.Rows[i].Cells[AliasColumnIndex].Value.ToString().Trim();
                    return;
                }         
            }
            this.chosenPythonLable.Text = String.Empty;         
        }

        private void DataGridView_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                // 全部都没选中时, chosen清空，  选定的当前列, 需要用 EditedFormattedValue,此时Value尚未提交
                DataGridViewCell cell = row.Cells[CheckBoxColumnIndex];
                if ((bool)cell.Value)
                {
                    this.chosenPythonLable.Text = row.Cells[AliasColumnIndex].Value.ToString().Trim();
                    return;
                }
            }
            this.chosenPythonLable.Text = String.Empty;
        }
    }
}
