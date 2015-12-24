using System;
using System.Collections.Generic;
using Tools;

namespace Chess
{
    [Serializable]
    public class Packet
    {
        public enum Type
        {
                SignIn
            ,   SignInResult
            ,   Registration
            ,   RegistrationResult
            ,   GetConnectedPlayers
            ,   GiveConnectedPlayers
            ,   StartGame
            ,   StartGameResult
        }

        public static Packet FromBytes( byte[] bytes )
        {
            return BinSerializer<Packet>.FromBytes( bytes );
        }

        public byte[] ToBytes()
        {
            return BinSerializer<Packet>.ToBytes( this );
        }

        public Type PacketType { get; set; }
        private Packet( Type packet )
        {
            PacketType = packet;
        }
        
        public SignInData SignInData { get; set; }
        public static Packet SingInPacket( SignInData singIn )
        {
            Packet packet = new Packet( Type.SignIn );
            packet.SignInData = singIn;
            return packet;
        }

        public SignInResult SignInResult { get; set; }
        public static Packet SignInResultPacket( SignInResult result )
        {
            Packet packet = new Packet( Type.SignInResult );
            packet.SignInResult = result;
            return packet;
        }

        public RegistrationData RegistrationData { get; set; }
        public static Packet RegistrationPacket( RegistrationData registrationData )
        {
            Packet packet = new Packet( Type.Registration );
            packet.RegistrationData = registrationData;
            return packet;
        }

        public RegistrationResult RegistrationResult { get; set; }
        public static Packet RegistrationResultPacket( RegistrationResult result )
        {
            Packet packet = new Packet( Type.RegistrationResult );
            packet.RegistrationResult = result;
            return packet;
        }
        
        public string Login { get; set; }
        public static Packet GetConnectedPlayersPacket( string login )
        {
            Packet packet = new Packet( Type.GetConnectedPlayers );
            packet.Login = login;
            return packet;
        }

        public List<RegistrationData> ConnectedPlayers { get; set; }
        public static Packet GiveConnectedPlayersPacket( List<RegistrationData> players )
        {
            Packet packet = new Packet( Type.GiveConnectedPlayers );
            packet.ConnectedPlayers = players;
            return packet;
        }

        public static Packet StartGamePacket( string login )
        {
            Packet packet = new Packet( Type.StartGame );
            packet.Login = login;
            return packet;
        }

        public StartGameResult StartGameResult { get; set; }
        public static Packet StartGameResultPacket( StartGameResult result )
        {
            Packet packet = new Packet( Type.StartGameResult );
            packet.StartGameResult = result;
            return packet;
        }
    }
}
