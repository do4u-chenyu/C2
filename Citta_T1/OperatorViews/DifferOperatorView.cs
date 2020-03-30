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
    public partial class DifferOperatorView : Form
    {
        private MoveOpControl opControl;
        private string dataPath1;
        private string dataPath2;
        public DifferOperatorView(MoveOpControl opControl)
        {
            InitializeComponent();
            this.opControl = opControl;
            dataPath1 = "";
            dataPath2 = "";
        }
        #region 添加取消
        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }
        #endregion
        #region 初始化配置
        private void InitOptionInfo()
        {
            int startID1 = -1;
            int startID2 = -1;
            string encoding1 = "";
            string encoding2 = "";
            List<ModelRelation> modelRelations = Global.GetCurrentDocument().ModelRelations;
            List<ModelElement> modelElements = Global.GetCurrentDocument().ModelElements;
            foreach (ModelRelation mr in modelRelations)
            {
                //左侧数据源
                if (mr.End == this.opControl.ID && mr.EndPin == 1)
                    startID1 = mr.Start;
                //右侧数据源
                if (mr.End == this.opControl.ID && mr.EndPin == 2)
                    startID2 = mr.Start;
            }
            foreach (ModelElement me in modelElements)
            {
                if (me.ID == startID1)
                {
                    this.dataPath1 = me.GetPath();
                    this.DataInfo1.Text = Path.GetFileNameWithoutExtension(this.dataPath1);
                    encoding1 = me.Encoding.ToString();
                }
                if (me.ID == startID2)
                {
                    this.dataPath2 = me.GetPath();
                    this.DataInfo1.Text = Path.GetFileNameWithoutExtension(this.dataPath2);
                    encoding2 = me.Encoding.ToString();
                }
            }
            if (this.dataPath1 != "")
                SetOption(this.dataPath1, this.DataInfo1.Text, encoding1);
            if (this.dataPath2 != "")
                SetOption(this.dataPath2, this.DataInfo2.Text, encoding2);

        }
        private void SetOption(string path, string dataName, string encoding)
        {
            BcpInfo bcpInfo = new BcpInfo(path, dataName, ElementType.Null, EnType(encoding));
            string column = bcpInfo.columnLine;
            string[] columnName = column.Split('\t');
            foreach (string name in columnName)
            {
                this.OutList.AddItems(name);

            }
        }

        #endregion
        private DSUtil.Encoding EnType(string type)
        { return (DSUtil.Encoding)Enum.Parse(typeof(DSUtil.Encoding), type); }
    }
}
