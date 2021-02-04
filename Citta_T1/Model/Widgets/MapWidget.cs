using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2.Model.Widgets
{
    class MapWidget : C2BaseWidget, IRemark
    {
        public const string TypeID = "MAP";
        public MapWidget()
        {
            DisplayIndex = 9;
            Alignment = WidgetAlignment.Right;//默认位置改成右侧,让图标挂件和主题文字紧挨着
            widgetIcon = Properties.Resources.地图;
        }

        public override string GetTypeID()
        {
            return TypeID;
        }
    }
}
