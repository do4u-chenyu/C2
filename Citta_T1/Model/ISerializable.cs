using System;
using System.Xml;

namespace Citta_T1.Model
{
    interface ISerializable
    {
        void Deserialize(Version documentVersion, XmlElement node);

        void Serialize(XmlDocument dom, XmlElement node);
    }
}
