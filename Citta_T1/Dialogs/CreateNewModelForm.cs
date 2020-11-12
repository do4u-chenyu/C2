using C2.Business.Model;
using C2.Core;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace C2.Dialogs
{

    public partial class CreateNewModelForm : Form
    {
        private int titlePostfix;  // 模型建议命名的数字后缀
        private string modelTitle;
        public string ModelTitle { get => modelTitle; }
        public string ModelType { set => this.label1.Text = value; }
        public List<string> OpenDocuments { get; set; }


        public CreateNewModelForm()
        {
            InitializeComponent();
            OpenDocuments = new List<string>();
            this.label1.Text = "新建业务视图";
            this.Text = "新建业务视图";
            this.label1.Location = new Point(70,47);
            this.label2.Location = new Point(50,47);
            modelTitle = "";
            titlePostfix = 1;
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            string titleName = this.textBox.Text.Trim();

            if (titleName.Length == 0)
                return;
            if (FileUtil.ContainIllegalCharacters(titleName, "业务视图名")
               || FileUtil.NameTooLong(titleName, "业务视图名"))
            {
                this.textBox.Text = String.Empty;
                return;
            }
            // 业务视图已存在,提示并退出
            if (CheckModelTitelExists(titleName))
            {
                MessageBox.Show(titleName + "，已存在，请重新命名", "确认另存为", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.modelTitle = titleName;
            this.DialogResult = DialogResult.OK;
        }

        private void CreateNewModel_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.textBox.Text = "";
            if (this.DialogResult != DialogResult.OK)
                this.modelTitle = "";
        }

        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.textBox.ForeColor = System.Drawing.SystemColors.ControlText;
            // 按下回车键
            if (e.KeyChar == 13)
            {
                if (this.textBox.Text.Length == 0)
                    return;
                this.modelTitle = this.textBox.Text;
                this.DialogResult = DialogResult.OK;
            }
        }

        private void CreateNewModel_Load(object sender, EventArgs e)
        {
            string title = String.Format("业务视图{0}", this.titlePostfix);
            List<string> currentTitles = GetModelTitleList();

            while (currentTitles.Contains(title))
                title = String.Format("业务视图{0}", ++this.titlePostfix);
            this.textBox.Text = title;
            this.textBox.ForeColor = System.Drawing.SystemColors.ActiveCaption;
        }

        private void TextBox_MouseDown(object sender, MouseEventArgs e)
        {
            this.textBox.ForeColor = System.Drawing.SystemColors.ControlText;
        }

        private bool CheckModelTitelExists(string inputModelTitle)
        {
            return GetModelTitleList().Contains(inputModelTitle);
        }

        private List<string> GetModelTitleList()
        {
            List<string> titles = new List<string>();
            try
            {
                DirectoryInfo di = new DirectoryInfo(Path.Combine(Global.WorkspaceDirectory, Global.GetMainForm().UserName,"业务视图"));
                DirectoryInfo[] titleList = di.GetDirectories();
                foreach (DirectoryInfo title in titleList)
                    titles.Add(title.ToString());
            }
            catch
            { }
            titles.AddRange(OpenDocuments);
            return titles;
        }
    }
}
