using System.Drawing;

namespace Chess
{
    public class ChessQueen : Chess
    {
        public ChessQueen( GameColor color )
            :   base( color, ChessType.Queen )
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
