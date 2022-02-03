using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2.Business.Cracker.Model
{
    class ServiceModel
    {
        public String Name = string.Empty;
        public String Port = string.Empty;
        public String DicUserNamePath = string.Empty;
        public String DicPasswordPath = string.Empty;
        public HashSet<String> ListUserName = new HashSet<string>();
        public HashSet<String> ListPassword = new HashSet<string>();
    }
}
