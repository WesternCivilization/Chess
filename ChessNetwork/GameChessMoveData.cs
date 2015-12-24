using System;
using System.Drawing;

namespace Chess
{
    [Serializable]
    public class GameChessMoveData
    {
        public Point From { get; set; }
        public Point To { get; set; }

        public GameChessMoveData( Point from, Point to )
        {
            From = from;
            To = to;
        }
    }
}
