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
        NICKNAME,
        USERLIST,
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
    public class EmptyPacket : Packet
    {
        public EmptyPacket()
        {
            this.type = PacketType.EMPTY;
        }
    }
}
