using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2.Business.Cracker.rdp
{
    public enum ResponseType
    {
        Connecting,
        LoggedIn,
        LoginFailed,
        TimedOut,
        Connected,
        Disconnected,
        Finished,
        Error
    }
}
