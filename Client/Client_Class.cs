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
        bool connected = false;

        public Client_Client(ChatWindow chatWindow)
        {
            tcpClient = new TcpClient();
            this.chatWindow = chatWindow;
            this.chatWindow.InitializeClient(this);
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
                connected = true;
                readerThread = new Thread(Receive); // Process Server Response
                //Application.Run(chatWindow);
            }
            catch(SocketException e)
            {
                Console.WriteLine("Exception: Connection error - "+e.Message);
                return false;
            }
            return true;
        }

        public void Run()
        {
            readerThread.Start();
            ProcessMessage(idTemp,PacketType.INIT_MESSAGE);
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
            if (connected)
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
                catch (IOException e)
                {
                    Console.WriteLine("Send Failed - " + e.Message);
                }
            }
            else
            {
                chatWindow.UpdateServerLog("Cannot Send Message - No Connection to server");
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
            try
            {
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
            catch(IOException e)
            {
                Console.WriteLine("Receive Failed - "+e.Message);
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
                        connected = false;
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
                    try
                    {
                        tcpClient.Dispose();
                        chatWindow.Dispose();
                        breader.Dispose();
                        bwriter.Dispose();
                        stream.Dispose();
                    }
                    catch (NullReferenceException e)
                    {
                        Console.WriteLine("Can't Dispose of nothing - " + e.Message);
                    }
                }
                disposed = true;
            }
        }
    }
}
