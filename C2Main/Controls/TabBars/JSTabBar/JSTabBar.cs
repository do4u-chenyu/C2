using System.ComponentModel;
using System.Drawing;

namespace C2.Controls
{
    [DefaultEvent("SelectedItemChanged")]
    public partial class JSTabBar : TabBar
    {
       
        public JSTabBar()
        {
            
        }
        public override int setX(TabItem ti)
        {
            return ti.Size.Width + ItemSpace + 30; //调整各个Tabitem之间的距离
        }
        public override void ApplyTheme(UITheme theme)
        {
            base.ApplyTheme(theme);

            if (theme != null && !DesignMode)
            {
                TabRounded = theme.RoundCorner;

                SelectedItemBackColor = theme.Colors.Window;//.Sharp;
                SelectedItemForeColor = Color.DodgerBlue;//点击TabBar控件，修改选中控件的文本颜色
                //SelectedItemForeColor = theme.Colors.WindowText;// PaintHelper.FarthestColor(SelectedItemBackColor, theme.Colors.Dark, theme.Colors.Light);// theme.Colors.SharpText;
                //ItemBackColor = theme.Colors.MediumLight;
                ItemBackColor = theme.Colors.Window;
                //ItemForeColor = PaintHelper.FarthestColor(ItemBackColor, theme.Colors.Dark, theme.Colors.Light);
                ItemForeColor = PaintHelper.FarthestColor(ItemBackColor, theme.Colors.ScrollBarColor, theme.Colors.ScrollBarColor);
                //HoverItemBackColor = theme.Colors.Sharp;
                HoverItemBackColor = theme.Colors.Window;
                //HoverItemForeColor = PaintHelper.FarthestColor(HoverItemBackColor, theme.Colors.Dark, theme.Colors.Light);
                HoverItemForeColor = PaintHelper.FarthestColor(HoverItemBackColor, theme.Colors.ScrollBarColor, theme.Colors.Light);
                //BaseLineColor = theme.Colors.BorderColor;
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
