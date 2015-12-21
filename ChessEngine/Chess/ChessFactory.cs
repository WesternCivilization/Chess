using System.Collections.Generic;
using System.Drawing;

namespace Chess
{
    public class ChessFactory : IDrawable
    {
        private static ChessFactory instance;
        public static ChessFactory Instance
        {
            get
            {
                if ( instance == null )
                    instance = new ChessFactory();
                return instance;
            }
        }

        private ChessFactory()
        {
            Skin = new Skin( "resources/skin.xml" ); // TODO load from properties
            ChessKit = new List<Chess>();
            KilledChess = new List<Chess>();
            WhiteChessDirectionIsUp = true;
        }
        
        public List<Chess> ChessKit { get; private set; }
        public List<Chess> KilledChess { get; private set; }

        public void Draw( Graphics g )
        {
            foreach ( Chess chess in ChessKit )
                chess.Draw( g );
        }

        private SizeF chessSize;
        public SizeF ChessSize
        {
            get { return chessSize; }
            set
            {
                chessSize = value;
                foreach ( Chess chess in ChessKit )
                    chess.Sprite.Size = chessSize;
            }
        }

        public void UpdatePositions()
        {
            foreach ( Chess chess in ChessKit )
                chess.UpdatePosition();
        }

        private Skin skin;
        public Skin Skin
        {
            get { return skin; }
            set
            {
                skin = value;
                foreach ( Chess chess in ChessKit )
                    chess.UpdateSprite();
            }
        }

        public bool WhiteChessDirectionIsUp { get; set; }
        // true is UP
        public bool GetDirection( Chess chess )
        {
            if ( chess.Color == GameColor.White )
                return WhiteChessDirectionIsUp;
            return !WhiteChessDirectionIsUp;
        }

        public Chess CreateChess( ChessType type, GameColor color, PointF position = new PointF() )
        {
            Chess chess;
            switch ( type )
            {
                case ChessType.Bishop:  chess = new ChessBishop( color ); break;
                case ChessType.King:    chess = new ChessKing( color ); break;
                case ChessType.Knight:  chess = new ChessKnight( color ); break;
                case ChessType.Pawn:    chess = new ChessPawn( color ); break;
                case ChessType.Queen:   chess = new ChessQueen( color ); break;
                case ChessType.Rook:    chess = new ChessRook( color ); break;
                default:
                    return null;
            }
            ChessKit.Add( chess );
            chess.Sprite.Size = chessSize;
            chess.Sprite.Position = position;
            return chess;
        }

        public void Kill( Chess chess )
        {
            KilledChess.Add( chess );
            ChessKit.Remove( chess );
        }
        public void Remove( Chess chess )
        {
            KilledChess.Remove( chess );
            ChessKit.Remove( chess );
        }
    }
}
