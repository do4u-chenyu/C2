using System;
using System.Diagnostics;
using System.Windows.Forms;
using Citta_T1.Utils;
using System.Reflection;
namespace Citta_T1.Controls.Left
{
    public partial class DataButton : UserControl
    {
        public DSUtil.Encoding encoding;
        private int count = 0;
        public DSUtil.Encoding Encoding { get => this.encoding; set => this.encoding = value; }
        public string FilePath { get => this.txtButton.Name; set => this.txtButton.Name = value; }
        public string DataName { get => this.txtButton.Text; set => this.txtButton.Text = value; }
        public int Count
        { get => this.count;
            set
            {
                this.count = value;
                EnableDeleteDataSource(this.count);
            }
        }
        public DataButton()
        {
            InitializeComponent();
        }
        public DataButton(string ffp, string dataName, DSUtil.Encoding encoding)
        {
            InitializeComponent();
            txtButton.Name = ffp;
            txtButton.Text = dataName;
            this.encoding = encoding;
        }
        private void moveOpControl1_Load(object sender, EventArgs e)
        {

        }

        private void rightPictureBox_MouseEnter(object sender, EventArgs e)
        {
            //String helpInfo = Program.inputDataDict[txtButton.Name].filePath;
            String helpInfo = txtButton.Name;
            this.helpToolTip.SetToolTip(this.rightPictureBox, helpInfo);
        }
        #region 右键菜单
        private void ReviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Global.GetMainForm().PreViewDataByBcpPath(txtButton.Name, this.encoding);
        }

        private void RenameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 1. DataSource中重命名
            // 2. Program中重命名
            // TODO [DK] 3. 画布中已存在的该如何处理？ 
            ((DataButton)(this.Parent.Controls.Find(this.Name, false)[0])).txtButton.Text = "重命名";
        }

        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 1. DataSource中删除控件
            // 2. Program中删除数据
            // TODO [DK] 3. 画布中已存在的该如何处理？ 
            this.Parent.Controls.Remove(this);
            BCPBuffer.GetInstance().Remove(this.txtButton.Name);

        }
        #endregion

        private void OpenFilePathMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ProcessStartInfo processStartInfo = new ProcessStartInfo();
                processStartInfo.FileName = "explorer.exe";  //资源管理器
                processStartInfo.Arguments = "/e,/select," + txtButton.Name;
                System.Diagnostics.Process.Start(processStartInfo);
            }
            catch (System.ComponentModel.Win32Exception ex) 
            {
                LogUtil logUtil = new LogUtil();
                logUtil.Error(ex.Message);
                //某些机器直接打开文档目录会报“拒绝访问”错误，此时换一种打开方式
                ReplaceOpenMethod();
            }
        }
        private void ReplaceOpenMethod()
        {
            try
            {
                ProcessStartInfo processStartInfo = new ProcessStartInfo();
                processStartInfo.FileName = "explorer.exe";  //资源管理器
                processStartInfo.Arguments = System.IO.Path.GetDirectoryName(txtButton.Name);
                System.Diagnostics.Process.Start(processStartInfo);
            }
            catch { };
        }
        private void CopyFilePathToClipboard(object sender, EventArgs e)
        {
            Clipboard.SetText(txtButton.Name);
        }
        private void EnableDeleteDataSource(int count)
        {
            if(count>0)
                this.DeleteToolStripMenuItem.Enabled = true;

        }
    }
}
