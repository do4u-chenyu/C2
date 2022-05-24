using C2.Business.Model;
using C2.Core;
using C2.Dialogs;
using C2.Model.MindMaps;
using C2.Utils;
using System;
using System.IO;
using System.Windows.Forms;

namespace C2.Controls.Left
{
    public partial class MyMindMapControl : ManualControl
    {
        public MyMindMapControl()
        {
            InitializeComponent();
            InitializeOther();
        }

        private void InitializeOther()
        {
            PaintPanel.Controls.Remove(textBox1);
        }

        public override void AddButton(string modelName)
        {
            C2Button mb = new C2Button(modelName);
            // 获得当前要添加的model button的初始位置
            LayoutButton(mb);
            this.PaintPanel.Controls.Add(mb);
        }

        public void AddButton_Click(object sender, EventArgs e)
        {
            ZipDialog zipDialog = new ImportZipDialog();
            if (zipDialog.ShowDialog() == DialogResult.OK)
            {
                string fullFilePath = zipDialog.ModelPath;
                string filename = fullFilePath.Substring(fullFilePath.LastIndexOf("\\") + 1, fullFilePath.Length - fullFilePath.LastIndexOf("\\") - 1).Replace(".c2", string.Empty);
                string password = zipDialog.Password;
                if (Path.GetExtension(fullFilePath) == ".doc" || Path.GetExtension(fullFilePath) == ".docx")
                {
                    ImportWordFile.GetInstance().Import(fullFilePath);
                    return;
                }
                if (Path.GetExtension(fullFilePath) == ".xmind")
                {
                    ImportXmindFile.GetInstance().LoadXml(fullFilePath);
                    return;
                }
                using (GuarderUtil.WaitCursor)
                    if (ImportModel.GetInstance().UnZipC2File(fullFilePath, Global.GetMainForm().UserName, password))
                    {
                        new Log.Log().LogManualButton("分析笔记", "导入");
                        HelpUtil.ShowMessageBox(String.Format("[{0}] 导入 分析笔记 成功", filename));
                    }
            }
        }
    }
}
