using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2.Business.CastleBravo.VPN.Info
{
    public enum VPNProbeType
    {
        None,
        Random,    // 随机探针
        Replay,    // 重放探针
        Password   // 密码探针
    }
}
