using System;
using System.Windows.Forms;
using System.Drawing;
using Tools;

namespace Chess
{
    public class ChessGameControl : UserControl
    {
        public Game Game { get; private set; }

        public ChessGameControl( Skin skin )
        {
            Game = new Game( skin );
            DoubleBuffered = true;
        }
    }
}
