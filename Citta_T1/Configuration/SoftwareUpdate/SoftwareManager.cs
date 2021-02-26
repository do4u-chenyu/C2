using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2.Configuration.SoftwareUpdate
{
   public  class SoftwareManager
    {
        private static SoftwareManager softwareManager;
        public static SoftwareManager Instance
        {
            get
            {
                if (softwareManager == null)
                    softwareManager = new SoftwareManager();
                return softwareManager;
            }
        }



    }
}
