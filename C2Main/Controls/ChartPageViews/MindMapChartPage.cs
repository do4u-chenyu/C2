using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using C2.Controls;
using C2.Controls.MapViews;
using C2.Core;
using C2.Design;
using C2.Dialogs;
using C2.Globalization;
using C2.Model;
using C2.Model.Documents;
using C2.Model.MindMaps;
using C2.Model.Styles;
using C2.Core.Exports;

namespace C2.ChartPageView
{
    class MindMapChartPage : BaseChartPage, IThemableUI
    {
        MindMapView mindMapView1;

        public MindMapChartPage()
        {
            InitializeComponent();

            UITheme.Default.Listeners.Add(this);
            ApplyTheme(UITheme.Default);
        }

        [Browsable(false)]
        public MindMap MindMapChart
        {
            get { return mindMapView1.Map; }
        }

        [Browsable(false)]
        public override ChartControl ChartBox
        {
            get
            {
                return mindMapView1;
            }
        }

        void InitializeComponent()
        {
            mindMapView1 = new C2.Controls.MapViews.MindMapView();
            SuspendLayout();

            // mindMapView1
            mindMapView1.Dock = DockStyle.Fill;
            mindMapView1.Name = "mindMapView1";
            mindMapView1.ShowBorder = false;
            mindMapView1.SelectionChanged += new System.EventHandler(this.mindMapView1_SelectionChanged);
            mindMapView1.ChartBackColorChanged += new System.EventHandler(this.mindMapView1_ChartBackColorChanged);
            mindMapView1.TopicDataChanged += new System.EventHandler(this.mindMapView1_TopicDataChanged);
            mindMapView1.NeedShowDesigner += mindMapView1_NeedShowDesigner;

            // MindMapChartPage
            Controls.Add(this.mindMapView1);
            Name = "MindMapChartPage";
            ResumeLayout(false);
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            if (!this.DesignMode)
                BackColor = PaintHelper.WithoutAlpha(mindMapView1.ChartBackColor);
        }

        protected override void OnChartChanged()
        {
            base.OnChartChanged();

            if (Chart is MindMap)
            {
                MindMap chart = (MindMap)Chart;
                mindMapView1.Map = chart;
                chart.NameChanged += new EventHandler(chart_NameChanged);
                chart.LayoutTypeChanged += new EventHandler(chart_LayoutTypeChanged);
            }
            else
            {
                throw new ArgumentNullException();
            }

            Icon = GetIconByLayoutType();
        }

        void chart_LayoutTypeChanged(object sender, EventArgs e)
        {
            Icon = GetIconByLayoutType();
        }

        Image GetIconByLayoutType()
        {
            if (MindMapChart == null)
                return null;

            return MindMapLayoutTypeEditor.GetIcon(MindMapChart.LayoutType);
        }

        void chart_NameChanged(object sender, EventArgs e)
        {
            if(Chart != null)
                Text = Chart.Name;
        }

        void mindMapView1_ChartBackColorChanged(object sender, EventArgs e)
        {
            BackColor = PaintHelper.WithoutAlpha(mindMapView1.ChartBackColor);
        }

        protected override void OnSelectedObjectsChanged()
        {
            //if (SelectedObjects != null && SelectedObjects.Length == 1 && SelectedObjects[0] is Topic)
            //{
            //    // mapView1.Select((Topic)SelectedObjects);
            //    TsbFormatPainter.Enabled = true;
            //}
            //else
            //{
            //    TsbFormatPainter.Enabled = TsbFormatPainter.Checked;
            //}

            ShowProperty(SelectedObjects);

            ResetControlStatus();

            base.OnSelectedObjectsChanged();
        }

        void ShowProperty(object[] SelectedObjects)
        {
            //throw new NotImplementedException();
        }

        protected override void ResetControlStatus()
        {
            base.ResetControlStatus();
        }

        void mindMapView1_SelectionChanged(object sender, EventArgs e)
        {
            var so = mindMapView1.SelectedObjects;
            if (so == null || so.Length == 0)
                SelectedObjects = new object[] { MindMapChart };
            else
                SelectedObjects = so;
        }

        void mindMapView1_NeedShowDesigner(bool needShow)
        {
            var so = mindMapView1.ShowDesignerObject;
            if (so != null)
            {
                NeedShowControl = needShow;
                ShowDesignerObject = so;
            }
        }
        void mindMapView1_TopicDataChanged(object sender, EventArgs e)
        {
            var so = mindMapView1.DataChangeObject;
            var item = mindMapView1.DataChangeItem;
            DataChangeItem = item;
            if (so != null)
                DataChangeObject = so;
            
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (MindMapChart != null && MindMapChart.Root != null)
            {
                mindMapView1.EnsureVisible(MindMapChart.Root);
            }
        }

        protected override void OnKeyMapChanged()
        {
            MenuAddTopic.ShortcutKeyDisplayString = KeyMap.AddTopic.ToString();
            MenuAddSubTopic.ShortcutKeyDisplayString = KeyMap.AddSubTopic.ToString();

            MenuCollapseFolding.ShortcutKeyDisplayString = KeyMap.Collapse.ToString();
            MenuExpandFolding.ShortcutKeyDisplayString = KeyMap.Expand.ToString();
            MenuToggleFolding.ShortcutKeyDisplayString = KeyMap.ToggleFolding.ToString();
            MenuCollapseAll.ShortcutKeyDisplayString = KeyMap.CollapseAll.ToString();
            MenuExpandAll.ShortcutKeyDisplayString = KeyMap.ExpandAll.ToString();

            MenuCut.ShortcutKeyDisplayString = KeyMap.Cut.ToString();
            MenuCopy.ShortcutKeyDisplayString = KeyMap.Copy.ToString();
            MenuPaste.ShortcutKeyDisplayString = KeyMap.Paste.ToString();
            MenuDelete.ShortcutKeyDisplayString = KeyMap.Delete.ToString();

            MenuEdit.ShortcutKeyDisplayString = KeyMap.Edit.ToString();
        }

        protected override void OnCurrentLanguageChanged()
        {
            if (!Created)
                return;

            MenuOpenHyperlink.Text = Lang._("Open Hyperlink");
            MenuAddTopic.Text = Lang._("Add Topic");
            MenuAddSubTopic.Text = Lang._("Add Sub Topic");
            MenuAdd.Text = Lang._("Add");
            MenuAddIcon.Text = Lang._("Icon");
            MenuAddRemark.Text = Lang._("Notes");
            MenuAddProgressBar.Text = Lang._("Progress Bar");
            MenuAddOperator.Text = Lang._("Operator");
            MenuAddAttachment.Text = Lang._("AddAttachment");
            MenuAddMap.Text = Lang._("AddMap");
            MenuAddBoss.Text = Lang._("AddBoss");
            MenuAddMaxOp.Text = Lang._("Max");
            MenuAddAIOp.Text = Lang._("AI");
            MenuAddModelOp.Text = Lang._("Model");
            MenuFolding.Text = Lang._("Folding");
            MenuCollapseFolding.Text = Lang._("Collapse");
            MenuExpandFolding.Text = Lang._("Expand");
            MenuToggleFolding.Text = Lang._("Toggle Folding");
            MenuCollapseAll.Text = Lang._("Collapse All");
            MenuExpandAll.Text = Lang._("Expand All");
            MenuCut.Text = Lang._("Cut");
            MenuCopy.Text = Lang._("Copy");
            MenuPaste.Text = Lang._("Paste");
            MenuDelete.Text = Lang._("Delete");
            MenuEdit.Text = Lang._("Edit");
            MenuProperty.Text = Lang._("Property");
            MenuLink.Text = Lang._("Link");
            MenuStraightening.Text = Lang._("Straightening");
            MenuInvert.Text = Lang._("Invert");
            MenuAdvance.Text = Lang._("Advance");
            MenuExportDocx.Text = Lang._("Export Docx");
            MenuExportXmind.Text = Lang._("Export Xmind");

            if (MenuLayout != null)
            {
                MenuLayout.Dispose();
                MenuLayout = null;
            }
        }

        #region menus
        ToolStripMenuItem MenuOpenHyperlink;
        ToolStripSeparator toolStripSeparator15;
        ToolStripMenuItem MenuAddTopic;
        ToolStripMenuItem MenuAddSubTopic;
        ToolStripMenuItem MenuAdd;
        ToolStripMenuItem MenuAddIcon;
        ToolStripMenuItem MenuAddRemark;
        ToolStripMenuItem MenuAddProgressBar;
        ToolStripMenuItem MenuAddOperator;
        ToolStripMenuItem MenuAddAttachment;
        ToolStripMenuItem MenuAddMap;
        ToolStripMenuItem MenuAddBoss;
        ToolStripMenuItem MenuAddMaxOp;
        ToolStripMenuItem MenuAddAIOp;
        ToolStripMenuItem MenuAddModelOp;
        ToolStripSeparator toolStripSeparator5;
        ToolStripMenuItem MenuLink;
        ToolStripMenuItem MenuStraightening;
        ToolStripMenuItem MenuInvert;
        ToolStripMenuItem MenuFolding;
        ToolStripMenuItem MenuCollapseFolding;
        ToolStripMenuItem MenuExpandFolding;
        ToolStripMenuItem MenuToggleFolding;
        ToolStripSeparator toolStripSeparator9;
        ToolStripMenuItem MenuCollapseAll;
        ToolStripMenuItem MenuExpandAll;
        ToolStripSeparator toolStripMenuItem2;
        ToolStripMenuItem MenuCut;
        ToolStripMenuItem MenuCopy;
        ToolStripMenuItem MenuPaste;
        ToolStripMenuItem MenuDelete;
        ToolStripSeparator toolStripSeparator10;
        ToolStripMenuItem MenuEdit;
        ToolStripMenuItem MenuProperty;
        ToolStripMenuItem MenuAdvance;
        ToolStripMenuItem MenuExportDocx;
        ToolStripMenuItem MenuExportXmind;

        protected override int ExtendActionMenuIndex
        {
            get
            {
                return ChartContextMenuStrip.Items.IndexOf(toolStripSeparator10);
            }
        }

        protected override void InitializationChartContextMenuStrip(ContextMenuStrip contextMenu)
        {
            base.InitializationChartContextMenuStrip(contextMenu);
            contextMenu.SuspendLayout();

            //
            MenuOpenHyperlink = new ToolStripMenuItem();
            toolStripSeparator15 = new ToolStripSeparator();
            MenuAddTopic = new ToolStripMenuItem();
            MenuAddSubTopic = new ToolStripMenuItem();
            MenuAdd = new ToolStripMenuItem();
            MenuAddIcon = new ToolStripMenuItem();
            MenuAddRemark = new ToolStripMenuItem();
            MenuAddProgressBar = new ToolStripMenuItem();
            MenuAddOperator = new ToolStripMenuItem();
            MenuAddAttachment = new ToolStripMenuItem();
            MenuAddMap = new ToolStripMenuItem();
            MenuAddBoss = new ToolStripMenuItem();
            MenuAddMaxOp = new ToolStripMenuItem();
            MenuAddAIOp = new ToolStripMenuItem();
            MenuAddModelOp = new ToolStripMenuItem();
            toolStripSeparator5 = new ToolStripSeparator();
            MenuLink = new ToolStripMenuItem();
            MenuStraightening = new ToolStripMenuItem();
            MenuInvert = new ToolStripMenuItem();
            MenuFolding = new ToolStripMenuItem();
            MenuCollapseFolding = new ToolStripMenuItem();
            MenuExpandFolding = new ToolStripMenuItem();
            MenuToggleFolding = new ToolStripMenuItem();
            toolStripSeparator9 = new ToolStripSeparator();
            MenuCollapseAll = new ToolStripMenuItem();
            MenuExpandAll = new ToolStripMenuItem();
            toolStripMenuItem2 = new ToolStripSeparator();
            MenuCut = new ToolStripMenuItem();
            MenuCopy = new ToolStripMenuItem();
            MenuPaste = new ToolStripMenuItem();
            MenuDelete = new ToolStripMenuItem();
            toolStripSeparator10 = new ToolStripSeparator();
            MenuAdvance = new ToolStripMenuItem();
            MenuEdit = new ToolStripMenuItem();
            MenuProperty = new ToolStripMenuItem();
            MenuExportDocx = new ToolStripMenuItem();
            MenuExportXmind = new ToolStripMenuItem();

            //
            contextMenu.Items.AddRange(new ToolStripItem[] {
                MenuOpenHyperlink,
                toolStripSeparator15,
                MenuAddTopic,
                MenuAddSubTopic,
                MenuAdd,
                MenuAdvance,
                toolStripSeparator5,
                //MenuLink,
                MenuFolding,
                //MenuAdvance,
                toolStripMenuItem2,
                MenuCut,
                MenuCopy,
                MenuPaste,
                MenuDelete,
                toolStripSeparator10,
                MenuEdit,
                MenuProperty});

            // MenuOpenHyperlink
            MenuOpenHyperlink.Image = C2.Properties.Resources.hyperlink;
            MenuOpenHyperlink.Name = "MenuOpenHyperlink";
            MenuOpenHyperlink.Text = "&Open Hyperlink";
            MenuOpenHyperlink.Click += new System.EventHandler(MenuOpenHyperlink_Click);

            // toolStripSeparator15
            toolStripSeparator15.Name = "toolStripSeparator15";

            // MenuAddTopic
            MenuAddTopic.Image = C2.Properties.Resources.add_topic;
            MenuAddTopic.Name = "MenuAddTopic";
            MenuAddTopic.ShortcutKeyDisplayString = "Enter";
            MenuAddTopic.Text = "Add Topic";
            MenuAddTopic.Click += new System.EventHandler(MenuAddTopic_Click);

            // MenuAddSubTopic
            MenuAddSubTopic.Image = C2.Properties.Resources.add_sub_topic;
            MenuAddSubTopic.Name = "MenuAddSubTopic";
            MenuAddSubTopic.ShortcutKeyDisplayString = "Tab/Insert";
            MenuAddSubTopic.Text = "Add Sub Topic";
            MenuAddSubTopic.Click += new System.EventHandler(MenuAddSubTopic_Click);

            // MenuAdd
            MenuAdd.DropDownItems.AddRange(new ToolStripItem[] {
                MenuAddIcon,
                MenuAddRemark,
                MenuAddProgressBar,
                MenuAddOperator,
                MenuAddModelOp,
                MenuAddAttachment,
                MenuAddMap,
                MenuAddBoss});
            MenuAdd.Name = "MenuAdd";
            MenuAdd.Text = "Add";


            // MenuAddIcon
            MenuAddIcon.Image = Properties.Resources.image;
            MenuAddIcon.Name = "MenuAddIcon";
            MenuAddIcon.Text = "&Icon";
            MenuAddIcon.Click += new System.EventHandler(MenuAddIcon_Click);

            // MenuAddRemark
            MenuAddRemark.Image = Properties.Resources.备注;
            MenuAddRemark.Name = "MenuAddRemark";
            MenuAddRemark.Text = "&Notes";
            MenuAddRemark.Click += new System.EventHandler(MenuAddRemark_Click);

            // MenuAddProgressBar
            MenuAddProgressBar.Image = Properties.Resources.progress_bar;
            MenuAddProgressBar.Name = "MenuAddProgressBar";
            MenuAddProgressBar.Text = "&Progress Bar";
            MenuAddProgressBar.Click += new System.EventHandler(MenuAddProgressBar_Click);

            //MenuAddOperator
            MenuAddOperator.Image = C2.Properties.Resources.算子;
            MenuAddOperator.Name = "MenuAddOperator";
            MenuAddOperator.Text = "Operator";
            MenuAddOperator.Click += new System.EventHandler(MenuAddOperator_Click);

            //MenuAddAttachment
            MenuAddAttachment.Image = C2.Properties.Resources.附件;
            MenuAddAttachment.Name = "MenuAddAttachment";
            MenuAddAttachment.Text = "Attachment";
            MenuAddAttachment.Click += new System.EventHandler(MenuAddAttachment_Click);

            MenuAddModelOp.Image = C2.Properties.Resources.模型视图;
            MenuAddModelOp.Name = "MenuAddModelOp";
            MenuAddModelOp.Text = "Model";
            MenuAddModelOp.Click += new System.EventHandler(MenuAddModelOp_Click);

            //MenuAddMap
            MenuAddMap.Image = C2.Properties.Resources.地图;
            MenuAddMap.Name = "MenuAddMap";
            MenuAddMap.Text = "Map";
            MenuAddMap.Click += new System.EventHandler(MenuAddMap_Click);

            //MenuAddBoss
            MenuAddBoss.Image = C2.Properties.Resources.大屏;
            MenuAddBoss.Name = "MenuAddBoss";
            MenuAddBoss.Text = "Boss";
            MenuAddBoss.Click += new System.EventHandler(MenuAddBoss_Click);

            // toolStripSeparator5
            toolStripSeparator5.Name = "toolStripSeparator5";

            // MenuLink
            MenuLink.DropDownItems.AddRange(new ToolStripItem[] {
                MenuStraightening,
                MenuInvert});
            MenuLink.Name = "MenuLink";
            MenuLink.Text = "Link";

            // MenuStraightening
            MenuStraightening.Name = "MenuStraightening";
            MenuStraightening.Text = "Straightening";
            MenuStraightening.Click += new System.EventHandler(MenuStraightening_Click);

            // MenuInvert
            MenuInvert.Name = "MenuInvert";
            MenuInvert.Text = "Invert";
            MenuInvert.Click += new System.EventHandler(MenuInvert_Click);

            // MenuFolding
            MenuFolding.DropDownItems.AddRange(new ToolStripItem[] {
                MenuCollapseFolding,
                MenuExpandFolding,
                MenuToggleFolding,
                toolStripSeparator9,
                MenuCollapseAll,
                MenuExpandAll});
            MenuFolding.Name = "MenuFolding";
            MenuFolding.Text = "&Folding";

            // MenuCollapseFolding
            MenuCollapseFolding.Name = "MenuCollapseFolding";
            MenuCollapseFolding.ShortcutKeyDisplayString = "Shift+-";
            MenuCollapseFolding.Text = "Collapse";
            MenuCollapseFolding.Click += new System.EventHandler(MenuCollapseFolding_Click);

            // MenuExpandFolding
            MenuExpandFolding.Name = "MenuExpandFolding";
            MenuExpandFolding.ShortcutKeyDisplayString = "Shift++";
            MenuExpandFolding.Text = "Expand";
            MenuExpandFolding.Click += new System.EventHandler(MenuExpandFolding_Click);

            // MenuToggleFolding
            MenuToggleFolding.Name = "MenuToggleFolding";
            MenuToggleFolding.ShortcutKeyDisplayString = "Shift+*";
            MenuToggleFolding.Text = "Toggle Folding";
            MenuToggleFolding.Click += new System.EventHandler(MenuToggleFolding_Click);

            // toolStripSeparator9
            toolStripSeparator9.Name = "toolStripSeparator9";
            toolStripSeparator9.Size = new System.Drawing.Size(217, 6);

            // MenuCollapseAll
            MenuCollapseAll.Name = "MenuCollapseAll";
            MenuCollapseAll.ShortcutKeyDisplayString = "Ctrl+Shift+-";
            MenuCollapseAll.Text = "Collapse All";
            MenuCollapseAll.Click += new System.EventHandler(MenuCollapseAll_Click);

            // MenuExpandAll
            MenuExpandAll.Name = "MenuExpandAll";
            MenuExpandAll.ShortcutKeyDisplayString = "Ctrl+Shift++";
            MenuExpandAll.Text = "Expand All";
            MenuExpandAll.Click += new System.EventHandler(MenuExpandAll_Click);

            // toolStripMenuItem2
            toolStripMenuItem2.Name = "toolStripMenuItem2";

            // MenuCut
            MenuCut.Image = C2.Properties.Resources.cut;
            MenuCut.Name = "MenuCut";
            MenuCut.ShortcutKeyDisplayString = "Ctrl+X";
            MenuCut.Text = "Cu&t";
            MenuCut.Click += new System.EventHandler(MenuCut_Click);

            // MenuCopy
            MenuCopy.Image = C2.Properties.Resources.copy;
            MenuCopy.Name = "MenuCopy";
            MenuCopy.ShortcutKeyDisplayString = "Ctrl+C";
            MenuCopy.Text = "&Copy";
            MenuCopy.Click += new System.EventHandler(MenuCopy_Click);

            // MenuPaste
            MenuPaste.Image = C2.Properties.Resources.paste;
            MenuPaste.Name = "MenuPaste";
            MenuPaste.ShortcutKeyDisplayString = "Ctrl+V";
            MenuPaste.Text = "&Paste";
            MenuPaste.Click += new System.EventHandler(MenuPaste_Click);

            // MenuDelete
            MenuDelete.Image = C2.Properties.Resources.delete;
            MenuDelete.Name = "MenuDelete";
            MenuDelete.ShortcutKeyDisplayString = "Del";
            MenuDelete.Text = "&Delete";
            MenuDelete.Click += new System.EventHandler(MenuDelete_Click);

            // toolStripSeparator10
            toolStripSeparator10.Name = "toolStripSeparator10";

            // MenuAdvance
            MenuAdvance.Name = "MenuAdvance";
            MenuAdvance.Text = "Advance";
            MenuAdvance.DropDownItems.AddRange(new ToolStripItem[] { MenuExportDocx, MenuExportXmind });

            // MenuExportDocx
            MenuExportDocx.Image = C2.Properties.Resources.word;
            MenuExportDocx.Name = "MenuExportDocx";
            MenuExportDocx.Text = "Export Docx";
            MenuExportDocx.Click += MenuExportDocx_Click;

            // MenuExportXmind
            MenuExportXmind.Image = C2.Properties.Resources.xmind;
            MenuExportXmind.Name = "MenuExportXmind";
            MenuExportXmind.Text = "Export Xmind";
            MenuExportXmind.Click += MenuExportXmind_Click;

            // MenuEdit
            MenuEdit.Image = C2.Properties.Resources.edit;
            MenuEdit.Name = "MenuEdit";
            MenuEdit.ShortcutKeyDisplayString = "F2";
            MenuEdit.Text = "&Edit";
            MenuEdit.Click += new System.EventHandler(MenuEdit_Click);

            // MenuProperty
            MenuProperty.Image = C2.Properties.Resources.property;
            MenuProperty.Name = "MenuProperty";
            MenuProperty.Text = "&Property";
            MenuProperty.Click += new System.EventHandler(MenuProperty_Click);

            //
            contextMenu.ResumeLayout();
        }

        protected override void OnChartContextMenuStripOpening(CancelEventArgs e)
        {
            if (SelectedObjects != null && SelectedObjects.Length > 0)
            {
                int count = SelectedObjects.Length;
                Topic topic = mindMapView1.SelectedTopic;
                int topicCount = mindMapView1.SelectedTopics.Length;
                string chartName = Global.GetCurrentDocument().ActiveChart.Name;

                string urls = null;
                MenuOpenHyperlink.Enabled = HasAnyUrl(SelectedObjects, out urls);
                MenuOpenHyperlink.Available = MenuOpenHyperlink.Enabled;
                MenuOpenHyperlink.ToolTipText = urls;

                MenuAddTopic.Enabled = !ReadOnly && count == 1 && topicCount > 0 && !topic.IsRoot;
                MenuAddSubTopic.Enabled = !ReadOnly && count == 1 && topicCount > 0;
                MenuFolding.Available = topicCount > 0 && count == 1 && topic.HasChildren;
                MenuExpandFolding.Enabled = topicCount > 0 && count == 1 && topic.Folded && !topic.IsRoot;
                MenuCollapseFolding.Enabled = topicCount > 0 && count == 1 && !topic.Folded && !topic.IsRoot;
                MenuToggleFolding.Enabled = topicCount > 0 && count == 1 && !topic.IsRoot;
                MenuExpandAll.Enabled = topicCount > 0 && count == 1;
                MenuCollapseAll.Enabled = topicCount > 0 && count == 1;
                MenuAdd.Enabled = !ReadOnly && count == 1 && topicCount > 0;
                MenuAddOperator.Enabled = topicCount > 0 && count == 1 && string.Equals("业务拓展视图",chartName);
                MenuAddAttachment.Enabled = topicCount > 0 && count == 1;
                MenuAddMap.Enabled = topicCount > 0 && count == 1;
                MenuAddBoss.Enabled = topicCount > 0 && count == 1;
                MenuAddProgressBar.Enabled = topicCount > 0;
                MenuAddModelOp.Enabled = topicCount > 0 && count == 1 && string.Equals("业务拓展视图", chartName);

                bool hasLink = false;
                foreach (var mo in SelectedObjects)
                {
                    if (mo is Link)
                    {
                        hasLink = true;
                        break;
                    }
                }
                MenuLink.Available = hasLink;
            }
            else
            {
                MenuOpenHyperlink.Enabled = false;
                MenuOpenHyperlink.ToolTipText = null;
                MenuAddTopic.Enabled = false;
                MenuAddSubTopic.Enabled = false;
                MenuFolding.Available = false;
                MenuAdd.Enabled = false;
                MenuLink.Available = false;
                MenuExportDocx.Available = false;
                MenuExportXmind.Available = false;
            }

            MenuCut.Enabled = mindMapView1.CanCut;
            MenuCopy.Enabled = mindMapView1.CanCopy;
            MenuPaste.Enabled = mindMapView1.CanPaste;
            MenuDelete.Enabled = mindMapView1.CanDelete;
            MenuEdit.Enabled = mindMapView1.CanEdit;
            MenuAdvance.Available = MenuAdvance.HasAvailableItems();

            ChartContextMenuStrip.SmartHideSeparators();
        }

        public override IEnumerable<ExtendActionInfo> GetExtendActions()
        {
            var actions = new List<ExtendActionInfo>();

            //暂时隐去粘帖为备注、粘帖为图片功能
            //if (mindMapView1.CanPasteAsRemark && Clipboard.ContainsText())
            //{
            //    actions.Add(new ExtendActionInfo("Paste as Note", Properties.Resources.paste_as_remark, MenuPasteAsNote_Click));
            //}

            //if (mindMapView1.CanPaste && Clipboard.ContainsImage())
            //{
            //    actions.Add(new ExtendActionInfo("Paste as Image", Properties.Resources.paste_as_image, MenuPasteAsImage_Click));
            //}

            Topic topic = mindMapView1.SelectedTopic;
            if (topic != null && !ReadOnly)
            {

                if (topic.ParentTopic != null && topic.ParentTopic.Children.Count > 0)
                {
                    if (topic.Index > 0)
                        actions.Add(new ExtendActionInfo("Move Up", Properties.Resources.up, KeyMap.MoveUp.ToString(), MenuMoveUp_Click));

                    if (topic.Index < topic.ParentTopic.Children.Count - 1)
                        actions.Add(new ExtendActionInfo("Move Down", Properties.Resources.down, KeyMap.MoveDown.ToString(), MenuMoveDown_Click));
                }

                if (SelectedObjects.Length == 1 && topic.Children.Count > 1)
                {
                    actions.Add(new ExtendActionInfo(Lang.GetTextWithEllipsis("Custom Sort"), null, MenuCustomSort_Click));
                }
            }

            return actions;
        }

        bool HasAnyUrl(object[] objects, out string urls)
        {
            urls = null;

            foreach (object obj in objects)
            {
                if (obj is IHyperlink)
                {
                    IHyperlink link = (IHyperlink)obj;
                    if (!string.IsNullOrEmpty(link.Hyperlink))
                    {
                        if (urls == null)
                            urls = link.Hyperlink;
                        else
                            urls += "\n" + link.Hyperlink;
                    }
                }
            }

            return urls != null;
        }

        void MenuOpenHyperlink_Click(object sender, EventArgs e)
        {
            mindMapView1.OpenSelectedUrl();
        }

        void MenuAddTopic_Click(object sender, EventArgs e)
        {
            mindMapView1.AddTopic();
        }

        void MenuAddSubTopic_Click(object sender, EventArgs e)
        {
            mindMapView1.AddSubTopic();
        }

        void MenuAddIcon_Click(object sender, EventArgs e)
        {
            mindMapView1.AddIcon();
        }

        void MenuAddRemark_Click(object sender, EventArgs e)
        {
            mindMapView1.AddRemark();
        }
        void MenuAddProgressBar_Click(object sender, EventArgs e)
        {
            mindMapView1.AddProgressBar();
        }
        void MenuAddOperator_Click(object sender, EventArgs e)
        {
            ShowDesignerObject = mindMapView1.SelectedTopic;
            mindMapView1.AddOperator();
        }
        void MenuAddAttachment_Click(object sender, EventArgs e)
        {
            mindMapView1.AddAttachment();
        }

        void MenuAddModelOp_Click(object sender, EventArgs e)
        {
            mindMapView1.AddModelOp();
        }
        void MenuAddMap_Click(object sender, EventArgs e)
        {
            mindMapView1.AddMap();
        }
        void MenuAddBoss_Click(object sender, EventArgs e)
        {
            mindMapView1.AddBoss();
        }
        
        void MenuStraightening_Click(object sender, EventArgs e)
        {
            foreach (ChartObject mo in SelectedObjects)
            {
                if (mo is Link)
                {
                    mindMapView1.ExecuteCommand(new StraighteningCommand((Link)mo));
                }
            }
        }

        void MenuInvert_Click(object sender, EventArgs e)
        {
            foreach (ChartObject mo in SelectedObjects)
            {
                if (mo is Link)
                {
                    mindMapView1.ExecuteCommand(new InvertLinkCommand((Link)mo));
                }
            }
        }

        void MenuCollapseFolding_Click(object sender, EventArgs e)
        {
            Topic topic = mindMapView1.SelectedTopic;
            if (topic != null)
            {
                topic.Collapse();
            }
        }

        void MenuExpandFolding_Click(object sender, EventArgs e)
        {
            Topic topic = mindMapView1.SelectedTopic;
            if (topic != null)
            {
                topic.Expand();
            }
        }

        void MenuToggleFolding_Click(object sender, EventArgs e)
        {
            Topic topic = mindMapView1.SelectedTopic;
            if (topic != null)
            {
                topic.Toggle();
            }
        }

        void MenuCollapseAll_Click(object sender, EventArgs e)
        {
            mindMapView1.CollapseAll();
        }

        void MenuExpandAll_Click(object sender, EventArgs e)
        {
            mindMapView1.ExpandAll();
        }

        void MenuCut_Click(object sender, EventArgs e)
        {
            if (mindMapView1.CanCut)
            {
                mindMapView1.Cut();
            }
        }

        void MenuCopy_Click(object sender, EventArgs e)
        {
            if (mindMapView1.CanCopy)
            {
                mindMapView1.Copy();
            }
        }

        void MenuPaste_Click(object sender, EventArgs e)
        {
            if (mindMapView1.CanPaste)
            {
                mindMapView1.Paste();
            }
        }

        void MenuPasteAsNote_Click(object sender, EventArgs e)
        {
            if (mindMapView1.CanPasteAsRemark)
            {
                mindMapView1.PasteAsRemark();
            }
        }

        void MenuPasteAsImage_Click(object sender, EventArgs e)
        {
            if (mindMapView1.CanPaste)
            {
                mindMapView1.PasteAsImage();
            }
        }

        void MenuDelete_Click(object sender, EventArgs e)
        {
            mindMapView1.DeleteObject();
        }

        void MenuMoveUp_Click(object sender, EventArgs e)
        {
            mindMapView1.CustomSort(-1);
        }

        void MenuMoveDown_Click(object sender, EventArgs e)
        {
            mindMapView1.CustomSort(1);
        }

        void MenuCustomSort_Click(object sender, EventArgs e)
        {
            Topic topic = mindMapView1.SelectedTopic;
            if (topic != null && topic.Children.Count > 1)
            {
                SortTopicDialog dlg = new SortTopicDialog(topic.Children.ToArray());
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    this.mindMapView1.CustomSort(topic, dlg.GetNewIndices());
                }
            }
        }

        void MenuEdit_Click(object sender, EventArgs e)
        {
            mindMapView1.EditObject();
        }

        void MenuProperty_Click(object sender, EventArgs e)
        {
            OnNeedShowProperty(true);
        }

        void MenuExportDocx_Click(object sender, EventArgs e)
        {
            Global.GetDocumentForm().Save();
            var dialog = new SaveFileDialog();
            dialog.Filter = "Word(*.docx) | *.docx";
            dialog.Title = Lang._("Export");
            dialog.FileName = ST.EscapeFileName(this.Chart.Document.Name);
            if (dialog.ShowDialog(Global.GetMainForm()) == DialogResult.OK)
            {
                DocxEngine docxEngine = new DocxEngine();
                if (docxEngine.MindMapExportChartToFile(this.Chart.Document, this.Chart, dialog.FileName))
                {
                    var fld = new FileLocationDialog(dialog.FileName, dialog.FileName);
                    fld.Text = Lang._("Export Success");
                    fld.ShowDialog(Global.GetMainForm());
                }
            }
        }

        void MenuExportXmind_Click(object sender, EventArgs e)
        {
            Global.GetDocumentForm().Save();
            var dialog = new SaveFileDialog();
            dialog.Filter = "Xmind(*.xmind) | *.xmind";
            dialog.Title = Lang._("Export");
            dialog.FileName = ST.EscapeFileName(this.Chart.Document.Name);
            if (dialog.ShowDialog(Global.GetMainForm()) == DialogResult.OK)
            {
                XmindEngine xmindEngine = new XmindEngine();
                if (xmindEngine.MindMapExportChartToFile(this.Chart.Document, this.Chart, dialog.FileName))
                {
                    var fld = new FileLocationDialog(dialog.FileName, dialog.FileName);
                    fld.Text = Lang._("Export Success");
                    fld.ShowDialog(Global.GetMainForm());
                }
            }
        }
        #endregion

        #region Custom Tab Menu
        ToolStripMenuItem MenuLayout { get; set; }

        public override List<ToolStripItem> CustomTabMenuItems()
        {
            if (MindMapChart == null)
                return base.CustomTabMenuItems();

            if (MenuLayout == null)
            {
                MenuLayout = new ToolStripMenuItem(Lang._("Layout Type"));

                //
                foreach (MindMapLayoutType layout in Enum.GetValues(typeof(MindMapLayoutType)))
                {
                    ToolStripMenuItem menuLayout = new ToolStripMenuItem();
                    menuLayout.Text = ST.EnumToString(layout);
                    menuLayout.Tag = layout;
                    menuLayout.Image = MindMapLayoutTypeEditor.GetIcon(layout);
                    menuLayout.Click += new EventHandler(MenuLayout_Click);
                    MenuLayout.DropDownItems.Add(menuLayout);
                }
            }

            foreach (ToolStripMenuItem mi in MenuLayout.DropDownItems)
            {
                mi.Checked = (MindMapLayoutType)mi.Tag == MindMapChart.LayoutType;
                mi.Enabled = !ReadOnly;
            }

            List<ToolStripItem> list = new List<ToolStripItem>();
            list.Add(MenuLayout);
            return list;
        }

        void MenuLayout_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem && MindMapChart != null)
            {
                ToolStripMenuItem mi = (ToolStripMenuItem)sender;
                if (mi.Tag is MindMapLayoutType)
                {
                    MindMapChart.LayoutType = (MindMapLayoutType)mi.Tag;
                }
            }
        }

        #endregion
    }
}
