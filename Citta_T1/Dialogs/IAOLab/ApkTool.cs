using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using C2.Controls;

namespace C2.Dialogs.IAOLab
{
    public partial class ApkTool : BaseDialog
    {
        public ApkTool()
        {
            InitializeComponent();

        }
        public void ExactApk()
        {
            // 运行JAR包

            GetApkInfo();
            GetApkSize();

            //先不用生成result,读取需要的数据加载到内存，并控件预览框展示


            // 删除临时结果文件
        }
        private void GetApkInfo()
        {

        }
        private void GetApkSize()
        {

        }
        public void ExporResult()
        {
            // 右键导出，生成结果文件到excel
            // python的save方法调用
        }
    }
}
