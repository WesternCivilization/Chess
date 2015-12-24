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
        public Player Player1 { get; private set; }
        public Player Player2 { get; private set; }

        public Desk Desk { get; private set; }
        public ChessFactory Factory { get; private set; }

        public enum State { None, InTheGame, Finish }
        public State GameState { get; private set; }

        public Game( Skin skin )
        {
            Desk = new Desk();
            Factory = new ChessFactory( skin );
            GameState = State.None;
        }

        public void Start( Player player1, Player player2 )
        {
            GameState = State.InTheGame;
            Player1 = player1;
            Player2 = player2;

            Factory.Reset();
            BuildStandartArrangement();
        }

        public bool Move( Chess chess, Point index )
        {
            // TODO
            return chess.Move( index );
        }

        public void AddChess( ChessType type, GameColor color, int x, int y )
        {
            Desk[ x, y ].Chess = Factory.CreateChess( type, color, Desk[ x, y ].Position );
        }

        public void BuildStandartArrangement()
        {
            Desk.Clean();

            GameColor up, down;
            if ( Player1.Color == GameColor.White )
            {
                down = Player1.Color;
                up = Player2.Color;
            }
            else
            {
                down = Player2.Color;
                up = Player1.Color;
            }

            AddChess( ChessType.Rook, up, 0, 0 );
            AddChess( ChessType.Rook, up, 7, 0 );
            AddChess( ChessType.Rook, down, 0, 7 );
            AddChess( ChessType.Rook, down, 7, 7 );

            AddChess( ChessType.Knight, up, 1, 0 );
            AddChess( ChessType.Knight, up, 6, 0 );
            AddChess( ChessType.Knight, down, 1, 7 );
            AddChess( ChessType.Knight, down, 6, 7 );

            AddChess( ChessType.Bishop, up, 2, 0 );
            AddChess( ChessType.Bishop, up, 5, 0 );
            AddChess( ChessType.Bishop, down, 2, 7 );
            AddChess( ChessType.Bishop, down, 5, 7 );

            AddChess( ChessType.King, up, 3, 0 );
            AddChess( ChessType.King, down, 3, 7 );

            AddChess( ChessType.Queen, up, 4, 0 );
            AddChess( ChessType.Queen, down, 4, 7 );

            for ( int i = 0; i < 8; ++i )
            {
                AddChess( ChessType.Pawn, up, i, 1 );
                AddChess( ChessType.Pawn, down, i, 6 );
            }
        }
    }
}
