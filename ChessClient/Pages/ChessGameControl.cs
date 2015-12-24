using System;
using System.Windows.Forms;
using System.Drawing;
using Tools;

namespace Chess
{
    public partial class ChessGameControl : UserControl
    {
        public ChessGameControl()
        {
            InitializeComponent();
            DoubleBuffered = true;
        }
    }
}
