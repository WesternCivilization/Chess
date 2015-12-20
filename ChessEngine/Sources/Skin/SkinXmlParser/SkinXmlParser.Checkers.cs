using System.Linq;
using System.Xml;

namespace Chess
{
    static partial class SkinXmlParser
    {
        static private void CheckChilds(XmlNode node, string[] childTags)
        {
            if (childTags.Length == 0 && node.ChildNodes.Count > 0)
                throw new RestoreSkinXmlException();
            foreach (XmlNode child in node.ChildNodes)
            {
                if (!childTags.Contains(child.Name))
                    throw new RestoreSkinXmlException();
            }
        }
        static private void CheckAttributes(XmlNode node, string[] attributes)
        {
            if (attributes.Length == 0 && node.Attributes.Count > 0)
                throw new RestoreSkinXmlException();
            foreach (XmlNode attr in node.Attributes)
            {
                if (!attributes.Contains(attr.Name))
                    throw new RestoreSkinXmlException();
            }
        }
        static private void CheckNodeValues(XmlAttribute attr, string[] values)
        {
            if (!values.Contains(attr.Value))
                throw new RestoreSkinXmlException();
        }
    }
}