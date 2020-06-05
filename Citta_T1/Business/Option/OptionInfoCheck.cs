using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Citta_T1.Business.Option
{
    public class OptionInfoCheck
    {
        #region 配置窗口不合法字符判断
       
        public void IsIllegalInputName(Control control, String[] columnName, String name)
        {
            if (columnName.Count() == 0 || name == "") return;
            if (!columnName.Contains(name))
            {
                control.Text = "";
                MessageBox.Show("未输入正确列名，请从下拉列表中选择正确列名");
            }
        }
        public void Control_Leave(object sender, EventArgs e)
        {
            List<string> columnName = new List<string>();
            foreach (var item in (sender as ComboBox).Items)
            {
                columnName.Add(item.ToString());
            }
            IsIllegalInputName((sender as ComboBox), columnName.ToArray(), (sender as ComboBox).Text);
        }
        public void Control_KeyUp(object sender, KeyEventArgs e)
        {
            List<string> columnName = new List<string>();
            foreach (var item in (sender as ComboBox).Items)
            {
                columnName.Add(item.ToString());
            }
            if (e.KeyCode == Keys.Enter)
                IsIllegalInputName((sender as ComboBox), columnName.ToArray(), (sender as ComboBox).Text);
        }
        public void IsIllegalCharacter(object sender, EventArgs e)
        {

            if ((sender as TextBox).Text.Contains("\\t"))
            {
                (sender as TextBox).Text = "";
                MessageBox.Show("输入非法字'\\t'，请重新输入过滤条件");
            }
        }
        #endregion
    }
}
