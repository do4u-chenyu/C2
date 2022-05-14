using C2.Business.CastleBravo.VPN;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using v2rayN.Mode;

namespace v2rayN.Handler
{
    class V2rayConfigHandler
    {

        public static string GenerateClientSpeedtestConfigString(List<ListViewItem> lv, int startPort)
        {
            V2rayConfig v2rayConfig = Utils.FromJson<V2rayConfig>(Utils.GetSampleClientEmbedText());
            
            // 先把从样例模板中创建的config清空一下
            ResetV2rayConfig(v2rayConfig);

            // 创建 Inbounds  区块
            GenerateV2rayInbounds(lv, startPort, v2rayConfig);

            // 创建 Outbounds 区块
            GenerateV2rayOutbounds(lv, startPort, v2rayConfig);

            // 创建 in 和 out 路由关系
            GenerateV2rayRoutingRules(v2rayConfig);

            return Utils.ToJson(v2rayConfig);

        }
        private static void GenerateV2rayRoutingRules(V2rayConfig v2rayConfig)
        {
            int count = Math.Min(v2rayConfig.inbounds.Count, v2rayConfig.outbounds.Count);

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

        private static void ResetV2rayConfig(V2rayConfig v2rayConfig)
        {
            v2rayConfig.inbounds.Clear();
            v2rayConfig.outbounds.Clear();
            v2rayConfig.routing.rules.Clear();
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

        /// <summary>
        /// 构造Outbound,从v2ray源码里拿来改造的
        /// 原来的就又臭又长
        /// </summary>
        /// <param name="vtc"></param>
        /// <param name="v2rayConfig"></param>
        private static void Outbound(VPNTaskConfig vtc, ref V2rayConfig v2rayConfig)
        {
            _ = vtc;

            Outbounds outbound = v2rayConfig.outbounds[0];
            if (vtc.configType() == EConfigType.Vmess)
            {
                VnextItem vnextItem;
                if (outbound.settings.vnext.Count <= 0)
                {
                    vnextItem = new VnextItem();
                    outbound.settings.vnext.Add(vnextItem);
                }
                else
                {
                    vnextItem = outbound.settings.vnext[0];
                }
                //远程服务器地址和端口
                vnextItem.address = vtc.address();
                vnextItem.port = vtc.port();

                UsersItem usersItem;
                if (vnextItem.users.Count <= 0)
                {
                    usersItem = new UsersItem();
                    vnextItem.users.Add(usersItem);
                }
                else
                {
                    usersItem = vnextItem.users[0];
                }
                //远程服务器用户ID
                usersItem.id = vtc.id();
                usersItem.alterId = vtc.alterId();
                usersItem.email = Global.userEMail;
                usersItem.security = vtc.security();

                //Mux
                outbound.mux.enabled = vtc.muxEnabled;
                outbound.mux.concurrency = vtc.muxEnabled ? 8 : -1;

                //远程服务器底层传输配置
                StreamSettings streamSettings = outbound.streamSettings;
                boundStreamSettings(vtc, "out", ref streamSettings);

                outbound.protocol = Global.vmessProtocolLite;
                outbound.settings.servers = null;
            }
            else if (vtc.configType() == EConfigType.Shadowsocks)
            {
                ServersItem serversItem;
                if (outbound.settings.servers.Count <= 0)
                {
                    serversItem = new ServersItem();
                    outbound.settings.servers.Add(serversItem);
                }
                else
                {
                    serversItem = outbound.settings.servers[0];
                }
                //远程服务器地址和端口
                serversItem.address = vtc.address();
                serversItem.port = vtc.port();
                serversItem.password = vtc.id();
                serversItem.method = vtc.security();

                serversItem.ota = false;
                serversItem.level = 1;

                outbound.mux.enabled = false;
                outbound.mux.concurrency = -1;


                outbound.protocol = Global.ssProtocolLite;
                outbound.settings.vnext = null;
            }
            else if (vtc.configType() == EConfigType.Socks)
            {
                ServersItem serversItem;
                if (outbound.settings.servers.Count <= 0)
                {
                    serversItem = new ServersItem();
                    outbound.settings.servers.Add(serversItem);
                }
                else
                {
                    serversItem = outbound.settings.servers[0];
                }
                //远程服务器地址和端口
                serversItem.address = vtc.address();
                serversItem.port = vtc.port();
                serversItem.method = null;
                serversItem.password = null;

                if (!Utils.IsNullOrEmpty(vtc.security())
                    && !Utils.IsNullOrEmpty(vtc.id()))
                {
                    SocksUsersItem socksUsersItem = new SocksUsersItem
                    {
                        user = vtc.security(),
                        pass = vtc.id(),
                        level = 1
                    };

                    serversItem.users = new List<SocksUsersItem>() { socksUsersItem };
                }

                outbound.mux.enabled = false;
                outbound.mux.concurrency = -1;

                outbound.protocol = Global.socksProtocolLite;
                outbound.settings.vnext = null;
            }
            else if (vtc.configType() == EConfigType.VLESS)
            {
                VnextItem vnextItem;
                if (outbound.settings.vnext.Count <= 0)
                {
                    vnextItem = new VnextItem();
                    outbound.settings.vnext.Add(vnextItem);
                }
                else
                {
                    vnextItem = outbound.settings.vnext[0];
                }
                //远程服务器地址和端口
                vnextItem.address = vtc.address();
                vnextItem.port = vtc.port();

                UsersItem usersItem;
                if (vnextItem.users.Count <= 0)
                {
                    usersItem = new UsersItem();
                    vnextItem.users.Add(usersItem);
                }
                else
                {
                    usersItem = vnextItem.users[0];
                }
                //远程服务器用户ID
                usersItem.id = vtc.id();
                usersItem.alterId = 0;
                usersItem.flow = string.Empty;
                usersItem.email = Global.userEMail;
                usersItem.encryption = vtc.security();

                //Mux
                outbound.mux.enabled = vtc.muxEnabled;
                outbound.mux.concurrency = vtc.muxEnabled ? 8 : -1;

                //远程服务器底层传输配置
                StreamSettings streamSettings = outbound.streamSettings;
                boundStreamSettings(vtc, "out", ref streamSettings);

                //if xtls
                if (vtc.streamSecurity() == Global.StreamSecurityX)
                {
                    if (Utils.IsNullOrEmpty(vtc.flow()))
                    {
                        usersItem.flow = "xtls-rprx-origin";
                    }
                    else
                    {
                        usersItem.flow = vtc.flow();
                    }

                    outbound.mux.enabled = false;
                    outbound.mux.concurrency = -1;
                }

                outbound.protocol = Global.vlessProtocolLite;
                outbound.settings.servers = null;
            }
            else if (vtc.configType() == EConfigType.Trojan)
            {
                ServersItem serversItem;
                if (outbound.settings.servers.Count <= 0)
                {
                    serversItem = new ServersItem();
                    outbound.settings.servers.Add(serversItem);
                }
                else
                {
                    serversItem = outbound.settings.servers[0];
                }
                //远程服务器地址和端口
                serversItem.address = vtc.address();
                serversItem.port = vtc.port();
                serversItem.password = vtc.id();

                serversItem.ota = false;
                serversItem.level = 1;

                outbound.mux.enabled = false;
                outbound.mux.concurrency = -1;


                //远程服务器底层传输配置
                StreamSettings streamSettings = outbound.streamSettings;
                boundStreamSettings(vtc, "out", ref streamSettings);

                outbound.protocol = Global.trojanProtocolLite;
                outbound.settings.vnext = null;
            }
            // TODO
            _ = v2rayConfig;
        }


        /// <summary>
        /// vmess协议远程服务器底层传输配置
        /// </summary>
        /// <param name="config"></param>
        /// <param name="iobound"></param>
        /// <param name="streamSettings"></param>
        /// <returns></returns>
        private static int boundStreamSettings(VPNTaskConfig vtc, string iobound, ref StreamSettings streamSettings)
        {
            try
            {
                //远程服务器底层传输配置
                streamSettings.network = vtc.network();
                string host = vtc.requestHost();
                //if tls
                if (vtc.streamSecurity() == Global.StreamSecurity)
                {
                    streamSettings.security = vtc.streamSecurity();

                    TlsSettings tlsSettings = new TlsSettings
                    {
                        allowInsecure = vtc.allowInsecure()
                    };
                    if (!string.IsNullOrWhiteSpace(host))
                    {
                        tlsSettings.serverName = Utils.String2List(host)[0];
                    }
                    streamSettings.tlsSettings = tlsSettings;
                }

                //if xtls
                if (vtc.streamSecurity() == Global.StreamSecurityX)
                {
                    streamSettings.security = vtc.streamSecurity();

                    TlsSettings xtlsSettings = new TlsSettings
                    {
                        allowInsecure = vtc.allowInsecure()
                    };
                    if (!string.IsNullOrWhiteSpace(host))
                    {
                        xtlsSettings.serverName = Utils.String2List(host)[0];
                    }
                    streamSettings.xtlsSettings = xtlsSettings;
                }

                //streamSettings
                switch (vtc.network())
                {
                    //kcp基本配置暂时是默认值，用户能自己设置伪装类型
                    case "kcp":
                        KcpSettings kcpSettings = new KcpSettings
                        {
                            mtu = vtc.kcpItem.mtu,
                            tti = vtc.kcpItem.tti
                        };
                        if (iobound.Equals("out"))
                        {
                            kcpSettings.uplinkCapacity = vtc.kcpItem.uplinkCapacity;
                            kcpSettings.downlinkCapacity = vtc.kcpItem.downlinkCapacity;
                        }
                        else if (iobound.Equals("in"))
                        {
                            kcpSettings.uplinkCapacity = vtc.kcpItem.downlinkCapacity; ;
                            kcpSettings.downlinkCapacity = vtc.kcpItem.downlinkCapacity;
                        }
                        else
                        {
                            kcpSettings.uplinkCapacity = vtc.kcpItem.uplinkCapacity;
                            kcpSettings.downlinkCapacity = vtc.kcpItem.downlinkCapacity;
                        }

                        kcpSettings.congestion = vtc.kcpItem.congestion;
                        kcpSettings.readBufferSize = vtc.kcpItem.readBufferSize;
                        kcpSettings.writeBufferSize = vtc.kcpItem.writeBufferSize;
                        kcpSettings.header = new Header
                        {
                            type = vtc.headerType()
                        };
                        if (!Utils.IsNullOrEmpty(vtc.path()))
                        {
                            kcpSettings.seed = vtc.path();
                        }
                        streamSettings.kcpSettings = kcpSettings;
                        break;
                    //ws
                    case "ws":
                        WsSettings wsSettings = new WsSettings
                        {
                            connectionReuse = true
                        };

                        string path = vtc.path();
                        if (!string.IsNullOrWhiteSpace(host))
                        {
                            wsSettings.headers = new Headers
                            {
                                Host = host
                            };
                        }
                        if (!string.IsNullOrWhiteSpace(path))
                        {
                            wsSettings.path = path;
                        }
                        streamSettings.wsSettings = wsSettings;

                        //TlsSettings tlsSettings = new TlsSettings();
                        //tlsSettings.allowInsecure = config.allowInsecure();
                        //if (!string.IsNullOrWhiteSpace(host))
                        //{
                        //    tlsSettings.serverName = host;
                        //}
                        //streamSettings.tlsSettings = tlsSettings;
                        break;
                    //h2
                    case "h2":
                        HttpSettings httpSettings = new HttpSettings();

                        if (!string.IsNullOrWhiteSpace(host))
                        {
                            httpSettings.host = Utils.String2List(host);
                        }
                        httpSettings.path = vtc.path();

                        streamSettings.httpSettings = httpSettings;

                        //TlsSettings tlsSettings2 = new TlsSettings();
                        //tlsSettings2.allowInsecure = config.allowInsecure();
                        //streamSettings.tlsSettings = tlsSettings2;
                        break;
                    //quic
                    case "quic":
                        QuicSettings quicsettings = new QuicSettings
                        {
                            security = host,
                            key = vtc.path(),
                            header = new Header
                            {
                                type = vtc.headerType()
                            }
                        };
                        streamSettings.quicSettings = quicsettings;
                        if (vtc.streamSecurity() == Global.StreamSecurity)
                        {
                            streamSettings.tlsSettings.serverName = vtc.address();
                        }
                        break;
                    default:
                        //tcp带http伪装
                        if (vtc.headerType().Equals(Global.TcpHeaderHttp))
                        {
                            TcpSettings tcpSettings = new TcpSettings
                            {
                                connectionReuse = true,
                                header = new Header
                                {
                                    type = vtc.headerType()
                                }
                            };

                            if (iobound.Equals("out"))
                            {
                                //request填入自定义Host
                                string request = Utils.GetSampleHttprequestEmbedText();
                                string[] arrHost = host.Split(',');
                                string host2 = string.Join("\",\"", arrHost);
                                request = request.Replace("$requestHost$", string.Format("\"{0}\"", host2));
                                //request = request.Replace("$requestHost$", string.Format("\"{0}\"", config.requestHost()));

                                //填入自定义Path
                                string pathHttp = @"/";
                                if (!Utils.IsNullOrEmpty(vtc.path()))
                                {
                                    string[] arrPath = vtc.path().Split(',');
                                    pathHttp = string.Join("\",\"", arrPath);
                                }
                                request = request.Replace("$requestPath$", string.Format("\"{0}\"", pathHttp));
                                tcpSettings.header.request = Utils.FromJson<object>(request);
                            }
                            else if (iobound.Equals("in"))
                            {
                                //string response = Utils.GetEmbedText(Global.v2raySampleHttpresponseFileName);
                                //tcpSettings.header.response = Utils.FromJson<object>(response);
                            }

                            streamSettings.tcpSettings = tcpSettings;
                        }
                        break;
                }
            }
            catch
            {
            }
            return 0;
        }
    }
}
