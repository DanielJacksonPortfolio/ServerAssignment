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

        public bool IsConnected()
        {
            return this.connected;
        }

        public Client_Client(ChatWindow chatWindow)
        {
            this.chatWindow = chatWindow;
            this.chatWindow.InitializeClient(this);
            this.chatWindow.UpdateServerLog("Welcome to my chat window. You can connect to a server by inputing your desired IP and Port into the 'Connection Destination' boxes. Choose a username and click connect\n-------------------------------------------------------------------------------------------------------------------------------------------------------------");
        }
        public void Connect(object args)
        {
            Array argsArray;
            argsArray = (Array)args;
            string ipAddress = (string)argsArray.GetValue(0);
            int port = (int)argsArray.GetValue(1);
            string id = (string)argsArray.GetValue(2);
            try
            {
                idTemp = id;
                tcpClient = new TcpClient();
                tcpClient.Connect(ipAddress, port);
                stream = tcpClient.GetStream();
                breader = new BinaryReader(stream);
                bwriter = new BinaryWriter(stream);
                connected = true;
                readerThread = new Thread(Receive);
                chatWindow.StartConnection();
            }
            catch(SocketException e)
            {
                Console.WriteLine("Exception: Connection error - "+e.Message);
                chatWindow.UpdateServerLog("Error - Failed to connect: IP - " + ipAddress + ", Port - " + port.ToString());
                connected = false;
            }
        }

        public void Run()
        {
            readerThread.Start();
            ProcessMessage(idTemp,PacketType.INIT_MESSAGE);
        }

        public void Stop()
        {
            if (tcpClient != null)
                tcpClient.Close();
            if (readerThread != null)
                readerThread.Abort();
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
                if(!CheckForConnect(data))
                {
                    chatWindow.UpdateServerLog("Cannot Send Message - No Connection to server");
                }
            }
        }

        bool CheckForConnect(Packet data)
        {
            if (data.type == PacketType.CHAT_MESSAGE)
            {
                ChatMessagePacket data2 = (ChatMessagePacket)data;
                data2.message = data2.message.ToLower();
                if (data2.message.StartsWith("/connect"))
                {
                    List<string> connectData = new List<string>(data2.message.Split(' '));
                    string ip = "NONE";
                    string port = "NONE";
                    string username = "NONE";
                    if (connectData.Count > 1)
                    {
                        ip = connectData[1];
                    }
                    if (connectData.Count > 2)
                    {
                        port = connectData[2];
                    }
                    if (connectData.Count > 3)
                    {
                        username = connectData[3];
                        username[0].ToString().ToUpper();
                    }
                    chatWindow.ProcessConnect(ip,port,username);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
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
                        case PacketType.DISCONNECT:
                            {
                                DisconnectPacket packet = (DisconnectPacket)rawPacket;
                                ProcessServerResponse(packet.message, packet.disconnectType);
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

        void ProcessServerResponse(string serverText, DisconnectPacket.DisconnectType dType = DisconnectPacket.DisconnectType.INVALID)
        {
            switch(dType)
            {
                case DisconnectPacket.DisconnectType.CLEAN:
                case DisconnectPacket.DisconnectType.SERVER_DEAD:
                case DisconnectPacket.DisconnectType.USER_KILL:
                case DisconnectPacket.DisconnectType.SERVER_KILL:
                    {
                        chatWindow.UpdateServerLog(serverText);
                        connected = false;
                        chatWindow.UpdateConnectionLabels(false);
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
                        breader.Dispose();
                        bwriter.Dispose();
                        stream.Dispose();
                        chatWindow.Dispose();
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
