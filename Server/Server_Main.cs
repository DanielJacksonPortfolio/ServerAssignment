using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    static class Server_Main
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            StartLoop();
        }
        public static void StartLoop()
        {
            Application.Run(new ServerStartup());
        }

        public static void StartServer(object args)
        {
            Array argsArray;
            argsArray = (Array)args;
            string ip = (string)argsArray.GetValue(0);
            string portString = (string)argsArray.GetValue(1);
            string id = (string)argsArray.GetValue(2);
            Int32.TryParse(portString, out int port);

            Console.WriteLine("Server Started...");
            Server_Server server = new Server_Server();
            server.Connect(ip, port, id);
            server.Dispose();
            StartLoop();
        }
    }
}
