using System.Drawing;

namespace Chess
{
    public class ChessBishop : Chess
    {
        public ChessBishop( ChessFactory factory, GameColor color )
            :   base( factory, color, ChessType.Bishop )
        {
        }

        protected override bool CanMoveInternal( Point index )
        {
            if ( CheckBishopLine( index ) )
                return CheckMove( index );
            return false;
        }
    }
}
