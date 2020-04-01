using Citta_T1.Business.Model;
using Citta_T1.Business.Option;
using Citta_T1.Controls.Move;
using Citta_T1.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Citta_T1.Utils;

namespace Citta_T1.OperatorViews
{
    public partial class MaxOperatorView : Form
    {
        private MoveOpControl opControl;
        private string dataPath;
        private string oldMaxfield;
        private List<int> oldOutList;
        private ElementStatus oldstatus;
        public MaxOperatorView(MoveOpControl opControl)
        {
            InitializeComponent();
            dataPath = "";
            this.opControl = opControl;
            InitOptionInfo();
            LoadOption();
            
            this.oldMaxfield = this.MaxValueBox.Text;
            this.oldOutList = this.OutList.GetItemCheckIndex();
            this.oldstatus = opControl.Status;


        }
        #region 添加取消
        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            //未设置字段警告
            if (this.MaxValueBox.Text == "")
            {
                MessageBox.Show("请选择最大值字段!");
                return;
            }
            if (this.OutList.GetItemCheckIndex().Count == 0)
            {
                MessageBox.Show("请选择输出字段!");
                return;
            }
            this.DialogResult = DialogResult.OK;
            if (this.DataInfoBox.Text == "") return;
            SaveOption();
            //内容修改，引起文档dirty
            if (this.oldMaxfield != this.MaxValueBox.Text)
                Global.GetMainForm().SetDocumentDirty();
            else if (!this.oldOutList.SequenceEqual(this.OutList.GetItemCheckIndex()))
                Global.GetMainForm().SetDocumentDirty();
            //生成结果控件,创建relation,bcp结果文件
            if (this.oldstatus == ElementStatus.Null)
            {
                foreach (ModelRelation mr in Global.GetCurrentDocument().ModelRelations)
                    if (mr.Start == this.opControl.ID) return;
                int x = this.opControl.Location.X + this.opControl.Width + 15;
                int y = this.opControl.Location.Y;
                string tmpName = "Result" + DateTime.Now.ToString("yyyyMMdd") + this.opControl.ID.ToString();
                MoveRsControl mrc = Global.GetCanvasPanel().AddNewResult(0, tmpName, new Point(x,y));
                Global.GetModelDocumentDao().AddDocumentRelation(this.opControl.ID, mrc.ID, this.opControl.Location, mrc.Location, 1);
                string path = BCPBuffer.GetInstance().CreateNewBCPFile(tmpName);
                mrc.Path = path;
            }

        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }
        #endregion
        #region 配置信息的保存与加载
        private void SaveOption()
        {
            List<int> checkIndexs = this.OutList.GetItemCheckIndex();
            string outField = string.Join(",", checkIndexs);
            if(this.MaxValueBox.Text == "")
                this.opControl.Option.SetOption("maxfield", "");
            else
                this.opControl.Option.SetOption("maxfield", this.MaxValueBox.SelectedIndex.ToString());
            this.opControl.Option.SetOption("outfield", outField);
            if (this.MaxValueBox.Text != "" && outField != "")
                this.opControl.Status = ElementStatus.Ready;

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
        private void InitOptionInfo()
        {
            int startID = -1;
            string encoding = "";
            List<ModelRelation> modelRelations = Global.GetCurrentDocument().ModelRelations;
            List<ModelElement> modelElements = Global.GetCurrentDocument().ModelElements;
            foreach (ModelRelation mr in modelRelations)
            {
                if (mr.End == this.opControl.ID)
                {
                    startID = mr.Start;
                    break;
                }
            }
            foreach (ModelElement me in modelElements)
            {
                if (me.ID == startID)
                {
                    this.dataPath = me.GetPath();
                    //设置数据信息选项
                    this.DataInfoBox.Text = Path.GetFileNameWithoutExtension(this.dataPath);
                    encoding = me.Encoding.ToString();
                    break;
                }
            }
            if(this.dataPath != "") 
                SetOption(this.dataPath, this.DataInfoBox.Text, encoding);

        }
        private void SetOption(string path, string dataName, string encoding)
        {
 
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
