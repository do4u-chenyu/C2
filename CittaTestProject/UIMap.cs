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



        /// <summary>
        /// 算子模型文档构建 - 使用“算子模型文档构建Params”将参数传递到此方法中。
        /// </summary>
        public void 算子模型文档构建()
        {
            #region Variable Declarations
            WinButton uI登录Button = this.UI用户登录主窗体.UI登录.UI登录Button;
            WinButton uI新建Button = this.UICitta主界面.UI新建.UI新建Button;
            WinEdit uITextBoxEdit = this.UI新建模型.UI我的新模型Window.UITextBoxEdit;
            WinButton uI添加Button = this.UI新建模型.UI添加.UI添加Button;
            #endregion

            //启动Citta程序
            ApplicationUnderTest citta登陆 = ApplicationUnderTest.Launch(@"D:\work\\Citta\Citta_T1\bin\开发调试\Citta_T1.exe");
            Mouse.Click(citta登陆);

            // 在 “用户名” 组合框 中选择“90”
            UI用户名.EditableItem = "90";

            // 单击 “登录” 按钮
            Mouse.Click(uI登录Button, new Point(114, 12));

            // 单击 “newModelButton” 按钮
            Mouse.Click(uI新建Button, new Point(56, 19));

            // 在 “textBox” 文本框 中键入“最大值算子测试”
            uITextBoxEdit.Text = this.算子模型文档构建Params.UITextBoxEditText;

            // 单击 “添加” 按钮
            Mouse.Click(uI添加Button, new Point(26, 12));
        }

        public virtual 算子模型文档构建Params 算子模型文档构建Params
        {
            get
            {
                if ((this.m算子模型文档构建Params == null))
                {
                    this.m算子模型文档构建Params = new 算子模型文档构建Params();
                }
                return this.m算子模型文档构建Params;
            }
        }

        private 算子模型文档构建Params m算子模型文档构建Params;

        /// <summary>
        /// 创建最大值算子模型
        /// </summary>
        public void 创建最大值算子模型()
        {
            #region Variable Declarations
            WinButton uI算子Button = this.UICitta主界面.UI算子.UI算子Button;
            WinButton uI取最大值Button = this.UICitta主界面.UI取最大值.UI取最大值Button;
            WinClient uICanvasPanelClient = this.UICitta主界面.UICanvasPanel.UICanvasPanelClient;
            WinButton uI数据Button = this.UICitta主界面.UI数据Window.UI数据Button;
            WinButton uITest_data_1Button = this.UICitta主界面.UITest_data_1Window.UITest_data_1Button;
            #endregion

            // 单击 “算子” 按钮
            Mouse.Click(uI算子Button, new Point(62, 24));

            Mouse.Hover(uI取最大值Button, uI取最大值Button.BoundingRectangle.Location, 1000);

            // 将  “取最大值” 按钮 移至 “canvasPanel” 客户端
            uICanvasPanelClient.EnsureClickable(new Point(503, 188));
            Mouse.StartDragging(uI取最大值Button, new Point(35, 17));
            Mouse.StopDragging(uICanvasPanelClient, new Point(503, 188));

            // 单击 “数据” 按钮
            Mouse.Click(uI数据Button, new Point(32, 26));

            Mouse.Hover(uITest_data_1Button, uITest_data_1Button.BoundingRectangle.Location, 1000);

            // 将  “test_data_1” 按钮 移至 “canvasPanel” 客户端
            uICanvasPanelClient.EnsureClickable(new Point(248, 195));
            Mouse.StartDragging(uITest_data_1Button, new Point(46, 16));
            Mouse.StopDragging(uICanvasPanelClient, new Point(248, 195));
        }


        /// <summary>
        /// 最大值算子配置结果生成 - 使用“最大值算子配置结果生成Params”将参数传递到此方法中。
        /// </summary>
        public void 最大值算子配置结果生成()
        {
            #region Variable Declarations
            WinClient uI设置Client = this.UICitta主界面.UI算子_设置.UI设置Client;
            WinComboBox uI取最大值ComboBox = this.UI取最大值算子设置.UI取最大值.UI取最大值ComboBox;
            WinButton uIItemButton = this.UI取最大值算子设置.UIItemWindow.UIItemButton;
            WinCheckBox uIHOSTCheckBox = this.UIItemWindow.UIItemWindow1.UIItemList.UIHOSTCheckBox;
            WinCheckBox uIREFERERCheckBox = this.UIItemWindow.UIItemWindow1.UIItemList.UIREFERERCheckBox;
            WinCheckBox uIUSERNAMECheckBox = this.UIItemWindow.UIItemWindow1.UIItemList.UIUSERNAMECheckBox;
            WinCheckBox uIPASSWORDCheckBox = this.UIItemWindow.UIItemWindow1.UIItemList.UIPASSWORDCheckBox;
            WinCheckBox uICodeCheckBox = this.UIItemWindow.UIItemWindow1.UIItemList.UICodeCheckBox;
            WinButton uI确认Button = this.UI取最大值算子设置.UI确认Window.UI确认Button;
            WinButton uI保存Button = this.UICitta主界面.UI保存.UI保存Button;
            WinButton uI取最大值Button = this.UICitta主界面.UI取最大值Window1.UI取最大值Button;
            WinMenuItem uI运行到此MenuItem = this.UIItemWindow1.UIDropDownMenu.UI运行到此MenuItem;
            #endregion

            // 双击 “statusBox” 客户端
            Mouse.DoubleClick(uI设置Client, new Point(8, 6));

            // 在 “数据信息：” 组合框 中选择“mem_num”
            uI取最大值ComboBox.SelectedItem = this.最大值算子配置结果生成Params.UI取最大值ComboBoxSelectedItem;

            // 单击 按钮
            Mouse.Click(uIItemButton, new Point(10, 1));

            // 选择 “HOST” 复选框
            uIHOSTCheckBox.Checked = this.最大值算子配置结果生成Params.UIHOSTCheckBoxChecked;

            // 选择 “REFERER” 复选框
            uIREFERERCheckBox.Checked = this.最大值算子配置结果生成Params.UIREFERERCheckBoxChecked;

            // 选择 “USERNAME” 复选框
            uIUSERNAMECheckBox.Checked = this.最大值算子配置结果生成Params.UIUSERNAMECheckBoxChecked;

            // 选择 “PASSWORD” 复选框
            uIPASSWORDCheckBox.Checked = this.最大值算子配置结果生成Params.UIPASSWORDCheckBoxChecked;

            // 选择 “code” 复选框
            uICodeCheckBox.Checked = this.最大值算子配置结果生成Params.UICodeCheckBoxChecked;

            // 单击 “确认” 按钮
            Mouse.Click(uI确认Button, new Point(34, 12));

            // 单击 “saveModelButton” 按钮
            Mouse.Click(uI保存Button, new Point(35, 14));

            // 单击 “取最大值” 按钮
            Mouse.Click(uI取最大值Button, new Point(31, 10));

            // 右-单击 “取最大值” 按钮
            Mouse.Click(uI取最大值Button, MouseButtons.Right, ModifierKeys.None, new Point(46, 9));

            // 单击 “运行到此” 菜单项
            Mouse.Click(uI运行到此MenuItem, new Point(50, 12));
        }

        public virtual 最大值算子配置结果生成Params 最大值算子配置结果生成Params
        {
            get
            {
                if ((this.m最大值算子配置结果生成Params == null))
                {
                    this.m最大值算子配置结果生成Params = new 最大值算子配置结果生成Params();
                }
                return this.m最大值算子配置结果生成Params;
            }
        }

        private 最大值算子配置结果生成Params m最大值算子配置结果生成Params;

        /// <summary>
        /// 单目算子模型测试数据导入 - 使用“单目算子模型测试数据导入Params”将参数传递到此方法中。
        /// </summary>
        public void 单目算子模型测试数据导入()
        {
            #region Variable Declarations
            WinButton uI导入Button = this.UICitta主界面.UI导入.UI导入Button;
            WinButton uI浏览Button = this.UI导入数据.UI浏览.UI浏览Button;
            WinComboBox uI文件名NComboBox = this.UI打开Window.UIItemWindow5.UI文件名NComboBox;
            WinButton uI打开OButton = this.UI打开Window.UI打开OWindow.UI打开OButton;
            WinText uIUTF8Text = this.UI导入数据.UIUTF8.UIUTF8Text;
            WinButton uI添加Button = this.UI导入数据.UI添加.UI添加Button;
            #endregion

            // 单击 “ImportButton” 按钮
            Mouse.Click(uI导入Button, new Point(40, 11));

            // 单击 “浏览” 按钮
            Mouse.Click(uI浏览Button, new Point(36, 9));

            // 在 “文件名(N):” 组合框 中选择“test_data_1.bcp”
            uI文件名NComboBox.EditableItem = this.单目算子模型测试数据导入Params.UI文件名NComboBoxEditableItem;

            // 单击 “打开(&O)” 按钮
            Mouse.Click(uI打开OButton, new Point(44, 6));

            // 单击 “UTF-8” 标签
            Mouse.Click(uIUTF8Text, new Point(26, 9));

            // 单击 “添加” 按钮
            Mouse.Click(uI添加Button, new Point(27, 26));
        }

        public virtual 单目算子模型测试数据导入Params 单目算子模型测试数据导入Params
        {
            get
            {
                if ((this.m单目算子模型测试数据导入Params == null))
                {
                    this.m单目算子模型测试数据导入Params = new 单目算子模型测试数据导入Params();
                }
                return this.m单目算子模型测试数据导入Params;
            }
        }

        private 单目算子模型测试数据导入Params m单目算子模型测试数据导入Params;
    }

    /// <summary>
    /// 要传递到“算子模型文档构建”中的参数
    /// </summary>
    [GeneratedCode("编码的 UI 测试生成器", "16.0.29514.35")]
    public class 算子模型文档构建Params
    {

        #region Fields
        /// <summary>
        /// 在 “textBox” 文本框 中键入“最大值算子测试”
        /// </summary>
        public string UITextBoxEditText = "最大值算子测试";
        #endregion
    }

    /// <summary>
    /// 要传递到“最大值算子配置结果生成”中的参数
    /// </summary>
    [GeneratedCode("编码的 UI 测试生成器", "16.0.29514.35")]
    public class 最大值算子配置结果生成Params
    {

        #region Fields
        /// <summary>
        /// 在 “数据信息：” 组合框 中选择“mem_num”
        /// </summary>
        public string UI取最大值ComboBoxSelectedItem = "mem_num";

        /// <summary>
        /// 选择 “HOST” 复选框
        /// </summary>
        public bool UIHOSTCheckBoxChecked = true;

        /// <summary>
        /// 选择 “REFERER” 复选框
        /// </summary>
        public bool UIREFERERCheckBoxChecked = true;

        /// <summary>
        /// 选择 “USERNAME” 复选框
        /// </summary>
        public bool UIUSERNAMECheckBoxChecked = true;

        /// <summary>
        /// 选择 “PASSWORD” 复选框
        /// </summary>
        public bool UIPASSWORDCheckBoxChecked = true;

        /// <summary>
        /// 选择 “code” 复选框
        /// </summary>
        public bool UICodeCheckBoxChecked = true;
        #endregion
    }
    /// <summary>
    /// 要传递到“单目算子模型测试数据导入”中的参数
    /// </summary>
    [GeneratedCode("编码的 UI 测试生成器", "16.0.29514.35")]
    public class 单目算子模型测试数据导入Params
    {

        #region Fields
        /// <summary>
        /// 在 “文件名(N):” 组合框 中选择“test_data_1.bcp”
        /// </summary>
        public string UI文件名NComboBoxEditableItem = "test_data_1.bcp";
        #endregion
    }  
}
