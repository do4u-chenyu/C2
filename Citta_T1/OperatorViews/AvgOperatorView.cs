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
    public partial class AvgOperatorView : Form
    {
        private MoveOpControl opControl;
        private string oldAvg;
        private string dataPath = "";
        private string[] columnName;
        private List<string> selectName;
        private string oldOptionDict;
        public AvgOperatorView(MoveOpControl opControl)
        {
            InitializeComponent();
            selectName = new List<string>();
            this.opControl = opControl;
            InitOptionInfor();
            LoadOption();
           
            this.oldAvg = this.AvgComBox.Text;
            this.oldOptionDict = string.Join(",", this.opControl.Option.OptionDict.ToList());
        }
        #region 初始化配置
        private void InitOptionInfor()
        {
            int startID = -1;
            string encoding = "";
            List<ModelRelation> modelRelations = Global.GetCurrentDocument().ModelRelations;
            List<ModelElement> modelElements = Global.GetCurrentDocument().ModelElements;
            foreach (ModelRelation mr in modelRelations)
            {
                if (mr.EndID== this.opControl.ID)
                {
                    startID = mr.StartID;
                    break;
                }
            }
            foreach (ModelElement me in modelElements)
            {
                if (me.ID == startID)
                {
                    this.dataPath = me.GetPath();
                    //设置数据信息选项
                    this.DataInfo.Text = Path.GetFileNameWithoutExtension(this.dataPath);
                    encoding = me.Encoding.ToString();
                    break;
                }
            }
            if (this.dataPath != "")
                SetOption(this.dataPath, this.DataInfo.Text, encoding);

        }
        private void SetOption(string path, string dataName, string encoding)
        {
            BcpInfo bcpInfo = new BcpInfo(path, dataName, ElementType.Null, EnType(encoding));
            string column = bcpInfo.columnLine;
            this.columnName = column.Split('\t');
            foreach (string name in this.columnName)
                this.AvgComBox.Items.Add(name);

        }

        #endregion

        private void confirmButton_Click(object sender, EventArgs e)
        {
            //未设置字段警告
            if (this.DataInfo.Text == "") return;
            if (this.AvgComBox.Text == "")
            {
                MessageBox.Show("请选择平均值字段!");
                return;
            }
            this.DialogResult = DialogResult.OK;
           
            SaveOption();
            //内容修改，引起文档dirty
            if (this.oldAvg != this.AvgComBox.Text)
                Global.GetMainForm().SetDocumentDirty();
            //生成结果控件,创建relation,bcp结果文件
            this.selectName.Add(this.AvgComBox.SelectedItem.ToString());
            if (this.oldOptionDict == "")
                Global.GetOptionDao().CreateResultControl(this.opControl, this.selectName);

        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }
        #region 配置信息的保存与加载
        private void SaveOption()
        {

            this.opControl.Option.SetOption("avgfield", this.AvgComBox.SelectedIndex.ToString());
            this.opControl.Option.SetOption("outfield", this.AvgComBox.SelectedIndex.ToString());
            this.opControl.Status = ElementStatus.Ready;

        }

        private void LoadOption()
        {
            if (this.opControl.Option.GetOption("avgfield") != "")
            {
                int index = Convert.ToInt32(this.opControl.Option.GetOption("avgfield"));
                this.AvgComBox.Text = this.AvgComBox.Items[index].ToString();
            }
        }
        #endregion
        private DSUtil.Encoding EnType(string type)
        { return (DSUtil.Encoding)Enum.Parse(typeof(DSUtil.Encoding), type); }
    }
}
