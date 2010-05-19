using System.Xml;

namespace IUDICO.DataModel.Common.ImportUtils
{
    /// <summary>
    /// Constants of to work with xml file (manifest.xml etc)
    /// </summary>
    public sealed class XmlUtility
    {
        public static XmlNode GetNode(XmlNode node, string path)
        {
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(node.OwnerDocument.NameTable);
            if (node.OwnerDocument.DocumentElement.HasAttribute("xmlns"))
            {
                nsmgr.AddNamespace("ns", node.OwnerDocument.DocumentElement.Attributes["xmlns"].Value);
            }
            return node.SelectSingleNode(path, nsmgr);
        }

        public static XmlNodeList GetNodes(XmlNode node, string path)
        {
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(node.OwnerDocument.NameTable);
            if (node.OwnerDocument.DocumentElement.HasAttribute("xmlns"))
            {
                nsmgr.AddNamespace("ns", node.OwnerDocument.DocumentElement.Attributes["xmlns"].Value);
            }
            return node.SelectNodes(path, nsmgr);
        }
        
        public static XmlNode GetNodeById(XmlNodeList nodeList, string Id)
        {
            foreach(XmlNode node in nodeList)
            {
              if (GetId(node)==Id)
              {
                return node;
              }
            }
            return null;
        }

        public static string GetId(XmlNode node)
        {
            if (node.Attributes != null)
                if (node.Attributes[XmlAttributes.Id] != null)
                    return node.Attributes[XmlAttributes.Id].Value;

            return null;
        }
        
        public static string GetIdentifier(XmlNode node)
        {
            if (node.Attributes != null)
                if (node.Attributes[XmlAttributes.Identifier] != null)
                    return node.Attributes[XmlAttributes.Identifier].Value;

            return null;
        }

        public static string GetIdentifierRef(XmlNode node)
        {
            if (node.Attributes != null)
                if (node.Attributes[XmlAttributes.Reference] != null)
                    return node.Attributes[XmlAttributes.Reference].Value;

            return null;
        }
    }
}