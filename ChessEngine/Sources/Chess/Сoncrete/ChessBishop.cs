using System.Drawing;

namespace Chess
{
    public class ChessBishop : Chess
    {
        public ChessBishop(ChessColor color)
            :   base(color)
        {
            Sprite = (color == ChessColor.Black)
                ?   ChessFactory.Instance.Skin.Black.Bishop.Clone()
                :   ChessFactory.Instance.Skin.White.Bishop.Clone();
        }

        public override void Accept(IChessVisitor visitor)
        {
            visitor.Visit(this);
        }

        protected override bool CanMoveInternal(Point index)
        {
            if (CheckBishopLine(index))
                return CheckMove(index);
            return false;
        }
    }
}
