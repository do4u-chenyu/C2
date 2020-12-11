using C2.Business.Model;
using C2.Controls.MapViews;
using C2.Core;
using C2.Forms;
using C2.Model.MindMaps;
using C2.Model.Widgets;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace C2.Dialogs
{

    public partial class CreateNewModelForm : Form
    {
        public int TitlePostfix { set; get; }  // 模型建议命名的数字后缀
        public string ModelTitle { set; get; }
        public string ModelType { set => this.label1.Text = value; }
        public List<string> OpenDocuments { get; set; }
        public FormType NewFormType { set; get; }
        public MindMapView RelateMindMapView { set; get; }

        public CreateNewModelForm()
        {
            InitializeComponent();
            TitlePostfix = 1;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            CheckName();
        }
        private void CheckName()
        {
            string titleName = this.textBox.Text.Trim();
            if (titleName.Length == 0)
                return;

            string target = GenTargetName(NewFormType);
            if (FileUtil.IsContainIllegalCharacters(titleName, target) || FileUtil.NameTooLong(titleName, target))
            {
                this.textBox.Text = String.Empty;
                return;
            }

            // 视图已存在,提示并退出
            if (CheckModelTitelExists(titleName))
                return;

            ModelTitle = titleName;
            this.DialogResult = DialogResult.OK;
        }
        private  string GenTargetName(FormType formType)
        {
            switch (formType)
            {
                case FormType.DocumentForm:
                    return "业务视图名";
                case FormType.CanvasForm:
                    return "模型视图名";
                default:
                    return "视图名";
            }
        }

        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.textBox.ForeColor = System.Drawing.SystemColors.ControlText;
            // 按下回车键
            if (e.KeyChar == 13)
            {
                CheckName();
            }
        }

        private void CreateNewModel_Load(object sender, EventArgs e)
        {
            //现在load的时候需要分两种
            string title = "";
            if(NewFormType == FormType.DocumentForm)
            {
                title = String.Format("业务视图{0}", TitlePostfix);
                List<string> currentTitles = GetModelTitleList();

                while (currentTitles.Contains(title))
                    title = String.Format("业务视图{0}", ++TitlePostfix);
            }
            else if(NewFormType == FormType.CanvasForm)
            {
                title = String.Format("模型视图{0}", TitlePostfix);
                List<string> currentTitles = GetMindMapTitleList();

                while (currentTitles.Contains(title))
                    title = String.Format("模型视图{0}", ++TitlePostfix);
            }
            else
            {
                title = ModelTitle;
            }
            this.textBox.Text = title;
            this.textBox.ForeColor = SystemColors.ActiveCaption;
        }

        private void TextBox_MouseDown(object sender, MouseEventArgs e)
        {
            this.textBox.ForeColor = System.Drawing.SystemColors.ControlText;
        }

        private bool CheckModelTitelExists(string inputModelTitle)
        {
            if((NewFormType == FormType.DocumentForm && GetModelTitleList().Contains(inputModelTitle)) ||
                (NewFormType == FormType.CanvasForm && GetMindMapTitleList().Contains(inputModelTitle)) ||
                (NewFormType == FormType.Null && Global.GetMyModelControl().ContainModel(inputModelTitle)))
            {
                MessageBox.Show(inputModelTitle + "，已存在，请重新命名", "已存在", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }
            else
                return false;
        }

        private List<string> GetMindMapTitleList()
        {
            List<string> titles = new List<string>();
            Topic[] topics = RelateMindMapView.Map.GetTopics(false);
            foreach(Topic topic in topics)
            {
                OperatorWidget opw = topic.FindWidget<OperatorWidget>();
                if(opw!=null && opw.HasModelOperator)
                {
                    titles.Add(opw.ModelDataItem.FileName);
                }
            }
            return titles;
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
