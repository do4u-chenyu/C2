﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Test.UC
{
    public partial class UCTestNavigationMenuOfficeItem : UserControl
    {
        public UCTestNavigationMenuOfficeItem(string str)
        {
            InitializeComponent();
            label1.Text = str;
        }
    }
}
