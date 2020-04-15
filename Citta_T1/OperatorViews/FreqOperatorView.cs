using Citta_T1.Business.Model;
using Citta_T1.Business.Option;
using Citta_T1.Controls.Move;
using Citta_T1.Utils;
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

namespace Citta_T1.OperatorViews
{
    public partial class FreqOperatorView : Form
    {
        private MoveOpControl opControl;
        private string dataPath;
        private string[] columnName;
        public FreqOperatorView(MoveOpControl opControl)
        {
            InitializeComponent();
            this.opControl = opControl;
            dataPath = "";
            InitOptionInfo();
        }
        #region 初始化配置
        private void InitOptionInfo()
        {
            Dictionary<string, string> dataInfo = Global.GetOptionDao().GetDataSourceInfo(this.opControl.ID);
            if (dataInfo.ContainsKey("dataPath0") && dataInfo.ContainsKey("encoding0"))
            {
                this.dataPath = dataInfo["dataPath0"];
                this.textBoxEx1.Text = Path.GetFileNameWithoutExtension(this.dataPath);
                SetOption(this.dataPath, this.textBoxEx1.Text, dataInfo["encoding0"]);
            }
        }

        private void SetOption(string path, string dataName, string encoding)
        {

            BcpInfo bcpInfo = new BcpInfo(path, dataName, ElementType.Null, EnType(encoding));
            string column = bcpInfo.columnLine;
            this.columnName = column.Split('\t');
            foreach (string name in this.columnName)
            {
                this.outList.AddItems(name);
            }
        }
        #endregion
        #region 添加取消
        private void confirmButton_Click(object sender, EventArgs e)
        {
        //    //未设置字段警告           
        //    if (this.outList.GetItemCheckIndex().Count == 0)
        //    {
        //        MessageBox.Show("请选择输出字段!");
        //        return;
        //    }
        //    if (!this.Repetition.Checked && !this.NoRepetition.Checked)
        //    {
        //        MessageBox.Show("请选择数据是否进行去重");
        //        return;
        //    }
        //    if (!this.Repetition.Checked && !this.NoRepetition.Checked)
        //    {
        //        MessageBox.Show("请选择数据排序");
        //        return;
        //    }
        //    this.DialogResult = DialogResult.OK;
        //    if (this.DataInfoBox.Text == "") return;
        //    SaveOption();
        //    //内容修改，引起文档dirty
        //    if (this.oldMaxfield != this.MaxValueBox.Text)
        //        Global.GetMainForm().SetDocumentDirty();
        //    else if (!this.oldOutList.SequenceEqual(this.OutList.GetItemCheckIndex()))
        //        Global.GetMainForm().SetDocumentDirty();
        //    //生成结果控件,创建relation,bcp结果文件
        //    this.selectColumn = this.OutList.GetItemCheckText();

        //    if (this.oldOptionDict == "")
        //        Global.GetOptionDao().CreateResultControl(this.opControl, this.selectColumn);

        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }
        #endregion
        #region 配置信息的保存与加载
        private void SaveOption()
        {
            List<int> checkIndexs = this.outList.GetItemCheckIndex();
            string outField = string.Join(",", checkIndexs);
            //if (this.MaxValueBox.Text == "")
            //    this.opControl.Option.SetOption("maxfield", "");
            //else
            //    this.opControl.Option.SetOption("maxfield", this.MaxValueBox.SelectedIndex.ToString());
            //this.opControl.Option.SetOption("outfield", outField);
            //if (this.MaxValueBox.Text != "" && outField != "")
            //    this.opControl.Status = ElementStatus.Ready;

        }

        private void LoadOption()
        {
            if (this.opControl.Option.GetOption("maxfield") != "")
            {
                int index = Convert.ToInt32(this.opControl.Option.GetOption("maxfield"));
                //this.MaxValueBox.Text = this.MaxValueBox.Items[index].ToString();
            }
            if (this.opControl.Option.GetOption("outfield") != "")
            {
                string[] checkIndexs = this.opControl.Option.GetOption("outfield").Split(',');
                //this.OutList.LoadItemCheckIndex(Array.ConvertAll<string, int>(checkIndexs, int.Parse));
            }
        }
        #endregion
        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(this.BackColor);
        }

        private void groupBox2_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(this.BackColor);
        }
        private DSUtil.Encoding EnType(string type)
        { return (DSUtil.Encoding)Enum.Parse(typeof(DSUtil.Encoding), type); }
    }
}
