namespace Chess
{
    public class Player
    {
        public string Name { get; set; }
        public GameColor Color { get; set; }
        public ChessDirection Direction { get; set; }

        public Player( string name, GameColor color, ChessDirection direction )
        {
            Name = name;
            Color = color;
            Direction = direction;
        }
    }
}
