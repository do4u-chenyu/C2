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
        private static LogUtil log = LogUtil.GetInstance("BottomPythonConsoleControl");
        private static string PythonInitParams = "-i -u";  // 控制台字符输出不缓冲
        private static string CmdConsoleString = "Cmd控制台";
        private List<PythonInterpreterInfo> piis; // 当前已配置的所有Python解释器

        public BottomPythonConsoleControl()
        {
            InitializeComponent();
            piis = new List<PythonInterpreterInfo>();
     
            //this.consoleControl1.InternalRichTextBox.BackColor = Color.White;
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
            //StartCmdProcess();
        }

        private void StartProcessButton_Click(object sender, EventArgs e)
        {
            int selectedIndex = Math.Max(this.comboBox1.SelectedIndex, 0);

            // 默认第一个选项是cmd控制台;这里的||逻辑还要再斟酌
            if (selectedIndex == 0 || this.comboBox1.SelectedItem.ToString() == CmdConsoleString)
            {
                StartCmdProcess();
                return;
            }
            // 越界时用cmd代替python虚拟机
            if (selectedIndex - 1> piis.Count)
            {
                StartCmdProcess();
                return;
            }

            StartPythonProcess(this.piis[selectedIndex - 1].PythonFFP, PythonInitParams);
                
        }

        private void ResetProcessButton_Click(object sender, EventArgs e)
        {
            this.consoleControl1.StopProcess();
            StartProcessButton_Click(sender, e);


        }

        private void ClearScreenButton_Click(object sender, EventArgs e)
        {
            this.consoleControl1.ClearOutput();
        }

        private void CopyContentButton_Click(object sender, EventArgs e)
        {
            FileUtil.TryClipboardSetText(this.consoleControl1.InternalRichTextBox.Text);
        }

        private void StartCmdProcess(string param = "")
        {
            if (!CmdConsoleSeleted())
                return;

            if (this.consoleControl1.IsProcessRunning)
                return;
                
            try
            {
                this.consoleControl1.StartProcess("cmd.exe", String.Empty);
                this.startProcessButton.Enabled = false;
            }
            catch
            {
                log.Error(String.Format("Cmd Console Start Error..."));
            }
            
        }

        private void StartPythonProcess(string pythonFFP, string param = "")
        {
            if (!System.IO.File.Exists(pythonFFP))
                return;
            this.consoleControl1.StartProcess(pythonFFP, param);
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // cmd被选中
            if (CmdConsoleSeleted())
            {
                if (this.consoleControl1.IsProcessRunning)
                    this.startProcessButton.Enabled = false;
                else
                    this.startProcessButton.Enabled = true;
            }
            this.startProcessButton.Enabled = true;
        }

        private bool CmdConsoleSeleted()
        {
            return this.comboBox1.SelectedIndex == 0 && this.comboBox1.SelectedItem.ToString() == CmdConsoleString;
        }
    }
}
