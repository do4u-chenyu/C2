﻿using Citta_T1.Business.Model;
using Citta_T1.Controls.Interface;
using Citta_T1.Controls.Move;
using Citta_T1.Controls.Move.Dt;
using Citta_T1.Controls.Move.Op;
using Citta_T1.Controls.Move.Rs;
using Citta_T1.Core;
using Citta_T1.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Citta_T1.Controls
{
    public delegate void NewElementEventHandler(MoveBaseControl ct);

    public enum ECommandType
    {
        Hold,
        PinDraw,
        Null,
    }
    public partial class CanvasPanel : UserControl
    {
        private static LogUtil log = LogUtil.GetInstance("CanvasPanel");
        public event NewElementEventHandler NewElementEvent;
        public Bitmap staticImage;
        public Bitmap staticImage2;


        //屏幕拖动涉及的变量
        private float screenFactor = 1;
        private DragWrapper dragWrapper;
        private FrameWrapper frameWrapper;

        Graphics g;


        // 绘图
        // 绘图
        public List<Bezier> lines = new List<Bezier>() { };
        
        public ECommandType cmd = ECommandType.Null;
        public PointF startP;
        public PointF endP;
        private Control startC;
        private Control endC;
        Rectangle invalidateRectWhenMoving;
        Bezier lineWhenMoving;
        private List<int> selectLineIndexs = new List<int> { };




        private bool delEnable = false;
        private bool startCopy = false;
        public void SetStartP(PointF p)
        {
            startP = p;
        }


        public Control StartC { get => startC; set => startC = value; }
        public Control EndC { get => endC;  set => endC = value; }
        public float ScreenFactor { get => screenFactor; set => screenFactor = value; }
        internal FrameWrapper FrameWrapper { get => frameWrapper; set => frameWrapper = value; }
        public bool DelEnable { get => delEnable; set => delEnable = value; }





        public CanvasPanel()
        {
            dragWrapper = new DragWrapper();
            frameWrapper = new FrameWrapper();
            InitializeComponent();
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true); // 双缓冲DoubleBuffer
            SetStyle(ControlStyles.ResizeRedraw, true);

        }




        #region 右上角功能实现部分
        //画布右上角的放大与缩小功能实现
        public void ChangSize(bool isLarger, float factor = Global.Factor)
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);//禁止擦除背景.
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);//双缓冲
            this.UpdateStyles();
            int sizeLevel = Global.GetCurrentDocument().WorldMap1.GetWmInfo().SizeLevel;
            if (isLarger && sizeLevel <= 2)
            {
                sizeLevel += 1;
                
                Global.GetCurrentDocument().WorldMap1.GetWmInfo().ScreenFactor *= factor;
                foreach (Control con in Controls)
                {
                    if (con is IScalable && con.Visible)
                    {
                        (con as IScalable).ChangeSize(sizeLevel);
                    }
                }
                foreach (ModelRelation mr in Global.GetCurrentDocument().ModelRelations)
                {
                    mr.ZoomIn();
                }
            }
            else if (!isLarger && sizeLevel > 0)
            {
                sizeLevel -= 1;
                
                Global.GetCurrentDocument().WorldMap1.GetWmInfo().ScreenFactor /= factor;
                foreach (Control con in Controls)
                {
                    if (con is IScalable && con.Visible)
                    {
                        (con as IScalable).ChangeSize(sizeLevel);
                    }
                }
                foreach (ModelRelation mr in Global.GetCurrentDocument().ModelRelations)
                {
                    mr.ZoomOut();
                }
            }
            
            Global.GetCurrentDocument().WorldMap1.GetWmInfo().SizeLevel = sizeLevel;
            Global.GetNaviViewControl().UpdateNaviView();
        }

        #endregion
        private Point WorldBoundControl(Point Ps)
        {

            Point dragOffset = new Point(0, 0);
            float screenFactor = Global.GetCurrentDocument().WorldMap1.GetWmInfo().ScreenFactor;

            if (Ps.Y < 70 * screenFactor)
            {
                dragOffset.Y = Ps.Y - 70;
            }
            if (Ps.X > 2000 * screenFactor)
            {
                dragOffset.X = Ps.X - 2000;
            }
            if (Ps.Y > 900 * screenFactor)
            {
                dragOffset.Y = Ps.Y - 900;
            }
            return dragOffset;
        }
        #region 各种事件
        public void CanvasPanel_DragDrop(object sender, DragEventArgs e)
        {
            ElementType type = ElementType.Empty;
            char separator = '\t';
            string path = "";
            string text = "";
            OpUtil.Encoding encoding = OpUtil.Encoding.UTF8;
            OpUtil.ExtType extType;
            Point location = this.Parent.PointToClient(new Point(e.X - 300, e.Y - 100));
            Point moveOffset = WorldBoundControl(location);
            location.X -=  moveOffset.X;
            location.Y -=  moveOffset.Y;
            type = (ElementType)e.Data.GetData("Type");
            text = e.Data.GetData("Text").ToString();
            int sizeLevel = Global.GetCurrentDocument().WorldMap1.GetWmInfo().SizeLevel;
            if (type == ElementType.DataSource)
            {
                path = e.Data.GetData("Path").ToString();
                separator = (char)e.Data.GetData("Separator");
                encoding = (OpUtil.Encoding)e.Data.GetData("Encoding");
                extType = (OpUtil.ExtType)e.Data.GetData("ExtType");
                AddNewDataSource(path, sizeLevel, text, location, separator, extType, encoding);
            }
            else if (type == ElementType.Operator)
                AddNewOperator(sizeLevel, text, location);


        }

        public void CanvasPanel_MouseDown(object sender, MouseEventArgs e)
        {
            selectLineIndexs.Clear();
            Global.GetMainForm().BlankButtonFocus();                    // 强制编辑控件失去焦点,触发算子控件的Leave事件
            this.ClickOnLine(e);
            if (e.Button == MouseButtons.Right) 
            {
                
                Point pw = Global.GetCurrentDocument().WorldMap1.ScreenToWorld(e.Location,false);
                if (frameWrapper.MinBoding.Contains(pw))
                {
                    this.DelSelectControl.Show(this,e.Location);
                    return;
                }
                    
                Global.GetFlowControl().ResetStatus();
                frameWrapper.MinBoding = new Rectangle(0, 0, 0, 0);// 点击右键, 清空操作状态,进入到正常编辑状态
                
            }
            if (sender is MoveDtControl || sender is MoveRsControl)
            {
                this.cmd = ECommandType.PinDraw;
                this.StartC = sender as Control;
                this.SetStartP(new PointF(e.X, e.Y));
                this.staticImage = new Bitmap(this.Width, this.Height);
                Graphics g = Graphics.FromImage(this.staticImage);
                g.Clear(this.BackColor);
                this.RepaintStatic(g);
                g.Dispose();
            }
            if (SelectFrame())
            {
                frameWrapper.FrameWrapper_MouseDown(e);
            }
            if (SelectDrag())
            {
                dragWrapper.DragDown(this.Size, Global.GetCurrentDocument().WorldMap1.GetWmInfo().ScreenFactor, e);
            }
        }
        private bool IsValidLine(ModelRelation mr)
        {
            // 合法的线有 Dt-Op Rs-Op
            ModelDocument md = Global.GetCurrentDocument();
            List<ModelRelation> mrs = md.ModelRelations;
            List<ModelElement> mes = md.ModelElements;

            ModelElement sMe = md.SearchElementByID(mr.StartID);
            ModelElement eMe = md.SearchElementByID(mr.EndID);

            return ((sMe.Type == ElementType.DataSource || sMe.Type == ElementType.Result) && eMe.Type == ElementType.Operator);
        }
        private void ClickOnLine(MouseEventArgs e)
        {
            /*
             * 点击规则见0511
             */
            List<ModelRelation> mrs = Global.GetCurrentDocument().ModelRelations;
            int mrIndex = PointToLine(new PointF(e.X, e.Y));

            if (mrIndex == -1)
            {
                if (e.Button == MouseButtons.Right)
                    this.SetAllLineStatus(null, true);
            }
            else
            {
                if (mrIndex < mrs.Count && !this.SelectDrag() && !this.SelectFrame())
                    if (IsValidLine(mrs[mrIndex]))
                        selectLineIndexs.Add(mrIndex);
                    else
                    {
                        MessageBox.Show("算子与结果之间的连线是系统自动维护的，不能被人为选中、添加或删除。");
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
            ModelRelation mr;
            List<ModelRelation> mrs = Global.GetCurrentDocument().ModelRelations;
            for (int i = 0; i < mrs.Count; i++)
            {
                mr = mrs[i];
                if (exceptLineIndex != null && exceptLineIndex.Contains(i))
                    mr.Selected = !mr.Selected;
                else
                    mr.Selected = false;
            }
            if (isInvalidate)
                this.Invalidate(false);
        }

        private void DeleteLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.DeleteSelectedLines();
        }

        public void DeleteSelectedLines()
        {
            List<ModelRelation> mrs = Global.GetCurrentDocument().ModelRelations;
            ModelRelation mr;
            foreach (int i in selectLineIndexs)
            {
                try
                {
                    mr = mrs[i];
                    //删除线配置逻辑
                    ModelDocument doc =  Global.GetCurrentDocument();
                    doc.StatusChangeWhenDeleteLine(mr.EndID);

                    doc.RemoveModelRelation(mr);
                    //关联算子引脚自适应改变
                    Control lineStartC = doc.SearchElementByID(mr.StartID).InnerControl;
                    this.RepaintStartcPin(lineStartC, mr.StartID);
                    Control lineEndC = doc.SearchElementByID(mr.EndID).InnerControl;
                    (lineEndC as IMoveControl).InPinInit(mr.EndPin);
                    //删除线文档dirty


                    Global.GetMainForm().SetDocumentDirty();
                   
                }
                catch (Exception e)
                {
                    log.Error("CanvasPanel删除线时发生错误:" + e);
                }

            }
            selectLineIndexs.Clear();
            this.Invalidate(false);
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
            float threshold = LineUtil.THRESHOLD;
            float distNotOnLine = LineUtil.DISTNOTONLINE;
            int mrIndex = 0;
            int index = 0;
            foreach(ModelRelation mr in Global.GetCurrentDocument().ModelRelations)
            {
                Bezier line = new Bezier(mr.StartP, mr.EndP);
                dist = line.PointToLine(p);
                if (Math.Abs(dist - distNotOnLine) > 0.0001 && dist < minDist)
                {
                    minDist = dist;
                    index = mrIndex;
                }
                mrIndex += 1;
            }
            if (minDist < threshold)
                return index;
            else
                return -1;
        }
        public void CanvasPanel_MouseMove(object sender, MouseEventArgs e)
        { 
            
            // 画框
            if (SelectFrame())
            {
                frameWrapper.FrameWrapper_MouseMove(e);
               
            }
            if (e.Button != MouseButtons.Left) return;
            // 控件移动
            else if (SelectDrag())
            {
                dragWrapper.DragMove(this.Size, Global.GetCurrentDocument().WorldMap1.GetWmInfo().ScreenFactor, e);
            }
            // 绘制
            else if (cmd == ECommandType.PinDraw)
            {
                // 吸附效果实现
                /*
                 * 1. 遍历当前Document上所有LeftPin，检查该点是否在LeftPin的附近
                 * 2. 如果在，对该点就行修正
                 * 3. 
                 */
                PointF nowP = e.Location;
                if (lineWhenMoving != null)
                    invalidateRectWhenMoving = LineUtil.ConvertRect(lineWhenMoving.GetBoundingRect());
                else
                    invalidateRectWhenMoving = new Rectangle();
                // 遍历所有OpControl的leftPin
                // 是否在一轮循环中被多次修正？=> 只要控件不堆叠在一起，就不会出现被多个控件修正的情况
                // 只要在循环中被修正一次，就退出，防止被多个控件修正坐标
                // 遍历一遍之后如果没有被校正，则this.endC=null
                foreach (ModelElement modelEle in Global.GetCurrentDocument().ModelElements)
                {
                    Control con = modelEle.InnerControl;
                    if (modelEle.Type == ElementType.Operator && con != startC)
                    {
                        // 修正坐标
                        nowP = (con as IMoveControl).RevisePointLoc(nowP);
                        // 完成一次矫正
                        if (this.endC != null)
                            break;
                    }
                }
                endP = nowP;
                lineWhenMoving = new Bezier(startP, nowP);
                // 不应该挡住其他的线
                CoverPanelByRect(invalidateRectWhenMoving);
                lineWhenMoving.OnMouseMove(nowP);
                
                // 重绘曲线
                RepaintObject(lineWhenMoving);
            }
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
            List<ModelRelation> mrs = Global.GetCurrentDocument().ModelRelations;
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
            g = this.CreateGraphics();
            if (r.X < 0) r.X = 0;
            if (r.X > this.staticImage.Width) r.X = 0;
            if (r.Y < 0) r.Y = 0;
            if (r.Y > this.staticImage.Height) r.Y = 0;

            if (r.Width > this.staticImage.Width || r.Width < 0)
                r.Width = this.staticImage.Width;
            if (r.Height > this.staticImage.Height || r.Height < 0)
                r.Height = this.staticImage.Height;
            // 用保存好的图来局部覆盖当前背景图
            Pen pen = new Pen(Color.Red);
            pen.Dispose();
            r.Inflate(1, 1);
            g.DrawImage(this.staticImage, r, r, GraphicsUnit.Pixel);
            g.Dispose();

        }
        public void CanvasPanel_MouseUp(object sender, MouseEventArgs e)
        {

            if (e.Button != MouseButtons.Left) 
                return;
            // 画框处理
            if (SelectFrame())
            {
                frameWrapper.FrameWrapper_MouseUp(e);
                return;
            }
            // 拖拽处理
            if (SelectDrag())
            {   
                dragWrapper.DragUp(this.Size, Global.GetCurrentDocument().WorldMap1.GetWmInfo().ScreenFactor, e);
                return;
            }

            // 非画线落点处理
            if (cmd != ECommandType.PinDraw)
                return;

            cmd = ECommandType.Null;
            lineWhenMoving = null;

            /* 不是所有位置Up都能形成曲线的
                * 如果没有endC，或者endC不是OpControl，那就不形成线，结束绘线动作
                */
            if (CanNotPinDraw())
            {
                this.RepaintAllRelations();
                this.RepaintStartcPin(startC, (startC as IMoveControl).GetID());
                return;
            }

            // 画线落点处理
            /* 
                * 在Canvas_MouseMove的时候，对鼠标的终点进行
                * 只保存线索引
                *         __________
                * endP1  | MControl | startP
                * endP2  |          |
                * 
                *         ----------
                */
            (endC as MoveOpControl).RectInAdd((endC as MoveOpControl).RevisedPinIndex);
            ModelRelation mr = new ModelRelation(
                (startC as IMoveControl).GetID(),
                (endC as IMoveControl).GetID(),
                startP,
                (endC as MoveOpControl).GetEndPinLoc((endC as MoveOpControl).RevisedPinIndex),
                (endC as MoveOpControl).RevisedPinIndex
                );
            // 1. 关系不能重复
            // 2. 一个MoveOpControl的任意一个左引脚至多只能有一个输入
            // 3. 成环不能添加
            ModelDocument cd = Global.GetCurrentDocument();
            CyclicDetector cdt = new CyclicDetector(cd, mr);
            bool isDuplicatedRelation = cd.IsDuplicatedRelation(mr);
            bool isCyclic = cdt.IsCyclic();
            if (!isDuplicatedRelation && !isCyclic)
            {
                cd.AddModelRelation(mr);
                //endC右键菜单设置Enable                     
                Global.GetOptionDao().EnableOpOptionView(mr);

            }
            this.Invalidate();
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
            foreach (ModelRelation mr in Global.GetCurrentDocument().ModelRelations)
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
            if (Global.GetCurrentDocument() == null)
                return;

            if (dragWrapper.DragPaint(this.Size, Global.GetCurrentDocument().WorldMap1.GetWmInfo().ScreenFactor, e))
                return;
            if (frameWrapper.FramePaint(e))
                return;
            //普通状态下算子的OnPaint处理
            //遍历当前文档所有line,然后画出来
            ModelDocument doc = Global.GetCurrentDocument();
            if (doc == null)
                return;
            // 将当前文档所有的线全部画出来
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Global.GetCurrentDocument().UpdateAllLines();
            foreach (ModelRelation mr in doc.ModelRelations)
                LineUtil.DrawBezier(e.Graphics, mr.StartP, mr.A, mr.B, mr.EndP, mr.Selected);
        }
        #endregion

        public void DeleteElement(Control ctl)
        {
            this.Controls.Remove(ctl);
        }

        public void AddElement(Control ctl)
        {
            this.Controls.Add(ctl);
        }
        private void AddNewOperator(int sizeL, string text, Point location)
        {
            MoveOpControl btn = new MoveOpControl(
                                sizeL,
                                text,
                                text,
                                location);
            AddNewElement(btn);
        }

        private void AddNewDataSource(string path, int sizeL, string text, Point location, char separator, OpUtil.ExtType extType, OpUtil.Encoding encoding)
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
            AddNewElement(btn);
            return btn;
        }

        private void AddNewElement(MoveBaseControl btn)
        {
            this.Controls.Add(btn);
            Global.GetNaviViewControl().UpdateNaviView();
            NewElementEvent?.Invoke(btn);
        }

        private bool SelectDrag()
        {
            return Global.GetFlowControl().SelectDrag;
        }

        private bool SelectFrame()
        {
            return Global.GetFlowControl().SelectFrame;
        }
        
        #region 关于引脚在画线状态的改变
        
        private void RepaintStartcPin(Control startC,int id)
        {
            //int id = (startC as IMoveControl).GetID();
            foreach (ModelRelation mr in Global.GetCurrentDocument().ModelRelations)
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
            startCopy = true;
        }
        public void ControlSelect_paste()
        {
            if (!startCopy)
                return;
            log.Info("sss");
            frameWrapper.FramePaste();
        }
    }
}