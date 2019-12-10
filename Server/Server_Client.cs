using System;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Net;
using System.Drawing;
using PacketData;

namespace Server
{
    public class Server_Client : IDisposable
    {
        Socket tcpSocket;
        Socket udpSocket;
        NetworkStream stream;
        bool op = false;
        bool disposed = false;
        public string ID { get; set; }
        public Color color { get; set; }
        public BinaryReader breader { get; private set; }
        public BinaryWriter bwriter { get; private set; }
        BinaryFormatter binaryFormatter = new BinaryFormatter();

        public Packet CreateLoginPacket(EndPoint endPoint)
        {
            return new LoginPacket(endPoint);
        }

        public Server_Client()
        {
            color = Color.Black;
            this.udpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        }

        public void TCPConnect(Socket socket)
        {
            this.tcpSocket = socket;
            stream = new NetworkStream(this.tcpSocket, true);
            breader = new BinaryReader(stream);
            bwriter = new BinaryWriter(stream);
        }

        public void UDPConnect(EndPoint endPoint)
        {
            udpSocket.Connect(endPoint);
            TCPSend(CreateLoginPacket(udpSocket.LocalEndPoint));
        }

        byte[] Serialize(Packet data)
        {
            MemoryStream memoryStream = new MemoryStream();
            binaryFormatter.Serialize(memoryStream, data);
            return memoryStream.GetBuffer();
        }

        Packet Deserialize(byte[] buffer)
        {
            MemoryStream memoryStream = new MemoryStream();
            memoryStream.Write(buffer, 0, buffer.Length);
            memoryStream.Position = 0;
            return binaryFormatter.Deserialize(memoryStream) as Packet;
        }

        public void TCPSend(Packet data)
        {
            try
            {
                byte[] buffer = Serialize(data);
                this.bwriter.Write(buffer.Length);
                this.bwriter.Write(buffer);
                this.bwriter.Flush();
            }
            catch (IOException e)
            {
                Console.WriteLine("TCP Send Failed - " + e.Message);
            }
        }

        public void UDPSend(Packet data)
        {
            try
            {
                byte[] buffer = Serialize(data);
                udpSocket.Send(buffer, buffer.Length, SocketFlags.None);
            }
            catch (IOException e)
            {
                Console.WriteLine("UDP Send Failed - " + e.Message);
            }
        }

        public Packet TCPRead()
        {
            int noOfIncomingBytes;
            if ((noOfIncomingBytes = this.breader.ReadInt32()) != 0)
                return Deserialize(this.breader.ReadBytes(noOfIncomingBytes));
            return null;
        }

        public Packet UDPRead()
        {
            byte[] buffer = new byte[2048];
            int noOfIncomingBytes;
            if ((noOfIncomingBytes = udpSocket.Receive(buffer)) != 0)
                return Deserialize(buffer);
            return null;
        }

        public void Close()
        {
            tcpSocket.Close();
            udpSocket.Close();
        }

        public void SetOP(bool val)
        {
            this.op = val;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    tcpSocket.Dispose();
                    udpSocket.Dispose();
                    stream.Dispose();
                    breader.Dispose();
                    bwriter.Dispose();
                }
                disposed = true;
            }
        }

        public string ColorID()
        {
            return "<color (" + this.color.R + "," + this.color.G + "," + this.color.B + ")>" + this.ID + "</color>";
        }

        public bool GetOP()
        {
            return op;
        }
    }
}
