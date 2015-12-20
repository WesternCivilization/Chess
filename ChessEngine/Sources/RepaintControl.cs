using System.Windows.Forms;

namespace Chess
{
    public static class RepaintControl
    {
        public static void Repaint(this Control control)
        {
            control.Invalidate();
            control.Update();
            control.Refresh();
        }
    }
}
