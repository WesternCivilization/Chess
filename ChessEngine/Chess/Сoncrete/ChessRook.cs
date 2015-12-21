using System.Drawing;

namespace Chess
{
    public class ChessRook : Chess
    {
        public ChessRook( GameColor color )
            :   base( color, ChessType.Rook )
        {
        }

        public override void Accept( IChessVisitor visitor )
        {
            visitor.Visit( this );
        }

        protected override bool CanMoveInternal( Point index )
        {
            if ( CheckRookLine( index ) )
                return CheckMove( index );
            return false;
        }
    }
}
