using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Chess
{
    public class ChessGame
    {
        private ChessDesk desk = new ChessDesk();
        public ChessDesk Desk { get { return desk; } }
        
        Player Player1 { get; set; }
        Player Player2 { get; set; }
        public ChessGame(ChessSkin skin, Player p1, Player p2)
        {
            Player1 = p1;
            Player2 = p2;
            ChessFactory.Initialize(skin);
            BuildStandartArrangement();
        }
        

        public void Move(Chess chess, Point index)
        {
            // TODO
            chess.Move(index);
        }

        public void AddChess(ChessType type, ChessColor color, int x, int y)
        {
            desk[x, y].Chess = ChessFactory.Instance.CreateChess(type, color, desk[x, y].Position);
        }

        public void BuildStandartArrangement()
        {
            desk.Clean();

            AddChess(ChessType.Rook, ChessColor.Black, 0, 0);
            AddChess(ChessType.Rook, ChessColor.Black, 7, 0);
            AddChess(ChessType.Rook, ChessColor.White, 0, 7);
            AddChess(ChessType.Rook, ChessColor.White, 7, 7);

            AddChess(ChessType.Knight, ChessColor.Black, 1, 0);
            AddChess(ChessType.Knight, ChessColor.Black, 6, 0);
            AddChess(ChessType.Knight, ChessColor.White, 1, 7);
            AddChess(ChessType.Knight, ChessColor.White, 6, 7);

            AddChess(ChessType.Bishop, ChessColor.Black, 2, 0);
            AddChess(ChessType.Bishop, ChessColor.Black, 5, 0);
            AddChess(ChessType.Bishop, ChessColor.White, 2, 7);
            AddChess(ChessType.Bishop, ChessColor.White, 5, 7);

            AddChess(ChessType.King, ChessColor.Black, 3, 0);
            AddChess(ChessType.King, ChessColor.White, 3, 7);

            AddChess(ChessType.Queen, ChessColor.Black, 4, 0);
            AddChess(ChessType.Queen, ChessColor.White, 4, 7);

            for (int i = 0; i < 8; ++i)
            {
                AddChess(ChessType.Pawn, ChessColor.Black, i, 1);
                AddChess(ChessType.Pawn, ChessColor.White, i, 6);
            }
        }
    }
}
