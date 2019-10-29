using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Windows.Forms;
using PacketData;

namespace Client
{
    public class Client_Client : IDisposable
    {
        TcpClient tcpClient;
        NetworkStream stream;
        StreamReader reader;
        StreamWriter writer;
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
                reader = new StreamReader(stream, Encoding.UTF8);
                writer = new StreamWriter(stream, Encoding.UTF8);
                readerThread = new Thread(ProccessServerResponse);
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
                writer.WriteLine(message);
                writer.Flush();
            }
        }

        public void Send(Packet data)
        {

        }

        void ProccessServerResponse()
        {
            string serverText;
            while ((serverText = reader.ReadLine()) != null)
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
