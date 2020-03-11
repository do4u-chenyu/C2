using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Citta_T1.Business
{
    class DataSource
    {
        private string path;
        private string UserInfoPath;
        public DataSource()
        {
            this.path = Directory.GetCurrentDirectory().ToString() + "\\cittaModelDocument\\";
            this.UserInfoPath = path + "\\UserInformation.xml";
        }
        public void WriteUserInfo(string userName)
        {
         
        }
    }
}
