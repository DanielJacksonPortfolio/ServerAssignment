using System;
using System.Collections.Generic;
using System.Text;

namespace PacketData
{
    public enum PacketType
    {
        INVALID,
        EMPTY,
        CHAT_MESSAGE,
        DISCONNECT,
        INIT_MESSAGE
    }

    [Serializable]
    public class Packet
    {
        public PacketType type = PacketType.INVALID;
    }

    [Serializable]
    public class ChatMessagePacket : Packet
    {
        public string message = String.Empty;

        public ChatMessagePacket(string message)
        {
            this.type = PacketType.CHAT_MESSAGE;
            this.message = message;
        }
    }
    [Serializable]
    public class InitMessagePacket : Packet
    {
        public string message = String.Empty;

        public InitMessagePacket(string message)
        {
            this.type = PacketType.INIT_MESSAGE;
            this.message = message;
        }
    }
    [Serializable]
    public class DisconnectPacket : Packet
    {
        public enum DisconnectType
        {
            INVALID,
            USER_KILL,
            CLEAN,
            SERVER_KILL,
            SERVER_DEAD,
        }

        public string message = String.Empty;
        public DisconnectType disconnectType = DisconnectType.INVALID;

        public DisconnectPacket(string message, DisconnectType type)
        {
            this.type = PacketType.DISCONNECT;
            this.disconnectType = type;
            this.message = message;
        }
    }
    [Serializable]
    public class EmptyPacket : Packet
    {
        public EmptyPacket()
        {
            this.type = PacketType.EMPTY;
        }
    }
}
