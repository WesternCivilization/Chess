using System.Drawing;

namespace Chess
{
    public class ChessKing : Chess
    {
        public ChessKing( GameColor color )
            :   base( color, ChessType.King )
        {
        }

        public override void Accept( IChessVisitor visitor )
        {
            visitor.Visit( this );
        }

        protected override bool CanMoveInternal( Point index )
        {
            if (    Cell.Index.X - 1 <= index.X
                &&  Cell.Index.X + 1 >= index.X
                &&  Cell.Index.Y - 1 <= index.Y
                &&  Cell.Index.Y + 1 >= index.Y )
                return CheckMove( index ) && CheckMayDie( index );
            return false;
        }
    }
}
