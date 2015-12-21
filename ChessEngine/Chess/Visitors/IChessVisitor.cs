namespace Chess
{
    // Maybe remove
    public interface IChessVisitor
    {
        void Visit( ChessBishop chess );
        void Visit( ChessKing chess );
        void Visit( ChessKnight chess );
        void Visit( ChessPawn chess );
        void Visit( ChessQueen chess );
        void Visit( ChessRook chess );
    }
}
