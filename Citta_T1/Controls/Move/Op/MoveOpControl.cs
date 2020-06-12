using Citta_T1.Business.Model;
using Citta_T1.Business.Option;
using Citta_T1.Business.Schedule;
using Citta_T1.Controls.Interface;
using Citta_T1.Core;
using Citta_T1.Core.UndoRedo;
using Citta_T1.Core.UndoRedo.Command;
using Citta_T1.OperatorViews;
using Citta_T1.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Citta_T1.Controls.Move.Op
{

    public partial class MoveOpControl : MoveBaseControl, IMoveControl
    {
        private static LogUtil log = LogUtil.GetInstance("MoveOpControl");

        private MoveWrapper moveWrapper;
        private static string doublePin = "关联算子 取差集 碰撞算子 取并集 多源算子 关键词过滤";


        private bool doublelPinFlag = false;

        public string SubTypeName { get; }
        public OperatorOption Option { get; set; }
        public override ElementStatus Status
        {
            get => base.Status;
            set
            {
                OptionDirty(value);
                base.Status = value;
            }
        }
        public bool EnableOption { get => OptionMenuItem.Enabled; set => OptionMenuItem.Enabled = value; }
        public int RevisedPinIndex { get; set; }

        public string[] FirstDataSourceColumns { get; set; }  //第一个入度的表头配置
        public string[] SecondDataSourceColumns { get; set; } //第二个入度的表头配置

        private List<Rectangle> leftPinArray = new List<Rectangle> { };

        // 以该控件为终点的所有点
        private List<int> endLineIndexs = new List<int>() { };

        private ECommandType cmd = ECommandType.Null;

        // 绘制引脚

        private Point leftPin = new Point(2, 10);
        private Point rightPin = new Point(140, 10);
        private Pen pen = new Pen(Color.DarkGray, 1f);
        private SolidBrush whiteSmkeBrush = new SolidBrush(Color.WhiteSmoke);
        private Rectangle rectIn_down;
        private Rectangle rectIn_up;
        private string pinStatus = "noEnter";
        private string rectArea = "rectIn_down rectIn_up rectOut";
        private List<int> linePinArray = new List<int> { };

        public MoveOpControl(int sizeL, string description, string subTypeName, Point loc)
        {
            InitializeComponent();
            InitializeContextMenuStrip();

            Type = ElementType.Operator;
            Description = description;
            Location = loc;
            FullFilePath = String.Empty;
            Encoding = OpUtil.Encoding.NoNeed;
            Separator = OpUtil.DefaultSeparator;

            SubTypeName = subTypeName;
            Option = new OperatorOption();

            doublelPinFlag = doublePin.Contains(SubTypeName);
            this.moveWrapper = new MoveWrapper();

            changeStatus = new Size(0, 29);
            normalStatus = new Size(72, 29);

            InitializeOpPinPicture();
            InitializeHelpInfoAndOpIcon();
            ChangeSize(sizeL);

            FirstDataSourceColumns = new string[0];
            SecondDataSourceColumns = new string[0];


        }

        // 算子维度, 目前就2元和1元算子两种
        public int OperatorDimension()
        {
            return doublelPinFlag ? 2 : 1;
        }

        public bool IsBinaryDimension()
        {
            return OperatorDimension() == 2;
        }

        public bool IsSingleDimension()
        {
            return OperatorDimension() == 1;
        }

        private void InitializeOpPinPicture()
        {
            int dy = doublelPinFlag ? 5 : 0;
            rectIn_up = new Rectangle(this.leftPin.X, this.leftPin.Y - dy, this.pinWidth, this.pinHeight);
            this.leftPinArray.Add(rectIn_up);
            this.endLineIndexs.Add(-1);

            rectIn_down = new Rectangle(this.leftPin.X, this.leftPin.Y + dy, this.pinWidth, this.pinHeight);
            this.leftPinArray.Add(rectIn_down);
            this.endLineIndexs.Add(-1);

            rectOut = new Rectangle(this.rightPin.X, this.rightPin.Y, this.pinWidth, this.pinHeight);
            SetOpControlName(this.textBox.Text);
        }
        private void InitializeContextMenuStrip()
        {
            this.contextMenuStrip.Items.AddRange(new ToolStripItem[] {
            this.OptionMenuItem,
            this.RenameMenuItem,
            this.RemarkMenuItem,
            this.RunMenuItem,
            this.ErrorLogMenuItem,
            this.DeleteMenuItem });
        }
        private void InitializeHelpInfoAndOpIcon()
        {
            switch (SubTypeName)
            {
                case "关联算子":
                    this.helpToolTip.SetToolTip(this.rightPictureBox, HelpUtil.RelateOperatorHelpInfo);
                    this.leftPictureBox.Image = global::Citta_T1.Properties.Resources.releateOp;
                    break;
                case "碰撞算子":
                    this.helpToolTip.SetToolTip(this.rightPictureBox, HelpUtil.CollideOperatorHelpInfo);
                    this.leftPictureBox.Image = global::Citta_T1.Properties.Resources.collideOp;
                    break;
                case "取并集":
                    this.helpToolTip.SetToolTip(this.rightPictureBox, HelpUtil.UnionOperatorHelpInfo);
                    this.leftPictureBox.Image = global::Citta_T1.Properties.Resources.unionOp;
                    break;
                case "取差集":
                    this.helpToolTip.SetToolTip(this.rightPictureBox, HelpUtil.DifferOperatorHelpInfo);
                    this.leftPictureBox.Image = global::Citta_T1.Properties.Resources.differOp;
                    break;
                case "随机采样":
                    this.helpToolTip.SetToolTip(this.rightPictureBox, HelpUtil.RandomOperatorHelpInfo);
                    this.leftPictureBox.Image = global::Citta_T1.Properties.Resources.randomOp;
                    break;
                case "条件筛选":
                    this.helpToolTip.SetToolTip(this.rightPictureBox, HelpUtil.FilterOperatorHelpInfo);
                    this.leftPictureBox.Image = global::Citta_T1.Properties.Resources.filterOp;
                    break;
                case "取最大值":
                    this.helpToolTip.SetToolTip(this.rightPictureBox, HelpUtil.MaxOperatorHelpInfo);
                    this.leftPictureBox.Image = global::Citta_T1.Properties.Resources.maxOp;
                    break;
                case "取最小值":
                    this.helpToolTip.SetToolTip(this.rightPictureBox, HelpUtil.MinOperatorHelpInfo);
                    this.leftPictureBox.Image = global::Citta_T1.Properties.Resources.minOp;
                    break;
                case "取平均值":
                    this.helpToolTip.SetToolTip(this.rightPictureBox, HelpUtil.AvgOperatorHelpInfo);
                    this.leftPictureBox.Image = global::Citta_T1.Properties.Resources.avgOp;
                    break;
                case "频率算子":
                    this.helpToolTip.SetToolTip(this.rightPictureBox, HelpUtil.FreqOperatorHelpInfo);
                    this.leftPictureBox.Image = global::Citta_T1.Properties.Resources.freqOp;
                    break;
                case "排序算子":
                    this.helpToolTip.SetToolTip(this.rightPictureBox, HelpUtil.SortOperatorHelpInfo);
                    this.leftPictureBox.Image = global::Citta_T1.Properties.Resources.sortOp;
                    break;
                case "分组算子":
                    this.helpToolTip.SetToolTip(this.rightPictureBox, HelpUtil.GroupOperatorHelpInfo);
                    this.leftPictureBox.Image = global::Citta_T1.Properties.Resources.groupOp;
                    break;
                case "AI实践":
                    this.helpToolTip.SetToolTip(this.rightPictureBox, HelpUtil.CustomOperator1HelpInfo);
                    break;
                case "Python算子":
                    this.helpToolTip.SetToolTip(this.rightPictureBox, HelpUtil.PythonOperatorHelpInfo);
                    break;
                case "多源算子":
                    this.helpToolTip.SetToolTip(this.rightPictureBox, HelpUtil.CustomOperator2HelpInfo);
                    break;
                case "关键词过滤":
                    this.helpToolTip.SetToolTip(this.rightPictureBox, HelpUtil.KeyWordOperatorHelpInfo);
                    this.leftPictureBox.Image = global::Citta_T1.Properties.Resources.wordFilterOp;
                    break;
                case "数据标准化":
                    this.helpToolTip.SetToolTip(this.rightPictureBox, HelpUtil.DataFormatOperatorHelpInfo);
                    this.leftPictureBox.Image = global::Citta_T1.Properties.Resources.dataStandarOp;
                    break;
                default:
                    break;
            }

        }

        #region MOC的事件
        private void MoveOpControl_MouseMove(object sender, MouseEventArgs e)
        {
            bool isNeedMoveLine = false;
            if (Global.GetFlowControl().SelectDrag || (Global.GetFlowControl().SelectFrame && !Global.GetCanvasPanel().DelEnable))
                return;
            PinOpLeaveAndEnter(this.PointToClient(MousePosition));

            if (cmd == ECommandType.Null)
                return;
            else if (cmd == ECommandType.Hold)
            {
                #region 控件移动
                int left = this.Left + e.X - mouseOffset.X;
                int top = this.Top + e.Y - mouseOffset.Y;
                Global.GetCurrentDocument().WorldMap
                      .WorldBoundControl(new Point(left, top), this);
                #endregion
                foreach (ModelRelation mr in Global.GetCurrentDocument().ModelRelations)
                {
                    if (mr.StartID == this.ID)
                    {
                        mr.StartP = this.GetStartPinLoc(0);
                        mr.UpdatePoints();
                        isNeedMoveLine = true;
                    }
                    if (mr.EndID == this.ID)
                    {
                        mr.EndP = this.GetEndPinLoc(mr.EndPin);
                        mr.UpdatePoints();
                        isNeedMoveLine = true;
                    }
                }
                if (isNeedMoveLine)
                    this.moveWrapper.DragMove(this.Size, Global.GetCanvasPanel().ScreenFactor, e);
            }
        }


        private void MoveOpControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (Global.GetFlowControl().SelectDrag || Global.GetFlowControl().SelectFrame)
                return;

            if (e.Button == MouseButtons.Left)
            {
                if (rectOut.Contains(e.Location))
                {
                    int startX = this.Location.X + e.X;
                    int startY = this.Location.Y + e.Y;
                    oldControlPosition = this.Location;
                    MouseEventArgs e1 = new MouseEventArgs(e.Button, e.Clicks, startX, startY, 0);
                    cmd = ECommandType.PinDraw;
                    Global.GetCanvasPanel().CanvasPanel_MouseDown(this, e1);
                    return;
                }
                mouseOffset.X = e.X;
                mouseOffset.Y = e.Y;
                cmd = ECommandType.Hold;
            }
            this.moveWrapper.DragDown(this.Size, Global.GetCanvasPanel().ScreenFactor, e);
            oldControlPosition = this.Location;
        }

        private void TxtButton_MouseDown(object sender, MouseEventArgs e)
        {
            if (Global.GetFlowControl().SelectDrag || Global.GetFlowControl().SelectFrame)
                return;
            // 单击鼠标, 移动控件
            if (e.Clicks == 1)
                MoveOpControl_MouseDown(sender, e);
            // 双击鼠标, 改名字
            if (e.Clicks == 2)
            {
                RenameMenuItem_Click(this, e);
            }
        }

        private void StatusBox_MouseDown(object sender, MouseEventArgs e)
        {   // 只处理左键点击
            if (e.Button != MouseButtons.Left)
                return;
            // 单击视为移动,按父控件鼠标点击处理
            if (e.Clicks == 1)
            {
                base.OnMouseDown(e);
            }// 双击,弹出配置窗口
            else if (e.Clicks == 2)
            {
                // 清空焦点
                Global.GetMainForm().BlankButtonFocus();
                // 显示配置
                if (this.OptionMenuItem.Enabled)
                    ShowOptionDialog();
                else
                    MessageBox.Show("请先画线连接数据源, 然后才能配置算子参数",
                        "没有对应的数据源",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

            }
        }

        private void MoveOpControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (Global.GetFlowControl().SelectDrag || Global.GetFlowControl().SelectFrame)
                return;

            if (e.Button == MouseButtons.Left)
            {
                if (cmd == ECommandType.PinDraw)
                {
                    cmd = ECommandType.Null;
                    int startX = this.Location.X + e.X;
                    int startY = this.Location.Y + e.Y;
                    MouseEventArgs e1 = new MouseEventArgs(e.Button, e.Clicks, startX, startY, 0);
                    Global.GetCanvasPanel().CanvasPanel_MouseUp(this, e1);
                }
                cmd = ECommandType.Null;
                this.moveWrapper.DragUp(this.Size, Global.GetCanvasPanel().ScreenFactor, e);
                Global.GetNaviViewControl().UpdateNaviView();
            }

            if (oldControlPosition != this.Location)
            {
                // 构造移动命令类,压入undo栈
                ModelElement element = Global.GetCurrentDocument().SearchElementByID(ID);
                if (element != ModelElement.Empty)
                {
                    Point oldControlPostionInWorld = Global.GetCurrentDocument().WorldMap.ScreenToWorld(oldControlPosition, false);
                    ICommand moveCommand = new ElementMoveCommand(element, oldControlPostionInWorld);
                    UndoRedoManager.GetInstance().PushCommand(Global.GetCurrentDocument(), moveCommand);
                }
                Global.GetMainForm().SetDocumentDirty();
            }
        }
        #endregion

        #region 控件名称长短改变时改变控件大小
        protected override void ResizeControl(int txtWidth, Size controlSize)
        {
            double f = Math.Pow(factor, sizeLevel);
            int pading = 4;

            if (f != 1)
                pading += 1;
            this.Size = new Size((int)(controlSize.Width * f), (int)(controlSize.Height * f));
            this.rightPictureBox.Location = new Point(this.Width - (int)(25 * f), (int)(7 * f));
            this.statusBox.Location = new Point(this.Width - (int)(42 * f), (int)(7 * f));
            this.rectOut.Location = new Point(this.Width - (int)(10 * f), (int)(11 * f));
            this.txtButton.Size = new Size((int)(txtWidth * f), this.Height - (int)(pading * f));
            this.textBox.Size = new Size((int)((txtWidth - 1) * f), this.Height - (int)(4 * f));

            DrawRoundRect((int)(4 * f), 0, this.Width - (int)(11 * f), this.Height - (int)(2 * f), (int)(3 * f));
        }
        #endregion

        #region 右键菜单
        public void OptionMenuItem_Click(object sender, EventArgs e)
        {
            ShowOptionDialog();
        }

        private void ShowOptionDialog()
        {
            if (!this.OptionMenuItem.Enabled)
            {
                MessageBox.Show("该算子没有对应的数据源，暂时还无法配置，请先连接数据，再进行算子设置。");
                return;
            }
            switch (SubTypeName)
            {
                case "关联算子":
                    new RelateOperatorView(this).ShowDialog();
                    break;
                case "碰撞算子":
                    new CollideOperatorView(this).ShowDialog();
                    break;
                case "取并集":
                    new UnionOperatorView(this).ShowDialog();
                    break;
                case "取差集":
                    new DifferOperatorView(this).ShowDialog();
                    break;
                case "随机采样":
                    new RandomOperatorView(this).ShowDialog();
                    break;
                case "条件筛选":
                    new FilterOperatorView(this).ShowDialog();
                    break;
                case "取最大值":
                    new MaxOperatorView(this).ShowDialog();
                    break;
                case "取最小值":
                    new MinOperatorView(this).ShowDialog();
                    break;
                case "取平均值":
                    new AvgOperatorView(this).ShowDialog();
                    break;
                case "频率算子":
                    new FreqOperatorView(this).ShowDialog();
                    break;
                case "排序算子":
                    new SortOperatorView(this).ShowDialog();
                    break;
                case "分组算子":
                    new GroupOperatorView(this).ShowDialog();
                    break;
                case "AI实践":
                    new CustomOperatorView(this).ShowDialog();
                    break;
                case "多源算子":
                    new CustomOperatorView(this).ShowDialog();
                    break;
                case "Python算子":
                    new PythonOperatorView(this).ShowDialog();
                    break;
                case "关键词过滤":
                    new KeywordOperatorView(this).ShowDialog();
                    break;
                case "数据标准化":
                    new DataFormatOperatorView(this).ShowDialog();
                    break;
                default:
                    break;
            }
        }


        public void RunMenuItem_Click(object sender, EventArgs e)
        {
            //运行到此
            //判断该算子是否配置完成
            ModelElement currentOp = Global.GetCurrentDocument().SearchElementByID(this.ID);
            if (currentOp.Status == ElementStatus.Null)
            {
                MessageBox.Show("该算子未配置，请配置后再运行", "未配置", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //判断模型是否保存
            if (Global.GetCurrentDocument().Dirty)
            {
                MessageBox.Show("当前模型没有保存，请保存后再运行模型", "保存", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //需要判断模型当前运行状态，正在运行时，无法执行运行到此
            TaskManager currentManager = Global.GetCurrentDocument().TaskManager;
            currentManager.GetCurrentModelTripleList(Global.GetCurrentDocument(), "mid", currentOp);
            Global.GetMainForm().BindUiManagerFunc();
            if (currentManager.IsAllOperatorDone())
            {
                MessageBox.Show("运行到此路径上的算子均已运算完毕，重新运算需要先点击‘重置’按钮。", "运算完毕", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            currentManager.Start();
            Global.GetMainForm().UpdateRunbuttonImageInfo();
        }

        private void PushUndoStackWhenDel()
        {
            /*
             * 保存自己的element
             * 保存所连接的关系，具体是[(startID, endID, endPin), (startID, endID, endPin)]
             * 保存所连接的结果控件[RsMe1, RsMe2,...]
             */
             //ICommand Ele

        }

        public void DeleteMenuItem_Click(object sender, EventArgs e)
        {
            if (Global.GetFlowControl().SelectDrag || (Global.GetFlowControl().SelectFrame && !Global.GetCanvasPanel().DelEnable))
                return;

            this.DeleteMyself();
        }
        private void DeleteMyself()
        {
            CanvasPanel cp = Global.GetCanvasPanel();
            ModelDocument doc = Global.GetCurrentDocument();

            List<ModelRelation> modelRelations = new List<ModelRelation>(Global.GetCurrentDocument().ModelRelations);
            List<Tuple<int, int, int>> relations = new List<Tuple<int, int, int>>();
            ModelElement rsEles = null;
            Tuple<List<Tuple<int, int, int>>, ModelElement> relationAndRsEles = null;

            foreach (ModelRelation mr in modelRelations)
            {
                // TODO 删结果算子和任何与结果算子相连的关系 DT-Op-Rs-X del Op-Rs Rs-X Rs
                if (mr.StartID == this.ID)
                    relationAndRsEles = DeleteResultControl(mr.EndID, modelRelations);
                // 删关系
                if (mr.EndID == this.ID)
                {
                    cp.DeleteRelation(mr);
                    relations.Add(new Tuple<int, int, int>(mr.StartID, mr.EndID, mr.EndPin));
                }
            }
            if (relationAndRsEles != null)
            {
                foreach (var r in relationAndRsEles.Item1)
                    relations.Add(r);
                rsEles = relationAndRsEles.Item2;
            }
            
            cp.Invalidate();

            ModelElement me = doc.SearchElementByID(ID);
            ICommand cmd = new ElementDeleteCommand(me, relations, rsEles);
            UndoRedoManager.GetInstance().PushCommand(doc, cmd);

            //删除自身
            cp.DeleteEle(me);
        }
        public void UndoRedoDeleteElement(ModelElement me, List<Tuple<int, int, int>> relations, ModelElement rsEle)
        {
            //TODO undo,redo时关系处理
            /*
             * 1. 删自身
             * 2. 删与之相连的关系
             * 3. 删与之相连的结果控件
             * 4. 改变其他控件的Pin状态
             */
            CanvasPanel cp = Global.GetCanvasPanel();
            cp.DeleteEle(me);
            foreach (Tuple<int, int, int> rel in relations)
                cp.DeleteRelationByCtrID(rel.Item1, rel.Item2, rel.Item3);
            if (rsEle != null)
                cp.DeleteEle(rsEle);
        }
        public void UndoRedoAddElement(ModelElement me, List<Tuple<int, int, int>> relations, ModelElement rsEle)
        {
            //TODO undo,redo时关系处理
            /*
             * 1. 恢复自身
             * 2. 恢复与之相连的关系
             * 3. 恢复与之相连的结果控件
             * 4. 改变其他控件的Pin状态
             */
            CanvasPanel cp = Global.GetCanvasPanel();
            cp.AddEle(me);
            if (rsEle != null)
                cp.AddEle(rsEle);
            
            foreach (Tuple<int, int, int> rel in relations)
                cp.AddNewRelationByCtrID(rel.Item1, rel.Item2, rel.Item3);
        }
        
        private Tuple<List<Tuple<int, int, int>>, ModelElement> DeleteResultControl(int endID, List<ModelRelation> modelRelations)
        {
            // modelRelations = deepcopy(Global.GetCurrentDocument().modelRelations)
            CanvasPanel cp = Global.GetCanvasPanel();
            ModelDocument doc = Global.GetCurrentDocument();

            List<Tuple<int, int, int>> relations = new List<Tuple<int, int, int>>();
            ModelElement rsEles = null;

            // 改状态
            doc.StatusChangeWhenDeleteControl(endID);
            // 删除关系
            foreach (ModelRelation mr in modelRelations)
            {
                if (mr.StartID == endID || mr.EndID == endID)
                {
                    cp.DeleteRelation(mr);
                    relations.Add(new Tuple<int, int, int>(mr.StartID, mr.EndID, mr.EndPin));
                }
            }
            cp.Invalidate();
            List<ModelElement> modelElements = new List<ModelElement>(Global.GetCurrentDocument().ModelElements);
            // 删除与之相连的结果算子
            foreach (ModelElement me in modelElements)
            {
                if (me.ID == endID)
                {
                    cp.DeleteEle(me);
                    rsEles = me;
                    break;
                }
            }
            return new Tuple<List<Tuple<int, int, int>>, ModelElement>(relations, rsEles);
        }
        private void OptionDirty(ElementStatus status)
        {
            if (status == ElementStatus.Null)
                this.statusBox.Image = Properties.Resources.set;
            else if (status == ElementStatus.Done)
                this.statusBox.Image = Properties.Resources.done;
            else if (status == ElementStatus.Ready)
                this.statusBox.Image = Properties.Resources.setSuccess;
            else if (status == ElementStatus.Warn)
                this.statusBox.Image = Properties.Resources.warn;

        }
        #endregion

        #region 针脚事件
        private void PinOpLeaveAndEnter(Point mousePosition)
        {
            if (rectIn_up.Contains(mousePosition))
            {
                if (rectArea.Contains(pinStatus) || linePinArray.Contains(0)) return;
                rectIn_up = RectEnter(rectIn_up);

                this.Invalidate();
                pinStatus = "rectIn_up";
            }

            else if (rectIn_down.Contains(mousePosition))
            {
                if (rectArea.Contains(pinStatus) || linePinArray.Contains(1)) return;
                rectIn_down = RectEnter(rectIn_down);
                this.Invalidate();
                pinStatus = "rectIn_down";

            }
            else if (rectOut.Contains(mousePosition))
            {
                if (rectArea.Contains(pinStatus) || linePinArray.Contains(-1)) return;
                rectOut = RectEnter(rectOut);
                this.Invalidate();
                pinStatus = "rectOut";
            }

            else if (pinStatus != "noEnter")
            {
                switch (pinStatus)
                {
                    case "rectIn_down":
                        if (!linePinArray.Contains(1))
                            rectIn_down = RectLeave(rectIn_down);
                        break;
                    case "rectIn_up":
                        if (!linePinArray.Contains(0))
                            rectIn_up = RectLeave(rectIn_up);
                        break;
                    case "rectOut":
                        if (!linePinArray.Contains(-1))
                            rectOut = RectLeave(rectOut);
                        break;
                }
                pinStatus = "noEnter";
                this.Invalidate();
            }
        }

        public void OutPinInit(String status)
        {
            if ((pinStatus != "rectOut") && (status == "lineExit") && (!linePinArray.Contains(-1)))
            {
                rectOut = RectEnter(rectOut);
                linePinArray.Add(-1);
                this.Invalidate();
            }
            PinOpLeaveAndEnter(new Point(0, 0));
        }

        public void InPinInit(int pinIndex)
        {
            linePinArray.Remove(pinIndex);
            if ((pinIndex == 1) && (pinStatus != "rectIn_down") && (!linePinArray.Contains(1)))
            {
                rectIn_down = RectLeave(rectIn_down);
            }
            if ((pinIndex == 0) && (pinStatus != "rectIn_up") && (!linePinArray.Contains(0)))
            {
                rectIn_up = RectLeave(rectIn_up);
            }
            this.Invalidate();
        }

        public Rectangle RectEnter(Rectangle rect)
        {
            Point oriLtCorner = rect.Location;
            Size oriSize = rect.Size;
            Point oriCenter = new Point(oriLtCorner.X + oriSize.Width / 2, oriLtCorner.Y + oriSize.Height / 2);
            Point dstLtCorner = new Point(oriCenter.X - oriSize.Width / 2 - 1, oriCenter.Y - oriSize.Height / 2 - 1);
            Size dstSize = new Size(oriSize.Width + 2, oriSize.Height + 2);
            return new Rectangle(dstLtCorner, dstSize);
        }
        public Rectangle RectLeave(Rectangle rect)
        {
            Point oriLtCorner = rect.Location;
            Size oriSize = rect.Size;
            Point oriCenter = new Point(oriLtCorner.X + oriSize.Width / 2, oriLtCorner.Y + oriSize.Height / 2);
            Point dstLtCorner = new Point(oriCenter.X - oriSize.Width / 2 + 1, oriCenter.Y - oriSize.Height / 2 + 1);
            Size dstSize = new Size(oriSize.Width - 2, oriSize.Height - 2);
            return new Rectangle(dstLtCorner, dstSize);
        }
        #endregion

        #region 托块的放大与缩小
        protected override void ChangeSize(bool zoomUp, float factor = Global.Factor)
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true); // 双缓冲DoubleBuffer
            ExtensionMethods.SetDouble(this);
            double f = Math.Pow(factor, sizeLevel);
            DrawRoundRect((int)(4 * f), 0, this.Width - (int)(11 * f), this.Height - (int)(2 * f), (int)(3 * f));

            factor = zoomUp ? factor : 1 / factor;

            SetControlsBySize(factor, this);
            this.rectOut = SetRectBySize(factor, this.rectOut);
            this.rectIn_down = SetRectBySize(factor, this.rectIn_down);
            this.rectIn_up = SetRectBySize(factor, this.rectIn_up);
            this.Invalidate();
        }
        #endregion

        #region IMoveControl 接口实现方法
        public override PointF RevisePointLoc(PointF p)
        {
            /*
             * 1. 遍历当前Document上所有LeftPin，检查该点是否在LeftPin的附近
             * 2. 如果在，对该点就行修正
             */
            // 鼠标判定矩形大小
            int mouseR = 15;
            bool isRevised = false;
            float maxIntersectPerct = 0.0F;
            PointF revisedP = new PointF(p.X, p.Y);
            Rectangle rect = new Rectangle(
                   new Point((int)p.X - mouseR / 2, (int)p.Y - mouseR / 2),
                   new Size(mouseR, mouseR));
            CanvasPanel canvas = Global.GetCanvasPanel();

            foreach (Rectangle _leftPinRect in leftPinArray)
            {
                int sizeLevel = Global.GetCurrentDocument().WorldMap.SizeLevel;
                double multiper = Math.Pow(Global.Factor, sizeLevel);
                Rectangle leftPinRect = new Rectangle(
                    new Point(
                        this.Location.X + (int)(_leftPinRect.Location.X * multiper),
                        this.Location.Y + (int)(_leftPinRect.Location.Y * multiper)
                        ),
                    new Size(
                        (int)(_leftPinRect.Width * multiper),
                        (int)(_leftPinRect.Height * multiper)
                        )
                    );

                int pinLeftX = leftPinRect.X;
                int pinTopY = leftPinRect.Y;

                if (leftPinRect.IntersectsWith(rect))
                {
                    // 计算相交面积比
                    float iou = OpUtil.IOU(rect, leftPinRect);
                    if (iou > maxIntersectPerct)
                    {
                        maxIntersectPerct = iou;
                        revisedP = new PointF(
                            pinLeftX + leftPinRect.Width / 2,
                            pinTopY + leftPinRect.Height / 2);
                        // 绑定控件
                        canvas.EndC = this;
                        isRevised = true;
                        this.RevisedPinIndex = leftPinArray.IndexOf(_leftPinRect);
                    }
                }
            }
            if (!isRevised)
                canvas.EndC = null;
            return revisedP;
        }

        public override PointF GetEndPinLoc(int pinIndex)
        {
            switch (pinIndex)
            {
                case 0:
                    return new PointF(
                        this.Location.X + this.rectIn_up.Location.X + this.rectIn_up.Width / 2,
                        this.Location.Y + this.rectIn_up.Location.Y + this.rectIn_up.Height / 2);
                case 1:
                    return new PointF(
                        this.Location.X + this.rectIn_down.Location.X + this.rectIn_down.Width / 2,
                        this.Location.Y + this.rectIn_down.Location.Y + this.rectIn_down.Height / 2);
                default:
                    // TODO [DK] 需要定义一个异常
                    return new PointF(0, 0);
            }
        }


        public void RectInAdd(int pinIndex)
        {
            if ((pinIndex == 1) && (pinStatus != "rectIn_down") && (!linePinArray.Contains(1)))
            {

                rectIn_down = RectEnter(rectIn_down);
                linePinArray.Add(pinIndex);
            }
            if ((pinIndex == 0) && (pinStatus != "rectIn_up") && (!linePinArray.Contains(0)))
            {

                rectIn_up = RectEnter(rectIn_up);
                linePinArray.Add(pinIndex);
            }
            this.Invalidate();
            PinOpLeaveAndEnter(new Point(0, 0));
        }
        #endregion

        public void SetStatusBoxErrorContent(string error)
        {
            this.helpToolTip.SetToolTip(this.statusBox, error);
        }

        private void MoveOpControl_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;//去掉锯齿
            e.Graphics.CompositingQuality = CompositingQuality.HighQuality;//合成图像的质量
            e.Graphics.FillEllipse(whiteSmkeBrush, rectIn_down);
            e.Graphics.DrawEllipse(pen, rectIn_down);
            e.Graphics.FillEllipse(whiteSmkeBrush, rectIn_up);
            e.Graphics.DrawEllipse(pen, rectIn_up);
            e.Graphics.FillEllipse(whiteSmkeBrush, rectOut);
            e.Graphics.DrawEllipse(pen, rectOut);
        }

        public void ControlSelect()
        {
            double f = Math.Pow(factor, sizeLevel);
            pen = new Pen(Color.DarkGray, 1.5f);
            DrawRoundRect((int)(4 * f), 0, this.Width - (int)(11 * f), this.Height - (int)(2 * f), (int)(3 * f));
            UpdateRound((int)(4 * f), 0, this.Width - (int)(11 * f), this.Height - (int)(2 * f), (int)(3 * f));
        }
        public void ControlNoSelect()
        {
            pen = new Pen(Color.DarkGray, 1f);
            double f = Math.Pow(factor, sizeLevel);
            DrawRoundRect((int)(4 * f), 0, this.Width - (int)(11 * f), this.Height - (int)(2 * f), (int)(3 * f));
        }
    }
}