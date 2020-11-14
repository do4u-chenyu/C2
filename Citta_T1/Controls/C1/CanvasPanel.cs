using C2.Business.Model;
using C2.Business.Model.World;
using C2.Business.Schedule;
using C2.Controls.Flow;
using C2.Controls.Interface;
using C2.Controls.Move;
using C2.Controls.Move.Dt;
using C2.Controls.Move.Op;
using C2.Controls.Move.Rs;
using C2.Core;
using C2.Core.UndoRedo;
using C2.Core.UndoRedo.Command;
using C2.Model;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace C2.Controls
{
    public delegate void NewElementEventHandler(MoveBaseControl ct);

    public enum ECommandType
    {
        Null,
        Hold,
        PinDraw
    }
    public partial class CanvasPanel : UserControl
    {
        ModelDocument _Document;
        private const int drgOffsetX = 380;
        private const int drgOffsetY = 100;
        private static bool leftButtonDown = false;
        private static LogUtil log = LogUtil.GetInstance("CanvasPanel");
        public event NewElementEventHandler NewElementEvent;
        private Bitmap staticImage;
        //屏幕拖动涉及的变量
        private readonly DragWrapper dragWrapper;
        private FrameWrapper frameWrapper;
        private ECommandType cmd = ECommandType.Null;
        private PointF startP;
        private PointF endP;
        private MoveBaseControl startC;
        private MoveBaseControl endC;
        private Rectangle invalidateRectWhenMoving;
        private Bezier lineWhenMoving;
        private List<int> selectLineIndexs = new List<int> { };
        private bool delEnable = false;
        private ClipBoardWrapper clipBoard = new ClipBoardWrapper();
        public MoveBaseControl StartC { get => startC; set => startC = value; }
        public MoveBaseControl EndC { get => endC; set => endC = value; }
        internal FrameWrapper FrameWrapper { get => frameWrapper; set => frameWrapper = value; }
        public bool DelEnable { get => delEnable; set => delEnable = value; }
        public PointF StartP { get => startP; set => startP = value; }
        public PointF EndP { get => endP; set => endP = value; }

        internal DragWrapper DragWrapper => dragWrapper;

        public  bool LeftButtonDown { get => leftButtonDown; set => leftButtonDown = value; }

        public CanvasPanel()
        {
            dragWrapper = new DragWrapper(this);
            frameWrapper = new FrameWrapper(this);
            InitializeComponent();
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true); // 双缓冲DoubleBuffer
            SetStyle(ControlStyles.ResizeRedraw, true);
        }
        public ModelDocument Document
        {
            get { return _Document; }
            set
            {
                if (_Document != value)
                {
                    ModelDocument old = _Document;
                    _Document = value;
                }
            }
        }
        #region 右上角功能实现部分
        //画布右上角的放大与缩小功能实现
        public void ChangSize(bool isLarger, float factor = Global.Factor)
        {
            
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);//禁止擦除背景.
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);//双缓冲
            this.UpdateStyles();
            int sizeLevel = this.Document.WorldMap.SizeLevel;
            if (isLarger && sizeLevel <= 2)
            {
                sizeLevel += 1;

                this.Document.WorldMap.ScreenFactor *= factor;
                foreach (ModelElement me in this.Document.ModelElements)
                {
                    me.InnerControl.ChangeSize(sizeLevel);
                }
                foreach (ModelRelation mr in this.Document.ModelRelations)
                {
                    mr.ZoomIn();
                }
            }
            else if (!isLarger && sizeLevel > 0)
            {
                sizeLevel -= 1;

                this.Document.WorldMap.ScreenFactor /= factor;
                foreach (ModelElement me in this.Document.ModelElements)
                {
                    me.InnerControl.ChangeSize(sizeLevel);
                }
                foreach (ModelRelation mr in this.Document.ModelRelations)
                {
                    mr.ZoomOut();
                }
            }

            this.Document.WorldMap.SizeLevel = sizeLevel;
            Global.GetNaviViewControl().UpdateNaviView();
            
        }

        #endregion
        #region 各种事件
        public void CanvasPanel_DragDrop(object sender, DragEventArgs e)
        {
            ElementType type;
            try
            {
                type = (ElementType)e.Data.GetData("Type");
            }
            catch (Exception)
            {
                return;
            }
            // TODO C2不允许数据拖到Canvas
            //if (type == ElementType.DataSource)
            //    return;
            float screenFactor = Global.GetCurrentModelDocument().WorldMap.ScreenFactor;
            int locX = Convert.ToInt32(e.X / screenFactor);
            int locY = Convert.ToInt32(e.Y / screenFactor);
            int dx = Convert.ToInt32(drgOffsetX / screenFactor);
            int dy = Convert.ToInt32(drgOffsetY / screenFactor);
            Point location = Global.GetCanvasForm().PointToClient(new Point(locX - dx / 2 + 130, locY - dy / 2 + 10));

            string text = e.Data.GetData("Text").ToString();
            int sizeLevel = Global.GetCurrentModelDocument().WorldMap.SizeLevel;

            if (type == ElementType.Operator)
                AddNewOperator(sizeLevel, text, text, location);
            if (type == ElementType.DataSource)
            {
                string path = e.Data.GetData("Path").ToString();
                char separator = (char)e.Data.GetData("Separator");
                OpUtil.Encoding encoding = (OpUtil.Encoding)e.Data.GetData("Encoding");
                OpUtil.ExtType extType = (OpUtil.ExtType)e.Data.GetData("ExtType");
                AddNewDataSource(path, sizeLevel, text, location, separator, extType, encoding);
            }
        }

        public void CanvasPanel_MouseDown(object sender, MouseEventArgs e)
        {
            selectLineIndexs.Clear();
            // 强制编辑控件失去焦点,触发算子控件的Leave事件
            Global.GetCanvasForm()?.BlankButtonFocus();
            ModelStatus currentModelStatus = Global.GetCurrentModelDocument().TaskManager.ModelStatus;
            if (!(sender is MoveBaseControl) && currentModelStatus != ModelStatus.Running && currentModelStatus != ModelStatus.Pause)
                this.ClickOnLine(e);
            if (e.Button == MouseButtons.Right && !leftButtonDown)
            {
                Point pw = this.Document.WorldMap.ScreenToWorld(e.Location, false);
                if (frameWrapper.MinBoundingBox.Contains(pw))
                {
                    this.DelSelectControl.Show(this, e.Location);
                    return;
                }
                
                Global.GetTopToolBarControl().ResetStatus();
                frameWrapper.MinBoundingBox = new Rectangle(0, 0, 0, 0);// 点击右键, 清空操作状态,进入到正常编辑状态 
                this.Invalidate();
            }
            else if (e.Button == MouseButtons.Left)
            {
                leftButtonDown = true;
                if (sender is MoveDtControl || sender is MoveRsControl)
                    this.MouseDownWhenPinDraw(sender, e);
                else if (SelectFrame())
                    frameWrapper.FrameWrapper_MouseDown(e);
                else if (SelectDrag())
                    dragWrapper.DragDown(this.Size, this.Document.WorldMap.ScreenFactor, e);
            }

        }
        private void MouseDownWhenPinDraw(object sender, MouseEventArgs e)
        {
            this.cmd = ECommandType.PinDraw;
            this.StartC = sender as MoveBaseControl;
            this.StartP = new PointF(e.X, e.Y);
            if (this.staticImage != null)
            {
                this.staticImage.Dispose();
                this.staticImage = null;
            }
            this.staticImage = new Bitmap(this.Width, this.Height);
            Graphics g = Graphics.FromImage(this.staticImage);
            g.Clear(this.BackColor);
            this.RepaintStatic(g);
            g.Dispose();
        }
        public void CanvasPanel_MouseMove(object sender, MouseEventArgs e)
        {
            // 别删这句话，删了就出问题
            this.endC = null;
            // 画框涉及存在鼠标ENTENT事件，会自行判断鼠标左键，需要前提
            if (SelectFrame())
                frameWrapper.FrameWrapper_MouseMove(e);
            else if (e.Button != MouseButtons.Left) 
                return;
            // 控件移动
            else if (SelectDrag())
                dragWrapper.DragMove(this.Size, this.Document.WorldMap.ScreenFactor, e);
            // 绘制
            else if (cmd == ECommandType.PinDraw)
                this.MouseMoveWhenPinDraw(e);
        }
        private void MouseMoveWhenPinDraw(MouseEventArgs e)
        {
            // 吸附效果实现
            /*
             * 1. 遍历当前Document上所有LeftPin，检查该点是否在LeftPin的附近
             * 2. 如果在，对该点就行修正
             * 3. 
             */
            PointF nowP = e.Location;
            if (this.lineWhenMoving != null)
                invalidateRectWhenMoving = LineUtil.ConvertRect(this.lineWhenMoving.GetBoundingRect());
            else
                invalidateRectWhenMoving = new Rectangle();
            // 遍历所有OpControl的leftPin
            // 是否在一轮循环中被多次修正？=> 只要控件不堆叠在一起，就不会出现被多个控件修正的情况
            // 只要在循环中被修正一次，就退出，防止被多个控件修正坐标
            // 遍历一遍之后如果没有被校正，则this.endC=null
            foreach (ModelElement modelEle in this.Document.ModelElements)
            {
                MoveBaseControl con = modelEle.InnerControl;
                if (modelEle.Type == ElementType.Operator && con != startC)
                {
                    // 修正坐标
                    nowP = con.RevisePointLoc(nowP);
                    // 完成一次矫正
                    if (this.endC != null)
                        break;
                }
            }
            EndP = nowP;
            this.lineWhenMoving = new Bezier(StartP, nowP);
            // 不应该挡住其他的线
            CoverPanelByRect(invalidateRectWhenMoving);
            this.lineWhenMoving.OnMouseMove(nowP);
            // 重绘曲线
            RepaintObject(this.lineWhenMoving);
        }
        public void CanvasPanel_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;
            // 画框处理
            
            if (SelectFrame())
                frameWrapper.FrameWrapper_MouseUp(e);
            // 拖拽处理
            else if (SelectDrag())
                dragWrapper.DragUp(this.Size, this.Document.WorldMap.ScreenFactor, e);
            // 非画线落点处理
            else if (cmd == ECommandType.PinDraw)
            {
                this.MouseUpWhenPinDraw(sender, e);
                Global.GetMainForm().SetDocumentDirty();
            }
            leftButtonDown = false;
        }
        private void MouseUpWhenPinDraw(object sender, MouseEventArgs e)
        {
            cmd = ECommandType.Null;
            lineWhenMoving = null;

            /* 不是所有位置Up都能形成曲线的
             * 如果没有endC，或者endC不是OpControl，那就不形成线，结束绘线动作
             */
            if (CanNotPinDraw())
            {
                this.RepaintAllRelations();
                this.RepaintStartcPin(startC, startC.ID);
                return;
            }

            // 画线落点处理
            /* 
             * 在Canvas_MouseMove的时候，对鼠标的终点进行
             * 只保存线索引
             *         __________
             * endP1  | MControl | startP
             * endP2  |          |
             *         ----------
             */
            int endPinIndex = (endC as MoveOpControl).RevisedPinIndex;
            this.AddNewRelationByCtr(startC, endC, endPinIndex);
            this.RepaintStartcPin(startC, startC.ID);
        }

        private bool IsValidModelRelation(ModelRelation mr)
        {
            // 合法的线有 Dt-Op Rs-Op
            ModelDocument md = this.Document;
            ModelElement sMe = md.SearchElementByID(mr.StartID);
            ModelElement eMe = md.SearchElementByID(mr.EndID);

            return ((sMe.Type == ElementType.DataSource || sMe.Type == ElementType.Result) && eMe.Type == ElementType.Operator);
        }
        private void ClickOnLine(MouseEventArgs e)
        {
            /*
             * 点击规则见0511
             */
            List<ModelRelation> mrs = this.Document.ModelRelations;
            int mrIndex = PointToLine(new PointF(e.X, e.Y));

            if (mrIndex == -1)
            {
                //if (e.Button == MouseButtons.Right)
                this.ClearAllLineStatus();
            }
            else
            {
                if (mrIndex < mrs.Count && !this.SelectDrag() && !this.SelectFrame())
                    if (IsValidModelRelation(mrs[mrIndex]))
                        selectLineIndexs.Add(mrIndex);
                    else
                    {
                        HelpUtil.ShowMessageBox("算子与结果之间的连线是系统自动维护的，不能被人为选中、添加或删除。");
                        return;
                    }
                else
                    return;
                if (e.Button == MouseButtons.Left)
                {
                    if (!this.SelectDrag() && !this.SelectFrame())
                    {
                        this.SetAllLineStatus(this.selectLineIndexs, false);       // 如果此时已有线被选中，点击另一根线时，将该线置为选中状态，其他被选中的线置为未选中状态
                        this.Invalidate(false);
                    }
                }
                else if (e.Button == MouseButtons.Right)
                {
                    if (mrIndex < mrs.Count && mrs[mrIndex].Selected)
                        this.ShowDelectOption(e.Location);                          // 鼠标右键点击在当前选择的线上，弹出菜单，就一个删除选项，选中删除。
                }
            }
        }

        private void ShowDelectOption(Point p)
        {
            this.contextMenuStrip1.Show(this, p);
        }

        public void SetAllLineStatus(List<int> exceptLineIndex = null, bool isInvalidate = false)
        {
            /* 点击线的时候设置线的状态
             * 1. 当前线（exceptLineIndex）状态取反
             * 2. 其他线状态置为false
             */
            List<ModelRelation> mrs = this.Document.ModelRelations;
            for (int i = 0; i < mrs.Count; i++)
            {
                ModelRelation mr = mrs[i];
                if (exceptLineIndex != null && exceptLineIndex.Contains(i))
                    mr.Selected = !mr.Selected;
                else
                    mr.Selected = false;
            }
            if (isInvalidate)
                this.Invalidate(false);
        }

        public void ClearAllLineStatus()
        {
            this.SetAllLineStatus(null, true);
            if (this.selectLineIndexs != null)
                this.selectLineIndexs.Clear();
        }

        private void DeleteLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.DeleteSelectedLinesByIndex();

        }

        public void DeleteSelectedLinesByIndex()
        {
            List<ModelRelation> mrs = this.Document.ModelRelations;
            ModelDocument doc = this.Document;
            MoveBaseControl startC;
            MoveBaseControl endC;
            foreach (int i in selectLineIndexs)
            {
                ModelRelation mr = mrs[i];
                this.DeleteRelation(mr);

                startC = doc.SearchElementByID(mr.StartID).InnerControl;
                endC = doc.SearchElementByID(mr.EndID).InnerControl;
                if (startC != null && endC != null)
                {
                    BaseCommand delRelationCommand = new RelationDeleteCommand(startC.ID, endC.ID, mr.EndPin);
                    UndoRedoManager.GetInstance().PushCommand(this.Document, delRelationCommand);
                }
            }
            selectLineIndexs.Clear();
            this.Invalidate(false);

        }
        public void DeleteRelationByCtrID(int startID, int endID, int pinIndex)
        {
            ModelRelation mr = this.Document.ModelRelations.Find(t => t.StartID == startID && t.EndID == endID && t.EndPin == pinIndex);
            if (mr != null)
                this.DeleteRelation(mr);
        }
        public void DeleteRelation(ModelRelation mr)
        {
            try
            {
                //删除线配置逻辑
                ModelDocument doc = this.Document;
                doc.StatusChangeWhenDeleteLine(mr.EndID); // 这里会改变算子状态
                doc.RemoveModelRelation(mr);
                //关联算子引脚自适应改变
                MoveBaseControl lineStartC = doc.SearchElementByID(mr.StartID).InnerControl;
                this.RepaintStartcPin(lineStartC, mr.StartID);
                MoveBaseControl lineEndC = doc.SearchElementByID(mr.EndID).InnerControl;
                if (lineEndC != null)
                    (lineEndC as IMoveControl).InPinInit(mr.EndPin);
                //删除线文档dirty
                Global.GetMainForm().SetDocumentDirty();
            }
            catch (Exception e)
            {
                log.Error("CanvasPanel删除线时发生错误:" + e);
            }
            finally
            {
                Global.GetNaviViewControl().UpdateNaviView();
            }
        }
        public void AddNewRelationByCtrID(int startCID, int endCID, int pinIndex, bool isPushCmd=false)
        {
            ModelDocument doc = this.Document;
            MoveBaseControl startC = doc.SearchElementByID(startCID).InnerControl;
            MoveBaseControl endC = doc.SearchElementByID(endCID).InnerControl;
            this.AddNewRelationByCtr(startC, endC, pinIndex, isPushCmd);
        }
        private void AddNewRelationByCtr(MoveBaseControl startC, MoveBaseControl endC, int endPinIndex, bool isPushCmd=true)
        {
            PointF startP = startC.GetEndPinLoc(0);

            ModelRelation mr = new ModelRelation(
                startC.ID,
                endC.ID,
                startP,
                endC.GetEndPinLoc(endPinIndex),
                endPinIndex
                );
            // 1. 关系不能重复
            // 2. 一个MoveOpControl的任意一个左引脚至多只能有一个输入
            // 3. 成环不能添加
            ModelDocument cd = this.Document;
            CyclicDetector cdt = new CyclicDetector(cd, mr);
            bool isDuplicatedRelation = cd.IsDuplicatedRelation(mr);
            bool isCyclic = cdt.IsCyclic();
            if (!isDuplicatedRelation && !isCyclic)
            {
                cd.AddModelRelation(mr);
                // 针脚状态要改变
                Global.GetMainForm().SetDocumentDirty();
                //endC右键菜单设置Enable                     
                Global.GetOptionDao().EnableOpOptionView(mr); // 这里会改变算子的状态
                // UndoRedo
                if (isPushCmd)
                {
                    BaseCommand delRelationCommand = new RelationAddCommand(startC.ID, endC.ID, mr.EndPin);
                    UndoRedoManager.GetInstance().PushCommand(this.Document, delRelationCommand);
                }
            }
            this.Invalidate();
            Global.GetNaviViewControl().UpdateNaviView();
        }
        /// <summary>
        /// 如果点在某条先附近，则返回该条线的索引，如果不在则返回-1
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private int PointToLine(PointF p)
        {
            float minDist = 100;
            float dist;
            int i = 0;
            int index = 0;
            foreach (ModelRelation mr in this.Document.ModelRelations)
            {
                Bezier line = new Bezier(mr.StartP, mr.EndP);
                dist = line.Distance(p);
                if (Math.Abs(dist - LineUtil.DISTNOTONLINE) > 0.0001 && dist < minDist)
                {
                    minDist = dist;
                    index = i;
                }
                i += 1;
            }
            if (minDist < LineUtil.THRESHOLD)
                return index;
            else
                return -1;
        }
        /*
         * 根据lines来重绘保存好的静态图
         */
        public void RepaintStatic(Graphics g)
        {
            // 给staticImage上色
            // canvasWrp.DrawBackgroud(r);
            // 将`需要重绘`IDrawable对象重绘在静态图上
            g.SmoothingMode = SmoothingMode.AntiAlias;
            List<ModelRelation> mrs = this.Document.ModelRelations;
            foreach (ModelRelation mr in mrs)
                LineUtil.DrawBezier(g, mr.StartP, mr.A, mr.B, mr.EndP, mr.Selected);
            g.Dispose();
        }

        public void RepaintObject(Bezier line, bool isBold = false)
        {
            if (line == null)
                return;
            Graphics g = this.CreateGraphics();
            g.SmoothingMode = SmoothingMode.AntiAlias;
            LineUtil.DrawBezier(g, line.StartP, line.A, line.B, line.EndP, isBold);
            g.Dispose();
        }


        /*
         * 使用静态图的指定位置的指定大小来覆盖当前屏幕的指定位置的指定大小
         */
        public void CoverPanelByRect(Rectangle r)
        {
            if (this.staticImage == null)
                return;
            Graphics g = this.CreateGraphics();
            if (r.X < 0) r.X = 0;
            if (r.X > this.staticImage.Width) r.X = 0;
            if (r.Y < 0) r.Y = 0;
            if (r.Y > this.staticImage.Height) r.Y = 0;

            if (r.Width > this.staticImage.Width || r.Width < 0)
                r.Width = this.staticImage.Width;
            if (r.Height > this.staticImage.Height || r.Height < 0)
                r.Height = this.staticImage.Height;
            // 用保存好的图来局部覆盖当前背景图
            r.Inflate(1, 1);
            g.DrawImage(this.staticImage, r, r, GraphicsUnit.Pixel);
            g.Dispose();

        }

        private bool CanNotPinDraw()
        {
            // return this.endC == null || !(this.endC is MoveOpControl) || !(this.startC is MoveDtControl || this.startC is MoveRsControl);
            // OPControl 不能连 OPControl
            if (StartC is MoveOpControl)
                return true;
            // 没有落点Control
            if (EndC == null)
                return true;
            // 落点Control必须是OpControl
            if (!(EndC is MoveOpControl))
                return true;

            return false;
        }

        private void RepaintAllRelations()
        {
            Graphics g = this.CreateGraphics();

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(this.BackColor);
            foreach (ModelRelation mr in this.Document.ModelRelations)
                LineUtil.DrawBezier(g, mr.StartP, mr.A, mr.B, mr.EndP, mr.Selected);
            g.Dispose();
        }



        public void CanvasPanel_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }
        private void CanvasPanel_Paint(object sender, PaintEventArgs e)
        {
            
            // 拖动时的OnPaint处理
            if (this.Document == null)
                return;

            if (dragWrapper.DragPaint(this.Size, this.Document.WorldMap.ScreenFactor, e))
                return;
            if (frameWrapper.FramePaint(e))
                return;
            //普通状态下算子的OnPaint处理
            //遍历当前文档所有line,然后画出来
            ModelDocument doc = this.Document;
            if (doc == null)
                return;
            // 将当前文档所有的线全部画出来
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            this.Document.UpdateAllLines();
            foreach (ModelRelation mr in doc.ModelRelations)
                LineUtil.DrawBezier(e.Graphics, mr.StartP, mr.A, mr.B, mr.EndP, mr.Selected);
        }
        #endregion
        public void AddElesAndRels(List<ModelElement> mes, List<Tuple<int, int, int>> mrs, bool isPushCmd=false)
        {
            foreach (ModelElement me in mes)
                this.AddEle(me);
            foreach (var mr in mrs)
                this.AddNewRelationByCtrID(mr.Item1, mr.Item2, mr.Item3, isPushCmd);
        }
        public Tuple<List<ModelElement>, List<Tuple<int, int, int>>> DeleteSelectedElesByCtrID(IEnumerable<int> ids)
        {
            ModelDocument doc = this.Document;
            List<ModelElement> mes = new List<ModelElement>();
            List<Tuple<int, int, int>> mrs = new List<Tuple<int, int, int>>();
            List<ModelRelation> oriMrs = new List<ModelRelation>(doc.ModelRelations);
            Tuple<List<Tuple<int, int, int>>, ModelElement> relsAndEle = null;
            int endID = -1;
            foreach (int id in ids)
            {
                ModelElement me = doc.SearchElementByID(id);
                switch (me.Type)
                {
                    case ElementType.DataSource:
                        foreach(ModelRelation mr in oriMrs.Where(t => t.StartID == me.ID))
                        {
                            this.DeleteRelationByCtrID(mr.StartID, mr.EndID, mr.EndPin);
                            mrs.Add(new Tuple<int, int, int>(mr.StartID, mr.EndID, mr.EndPin));
                        }
                        this.DeleteEle(me);
                        mes.Add(me);
                        break;
                    case ElementType.Operator:
                        var endIDs = doc.ModelRelations.Where(t => t.StartID == me.ID).ToList();
                        endID = endIDs.Count == 1 ? endIDs[0].EndID : -1;
                        if (endID != -1)
                        {
                            relsAndEle = (me.InnerControl as MoveOpControl).DeleteResult(endID, oriMrs);
                            if (relsAndEle.Item2 != null)
                            {
                                this.DeleteEle(relsAndEle.Item2);
                                mes.Add(relsAndEle.Item2);
                            }
                            if (relsAndEle.Item1 != null)
                            {
                                foreach (var mr in relsAndEle.Item1)
                                {
                                    this.DeleteRelationByCtrID(mr.Item1, mr.Item2, mr.Item3);
                                    mrs.Add(mr);
                                }
                            }
                        }
                        this.DeleteEle(me);
                        mes.Add(me);
                        foreach (ModelRelation mr in oriMrs.Where(t => t.EndID == me.ID))
                        {
                            this.DeleteRelationByCtrID(mr.StartID, mr.EndID, mr.EndPin);
                            mrs.Add(new Tuple<int, int, int>(mr.StartID, mr.EndID, mr.EndPin));
                        }
                        break;
                    case ElementType.Result:
                        break;
                    default:
                        break;
                }
            }
            Global.GetMainForm().SetDocumentDirty();
            Global.GetNaviViewControl().UpdateNaviView();
            return new Tuple<List<ModelElement>, List<Tuple<int, int, int>>>(mes, mrs);
        }
        public void UndoRedoAddSelectedEles(Dictionary<int, Point> eleWorldCordDict, List<ModelElement> mes, List<Tuple<int, int, int>> mrs)
        {
            this.AddElesAndRels(mes, mrs);
            WorldMap currWM = this.Document.WorldMap;
            foreach (ModelElement me in mes)
                me.Location = currWM.WorldToScreen(eleWorldCordDict[me.ID]);
        }
        public void UndoRedoDelSelectedEles(List<ModelElement> mes, List<Tuple<int, int, int>> mrs)
        {
            this.DeleteSelectedElesByCtrID(mes.Select(t => t.ID));
        }
        public void UndoRedoMoveEles(Dictionary<int, Point> idPtsDict)
        {
            // 前后两个坐标的世界坐标原点一致时，使用该方法
            ModelDocument doc = this.Document;
            WorldMap curWorldMap = this.Document.WorldMap;
            List<int> ids = new List<int>(idPtsDict.Keys);
            foreach (int id in ids)
            {
                ModelElement me = doc.SearchElementByID(id);
                if (me == null)
                    return;
                Point meOldLocation = me.InnerControl.Location;
                me.InnerControl.Location = curWorldMap.WorldToScreen(idPtsDict[id]);
                idPtsDict[id] = curWorldMap.ScreenToWorld(meOldLocation, true);
            }
            this.Document.UpdateAllLines();
        }
        public void DeleteEle(ModelElement me)
        {
            this.DeleteCtr(me.InnerControl);
            this.Document.DeleteModelElement(me);
            Global.GetMainForm().SetDocumentDirty();
            Global.GetNaviViewControl().UpdateNaviView();
        }
        public void AddEle(ModelElement me)
        {
            MoveBaseControl mbc = me.InnerControl as MoveBaseControl;
            mbc.ChangeSize(this.Document.WorldMap.SizeLevel); 
            this.AddCtr(mbc);
            this.Document.AddModelElement(me);
            Global.GetMainForm().SetDocumentDirty();
            Global.GetNaviViewControl().UpdateNaviView();
        }
        public void AddEleWhenUndoRedo(ModelElement me)
        {
            MoveBaseControl mbc = me.InnerControl as MoveBaseControl;
            mbc.ChangeSize(this.Document.WorldMap.SizeLevel);
            mbc.Location = this.Document.WorldMap.WorldToScreen(mbc.WorldCord);
            this.AddCtr(mbc);
            this.Document.AddModelElement(me);
            Global.GetMainForm().SetDocumentDirty();
            Global.GetNaviViewControl().UpdateNaviView();
        }
        public void DeleteCtr(Control ctl)
        {
            this.Controls.Remove(ctl);
        }
        public void AddCtr(Control ctl)
        {
            this.Controls.Add(ctl);
        }
        public void AddNewOperator(int sizeL, string description, string subTypeName, Point location)
        {
            MoveOpControl btn = new MoveOpControl(
                                sizeL,
                                description,
                                subTypeName,
                                location);
            
            AddNewElement(btn);
        }

        public void AddMindMapDataSource(List<DataItem> dataItems)
        {
            if (dataItems == null)
                return;

            int diX = 10;
            int diY = 10;
            int marginX = 200;
            int marginY = 40;
            int sizeLevel = Global.GetCurrentModelDocument().WorldMap.SizeLevel;
            float screenFactor = Global.GetCurrentModelDocument().WorldMap.ScreenFactor;

            foreach (DataItem di in dataItems)
            {
                string text = di.FileName;
                string path = di.FilePath;
                char separator = di.FileSep;
                OpUtil.Encoding encoding = di.FileEncoding;
                OpUtil.ExtType extType = di.FileType;
                Point location = new Point(Convert.ToInt32(diX / screenFactor), Convert.ToInt32(diY / screenFactor));
                //转世界坐标，目前看起来没有用，有显示错误之后再调整
                //PointF locationWorld = Global.GetCurrentModelDocument().WorldMap.ScreenToWorldF(location, false);
                if (diY + marginY > 930)
                {
                    diX += marginX;
                    diY = 10;
                }
                else
                    diY += marginY;

                AddNewDataSource(path, sizeLevel, text, location, separator, extType, encoding);

            }
        }
            public void AddNewDataSource(string path, int sizeL, string text, Point location, char separator, OpUtil.ExtType extType, OpUtil.Encoding encoding)
        {
            MoveDtControl btn = new MoveDtControl(
                path,
                sizeL,
                text,
                location,
                separator,
                encoding);
            AddNewElement(btn);
        }
        public MoveRsControl AddNewResult(string desciption, int sizeL, Point location, char separator, OpUtil.Encoding encoding)
        {
            MoveRsControl btn = new MoveRsControl(sizeL,
                                desciption,
                                location)
            {
                Separator = separator,
                Encoding = encoding
            };
            btn.Location = location;
            AddNewElement(btn);
            return btn;
        }

        private void AddNewElement(MoveBaseControl btn)
        {
            // NewElementEvent中有压栈操作，可以UndoRedo
            this.Controls.Add(btn);
            this.Document.WorldMap.WorldBoundControl(btn.Location, btn);
            Global.GetNaviViewControl().UpdateNaviView();
            NewElementEvent?.Invoke(btn);
        }

        private bool SelectDrag()
        {
            return Global.GetTopToolBarControl().SelectDrag;
        }

        private bool SelectFrame()
        {
            return Global.GetTopToolBarControl().SelectFrame; 
        }

        #region 关于引脚在画线状态的改变

        private void RepaintStartcPin(MoveBaseControl startC, int id)
        {
            //int id = (startC as IMoveControl).GetID();
            foreach (ModelRelation mr in this.Document.ModelRelations)
            {
                if (mr.StartID == id)
                    return;
            }

            (startC as IMoveControl).OutPinInit("noLine");
        }
        #endregion

        private void DelControls_Click(object sender, EventArgs e)
        {
            delEnable = true;
            frameWrapper.FrameDel(sender, e);
        }
        public void ControlSelect_Copy()
        {
            clipBoard.ClipBoardCts = frameWrapper.Controls.ToList();
        }
        public void ControlSelect_paste()
        {
            clipBoard.ClipBoardPaste();
        }
    }
}