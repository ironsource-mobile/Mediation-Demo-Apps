using System;
using System.Xml;

namespace Unity.Services.LevelPlay.Editor
{
    internal interface IXmlDocument
    {
        void Load(string filename);
        void LoadXml(string xml);
        #nullable enable
        System.Xml.XmlNode ? SelectSingleNode(string xpath);
        #nullable disable
        void Save(string filename);
    }

    internal interface IXmlDocumentFactory
    {
        IXmlDocument CreateXmlDocument();
    }
}
