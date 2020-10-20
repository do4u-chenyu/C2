using System;
using System.Xml;

namespace C2.Model
{
    interface ISerializable
    {
        void Deserialize(Version documentVersion, XmlElement node);

        void Serialize(XmlDocument dom, XmlElement node);
    }
}
