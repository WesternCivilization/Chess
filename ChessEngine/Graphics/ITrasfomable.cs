using System.Drawing;

namespace Chess
{
    public interface ITrasfomable
    {
        PointF Position { get; set; }
        SizeF Size { get; set; }
        RectangleF Rectangle { get; set; }
    }
}
