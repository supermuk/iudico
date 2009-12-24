using System.Xml;

namespace IUDICO.DataModel.Common.ImportUtils
{
    public sealed class XmlUtility
    {
        public static bool IsChapter(XmlNode node)
        {
            return (GetPageType(node) == PageType.Chapter);
        }

        public static bool IsControlChapter(XmlNode node)
        {
            return (GetPageType(node) == PageType.ControlChapter);
        }

        public static bool IsTheory(XmlNode node)
        {
            return (GetPageType(node) == PageType.Theory);
        }

        public static bool IsPractice(XmlNode node)
        {
            return (GetPageType(node) == PageType.Practice);
        }

        public static bool IsPage(XmlNode node)
        {
            return IsTheory(node) || IsPractice(node);
        }

        public static bool IsItem(XmlNode node)
        {
            return (node.Name == "item");
        }

        public static bool IsLanguage(XmlNode node)
        {
            return (node.Name == "language");
        }

        public static bool IsTestCase(XmlNode node)
        {
            return (node.Name == "testcase");
        }

        public static string GetIdentifier(XmlNode node)
        {
            if (node.Attributes != null)
                if (node.Attributes[XmlAttributes.Identifier] != null)
                    return node.Attributes[XmlAttributes.Identifier].Value;

            return null;
        }

        public static string GetId(XmlNode node)
        {
            if (node.Attributes != null)
                if (node.Attributes[XmlAttributes.Id] != null)
                    return node.Attributes[XmlAttributes.Id].Value;

            return null;
        }

        public static string GetIdentifierRef(XmlNode node)
        {
            if (node.Attributes != null)
                if (node.Attributes[XmlAttributes.Reference] != null)
                    return node.Attributes[XmlAttributes.Reference].Value;

            return null;
        }

        public static string GetPageType(XmlNode node)
        {
            if (node.Attributes != null)
                if (node.Attributes[XmlAttributes.PageType] != null)
                    return node.Attributes[XmlAttributes.PageType].Value;

            return null;
        }

        public static int GetTimeLimit(XmlNode node)
        {
            if (node.Attributes != null)
                if (node.Attributes[XmlAttributes.TimeLimit] != null)
                    return int.Parse(node.Attributes[XmlAttributes.TimeLimit].Value);

            return 0;
        }

        public static int GetMemoryLimit(XmlNode node)
        {
            if (node.Attributes != null)
                if (node.Attributes[XmlAttributes.MemoryLimit] != null)
                    return int.Parse(node.Attributes[XmlAttributes.MemoryLimit].Value);

            return 0;
        }

        public static int GetOutputLimit(XmlNode node)
        {
            if (node.Attributes != null)
                if (node.Attributes[XmlAttributes.OutputLimit] != null)
                    return int.Parse(node.Attributes[XmlAttributes.OutputLimit].Value);

            return 0;
        }

        public static string GetAnswer(XmlNode node)
        {
            if (node.Attributes != null)
                if (node.Attributes[XmlAttributes.Answer] != null)
                    return node.Attributes[XmlAttributes.Answer].Value;

            return null;
        }
    }
}