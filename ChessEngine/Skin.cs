using System;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Xml;

namespace Chess
{
    public class Skin
    {
        public Skin( string xmlFilePath )
        {
            Load( xmlFilePath );
        }

        private class ChessSpriteKit
        {
            private Sprite[] chessSprites = new Sprite[ 6 ];
            public Sprite this [ ChessType type ]
            {
                get { return chessSprites[ ( int ) type ]; }
                set { chessSprites[ ( int ) type ] = value; }
            }
            public void SetTexture( Image img )
            {
                foreach ( Sprite sprite in chessSprites )
                    sprite.Image = img;
            }
        }
        private ChessSpriteKit black = new ChessSpriteKit();
        private ChessSpriteKit white = new ChessSpriteKit();
        public Sprite this[ GameColor color, ChessType type ]
        {
            get
            {
                if ( color == GameColor.Black )
                    return black[ type ];
                return white[ type ];
            }
            set
            {
                if ( color == GameColor.Black )
                    black[ type ] = value;
                white[ type ] = value;
            }
        }
        
        private Image texture;
        public Image Texture
        {
            get { return texture; }
            set
            {
                texture = value;
                black.SetTexture( texture );
                white.SetTexture( texture );
            }
        }

        #region XmlLoader

        public class RestoreSkinXmlException : Exception
        {
            public RestoreSkinXmlException()
                : base( "Restore skin xml" )
            {
            }
        }

        static private void CheckChilds( XmlNode node, string[] childTags )
        {
            if ( childTags.Length == 0 && node.ChildNodes.Count > 0 )
                throw new RestoreSkinXmlException();
            foreach ( XmlNode child in node.ChildNodes )
            {
                if ( !childTags.Contains( child.Name ) )
                    throw new RestoreSkinXmlException();
            }
        }

        static private void CheckAttributes( XmlNode node, string[] attributes )
        {
            if ( attributes.Length == 0 && node.Attributes.Count > 0 )
                throw new RestoreSkinXmlException();
            foreach ( XmlNode attr in node.Attributes )
            {
                if ( !attributes.Contains( attr.Name ) )
                    throw new RestoreSkinXmlException();
            }
        }

        static private void CheckNodeValues( XmlAttribute attr, string[] values )
        {
            if ( !values.Contains( attr.Value ) )
                throw new RestoreSkinXmlException();
        }

        static private class Constains
        {
            public const string TagRoot      = "skin";
            public const string TagTypeBlack = "black";
            public const string TagTypeWhite = "white";
            public const string TagChess     = "chess";

            public const string AttrSkinImage   = "image";
            public const string AttrChessX      = "x";
            public const string AttrChessY      = "y";
            public const string AttrChessWidth  = "width";
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

        public void Load( string xmlFilePath )
        {
            XmlDocument document = new XmlDocument();
            document.Load( xmlFilePath );

            XmlNodeList list = document.GetElementsByTagName( Constains.TagRoot );
            if ( list.Count != 1 )
                throw new RestoreSkinXmlException();

            XmlNode root = list[ 0 ];
            if ( root.Name != Constains.TagRoot )
                throw new RestoreSkinXmlException();

            texture = Image.FromFile( root.Attributes[ Constains.AttrSkinImage ].Value );

            CheckChilds( root, Constains.ChildTagsSkin );
            CheckAttributes( root, Constains.AttributesSkin );

            foreach ( XmlNode colorTag in root.ChildNodes )
            {
                CheckChilds( colorTag, Constains.ChessTags );
                CheckAttributes( colorTag, Constains.Empty );

                GameColor colorChess;
                switch ( colorTag.Name )
                {
                    case Constains.TagTypeWhite: colorChess = GameColor.White; break;
                    case Constains.TagTypeBlack: colorChess = GameColor.Black; break;
                    default: throw new RestoreSkinXmlException();
                }

                foreach ( XmlNode chessTag in colorTag.ChildNodes )
                {
                    CheckChilds( chessTag, Constains.ChessTags );
                    CheckAttributes( chessTag, Constains.AttributesChess );

                    ChessType type = ( ChessType ) Enum.Parse(
                            typeof( ChessType )
                        , char.ToUpper( chessTag.Name[ 0 ] ) + chessTag.Name.Remove( 0, 1 )
                    );

                    Rectangle rect = new Rectangle();
                    foreach ( XmlAttribute attr in chessTag.Attributes )
                    {
                        if ( attr.Name == Constains.AttrChessX )
                            rect.X = int.Parse( attr.Value );
                        else if ( attr.Name == Constains.AttrChessY )
                            rect.Y = int.Parse( attr.Value );
                        else if ( attr.Name == Constains.AttrChessWidth )
                            rect.Width = int.Parse( attr.Value );
                        else if ( attr.Name == Constains.AttrChessHeight )
                            rect.Height = int.Parse( attr.Value );
                    }
                    this[ colorChess, type ] = new Sprite( texture, rect );

                }
            }

        }
        #endregion

    }
}
