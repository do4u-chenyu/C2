using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using Citta_T1.Utils;
using Citta_T1.Business.Model;


namespace Citta_T1.Dialogs
{

    public partial class CreateNewModel : Form
    {
        private int titlePostfix;  // 模型建议命名的数字后缀
        private string modelTitle;
        public string ModelTitle { get => modelTitle; }

        public CreateNewModel()
        {
            InitializeComponent();
            modelTitle = "";
            titlePostfix = 1;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            string inputTitleModel = this.textBox.Text.Trim();

            if (inputTitleModel.Length == 0)
                return;
            // 模型已存在,提示并推出
            if (CheckModelTitelExists(inputTitleModel))
            {
                MessageBox.Show(inputTitleModel + "，已存在，请重新命名", "确认另存为", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.DialogResult = DialogResult.Cancel;
                return;
            }

            this.modelTitle = inputTitleModel;
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
            string titile = String.Format("我的新模型{0}", this.titlePostfix++);
            List<string> titels = GetModelTitleList();

            while (titels.Contains(titile))
                titile = String.Format("我的新模型{0}", this.titlePostfix++);
            this.textBox.Text = titile;
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
            List<string> titels = new List<string>();
            try
            {
                DirectoryInfo di = new DirectoryInfo(Path.Combine(Global.WorkspaceDirectory, Global.GetMainForm().UserName));
                DirectoryInfo[] modelTitleList = di.GetDirectories();
                foreach (DirectoryInfo modelTitle in modelTitleList)
                    titels.Add(modelTitle.ToString());
            }
            catch
            { }

            foreach (ModelDocument md in Global.GetMainForm().DocumentsList())
                titels.Add(md.ModelTitle);
            return titels;
        }
    }
}
