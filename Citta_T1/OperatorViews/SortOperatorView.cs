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
    public partial class SortOperatorView : Form
    {
        private MoveOpControl opControl;
        private string dataPath;
        private string[] columnName;
        private string oldOptionDict;
        private List<int> oldOutList;
        private List<string> selectColumn;
        private List<bool> oldCheckedItems = new List<bool>();
        private string oldFirstRow;
        private string oldEndRow; 
        public SortOperatorView(MoveOpControl opControl)
        {
            InitializeComponent();
            /*
            this.opControl = opControl;
            dataPath = "";
            InitOptionInfo();
            LoadOption();
            this.oldFirstRow = this.firstRow.Text;
            this.oldEndRow = this.endRow.Text;
            this.oldOutList = this.outList.GetItemCheckIndex();
            this.oldCheckedItems.Add(this.noRepetition.Checked);
            this.oldCheckedItems.Add(this.repetition.Checked);
            this.oldCheckedItems.Add(this.ascendingOrder.Checked);
            this.oldCheckedItems.Add(this.descendingOrder.Checked);
            this.oldOptionDict = string.Join(",", this.opControl.Option.OptionDict.ToList());
            */
        }
        /*
        #region 配置初始化
        private void InitOptionInfo()
        {
            Dictionary<string, string> dataInfo = Global.GetOptionDao().GetDataSourceInfo(this.opControl.ID);
            if (dataInfo.ContainsKey("dataPath0") && dataInfo.ContainsKey("encoding0"))
            {
                this.dataPath = dataInfo["dataPath0"];
                this.dataInfo.Text = Path.GetFileNameWithoutExtension(this.dataPath);
                SetOption(this.dataPath, this.dataInfo.Text, dataInfo["encoding0"]);
            }
        }

        private void SetOption(string path, string dataName, string encoding)
        {

            BcpInfo bcpInfo = new BcpInfo(path, dataName, ElementType.Null, EnType(encoding));
            string column = bcpInfo.columnLine;
            this.columnName = column.Split('\t');
            foreach (string name in this.columnName)
                this.outList.AddItems(name);
        }
        private DSUtil.Encoding EnType(string type)
        { return (DSUtil.Encoding)Enum.Parse(typeof(DSUtil.Encoding), type); }
        #endregion
        #region 添加取消
        private void confirmButton_Click(object sender, EventArgs e)
        {
            if (this.outList.GetItemCheckIndex().Count == 0)
            {
                MessageBox.Show("请选择输出字段!");
                return;
            }
            if (!this.repetition.Checked && !this.noRepetition.Checked)
            {
                MessageBox.Show("请选择数据是否进行去重");
                return;
            }
            if (!this.ascendingOrder.Checked && !this.descendingOrder.Checked)
            {
                MessageBox.Show("请选择数据排序");
                return;
            }
            if (this.firstRow.Text == "" ^ this.endRow.Text == "")
            {
                MessageBox.Show("请选择输出条数");
                return;
            }
            this.DialogResult = DialogResult.OK;
            if (this.dataInfo.Text == "") return;
            SaveOption();
            //内容修改，引起文档dirty
            
            if (this.oldCheckedItems[0] != this.noRepetition.Checked)
                Global.GetMainForm().SetDocumentDirty();
            else if (this.oldCheckedItems[1] != this.repetition.Checked)
                Global.GetMainForm().SetDocumentDirty();
            else if (this.oldCheckedItems[2] != this.ascendingOrder.Checked)
                Global.GetMainForm().SetDocumentDirty();
            else if (this.oldCheckedItems[3] != this.descendingOrder.Checked)
                Global.GetMainForm().SetDocumentDirty();
            else if (!this.oldOutList.SequenceEqual(this.outList.GetItemCheckIndex()))
                Global.GetMainForm().SetDocumentDirty();
            else if(this.oldFirstRow!=this.firstRow.Text)
                Global.GetMainForm().SetDocumentDirty();
            else if(this.oldEndRow!=this.endRow.Text)
                Global.GetMainForm().SetDocumentDirty();
            //生成结果控件,创建relation,bcp结果文件
            this.selectColumn = this.outList.GetItemCheckText();

            if (this.oldOptionDict == "")
                Global.GetOptionDao().CreateResultControl(this.opControl, this.selectColumn);
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

            this.opControl.Option.SetOption("outfield", outField);
            this.opControl.Option.SetOption("noRepetition", this.noRepetition.Checked.ToString());
            this.opControl.Option.SetOption("repetition", this.repetition.Checked.ToString());
            this.opControl.Option.SetOption("ascendingOrder", this.ascendingOrder.Checked.ToString());
            this.opControl.Option.SetOption("descendingOrder", this.descendingOrder.Checked.ToString());
            this.opControl.Option.SetOption("firstRow", this.firstRow.Text);         
            this.opControl.Option.SetOption("endRow", this.endRow.Text);

            this.opControl.Status = ElementStatus.Ready;

        }

        private void LoadOption()
        {
            if (this.opControl.Option.GetOption("noRepetition") != "")
                this.noRepetition.Checked = Convert.ToBoolean(this.opControl.Option.GetOption("noRepetition"));
            if (this.opControl.Option.GetOption("repetition") != "")
                this.repetition.Checked = Convert.ToBoolean(this.opControl.Option.GetOption("repetition"));
            if (this.opControl.Option.GetOption("ascendingOrder") != "")
                this.ascendingOrder.Checked = Convert.ToBoolean(this.opControl.Option.GetOption("ascendingOrder"));
            if (this.opControl.Option.GetOption("descendingOrder") != "")
                this.descendingOrder.Checked = Convert.ToBoolean(this.opControl.Option.GetOption("descendingOrder"));
            if (this.opControl.Option.GetOption("firstRow") != "")
                this.firstRow.Text = this.opControl.Option.GetOption("firstRow");
            if (this.opControl.Option.GetOption("endRow") != "")
                this.endRow.Text = this.opControl.Option.GetOption("endRow");
            if (this.opControl.Option.GetOption("outfield") != "")
            {
                string[] checkIndexs = this.opControl.Option.GetOption("outfield").Split(',');
                this.outList.LoadItemCheckIndex(Array.ConvertAll<string, int>(checkIndexs, int.Parse));
            }
        }
        #endregion
        */
        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(this.BackColor);
        }

        private void groupBox2_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(this.BackColor);
        }
    }
}
