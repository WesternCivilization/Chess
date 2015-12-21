using System;
using System.Windows.Forms;

namespace Tools
{
    public static class ControlEx
    {
        public static void Repaint( this Control control )
        {
            control.Invalidate();
            control.Update();
            control.Refresh();
        }
        public static void InvokeEx( this Control control, Action action )
        {
            if ( control.InvokeRequired )
                control.Invoke( action );
            else
                action();
        }
    }
}
