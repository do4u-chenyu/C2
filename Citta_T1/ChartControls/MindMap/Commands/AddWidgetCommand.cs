using Citta_T1.Core;
using Citta_T1.Model;
using Citta_T1.Model.Documents;
using Citta_T1.Model.MindMaps;
using Citta_T1.Model.Widgets;

namespace Citta_T1.Controls.MapViews
{
    class AddWidgetCommand : Command
    {
        Topic[] Topics;
        Widget[] Widgets;
        Widget Template;
        string WidgetType;

        public AddWidgetCommand(Topic[] topics, string widgetType, Widget template)
        {
            Topics = topics;
            WidgetType = widgetType;
            Template = template;
        }

        public override string Name
        {
            get { return "Add Widget"; }
        }

        public override bool Rollback()
        {
            if (Topics == null || Topics.Length == 0 || Widgets == null || Widgets.Length != Topics.Length)
                return false;

            for (int i = 0; i < Topics.Length; i++)
            {
                Topic topic = Topics[i];
                if (topic.Widgets.Contains(Widgets[i]))
                    topic.Widgets.Remove(Widgets[i]);
            }

            return true;
        }

        public override bool Execute()
        {
            if (Topics == null || Topics.Length == 0)
                return false;

            Widgets = new Widget[Topics.Length];
            for (int i = 0; i < Topics.Length; i++)
            {
                Topic topic = Topics[i];
                Widget widget = Widget.Create(WidgetType);
                Widgets[i] = widget;
                if (widget != null)
                {
                    if (Template != null)
                        Template.CopyTo(widget);
                    topic.Widgets.Add(widget);
                    widget.OnAddByCommand(topic);
                }
            }

            return true;
        }
    }
}
