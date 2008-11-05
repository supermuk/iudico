using System.Xml;

namespace IUDICO.DataModel.Common
{
    public sealed class XmlUtility
    {
        public static bool isChapter(XmlNode node)
        {
            return (getPageType(node) == PageType.Chapter);
        }

        public static bool isControlChapter(XmlNode node)
        {
            return (getPageType(node) == PageType.ControlChapter);
        }

        public static bool isTheory(XmlNode node)
        {
            return (getPageType(node) == PageType.Theory);
        }

        public static bool isPractice(XmlNode node)
        {
            return (getPageType(node) == PageType.Practice);
        }

        public static bool isPage(XmlNode node)
        {
            return isTheory(node) || isPractice(node);
        }

        public static bool isItem(XmlNode node)
        {
            return (node.Name == "item");
        }

        public static bool isLanguage(XmlNode node)
        {
            return (node.Name == "language");
        }

        public static bool isTestCase(XmlNode node)
        {
            return (node.Name == "testcase");
        }

        public static string getIdentifier(XmlNode node)
        {
            if (node.Attributes != null)
                if (node.Attributes[XmlAttributes.Identifier] != null)
                    return node.Attributes[XmlAttributes.Identifier].Value;

            return null;
        }

        public static string getId(XmlNode node)
        {
            if (node.Attributes != null)
                if (node.Attributes[XmlAttributes.Id] != null)
                    return node.Attributes[XmlAttributes.Id].Value;

            return null;
        }

        public static string getIdentifierRef(XmlNode node)
        {
            if (node.Attributes != null)
                if (node.Attributes[XmlAttributes.Reference] != null)
                    return node.Attributes[XmlAttributes.Reference].Value;

            return null;
        }

        public static string getPageType(XmlNode node)
        {
            if (node.Attributes != null)
                if (node.Attributes[XmlAttributes.PageType] != null)
                    return node.Attributes[XmlAttributes.PageType].Value;

            return null;
        }

        public static int getTimeLimit(XmlNode node)
        {
            if (node.Attributes != null)
                if (node.Attributes[XmlAttributes.TimeLimit] != null)
                    return int.Parse(node.Attributes[XmlAttributes.TimeLimit].Value);

            return 0;
        }

        public static int getMemoryLimit(XmlNode node)
        {
            if (node.Attributes != null)
                if (node.Attributes[XmlAttributes.MemoryLimit] != null)
                    return int.Parse(node.Attributes[XmlAttributes.MemoryLimit].Value);

            return 0;
        }

        public static int getOutputLimit(XmlNode node)
        {
            if (node.Attributes != null)
                if (node.Attributes[XmlAttributes.OutputLimit] != null)
                    return int.Parse(node.Attributes[XmlAttributes.OutputLimit].Value);

            return 0;
        }

        public static string getAnswer(XmlNode node)
        {
            if (node.Attributes != null)
                if (node.Attributes[XmlAttributes.Answer] != null)
                    return node.Attributes[XmlAttributes.Answer].Value;

            return null;
        }
    }
}