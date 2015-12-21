using System.Drawing;

namespace Chess
{
    public class ChessSelect : IDrawable
    {
        public class CellDrawSelect : ITrasfomable
        {
            public IDrawObject BlackCell { get; set; }
            public IDrawObject WhiteCell { get; set; }

            public CellDrawSelect( IDrawObject black, IDrawObject white )
            {
                BlackCell = black;
                WhiteCell = white;
            }

            public PointF Position
            {
                get { return BlackCell.Position; }
                set
                {
                    BlackCell.Position = value;
                    WhiteCell.Position = value;
                }
            }

            public SizeF Size
            {
                get { return BlackCell.Size; }
                set
                {
                    BlackCell.Size = value;
                    WhiteCell.Size = value;
                }
            }

            public RectangleF Rectangle
            {
                get { return BlackCell.Rectangle; }
                set
                {
                    BlackCell.Rectangle = value;
                    WhiteCell.Rectangle = value;
                }
            }
        }

        public ChessDeskCell HoveredCell { get; set; }
        public ChessDeskCell SelectedCell { get; set; }

        public CellDrawSelect DrawingHovered { get; set; }
        public CellDrawSelect DrawingSelectedEmptyCell { get; set; }
        public CellDrawSelect DrawingSelectedChess { get; set; }

        public void DrawHover( Graphics g )
        {
            if ( HoveredCell != null )
            {
                g.Draw( HoveredCell.Color == GameColor.Black
                        ? DrawingHovered.BlackCell
                        : DrawingHovered.WhiteCell );
            }
        }

        public void DrawSelect( Graphics g )
        {
            if ( SelectedCell != null )
            {
                bool isBlack = SelectedCell.Color == GameColor.Black;
                g.Draw( SelectedCell.Chess == null
                    ?   isBlack
                        ?   DrawingSelectedEmptyCell.BlackCell
                        :   DrawingSelectedChess.WhiteCell
                    : isBlack
                        ?   DrawingSelectedEmptyCell.BlackCell
                        :   DrawingSelectedChess.WhiteCell
                );
            }
        }

        public void Draw( Graphics g )
        {
            DrawHover( g );
            DrawSelect( g );
        }
    }
}
