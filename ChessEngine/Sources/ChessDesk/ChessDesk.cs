﻿using System.Drawing;

namespace Chess
{
    public class ChessDesk : IDrawable
    {
        public static bool CheckOutOfRange(int x, int y)
        {
            return x < 0 || y < 0 || x >= 8 || y >= 8;
        }
        public static bool CheckOutOfRange(Point p)
        {
            return CheckOutOfRange(p.X, p.Y);
        }
        
        public RectangleF Rectangle { get; set; }

        private ChessDeskCell[,] cells = new ChessDeskCell[8, 8];
        public ChessDeskCell this[ Point p ]
        {
            get { return this[p.X, p.Y]; }
            set { cells[p.X, p.Y] = value; }
        }

        public ChessDeskCell this[int x, int y]
        {
            get
            {
                if (!CheckOutOfRange(x, y))
                    return cells[x, y];
                return null;
            }
            set { cells[x, y] = value; }
        }

        public ChessDeskCell GetCellByMouse(Point mouseCoord)
        {
            if(mouseCoord.X != 0)
                mouseCoord.X /= (int)CellsSize.Width;
            if(mouseCoord.Y != 0)
                mouseCoord.Y /= (int)CellsSize.Height;
            return this[mouseCoord];
        }

        public SizeF CellsSize { get { return cells[0, 0].Size; } }

        public ChessDesk()
        {
            LoadCells();
        }

        public void Clean()
        {
            for(int i = 0; i < 8; ++i)
            for(int j = 0; j < 8; ++j)
            {
                if (cells[i, j].Chess != null)
                {
                    cells[i, j].Chess.Dispose();
                    cells[i, j].Chess = null;
                }
            }
        }


        private void LoadCells()
        {
            ChessDeskCell.ColorCell currentType;

            for (int j = 0; j < 8; ++j)
            {
                currentType = (j % 2 == 0)
                    ?   ChessDeskCell.ColorCell.Black
                    :   ChessDeskCell.ColorCell.White;

                for (int i = 0; i < 8; ++i)
                {
                    cells[i, j] = new ChessDeskCell(this, currentType, new Point(i, j));

                    currentType = (currentType == ChessDeskCell.ColorCell.Black)
                        ?   ChessDeskCell.ColorCell.White
                        :   ChessDeskCell.ColorCell.Black;
                }
            }
        }

        public void Draw(Graphics g)
        {
            foreach (ChessDeskCell cell in cells)
                cell.Draw(g);
        }
    }
}
