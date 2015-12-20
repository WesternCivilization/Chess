namespace Chess
{
    public class ChessSkinUpdaterVisitor : IChessVisitor
    {
        private ChessSkin skin;

        public ChessSkinUpdaterVisitor(ChessSkin skin)
        {
            this.skin = skin;
        }

        public static void Update(ChessSkin skin, Chess chess)
        {
            chess.Sprite.Image = skin.Image;
            chess.Accept(new ChessSkinUpdaterVisitor(skin));
        }

        private ChessSkin.ChessSpriteKit GetTypeKit(Chess chess)
        {
            return (chess.Color == ChessColor.Black)
                ?   skin.Black
                :   skin.White;
        }

        public void Visit(ChessKnight chess)
        {
            chess.Sprite.Cut = GetTypeKit(chess).Knight.Cut;
        }

        public void Visit(ChessQueen chess)
        {
            chess.Sprite.Cut = GetTypeKit(chess).Queen.Cut;
        }

        public void Visit(ChessRook chess)
        {
            chess.Sprite.Cut = GetTypeKit(chess).Rook.Cut;
        }

        public void Visit(ChessPawn chess)
        {
            chess.Sprite.Cut = GetTypeKit(chess).Pawn.Cut;
        }

        public void Visit(ChessKing chess)
        {
            chess.Sprite.Cut = GetTypeKit(chess).King.Cut;
        }

        public void Visit(ChessBishop chess)
        {
            chess.Sprite.Cut = GetTypeKit(chess).Bishop.Cut;
        }
    }
}
