using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2.SearchToolkit
{
    class TaskInfo
    {
        public static readonly String GambelModelDescription = "胶水系统全文涉赌后台模型";
        public static readonly String GunModelDescription = "胶水系统全文涉枪模型";
        public static readonly String YellowModelDescription = "胶水系统全文涉黄模型";
        public static readonly String PlaneModelDescription = "胶水系统飞机场模型";

        public String Username {  get; private set; }
        public String Password { get; private set; }
        public String BastionIP { get; private set; }
        public String SearchAgentIP { get; private set; }

        public String ChosenTask { get; private set; }

        public String RemoteWorkspace { get; private set; }

        public String TaskStatus { get; private set; }

        public String TaskPID { get; private set; }

        public String TaskName { get; private set; }
    }
}
