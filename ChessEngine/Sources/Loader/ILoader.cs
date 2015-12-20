using System.Drawing;
using System.Xml;

namespace Chess
{
    public interface ILoader
    {
        Image LoadImage(string filename);
        XmlDocument LoadXmlDocument(string filename);
        string LoadTextFile(string filename);
    }
}
