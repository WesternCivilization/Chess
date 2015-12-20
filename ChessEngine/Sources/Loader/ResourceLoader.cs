using System.Drawing;
using System.Resources;
using System.Xml;

namespace Chess
{
    public class ResourceLoader : ILoader
    {
        ResourceManager manager;

        public ResourceLoader(ResourceManager manager)
        {
            this.manager = manager;
        }

        public XmlDocument LoadXmlDocument(string filename)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(LoadTextFile(filename));
            return doc;
        }

        public Image LoadImage(string filename)
        {
           return (Image)manager.GetObject(filename);
        }

        public string LoadTextFile(string filename)
        {
            return (string)manager.GetObject(filename);
        }
    }
}
