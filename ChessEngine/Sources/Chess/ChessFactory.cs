using System.Collections.Generic;
using System.Drawing;

namespace Chess
{
    public class ChessFactory : IDrawable
    {
        private static ChessFactory instance;
        public static ChessFactory Instance { get { return instance; } }

        private ChessFactory(ChessSkin skin)
        {
            Skin = skin;
            WhiteChessDirectionIsUp = true;
        }

        public static void Initialize(ChessSkin skin)
        {
            if (Instance == null)
                instance = new ChessFactory(skin);
        }


        private List<Chess> chessKit = new List<Chess>();
        private List<Chess> killedChess = new List<Chess>();
        public List<Chess> ChessKit { get { return chessKit; } }
        public List<Chess> KilledChess { get { return killedChess; } }

        public void Draw(Graphics g)
        {
            foreach (Chess chess in chessKit)
                chess.Draw(g);
        }

        private SizeF chessSize;
        public SizeF ChessSize
        {
            get { return chessSize; }
            set
            {
                chessSize = value;
                foreach (Chess chess in chessKit)
                    chess.Sprite.Size = chessSize;
            }
        }

        public void UpdatePositions()
        {
            foreach (Chess chess in chessKit)
                chess.UpdatePosition();
        }

        private ChessSkin skin;
        public ChessSkin Skin
        {
            get { return skin; }
            set
            {
                skin = value;
                foreach (Chess chess in chessKit)
                    ChessSkinUpdaterVisitor.Update(skin, chess);
            }
        }
        
        public bool WhiteChessDirectionIsUp { get; set; }
        // true is UP
        public bool GetDirection(Chess chess)
        {
            if (chess.Color == ChessColor.White)
                return WhiteChessDirectionIsUp;
            return !WhiteChessDirectionIsUp;
        }

        public Chess CreateChess(ChessType type, ChessColor color, PointF position = new PointF())
        {
            Chess chess;
            switch(type)
            {
                case ChessType.Bishop:  chess = new ChessBishop(color); break;
                case ChessType.King:    chess = new ChessKing(color);   break;
                case ChessType.Knight:  chess = new ChessKnight(color); break;
                case ChessType.Pawn:    chess = new ChessPawn(color);   break;
                case ChessType.Queen:   chess = new ChessQueen(color);  break;
                case ChessType.Rook:    chess = new ChessRook(color);   break;
                default: return null;
            }
            chessKit.Add(chess);
            chess.Sprite.Size = chessSize;
            chess.Sprite.Position = position;
            return chess;
        }
        
        public void Kill(Chess chess)
        {
            killedChess.Add(chess);
            chessKit.Remove(chess);
        }
        public void Remove(Chess chess)
        {
            killedChess.Remove(chess);
            chessKit.Remove(chess);
        }
    }
}
