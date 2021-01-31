using System;
using System.Xml;

namespace C2.Utils
{
    class XmlUtil
    {
        private static readonly LogUtil log = LogUtil.GetInstance("XmlUtil");
        public static string GetInnerText(XmlNode node, string nodeName)
        {
            string text = String.Empty;
            try
            {
                if (node.SelectSingleNode(nodeName) == null)
                    return text;
                text = node.SelectSingleNode(nodeName).InnerText;
            }
            catch (Exception e)
            {
                log.Error("DocumentSaveLoad 读取InnerText: " + e.Message);
            }
            return text;
        }
    }
}
