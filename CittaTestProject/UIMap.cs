namespace CittaTestProject
{
    using Microsoft.VisualStudio.TestTools.UITesting.WinControls;
    using System;
    using System.Collections.Generic;
    using System.CodeDom.Compiler;
    using Microsoft.VisualStudio.TestTools.UITest.Extension;
    using Microsoft.VisualStudio.TestTools.UITesting;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;
    using Mouse = Microsoft.VisualStudio.TestTools.UITesting.Mouse;
    using MouseButtons = System.Windows.Forms.MouseButtons;
    using System.Drawing;
    using System.Windows.Input;
    using System.Text.RegularExpressions;


    public partial class UIMap
    {
        #region Variable Declarations
        public WinButton UI登录;
        public WinButton UI我的模型Left;
        public WinButton UI数据;
        public WinButton UI算子;
        public WinButton UIIAO实验室Left;
        public WinButton UIAI实践;
        public WinButton UI多源算子;
        public WinButton UIPython算子;
        public WinButton UI关联算子;
        public WinButton UI碰撞算子;
        public WinButton UI取并集;
        public WinButton UI取差集;
        public WinButton UI随机采样;
        public WinButton UI条件筛选;
        public WinButton UI取最大值;
        public WinButton UI取最小值;
        public WinButton UI取平均值;
        public WinButton UI频率算子;
        public WinButton UI排序算子;
        public WinButton UI分组算子;
        public WinButton UI关键词过滤;
        public WinButton UI数据标准化;
        public WinButton UI新建;
        public WinButton UI添加;
        public WinButton UI导入;
        public WinButton UI浏览;
        public WinButton UI关闭;
        public WinText UIGBK;
        public WinButton UI取消;
        public WinButton UI全部保存;
        public WinButton UI一键排版;
        public WinClient UI拖动;
        public WinClient UI放大;
        public WinClient UI缩小;
        public WinClient UI备注;
        public WinClient UI框选;
        public WinButton UI运行;
        public WinButton UI终止;
        public WinButton UI重置;
        public WinText UI数据预览;
        public WinText UI运行日志;
        public WinText UI控制台;
        public WinClient UI隐藏底层面板;
        public WinComboBox UI用户名;
        public WinClient UICanvasPanel;
        public WinButton UI保存模型;
        #endregion

        public UIMap()
        {
            // 登陆窗体
            UI登录 = this.UI用户登录主窗体.UI登录.UI登录Button;
            UI用户名 = this.UI用户登录主窗体.UI用户名.UI用户名ComboBox;

            // 左侧任务栏
            UI我的模型Left = this.UICitta主界面.UI左侧_我的模型.UI我的模型LeftButton;
            UI数据 = this.UICitta主界面.UI数据Window.UI数据Button;
            UI算子 = this.UICitta主界面.UI算子.UI算子Button;
            UIIAO实验室Left = this.UICitta主界面.UIIAO实验室.UIIAO实验室LeftButton;

            // 算子列表
            UIAI实践 = this.UICitta主界面.UIAI实践算子.UIAI实践Button;
            UI多源算子 = this.UICitta主界面.UI多源算子.UI多源算子Button;
            UIPython算子 = this.UICitta主界面.UIPython算子.UIPython算子Button;
            UI关联算子 = this.UICitta主界面.UI关联算子.UI关联算子Button;
            UI碰撞算子 = this.UICitta主界面.UI碰撞算子.UI碰撞算子Button;
            UI取并集 = this.UICitta主界面.UI取并集.UI取并集Button;
            UI取差集 = this.UICitta主界面.UI取差集.UI取差集Button;
            UI随机采样 = this.UICitta主界面.UI随机采样.UI随机采样Button;
            UI条件筛选 = this.UICitta主界面.UI条件筛选.UI条件筛选Button;
            UI取最大值 = this.UICitta主界面.UI取最大值.UI取最大值Button;
            UI取最小值 = this.UICitta主界面.UI取最小值.UI取最小值Button;
            UI取平均值 = this.UICitta主界面.UI取平均值.UI取平均值Button;
            UI频率算子 = this.UICitta主界面.UI频率算子.UI频率算子Button;
            UI排序算子 = this.UICitta主界面.UI排序算子.UI排序算子Button;
            UI分组算子 = this.UICitta主界面.UI分组算子.UI分组算子Button;
            UI关键词过滤 = this.UICitta主界面.UI关键词过滤.UI关键词过滤Button;
            UI数据标准化 = this.UICitta主界面.UI数据标准化.UI数据标准化Button;

            //上侧菜单栏
            UI全部保存 = this.UICitta主界面.UI全部保存.UI全部保存Button;
            UI一键排版 = this.UICitta主界面.UI一键排版.UI一键排版Button;
            UI新建 = this.UICitta主界面.UI新建.UI新建Button;
            UI添加 = this.UI新建模型.UI添加.UI添加Button;
            UI保存模型 = this.UICitta主界面.UI保存.UI保存Button;

            UI导入 = this.UICitta主界面.UI导入.UI导入Button;
            UI浏览 = this.UI导入数据.UI浏览.UI浏览Button;
            UIGBK = this.UI导入数据.UIGBKWindow.UIGBKText;
            UI取消 = this.UI导入数据.UI取消Window.UI取消Button;

            // 系统自带
            UI关闭 = this.UICitta主界面.UI烽火FiberHomeTitleBar.UI关闭Button;


            // 浮动工具栏
            UI拖动 = this.UICitta主界面.UI浮动工具栏_拖动.UI拖动Client;
            UI放大 = this.UICitta主界面.UI浮动工具栏_放大.UI放大Client;
            UI缩小 = this.UICitta主界面.UI浮动工具栏_缩小.UI缩小Client;
            UI备注 = this.UICitta主界面.UI浮动工具栏_备注.UI备注Client;
            UI框选 = this.UICitta主界面.UI浮动工具栏_框选.UI框选Client;

            // 运算控件
            UI运行 = this.UICitta主界面.UI运行.UI运行Button;
            UI终止 = this.UICitta主界面.UI终止.UI终止Button;
            UI重置 = this.UICitta主界面.UI重置.UI重置Button;

            // 底层面板
            UI数据预览 = this.UICitta主界面.UI数据预览.UI数据预览Text;
            UI运行日志 = this.UICitta主界面.UI运行日志.UI运行日志Text;
            UI控制台 = this.UICitta主界面.UI控制台.UI控制台Text;
            UI隐藏底层面板 = this.UICitta主界面.UI隐藏底层面板.UI隐藏底层面板Client;

            // CanvasPanel
            UICanvasPanel = this.UICitta主界面.UICanvasPanel.UICanvasPanelClient;
        }

        public void ExampleTest()
        {

            // 单击 列表框
            //Mouse.Click(uIItemList, new Point(1242, 423));

            //启动Citta程序
            ApplicationUnderTest citta登陆 = ApplicationUnderTest.Launch(@"D:\work\\Citta\Citta_T1\bin\开发调试\Citta_T1.exe");
            Mouse.Click(citta登陆);

            // 在 “用户名” 组合框 中选择“90”
            UI用户名.EditableItem = "90";

            // 单击 “登录” 按钮
            Mouse.Click(UI登录, new Point(130, 12));

            // 单击 “算子” 按钮
            Mouse.Click(UI算子, new Point(61, 25));

            // 鼠标悬停在取差集算子
            Mouse.Hover(UI取差集, UI取差集.BoundingRectangle.Location, 1000);

            // 将  “取差集” 按钮 移至 “canvasPanel” 客户端
            UICanvasPanel.EnsureClickable(new Point(252, 115));
            Mouse.StartDragging(UI取差集, new Point(58, 17));
            Mouse.StopDragging(UICanvasPanel, new Point(252, 115));

            // 鼠标悬停在取最大值算子
            Mouse.Hover(UI取最大值, UI取最大值.BoundingRectangle.Location, 1000);

            // 将  “取最大值” 按钮 移至 “canvasPanel” 客户端
            UICanvasPanel.EnsureClickable(new Point(394, 170));
            Mouse.StartDragging(UI取最大值, new Point(44, 28));
            Mouse.StopDragging(UICanvasPanel, new Point(394, 170));

            // 单击 “saveModelButton” 按钮
            Mouse.Click(UI保存模型, new Point(25, 9));

            // 单击 “关闭”
            Mouse.Click(UI关闭);
        }

       
      
    }
}
