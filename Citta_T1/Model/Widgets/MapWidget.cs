using C2.Dialogs;
using C2.IAOLab.WebEngine.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace C2.Model.Widgets
{
    class MapWidget : C2BaseWidget, IRemark
    {
        public const string TypeID = "MAP";
        public MapConfig MapConfig;

        [Browsable(false)]
        public string WebUrl { set; get; }

        public MapWidget()
        {
            DisplayIndex = 9;
            Alignment = WidgetAlignment.Right;//默认位置改成右侧,让图标挂件和主题文字紧挨着
            widgetIcon = Properties.Resources.地图;
            MapConfig = new MapConfig();
        }

        public override string GetTypeID()
        {
            return TypeID;
        }
        /// <summary>
        /// 序列化MapConfig
        /// </summary>
        /// <param name="dom"></param>
        /// <param name="node"></param>
        public override void Serialize(XmlDocument dom, XmlElement node)
        {
            base.Serialize(dom, node);
            XmlElement mapConfigNode = node.OwnerDocument.CreateElement("MapConfig");
            XmlElement overlapConfigNodes = node.OwnerDocument.CreateElement("overlapConfigNodes");
            XmlElement sourceCodeNode = node.OwnerDocument.CreateElement("sourceCodeNode");
            node.AppendChild(mapConfigNode);
            mapConfigNode.AppendChild(sourceCodeNode);
            mapConfigNode.AppendChild(overlapConfigNodes);
            sourceCodeNode.InnerText = MapConfig.SourceCode;
            mapConfigNode.SetAttribute("InitLat", MapConfig.InitLat.ToString());
            mapConfigNode.SetAttribute("InitLng", MapConfig.InitLng.ToString());
            mapConfigNode.SetAttribute("Level", MapConfig.Level.ToString());

            foreach (OverlapConfig oc in MapConfig.OverlapConfigList)
            {
                XmlElement overlapConfigNode = node.OwnerDocument.CreateElement("overlapConfigNode");
                overlapConfigNodes.AppendChild(overlapConfigNode);
                overlapConfigNode.SetAttribute("LatIndex", oc.LatIndex.ToString());
                overlapConfigNode.SetAttribute("LngIndex", oc.LngIndex.ToString());
                overlapConfigNode.SetAttribute("WeightIndex", oc.WeightIndex.ToString());
                WriteDataItem(overlapConfigNode, new List<DataItem>() { oc.DataItem }, "DataItems");
                overlapConfigNode.SetAttribute("OverlapType", oc.OverlapType.ToString());
            }
        }

        public override void Deserialize(Version documentVersion, XmlElement node)
        {
            base.Deserialize(documentVersion, node);
            this.MapConfig = new MapConfig();
            XmlElement mapConfigNode = node.SelectSingleNode("MapConfig") as XmlElement;
            float.TryParse(mapConfigNode.GetAttribute("InitLat"), out this.MapConfig.InitLat);
            float.TryParse(mapConfigNode.GetAttribute("InitLng"), out this.MapConfig.InitLng);
            int.TryParse(mapConfigNode.GetAttribute("Level"), out this.MapConfig.Level);
            this.MapConfig.SourceCode = mapConfigNode.SelectSingleNode("sourceCodeNode").InnerText;
            foreach(XmlElement xe in mapConfigNode.SelectSingleNode("overlapConfigNodes").SelectNodes("overlapConfigNode"))
            {
                List<DataItem> dis = new List<DataItem>();
                ReadAttribute(xe.SelectSingleNode("DataItems").SelectNodes("DataItem"), dis);
                try
                {
                    if (dis.Count != 0)
                    {
                        OverlapConfig oc = new OverlapConfig(
                            int.Parse(xe.GetAttribute("LatIndex")),
                            int.Parse(xe.GetAttribute("LngIndex")),
                            int.Parse(xe.GetAttribute("WeightIndex")),
                            dis[0],
                            (OverlapType)Enum.Parse(typeof(OverlapType), xe.GetAttribute("OverlapType"))
                            );
                        this.MapConfig.OverlapConfigList.Add(oc);
                    }
                }
                catch { }
            }
        }
    }
}
