using System.Drawing;

namespace Chess
{
    public abstract class Shape
        :   IDrawObject
    {
        protected PointF[] points;
        public Brush Brush { get; set; }
        public Pen Pen { get; set; }

        public abstract PointF Position { get; set; }
        public abstract SizeF Size { get; set; }
        public abstract RectangleF Rectangle { get; set; }

        protected Shape( PointF[] points )
        {
            this.points = points;
            Brush = Brushes.Black;
            Pen = Pens.Transparent;
        }

        public void Draw( Graphics g )
        {
            g.FillPolygon( Brush, points );
            g.DrawPolygon( Pen, points );
        }
    }
}
