using System.Drawing;

namespace Chess
{
    static partial class SkinXmlParser
    {
        public class Results
        {
            public string Image { get; set; }
            public class ChessColorType
            {
                public struct Chess
                {
                    public string Name { get; set; }
                    public Rectangle CutRectangle { get; set; }
                }
                public Chess Bishop { get; set; }
                public Chess King { get; set; }
                public Chess Knight { get; set; }
                public Chess Pawn { get; set; }
                public Chess Queen { get; set; }
                public Chess Rook { get; set; }
            }
            public ChessColorType Black = new ChessColorType();
            public ChessColorType White = new ChessColorType();
        }
    }
}
