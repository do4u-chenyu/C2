using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2.WorkSpace
{
    public partial class MciWorkSpace : Component
    {
        public MciWorkSpace()
        {
            InitializeComponent();
        }

        public MciWorkSpace(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
