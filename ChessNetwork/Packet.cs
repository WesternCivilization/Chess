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
            ,   GameChessMoveData
        }

        public static Packet FromBytes( byte[] bytes )
        {
            return BinSerializer<Packet>.FromBytes( bytes );
        }

        public byte[] ToBytes()
        {
            return BinSerializer<Packet>.ToBytes( this );
        }

        public static implicit operator byte[] ( Packet packet )
        {
            return packet.ToBytes();
        }

        public Type PacketType { get; set; }
        public object Data { get; set; }

        private Packet( Type packet, object data = null )
        {
            PacketType = packet;
            Data = data;
        }

        public static Packet SingInPacket( SignInData singIn )
        {
            return new Packet( Type.SignIn, singIn );
        }

        public static Packet SignInResultPacket( SignInResult result )
        {
            return new Packet( Type.SignInResult, result );
        }

        public static Packet RegistrationPacket( RegistrationData registrationData )
        {
            return new Packet( Type.Registration, registrationData );
        }

        public static Packet RegistrationResultPacket( RegistrationResult result )
        {
            return new Packet( Type.RegistrationResult, result );
        }

        public static Packet GetConnectedPlayersPacket( string login )
        {
            return new Packet( Type.GetConnectedPlayers, login );
        }

        public static Packet GiveConnectedPlayersPacket( List<RegistrationData> players )
        {
            return new Packet( Type.GiveConnectedPlayers, players );
        }

        public static Packet StartGamePacket( StartGameData data )
        {
            return new Packet( Type.StartGame, data );
        }

        public static Packet StartGameReplyPacket( StartGameData replyData )
        {
            return new Packet( Type.StartGameResult, replyData );
        }

        public static Packet MoveChessPacket( GameChessMoveData chessMoveData )
        {
            return new Packet( Type.GameChessMoveData, chessMoveData );
        }
    }
}
