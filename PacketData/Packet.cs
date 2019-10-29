using System;
using System.Collections.Generic;
using System.Text;

namespace PacketData
{
    public enum PacketType
    {
        EMPTY,
        CHAT_MESSAGE,
        NICKNAME,
        USERLIST
    }

    [Serializable]
    public class Packet
    {
        public PacketType type = PacketType.EMPTY;
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
}
