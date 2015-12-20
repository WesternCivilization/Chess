using System.Drawing;

namespace Chess
{
    public class ChessRook : Chess
    {
        public ChessRook(ChessColor color)
            :   base(color)
        {
            Sprite = (color == ChessColor.Black)
                ?   ChessFactory.Instance.Skin.Black.Rook.Clone()
                :   ChessFactory.Instance.Skin.White.Rook.Clone();
        }

        public override void Accept(IChessVisitor visitor)
        {
            visitor.Visit(this);
        }

        protected override bool CanMoveInternal(Point index)
        {
            if (CheckRookLine(index))
                return CheckMove(index);
            return false;
        }
    }
}
