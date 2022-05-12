using C2.Business.CastleBravo.VPN;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using v2rayN.Mode;

namespace v2rayN.Handler
{
    class V2rayConfigHandler
    {

        public static string GenerateClientSpeedtestConfigString(List<ListViewItem> lv, int startPort)
        {
            V2rayConfig v2rayConfig = Utils.FromJson<V2rayConfig>(Utils.GetSampleClientEmbedText());

            // 创建 Inbounds  区块
            GenerateV2rayInbounds(lv, startPort, v2rayConfig);
            // 创建 Outbounds 区块
            GenerateV2rayOutbounds(lv, startPort, v2rayConfig);
            // 创建 in 和 out 路由关系
            GenerateV2rayRoutingRules(v2rayConfig);

            // return Utils.ToJson(v2rayConfig);

            StreamReader sr = new StreamReader(@"C:\Users\quixote\Desktop\熊猫网络5用户\验活测试.v2rayconfig.v1.txt");
            return sr.ReadToEnd();
        }
        private static void GenerateV2rayRoutingRules(V2rayConfig v2rayConfig)
        {
            // 路由不对称,肯定是哪里出错了
            if (v2rayConfig.inbounds.Count != v2rayConfig.outbounds.Count)
                return;

            int count = v2rayConfig.inbounds.Count;

            for (int i = 0; i < count; i++)
            {
                RulesItem rule = new RulesItem
                {
                    inboundTag = new List<string> { v2rayConfig.inbounds[i].tag },
                    outboundTag = v2rayConfig.outbounds[i].tag,
                    type = "field"
                };
                v2rayConfig.routing.rules.Add(rule);
            }
        }

        private static void GenerateV2rayInbounds(List<ListViewItem> lv, int startPort, V2rayConfig v2rayConfig)
        {
            for (int i = 0; i < lv.Count; i++)
            {
                Inbounds inbound = new Inbounds
                {
                    listen = Global.Loopback,
                    port = startPort + i,
                    protocol = Global.InboundHttp
                };

                inbound.tag = Global.InboundHttp + inbound.port.ToString();
                v2rayConfig.inbounds.Add(inbound);
            }
        }

        private static void GenerateV2rayOutbounds(List<ListViewItem> lv, int startPort, V2rayConfig v2rayConfig)
        {
            for (int i = 0; i < lv.Count; i++)
                GenOutbound(lv[i].Tag as VPNTaskConfig, startPort + i, v2rayConfig);
        }

        private static void GenOutbound(VPNTaskConfig vtc, int port, V2rayConfig v2rayConfig)
        {
            // Outbounds是个复杂的嵌套结构体,从零手动构造太麻烦
            // 直接从模板文件里复制出来一个用
            V2rayConfig v2rayConfigCopy = Utils.FromJson<V2rayConfig>(Utils.GetSampleClientEmbedText());
            // TODO 这里要照抄 v2ray的 outbound 函数
            Outbound(vtc, ref v2rayConfigCopy);

            v2rayConfigCopy.outbounds[0].tag = Global.agentTag + port.ToString();
            v2rayConfig.outbounds.Add(v2rayConfigCopy.outbounds[0]);
        }

        private static void Outbound(VPNTaskConfig vtc, ref V2rayConfig v2rayConfig)
        {
            _ = vtc;
            // TODO
            _ = v2rayConfig;
        }
    }
}
