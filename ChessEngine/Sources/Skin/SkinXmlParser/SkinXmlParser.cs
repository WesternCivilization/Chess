using System.Xml;
using System.Drawing;

namespace Chess
{
    static partial class SkinXmlParser
    {
        public class RestoreSkinXmlException : XmlException { /*TODO*/ }
        static private void ParseChess(XmlNode typeNode, ChessDeskCell.ColorCell type)
        {
            CheckChilds(typeNode, Constains.ChessTags);
            CheckAttributes(typeNode, Constains.Empty);

            Results.ChessColorType chessType = new Results.ChessColorType();
            foreach (XmlNode chessNode in typeNode.ChildNodes)
            {
                CheckChilds(chessNode, Constains.Empty);
                CheckAttributes(chessNode, Constains.AttributesChess);

                Results.ChessColorType.Chess chess = new Results.ChessColorType.Chess();
                Rectangle rect = new Rectangle();

                chess.Name = chessNode.Name;

                foreach (XmlAttribute attr in chessNode.Attributes)
                {
                    if (attr.Name == Constains.AttrChessX)
                        rect.X = int.Parse(attr.Value);
                    else if (attr.Name == Constains.AttrChessY)
                        rect.Y = int.Parse(attr.Value);
                    else if (attr.Name == Constains.AttrChessWidth)
                        rect.Width = int.Parse(attr.Value);
                    else if (attr.Name == Constains.AttrChessHeight)
                        rect.Height = int.Parse(attr.Value);
                }
                chess.CutRectangle = rect;
                ApplyType(chess, chessType);
            }

            if (type == ChessDeskCell.ColorCell.Black)
                results.Black = chessType;
            else
                results.White = chessType;
        }

        private static void ApplyType(Results.ChessColorType.Chess chess, Results.ChessColorType type)
        {
            if (chess.Name == Constains.ChessTags[(int)ChessType.Bishop])
                type.Bishop = chess;
            else if (chess.Name == Constains.ChessTags[(int)ChessType.King])
                type.King = chess;
            else if (chess.Name == Constains.ChessTags[(int)ChessType.Knight])
                type.Knight = chess;
            else if (chess.Name == Constains.ChessTags[(int)ChessType.Pawn])
                type.Pawn = chess;
            else if (chess.Name == Constains.ChessTags[(int)ChessType.Queen])
                type.Queen = chess;
            else if (chess.Name == Constains.ChessTags[(int)ChessType.Rook])
                type.Rook = chess;
            else
                throw new RestoreSkinXmlException();
        }

        private static Results results;
        public static Results Parse(XmlDocument document)
        {
            results = new Results();

            XmlNodeList list = document.GetElementsByTagName(Constains.TagSkin);
            if(list.Count != 1)
                throw new RestoreSkinXmlException();

            XmlNode skin = list[0];
            if (skin.Name != Constains.TagSkin)
                throw new RestoreSkinXmlException();

            CheckChilds(skin, Constains.ChildTagsSkin);
            CheckAttributes(skin, Constains.AttributesSkin);

            results.Image = skin.Attributes[Constains.AttrSkinImage].Value;

            foreach (XmlNode type in skin.ChildNodes)
            {
                CheckChilds(type, Constains.ChessTags);
                CheckAttributes(type, Constains.Empty);

                ChessDeskCell.ColorCell typeCell;
                if (type.Name == Constains.TagTypeWhite)
                    typeCell = ChessDeskCell.ColorCell.White;
                else if (type.Name == Constains.TagTypeBlack)
                    typeCell = ChessDeskCell.ColorCell.Black;
                else
                    throw new RestoreSkinXmlException();

                foreach (XmlNode node in type.ChildNodes)
                    ParseChess(type, typeCell);
            }
            return results;
        }
    }
}
