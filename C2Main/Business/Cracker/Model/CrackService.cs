using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2.Business.Cracker.Model
{
    public abstract class CrackService
    {
        public abstract Server creack(String ip, int port, String username, String password, int timeOut);
    }
}
