using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Citta_T1.Dialogs
{
    public delegate void CoverDocumentEventHandler(string modelTitle);
    public partial class CreateNewModel : Form
    {
        private string modelTitle;
        public string ModelTitle { get => modelTitle; }
        public event CoverDocumentEventHandler CoverModelDocument;

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
            try
            {
                MainForm mainForm = (MainForm)this.Owner;
                Console.WriteLine(mainForm.GetUserName+"--------------");
                DirectoryInfo di = new DirectoryInfo(Directory.GetCurrentDirectory() + "\\cittaModelDocument\\" + mainForm.GetUserName + "\\");
                DirectoryInfo[] modelTitleList = di.GetDirectories();
                foreach (DirectoryInfo modelTitle in modelTitleList)
                {
                    if (this.textBoxEx1.Text == modelTitle.ToString())
                    {
                        DialogResult result = MessageBox.Show(this.textBoxEx1.Text + "已存在，要替换它吗？", "确认另存为", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (DialogResult.Yes == result)
                        {
                            this.modelTitle = this.textBoxEx1.Text;
                            CoverModelDocument?.Invoke(this.textBoxEx1.Text);                          
                            this.DialogResult = DialogResult.Cancel;
                            this.Close();
                            return;
                        }
                        else
                        { 
                            return;
                        }

                    }
                                            
                }
            }
            catch
            { }
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
