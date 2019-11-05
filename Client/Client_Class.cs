using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
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
                chatWindow.UpdateServerLog("Error - Failed to connect: IP - " + ipAddress + ", Port - " + port.ToString(), Color.DarkRed);
                connected = false;
            }
        }

        public void Run()
        {
            readerThread.Start();
            //ProcessMessage(idTemp,PacketType.INIT_MESSAGE);
            SendInitMessage(idTemp, chatWindow.GetColor());
        }

        public void Stop()
        {
            if (tcpClient != null)
                tcpClient.Close();
            if (readerThread != null)
                readerThread.Abort();
        }

        public void SendChatMessage(string message)
        {
            if (message.Length != 0 && !message.StartsWith("\n"))
            {
                Send(CreateChatPacket(message, Color.Black));
            }
        }
        public void SendInitMessage(string message, Color color)
        {
            Send(CreateInitPacket(message,color));
        }
        public void SendColor(Color color)
        {
            Send(CreateColorPacket(color));
        }

        public void Send(Packet data)
        {
            if (connected)
            {
                try
                {
                    memoryStream.SetLength(0);
                    binaryFormatter.Serialize(memoryStream, data);
                    memoryStream.Flush();
                    byte[] buffer = memoryStream.GetBuffer();
                    memoryStream.SetLength(0);

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
                    chatWindow.UpdateServerLog("Cannot Send Message - No Connection to server", Color.Red);
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
        
        public Packet CreateChatPacket(string message, Color color)
        {
            return new ChatMessagePacket(message, Color.Black);
        }
        public Packet CreateInitPacket(string message, Color color)
        {
            return new InitMessagePacket(message, color);
        }
        public Packet CreateColorPacket(Color color)
        {
            return new ColorPacket(color);
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
                    memoryStream.SetLength(0);
                    switch (rawPacket.type)
                    {
                        case PacketType.CHAT_MESSAGE:
                            {
                                ChatMessagePacket packet = (ChatMessagePacket)rawPacket;
                                ProcessServerResponse(packet.message, packet.sendersColor);
                                break;
                            }
                        case PacketType.DISCONNECT:
                            {
                                DisconnectPacket packet = (DisconnectPacket)rawPacket;
                                ProcessServerResponse(packet.message, Color.Black, packet.disconnectType);
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
