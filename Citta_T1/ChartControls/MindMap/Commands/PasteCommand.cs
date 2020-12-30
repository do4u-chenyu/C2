using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using C2.Core;
using C2.Core.Documents;
using C2.Model;
using C2.Model.Documents;
using C2.Model.MindMaps;
using C2.Model.Widgets;

namespace C2.Controls.MapViews
{
    class PasteCommand : Command
    {
        Topic Topic;
        ChartObject[] PasteObjects;
        object ClipboardData;

        public PasteCommand(Document document, Topic topic)
        {
            /*
             * 需要保存
             */
            Document = document;
            Topic = topic;

            if (Topic == null)
                throw new ArgumentNullException();
        }

        public override string Name
        {
            get { return "Paste"; }
        }

        public Document Document { get; private set; }

        public override bool Rollback()
        {
            /*
             * 不是真的删除Topic，而是存起来
             */
            AfterSelection = null;

            if (PasteObjects != null)
            {
                foreach (var obj in PasteObjects)
                {
                    //撤回的时候，原先是直接判断为topic，存在bug
                    if(obj is Topic)
                    {
                        Topic topic = obj as Topic;
                        if (topic.ParentTopic != null)
                        {
                            topic.ParentTopic.Children.Remove(topic);
                        }
                    }
                    else if(obj is Widget)
                    {
                        Widget widget = obj as Widget;
                        (widget.Container as Topic).Remove(widget);
                    }

                }

                //PasteObjects = null;
                return true;
            }
            else
            {
                return false;
            }
        }
        public override bool Redo()
        {
            /* Topic在Undo的时候不能真的删掉
             * 需要存起来，Redo的时候直接用这个Topic，而不是使用Clipboard data 新建一个
             * Redo的时候，不能直接把粘贴板的数据弄过来
             */
            AfterSelection = null;

            if (ClipboardData is string)
            {
                return PasteText(Topic, ClipboardData as string, true);
            }
            else if (ClipboardData is object)
            {
                return PasteTopic(Topic, ClipboardData, true);
            }
            else
            {
                return false;
            }
        }
        public override bool Execute()
        {
            AfterSelection = null;

            if (Clipboard.ContainsData(typeof(MapClipboardData).ToString()))
            {
                return PasteTopic(Topic, Clipboard.GetData(typeof(MapClipboardData).ToString()));
            }
            else if (Clipboard.ContainsText())
            {
                return PasteText(Topic, Clipboard.GetText());
            }
            else
            {
                return false;
            }
        }

        bool PasteText(Topic topic, string text, bool isRedo=false)
        {
            if (!string.IsNullOrEmpty(text))
            {
                ClipboardData = text;
                var ts = new TextSerializer();
                var topics = isRedo ? PasteObjects : ts.DeserializeTopic(text); 
                if (topics != null && topics.Length > 0)
                {
                    foreach (Topic ct in topics)
                    {
                        topic.Children.Add(ct);
                    }
                    PasteObjects = topics;
                    AfterSelection = topics;
                }
            }

            return true;
        }

        bool PasteTopic(Topic topic, object data, bool isRedo=false)
        {
            if (data is MapClipboardData)
            {
                ChartObject[] chartObjects = null;
                if (isRedo)
                {
                    chartObjects = PasteObjectsTo(PasteObjects, Document, topic); // Redo的时候不可以直接粘贴，需要把PasteObjects直接贴进去
                }
                else
                {
                    var tcd = (MapClipboardData)data;
                    //var topics = tcd.GetTopics();
                    chartObjects = PasteObjectsTo(tcd, Document, topic); // Redo的时候不可以直接粘贴，需要把PasteObjects直接贴进去
                }
                if (chartObjects.IsNullOrEmpty())
                    return true;

                PasteObjects = chartObjects;
                AfterSelection = chartObjects;

                if (ClipboardData == null)
                    ClipboardData = data;
                return true;
            }
            else
            {
                return false;
            }
        }

        public static ChartObject[] PasteObjectsTo(MapClipboardData data, Document document, Topic target)
        {
            var chartObjects = data.GetChartObjects();
            return PasteObjectsTo(chartObjects, document, target);
        }
        public static ChartObject[] PasteObjectsTo(ChartObject[] chartObjects, Document document, Topic target)
        {
            if (!chartObjects.IsNullOrEmpty())
            {
                var newids = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                foreach (var co in chartObjects)
                {
                    if (!string.IsNullOrEmpty(co.ID))
                    {
                        string id = co.ID;
                        co.ID = document.GetNextObjectID();
                        newids[id] = co.ID;
                    }
                }

                foreach (var co in chartObjects)
                {
                    if (co is Topic)
                    {
                        var t = (Topic)co;
                        target.Children.Add(t);//增加节点
                        t.Widgets.RemoveAll(w => w is C2BaseWidget);
                        for (int j = t.Links.Count - 1; j >= 0; j--)
                        {
                            Link line = t.Links[j];
                            t.Links.Remove(line);
                        }

                        //t的所有子孙节点也要移除C2挂件、link
                        foreach (Topic ct in t.GetAllChildren())
                        {
                            ct.Widgets.RemoveAll(w => w is C2BaseWidget);
                            for (int j = ct.Links.Count - 1; j >= 0; j--)
                            {
                                Link line = ct.Links[j];
                                ct.Links.Remove(line);
                            }
                        }

                    }
                    else if (!(co is C2BaseWidget))
                    {    
                        target.Widgets.Add((Widget)co);
                    }
                }
            }

            return chartObjects;
        }
    }
}
