using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2.SearchToolkit
{
    class TaskInfo
    {
        public String Username { private get; set; }
        public String Password { private get; set; }
        public String BastionIP { private get; set; }
        public String SearchAgentIP { private get; set; }

        public String ChosenTask { private get; set; }
    }
}
