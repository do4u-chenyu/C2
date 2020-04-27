using Citta_T1.Utils;
using System;
using System.Diagnostics;
using System.Windows.Forms;
namespace Citta_T1.Controls.Left
{
    public partial class DataButton : UserControl
    {
        private DSUtil.Encoding encoding;
        private DSUtil.ExtType extType;
        private char separator;
        private int count = 0;
        public DSUtil.Encoding Encoding { get => this.encoding; set => this.encoding = value; }
        public DSUtil.ExtType ExtType { get => extType; set => extType = value; }
        public char Separator { get => separator; set => separator = value; }
        public string FullFilePath { get => this.txtButton.Name; set => this.txtButton.Name = value; }
        public string DataName { get => this.txtButton.Text; set => this.txtButton.Text = value; }
        public int Count
        { get => this.count;
            set
            {
                this.count = value;
            }
        }

        private static string RemoveMessageBoxTemplate = "有模型在使用此数据, 继续卸载请点击 \"确认\"";
        private static string DataButtonFlowTemplate  = "编码:{0} 文件类型:{1} 引用次数:{2} 分割符:{3}";


        public DataButton()
        {
            InitializeComponent();
        }
        public DataButton(string ffp, string dataName, char separator, DSUtil.ExtType extType, DSUtil.Encoding encoding)
        {
            InitializeComponent();
            txtButton.Name = ffp;
            txtButton.Text = dataName;
            this.separator = separator;
            this.extType = extType;
            this.encoding = encoding;
        }

        private void DataButton_Load(object sender, EventArgs e)
        {
            // 数据源全路径浮动提示信息
            String helpInfo = txtButton.Name;
            this.helpToolTip.SetToolTip(this.rightPictureBox, helpInfo);

            // 数据源名称浮动提示信息
            helpInfo = txtButton.Text;
            this.helpToolTip.SetToolTip(this.txtButton, helpInfo);

            helpInfo = String.Format(DataButtonFlowTemplate, 
                                    encoding.ToString(),
                                    this.ExtType,
                                    Global.GetModelDocumentDao().CountDataSourceUsage(this.FullFilePath),
                                    this.Separator == '\t' ? "TAB" : this.Separator.ToString());
            this.helpToolTip.SetToolTip(this.leftPictureBox, helpInfo);
        }


        #region 右键菜单
        private void ReviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Global.GetMainForm().PreViewDataByBcpPath(txtButton.Name, this.separator, this.extType, this.encoding);
        }

        private void RenameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 1. DataSource中重命名
            // 2. Program中重命名
            // TODO [DK] 3. 画布中已存在的该如何处理？ 
            ((DataButton)(this.Parent.Controls.Find(this.Name, false)[0])).txtButton.Text = "重命名";
        }

        private void RemoveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int count = Global.GetModelDocumentDao().CountDataSourceUsage(this.FullFilePath);
            DialogResult rs = DialogResult.OK;

            // 数据源引用大于0时,弹出警告窗
            if (count > 0)
                rs = MessageBox.Show(RemoveMessageBoxTemplate, 
                    "卸载 " + this.DataName, 
                    MessageBoxButtons.OKCancel, 
                    MessageBoxIcon.Information);

            if (rs != DialogResult.OK)
                return;

            // 卸载数据源
            Global.GetDataSourceControl().RemoveDataButton(this);
            // 引用不为0时,有可能还会预览该数据源的数据,此时不用移除buffer
            if (count == 0)
                BCPBuffer.GetInstance().Remove(this.FullFilePath);

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
                LogUtil logUtil = LogUtil.GetInstance("DataButton");
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

        private void LeftPictureBox_MouseEnter(object sender, EventArgs e)
        {
            string helpInfo = String.Format(DataButtonFlowTemplate,
                                        encoding.ToString(),
                                        this.ExtType,
                                        Global.GetModelDocumentDao().CountDataSourceUsage(this.FullFilePath),
                                        this.Separator == '\t' ? "TAB" : this.Separator.ToString());
            this.helpToolTip.SetToolTip(this.leftPictureBox, helpInfo);
        }
    }
}
