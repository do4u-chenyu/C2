using C2.IAOLab.PythonOP;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace C2.Controls.Bottom
{
    public partial class BottomConsoleControl : UserControl
    {
        private static LogUtil log = LogUtil.GetInstance("BottomPythonConsoleControl");
        private static string PythonInitParams = "-i -u";  // 控制台字符输出不缓冲
        private static string CmdConsoleString = "Cmd控制台";
        private List<PythonInterpreterInfo> piis; // 当前已配置的所有Python解释器, 元素顺序与combox保持一致
        private Dictionary<string, ConsoleControl.ConsoleControl> consoles;

        public BottomConsoleControl()
        {
            piis = new List<PythonInterpreterInfo>();
            consoles = new Dictionary<string, ConsoleControl.ConsoleControl>();
            InitializeComponent();
            //this.cmdConsoleControl.InternalRichTextBox.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdConsoleControl.InternalRichTextBox.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmdConsoleControl.Name = CmdConsoleString;
            this.cmdConsoleControl.InternalRichTextBox.KeyDown += InternalRichTextBox_KeyDown;
            consoles.Add(this.cmdConsoleControl.Name, this.cmdConsoleControl);
            this.Disposed += new EventHandler(BottomConsoleControl_Disposed);
        }

        private void BottomConsoleControl_Disposed(object sender, EventArgs e)
        {
            ReleaseConsoleControl(cmdConsoleControl);
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
            ConsoleControl.ConsoleControl consoleControl = new ConsoleControl.ConsoleControl
            {
                BorderStyle = BorderStyle.FixedSingle,
                Dock = DockStyle.Fill,
                IsInputEnabled = true,
                Location = new System.Drawing.Point(3, 37),
                Name = controlName,
                SendKeyboardCommandsToProcess = false,
                ShowDiagnostics = false,
                Size = new System.Drawing.Size(1005, 97),
                Visible = visible
            };
            consoleControl.InternalRichTextBox.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            consoleControl.InternalRichTextBox.KeyDown += InternalRichTextBox_KeyDown;
            return consoleControl;
        }

        private void InternalRichTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
            {
                e.SuppressKeyPress = true;
                ((RichTextBox)sender).Paste(DataFormats.GetFormat(DataFormats.Text));
            }
        }

        private void ReleaseConsoleControl(ConsoleControl.ConsoleControl consoleControl)
        {
            if (consoleControl != null && consoleControl.IsProcessRunning)
            {
                TryRleaseProcess(consoleControl);
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
            if (DesignerModelClass.IsDesignerMode)
                return;

            //LoadPythonInterpreter(); 加载py控制台的暂时不用,bug太多
            this.comboBox1.SelectedIndex = 0;
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
            if (selectedIndex - 1 > piis.Count)
                return;

            string owner = this.piis[selectedIndex - 1].PythonFFP;
            if (piis.FindIndex(c => c.PythonFFP == owner) < 0)
                return;
            ConsoleControl.ConsoleControl console = CreateNewConsoleControl(owner, false);
            if (consoles.ContainsKey(console.Name))
                consoles.Remove(console.Name);
            consoles.Add(console.Name, console);
            StartPythonProcess(console, console.Name, PythonInitParams);
        }

        private void ResetProcessButton_Click(object sender, EventArgs e)
        {
            // 清理当前console
            string owner = CurrentConsoleOwnerString();
            if (String.IsNullOrEmpty(owner))
                return;
            ConsoleControl.ConsoleControl oldConsoleControl = CurrentConsoleControl();
            // 创建cmd console
            if (owner == CmdConsoleString)
            {
                ConsoleControl.ConsoleControl console = CreateNewConsoleControl(CmdConsoleString, false);

                this.panel2.SuspendLayout();
                this.panel2.Controls.Add(console);
                if (oldConsoleControl != null)
                    this.panel2.Controls.Remove(oldConsoleControl);
                this.panel2.ResumeLayout(false);

                if (consoles.ContainsKey(console.Name))
                    consoles.Remove(console.Name);
                consoles.Add(console.Name, console);

                cmdConsoleControl = console;
                StartCmdProcess();
            }
            // 存在当前python解释器
            else if (piis.FindIndex(c => c.PythonFFP == owner) >= 0)
            {
                ConsoleControl.ConsoleControl console = CreateNewConsoleControl(owner, false);

                this.panel2.Controls.Add(console);
                if (oldConsoleControl != null)
                    this.panel2.Controls.Remove(oldConsoleControl);

                if (consoles.ContainsKey(console.Name))
                    consoles.Remove(console.Name);
                consoles.Add(console.Name, console);

                StartPythonProcess(console, console.Name, PythonInitParams);
            }

            if (oldConsoleControl != null)
                ReleaseConsoleControl(oldConsoleControl);
        }

        private string CurrentConsoleOwnerString()
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

        private ConsoleControl.ConsoleControl CurrentConsoleControl()
        {
            string owner = CurrentConsoleOwnerString();
            if (String.IsNullOrEmpty(owner))
                return this.cmdConsoleControl;
            if (this.consoles.ContainsKey(owner))
                return consoles[owner];
            return CreateNewConsoleControl(owner);
        }
        private void ClearScreenButton_Click(object sender, EventArgs e)
        {
            ConsoleControl.ConsoleControl console = CurrentConsoleControl();
            if (console != null)
                console.ClearOutput();
        }

        private void CopyContentButton_Click(object sender, EventArgs e)
        {
            ConsoleControl.ConsoleControl console = CurrentConsoleControl();
            if (console != null)
                FileUtil.TryClipboardSetText(console.InternalRichTextBox.Text);
        }

        private void StartCmdProcess()
        {
            if (cmdConsoleControl == null || cmdConsoleControl.IsProcessRunning)
                return;
            // 直接指定参数来改变初始目录的方式,清空屏幕会有问题
            // 改用更改工作目录的方式,cmd启动完,要把工作目录切回来

            string currentDirectory = Environment.CurrentDirectory;
            string targetDirectory = System.IO.Path.Combine(currentDirectory, "sbin");
            if (!System.IO.Directory.Exists(targetDirectory))
            {
                TryStartProcess(cmdConsoleControl, "cmd.exe", String.Empty);
                return;
            }

            Environment.CurrentDirectory = targetDirectory;
            TryStartProcess(cmdConsoleControl, "cmd.exe", String.Empty);
            Environment.CurrentDirectory = currentDirectory;
            this.startProcessButton.Enabled = false;
            // 隐藏其他console
            ConsoleControlReLayout(cmdConsoleControl);
        }

        private void HideAllConsoleExceptOne(string exceptOne)
        {
            foreach (string key in consoles.Keys)
            {
                if (key == exceptOne)
                    continue;
                if (consoles[key] != null)
                    consoles[key].Hide();
            }
        }

        private void StartPythonProcess(ConsoleControl.ConsoleControl console, string pythonFFP, string param = "")
        {
            if (!System.IO.File.Exists(pythonFFP))
                return;

            TryStartProcess(console, pythonFFP, param);
            this.startProcessButton.Enabled = false;
            ConsoleControlReLayout(console);
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string owner = CurrentConsoleOwnerString();
            if (string.IsNullOrEmpty(owner) || !consoles.ContainsKey(owner) || consoles[owner] == null)
            {
                this.startProcessButton.Enabled = true;
                return;
            }


            ConsoleControl.ConsoleControl console = consoles[owner];
            if (console.IsProcessRunning)
            {
                this.startProcessButton.Enabled = false;
                ConsoleControlReLayout(console);
            }
            else
                this.startProcessButton.Enabled = true;

        }

        private void ConsoleControlReLayout(ConsoleControl.ConsoleControl console)
        {
            console.Show();
            HideAllConsoleExceptOne(console.Name);
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
                if (console.ProcessInterface.Process != null && !console.ProcessInterface.Process.HasExited)
                    console.ProcessInterface.Process.Kill();
                //console.Dispose();
                //console = null;
            }
            catch
            {
                return false;
            }

            return true;
        }



    }
}