using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using PacketData;

namespace Client
{
    public class Client_Client : IDisposable
    {
        TcpClient tcpClient;
        NetworkStream stream;
        MemoryStream memoryStream = new MemoryStream();
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        BinaryReader breader;
        BinaryWriter bwriter;
        Thread readerThread;
        ChatWindow chatWindow;
        string idTemp = "TEMP";
        bool disposed = false;

        public Client_Client()
        {
            tcpClient = new TcpClient();
            chatWindow = new ChatWindow(this);
        }

        public bool Connect(string ipAddress, int port, string id)
        {
            try
            {
                idTemp = id;
                tcpClient.Connect(ipAddress, port);
                stream = tcpClient.GetStream();
                breader = new BinaryReader(stream);
                bwriter = new BinaryWriter(stream);
                readerThread = new Thread(Receive); // Process Server Response
                Application.Run(chatWindow);
            }
            catch
            {
                Console.WriteLine("Exception: Connection error");
                return false;
            }
            return true;
        }

        public void Run()
        {
            try
            {
                readerThread.Start();
                ProcessMessage(idTemp,PacketType.INIT_MESSAGE);
            }
            catch
            {
                Console.WriteLine("Client Exitted");
            }
        }

        public void Stop()
        {
            readerThread.Abort();
            tcpClient.Close();
        }

        public void ProcessMessage(string message, PacketType packetType)
        {
            if (message.Length != 0 && !message.StartsWith("\n"))
            {
                Send(CreatePacket(message, packetType));
            }
            else
            {
                Send(new EmptyPacket());
            }
        }

        public void Send(Packet data)
        {
            try
            {
                memoryStream = new MemoryStream();
                binaryFormatter.Serialize(memoryStream, data);
                memoryStream.Flush();
                byte[] buffer = memoryStream.GetBuffer();
                memoryStream = new MemoryStream();

                bwriter.Write(buffer.Length);
                bwriter.Write(buffer);
                bwriter.Flush();
            }
            catch
            {
                Console.WriteLine("Send Failed");
                chatWindow.CloseForm();
            }
        }
        
        public Packet CreatePacket(string message, PacketType packetType)
        {
            Packet data;
            switch (packetType)
            {
                case PacketType.INIT_MESSAGE:
                    data = new InitMessagePacket(message);
                    break;
                default:
                    data = new ChatMessagePacket(message);
                    break;
            }
            return data;
        }

        public void Receive()
        {
            int noOfIncomingBytes;
            while ((noOfIncomingBytes = breader.ReadInt32()) != 0)
            {
                byte[] buffer = breader.ReadBytes(noOfIncomingBytes);
                memoryStream.Write(buffer, 0, noOfIncomingBytes);
                memoryStream.Position = 0;
                Packet rawPacket = binaryFormatter.Deserialize(memoryStream) as Packet;
                memoryStream = new MemoryStream();
                switch (rawPacket.type)
                {
                    case PacketType.CHAT_MESSAGE:
                        {
                            ChatMessagePacket packet = (ChatMessagePacket)rawPacket;
                            ProcessServerResponse(packet.message);
                            break;
                        }
                    case PacketType.INIT_MESSAGE:
                        {
                            InitMessagePacket packet = (InitMessagePacket)rawPacket;
                            ProcessServerResponse(packet.message);
                            return;
                        }
                }
            }
        }

        void ProcessServerResponse(string serverText)
        {
            switch(serverText)
            {
                case "CODE::KILL":
                    {
                        chatWindow.CloseForm();
                        break;
                    }
                case "CODE::SERVER_DEAD":
                    {
                        serverText = "Disconnected from Server - Reason: Force Termination";
                        chatWindow.UpdateServerLog(serverText);
                        this.Stop();
                        break;
                    }
            }

            chatWindow.UpdateServerLog(serverText);

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
                    tcpClient.Dispose();
                    chatWindow.Dispose();
                    breader.Dispose();
                    bwriter.Dispose();
                    stream.Dispose();
                }
                disposed = true;
            }
        }
    }
}
