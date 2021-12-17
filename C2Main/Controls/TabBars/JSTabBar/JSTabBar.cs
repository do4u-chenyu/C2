using System.ComponentModel;
using System.Drawing;

namespace C2.Controls
{
    [DefaultEvent("SelectedItemChanged")]
    public partial class JSTabBar : TabBar
    {
       
        public JSTabBar()
        { }

        public override void ApplyTheme(UITheme theme)
        {
            base.ApplyTheme(theme);

            if (theme != null && !DesignMode)
            {
                TabRounded = theme.RoundCorner;

                SelectedItemBackColor = theme.Colors.Window; //.Sharp;
                SelectedItemForeColor = Color.DodgerBlue;    //点击TabBar控件，修改选中控件的文本颜色

                ItemBackColor = theme.Colors.Window;
                ItemForeColor = PaintHelper.FarthestColor(ItemBackColor, theme.Colors.ScrollBarColor, theme.Colors.ScrollBarColor);

                HoverItemBackColor = theme.Colors.Window;
                HoverItemForeColor = PaintHelper.FarthestColor(HoverItemBackColor, theme.Colors.ScrollBarColor, theme.Colors.Light);
               
                BaseLineColor = theme.Colors.ScrollBarColor;
            }
        }
 
        public override TabBarRenderer DefaultRenderer
        {
            get
            {
                return new JSTabBarRenderer(this);
            }
        }
        
        
     
    }
}
