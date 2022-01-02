﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using C2.Core;
using C2.Dialogs;
using C2.Forms;
using C2.Globalization;
using C2.IAOLab.WebEngine;
using C2.Model;
using C2.Model.MindMaps;
using C2.Model.Widgets;
using C2.Utils;

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

        #region 添加blu挂件
        public void AddProgressBar()
        {
            if (SelectedTopics != null && SelectedTopics.Length > 0)
            {
                ProgressBarWidget pbw = SelectedTopics[0].FindWidget<ProgressBarWidget>();
                if(pbw == null)
                    AddWidget(ProgressBarWidget.TypeID, new ProgressBarWidget(), true);
            }
        }

        public void AddIcon()
        {
            if (SelectedTopics != null && SelectedTopics.Length > 0)
            {
                this.Cursor = Cursors.WaitCursor;
                var dialog = new AddIconDialog();
                this.Cursor = Cursors.Default;
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

                if (Clipboard.ContainsText())
                    dialog.Remark = ClipboardHelper.GetHtml();

                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    NoteWidget nw = SelectedTopics[0].FindWidget<NoteWidget>();
                    if(nw == null)
                    {
                        var template = new NoteWidget();
                        template.Remark = dialog.Remark;
                        AddWidget(NoteWidget.TypeID, template, false);
                    }
                    else
                    {
                        nw.Remark = dialog.Remark;
                    }
                }
            }
        }
        #endregion

        #region 添加C2挂件
        public void AddOperator()
        {
            foreach (Topic topic in SelectedTopics)
            {
                if (topic.FindWidget<OperatorWidget>() != null)
                {
                    ShowDesigner(topic.FindWidget<OperatorWidget>(), true);
                    return;
                }
                    
            }

            if (SelectedTopics != null && SelectedTopics.Length > 0)
            {
                SelectedTopics[0].Widgets.Add(new OperatorWidget());
                //ShowDesigner(SelectedTopics[0],false);
                ShowDesigner(SelectedTopics[0].FindWidget<OperatorWidget>(), true);
            }
        }

        public void AddModelOp()
        {
            if (SelectedTopics != null && SelectedTopics.Length > 0)
            {
                Topic topic = SelectedTopics[0];
                CreateNewModelForm createNewModelForm = new CreateNewModelForm
                {
                    StartPosition = FormStartPosition.CenterScreen,
                    RelateMindMapView = this,
                    OpenDocuments = Global.GetMainForm().OpenedDocuments(),
                    NewFormType = FormType.CanvasForm,
                    ModelType = "新建运算视图"
                };
                DialogResult dialogResult = createNewModelForm.ShowDialog();
                if (dialogResult != DialogResult.OK)
                    return;


                // 新建模型视图
                string modelDocumentName = createNewModelForm.ModelTitle;
                string modelUserPath = Path.Combine(Global.WorkspaceDirectory, Global.GetMainForm().UserName, "业务视图", Global.GetCurrentDocument().Name);
                string modelSavePath = Path.Combine(modelUserPath, modelDocumentName, modelDocumentName + ".xml");
                DataItem modelDataItem = new DataItem(modelSavePath, modelDocumentName, '\t', OpUtil.Encoding.NoNeed, OpUtil.ExtType.Unknow);

                OperatorWidget opw = topic.FindWidget<OperatorWidget>();
                if ( opw == null)
                {
                    topic.Add(new OperatorWidget { HasModelOperator = true, ModelDataItem = modelDataItem });

                }
                else if (opw.HasModelOperator)
                {
                    HelpUtil.ShowMessageBox("该节点已存在多维运算流程，请右键算子挂件，选择对应流程进行修改或删除。", "已存在", MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    opw.HasModelOperator = true;
                    opw.ModelDataItem = modelDataItem;
                    Global.GetCurrentDocument().Modified = true;
                }
                //新建模型前保存一次，防止出现用户一直未保存导致模型视图路径逻辑出错
                Global.GetDocumentForm().Save();

                Global.GetMainForm().NewCanvasFormByMindMap(modelDocumentName, Global.GetCurrentDocument().Name, topic);
            }
        }
        public void AddAttachment()
        {
            if (SelectedTopics != null && SelectedTopics.Length > 0)
            {
                OpenFileDialog fd = new OpenFileDialog
                {
                    //Filter = "文件|*.docx;*.xlsx;*.doc;*.xls;*.pdf;*.txt;*.bcp;*.xmind",
                    Filter = null,
                    Title = Lang._("AddAttachment")
                };

                if (fd.ShowDialog() == DialogResult.OK)
                {
                    Topic hitTopic = SelectedTopics[0];

                    AttachmentWidget atw = hitTopic.FindWidget<AttachmentWidget>();
                    if (atw == null)
                    {
                        hitTopic.Widgets.Add(new AttachmentWidget { AttachmentPaths = new List<string> { fd.FileName} });
                        /*
                        AttachmentWidget a = new AttachmentWidget();
                        List<string> l = new List<string>() { fd.FileName };
                        a.AttachmentPaths = l;
                        */

                        return;
                    }
                    if (atw.AttachmentPaths.Find(x => x.Equals(fd.FileName)) == null)
                        atw.AttachmentPaths.Add(fd.FileName); // 估计永远都不会返回null
                    else
                        HelpUtil.ShowMessageBox(String.Format("附件{0}已存在,可以删除后重新导入.", fd.FileName),  "附件已存在");
                }
            }
        }

        public void AddMap()
        {
            Global.GetDocumentForm().Save();

            Topic hitTopic = SelectedTopics[0];
            MapWidget atw = hitTopic.FindWidget<MapWidget>();
            if (atw == null)
                hitTopic.Widgets.Add(new MapWidget());
            

            //当前节点的数据源作为参数传给webbrowser
            new WebManager()
            {
                Type = WebManager.WebType.Map,
                HitTopic = hitTopic
            }.OpenWebBrowser();
            
            return;
        }
        public void AddBoss()
        {
            Global.GetDocumentForm().Save();

            Topic hitTopic = SelectedTopics[0];
            new WebManager()
            {
                Type = WebManager.WebType.Boss,
                HitTopic = hitTopic
            }.OpenWebBrowser();

            return;
        }
        #endregion

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
                return AddSubTopic(SelectedTopic, null, true);
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

