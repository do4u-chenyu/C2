using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Citta_T1.Utils
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

    }
}
