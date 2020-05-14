using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Citta_T1.Utils;
using Citta_T1.IAOLab.PythonOP;

namespace Citta_T1.Controls.Bottom
{
    public partial class BottomConsoleControl : UserControl
    {
        private static LogUtil log = LogUtil.GetInstance("BottomPythonConsoleControl");
        private static string CmdConsoleString = "Cmd控制台";

        public BottomConsoleControl()
        {
            InitializeComponent();
            this.cmdConsoleControl.InternalRichTextBox.Font = new System.Drawing.Font("新宋体", 10F);
            this.cmdConsoleControl.Name = CmdConsoleString;
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
           
            return consoleControl;
        }

        private void ReleaseConsoleControl(ConsoleControl.ConsoleControl consoleControl)
        {

        }

        private void BottomPythonConsoleControl_Load(object sender, EventArgs e)
        {   // 设计器模式,直接返回
            if (DesignerModelClass.IsDesignerMode)
                return;
            this.comboBox1.SelectedIndex = 0;
            StartCmdProcess();
        }

        private void StartProcessButton_Click(object sender, EventArgs e)
        {
            StartCmdProcess();
            return;
        }

        private void ResetProcessButton_Click(object sender, EventArgs e)
        {


            //ConsoleControl.ConsoleControl console = this.cmdConsoleControl;

            //this.tableLayoutPanel1.Controls.Remove(console);

            //this.cmdConsoleControl = CreateNewConsoleControl(CmdConsoleString, true);
            //this.tableLayoutPanel1.Controls.Add(cmdConsoleControl, 0, 1);



            //StartCmdProcess();
            //if (console.IsProcessRunning)
            //{
            //    TryRleaseProcess(console);
            //}
            //else if (console != null)
            //{
            //    console.
            //    console.Dispose();
            //    console = null;
            //}
            //this.cmdConsoleControl.StopProcess();
            //this
            //this.cmdConsoleControl.StartProcess("cmd.exe", "");

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

            return String.Empty;
        }

        private void ClearScreenButton_Click(object sender, EventArgs e)
        {
            
            if (cmdConsoleControl != null && cmdConsoleControl.IsProcessRunning)
                cmdConsoleControl.ClearOutput();
        }

        private void CopyContentButton_Click(object sender, EventArgs e)
        {

            if (cmdConsoleControl != null && cmdConsoleControl.IsProcessRunning)
                FileUtil.TryClipboardSetText(cmdConsoleControl.InternalRichTextBox.Text);
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
            }
            else 
            {
                Environment.CurrentDirectory = targetDirectory;
                TryStartProcess(cmdConsoleControl, "cmd.exe", String.Empty);
                Environment.CurrentDirectory = currentDirectory;
            }
            this.startProcessButton.Enabled = false;
        }



        private void ConsoleControlReLayout(ConsoleControl.ConsoleControl console)
        {
            this.SuspendLayout();
            console.Show();
            this.ResumeLayout(false);
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
