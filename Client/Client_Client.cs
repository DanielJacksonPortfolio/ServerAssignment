using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using PacketData;

namespace Client
{
    public class Client_Client : IDisposable
    {
        TcpClient tcpClient;
        UdpClient udpClient;
        NetworkStream stream;
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        BinaryReader breader;
        BinaryWriter bwriter;
        Thread readerThreadTCP;
        Thread readerThreadUDP;
        ChatWindow chatWindow;
        GameWindow gameWindow;
        string idTemp = "";
        bool disposed = false;
        bool connected = false;
        IPAddress ipAddress;
        int port;

        public bool IsConnected()
        {
            return this.connected;
        }

        public Client_Client(ChatWindow chatWindow)
        {
            this.chatWindow = chatWindow;
            this.chatWindow.InitializeClient(this);
        }

        public void Connect(object args)
        {
            Array argsArray= (Array)args;
            string ip = (string)argsArray.GetValue(0);
            port = (int)argsArray.GetValue(1);
            string id = (string)argsArray.GetValue(2);
            try
            {
                this.ipAddress = IPAddress.Parse(ip);
                idTemp = id;
                tcpClient = new TcpClient();
                tcpClient.Connect(ipAddress, port);
                stream = tcpClient.GetStream();
                breader = new BinaryReader(stream);
                bwriter = new BinaryWriter(stream);

                udpClient = new UdpClient();
                udpClient.Client.ReceiveBufferSize = 2048;
                udpClient.Connect(ipAddress, port);

                connected = true;
                readerThreadTCP = new Thread(TCPReceive);
                chatWindow.StartConnection();
            }
            catch(SocketException e)
            {
                Console.WriteLine("Exception: Connection error - "+e.Message);
                chatWindow.UpdateServerLog("Error: Failed to connect: IP - " + ipAddress + ", Port - " + port.ToString(), Color.DarkRed);
                connected = false;
            }
        }
        public void Run()
        {
            readerThreadTCP.Start();

            SendLogin(udpClient.Client.LocalEndPoint);
            SendInitMessage(idTemp, chatWindow.GetColor());
        }

        public void Stop()
        {
            if (tcpClient != null)
                tcpClient.Close();
            if (udpClient != null)
                udpClient.Close();
            if (readerThreadTCP != null)
                readerThreadTCP.Abort();
            if (readerThreadUDP != null)
                readerThreadUDP.Abort();
        }

        public void SendChatMessage(string message)
        {
            if (message.Length != 0 && !message.StartsWith("\n"))
            {
                if (message.EndsWith("\n"))
                    message = message.Substring(0, message.Length - 1);
                if(message.StartsWith("/udp"))
                    UDPSend(CreateChatPacket(message.Substring(4), Color.Black));
                else if(message.StartsWith("/udp "))
                    UDPSend(CreateChatPacket(message.Substring(5), Color.Black));
                else if(message.StartsWith("/tcp"))
                    TCPSend(CreateChatPacket(message.Substring(4), Color.Black));
                else if(message.StartsWith("/tcp "))
                    TCPSend(CreateChatPacket(message.Substring(5), Color.Black));
                else
                    TCPSend(CreateChatPacket(message, Color.Black));
            }
        }
        public void SendInitMessage(string message, Color color)
        {
            TCPSend(CreateInitPacket(message,color));
        }
        public void SendColor(Color color)
        {
            TCPSend(CreateColorPacket(color));
        }
        public void SendLogin(EndPoint endPoint)
        {
            TCPSend(CreateLoginPacket(endPoint));
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
            if (connected)
            {
                try
                {
                    byte[] buffer = Serialize(data);
                    bwriter.Write(buffer.Length);
                    bwriter.Write(buffer);
                    bwriter.Flush();
                }
                catch (IOException e)
                {
                    Console.WriteLine("Client TCP Send Failed - " + e.Message);
                }
            }
            else
            {
                if(!CheckForConnect(data))
                    chatWindow.UpdateServerLog("Cannot Send Message - No Connection to server", Color.Red);
            }
        }

        public void UDPSend(Packet data)
        {
            try
            {
                byte[] buffer = Serialize(data);
                udpClient.Send(buffer, buffer.Length);
            }
            catch (IOException e)
            {
                Console.WriteLine("Client UDP Send Failed - " + e.Message);
            }
            catch (ObjectDisposedException e)
            {
                Console.WriteLine("Client UDP Send Failed - " + e.Message);
            }
        }

        public Packet UDPRead()
        {
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddress,port);
            return Deserialize(udpClient.Receive(ref ipEndPoint));
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
                        ip = connectData[1];
                    if (connectData.Count > 2)
                        port = connectData[2];
                    if (connectData.Count > 3)
                    {
                        username = connectData[3];
                        username[0].ToString().ToUpper();
                    }
                    chatWindow.ProcessConnect(ip,port,username);
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }
        
        Packet CreateChatPacket(string message, Color color)
        {
            return new ChatMessagePacket(message, color);
        }
        Packet CreateInitPacket(string message, Color color)
        {
            return new InitMessagePacket(message, color);
        }
        Packet CreateColorPacket(Color color)
        {
            return new ColorPacket(color);
        }
        Packet CreateLoginPacket(EndPoint endPoint)
        {
            return new LoginPacket(endPoint);
        }
        
        public Packet CreatePlayerUpdatePacket(List<float> playerData, int playerID)
        {
            return new PlayerUpdatePacket(playerID, playerData);
        }

        void HandlePacket(Packet rawPacket)
        {
            switch (rawPacket.type)
            {
                case PacketType.CHAT_MESSAGE:
                    {
                        ChatMessagePacket packet = (ChatMessagePacket)rawPacket;
                        ProcessServerResponse(packet.message, packet.sendersColor);
                        break;
                    }
                case PacketType.LOGIN:
                    {
                        LoginPacket packet = (LoginPacket)rawPacket;
                        udpClient.Connect((IPEndPoint)packet.endPoint);
                        readerThreadUDP = new Thread(UDPReceive);
                        readerThreadUDP.Start();
                        break;
                    }
                case PacketType.DISCONNECT:
                    {
                        DisconnectPacket packet = (DisconnectPacket)rawPacket;
                        ProcessServerResponse(packet.message, Color.Black, packet.disconnectType);
                        return;
                    }
                case PacketType.INIT_GAME:
                    {
                        InitGamePacket packet = (InitGamePacket)rawPacket;
                        this.gameWindow = new GameWindow(packet, this);
                        Thread t = new Thread(RunGame);
                        t.Start();
                        return;
                    }
                case PacketType.GAME_WORLD_UPDATE:
                    {
                        this.gameWindow.WorldUpdate((WorldUpdatePacket)rawPacket);
                        return;
                    }
            }
        }

        void RunGame()
        {
            Application.Run(gameWindow);
        }

        public void TCPReceive()
        {
            int noOfIncomingBytes;
            try
            {
                while ((noOfIncomingBytes = breader.ReadInt32()) != 0)
                {
                    byte[] buffer = breader.ReadBytes(noOfIncomingBytes);
                    MemoryStream memoryStream = new MemoryStream();
                    memoryStream.Write(buffer, 0, noOfIncomingBytes);
                    memoryStream.Position = 0;
                    Packet rawPacket = binaryFormatter.Deserialize(memoryStream) as Packet;
                    HandlePacket(rawPacket);
                }
            }
            catch(IOException e)
            {
                Console.WriteLine("Client TCP Receive Failed - " + e.Message);
            }
        }
        public void UDPReceive()
        {
            try
            {
                while (true)
                {
                    HandlePacket(UDPRead());
                }
            }
            catch(IOException e)
            {
                Console.WriteLine("Client UDP Receive Failed - " + e.Message);
            }
            catch(SocketException e)
            {
                Console.WriteLine("Client UDP Receive Failed - " + e.Message);
            }
        }

        void ProcessServerResponse(string serverText,Color color, DisconnectPacket.DisconnectType dType = DisconnectPacket.DisconnectType.INVALID)
        {
            switch(dType)
            {
                case DisconnectPacket.DisconnectType.CLEAN:
                case DisconnectPacket.DisconnectType.SERVER_DEAD:
                case DisconnectPacket.DisconnectType.USER_KILL:
                case DisconnectPacket.DisconnectType.SERVER_KILL:
                    {
                        chatWindow.UpdateServerLog(serverText, Color.Yellow);
                        connected = false;
                        chatWindow.UpdateConnectionLabels(false);
                        this.Stop();
                        break;
                    }
            }
            chatWindow.UpdateServerLog(serverText, color);
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
                        udpClient.Dispose();
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
