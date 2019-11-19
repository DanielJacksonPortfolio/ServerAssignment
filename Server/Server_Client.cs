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
        private MemoryStream memoryStream = new MemoryStream();
        BinaryFormatter binaryFormatter = new BinaryFormatter();

        public Packet CreateLoginPacket(EndPoint endPoint)
        {
            return new LoginPacket(endPoint);
        }

        public Server_Client()
        {
            ID = "";
            color = Color.Black;
        }

        public void TCPConnect(Socket socket)
        {
            this.tcpSocket = socket;
            stream = new NetworkStream(this.tcpSocket, true);
            breader = new BinaryReader(stream);
            bwriter = new BinaryWriter(stream);
        }

        public void UDPConnect(Socket socket, EndPoint endPoint)
        {
            this.udpSocket = socket;
            udpSocket.Connect(endPoint);
            TCPSend(CreateLoginPacket(udpSocket.LocalEndPoint));


            stream = new NetworkStream(this.tcpSocket, true);
            breader = new BinaryReader(stream);
            bwriter = new BinaryWriter(stream);
        }

        public void TCPSend(Packet data)
        {
            try
            {
                memoryStream.SetLength(0);
                binaryFormatter.Serialize(memoryStream, data);
                memoryStream.Flush();
                byte[] buffer = memoryStream.GetBuffer();
                memoryStream.SetLength(0);

                this.bwriter.Write(buffer.Length);
                this.bwriter.Write(buffer);
                this.bwriter.Flush();
            }
            catch (IOException e)
            {
                Console.WriteLine("Send Failed - " + e.Message);
            }
        }
        
        public void UDPSend(Packet data)
        {
            try
            {
                memoryStream.SetLength(0);
                binaryFormatter.Serialize(memoryStream, data);
                memoryStream.Flush();
                byte[] buffer = memoryStream.GetBuffer();
                memoryStream.SetLength(0);
                udpSocket.Send(buffer, buffer.Length, SocketFlags.None);
            }
            catch (IOException e)
            {
                Console.WriteLine("Send Failed - " + e.Message);
            }
        }

        public Packet TCPRead()
        {
            int noOfIncomingBytes;
            if ((noOfIncomingBytes = this.breader.ReadInt32()) != 0)
            {
                byte[] buffer = this.breader.ReadBytes(noOfIncomingBytes);
                memoryStream.Write(buffer, 0, noOfIncomingBytes);
                memoryStream.Position = 0;
                Packet rawPacket = binaryFormatter.Deserialize(memoryStream) as Packet;
                memoryStream.SetLength(0);
                return rawPacket;
            }
            return null;
        }

        public void Close()
        {
            tcpSocket.Close();
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
                    stream.Dispose();
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
