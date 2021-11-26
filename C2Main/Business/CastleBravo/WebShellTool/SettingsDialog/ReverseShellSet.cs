using C2.Core;

namespace C2.Business.CastleBravo.WebShellTool
{
    partial class ReverseShellSet : MSFSet
    {
        public ReverseShellSet(WebShellTaskConfig taskConfig, ProxySetting proxy) : base(taskConfig, proxy)
        {
            this.Text = "反弹Shell配置";
            this.addr.Text = "反弹地址:";

            this.payload = Global.ReverseShellPayload;
            this.RemoteHost = Global.ReverseShellHost;
        }

        protected override void SetRemoteHost()
        {
            Global.ReverseShellHost = RemoteHost.Trim();     
        }
    }
}
