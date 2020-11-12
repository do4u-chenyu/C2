using System;
using System.Reflection;
using System.Windows.Forms;

namespace C2.Core
{
    public static class ExtensionMethods

    {
        /*
         * 解决DataGridView滚动时卡顿的问题
         * 使用反射来调用被隐藏的DoubleBuffer方法
         */
        public static void DoubleBuffered(this DataGridView dgv, bool setting)

        {

            Type dgvType = dgv.GetType();

            PropertyInfo pi = dgvType.GetProperty("DoubleBuffered",

            BindingFlags.Instance | BindingFlags.NonPublic);

            pi.SetValue(dgv, setting, null);

        }
        public static void SetDouble(Control c)
        {

            c.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance |
                         System.Reflection.BindingFlags.NonPublic).SetValue(c, true, null);

        }

    }
}
