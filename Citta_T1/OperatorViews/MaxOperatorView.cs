using Citta_T1.Business.Option;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Citta_T1.Utils;
using Citta_T1.Business.Model;
using Citta_T1.Controls.Move;
using System.IO;

namespace Citta_T1.OperatorViews
{
    public partial class MaxOperatorView : Form
    {
        private MoveOpControl opControl;
        private string dataPath = "";
        private string oldMaxfield;
        private List<int> oldOutList; 
        public MaxOperatorView(MoveOpControl opControl)
        {
            InitializeComponent();
            this.opControl = opControl;
            InitOptionInfor();
            LoadOption();
            this.oldMaxfield = this.MaxValueBox.Text;
            this.oldOutList = this.OutList.GetItemCheckIndex();


        }

        private void confirmButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            SaveOption();
            //内容修改，引起文档dirty
            if(this.oldMaxfield != this.MaxValueBox.Text)
                Global.GetMainForm().SetDocumentDirty();
            else if (!this.oldOutList.SequenceEqual(this.OutList.GetItemCheckIndex()))
                 Global.GetMainForm().SetDocumentDirty();

        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }
        #region 配置信息的保存与加载
        private void SaveOption()
        {
            List<int> checkIndexs = this.OutList.GetItemCheckIndex();
            string outField = string.Join(",", checkIndexs);
            //没有数据源，或者有数据源其他配置为空，直接返回
            if (this.DataInforBox.Text == "" || this.MaxValueBox.Text == "" && outField == "") return;
            this.opControl.Option.SetOption("maxfield", this.MaxValueBox.SelectedIndex.ToString());
            this.opControl.Option.SetOption("outfield", outField);
            if (this.MaxValueBox.Text != "" && outField != "")
                this.opControl.opViewStatus = true;

        }

        private void LoadOption()
        {
            if (this.opControl.Option.GetOption("maxfield") != "")
            {
                int index = Convert.ToInt32(this.opControl.Option.GetOption("maxfield"));
                this.MaxValueBox.Text = this.MaxValueBox.Items[index].ToString();
            }
            if (this.opControl.Option.GetOption("outfield") != "")
            {
                string[] checkIndexs = this.opControl.Option.GetOption("outfield").Split(',');
                this.OutList.LoadItemCheckIndex(Array.ConvertAll<string, int>(checkIndexs, int.Parse));
            }
        }
        #endregion
        #region 初始化配置
        private void InitOptionInfor()
        {
            string startID = "";
            string encoding = "";
            List<ModelRelation> modelRelations = Global.GetCurrentDocument().ModelRelations;
            List<ModelElement> modelElements = Global.GetCurrentDocument().ModelElements;
            foreach (ModelRelation mr in modelRelations)
            {
                if (mr.End == this.opControl.ID.ToString())
                {
                    startID = mr.Start;
                    break;
                }
            }
            foreach (ModelElement me in modelElements)
            {
                if (me.ID.ToString() == startID)
                {
                    this.dataPath = me.GetPath();
                    //设置数据信息选项
                    this.DataInforBox.Text = Path.GetFileNameWithoutExtension(this.dataPath);
                    encoding = me.Encoding.ToString();
                    break;
                }
            }
            SetOption(this.dataPath, this.DataInforBox.Text, encoding);

        }
        private void SetOption(string path, string dataName, string encoding)
        {
            if (path == "") return;
            BcpInfo bcpInfo = new BcpInfo(path, dataName, ElementType.Null, EnType(encoding));
            string column = bcpInfo.columnLine;
            string[] columnName = column.Split('\t');
            foreach (string name in columnName)
            {
                this.OutList.AddItems(name);
                this.MaxValueBox.Items.Add(name);
            }
        }

        #endregion
        private DSUtil.Encoding EnType(string type)
        { return (DSUtil.Encoding)Enum.Parse(typeof(DSUtil.Encoding), type); }


    }

}
