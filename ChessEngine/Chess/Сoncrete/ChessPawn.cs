using System.Drawing;

namespace Chess
{
    public class ChessPawn : Chess
    {
        public ChessPawn( ChessFactory factory, GameColor color )
            :   base( factory, color, ChessType.Pawn )
        {
        }

        protected override bool CanMoveInternal( Point index )
        {
            bool upDirection = Factory.GetDirection( this ) == ChessDirection.Up;
            if (    new Point( Cell.Index.X - 1, upDirection ? Cell.Index.Y - 1 : Cell.Index.Y + 1 ) == index     // left
                ||  new Point( Cell.Index.X + 1, upDirection ? Cell.Index.Y - 1 : Cell.Index.Y + 1 ) == index )   // right
                return CheckMoveKillOpponent( index );

            if ( !firstMove )
            {
                Point doubleStep = new Point( Cell.Index.X, upDirection ? Cell.Index.Y - 2 : Cell.Index.Y + 2 );
                if ( doubleStep == index && CheckMoveOnEmptyCell( doubleStep ) )
                    return true;
            }

            Point simpleMove = new Point( Cell.Index.X, upDirection ? Cell.Index.Y - 1 : Cell.Index.Y + 1 );
            if ( simpleMove == index && CheckMoveOnEmptyCell( simpleMove ) )
                return true;

            return false;
        }
    }
}
