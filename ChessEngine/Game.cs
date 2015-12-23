using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Chess
{
    public class Game
    {
        Player Player1 { get; set; }
        Player Player2 { get; set; }

        public Desk Desk { get; private set; }
        public ChessFactory Factory { get; private set; }

        public Game( Player player1, Player player2, ChessFactory factory )
        {
            Desk = new Desk();
            Factory = factory;

            Player1 = player1;
            Player2 = player2;

            Player1.ChessColor = GameColor.White;
            Player2.ChessColor = GameColor.Black;

            BuildStandartArrangement();
        }

        public void Move( Chess chess, Point index )
        {
            // TODO
            chess.Move( index );
        }

        public void AddChess( ChessType type, GameColor color, int x, int y )
        {
            Desk[ x, y ].Chess = Factory.CreateChess( type, color, Desk[ x, y ].Position );
        }

        public void BuildStandartArrangement()
        {
            Desk.Clean();

            AddChess( ChessType.Rook, GameColor.Black, 0, 0 );
            AddChess( ChessType.Rook, GameColor.Black, 7, 0 );
            AddChess( ChessType.Rook, GameColor.White, 0, 7 );
            AddChess( ChessType.Rook, GameColor.White, 7, 7 );

            AddChess( ChessType.Knight, GameColor.Black, 1, 0 );
            AddChess( ChessType.Knight, GameColor.Black, 6, 0 );
            AddChess( ChessType.Knight, GameColor.White, 1, 7 );
            AddChess( ChessType.Knight, GameColor.White, 6, 7 );

            AddChess( ChessType.Bishop, GameColor.Black, 2, 0 );
            AddChess( ChessType.Bishop, GameColor.Black, 5, 0 );
            AddChess( ChessType.Bishop, GameColor.White, 2, 7 );
            AddChess( ChessType.Bishop, GameColor.White, 5, 7 );

            AddChess( ChessType.King, GameColor.Black, 3, 0 );
            AddChess( ChessType.King, GameColor.White, 3, 7 );

            AddChess( ChessType.Queen, GameColor.Black, 4, 0 );
            AddChess( ChessType.Queen, GameColor.White, 4, 7 );

            for ( int i = 0; i < 8; ++i )
            {
                AddChess( ChessType.Pawn, GameColor.Black, i, 1 );
                AddChess( ChessType.Pawn, GameColor.White, i, 6 );
            }
        }
    }
}
