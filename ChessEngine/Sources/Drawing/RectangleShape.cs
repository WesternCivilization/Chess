using System;
using System.Drawing;

namespace Chess
{
    public class RectangleShape : Shape
    {
        public RectangleShape()
            : base(new PointF[4])
        {
        }

        public RectangleShape(SizeF size)
           : base(new PointF[]
           {
                    new PointF()
                ,   new PointF( size.Width, 0 )
                ,   size.ToPointF()
                ,   new PointF( 0, size.Height )
           })
        {
        }
        public RectangleShape(Color fillColor)
         :  this()
        {
            Brush = new SolidBrush(fillColor);
        }

        public RectangleShape(SizeF size, Color fillColor)
           :    this(size)
        {
            Brush = new SolidBrush(fillColor);
        }

        public RectangleShape(RectangleF rect)
            :   base( new PointF[]
            {
                    new PointF( rect.Left, rect.Top )
                ,   new PointF( rect.Left, rect.Right )
                ,   new PointF( rect.Bottom, rect.Right )
                ,   new PointF( rect.Bottom, rect.Left )
            })
        {
        }

        public RectangleShape(RectangleF rect, Color fillColor)
           :    this(rect)
        {
            Brush = new SolidBrush(fillColor);
        }

        public override RectangleF Rectangle
        {
            get { return new RectangleF(Position, Size); }
            set { Size = value.Size; Position = value.Location; }
        }

        public override PointF Position
        {
            get { return points[0]; }
            set
            {
                points[0] = value;
                points[1] = new PointF(value.X + Width, value.Y);
                points[2] = new PointF(value.X + Width, value.Y + Height);
                points[3] = new PointF(value.X, value.Y + Height);
            }
        }

        public override SizeF Size
        {
            get { return new SizeF(points[1].X - points[0].X, points[2].Y - points[1].Y); }
            set
            {
                points[1] = new PointF(points[0].X + value.Width, points[0].Y);
                points[2] = new PointF(points[0].X + value.Width, points[0].Y + value.Height);
                points[3] = new PointF(points[0].X, points[0].Y + value.Height);
            }
        }

        public float Width
        {
            get { return Size.Width; }
            set { Size = new SizeF(value, Size.Width); }
        }

        public float Height
        {
            get { return Size.Height; }
            set { Size = new SizeF(Size.Height, value); }
        }
    }
}
