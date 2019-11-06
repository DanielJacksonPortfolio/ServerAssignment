using PacketData;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Drawing;
using System.Windows.Forms;

namespace Server
{

    class Server_Client : IDisposable
    {
        Socket socket;
        NetworkStream stream;
        bool op = false;
        bool disposed = false;
        public string ID { get; set; }
        public Color color { get; set; }
        public BinaryReader breader { get; private set; }
        public BinaryWriter bwriter { get; private set; }

        public Server_Client(Socket socket)
        {
            this.socket = socket;
            ID = "";
            color = Color.Black;
            stream = new NetworkStream(this.socket, true);
            breader = new BinaryReader(stream);
            bwriter = new BinaryWriter(stream);
        }

        public void Close()
        {
            socket.Close();
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

        public bool GetOP()
        {
            return op;
        }
    }
}
