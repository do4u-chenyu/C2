using Citta_T1.Business.Model;
using Citta_T1.Business.Option;
using Citta_T1.Controls;
using Citta_T1.Controls.Move;
using Citta_T1.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Citta_T1.OperatorViews
{
    public partial class MaxOperatorView : Form
    {
        private MoveOpControl opControl;
        private string dataPath;
        private string oldMaxfield;
        private List<int> oldOutList;
        private ElementStatus oldstatus;
        private string[] columnName;
        private LogUtil log = LogUtil.GetInstance("MoveRsControl");
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
                    if (mr.StartID == this.opControl.ID) return;
                int x = this.opControl.Location.X + this.opControl.Width + 15;
                int y = this.opControl.Location.Y;
                string tmpName = "Result" + DateTime.Now.ToString("yyyyMMdd") + this.opControl.ID.ToString();
                MoveRsControl mrc = Global.GetCanvasPanel().AddNewResult(0, tmpName, new Point(x,y));
                /*
                 * TODO [DK] 添加线
                 * 1. 形成线。以OpCotrol的右针脚为起点，以RS的左针脚为起点，形成线段
                 * 2. 控件绑定线。OpControl绑定线，RsControl绑定线
                 */
                Bezier line = new Bezier(
                    new PointF(
                        this.opControl.rectOut.Location.X + this.opControl.Location.X,
                        this.opControl.rectOut.Location.Y + this.opControl.Location.Y
                        ),
                    new PointF(
                        mrc.rectIn.Location.X + mrc.Location.X,
                        mrc.rectIn.Location.Y + mrc.Location.Y)
                );
                // TODO 绑定线和控件
                log.Info("MaxOperatorView.ConfirmButton_Click : line.x" + line.StartP + "line.y" + line.EndP);

                CanvasPanel canvas = Global.GetCanvasPanel();
                CanvasWrapper canvasWrp = new CanvasWrapper(canvas, canvas.CreateGraphics(), new Rectangle());
                canvas.RepaintObject(line);

                Global.GetModelDocumentDao().AddDocumentRelation(
                    this.opControl.ID, mrc.ID,
                    new Point(this.opControl.rectOut.Location.X + this.opControl.Location.X, this.opControl.rectOut.Location.Y + this.opControl.Location.Y),
                    new Point(mrc.rectIn.Location.X + mrc.Location.X,mrc.rectIn.Location.Y + mrc.Location.Y), 
                    0);
                Global.GetCurrentDocument().BindLineToControl(line, this.opControl, mrc);
                string path = BCPBuffer.GetInstance().CreateNewBCPFile(tmpName,this.columnName);
                mrc.Path = path;
            }
            Global.GetOptionDao().CreateResultControl(this.opControl, this.columnName);

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
            Dictionary<string, string> dataInfo = Global.GetOptionDao().GetInputDataInfo(this.opControl.ID);
            if (dataInfo.ContainsKey("dataPath") && dataInfo.ContainsKey("encoding"))
            {
                this.DataInfoBox.Text = Path.GetFileNameWithoutExtension(dataInfo["dataPath"]);
                this.dataPath = dataInfo["dataPath"];
                SetOption(this.dataPath, this.DataInfoBox.Text, dataInfo["encoding"]);
            }
        }

        private void SetOption(string path, string dataName, string encoding)
        {
 
            BcpInfo bcpInfo = new BcpInfo(path, dataName, ElementType.Null, EnType(encoding));
            string column = bcpInfo.columnLine;
            this.columnName = column.Split('\t');
            foreach (string name in this.columnName)
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
