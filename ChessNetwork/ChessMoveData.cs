using System;
using System.Drawing;

namespace Chess
{
    [Serializable]
    public class ChessMoveData
    {
        public Point From { get; set; }
        public Point To { get; set; }
        public string LoginСontender { get; set; }

        public ChessMoveData( Point from, Point to, string loginContender )
        {
            From = from;
            To = to;
            LoginСontender = loginContender;
        }
    }
}
