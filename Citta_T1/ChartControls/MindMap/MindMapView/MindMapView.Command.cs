using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.Linq;
using C2.Configuration;
using C2.Core;
using C2.Model;
using C2.Model.MindMaps;
using C2.Model.Styles;
using C2.Model.Widgets;
using System.Collections.Generic;

namespace C2.Controls.MapViews
{
    public partial class MindMapView 
    {
        #region Common Commands

        [Browsable(false)]
        public override bool CanCopy
        {
            get
            {
                if (SelectedTopics != null && SelectedTopics.Length > 0)
                    return true;

                if (SelectedObject is Widget && !(SelectedObject is C2BaseWidget))
                    return true;


                return false;
            }
        }

        [Browsable(false)]
        public override bool CanCut
        {
            get
            {
                if (ReadOnly)
                    return false;

                if (!SelectedTopics.IsNullOrEmpty() && !SelectedTopics.Exists(t=>t.IsRoot))
                    return true;

                if (SelectedObject is Widget && !(SelectedObject is C2BaseWidget))
                    return true;

                return false;
            }
        }

        [Browsable(false)]
        public override bool CanPaste
        {
            get
            {
                return !ReadOnly && Selection.Count == 1 && SelectedTopic != null;
            }
        }

        [Browsable(false)]
        public override bool CanPasteAsRemark
        {
            get
            {
                return !ReadOnly && SelectedObject is IRemark;
            }
        }

        [Browsable(false)]
        public override bool CanDelete
        {
            get
            {
                return !ReadOnly && Selection.Count > 0 && (SelectedTopic == null || !SelectedTopic.IsRoot);
            }
        }

        [Browsable(false)]
        public override bool CanEdit
        {
            get
            {
                return !ReadOnly && SelectedTopic != null;
            }
        }

        public override void DeleteObject()
        {
            if (Selection.Count > 0)
            {
                Delete(Selection.ToArray());
            }

        }

        private void Delete(ChartObject[] mapObjects)
        {
            if (mapObjects != null && mapObjects.Length > 0)
            {
                foreach (ChartObject mapObject in mapObjects)
                {
                    OperatorWidget tmpOpw = null;
                    if (mapObject is Topic && ((Topic)mapObject).IsRoot)
                    {
                        return;
                    }
                    if (mapObject is Topic)
                    {
                        //因为删除的时候直接remove，需要手动遍历所有孩子节点的算子挂件
                        Topic tmpTopic = (Topic)mapObject;
                        foreach (Topic ct in tmpTopic.GetAllChildren())
                        {
                            tmpOpw = ct.FindWidget<OperatorWidget>();
                            CloseRelateOpTab(tmpOpw);
                        }
                    }
                    else if (mapObject is OperatorWidget)
                    {
                        tmpOpw = (OperatorWidget)mapObject;
                        CloseRelateOpTab(tmpOpw);
                    }
                    else if (mapObject is DataSourceWidget)
                    {
                        DelAllItems((Topic)mapObject.Container, ((DataSourceWidget)mapObject).DataItems);
                    }
                    else if (mapObject is ResultWidget)
                    {
                        DelAllItems((Topic)mapObject.Container, ((ResultWidget)mapObject).DataItems);
                    }
                }

                DeleteCommand command = new DeleteCommand(mapObjects);
                ExecuteCommand(command);
            }
        }
        private void DelAllItems(Topic topic,List<DataItem> dataItems)
        {
            foreach(DataItem dataItem in dataItems)
            {
                TopicUpdate(topic, dataItem);
            }
        }
        private void CloseRelateOpTab(OperatorWidget tmpOpw)
        {
            if (tmpOpw != null && tmpOpw.HasModelOperator && tmpOpw.ModelRelateTab !=null && Global.GetMainForm().TaskBar.Items.Contains(tmpOpw.ModelRelateTab))
                Global.GetMainForm().MdiClient.CloseMdiForm((Form)tmpOpw.ModelRelateTab.Tag);
        }

        public override void Copy()
        {
            if (SelectedTopics != null && SelectedTopics.Length > 0)
            {
                var topics = SelectedTopics.OrderBy(t => t.Level).ToArray();
                Copy(topics, true);
            }
            else if (SelectedObject is Widget)// && ((Widget)SelectedObject).CanCopy)
            {
                var widgets = SelectedObjects.Where(w=> w is NoteWidget || w is PictureWidget || w is ProgressBarWidget).ToArray();
                if (widgets.Count() == 0)
                    return;
                Copy(widgets, false);
            }
        }

        void Copy(ChartObject[] objects, bool recursive)
        {
            if (objects != null && objects.Length > 0)
            {
                var command = new CopyCommand(objects, recursive);
                ExecuteCommand(command);
            }
        }

        public override void Cut()
        {
            if (SelectedTopics != null && SelectedTopics.Length > 0)
            {
                //TODO
                //目前连同topic可以一起被剪切复制，不知道后续是否会存在逻辑不封闭
                var topics = SelectedTopics.OrderBy(t => t.Level).ToArray();
                var command = new CutCommand(topics);
                ExecuteCommand(command);
            }
            else if (SelectedObject is Widget)
            {
                //只有blu挂件可以被剪切
                var widgets = SelectedObjects.Where(o => o is NoteWidget || o is ProgressBarWidget || o is PictureWidget).ToArray();
                if (widgets.Count() == 0)
                    return;
                var command = new CutCommand(widgets);
                ExecuteCommand(command);
            }
        }

        public override void Paste()
        {
            if (SelectedTopic != null)
            {
                var command = new PasteCommand(Map.Document, SelectedTopic);
                ExecuteCommand(command);
            }
        }

        void DargDropTo(IEnumerable<ChartObject> chartObjects, Topic target, DragTopicsMethod dragMethod)
        {
            if (chartObjects.IsNullOrEmpty() || target == null || dragMethod == DragTopicsMethod.None)
                return;

            var comd = new DragDropCommand(Map.Document, chartObjects, target, dragMethod);
            this.ExecuteCommand(comd);
        }

        public void PasteAsRemark()
        {
            if (SelectedObject is IRemark && Clipboard.ContainsText())
            {
                if (!string.IsNullOrEmpty(SelectedObject.Remark))
                {
                    if (this.ShowMessage("Whether to replace the current remark?", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                        return;
                }

                SelectedObject.Remark = ClipboardHelper.GetHtml();
            }
        }

        public void PasteAsImage()
        {
            if (SelectedTopic != null && Clipboard.ContainsImage())
            {
                PictureWidget template = new PictureWidget();
                template.Image = PictureWidget.PictureDesign.FromClipboard();
                if (template.Image.Data != null)
                {
                    Size size = template.Image.Data.Size;
                    if (size.Width > 128 || size.Height > 128)
                    {
                        size = PaintHelper.SizeInSize(size, new Size(128, 128));
                        template.CustomWidth = size.Width;
                        template.CustomHeight = size.Height;
                    }

                    AddWidget(PictureWidget.TypeID, template, Helper.TestModifierKeys(Keys.Shift));
                }
            }
        }

        public void AddTopic(Topic parentTopic, Topic subTopic, int index)
        {
            var command = new AddTopicCommand(parentTopic, subTopic, index);
            ExecuteCommand(command);
        }

        public void AddTopics(Topic parentTopic, Topic[] subTopics, int index)
        {
            var command = new AddTopicCommand(parentTopic, subTopics, index);
            ExecuteCommand(command);
        }

        public void ChangeObjectText(ITextObject tobj, string newText)
        {
            ChangeTextCommand command = new ChangeTextCommand(tobj, newText);
            ExecuteCommand(command);
        }

        public override void CopyStyle(bool holdOn)
        {
            if (FormatPainter.Default.IsEmpty)
            {
                if (SelectedTopic != null)
                {
                    FormatPainter.Default.Copy(SelectedTopic);
                    FormatPainter.Default.HoldOn = holdOn;
                }
            }
            else
            {
                FormatPainter.Default.Clear();
            }
        }

        public void CustomSort(int step)
        {
            if (SelectedTopics.Length > 0)
            {
                CustomSortCommand comd = new CustomSortCommand(SelectedTopics, step);
                ExecuteCommand(comd);
            }
        }

        public void CustomSort(Topic parent, int[] newIndices)
        {
            if (parent == null || newIndices == null || newIndices.Length == 0)
                return;

            CustomSortCommand comd = new CustomSortCommand(parent, newIndices);
            ExecuteCommand(comd);
        }

        #endregion
    }
}
