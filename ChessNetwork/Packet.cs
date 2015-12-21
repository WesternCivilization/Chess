using System;

namespace Chess
{
    [Serializable]
    public class Packet
    {
        public enum Type
        {
                SignIn                  // Send login & password
            ,   SignInResult            // Verify
            ,   Registration            // Send registration info
            ,   RegistrationResult      // Result
            ,   GetConnectedPlayers     // Query for 
            ,   GiveConnectedPlayers    // Give connected Player list
            // TODO other packets
        }

        public Type PacketType { get; set; }

        public Packet( Type type )
        {
            PacketType = type;
        }
        
        // TODO info

    }
}
