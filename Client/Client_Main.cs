using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    class Client_Main
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
            Application.Run(new NewUserSetup());
        }

        public static void ConnectToServer(object args)
        {
            Client_Client client = new Client_Client();

            Array argsArray;
            argsArray = (Array)args;
            string ip = (string)argsArray.GetValue(0);
            string portString = (string)argsArray.GetValue(1);
            string id = (string)argsArray.GetValue(2);
            Int32.TryParse(portString, out int port);
            if (!client.Connect(ip, port,id))
            {
                Console.WriteLine("Error Failed to Connect");
            }
            client.Dispose();
            StartLoop();
        }
    }
}
