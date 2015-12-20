using System;
using System.Drawing;

namespace Chess
{
    public class ChessSkin : IDisposable
    {
        public static ChessSkin FromXml(ILoader loader, string xmlFilePath)
        {
            ChessSkin skin = new ChessSkin();
            SkinXmlParser.Results results = SkinXmlParser.Parse(loader.LoadXmlDocument(xmlFilePath));
            skin.Image = loader.LoadImage(results.Image);
            ApplyChessKit(skin.Black, results.Black);
            ApplyChessKit(skin.White, results.White);
            return skin;
        }

        private static void ApplyChessKit(
                ChessSpriteKit kit
            ,   SkinXmlParser.Results.ChessColorType resoreKit )
        {
            kit.Bishop.Cut = resoreKit.Bishop.CutRectangle;
            kit.King.Cut = resoreKit.King.CutRectangle;
            kit.Knight.Cut = resoreKit.Knight.CutRectangle;
            kit.Pawn.Cut = resoreKit.Pawn.CutRectangle;
            kit.Queen.Cut = resoreKit.Queen.CutRectangle;
            kit.Rook.Cut = resoreKit.Rook.CutRectangle;
        }

        private Image image;
        public Image Image
        {
            get { return image; }
            set
            {
                image = value;
                ApplyImage(Black);
                ApplyImage(White);
            }
        }

        private void ApplyImage(ChessSpriteKit kit)
        {
            kit.Bishop.Image = image;
            kit.King.Image = image;
            kit.Knight.Image = image;
            kit.Pawn.Image = image;
            kit.Queen.Image = image;
            kit.Rook.Image = image;
        }

        public class ChessSpriteKit
        {
            public Sprite Rook = new Sprite();
            public Sprite Queen = new Sprite();
            public Sprite Pawn = new Sprite();
            public Sprite Knight = new Sprite();
            public Sprite King = new Sprite();
            public Sprite Bishop = new Sprite();
        }
        public ChessSpriteKit Black = new ChessSpriteKit();
        public ChessSpriteKit White = new ChessSpriteKit();

        public void Dispose()
        {
            Image.Dispose();
        }
    }
}
