using System;
using System.Collections.Generic;
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
        private List<PythonInterpreterInfo> piis; // 当前已配置的所有Python解释器, 元素顺序与combox保持一致
        private Dictionary<string, ConsoleControl.ConsoleControl> consoles;

        public BottomPythonConsoleControl()
        {
            piis = new List<PythonInterpreterInfo>();
            consoles = new Dictionary<string, ConsoleControl.ConsoleControl>();
            InitializeComponent();
            this.cmdConsoleControl.InternalRichTextBox.Font = new System.Drawing.Font("新宋体", 10F);
            consoles.Add(CmdConsoleString, this.cmdConsoleControl);
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

        
        private ConsoleControl.ConsoleControl CreateNewConsoleControl(string controlName, bool visible = false)
        {
            ConsoleControl.ConsoleControl consoleControl = new ConsoleControl.ConsoleControl();

            consoleControl.BorderStyle = BorderStyle.FixedSingle;
            consoleControl.Dock = DockStyle.Fill;
            consoleControl.IsInputEnabled = true;
            consoleControl.Location = new System.Drawing.Point(3, 37);
            consoleControl.Name = controlName;
            consoleControl.SendKeyboardCommandsToProcess = false;
            consoleControl.ShowDiagnostics = false;
            consoleControl.Size = new System.Drawing.Size(1005, 97);
            consoleControl.Visible = visible;
            consoleControl.InternalRichTextBox.Font = new System.Drawing.Font("新宋体", 10F);
            this.tableLayoutPanel1.Controls.Add(consoleControl, 0, 1);

            return consoleControl;
        }

        private void ReleaseConsoleControl(ConsoleControl.ConsoleControl consoleControl)
        {
            this.tableLayoutPanel1.Controls.Remove(consoleControl);
            if (consoleControl.IsProcessRunning)
            {
                TryRleaseProcess(consoleControl);     
            }
            else if (consoleControl != null)
            {
                consoleControl.Dispose();
            }
        }

        private void PIISClear()
        {
            piis.Clear();
            this.comboBox1.Items.Clear();
            this.comboBox1.Items.Add(CmdConsoleString);
            this.comboBox1.SelectedIndex = 0;
        }

        private void BottomPythonConsoleControl_Load(object sender, EventArgs e)
        {
            LoadPythonInterpreter();
            if (CmdConsoleSeleted())
                StartCmdProcess();
        }

        private void StartProcessButton_Click(object sender, EventArgs e)
        {
            int selectedIndex = Math.Max(this.comboBox1.SelectedIndex, 0);

            // 默认第一个选项是cmd控制台;这里的||逻辑还要再斟酌
            if (CmdConsoleSeleted())
            {
                StartCmdProcess();
                return;
            }
            // 越界时用cmd代替python虚拟机
            if (selectedIndex - 1> piis.Count)
                return;

            StartPythonProcess(this.piis[selectedIndex - 1].PythonFFP, PythonInitParams);     
        }

        private void ResetProcessButton_Click(object sender, EventArgs e)
        {
            
            ReleaseConsoleControl(cmdConsoleControl);
        }

        private string CurrentConsoleOwner()
        {
            int selectedIndex = this.comboBox1.SelectedIndex;
            // 啥也没选
            if (selectedIndex < 0)
                return String.Empty;
            // 选择的cmd控制台
            if (selectedIndex == 0)
                return CmdConsoleString;
            // 选择越界,出错了
            if (selectedIndex - 1 > piis.Count)
                return String.Empty;

            return this.piis[selectedIndex - 1].PythonFFP;
        }
        private void ClearScreenButton_Click(object sender, EventArgs e)
        {
            this.cmdConsoleControl.ClearOutput();
        }

        private void CopyContentButton_Click(object sender, EventArgs e)
        {
            FileUtil.TryClipboardSetText(this.cmdConsoleControl.InternalRichTextBox.Text);
        }

        private void StartCmdProcess()
        {
            if (this.cmdConsoleControl.IsProcessRunning)
                return;
            // 直接指定参数来改变初始目录的方式,清空屏幕会有问题
            // 改用更改工作目录的方式,cmd启动完,要把工作目录切回来
            string currentDirectory = Environment.CurrentDirectory;
            Environment.CurrentDirectory = System.IO.Path.Combine(currentDirectory, "sbin");
            TryStartProcess(cmdConsoleControl, "cmd.exe", String.Empty);
            Environment.CurrentDirectory = currentDirectory;
            this.startProcessButton.Enabled = false;
        }

        private void StartPythonProcess(string pythonFFP, string param = "")
        {
            if (!System.IO.File.Exists(pythonFFP))
                return;

            TryStartProcess(cmdConsoleControl, pythonFFP, param);
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // cmd被选中
            if (CmdConsoleSeleted())
            {
                if (this.cmdConsoleControl.IsProcessRunning)
                    this.startProcessButton.Enabled = false;
                else
                    this.startProcessButton.Enabled = true;
            }
            else
                this.startProcessButton.Enabled = true;
        }

        private bool CmdConsoleSeleted()
        {
            return this.comboBox1.SelectedIndex == 0 && this.comboBox1.SelectedItem.ToString() == CmdConsoleString;
        }

        // 引用的第三方库,为了安全期间要假设其会失败,捕捉所有异常,以免主程序崩溃
        private bool TryStartProcess(ConsoleControl.ConsoleControl console, string cmd, string param = "")
        {
            try
            {
                console.StartProcess(cmd, param);
            }
            catch
            {
                log.Error(String.Format("[{0} {1}] start error.", cmd, param));
                return false;
            }
            return true;
        }

        // 引用的第三方库,为了安全期间要假设其会失败,捕捉所有异常,以免主程序崩溃
        private bool TryRleaseProcess(ConsoleControl.ConsoleControl console)
        {
            try
            {
                console.StopProcess();
                // 纯粹为了保险
                if (console.ProcessInterface.Process != null && console.ProcessInterface.Process.HasExited)
                    console.ProcessInterface.Process.Kill();
                console.Dispose();
                console = null;
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
