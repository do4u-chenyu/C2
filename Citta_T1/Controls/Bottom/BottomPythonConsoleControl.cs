using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Citta_T1.Utils;
using Citta_T1.IAOLab.PythonOP;

namespace Citta_T1.Controls.Bottom
{
    public partial class BottomPythonConsoleControl : UserControl
    {
        private List<PythonInterpreterInfo> piis;

        public BottomPythonConsoleControl()
        {
            InitializeComponent();
            piis = new List<PythonInterpreterInfo>();
        }

        private void MenuItemClearAll_Click(object sender, EventArgs e)
        {

        }

        private void MenuItemSelectAll_Click(object sender, EventArgs e)
        {
            
        }

        public void LoadPythonInterpreter()
        {
            // 先所有现存的Python Console清空
            // 可能包含很多清空步骤
            PIISClear();

            string pythonConfigString = ConfigUtil.TryGetAppSettingsByKey("python");
            PythonOPConfig config = new PythonOPConfig(pythonConfigString);
            if (config.Empty())
                return;


            
            foreach (PythonInterpreterInfo pii in config.AllPII)
            {
                piis.Add(pii);
                this.comboBox1.Items.Add(pii.PythonAlias);
            }

        }

        private void PIISClear()
        {
            piis.Clear();
            this.comboBox1.Items.Clear();
            this.comboBox1.Items.Add("Cmd控制台");
            this.comboBox1.SelectedIndex = 0;
        }

        private void BottomPythonConsoleControl_Load(object sender, EventArgs e)
        {
            LoadPythonInterpreter();
        }
    }
}
