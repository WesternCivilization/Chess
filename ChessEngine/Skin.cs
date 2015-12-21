using System;
using System.Drawing;
using System.Linq;
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
            public Image Texture
            {
                set
                {
                    foreach ( Sprite sprite in chessSprites )
                        sprite.Image = value;
                }
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
                black.Texture = texture;
                white.Texture = texture;
            }
        }

        #region XmlLoader

        #region Checkers

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

        #endregion

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

        private void LoadChess( XmlNode typeNode, GameColor color )
        {
            CheckChilds( typeNode, Constains.ChessTags );
            CheckAttributes( typeNode, Constains.Empty );
            
            foreach ( XmlNode chessNode in typeNode.ChildNodes )
            {
                CheckChilds( chessNode, Constains.Empty );
                CheckAttributes( chessNode, Constains.AttributesChess );

                ChessType type = ( ChessType ) Enum.Parse(
                        typeof( ChessType )
                    ,   char.ToUpper( chessNode.Name[ 0 ] ) + chessNode.Name.Remove( 0 )
                );
                
                Rectangle rect = new Rectangle();
                foreach ( XmlAttribute attr in chessNode.Attributes )
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
                this[ color, type ].Rectangle = rect;
            }
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

            CheckChilds( root, Constains.ChildTagsSkin );
            CheckAttributes( root, Constains.AttributesSkin );
            
            Texture = Image.FromFile( root.Attributes[ Constains.AttrSkinImage ].Value );

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
                    LoadChess( chessTag, colorChess );
            }
        }
        #endregion

    }
}
