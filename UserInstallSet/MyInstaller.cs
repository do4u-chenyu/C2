﻿using System;
using System.Collections;
using System.ComponentModel;

namespace UserInstallSet
{
    [RunInstaller(true)]
    public partial class MyInstaller : System.Configuration.Install.Installer
    {
        private String configText =
        "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n" +
        "<configuration>\r\n" +
        "  <configSections>" +
        "    <sectionGroup name=\"userSettings\" type=\"System.Configuration.UserSettingsGroup, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\">" +
        "      <section name=\"C2.Properties.Settings\" type=\"System.Configuration.ClientSettingsSection, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\" allowExeDefinition=\"MachineToLocalUser\" requirePermission=\"false\" />" +
        "    </sectionGroup>" +
        "  </configSections>" +
        "    <startup>\r\n" +
        "        <supportedRuntime version=\"v4.0\" sku=\".NETFramework,Version=v4.5\"/>\r\n" +
        "    </startup>\r\n" +
        "    <appSettings>\r\n" +
        "      <add key = \"workspace\" value=\"{0}\"/>\r\n" +
        "      <add key = \"RunLevel\" value=\"NoLogin\"/>\r\n" +
        "      <add key=\"EPPlus:ExcelPackage.LicenseContext\" value=\"NonCommercial\" />\r\n" +
        "      <add key=\"ClientSettingsProvider.ServiceUri\" value=\"\" />\r\n" +
        "      <add key=\"IAOLab\" value=\"BigAPK, APK, BaseStation, Wifi, Card, Tude, Ip \"/>\r\n" +
        "      <add key=\"version\" value=\"1.1.3\"/>\r\n" +
        "    </appSettings>\r\n" +
        "    <userSettings>\r\n" +
        "      <C2.Properties.Settings>\r\n" +
        "       <setting name=\"longitude\" serializeAs=\"String\">\r\n" +
        "         <value>118.744288</value>\r\n" +
        "       </setting>\r\n" +
        "       <setting name = \"latitude\" serializeAs=\"String\">\r\n" +
        "         <value>31.996022</value>\r\n" +
        "       </setting>\r\n" +
        "       <setting name = \"scale\" serializeAs=\"String\">\r\n" +
        "         <value>19</value>\r\n" +
        "       </setting>\r\n" +
        "       <setting name = \"baiduAPIKey\" serializeAs=\"String\">\r\n" +
        "         <value>FtB873TFjPPzgs7M3fs4oxTPqxr7MGn9</value>\r\n" +
        "       </setting>\r\n" +
        "     </C2.Properties.Settings>\r\n" +
        "    </userSettings>\r\n" +
        "</configuration>";
        //APK:非法APK； BaseStation：基站查询； Wifi：WiFi查询； Card：银行卡查询； Tude：经纬度坐标转换； Ip：时间Ip转换；
        public MyInstaller()
        {
            InitializeComponent();
        }
        protected override void OnAfterInstall(IDictionary savedState)
        {
            LogWrite("OnAfterInstall！");
            string path = this.Context.Parameters["targetdir"];
            //获取用户设定的安装目标路径, 注意，需要在Setup项目里面自定义操作的属性栏里面的CustomActionData添加上/targetdir="[TARGETDIR]\"
            LogWrite("用户安装目录" + path);
            path = this.Context.Parameters["space"];
            LogWrite("模型空间" + path);
            ConfigWrite(path.Replace("/", ""));
            base.OnAfterInstall(savedState);
        }
        public override void Install(IDictionary stateSaver)
        {
            LogWrite("Install！");
            base.Install(stateSaver);
        }
        protected override void OnBeforeInstall(IDictionary savedState)
        {
            LogWrite("OnBeforeInstall!");
            base.OnBeforeInstall(savedState);
        }
        public override void Uninstall(IDictionary savedState)
        {
            LogWrite("Uninstall!");
            base.Uninstall(savedState);
        }
        public override void Rollback(IDictionary savedState)
        {
            LogWrite("Rollback");
            base.Rollback(savedState);
        }
        public void LogWrite(string str)
        {
            string LogPath = this.Context.Parameters["targetdir"];
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(LogPath + @"setup.log", true))
            {
                sw.WriteLine(DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss] ") + str + "\n");
            }
        }
        public void ConfigWrite(string str)
        {
            string configPath = this.Context.Parameters["targetdir"];
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(configPath + @"C2.exe.config", false))
            {
                sw.WriteLine(String.Format(configText, str));
            }
        }


    }
}
