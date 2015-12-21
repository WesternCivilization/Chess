using System.Drawing;

namespace Chess
{
    public class ChessBishop : Chess
    {
        public ChessBishop( GameColor color )
            :   base( color, ChessType.Bishop )
        {
        }

        public override void Accept( IChessVisitor visitor )
        {
            visitor.Visit( this );
        }

        protected override bool CanMoveInternal( Point index )
        {
            if ( CheckBishopLine( index ) )
                return CheckMove( index );
            return false;
        }
    }
}
