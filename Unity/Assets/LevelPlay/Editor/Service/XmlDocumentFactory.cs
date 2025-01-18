using System;

namespace Unity.Services.LevelPlay.Editor
{
    internal class XmlDocument : IXmlDocument
    {
        private readonly System.Xml.XmlDocument m_XmlDocument = new System.Xml.XmlDocument();

        public void Load(string filename)
        {
            m_XmlDocument.Load(filename);
        }

        public void LoadXml(string xml)
        {
            m_XmlDocument.LoadXml(xml);
        }

        #nullable enable
        public System.Xml.XmlNode ? SelectSingleNode(string xpath)
        {
            return m_XmlDocument.SelectSingleNode(xpath);
        }
        #nullable disable

        public void Save(string filename)
        {
            m_XmlDocument.Save(filename);
        }
    }

    internal class XmlDocumentFactory : IXmlDocumentFactory
    {
        public IXmlDocument CreateXmlDocument()
        {
            return new XmlDocument();
        }
    }
}
