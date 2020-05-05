using Citta_T1.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Citta_T1.Dialogs
{
    public partial class ConfigForm : Form
    {
        private static LogUtil log = LogUtil.GetInstance("ConfigForm");
        private static int CheckBoxRowIndex = 2;
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
            string aliasDefault = System.IO.Path.GetFileNameWithoutExtension(pythonFFP); 
            this.pythonFFPTextBox.Text = pythonFFP;

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
                (dataGridView.Rows[i].Cells[CheckBoxRowIndex] as DataGridViewCheckBoxCell).Value = false;
            }
        }
    }
}
