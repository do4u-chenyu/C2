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
            this.pythonFFPTextBox.Text = pythonFFP;
        }

        private void PythonConfigCancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private bool CheckPythonInterpreter(string pythonFFP)
        {
            return true;
        }

        private void PythonConfigOkButton_Click(object sender, EventArgs e)
        {
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
    }
}
