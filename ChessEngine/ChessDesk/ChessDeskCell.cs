﻿using System;
using System.Drawing;

namespace Chess
{
    public class ChessDeskCell : IDrawable
    {
        public GameColor Color { get; private set; }
        public ChessDesk Desk { get; private set; }

        private Chess chess;
        public Chess Chess
        {
            get { return chess; }
            set
            {
                chess = value;
                if ( chess != null )
                    chess.Cell = this;
            }
        }

        public ChessDeskCell( ChessDesk desk, GameColor type, Point index )
        {
            Desk = desk;
            Color = type;
            Index = index;
        }

        private Point index;
        public Point Index
        {
            get { return index; }
            set
            {
                if ( ChessDesk.CheckOutOfRange( value.X, value.Y ) )
                    throw new ArgumentOutOfRangeException();
                index = value;
            }
        }

        public SizeF Size
        {
            get
            {
                return new SizeF(
                        ( Desk.Rectangle.Width / 8 )
                    ,   ( Desk.Rectangle.Height / 8 )
                );
            }
        }
        public PointF Position
        {
            get
            {
                return new PointF(
                        Desk.Rectangle.X + ( Size.Width * Index.X )
                    , Desk.Rectangle.Y + ( Size.Height * Index.Y )
                );
            }
        }
        public RectangleF Rectangle
        {
            get { return new RectangleF( Position, Size ); }
        }

        public void Draw( Graphics g )
        {
            g.FillRectangle(
                    Color == GameColor.Black
                    ?   Brushes.Black
                    :   Brushes.White
                ,   Rectangle
            );
        }
    }
}
