using System;
using C2.Core;
using C2.Model.Widgets;
using C2.Model;
using C2.Model.MindMaps;
using System.Collections.Generic;

namespace C2.Controls.MapViews
{
    class DeleteCommand : Command
    {
        ChartObject[] MapObjects;
        Dictionary<ChartObject, object> Parents;
        Dictionary<ChartObject, int> Indices;
        Dictionary<ChartObject, List<Link>> Links;

        public DeleteCommand(ChartObject[] mapObjects)
        {
            MapObjects = mapObjects;

            if (MapObjects == null || MapObjects.Length == 0)
            {
                throw new ArgumentNullException();
            }
        }

        public override string Name
        {
            get { return "Delete"; }
        }

        public override bool Rollback()
        {
            return UndeleteObjects(MapObjects, Parents, Indices, Links, false);
        }
        public override bool Redo()
        {
            return Execute();
        }
        public override bool Execute()
        {
            Parents = new Dictionary<ChartObject, object>();
            Indices = new Dictionary<ChartObject, int>();
            Links = new Dictionary<ChartObject, List<Link>>();
            return DeleteObjects(MapObjects, Parents, Indices, Links, false);
        }

        public static bool DeleteObjects(ChartObject[] mapObjects,
            Dictionary<ChartObject, object> parents, Dictionary<ChartObject, int> indices, Dictionary<ChartObject, List<Link>> links, bool isDragDrop)
        {
            bool changed = false;
            
            for (int i = 0; i < mapObjects.Length; i++)
            {
                ChartObject mo = mapObjects[i];
                if (mo is Link)
                {
                    Link line = (Link)mo;
                    if (line.From != null)
                    {
                        parents[mo] = line.From;
                        line.From.Links.Remove(line);
                        changed = true;
                    }
                }
                else if (mo is Topic)
                {
                    Topic topic = (Topic)mapObjects[i];
                    if (topic.ParentTopic != null)
                    {
                        parents[mo] = topic.ParentTopic;
                        indices[mo] = topic.Index;

                        //不是移动的时候
                        //先拿到所有link，找到to为该topic的link，记录一下，删掉
                        //还有该topic的子孙topic的link也要全部删除
                        if (!isDragDrop)
                        {
                            links[mo] = topic.MindMap.GetLinks(false).FindAll(s => s.Target == topic);
                            foreach (Topic child in topic.GetAllChildren())
                            {
                                links[mo].AddRange(child.MindMap.GetLinks(false).FindAll(s => s.Target == child));
                            }
                            foreach (Link link in links[mo])
                            {
                                link.From.Links.Remove(link);
                            }
                        }


                        topic.ParentTopic.Children.Remove(topic);
                        changed = true;
                    }
                }
                else if (mo is Widget)
                {
                    Widget widget = (Widget)mapObjects[i];
                    if (widget.WidgetContainer != null)
                    {
                        parents[mo] = widget.WidgetContainer;
                        indices[mo] = widget.WidgetContainer.IndexOf(widget);

                        widget.WidgetContainer.Remove(widget);
                        changed = true;
                    }
                }
            }

            return changed;
        }

        public static bool UndeleteObjects(ChartObject[] MapObjects, Dictionary<ChartObject, object> Parents, Dictionary<ChartObject, int> Indices, Dictionary<ChartObject, List<Link>> Links,bool isDragDrop)
        {
            if (MapObjects.Length > 0)
            {
                for (int i = 0; i < MapObjects.Length; i++)
                {
                    ChartObject mo = MapObjects[i];
                    object parent = null;
                    int index = -1;
                    List<Link> links = null;
                    Parents.TryGetValue(mo, out parent);
                    Indices.TryGetValue(mo, out index);
                    Links.TryGetValue(mo, out links);

                    if (mo is Topic)
                    {
                        Topic topic = (Topic)mo;

                        if (parent is Topic)
                        {
                            Topic parentTopic = (Topic)parent;
                            if (!isDragDrop && links != null)
                            {
                                foreach(Link link in links)
                                {
                                    link.From.Links.Add(link);
                                }
                            }
                                
                            if (index > -1 && index < parentTopic.Children.Count)
                                parentTopic.Children.Insert(index, topic);
                            else
                                parentTopic.Children.Add(topic);
                        }
                    }
                    else if (mo is Link)
                    {
                        if (parent is Topic)
                        {
                            Topic parentTopic = (Topic)parent;
                            parentTopic.Links.Add((Link)mo);
                        }
                    }
                    else if (mo is Widget)
                    {
                        if (parent is IWidgetContainer)
                        {
                            var container = (IWidgetContainer)parent;
                            if (index > -1 && index < container.WidgetsCount)
                                container.Insert(index, (Widget)mo);
                            else
                                container.Add((Widget)mo);
                        }
                    }
                }
            }


            return true;
        }
    }
}
