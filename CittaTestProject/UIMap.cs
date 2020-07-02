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
        public WinEdit uI名称Edit;
        public WinComboBox uI用户名ComboBox;
        public WinButton uI登录Button;
        public WinButton uI新建Button;
        public WinButton uI添加Button;
        public WinButton uI导入Button;
        public WinButton uI浏览Button;
        public WinEdit uI名称Edit1;
        public WinButton uI打开OButton;
        public WinText uIUTF8Text;
        public WinButton uI添加Button1;
        public WinButton uI中英文测试Button;
        public WinClient uICanvasPanelClient;
        public WinButton uI算子Button;
        public WinButton uI取最大值Button;
        public WinClient uIItem0Client;
        public WinClient uIMoveOpControlClient;
        public WinClient uIStatusBoxClient;
        public WinComboBox uI数据信息ComboBox;
        public WinWindow uIItemWindow;
        public WinButton uIItemButton;
        public WinCheckBox uI姓名CheckBox;
        public WinClient uI数据信息Client;
        public WinButton uI确认Button;
        public WinButton uI保存Button;
        public WinTitleBar uI烽火FiberHomeTitleBar;
        public WinButton uI数据Button;
        public UIMap()
        {
            #region Variable Declarations
            uI名称Edit = this.UI开发调试Window.UIItemWindow.UICitta_T1exeListItem.UI名称Edit;
            uI用户名ComboBox = this.UI用户登录Window.UIUserNameComboBoxWindow.UI用户名ComboBox;
            uI登录Button = this.UI用户登录Window.UI登录Window.UI登录Button;
            uI新建Button = this.UI烽火FiberHomeWindow.UINewModelButtonWindow.UI新建Button;
            uI添加Button = this.UI新建模型Window.UI添加Window.UI添加Button;
            uI导入Button = this.UI烽火FiberHomeWindow.UIImportButtonWindow.UI导入Button;
            uI浏览Button = this.UI导入数据Window.UI浏览Window.UI浏览Button;
            uI名称Edit1 = this.UI打开Window.UIItemWindow.UI中英文测试bcpListItem.UI名称Edit;
            uI打开OButton = this.UI打开Window.UI打开OWindow.UI打开OButton;
            uIUTF8Text = this.UI导入数据Window.UIUTF8Window.UIUTF8Text;
            uI添加Button1 = this.UI导入数据Window.UI添加Window.UI添加Button;
            uI中英文测试Button = this.UI烽火FiberHomeWindow.UI中英文测试Window.UI中英文测试Button;
            uICanvasPanelClient = this.UI烽火FiberHomeWindow.UICanvasPanelWindow.UICanvasPanelClient;
            uI算子Button = this.UI烽火FiberHomeWindow.UI算子Window.UI算子Button;
            uI取最大值Button = this.UI烽火FiberHomeWindow.UI取最大值Window.UI取最大值Button;
            uIItem0Client = this.UI烽火FiberHomeWindow.UIMoveDtControlWindow1.UIItem0Client;
            uIMoveOpControlClient = this.UI烽火FiberHomeWindow.UIMoveOpControlWindow1.UIMoveOpControlClient;
            uIStatusBoxClient = this.UI烽火FiberHomeWindow.UIStatusBoxWindow.UIStatusBoxClient;
            uI数据信息ComboBox = this.UI取最大值算子设置Window.UIComboBox0Window.UI数据信息ComboBox;
            uIItemWindow = this.UI取最大值算子设置Window.UIOutListCCBL0Window.UIItemWindow;
            uIItemButton = this.UI取最大值算子设置Window.UIItemWindow.UIItemButton;
            uI姓名CheckBox = this.UIItemWindow.UIItemWindow1.UIItemList.UI姓名CheckBox;
            uI数据信息Client = this.UI取最大值算子设置Window.UIValuePanelWindow.UI数据信息Client;
            uI确认Button = this.UI取最大值算子设置Window.UI确认Window.UI确认Button;
            uI保存Button = this.UI烽火FiberHomeWindow.UISaveModelButtonWindow.UI保存Button;
            uI烽火FiberHomeTitleBar = this.UI烽火FiberHomeWindow.UI烽火FiberHomeTitleBar;
            uI数据Button = this.UI烽火FiberHomeWindow.UI数据Window.UI数据LeftButton;
            #endregion
        }

        /// <summary>
        /// ExampleTestMethod - 使用“ExampleTestMethodParams”将参数传递到此方法中。
        /// </summary>
        public void CreateNewModelTest()
        {

            // 双击 “名称” 文本框
            Mouse.DoubleClick(uI名称Edit, new Point(14, 8));

            // 在 “用户名” 组合框 中选择“tests1”
            uI用户名ComboBox.EditableItem = this.ExampleTestMethodParams.UI用户名ComboBoxEditableItem;

            // 单击 “登录” 按钮
            Mouse.Click(uI登录Button, new Point(116, 8));

            // 单击 “newModelButton” 按钮
            Mouse.Click(uI新建Button, new Point(39, 21));

            // 单击 “添加” 按钮
            Mouse.Click(uI添加Button, new Point(15, 16));

            // 单击 “ImportButton” 按钮
            //Mouse.Click(uI导入Button, new Point(55, 13));

            // 单击 “浏览” 按钮
            //Mouse.Click(uI浏览Button, new Point(21, 23));

            // 单击 “名称” 文本框
            // Mouse.Click(uI名称Edit1, new Point(36, 16));

            // 单击 “打开(&O)” 按钮
            //Mouse.Click(uI打开OButton, new Point(25, 12));

            // 单击 “UTF-8” 标签
            //Mouse.Click(uIUTF8Text, new Point(30, 13));

            // 单击 “添加” 按钮
            // Mouse.Click(uI添加Button1, new Point(34, 18));

            // 单击 “数据” 按钮
            Mouse.Click(uI数据Button);

            // 数据悬停在数据源上
            Mouse.Hover(uI中英文测试Button, uI中英文测试Button.BoundingRectangle.Location,1000);

            // 将  “中英文测试” 按钮 移至 “canvasPanel” 客户端
            uICanvasPanelClient.EnsureClickable(new Point(130, 144));
            Mouse.StartDragging(uI中英文测试Button, new Point(58, 17));
            Mouse.StopDragging(uICanvasPanelClient, new Point(130, 144));

            // 单击 “算子” 按钮
            Mouse.Click(uI算子Button, new Point(67, 18));

            // 数据悬停在最大值算子上
            Mouse.Hover(uI取最大值Button, uI取最大值Button.BoundingRectangle.Location,1000);

            // 将  “取最大值” 按钮 移至 “canvasPanel” 客户端
            uICanvasPanelClient.EnsureClickable(new Point(265, 146));
            Mouse.StartDragging(uI取最大值Button, new Point(62, 19));
            Mouse.StopDragging(uICanvasPanelClient, new Point(265, 146));

            // 将  “0%” 客户端 移至 “MoveOpControl” 客户端
            //uIMoveOpControlClient.EnsureClickable(new Point(6, 9));
            //Mouse.StartDragging(uIItem0Client, new Point(127, 11));
            //Mouse.StopDragging(uIMoveOpControlClient, new Point(6, 9));

            // 双击 “statusBox” 客户端
            //Mouse.DoubleClick(uIStatusBoxClient, new Point(5, 6));

            // 在 “数据信息：” 组合框 中选择“name”
            //uI数据信息ComboBox.SelectedItem = this.ExampleTestMethodParams.UI数据信息ComboBoxSelectedItem;

            // 单击 窗口
            //Mouse.Click(uIItemWindow, new Point(149, 11));

            // 单击 按钮
            //Mouse.Click(uIItemButton, new Point(11, 9));

            // 选择 “姓名” 复选框
            //uI姓名CheckBox.Checked = this.ExampleTestMethodParams.UI姓名CheckBoxChecked;

            // 单击 “数据信息：” 客户端
            //Mouse.Click(uI数据信息Client, new Point(165, 89));

            // 单击 “确认” 按钮
            //Mouse.Click(uI确认Button, new Point(10, 17));

            // 单击 “saveModelButton” 按钮
            Mouse.Click(uI保存Button, new Point(44, 24));

            // 单击 “烽火FiberHome” 标题栏
            Mouse.Click(uI烽火FiberHomeTitleBar, new Point(1343, 12));
        }

        public virtual ExampleTestMethodParams ExampleTestMethodParams
        {
            get
            {
                if ((this.mExampleTestMethodParams == null))
                {
                    this.mExampleTestMethodParams = new ExampleTestMethodParams();
                }
                return this.mExampleTestMethodParams;
            }
        }

        private ExampleTestMethodParams mExampleTestMethodParams;
    }
    /// <summary>
    /// 要传递到“ExampleTestMethod”中的参数
    /// </summary>
    [GeneratedCode("编码的 UI 测试生成器", "16.0.29514.35")]
    public class ExampleTestMethodParams
    {

        #region Fields
        /// <summary>
        /// 在 “用户名” 组合框 中选择“tests1”
        /// </summary>
        public string UI用户名ComboBoxEditableItem = "tests1";

        /// <summary>
        /// 在 “数据信息：” 组合框 中选择“name”
        /// </summary>
        public string UI数据信息ComboBoxSelectedItem = "name";

        /// <summary>
        /// 选择 “姓名” 复选框
        /// </summary>
        public bool UI姓名CheckBoxChecked = true;
        #endregion
    }
}
