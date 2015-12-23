using System;
using System.Net;

namespace Chess
{
    public class UserPlayer : Player
    {
        public IPAddress IP { get; private set; }

        public UserPlayer( IPAddress ip, string name )
            :   base( name )
        {
            IP = ip;
        }
    }
}
