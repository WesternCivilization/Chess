using System.Drawing;

namespace Chess
{
    public class ChessQueen : Chess
    {
        public ChessQueen(ChessColor color)
            :   base(color)
        {
            Sprite = (color == ChessColor.Black)
                ?   ChessFactory.Instance.Skin.Black.Queen.Clone()
                :   ChessFactory.Instance.Skin.White.Queen.Clone();
        }

        public override void Accept(IChessVisitor visitor)
        {
            visitor.Visit(this);
        }

        protected override bool CanMoveInternal(Point index)
        {
            if (CheckBishopLine(index) || CheckRookLine(index))
                return CheckMove(index);
            return false;
        }
    }
}
