using System;
using System.IO;
using System.Windows.Forms;
using Citta_T1.Utils;
using Citta_T1.Business.Model;


namespace Citta_T1.Dialogs
{

    public partial class CreateNewModel : Form
    {
        private string modelTitle;
        public string ModelTitle { get => modelTitle; }

        public CreateNewModel()
        {
            InitializeComponent();
            modelTitle = "";
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
            if (this.textBoxEx1.Text.Length == 0)
                return;
            MainForm mainForm = (MainForm)this.Owner;
            try
            {             
                DirectoryInfo di = new DirectoryInfo(Path.Combine(Global.WorkspaceDirectory, mainForm.UserName));
                DirectoryInfo[] modelTitleList = di.GetDirectories();
                foreach (DirectoryInfo modelTitle in modelTitleList)
                {
                    if (this.textBoxEx1.Text == modelTitle.ToString())
                    {
                        DialogResult result = MessageBox.Show(this.textBoxEx1.Text + "已存在，请重名", "确认另存为", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        if (DialogResult.OK == result)
                             return;
                    }                                           
                }
             
            }
            catch
            { 
                Console.WriteLine("CreateNewMode.AddButton_Click occurs error!");
            }
            //与内存中命名相同
            foreach (ModelDocument md in mainForm.DocumentsList())
            {
                if (this.textBoxEx1.Text == md.ModelTitle)
                {
                    DialogResult result = MessageBox.Show(this.textBoxEx1.Text + "已存在，请重名", "确认另存为", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    if (DialogResult.OK == result)
                        return;
                }
            }
            this.modelTitle = this.textBoxEx1.Text;
            this.DialogResult = DialogResult.OK;

        }

        private void CreateNewModel_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.textBoxEx1.Text = "";
            if (this.DialogResult != DialogResult.OK)
                this.modelTitle = "";
        }

        private void textBoxEx1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 按下回车键
            if (e.KeyChar == 13)
            {
                if (this.textBoxEx1.Text.Length == 0)
                    return;
                this.modelTitle = this.textBoxEx1.Text;
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}
