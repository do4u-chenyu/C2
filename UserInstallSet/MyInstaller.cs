using System;
using System.Collections;
using System.ComponentModel;
using System.Xml;

namespace UserInstallSet
{
    [RunInstaller(true)]
    public partial class MyInstaller : System.Configuration.Install.Installer
    {
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

            string cf = ConfigRead();
            ConfigReplace(cf, path.Replace("/", String.Empty));

            //ConfigWrite(String.Format(configText, path.Replace("/", String.Empty)));
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
        private void LogWrite(string str)
        {
            string LogPath = this.Context.Parameters["targetdir"];
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(LogPath + @"setup.log", true))
            {
                sw.WriteLine(DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss] ") + str + "\n");
            }
        }

        private string ConfigRead()
        {
            string configPath = this.Context.Parameters["targetdir"];
            using (System.IO.StreamReader sr = new System.IO.StreamReader(configPath + @"C2.exe.config", false))
            {
                return sr.ReadToEnd();
            }
        }

        private void ConfigReplace(string v, string s)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(v);
            XmlNode node = doc.SelectSingleNode("/configuration/appSettings/add[@key='workspace']");
            if (node != null && node.Attributes["value"] != null)
                node.Attributes["value"].Value = s;

            string configPath = System.IO.Path.Combine(this.Context.Parameters["targetdir"], @"C2.exe.config");

            doc.Save(configPath);
        }

    }
}
