using C2.Business.Model;
using C2.Configuration;
using C2.Core;
using C2.Dialogs;
using C2.Dialogs.C2OperatorViews;
using C2.Model;
using C2.Model.MindMaps;
using C2.Model.Styles;
using C2.Model.Widgets;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace C2.Controls.MapViews
{
    public partial class MindMapView
    {
        HitTestResult _HoverObject;
        HitTestResult _PressObject;
        bool IsMouseDown;
        ChartMouseState MouseState;
        Point MouseDownPos;
        Cursor StyleBrushCursor;
        Cursor ScrollCursor;
        MindMapViewDragBox DragBox;
        Point LastMousePos = Point.Empty;

        //ChartToolTip LastToolTip = null;

        void InitializeMouse()
        {
            try
            {
                StyleBrushCursor = Helper.LoadCursor(Properties.Resources.cur_style_brush);
                ScrollCursor = Helper.LoadCursor(Properties.Resources.hand_cur);
            }
            catch(System.Exception ex)
            {
                Helper.WriteLog(ex);
                StyleBrushCursor = C2.Resources.RS.GetCursor("cur_style_brush");
                ScrollCursor = C2.Resources.RS.GetCursor("cur_scroll");
            }

            DragBox = new MindMapViewDragBox(this);
        }

        internal HitTestResult HoverObject
        {
            get { return _HoverObject; }
            set
            {
                if (_HoverObject != value)
                {
                    HitTestResult old = _HoverObject;
                    _HoverObject = value;
                    OnHoverObjectChanged(old);
                }
            }
        }
        internal HitTestResult PressObject
        {
            get { return _PressObject; }
            set
            {
                if (_PressObject != value)
                {
                    _PressObject = value;
                    OnPressObjectChanged();
                }
            }
        }

        DragTopicsMethod CurrentDragMethod { get; set; }
        public ToolTip C2WidgetTip { get; set; } = new ToolTip();

        void OnHoverObjectChanged(HitTestResult old)
        {
            if (old != null)
            {
                if (old.Widget != null && (HoverObject == null || HoverObject.Widget != old.Widget) && old.Widget.ResponseMouse)
                    old.Widget.Hover = false;
            }

            if (HoverObject != null)
            {
                if (HoverObject.Widget != null && HoverObject.Widget.ResponseMouse)
                    HoverObject.Widget.Hover = true;
            }

            if (MouseState == ChartMouseState.Normal)
            {
                InvalidateChart();

                if (ShowToolTips)
                {
                    if ((HoverObject.Topic != null) && !HoverObject.IsFoldingButton)
                    {
                        if (HoverObject.Widget != null)
                        {
                            ShowToolTip(HoverObject.Widget);
                            //if (HoverObject.Widget is IColorToolTip)
                            //    ShowToolTip((IColorToolTip)HoverObject.Widget);
                            //else
                            //    HideToolTip();
                            //if (HoverObject.Widget is NotesWidget)
                            //    ShowToolTip(HoverObject.Widget.Tooltip, HoverObject.Widget.Hyperlink, ((NotesWidget)HoverObject.Widget).BackColor, true);
                            //else
                            //    ShowToolTip(HoverObject.Widget.Tooltip, HoverObject.Widget.Hyperlink, Color.Empty, true);
                        }
                        else
                        {
                            ShowToolTip(HoverObject.Topic);//.Tooltip, HoverObject.Topic.Hyperlink, Color.Empty, false);// .Notes);
                        }

                    }
                    else
                    {
                        HideToolTip();
                        //ShowToolTip(null, false);
                    }
                }
            }
        }

        void OnPressObjectChanged()
        {
            if (MouseState == ChartMouseState.Normal)
            {
                InvalidateChart();
            }
        }
        protected override void OnChartDragDrop(DragEventArgs e)
        {
            ElementType type = (ElementType)e.Data.GetData("Type");
            if (type == ElementType.DataSource)
                AddDataButtonToTopic(e);
            else if (type == ElementType.Empty)
                AddModelButtonToTopic(e);

        }
        private void AddModelButtonToTopic(DragEventArgs e)
        {
            // 模型市场信息
            string modelFullFilePath = (string)e.Data.GetData("Path");
            string modelTitle = (string)e.Data.GetData("Text");
            // 获取topic
            Point pointToClient = this.ChartBox.PointToClient(new Point(e.X, e.Y));
            var htr = HitTest(pointToClient.X, pointToClient.Y);

            if (htr == HitTestResult.Empty)
                return;

            CreateNewModelForm createNewModelForm = new CreateNewModelForm
            {
                StartPosition = FormStartPosition.CenterScreen,
                RelateMindMapView = this,
                OpenDocuments = Global.GetMainForm().OpendDocuments(),
                NewFormType = FormType.CanvasForm,
                ModelType = "新建模型视图"
            };
            DialogResult dialogResult = createNewModelForm.ShowDialog();
            if (dialogResult != DialogResult.OK)
                return;

            OperatorWidget opw = htr.Topic.FindWidget<OperatorWidget>();
            string modelDocumentName = createNewModelForm.ModelTitle;
            string modelUserPath = Path.Combine(Global.WorkspaceDirectory, Global.GetMainForm().UserName, "业务视图", Global.GetCurrentDocument().Name);
            string modelSavePath = Path.Combine(modelUserPath, modelDocumentName, modelDocumentName + ".xml");

            //新建模型前保存一次，防止出现用户一直未保存导致模型视图路径逻辑出错
            Global.GetDocumentForm().Save();

            if(opw != null && opw.HasModelOperator)
            {
                HelpUtil.ShowMessageBox("该节点已存在高级模型，请右键算子挂件，选择对应模型进行修改或删除。", "已存在", MessageBoxIcon.Information);
                return;
            }
            //需要拷贝模型市场文件夹到当前模型路径,复制之后修改XML文件中数据源路径
            if (!ExportModel.GetInstance().Export(modelFullFilePath, modelDocumentName, modelUserPath))
                return;

            string dirs = Path.Combine(modelUserPath, modelDocumentName, "_datas");
            ImportModel.GetInstance().RenameFile(dirs, modelSavePath);

            DataItem modelDataItem = new DataItem(modelSavePath, modelDocumentName, '\t', OpUtil.Encoding.NoNeed, OpUtil.ExtType.Unknow);
            if (opw == null)
            {
                htr.Topic.Add(new OperatorWidget { HasModelOperator = true, ModelDataItem = modelDataItem });
            }
            else
            {
                opw.HasModelOperator = true;
                opw.ModelDataItem = modelDataItem;
                Global.GetCurrentDocument().Modified = true;
            }

            Global.OnModifiedChange();
        }

        private void AddDataButtonToTopic(DragEventArgs e)
        {
            DataItem dataItem;
            DatabaseType databaseType = (DatabaseType)e.Data.GetData("DataType");
            if (databaseType == DatabaseType.Null)
                dataItem = new DataItem((string)e.Data.GetData("Path"), (string)e.Data.GetData("Text"), (char)e.Data.GetData("Separator"), (OpUtil.Encoding)e.Data.GetData("Encoding"), (OpUtil.ExtType)e.Data.GetData("ExtType"));
            else
                dataItem = new DataItem((DatabaseType)e.Data.GetData("DataType"),(DatabaseItem)e.Data.GetData("TableInfo"));

            // 获取topic
            Point pointToClient = this.ChartBox.PointToClient(new Point(e.X, e.Y));
            var htr = HitTest(pointToClient.X, pointToClient.Y);

            if (htr == HitTestResult.Empty)
                return;
            DataSourceWidget dsw = htr.Topic.FindWidget<DataSourceWidget>();
            if (dsw == null)
            {
                Topic[] hitTopic = new Topic[] { htr.Topic };
                hitTopic[0].Widgets.Add(new DataSourceWidget { DataItems = new List<DataItem> { dataItem } });
                TopicUpdate(hitTopic[0], null);
                ShowDesigner(hitTopic[0], false);
                return;
            }
            if (dsw.DataItems.Find((DataItem x) => x.FilePath.Equals(dataItem.FilePath)) == null)
            {
                dsw.DataItems.Add(dataItem);
                Global.GetCurrentDocument().Modified = true;
                TopicUpdate(dsw.Container, null);
                ShowDesigner(dsw.Container, false);
            }
            else
                MessageBox.Show(String.Format("数据源{0}:[{1}]已存在,可以删除后重新导入.", dataItem.FileName, dataItem.FilePath),
                    "数据源已存在",                // 标题
                    MessageBoxButtons.OK,          // 按钮样式
                    MessageBoxIcon.Information);   // 图标样式
        }
        protected override void OnChartMouseDown(MouseEventArgs e)
        {
            base.OnChartMouseDown(e);

            if (!ChartBox.Focused)
            {
                ChartBox.Focus();
            }

            IsMouseDown = true;
            MouseDownPos = new Point(e.X, e.Y);
            PressObject = HitTest(e.X, e.Y);
            LastMousePos = MouseDownPos;

            if (EditMode)
            {
                EndEdit(true);
            }

            ToolTipLayer.HideAllToolTips();

            // if the Shift is press, maybe is hover hyperlink, or reverse MouseMethod
            ChartMouseMethod mouseMethod = MouseMethod;
            if (Helper.TestModifierKeys(Keys.Shift))
            {
                if (mouseMethod == ChartMouseMethod.Select)
                    mouseMethod = ChartMouseMethod.Scroll;
            }

            //
            if (mouseMethod == ChartMouseMethod.Scroll)
            {
                _ResetCursor();
            }
            else if (mouseMethod == ChartMouseMethod.Select)
            {
                if (PressObject.Topic != null)
                {
                    if (PressObject.IsFoldingButton)
                    {
                        PressObject.Topic.Toggle();
                    }
                    else if (PressObject.Widget != null)
                    {
                        if (!PressObject.Widget.Selected)
                        {
                            Select(PressObject.Widget, !Helper.TestModifierKeys(Keys.Control));
                        }
                        else if (Helper.TestModifierKeys(Keys.Control))
                        {
                            Unselect(PressObject.Widget);
                        }
                    }
                    else
                    {
                        if (!PressObject.Topic.Selected)
                        {
                            Select(PressObject.Topic, !Helper.TestModifierKeys(Keys.Control));
                            //SelectTopic(PressObject.Topic, !Helper.TestModifierKeys(Keys.Control));
                        }
                        else if (Helper.TestModifierKeys(Keys.Control))
                        {
                            Unselect(PressObject.Topic);
                        }
                    }
                }
                else if (e.Button == MouseButtons.Left)
                {
                    if (!Helper.TestModifierKeys(Keys.Control))
                        ClearSelection();
                    EnterSelectMode();
                }
            }
        }

        protected override void OnChartMouseUp(MouseEventArgs e)
        {
            base.OnChartMouseUp(e);

            ProcessChartMouseUp(e);

            MouseState = ChartMouseState.Normal;
            IsMouseDown = false;
            MouseDownPos = Point.Empty;
            PressObject = HitTestResult.Empty;
            _ResetCursor();
        }

        void ProcessChartMouseUp(MouseEventArgs e)
        {
            if (MouseState == ChartMouseState.Drag)
            {
                DoChartDrag(e);  // 处理拖拽动作
                return;
            }

            if (MouseState == ChartMouseState.Select)
            {
                DoChartSelect(); // 处理节点选择
                return;
            }

            if (MouseState == ChartMouseState.Scroll)
            {
                //Scroll(MouseDownPos.X - e.X, MouseDownPos.Y - e.Y);
                return;
            }

            if (!FormatPainter.Default.IsEmpty && !ReadOnly)
            {
                DoFormat(e); // 处理格式刷动作
                return;
            }

            if (Helper.TestModifierKeys(Keys.Shift)
                && e.Button == MouseButtons.Left
                && (PressObject.Widget != null || PressObject.Topic != null))
            {
                DoOpenUrl();  // 处理打开链接
                return;
            }

            if (Helper.TestModifierKeys(Keys.Control)
                && e.Button == MouseButtons.Left
                && SelectedObjects.Length <= 1)
            {
                if (PressObject.Topic != null 
                    && !string.IsNullOrEmpty(PressObject.Topic.Hyperlink)
                    && (SelectedObjects.IsEmpty() || SelectedObjects[0] == PressObject.Topic))
                {
                    Helper.OpenUrl(PressObject.Topic.Hyperlink);
                    return;
                }

                if (PressObject.Widget != null 
                    && !string.IsNullOrEmpty(PressObject.Widget.Hyperlink)
                    && (SelectedObjects.IsEmpty() || SelectedObjects[0] == PressObject.Widget))
                {
                    Helper.OpenUrl(PressObject.Widget.Hyperlink);
                    return;
                }
            }

            // Normal Status
            if (HoverObject != null && HoverObject.Widget != null && HoverObject == PressObject)
            {
                if (e.Button == MouseButtons.Right && e.Clicks == 1)
                {
                    DoWidgetMenu(e); // 处理挂件菜单
                    return;
                }
                else if (e.Button == MouseButtons.Left && e.Clicks == 2)
                {
                    // 处理鼠标左键双击
                    HoverObject.Widget.OnDoubleClick(new HandledEventArgs());
                    return ;
                }
            }

            else if (e.Button == MouseButtons.Right && ChartContextMenuStrip != null)
            {
                ChartContextMenuStrip.Show(this.ChartBox, new Point(e.X, e.Y));
                return;
            }
        }

        private void DoOpenUrl()
        {
            // Open Url
            if (PressObject.Widget != null && !string.IsNullOrEmpty(PressObject.Widget.Hyperlink))
                Helper.OpenUrl(PressObject.Widget.Hyperlink);
            else if (PressObject.Topic != null && !string.IsNullOrEmpty(PressObject.Topic.Hyperlink))
                Helper.OpenUrl(PressObject.Topic.Hyperlink);
        }

        private void DoWidgetMenu(MouseEventArgs e)
        {
            //new 右键点击弹出菜单
            HoverObject.Widget.OnMouseClick(this.ChartBox, e.Location);
            CreateWidgetMenu();
            WidgetMenuStrip.Show(this.ChartBox, new Point(e.X, e.Y));
        }

        private void DoFormat(MouseEventArgs e)
        {
            HitTestResult htr = HitTest(e.X, e.Y);
            if (!htr.IsEmpty && !htr.IsFoldingButton && e.Button == MouseButtons.Left)
            {
                FormatPainter.Default.Assign(htr.Topic);
            }

            if (!FormatPainter.Default.HoldOn && !Helper.TestModifierKeys(Keys.Control))
                FormatPainter.Default.Clear();
        }

        private void DoChartSelect()
        {
            Topic[] topics = GetTopicsInRect(LastSelectionBox);
            if (topics != null && topics.Length > 0)
                Select(topics, !Helper.TestModifierKeys(Keys.Control));
            //SelectTopics(topics, !Helper.TestModifierKeys(Keys.Control));
            ExitSelectMode();
            //ClearSelectionBox();
            return;
        }

        private void DoChartDrag(MouseEventArgs e)
        {
            if (DragBox.Visible && !DragBox.Topics.IsNullOrEmpty())
            {
                var htr = HitTest(e.X, e.Y);
                if (!htr.IsEmpty && !htr.IsFoldingButton && TestDragDrop(DragBox.Topics, htr.Topic, CurrentDragMethod))
                {
                    DargDropTo(DragBox.Topics, htr.Topic, CurrentDragMethod);
                    foreach(Topic topic in DragBox.Topics)
                        TopicUpdate(topic, null);
                }
            }

            CancelDrag();
            return;
        }

        protected override void OnChartMouseMove(MouseEventArgs e)
        {
            base.OnChartMouseMove(e);

            // if the Shift is press, maybe is hover hyperlink, or reverse MouseMethod
            ChartMouseMethod mouseMethod = MouseMethod;
            if (Helper.TestModifierKeys(Keys.Shift))
            {
                if (mouseMethod == ChartMouseMethod.Select)
                    mouseMethod = ChartMouseMethod.Scroll;
            }

            if (MouseState == ChartMouseState.Drag)
            {
                CurrentDragMethod = Helper.TestModifierKeys(Keys.Control) ? DragTopicsMethod.Copy : DragTopicsMethod.Move;
                //bool canDrop = TestDrop(PressObject.Topic, HitTest(e.X, e.Y).Topic);
                //ShowDragBox(PressObject.Topic, e.X, e.Y, canDrop);
                bool canDrag = TestDragDrop(DragBox.Topics, HitTest(e.X, e.Y).Topic, CurrentDragMethod);
                ShowDragBox(DragBox.Topics, e.X, e.Y, canDrag, CurrentDragMethod);
            }
            else if (mouseMethod == ChartMouseMethod.Scroll)
            {
                if(MouseMethod == mouseMethod)
                    MouseState = ChartMouseState.Scroll;

                if (e.Button == MouseButtons.Left)
                {
                    Scroll(LastMousePos.X - e.X, LastMousePos.Y - e.Y);
                    LastMousePos.X = e.X;
                    LastMousePos.Y = e.Y;
                }
            }
            else if (IsMouseDown)
            {
                if (PressObject.IsEmpty) // select
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        MouseState = ChartMouseState.Select;
                    }
                }
                else if(MouseState != ChartMouseState.Drag)
                {
                    // test drag
                    if (Math.Abs(MouseDownPos.X - e.X) > SystemInformation.DragSize.Width
                    || Math.Abs(MouseDownPos.Y - e.Y) > SystemInformation.DragSize.Height)
                    {
                        DragBox.Start(MouseDownPos, SelectedTopics);
                        MouseState = ChartMouseState.Drag;
                    }
                }
            }

            if (!EditMode && MouseState == ChartMouseState.Normal)
            {
                HoverObject = HitTest(e.X, e.Y);
            }

            _ResetCursor();
        }

        protected override void OnChartMouseWheel(MouseEventArgs e)
        {
            base.OnChartMouseWheel(e);

            if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
            {
                if (e.Delta > 0)
                    ZoomIn();
                else if(e.Delta < 0)
                    ZoomOut();
            }
        }

        protected override void OnChartDoubleClick(EventArgs e)
        {
            base.OnChartDoubleClick(e);
            
            //
            if (EditMode)
            {
                EndEdit(!ReadOnly);
            }
            else
            {
                
                var hr = HitTest(ChartMouseDownPoint.X, ChartMouseDownPoint.Y);
                if (!hr.IsEmpty && !hr.IsFoldingButton&& ChartMouseDownButton == MouseButtons.Left)
                {
                    if (hr.Widget != null)
                    {
                        var he = new HandledEventArgs();
                        //if (ChartMouseDownButton == MouseButtons.Left)
                        hr.Widget.OnDoubleClick(he);

                        if (!he.Handled)
                        {
                            EditWidget(hr.Widget);
                        }
                    }
                    else if (hr.IsRemark)
                    {
                        ShowRemarkDialog(hr.Topic);
                    }
                    else
                    {
                        EditObject(hr.Topic);
                    }
                }
            }
        }

        void ShowRemarkDialog(Topic topic)
        {
            var dialog = new NoteWidgetDialog();
            dialog.ReadOnly = this.ReadOnly;
            dialog.Remark = topic.Remark;
            if (dialog.ShowDialog(this) == DialogResult.OK && !ReadOnly)
            {
                topic.Remark = dialog.Remark;
            }
        }

        protected override void OnChartMouseLeave(EventArgs e)
        {
            base.OnChartMouseLeave(e);

            HoverObject = HitTestResult.Empty;
        }

        protected override void OnMouseMethodChanged()
        {
            base.OnMouseMethodChanged();

            switch (MouseMethod)
            {
                case ChartMouseMethod.Scroll:
                    MouseState = ChartMouseState.Scroll;
                    break;
                case ChartMouseMethod.Select:
                    MouseState = ChartMouseState.Select;
                    break;
            }
        }

        public Topic GetTopicAt(int x, int y)
        {
            HitTestResult htr = HitTest(x, y);
            if (htr.IsEmpty)
                return null;
            else
                return htr.Topic;
        }

        protected override ChartObject[] GetAllObjects(bool onlyVisible)
        {
            return Map.GetAllObjects(onlyVisible);
            //return base.GetAllObjects(onlyVisible);
        }

        public Topic[] GetTopicsInRect(Rectangle rect)
        {
            rect.X -= ChartBox.Margin.Left;
            rect.Y -= ChartBox.Margin.Top;

            rect.X -= TranslatePoint.X;
            rect.Y -= TranslatePoint.Y;

            //
            if (HorizontalScroll.Enabled)
                rect.X += HorizontalScroll.Value;

            if (VerticalScroll.Enabled)
                rect.Y += VerticalScroll.Value;

            rect = PaintHelper.DeZoom(rect, Zoom);

            //
            List<Topic> topics = new List<Topic>();
            if (Map != null && Map.Root != null)
            {
                GetTopicsInRect(Map.Root, rect, topics);
            }

            return topics.ToArray();
        }

        void GetTopicsInRect(Topic topic, Rectangle selectionBox, List<Topic> topics)
        {
            if (selectionBox.IntersectsWith(topic.Bounds))
            {
                topics.Add(topic);
            }

            if (!topic.Folded && topic.HasChildren)
            {
                foreach (Topic subTopic in topic.Children)
                    GetTopicsInRect(subTopic, selectionBox, topics);
            }                
        }

        HitTestResult HitTest(int x, int y)
        {
            if (Map != null)
            {
                Point pt = PointToLogic(x, y);
                return HitTest(Map.Root, pt.X, pt.Y);
            }
            else
            {
                return HitTestResult.Empty;
            }
        }

        HitTestResult HitTest(Topic topic, int x, int y)
        {
            if (topic.FoldingButtonVisible && topic.FoldingButton.Contains(x, y))
            {
                return new HitTestResult(topic, null, true, false);
            }
            else if (topic.Bounds.Contains(x, y))
            {
                foreach (Widget widget in topic.Widgets)
                {
                    if (/*widget.Visible &&*/ widget.Bounds.Contains(x - topic.Bounds.X, y - topic.Bounds.Y))
                        return new HitTestResult(topic, widget, false, false);
                }

                return new HitTestResult(topic, null, false, false);
            }
            else if (topic.HaveRemark && Options.Current.GetBool(C2.Configuration.OptionNames.Charts.ShowRemarkIcon))
            {
                int x1 = x - topic.Bounds.Left;
                int y1 = y - topic.Bounds.Top;
                if (topic.RemarkIconBounds.Contains(x1, y1))
                {
                    return new HitTestResult(topic, null, false, true);
                }
            }

            if (!topic.Folded)
            {
                foreach (Topic subTopic in topic.Children)
                {
                    HitTestResult tp = HitTest(subTopic, x, y);
                    if (!tp.IsEmpty)
                    {
                        return tp;
                    }
                }
            }

            return HitTestResult.Empty;
        }

        Size GetTopicSize(Topic topic)
        {
            if (topic == null)
                throw new ArgumentNullException();

            Size size = topic.Size;
            size.Width = (int)Math.Ceiling(size.Width * Zoom);
            size.Height = (int)Math.Ceiling(size.Height * Zoom);

            return size;
        }

        Rectangle GetObjectRect(ITextObject tobj)
        {
            if (tobj == null)
                throw new ArgumentNullException();

            Rectangle rect = PaintHelper.Zoom(tobj.Bounds, Zoom);

            if (HorizontalScroll.Enabled)
                rect.X -= HorizontalScroll.Value;

            if (VerticalScroll.Enabled)
                rect.Y -= VerticalScroll.Value;

            //rect.X += Margin.Left;
            //rect.Y += Margin.Top;

            rect.X += TranslatePoint.X;
            rect.Y += TranslatePoint.Y;

            return rect;
        }

        //public void ShowToolTip(IColorToolTip tootipObject)
        //{
        //    ShowToolTip(tootipObject.ToolTip
        //        , tootipObject.ToolTipHyperlinks
        //        , tootipObject.ToolTipShowAlway);
        //}
       
        private void ShowToolTip(ChartObject chartObject)
        {
            if (chartObject is C2BaseWidget)
                ShowC2BaseWidgetTip(HoverObject.Topic, chartObject as C2BaseWidget); // 显示C2的挂件信息,图标挂件回头再加
            else
                ChartTip.Global.Show(this, chartObject);
        }

        private void ShowC2BaseWidgetTip(Topic topic, C2BaseWidget widget)
        {
            Point tipLoc = topic.Location;    // 确定提示框位置
            tipLoc.Offset(widget.Location);
            C2WidgetTip.Show(widget.Description, Global.GetDocumentForm(), PointToReal(tipLoc), 1750);
        }

        public void ShowToolTip(string text, string hyperlink, bool alwayVisible)
        {
            const int MaxLen = 300;

            if (!string.IsNullOrEmpty(text) && text.Length > MaxLen)
                text = text.Substring(0, MaxLen) + "...";

            if (string.IsNullOrEmpty(hyperlink))
                ShowToolTip(text);
            else
                ShowToolTip(text, new string[] { hyperlink });
            /*if (string.IsNullOrEmpty(text) && string.IsNullOrEmpty(hyperlink))
            {
                if (LastToolTip != null)
                    LastToolTip.Hide(true);
            }
            else
            {
                if (LastToolTip == null)
                    LastToolTip = ToolTipLayer.ShowToolTip(10, 10, text);
                else
                    LastToolTip.Text = text;

                if (LastToolTip is ChartHyperlinkToolTip)
                {
                    ((ChartHyperlinkToolTip)LastToolTip).Hyperlinks = new string[] { hyperlink };
                }

                LastToolTip.ShowAlways = alwayVisible;
                LastToolTip.Show();
                LastToolTip.Location = LastToolTip.CalculateBetterLocation(ChartBox.ClientSize, ChartBox.PointToClient(MousePosition));
            }*/
        }

        public void HideToolTip()
        {
            //if (LastToolTip != null)
            //    LastToolTip.Hide(true);
            ChartTip.Global.Hide();
        }

        void _ResetCursor()
        {
            ChartMouseMethod mouseMethod = MouseMethod;
            if (Helper.TestModifierKeys(Keys.Shift))
            {
                if (mouseMethod == ChartMouseMethod.Select)
                    mouseMethod = ChartMouseMethod.Scroll;
            }

            Cursor cur;
            if (!FormatPainter.Default.IsEmpty)
                cur = StyleBrushCursor;
            else if (mouseMethod == ChartMouseMethod.Scroll)
                cur = ScrollCursor;
            else
            {
                if (HoverObject.Widget != null && HoverObject.Widget.Cursor != null)
                    cur = HoverObject.Widget.Cursor;
                else
                    cur = Cursors.Default;
            }

            if (cur != Cursor)
                Cursor = cur;
        }

        #region drag

        //bool TestDrop(Topic topic, Topic target)
        //{
        //    if (ReadOnly || topic == null || target == null || topic == target || topic.ParentTopic == target)
        //        return false;

        //    if (topic.IsDescent(target))
        //        return false;

        //    return true;
        //}

        bool TestDragDrop(Topic[] topics, Topic target, DragTopicsMethod dragMethod)
        {
            if (ReadOnly || topics.IsNullOrEmpty() || target == null)
                return false;

            if (topics.Exists(t => t == target || t.ParentTopic == target || t.IsDescent(target)))
                return false;

            return true;
        }

        //protected void DropTo(Topic topic, Topic target)
        //{
        //    if (topic != null && target != null)
        //    {
        //        topic.ParentTopic = target;
        //    }
        //}

        void CancelDrag()
        {
            if (MouseState == ChartMouseState.Drag)
            {
                HideDragBox();
                MouseState = ChartMouseState.Normal;
                PressObject = HitTestResult.Empty;
                Cursor = Cursors.Default;
            }
        }

        void HideDragBox()
        {
            if (DragBox.Visible)
            {
                if (!DragBox.Bounds.IsEmpty)
                {
                    InvalidateChart(DragBox.Bounds);
                }
                DragBox.ClearTopics();
                DragBox.Visible = false;
            }
        }

        void ShowDragBox(Topic[] topics, int x, int y, bool canDrag, DragTopicsMethod dragMethod)
        {
            if (DragBox.Visible && !DragBox.Bounds.IsEmpty)
                InvalidateChart(DragBox.Bounds, true);

            if (topics.IsNullOrEmpty())
            {
                DragBox.ClearTopics();
                DragBox.Visible = false;
            }
            else
            {
                DragBox.Refresh(x, y, canDrag, dragMethod, ClientSize);
                DragBox.Visible = true;

                if (!DragBox.Bounds.IsEmpty)
                    InvalidateChart(DragBox.Bounds, true);
            }
        }

        //void ShowDragBox(Topic topic, int x, int y, bool canDrop)
        //{
        //    if (DragBox.Visible && !DragBox.Bounds.IsEmpty)
        //        InvalidateChart(DragBox.Bounds, true);

        //    if (topic == null)
        //    {
        //        DragBox.Visible = false;
        //        DragBox.Topic = null;
        //    }
        //    else
        //    {
        //        Rectangle rect = topic.Bounds;
        //        rect.Location = new Point(Math.Max(0, Math.Min(ClientSize.Width - rect.Width, x - rect.Width / 2)),
        //            Math.Max(0, Math.Min(ClientSize.Height - rect.Height, y - rect.Height / 2)));
        //        rect.Inflate(10, 10);
        //        DragBox.Bounds = rect;
        //        DragBox.Visible = true;
        //        DragBox.Topic = topic;
        //        DragBox.CanDragDrop = canDrop;

        //        if (!DragBox.Bounds.IsEmpty)
        //            InvalidateChart(DragBox.Bounds, true);
        //    }
        //}

        #endregion

        #region sub class

        internal struct HitTestResult
        {
            public Topic Topic;
            public bool IsFoldingButton;
            public Widget Widget;
            public bool IsRemark;

            public static readonly HitTestResult Empty = new HitTestResult();

            public HitTestResult(Topic topic, Widget widget, bool isFoldingButton, bool isRemark)
            {
                Topic = topic;
                IsFoldingButton = isFoldingButton;
                Widget = widget;
                IsRemark = isRemark;
            }

            public bool IsEmpty
            {
                get { return Topic == null;}
            }

            public static bool operator !=(HitTestResult htr1, HitTestResult htr2)
            {
                return htr1.Topic != htr2.Topic 
                    || htr1.IsFoldingButton != htr2.IsFoldingButton
                    || htr1.Widget != htr2.Widget
                    || htr1.IsRemark != htr2.IsRemark;
            }

            public static bool operator ==(HitTestResult htr1, HitTestResult htr2)
            {
                return htr1.Topic == htr2.Topic 
                    && htr1.IsFoldingButton == htr2.IsFoldingButton
                    && htr1.Widget == htr2.Widget
                    && htr1.IsRemark == htr2.IsRemark;
            }

            public override bool Equals(object obj)
            {
                if (!(obj is HitTestResult))
                    return false;
                else
                    return this == (HitTestResult)obj;
            }

            public override int GetHashCode()
            {
                if (Topic != null)
                    return Topic.GetHashCode();
                else
                    return 0;
            }
        }

        #endregion

        #region IDropFilesHandler
        ContextMenuStrip MenuDropFiles;
        ToolStripMenuItem menuDropFiles_AsSubTopics;
        ToolStripMenuItem menuDropFiles_AsHyperlink;
        ToolStripMenuItem menuDropFiles_AsRemark;
        Topic DropFilesTopic;
        string[] DropFileNames;

        void InitializeDropFilesMenu()
        {
            MenuDropFiles = new ContextMenuStrip();
            menuDropFiles_AsSubTopics = new ToolStripMenuItem();
            menuDropFiles_AsHyperlink = new ToolStripMenuItem();
            menuDropFiles_AsRemark = new ToolStripMenuItem();

            MenuDropFiles.Renderer = UITheme.Default.ToolStripRenderer;
            MenuDropFiles.Opening += MenuDropFiles_Opening;
            MenuDropFiles.Items.AddRange(new ToolStripItem[]{
                menuDropFiles_AsSubTopics,
                menuDropFiles_AsHyperlink,
                menuDropFiles_AsRemark,
            });

            menuDropFiles_AsSubTopics.Text = "As SubTopic";
            menuDropFiles_AsSubTopics.Click += menuDropFiles_AsSubTopics_Click;

            menuDropFiles_AsHyperlink.Text = "As Hyperlink";
            menuDropFiles_AsHyperlink.Click += menuDropFiles_AsHyperlink_Click;

            menuDropFiles_AsRemark.Text = "As Remark";
            menuDropFiles_AsRemark.Click += menuDropFiles_AsRemark_Click;
        }

        void MenuDropFiles_Opening(object sender, CancelEventArgs e)
        {
            if (DropFilesTopic != null && !DropFileNames.IsNullOrEmpty())
            {
                menuDropFiles_AsSubTopics.Available = true;
                menuDropFiles_AsHyperlink.Available = DropFileNames.Length == 1;
                menuDropFiles_AsRemark.Available = true;
            }
            else
            {
                menuDropFiles_AsSubTopics.Available = false;
                menuDropFiles_AsHyperlink.Available = false;
                menuDropFiles_AsRemark.Available = false;
            }
        }

        void menuDropFiles_AsRemark_Click(object sender, EventArgs e)
        {
            if (DropFilesTopic == null || DropFileNames.IsNullOrEmpty())
                return;

            var sb = new StringBuilder();
            sb.AppendLine("<ul>");
            foreach (var fn in DropFileNames)
            {
                sb.AppendFormat("<li><a href='{0}' target='_blank'>{1}</a></li>", fn, Path.GetFileName(fn));
                sb.AppendLine();
            }
            sb.AppendLine("</ul>");

            DropFilesTopic.Remark += sb.ToString();
        }

        void menuDropFiles_AsHyperlink_Click(object sender, EventArgs e)
        {
            if (DropFilesTopic == null || DropFileNames.IsNullOrEmpty())
                return;

            DropFilesTopic.Hyperlink = DropFileNames[0];
        }

        void menuDropFiles_AsSubTopics_Click(object sender, EventArgs e)
        {
            if (DropFilesTopic == null || DropFileNames.IsNullOrEmpty())
                return;

            var list = new List<Topic>();
            foreach (var fn in DropFileNames)
            {
                var t = new Topic(Path.GetFileName(fn));
                t.Hyperlink = fn;
                list.Add(t);
            }

            AddTopics(DropFilesTopic, list.ToArray(), -1);
        }
        
        public void OnFilesDrop(DropFilesEventArgs e)
        {
            if (ReadOnly)
                return;

            var topic = this.GetTopicAt(e.MousePosition.X, e.MousePosition.Y);
            if (topic != null)
            {
                if (MenuDropFiles == null)
                {
                    InitializeDropFilesMenu();
                }

                DropFilesTopic = topic;
                DropFileNames = e.FileNames;
                MenuDropFiles.Renderer = UITheme.Default.ToolStripRenderer;
                MenuDropFiles.Show(this, e.MousePosition, ToolStripDropDownDirection.BelowRight);
                e.Handled = true;
            }
        }

        #endregion
    }
}
