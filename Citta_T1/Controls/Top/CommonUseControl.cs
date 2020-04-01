using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Citta_T1.Business.Model;

namespace Citta_T1.Controls.Top
{
    public partial class CommonUseControl : UserControl
    {
        public CommonUseControl()
        {
            InitializeComponent();
        }
        private void CommonUse_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                DataObject dragDropData = new DataObject();
                dragDropData.SetData("Type", ElementType.Operator);
                dragDropData.SetData("Path", "");
                dragDropData.SetData("Text", NameTranslate((sender as Button).Name));
                this.connectOpButton.DoDragDrop(dragDropData, DragDropEffects.Copy | DragDropEffects.Move);
            }
        }
        private string NameTranslate(string name)
        {
            String text = "";
            switch (name)
            {
                case "connectOpButton":
                    text = "连接算子";
                    break;
                case "interOpButton":
                    text = "取交集";
                    break;
                case "UnionButton":
                    text = "取并集";
                    break;
                case "diffButton":
                    text = "取差集";
                    break;
                case "filterButton":
                    text = "过滤算子";
                    break;
                case "RandomButton":
                    text = "随机采样";
                    break;
            }
            return text;
        }


    }
}
