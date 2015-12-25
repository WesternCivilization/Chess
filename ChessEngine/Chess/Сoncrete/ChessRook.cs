using System.Drawing;

namespace Chess
{
    public class ChessRook : Chess
    {
        public ChessRook( ChessFactory factory, GameColor color )
            :   base( factory, color, ChessType.Rook )
        {
        }

        protected override bool CanMoveInternal( Point index )
        {
            if ( CheckRookLine( index ) )
                return CheckMove( index );
            return false;
        }
    }
}
