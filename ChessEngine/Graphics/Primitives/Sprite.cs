using System.Drawing;

namespace Chess
{
    public class Sprite : IDrawObject
    {
        public Image Image { get; set; }
        public RectangleF Cut { get; set; }
        public RectangleF Rectangle { get; set; }

        public PointF Position
        {
            get { return Rectangle.Location; }
            set { Rectangle = new RectangleF( value, Rectangle.Size ); }
        }

        public SizeF Size
        {
            get { return Rectangle.Size; }
            set { Rectangle = new RectangleF( Rectangle.Location, value ); }
        }

        public Sprite()
        {
        }

        public Sprite( Image image )
        {
            Image = image;
            Cut = new RectangleF( new PointF(), image.Size );
            Position = new PointF();
            Size = image.Size;
        }

        public Sprite( Image image, RectangleF cut )
        {
            Image = image;
            Cut = cut;
            Position = new PointF();
            Size = image.Size;
        }

        public Sprite( Image image, RectangleF cut, PointF position, SizeF size )
        {
            Image = image;
            Cut = cut;
            Position = position;
            Size = size;
        }

        public Sprite( Sprite other )
        {
            Image = other.Image;
            Cut = other.Cut;
            Rectangle = other.Rectangle;
        }

        public Sprite Clone()
        {
            return new Sprite( this );
        }

        public void Draw( Graphics g )
        {
            g.DrawImage(
                    Image
                ,   Rectangle
                ,   Cut
                ,   GraphicsUnit.Pixel
            );
        }
    }
}
