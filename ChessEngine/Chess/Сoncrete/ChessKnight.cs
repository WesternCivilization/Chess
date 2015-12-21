using System.Drawing;

namespace Chess
{
    public class ChessKnight : Chess
    {
        public ChessKnight( GameColor color )
            :   base( color, ChessType.Knight )
        {
        }

        public override void Accept(IChessVisitor visitor)
        {
            visitor.Visit(this);
        }

        protected override bool CanMoveInternal(Point index)
        {
            if (    new Point( Cell.Index.X - 1, Cell.Index.Y - 2 ) == index
                ||  new Point( Cell.Index.X - 2, Cell.Index.Y - 1 ) == index
                ||  new Point( Cell.Index.X + 1, Cell.Index.Y - 2 ) == index
                ||  new Point( Cell.Index.X + 2, Cell.Index.Y - 1 ) == index

                ||  new Point( Cell.Index.X - 1, Cell.Index.Y + 2 ) == index
                ||  new Point( Cell.Index.X - 2, Cell.Index.Y + 1 ) == index
                ||  new Point( Cell.Index.X + 1, Cell.Index.Y + 2 ) == index
                ||  new Point( Cell.Index.X + 2, Cell.Index.Y + 1 ) == index )
                return CheckMove( index );
            return false;
        }
    }
}
