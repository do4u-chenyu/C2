using Microsoft.VisualStudio.TestTools.UnitTesting;
using C2.Business.SSH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace C2.Business.SSH.Tests
{
    [TestClass()]
    public class BastionAPITests
    {
        [TestMethod()]
        public void DownloadGambleTaskResultTest()
        {
            BastionAPI api = new BastionAPI(SearchToolkit.SearchTaskInfo.EmptyTaskInfo);
            api.Login().DownloadTaskResult(String.Empty);
            Assert.Fail();
        }

        private const String configText =
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
"      <add key = \"workspace\" value=\"C:\\FiberHomeIAOModelDocument\"/>\r\n" +
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

        [TestMethod()]
        public  void TestConfig()
        {

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(configText);
            XmlNode node = doc.SelectSingleNode("/configuration/appSettings/add[@key='workspace']");
            if (node != null && node.Attributes["value"] != null)
                node.Attributes["value"].Value = @"D:\work1";

            _= doc.ToString();
            d
        }
    }
}