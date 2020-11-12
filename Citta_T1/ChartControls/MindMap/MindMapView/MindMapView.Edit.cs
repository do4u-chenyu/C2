using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using C2.Core;
using C2.Dialogs;
using C2.Globalization;
using C2.Model;
using C2.Model.MindMaps;
using C2.Model.Widgets;

namespace C2.Controls.MapViews
{
    public partial class MindMapView
    {
        private bool _EditMode;
        private TopicEditControl EditControl;
        private ITextObject EditingObject;

        [Browsable(false)]
        public bool EditMode
        {
            get { return _EditMode; }
            private set
            {
                if (_EditMode != value)
                {
                    _EditMode = value;
                    OnEditModeChanged();
                }
            }
        }

        void OnEditModeChanged()
        {
            if (EditMode)
            {
                if (EditControl != null)
                {
                    EditControl.Show();
                    EditControl.Focus();
                }
            }
            else
            {
                if (EditControl != null && EditControl.Visible)
                {
                    EditControl.Hide();
                }

                EditingObject = null;

                if (this.CanFocus)
                    this.Focus();
            }
        }

        public void AddLink()
        {
            if (SelectedTopic != null)
            {
                LinksLayer.NewLineFrom = SelectedTopic;
            }
        }

        public void AddProgressBar()
        {
            if (SelectedTopics != null && SelectedTopics.Length > 0)
            {
                AddWidget(ProgressBarWidget.TypeID, new ProgressBarWidget(), true);
            }
        }

        public void AddIcon()
        {
            if (SelectedTopics != null && SelectedTopics.Length > 0)
            {
                var dialog = new AddIconDialog();
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    var template = new PictureWidget();
                    template.Image = dialog.CurrentObject;
                    AddWidget(PictureWidget.TypeID, template, dialog.NeedMoreOptions);
                }
            }
        }

        public void AddRemark()
        {
            if (SelectedTopics != null && SelectedTopics.Length > 0)
            {
                var dialog = new NoteWidgetDialog();

                //
                if (Clipboard.ContainsText())
                    dialog.Remark = ClipboardHelper.GetHtml();

                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    var template = new NoteWidget();
                    template.Remark = dialog.Remark;
                    AddWidget(NoteWidget.TypeID, template, false);
                }
            }
        }

        public void AddOperator()
        {
            foreach(Topic topic in SelectedTopics)
            {
                if (topic.FindWidget<OperatorWidget>() != null)
                    return;
            }

            if (SelectedTopics != null && SelectedTopics.Length > 0)
            {
                var template = new OperatorWidget();
                AddWidget(OperatorWidget.TypeID, template, false);
                ShowDesigner(SelectedTopics[0],false);
            }
        }

        public void AddOperator(Topic[] topics)
        {
            var template = new OperatorWidget();
            AddWidgetCommand command = new AddWidgetCommand(topics, OperatorWidget.TypeID, template);
            ExecuteCommand(command);
        }
        public void AddModelOp()
        {
            if (SelectedTopics != null && SelectedTopics.Length > 0)
            {
                List<DataItem> dataItems = new List<DataItem>();
                DataSourceWidget dtw = SelectedTopics[0].FindWidget<DataSourceWidget>();
                if (dtw != null)
                    dataItems = dtw.DataItems;
                Global.GetMainForm().NewCanvasForm(string.Format("{0}-模型视图",SelectedTopics[0].Text), dataItems);
            }
        }
          public void AddAttachment()
        {
            if (SelectedTopics != null && SelectedTopics.Length > 0)
            {
                OpenFileDialog fd = new OpenFileDialog
                {
                    Filter = "文件|*.docx;*.xlsx;*.doc;*.xls;*.pdf;*.txt;*.bcp;*.xmind",
                    Title = Lang._("Attachment")
                };

                if (fd.ShowDialog() == DialogResult.OK)
                {
                    Topic hitTopic = SelectedTopics[0];

                    AttachmentWidget atw = hitTopic.FindWidget<AttachmentWidget>();
                    if (atw == null)
                    {
                        var template = new AttachmentWidget();
                        template.FullFilePaths = new List<string>() { fd.FileName };
                        AddWidget(AttachmentWidget.TypeID, template, false);
                        ShowDesigner(hitTopic, false);
                        return;
                    }
                    if (atw.FullFilePaths.Find(x => x.Equals(fd.FileName)) == null)
                    {
                        atw.FullFilePaths.Add(fd.FileName);
                        ShowDesigner(atw.Container, false);
                    }
                    else
                        MessageBox.Show(String.Format("数据源{0}已存在,可以删除后重新导入.", fd.FileName),
                            "数据源已存在",                // 标题
                            MessageBoxButtons.OK,          // 按钮样式
                            MessageBoxIcon.Information);   // 图标样式
                }
            }
        }

        public void AddDataSource(Topic[] hitTopic,DataItem dataItem)
        { 
            var template = new DataSourceWidget();
            template.DataItems.Add(dataItem);
            AddWidgetCommand command = new AddWidgetCommand(hitTopic, DataSourceWidget.TypeID, template);
            ExecuteCommand(command);
        }
        public void AddResult(Topic[] hitTopic, DataItem dataItem)
        {
            var template = new ResultWidget();
            template.DataItems.Add(dataItem);
            AddWidgetCommand command = new AddWidgetCommand(hitTopic, ResultWidget.TypeID, template);
            ExecuteCommand(command);
        }
        public void AddChartWidget(Topic[] hitTopic)
        {
            var template = new ChartWidget();
            AddWidgetCommand command = new AddWidgetCommand(hitTopic, ChartWidget.TypeID, template);
            ExecuteCommand(command);
        }
        void AddWidget(string typeID, Widget template, bool showDialog)
        {
            if (showDialog)
            {
                var dialog = new PropertyDialog();
                dialog.SelectedObject = template;
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    AddWidgetCommand command = new AddWidgetCommand(SelectedTopics, typeID, template);
                    ExecuteCommand(command);
                }
            }
            else
            {
                AddWidgetCommand command = new AddWidgetCommand(SelectedTopics, typeID, template);
                ExecuteCommand(command);
            }
        }

        public virtual Topic AddTopic(bool atFront)
        {
            if (SelectedTopic != null)
            {
                if (SelectedTopic.ParentTopic != null)
                {
                    return AddSubTopic(SelectedTopic.ParentTopic, SelectedTopic, atFront);
                }
                else
                {
                    return AddSubTopic();
                }
            }
            else
            {
                return null;
            }
        }

        public Topic AddTopic()
        {
            return AddTopic(false);
        }

        public Topic AddTopicFront()
        {
            return AddTopic(true);
        }

        public virtual Topic AddSubTopic()
        {
            if (SelectedTopic != null)
            {
                return AddSubTopic(SelectedTopic, null, false);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parentTopic"></param>
        /// <param name="reference"></param>
        /// <param name="atFront"></param>
        /// <returns></returns>
        Topic AddSubTopic(Topic parentTopic, Topic reference, bool atFront)
        {
            var index = -1;
            if (reference != null)
                index = parentTopic.Children.IndexOf(reference);
            if (!atFront)
                index++;

            Topic subTopic = new Topic(GetNewSubTopicText(parentTopic));
            AddTopic(parentTopic, subTopic, index);
            Select(subTopic);
            return subTopic;
        }

        public void EditWidget(Widget widget)
        {
            if (widget == null)
                return;

            IWidgetEditDialog dialog = widget.CreateEditDialog();
            if (dialog == null)
                return;

            dialog.Widget = widget.Clone() as Widget;
            if (dialog.Widget == null || dialog.ShowDialog(this) != DialogResult.OK)
                return;

            EditWidgetCommand comd = new EditWidgetCommand(widget, dialog.Widget);
            ExecuteCommand(comd);
        }

        public virtual void EditObject()
        {
            if (!CanEdit)
                return;

            if(SelectedObject is ITextObject)
            {
                EditObject((ITextObject)SelectedObject);
            }
        }

        public virtual void EditObject(ITextObject tobj)
        {
            if (EditMode)
            {
                EndEdit(false);
            }

            if (tobj == null || ReadOnly)
                return;

            if (EditControl == null)
                EditControl = CreateEditControl();
            if (EditControl == null)
                return;
            if (EditControl.Parent != this)
                this.Controls.Add(EditControl);
            EditControl.BringToFront();

            EditingObject = tobj;
            EditControl.Text = tobj.Text;
            ResetEditControlBounds(tobj);

            Font font;
            if(tobj.Font != null)
                font = tobj.Font;
            else
                font = ChartBox.Font;
            if (Zoom != 1.0f)
                font = new Font(font.FontFamily, font.Size * Zoom, font.Style);
            EditControl.Font = font;

            EditMode = true;
        }

        private bool ResetEditControlBounds()
        {
            if (EditMode && EditControl != null && EditingObject != null)
            {
                ResetEditControlBounds(EditingObject);
                return true;
            }
            else
            {
                return false;
            }
        }

        private void ResetEditControlBounds(ITextObject tobj)
        {
            Rectangle rect = GetObjectRect(tobj);
            rect.X += ChartBox.Left;
            rect.Y += ChartBox.Top;

            int minH = tobj.Font != null ? tobj.Font.Height : ChartBox.Font.Height;
            if (rect.Height < minH)
                rect.Height = minH;

            rect.Width--;
            rect.Height--;
            rect.Inflate(GeneralRender.OutBoxSpace + 2, GeneralRender.OutBoxSpace + 2);
            EditControl.Bounds = rect;
        }

        protected virtual void EndEdit(bool acceptChange)
        {
            if (EditMode)
            {
                if (EditControl != null && EditControl.Visible)
                {
                    EditControl.EndEdit(acceptChange, false);
                    EditControl.Hide();
                }

                if (acceptChange)
                {
                    if (EditingObject.Text != EditControl.Text)
                    {
                        ChangeObjectText(EditingObject, EditControl.Text);
                    }
                }

                EditMode = false;
            }
        }

        public void CancelEdit()
        {
            if (EditMode)
            {
                EndEdit(false);
            }
        }

        protected virtual TopicEditControl CreateEditControl()
        {
            TopicEditControl ctl = new TopicEditControl();
            ctl.Visible = false;
            ctl.Cancel += new EventHandler(EditControl_Cancel);
            ctl.Apply += new EventHandler(EditControl_Apply);
            return ctl;
        }

        private void EditControl_Apply(object sender, EventArgs e)
        {
            EndEdit(true);
        }

        private void EditControl_Cancel(object sender, EventArgs e)
        {
            EndEdit(false);
        }

        private string GetNewSubTopicText(Topic parentTopic)
        {
            if (parentTopic == null)
                throw new ArgumentNullException();

            string text = Lang._("Sub Topic");
            int i = parentTopic.Children.Count+1;
            string title = text + " " + i.ToString();
            while(parentTopic.FindChildByText(title) != null)
            {
                i++;
                title = text + " " + i.ToString();
            }

            return title;
        }

        protected override void OnHScrollValueChanged()
        {
            base.OnHScrollValueChanged();

            ResetEditControlBounds();
        }

        protected override void OnVScrollValueChanged()
        {
            base.OnVScrollValueChanged();

            ResetEditControlBounds();
        }
    }
}

