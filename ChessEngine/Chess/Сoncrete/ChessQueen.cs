using System.Drawing;

namespace Chess
{
    public class ChessQueen : Chess
    {
        public ChessQueen( ChessFactory factory, GameColor color )
            :   base( factory, color, ChessType.Queen )
        {
        }

        public override void Accept( IChessVisitor visitor )
        {
            visitor.Visit( this );
        }

        protected override bool CanMoveInternal( Point index )
        {
            if ( CheckBishopLine( index ) || CheckRookLine( index ) )
                return CheckMove( index );
            return false;
        }
    }
}
