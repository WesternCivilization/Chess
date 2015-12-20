using System.Drawing;

namespace Chess
{
    public class ChessKnight : Chess
    {
        public ChessKnight(ChessColor color)
            :   base(color)
        {
            Sprite = (color == ChessColor.Black)
                ?   ChessFactory.Instance.Skin.Black.Knight.Clone()
                :   ChessFactory.Instance.Skin.White.Knight.Clone();
        }

        public override void Accept(IChessVisitor visitor)
        {
            visitor.Visit(this);
        }

        protected override bool CanMoveInternal(Point index)
        {
            if (    new Point(Cell.Index.X - 1, Cell.Index.Y - 2) == index
                ||  new Point(Cell.Index.X - 2, Cell.Index.Y - 1) == index
                ||  new Point(Cell.Index.X + 1, Cell.Index.Y - 2) == index
                ||  new Point(Cell.Index.X + 2, Cell.Index.Y - 1) == index

                ||  new Point(Cell.Index.X - 1, Cell.Index.Y + 2) == index
                ||  new Point(Cell.Index.X - 2, Cell.Index.Y + 1) == index
                ||  new Point(Cell.Index.X + 1, Cell.Index.Y + 2) == index
                ||  new Point(Cell.Index.X + 2, Cell.Index.Y + 1) == index )
                return CheckMove(index);
            return false;
        }
    }
}
