using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

namespace C2.Database
{
    class Settings
    {
        /// <summary>
        /// The path to the settings file
        /// </summary>
        public string SettingsFilePath
        {
            get
            {
                return SettingsFolder + "/OracleExplorerSettings.xml";
            }
        }

        /// <summary>
        /// The folder that the settings file is in (no trailing slash)
        /// </summary>
        public string SettingsFolder
        {
            get
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/OracleExplorer";
            }
        }

        public XmlDocument xmldoc;
        public XmlElement root;
    }
}
