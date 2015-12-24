using System;

namespace Chess
{
    [Serializable]
    public class StartGameData
    {
        public string LoginQuery { get; private set; }
        public string LoginReply { get; private set; }
        public StartGameResult Result { get; private set; }
        public GameColor ColorQuery { get; private set; }
        public ChessDirection DirectionQuery { get; private set; }

        public StartGameData( string loginQuery, string loginReply, GameColor colorQuery, ChessDirection directionQuery )
        {
            LoginQuery = loginQuery;
            LoginReply = loginReply;
            ColorQuery = colorQuery;
            DirectionQuery = directionQuery;
        }

        public override string ToString()
        {
            return  "Login query: " + LoginQuery
                +   "\nLogin reply: " + LoginReply
                +   "\nReusult: " + Result.ToString();
        }

        public StartGameData Reply( StartGameResult result )
        {
            Result = result;
            return this;
        }
    }

    public enum StartGameResult
    {
        Unknown, OK, Cancel
    }
}
