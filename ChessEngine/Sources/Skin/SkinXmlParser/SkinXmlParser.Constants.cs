namespace Chess
{
    static partial class SkinXmlParser
    {
        static private class Constains
        {
            public const string TagSkin = "skin";
            public const string TagTypeBlack = "black";
            public const string TagTypeWhite = "white";
            public const string TagChess = "chess";

            public const string AttrSkinImage = "image";
            public const string AttrChessX = "x";
            public const string AttrChessY = "y";
            public const string AttrChessWidth = "width";
            public const string AttrChessHeight = "height";
            
            public static string[] ChessTags =
            {
                "queen", "king", "rook", "bishop", "knight", "pawn",
            };

            public static string[] Empty = { };
            public static string[] ChildTagsSkin = { TagTypeBlack, TagTypeWhite };

            public static string[] AttributesSkin = { AttrSkinImage };
            public static string[] AttributesChess =
            {
                    AttrChessX
                ,   AttrChessY
                ,   AttrChessWidth
                ,   AttrChessHeight
            };
        }
    }
}
