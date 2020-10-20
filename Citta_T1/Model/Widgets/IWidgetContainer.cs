using System;

namespace C2.Model.Widgets
{
    public interface IWidgetContainer
    {
        void Add(Widget widget);

        bool Remove(Widget widget);

        Widget[] FindWidgets(WidgetAlignment alignment);

        int IndexOf(Widget widget);

        void Insert(int index, Widget widget);

        int WidgetsCount { get; }
    }
}
