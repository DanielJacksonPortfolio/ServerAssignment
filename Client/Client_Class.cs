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
        //StreamReader reader;
        BinaryReader reader;
        //StreamWriter writer;
        BinaryWriter writer;
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
                //reader = new StreamReader(stream, Encoding.UTF8);
                reader = new BinaryReader(stream);
                //writer = new StreamWriter(stream, Encoding.UTF8);
                writer = new BinaryWriter(stream);
                readerThread = new Thread(Recieve); // Process Server Response
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
                ProcessMessage(idTemp);
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

        public void ProcessMessage(string message)
        {
            if (message.Length != 0 && !message.StartsWith("\n"))
            {
                Send(CreatePacket(message));
                //writer.WriteLine(message);
                //writer.Flush();
            }
            else
            {
                Send(new EmptyPacket());
            }
        }

        public void Send(Packet data)
        {
            binaryFormatter.Serialize(memoryStream, data);
            byte[] buffer = memoryStream.GetBuffer();

            writer.Write(buffer.Length);
            writer.Write(buffer);
            writer.Flush();
        }
        
        public Packet CreatePacket(string message)
        {
            Packet data;
            data = new ChatMessagePacket(message);
            return data;
        }

        public void Recieve()
        {
            int noOfIncomingBytes;
            while ((noOfIncomingBytes = reader.ReadInt32()) != 0)
            {
                Packet rawPacket = binaryFormatter.Deserialize(memoryStream) as Packet;
                switch (rawPacket.type)
                {
                    case PacketType.CHAT_MESSAGE:
                        ChatMessagePacket packet = (ChatMessagePacket)rawPacket;
                        ProcessServerResponse(packet.message);
                        break;
                }
            }
        }

        void ProcessServerResponse(string serverText)
        {
            //while ((serverText = reader.ReadLine()) != null)
            //{
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
            //}

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
                    reader.Dispose();
                    writer.Dispose();
                    stream.Dispose();
                }
                disposed = true;
            }
        }
    }
}
