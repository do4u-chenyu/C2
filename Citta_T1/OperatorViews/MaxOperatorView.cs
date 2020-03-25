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
        public MaxOperatorView( MoveOpControl opControl)
        {
            InitializeComponent();
            this.opControl = opControl;
            InitOptionInfor();
            LoadOption();

        }

        private void confirmButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            SaveOption();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }
        #region 配置信息的保存与加载
        private void SaveOption()
        {
            this.opControl.Option.SetOption("max", this.MaxValueBox.Text);
            List<int> checkIndexs = this.OutList.GetItemCheckIndex();
            string outField = string.Join(",", checkIndexs);
            this.opControl.Option.SetOption("out", outField);
            this.opControl.Option.SetOption("dataInfor",this.DataInforBox.Text);
            this.opControl.opViewStatus = true;
        }

        private void LoadOption()
        {         
            this.MaxValueBox.Text = this.opControl.Option.GetOption("max");
            string outFields = this.opControl.Option.GetOption("out");
            if (outFields != "")
            {
                string[] checkIndexs = outFields.Split(',');
                this.OutList.LoadItemCheckIndex(Array.ConvertAll<string, int>(checkIndexs, int.Parse));
            }
        }
        #endregion
        #region 初始化配置
        private void InitOptionInfor()
        {
            string dataName = "";
            string startID="";
            string encoding = "";
            List<ModelRelation> modelRelations = Global.GetCurrentDocument().ModelRelations;
            List<ModelElement> modelElements = Global.GetCurrentDocument().ModelElements;
            foreach (ModelRelation mr in modelRelations)
            {
                if (mr.End == this.opControl.ID.ToString())
                {
                    startID =mr.Start;
                    break;
                }                  
            }
            foreach (ModelElement me in modelElements)
            {
                if (me.ID.ToString() == startID)
                { 
                    dataName = me.GetPath();
                    encoding = me.Encoding.ToString();
                    break;
                }                    
            }
            //设置数据信息选项
            this.DataInforBox.Text = Path.GetFileNameWithoutExtension(@dataName);
            //设置输出选项与最大值选项
            SetOption(dataName, this.DataInforBox.Text, encoding);
        }
        private void SetOption(string path, string dataName, string encoding)
        {            
            BcpInfo bcpInfo = new BcpInfo(path, dataName, ElementType.Null, EnType(encoding));
            string column=bcpInfo.columnLine;
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
