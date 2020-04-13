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
        private string dataPath0;
        private string dataPath1;
        private string[] columnName0;
        private string[] columnName1;
        public DifferOperatorView(MoveOpControl opControl)
        {
            InitializeComponent();
            this.opControl = opControl;
            InitOptionInfo();
            columnName0 = new string[] { };
            columnName1 = new string[] { };
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
            Dictionary<string, string> dataInfo = Global.GetOptionDao().GetDataSourceInfo(this.opControl.ID, false);
            if (dataInfo.ContainsKey("dataPath0") && dataInfo.ContainsKey("encoding0"))
            {
                this.dataPath0 = dataInfo["dataPath0"];
                this.dataSource0.Text = Path.GetFileNameWithoutExtension(this.dataPath0);
                columnName0 = SetOption(this.dataPath0, this.dataSource0.Text, dataInfo["encoding0"]);
            }
            if (dataInfo.ContainsKey("dataPath1") && dataInfo.ContainsKey("encoding1"))
            {
                this.dataPath1 = dataInfo["dataPath1"];
                this.dataSource1.Text = Path.GetFileNameWithoutExtension(dataInfo["dataPath1"]);
                columnName1 = SetOption(this.dataPath1, this.dataSource1.Text, dataInfo["encoding1"]);
            }
            //foreach (string name in this.columnName0)
            //{
            //this.d.AddItems(name);
            //this.MaxValueBox.Items.Add(name);
            //}
            //foreach (string name in this.columnName1)
            //{
            //this.OutList.AddItems(name);
            //this.MaxValueBox.Items.Add(name);
            //}
        }

        private string[] SetOption(string path, string dataName, string encoding)
        {

            BcpInfo bcpInfo = new BcpInfo(path, dataName, ElementType.Null, EnType(encoding));
            string column = bcpInfo.columnLine;
            string[] columnName = column.Split('\t');
            return columnName;
        }

        #endregion
        private DSUtil.Encoding EnType(string type)
        { return (DSUtil.Encoding)Enum.Parse(typeof(DSUtil.Encoding), type); }
    }
}
