using Citta_T1.Business.Model;
using Citta_T1.Core;
using Citta_T1.Utils;
using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Citta_T1.Controls.Move
{
    public partial class MoveBaseControl : UserControl
    {
        //
        //  此类正在施工中,暂时没有引用，会有编译器报错
        //
        public ElementType Type { get; set; }
        public int ID { get; set; }
        public string Description { get => this.textBox.Text; set => this.textBox.Text = value; }
        public DSUtil.Encoding Encoding { get; set; }
        public char Separator { get; set ; }
        public ElementStatus Status { get; set; }
        public string FullFilePath { get; set; }

        public DSUtil.ExtType ExtType
        {
            get
            {
                if (String.IsNullOrWhiteSpace(FullFilePath))
                    return DSUtil.ExtType.Unknow;
                if (FullFilePath.EndsWith(".xlsx", true, System.Globalization.CultureInfo.CurrentCulture))
                    return DSUtil.ExtType.Excel;
                if (FullFilePath.EndsWith(".xls", true, System.Globalization.CultureInfo.CurrentCulture))
                    return DSUtil.ExtType.Excel;
                if (FullFilePath.EndsWith(".txt", true, System.Globalization.CultureInfo.CurrentCulture))
                    return DSUtil.ExtType.Text;
                if (FullFilePath.EndsWith(".bcp", true, System.Globalization.CultureInfo.CurrentCulture))
                    return DSUtil.ExtType.Text;
                if (FullFilePath.EndsWith(".cvs", true, System.Globalization.CultureInfo.CurrentCulture))
                    return DSUtil.ExtType.Text;
                return DSUtil.ExtType.Unknow;
            }
        }

        private string oldTextString;

        //private ECommandType cmd;

        public MoveBaseControl()
        {
            //cmd = ECommandType.Null;
            oldTextString = String.Empty;
            InitializeComponent();
        }

        
    }
}
