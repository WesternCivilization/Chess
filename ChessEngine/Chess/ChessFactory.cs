using System.Collections.Generic;
using System.Drawing;

namespace Chess
{
    public class ChessFactory
    {
        public ChessCollection ActiveChess = new ChessCollection();
        public ChessCollection KilledChess = new ChessCollection();

        public ChessFactory( Skin skin )
        {
            Skin = skin;
            WhiteChessDirection = ChessDirection.Up;
        }

        private SizeF chessSize;
        public SizeF ChessSize
        {
            get { return chessSize; }
            set
            {
                chessSize = value;
                ActiveChess.ChessSize = chessSize;
            }
        }
        
        private Skin skin;
        public Skin Skin
        {
            get { return skin; }
            set
            {
                skin = value;
                ActiveChess.UpdateSprite();
            }
        }

        public ChessDirection WhiteChessDirection { get; set; }
        public ChessDirection GetDirection( Chess chess )
        {
            if ( chess.Color == GameColor.White )
                return WhiteChessDirection;
            else
            {
                return WhiteChessDirection == ChessDirection.Up
                    ?   ChessDirection.Down
                    :   ChessDirection.Up;
            }
        }

        public Chess CreateChess( ChessType type, GameColor color, PointF position = new PointF() )
        {
            Chess chess;
            switch ( type )
            {
                case ChessType.Bishop:  chess = new ChessBishop( this, color ); break;
                case ChessType.King:    chess = new ChessKing( this, color ); break;
                case ChessType.Knight:  chess = new ChessKnight( this, color ); break;
                case ChessType.Pawn:    chess = new ChessPawn( this, color ); break;
                case ChessType.Queen:   chess = new ChessQueen( this, color ); break;
                case ChessType.Rook:    chess = new ChessRook( this, color ); break;
                default:
                    return null;
            }
            ActiveChess.Add( chess );
            chess.Sprite.Size = chessSize;
            chess.Sprite.Position = position;
            return chess;
        }

        public void Kill( Chess chess )
        {
            KilledChess.Add( chess );
            ActiveChess.Remove( chess );
        }
        public void Remove( Chess chess )
        {
            KilledChess.Remove( chess );
            ActiveChess.Remove( chess );
        }
        public void Reset()
        {
            KilledChess.Clear();
            ActiveChess.Clear();
        }
    }
}
