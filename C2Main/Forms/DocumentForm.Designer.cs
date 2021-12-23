
using C2.Controls.Common;
using C2.Model;

namespace C2.Forms
// origin/C2_issue_TaskBar_test:Citta_T1/Forms/DocumentForm.Designer.cs
{
    public partial class DocumentForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DocumentForm));
            this.multiChartsView1 = new C2.ChartPageView.MultiChartsView();
            this.MenuStripChartTab = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MenuRenameTab = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new C2.Controls.ToolStripPro();
            this.ImportDataSourceButton = new System.Windows.Forms.ToolStripButton();
            this.TsbSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.TsbUndo = new System.Windows.Forms.ToolStripButton();
            this.TsbRedo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.TsbCut = new System.Windows.Forms.ToolStripButton();
            this.TsbCopy = new System.Windows.Forms.ToolStripButton();
            this.TsbPaste = new System.Windows.Forms.ToolStripButton();
            this.TsbDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.TsbFind = new System.Windows.Forms.ToolStripButton();
            this.TsbReplace = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.TsbFormatPainter = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
            this.TsbThemes = new System.Windows.Forms.ToolStripDropDownButton();
            this.MenuThemes = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuSaveThemeAs = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuRefreshThemes = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.TsbSelectMode = new System.Windows.Forms.ToolStripButton();
            this.TsbScrollMode = new System.Windows.Forms.ToolStripButton();
            this.TsbTimer = new System.Windows.Forms.ToolStripDropDownButton();
            this.MenuStartTimer = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator14 = new System.Windows.Forms.ToolStripSeparator();
            this.TsbZoomOut = new System.Windows.Forms.ToolStripButton();
            this.TsbZoom = new System.Windows.Forms.ToolStripDropDownButton();
            this.MenuZoomFitPage = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuZoomFitWidth = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuZoomFitHeight = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.TsbZoomIn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.TsbExport = new System.Windows.Forms.ToolStripButton();
            this.TsbSidebar = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.progressBarLabel = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.currentModelFinLab = new System.Windows.Forms.Label();
            this.currentModelRunBackLab = new System.Windows.Forms.Label();
            this.currentModelRunLab = new System.Windows.Forms.Label();
            this.splitter1 = new C2.Controls.MySplitter();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.myTabControl1 = new C2.Controls.MyTabControl();
            this.designerControl = new C2.Controls.Common.DesignerControl();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.MenuStripChartTab.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.currentModelRunBackLab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // multiChartsView1
            // 
            this.multiChartsView1.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.multiChartsView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.multiChartsView1.Location = new System.Drawing.Point(0, 0);
            this.multiChartsView1.Name = "multiChartsView1";
            this.multiChartsView1.SelectedIndex = -1;
            this.multiChartsView1.SelectedPage = null;
            this.multiChartsView1.ShowBorder = C2.Controls.BoderType.NoLeft;
            this.multiChartsView1.Size = new System.Drawing.Size(574, 410);
            this.multiChartsView1.TabIndex = 0;
            this.multiChartsView1.TabsMenuStrip = this.MenuStripChartTab;
            this.multiChartsView1.NewChartPage += new System.EventHandler(this.multiChartsView1_NewChartPage);
            this.multiChartsView1.NeedShowProperty += new C2.ChartPageView.NeedShowPropertyEventHandler(this.multiChartsView1_NeedShowProperty);
            this.multiChartsView1.SelectedIndexChanged += new System.EventHandler(this.multiChartsView1_SelectedIndexChanged);
            // 
            // MenuStripChartTab
            // 
            this.MenuStripChartTab.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            });
            this.MenuStripChartTab.Name = "MenuStripChartTab";
            this.MenuStripChartTab.Size = new System.Drawing.Size(133, 26);
            // 
            // MenuRenameTab
            // 
            this.MenuRenameTab.Image = global::C2.Properties.Resources.edit;
            this.MenuRenameTab.Name = "MenuRenameTab";
            this.MenuRenameTab.Size = new System.Drawing.Size(132, 22);
            this.MenuRenameTab.Text = "&Rename...";
            this.MenuRenameTab.Click += new System.EventHandler(this.MenuRenameTab_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(90)))), ((int)(((byte)(177)))));
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ImportDataSourceButton,
            this.TsbSave,
            this.toolStripSeparator1,
            this.TsbUndo,
            this.TsbRedo,
            this.toolStripSeparator7,
            this.TsbCut,
            this.TsbCopy,
            this.TsbPaste,
            this.TsbDelete,
            this.toolStripSeparator8,
            this.TsbFind,
            this.TsbReplace,
            this.toolStripSeparator6,
            this.TsbFormatPainter,
            this.toolStripSeparator12,
            this.TsbThemes,
            this.toolStripSeparator11,
            this.TsbSelectMode,
            this.TsbScrollMode,
            this.TsbTimer,
            this.toolStripSeparator14,
            this.TsbZoomOut,
            this.TsbZoom,
            this.TsbZoomIn,
            this.toolStripSeparator5,
            this.TsbExport,
            this.TsbSidebar});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(804, 28);
            this.toolStrip1.TabIndex = 2;
            // 
            // ImportDataSourceButton
            // 
            this.ImportDataSourceButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ImportDataSourceButton.Image = global::C2.Properties.Resources.importModel;
            this.ImportDataSourceButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ImportDataSourceButton.Name = "ImportDataSourceButton";
            this.ImportDataSourceButton.Padding = new System.Windows.Forms.Padding(2);
            this.ImportDataSourceButton.Size = new System.Drawing.Size(24, 25);
            this.ImportDataSourceButton.ToolTipText = "���뱾�������ļ�,֧��bcp,txt,csv,xls���ָ�ʽ";
            this.ImportDataSourceButton.Click += new System.EventHandler(this.ImportDataSourceButton_Click);
            // 
            // TsbSave
            // 
            this.TsbSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TsbSave.Image = global::C2.Properties.Resources.save;
            this.TsbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsbSave.Name = "TsbSave";
            this.TsbSave.Padding = new System.Windows.Forms.Padding(2);
            this.TsbSave.Size = new System.Drawing.Size(24, 25);
            this.TsbSave.Text = "&Save";
            this.TsbSave.Click += new System.EventHandler(this.TsbSave_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Padding = new System.Windows.Forms.Padding(2);
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 28);
            // 
            // TsbUndo
            // 
            this.TsbUndo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TsbUndo.Enabled = false;
            this.TsbUndo.Image = global::C2.Properties.Resources.undo;
            this.TsbUndo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsbUndo.Name = "TsbUndo";
            this.TsbUndo.Padding = new System.Windows.Forms.Padding(2);
            this.TsbUndo.Size = new System.Drawing.Size(24, 25);
            this.TsbUndo.Text = "Undo";
            this.TsbUndo.Click += new System.EventHandler(this.TsbUndo_Click);
            // 
            // TsbRedo
            // 
            this.TsbRedo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TsbRedo.Enabled = false;
            this.TsbRedo.Image = global::C2.Properties.Resources.redo;
            this.TsbRedo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsbRedo.Name = "TsbRedo";
            this.TsbRedo.Padding = new System.Windows.Forms.Padding(2);
            this.TsbRedo.Size = new System.Drawing.Size(24, 25);
            this.TsbRedo.Text = "Redo";
            this.TsbRedo.Click += new System.EventHandler(this.TsbRedo_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Padding = new System.Windows.Forms.Padding(2);
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 28);
            // 
            // TsbCut
            // 
            this.TsbCut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TsbCut.Image = global::C2.Properties.Resources.cut;
            this.TsbCut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsbCut.Name = "TsbCut";
            this.TsbCut.Padding = new System.Windows.Forms.Padding(2);
            this.TsbCut.Size = new System.Drawing.Size(24, 25);
            this.TsbCut.Text = "C&ut";
            this.TsbCut.Click += new System.EventHandler(this.TsbCut_Click);
            // 
            // TsbCopy
            // 
            this.TsbCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TsbCopy.Image = global::C2.Properties.Resources.copy;
            this.TsbCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsbCopy.Name = "TsbCopy";
            this.TsbCopy.Padding = new System.Windows.Forms.Padding(2);
            this.TsbCopy.Size = new System.Drawing.Size(24, 25);
            this.TsbCopy.Text = "&Copy";
            this.TsbCopy.Click += new System.EventHandler(this.TsbCopy_Click);
            // 
            // TsbPaste
            // 
            this.TsbPaste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TsbPaste.Image = global::C2.Properties.Resources.paste;
            this.TsbPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsbPaste.Name = "TsbPaste";
            this.TsbPaste.Padding = new System.Windows.Forms.Padding(2);
            this.TsbPaste.Size = new System.Drawing.Size(24, 25);
            this.TsbPaste.Text = "&Paste";
            this.TsbPaste.Click += new System.EventHandler(this.TsbPaste_Click);
            // 
            // TsbDelete
            // 
            this.TsbDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TsbDelete.Image = global::C2.Properties.Resources.delete;
            this.TsbDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsbDelete.Name = "TsbDelete";
            this.TsbDelete.Padding = new System.Windows.Forms.Padding(2);
            this.TsbDelete.Size = new System.Drawing.Size(24, 25);
            this.TsbDelete.Text = "Delete";
            this.TsbDelete.Click += new System.EventHandler(this.TsbDelete_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Padding = new System.Windows.Forms.Padding(2);
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 28);
            // 
            // TsbFind
            // 
            this.TsbFind.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TsbFind.Enabled = false;
            this.TsbFind.Image = global::C2.Properties.Resources.find;
            this.TsbFind.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsbFind.Name = "TsbFind";
            this.TsbFind.Padding = new System.Windows.Forms.Padding(2);
            this.TsbFind.Size = new System.Drawing.Size(24, 25);
            this.TsbFind.Text = "Find";
            this.TsbFind.Click += new System.EventHandler(this.TsbFind_Click);
            // 
            // TsbReplace
            // 
            this.TsbReplace.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TsbReplace.Enabled = false;
            this.TsbReplace.Image = global::C2.Properties.Resources.replace;
            this.TsbReplace.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsbReplace.Name = "TsbReplace";
            this.TsbReplace.Padding = new System.Windows.Forms.Padding(2);
            this.TsbReplace.Size = new System.Drawing.Size(24, 25);
            this.TsbReplace.Text = "Replace";
            this.TsbReplace.Visible = false;
            this.TsbReplace.Click += new System.EventHandler(this.TsbReplace_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Padding = new System.Windows.Forms.Padding(2);
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 28);
            // 
            // TsbFormatPainter
            // 
            this.TsbFormatPainter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TsbFormatPainter.Enabled = false;
            this.TsbFormatPainter.Image = global::C2.Properties.Resources.format_painter;
            this.TsbFormatPainter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsbFormatPainter.Name = "TsbFormatPainter";
            this.TsbFormatPainter.Padding = new System.Windows.Forms.Padding(2);
            this.TsbFormatPainter.Size = new System.Drawing.Size(24, 25);
            this.TsbFormatPainter.Text = "Format Painter";
            this.TsbFormatPainter.Click += new System.EventHandler(this.TsbFormatPainter_Click);
            // 
            // toolStripSeparator12
            // 
            this.toolStripSeparator12.Name = "toolStripSeparator12";
            this.toolStripSeparator12.Padding = new System.Windows.Forms.Padding(2);
            this.toolStripSeparator12.Size = new System.Drawing.Size(6, 28);
            // 
            // TsbThemes
            // 
            this.TsbThemes.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TsbThemes.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuThemes,
            this.MenuSaveThemeAs,
            this.MenuRefreshThemes,
            this.toolStripSeparator4});
            this.TsbThemes.Image = global::C2.Properties.Resources.theme;
            this.TsbThemes.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsbThemes.Name = "TsbThemes";
            this.TsbThemes.Padding = new System.Windows.Forms.Padding(2);
            this.TsbThemes.Size = new System.Drawing.Size(33, 25);
            this.TsbThemes.Text = "Themes";
            this.TsbThemes.DropDownOpening += new System.EventHandler(this.TsbThemes_DropDownOpening);
            // 
            // MenuThemes
            // 
            this.MenuThemes.Image = global::C2.Properties.Resources.theme_manage;
            this.MenuThemes.Name = "MenuThemes";
            this.MenuThemes.Size = new System.Drawing.Size(202, 22);
            this.MenuThemes.Text = "Themes...";
            this.MenuThemes.Click += new System.EventHandler(this.MenuThemes_Click);
            // 
            // MenuSaveThemeAs
            // 
            this.MenuSaveThemeAs.Image = global::C2.Properties.Resources.save;
            this.MenuSaveThemeAs.Name = "MenuSaveThemeAs";
            this.MenuSaveThemeAs.Size = new System.Drawing.Size(202, 22);
            this.MenuSaveThemeAs.Text = "Save Current Theme...";
            this.MenuSaveThemeAs.Click += new System.EventHandler(this.MenuSaveThemeAs_Click);
            // 
            // MenuRefreshThemes
            // 
            this.MenuRefreshThemes.Image = global::C2.Properties.Resources.refresh;
            this.MenuRefreshThemes.Name = "MenuRefreshThemes";
            this.MenuRefreshThemes.Size = new System.Drawing.Size(202, 22);
            this.MenuRefreshThemes.Text = "&Refresh";
            this.MenuRefreshThemes.Click += new System.EventHandler(this.MenuRefreshThemes_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(199, 6);
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Padding = new System.Windows.Forms.Padding(2);
            this.toolStripSeparator11.Size = new System.Drawing.Size(6, 28);
            // 
            // TsbSelectMode
            // 
            this.TsbSelectMode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TsbSelectMode.Image = global::C2.Properties.Resources.cursor;
            this.TsbSelectMode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsbSelectMode.Name = "TsbSelectMode";
            this.TsbSelectMode.Padding = new System.Windows.Forms.Padding(2);
            this.TsbSelectMode.Size = new System.Drawing.Size(24, 25);
            this.TsbSelectMode.Text = "Select Mode";
            this.TsbSelectMode.Click += new System.EventHandler(this.TsbSelectMode_Click);
            // 
            // TsbScrollMode
            // 
            this.TsbScrollMode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TsbScrollMode.Image = global::C2.Properties.Resources.hand;
            this.TsbScrollMode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsbScrollMode.Name = "TsbScrollMode";
            this.TsbScrollMode.Padding = new System.Windows.Forms.Padding(2);
            this.TsbScrollMode.Size = new System.Drawing.Size(24, 25);
            this.TsbScrollMode.Text = "Scroll Mode";
            this.TsbScrollMode.Click += new System.EventHandler(this.TsbScrollMode_Click);
            // 
            // TsbTimer
            // 
            this.TsbTimer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TsbTimer.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuStartTimer,
            this.toolStripMenuItem3});
            this.TsbTimer.Image = global::C2.Properties.Resources.hourglass;
            this.TsbTimer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsbTimer.Name = "TsbTimer";
            this.TsbTimer.Padding = new System.Windows.Forms.Padding(2);
            this.TsbTimer.Size = new System.Drawing.Size(33, 25);
            this.TsbTimer.Text = "Timer";
            // 
            // MenuStartTimer
            // 
            this.MenuStartTimer.Name = "MenuStartTimer";
            this.MenuStartTimer.Size = new System.Drawing.Size(149, 22);
            this.MenuStartTimer.Text = "Start Timer...";
            this.MenuStartTimer.Click += new System.EventHandler(this.MenuStartTimer_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(146, 6);
            // 
            // toolStripSeparator14
            // 
            this.toolStripSeparator14.Name = "toolStripSeparator14";
            this.toolStripSeparator14.Padding = new System.Windows.Forms.Padding(2);
            this.toolStripSeparator14.Size = new System.Drawing.Size(6, 28);
            // 
            // TsbZoomOut
            // 
            this.TsbZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TsbZoomOut.Image = global::C2.Properties.Resources.zoom_out;
            this.TsbZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsbZoomOut.Name = "TsbZoomOut";
            this.TsbZoomOut.Padding = new System.Windows.Forms.Padding(2);
            this.TsbZoomOut.Size = new System.Drawing.Size(24, 25);
            this.TsbZoomOut.Text = "Zoom Out";
            this.TsbZoomOut.Click += new System.EventHandler(this.TsbZoomOut_Click);
            // 
            // TsbZoom
            // 
            this.TsbZoom.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.TsbZoom.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuZoomFitPage,
            this.MenuZoomFitWidth,
            this.MenuZoomFitHeight,
            this.toolStripSeparator2});
            this.TsbZoom.Image = ((System.Drawing.Image)(resources.GetObject("TsbZoom.Image")));
            this.TsbZoom.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsbZoom.Name = "TsbZoom";
            this.TsbZoom.Padding = new System.Windows.Forms.Padding(2);
            this.TsbZoom.Size = new System.Drawing.Size(57, 25);
            this.TsbZoom.Text = "100%";
            // 
            // MenuZoomFitPage
            // 
            this.MenuZoomFitPage.Name = "MenuZoomFitPage";
            this.MenuZoomFitPage.Size = new System.Drawing.Size(131, 22);
            this.MenuZoomFitPage.Text = "&Fit Page";
            this.MenuZoomFitPage.Click += new System.EventHandler(this.MenuZoomFitPage_Click);
            // 
            // MenuZoomFitWidth
            // 
            this.MenuZoomFitWidth.Name = "MenuZoomFitWidth";
            this.MenuZoomFitWidth.Size = new System.Drawing.Size(131, 22);
            this.MenuZoomFitWidth.Text = "Fit Width";
            this.MenuZoomFitWidth.Click += new System.EventHandler(this.MenuZoomFitWidth_Click);
            // 
            // MenuZoomFitHeight
            // 
            this.MenuZoomFitHeight.Name = "MenuZoomFitHeight";
            this.MenuZoomFitHeight.Size = new System.Drawing.Size(131, 22);
            this.MenuZoomFitHeight.Text = "Fit Height";
            this.MenuZoomFitHeight.Click += new System.EventHandler(this.MenuZoomFitHeight_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(128, 6);
            // 
            // TsbZoomIn
            // 
            this.TsbZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TsbZoomIn.Image = global::C2.Properties.Resources.zoom_in;
            this.TsbZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsbZoomIn.Name = "TsbZoomIn";
            this.TsbZoomIn.Padding = new System.Windows.Forms.Padding(2);
            this.TsbZoomIn.Size = new System.Drawing.Size(24, 25);
            this.TsbZoomIn.Text = "Zoom In";
            this.TsbZoomIn.Click += new System.EventHandler(this.TsbZoomIn_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Padding = new System.Windows.Forms.Padding(2);
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 28);
            // 
            // TsbExport
            // 
            this.TsbExport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TsbExport.Image = global::C2.Properties.Resources.export_image;
            this.TsbExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsbExport.Name = "TsbExport";
            this.TsbExport.Padding = new System.Windows.Forms.Padding(2);
            this.TsbExport.Size = new System.Drawing.Size(24, 25);
            this.TsbExport.Text = "Export";
            this.TsbExport.Click += new System.EventHandler(this.TsbExport_Click);
            // 
            // TsbSidebar
            // 
            this.TsbSidebar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.TsbSidebar.Checked = true;
            this.TsbSidebar.CheckState = System.Windows.Forms.CheckState.Checked;
            this.TsbSidebar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TsbSidebar.Image = global::C2.Properties.Resources.sidebar;
            this.TsbSidebar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsbSidebar.Name = "TsbSidebar";
            this.TsbSidebar.Padding = new System.Windows.Forms.Padding(2);
            this.TsbSidebar.Size = new System.Drawing.Size(24, 25);
            this.TsbSidebar.Text = "Sidebar";
            this.TsbSidebar.Click += new System.EventHandler(this.TsbSidebar_Click_1);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.progressBarLabel);
            this.panel1.Controls.Add(this.progressBar);
            this.panel1.Controls.Add(this.currentModelFinLab);
            this.panel1.Controls.Add(this.currentModelRunBackLab);
            this.panel1.Controls.Add(this.multiChartsView1);
            this.panel1.Controls.Add(this.splitter1);
            this.panel1.Controls.Add(this.splitContainer2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 28);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(804, 410);
            this.panel1.TabIndex = 3;
            // 
            // progressBarLabel
            // 
            this.progressBarLabel.AutoSize = true;
            this.progressBarLabel.BackColor = System.Drawing.Color.Transparent;
            this.progressBarLabel.Font = new System.Drawing.Font("΢���ź�", 8.25F);
            this.progressBarLabel.ForeColor = System.Drawing.Color.Black;
            this.progressBarLabel.Location = new System.Drawing.Point(402, 197);
            this.progressBarLabel.Name = "progressBarLabel";
            this.progressBarLabel.Size = new System.Drawing.Size(0, 16);
            this.progressBarLabel.TabIndex = 39;
            this.progressBarLabel.Visible = false;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(329, 200);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(125, 10);
            this.progressBar.TabIndex = 38;
            this.progressBar.Visible = false;
            // 
            // currentModelFinLab
            // 
            this.currentModelFinLab.Image = global::C2.Properties.Resources.currentModelFin;
            this.currentModelFinLab.Location = new System.Drawing.Point(327, 213);
            this.currentModelFinLab.Name = "currentModelFinLab";
            this.currentModelFinLab.Size = new System.Drawing.Size(150, 100);
            this.currentModelFinLab.TabIndex = 37;
            this.currentModelFinLab.Visible = false;
            // 
            // currentModelRunBackLab
            // 
            this.currentModelRunBackLab.Controls.Add(this.currentModelRunLab);
            this.currentModelRunBackLab.Image = global::C2.Properties.Resources.currentModelRunningBack;
            this.currentModelRunBackLab.Location = new System.Drawing.Point(327, 97);
            this.currentModelRunBackLab.Name = "currentModelRunBackLab";
            this.currentModelRunBackLab.Size = new System.Drawing.Size(150, 100);
            this.currentModelRunBackLab.TabIndex = 36;
            this.currentModelRunBackLab.Visible = false;
            // 
            // currentModelRunLab
            // 
            this.currentModelRunLab.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.currentModelRunLab.Cursor = System.Windows.Forms.Cursors.Hand;
            this.currentModelRunLab.Image = global::C2.Properties.Resources.currentModelRunning;
            this.currentModelRunLab.Location = new System.Drawing.Point(0, 0);
            this.currentModelRunLab.Name = "currentModelRunLab";
            this.currentModelRunLab.Size = new System.Drawing.Size(73, 47);
            this.currentModelRunLab.TabIndex = 28;
            this.currentModelRunLab.Visible = false;
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter1.Location = new System.Drawing.Point(574, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 410);
            this.splitter1.TabIndex = 1;
            this.splitter1.TabStop = false;
            this.splitter1.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitter1_SplitterMoved);
            this.splitter1.DoubleClick += new System.EventHandler(this.splitter1_DoubleClick_1);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitContainer2.Location = new System.Drawing.Point(577, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.myTabControl1);
            this.splitContainer2.Size = new System.Drawing.Size(227, 410);
            this.splitContainer2.SplitterDistance = 169;
            this.splitContainer2.SplitterWidth = 3;
            this.splitContainer2.TabIndex = 0;
            // 
            // myTabControl1
            // 
            this.myTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.myTabControl1.Location = new System.Drawing.Point(0, 0);
            this.myTabControl1.Name = "myTabControl1";
            this.myTabControl1.SelectedIndex = -1;
            this.myTabControl1.SelectedPage = null;
            this.myTabControl1.Size = new System.Drawing.Size(227, 169);
            this.myTabControl1.TabIndex = 3;
            this.myTabControl1.Text = "myTabControl1";
            // 
            // designerControl
            // 
            this.designerControl.BackColor = System.Drawing.Color.White;
            this.designerControl.ComboDataSource = null;
            this.designerControl.Font = new System.Drawing.Font("΢���ź�", 9F);
            this.designerControl.Location = new System.Drawing.Point(0, 0);
            this.designerControl.MindmapView = null;
            this.designerControl.Name = "designerControl";
            this.designerControl.OpWidget = null;
            this.designerControl.SelectedDataSource = null;
            this.designerControl.SelectedOperator = null;
            this.designerControl.SelectedTopic = null;
            this.designerControl.Size = new System.Drawing.Size(186, 260);
            this.designerControl.TabIndex = 1;
            this.designerControl.Text = "���������";
            // 
            // DocumentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(804, 438);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.IconImage = global::C2.Properties.Resources.ҵ����ͼ1;
            this.KeyPreview = true;
            this.Name = "DocumentForm";
            this.Text = "DocumentForm";
            this.SizeChanged += new System.EventHandler(this.DocumentForm_SizeChanged);
            this.MenuStripChartTab.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.currentModelRunBackLab.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private C2.ChartPageView.MultiChartsView multiChartsView1;
        private System.Windows.Forms.ContextMenuStrip MenuStripChartTab;
        private System.Windows.Forms.ToolStripMenuItem MenuRenameTab;
        private Controls.ToolStripPro toolStrip1;
        private System.Windows.Forms.ToolStripButton TsbSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton TsbUndo;
        private System.Windows.Forms.ToolStripButton TsbRedo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripButton TsbCut;
        private System.Windows.Forms.ToolStripButton TsbCopy;
        private System.Windows.Forms.ToolStripButton TsbPaste;
        private System.Windows.Forms.ToolStripButton TsbDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripButton TsbFind;
        private System.Windows.Forms.ToolStripButton TsbReplace;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton TsbFormatPainter;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
        private System.Windows.Forms.ToolStripDropDownButton TsbThemes;
        private System.Windows.Forms.ToolStripMenuItem MenuThemes;
        private System.Windows.Forms.ToolStripMenuItem MenuSaveThemeAs;
        private System.Windows.Forms.ToolStripMenuItem MenuRefreshThemes;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
        private System.Windows.Forms.ToolStripButton TsbSelectMode;
        private System.Windows.Forms.ToolStripButton TsbScrollMode;
        private System.Windows.Forms.ToolStripDropDownButton TsbTimer;
        private System.Windows.Forms.ToolStripMenuItem MenuStartTimer;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator14;
        private System.Windows.Forms.ToolStripButton TsbZoomOut;
        private System.Windows.Forms.ToolStripDropDownButton TsbZoom;
        private System.Windows.Forms.ToolStripMenuItem MenuZoomFitPage;
        private System.Windows.Forms.ToolStripMenuItem MenuZoomFitWidth;
        private System.Windows.Forms.ToolStripMenuItem MenuZoomFitHeight;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton TsbZoomIn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton TsbExport;
        private System.Windows.Forms.ToolStripButton TsbSidebar;
        private System.Windows.Forms.Panel panel1;
        private Controls.MySplitter splitter1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private DesignerControl designerControl;
        private Controls.MyTabControl myTabControl1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripButton ImportDataSourceButton;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label currentModelFinLab;
        private System.Windows.Forms.Label currentModelRunBackLab;
        private System.Windows.Forms.Label currentModelRunLab;
        private System.Windows.Forms.Label progressBarLabel;
    }
}