using System;
using System.Drawing;
using System.IO;
using System.Xml;

namespace Chess
{
    public class FilesSystemLoader : ILoader
    {
        public XmlDocument LoadXmlDocument(string filename)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filename);
            throw new NotImplementedException();
        }

        public Image LoadImage(string filename)
        {
            return Image.FromFile(filename);
        }

        public string LoadTextFile(string filename)
        {
            return File.ReadAllText(filename);
        }
    }
}
