using System.Drawing;

namespace Chess
{
    public static class GraphicsDrawableMethod
    {
        public static void Draw(this Graphics g, IDrawable drawable)
        {
            if (drawable != null)
            {
                drawable.Draw(g);
                g.ResetTransform();
            }
        }
    }
}
