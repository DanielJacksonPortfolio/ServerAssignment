using System;
using System.Drawing;
using System.Collections.Generic;
using System.Text;

namespace PacketData
{
    public enum PacketType
    {
        INVALID,
        CHAT_MESSAGE,
        DISCONNECT,
        COLOR,
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
        public Color sendersColor = Color.Black;
        public ChatMessagePacket(string message, Color color)
        {
            this.type = PacketType.CHAT_MESSAGE;
            this.message = message;
            this.sendersColor = color;
        }
    }
    [Serializable]
    public class InitMessagePacket : Packet
    {
        public string message = String.Empty;
        public Color chatColor = Color.Black;
        public InitMessagePacket(string message, Color chatColor)
        {
            this.type = PacketType.INIT_MESSAGE;
            this.message = message;
            this.chatColor = chatColor;
        }
    }
    [Serializable]
    public class ColorPacket : Packet
    {
        public Color color = Color.Black;
        public ColorPacket(Color color)
        {
            this.type = PacketType.COLOR;
            this.color = color;
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
}
