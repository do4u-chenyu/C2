using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using C2.Utils;

namespace C2Shell
{
    class Program
    {
        static void Main()
        {

            SoftwareUpdate updateInstance = new SoftwareUpdate();
            if (updateInstance.IsNeedUpdate())
            {
                if (!updateInstance.ExecuteUpdate(updateInstance.ZipName))
                    updateInstance.Rollback();
                updateInstance.Clean();
            }
            updateInstance.StartCoreProcess();
        }
       
    }
}
