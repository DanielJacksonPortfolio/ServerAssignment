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


            ChatWindow chatWindow = new ChatWindow();
            Client_Client client = new Client_Client(chatWindow);
            Application.Run(chatWindow);
            //StartLoop();
        }


        public static void StartLoop()
        {

            Application.Run();
        }

        public static void ConnectToServer(object args)
        {
            Array argsArray;
            argsArray = (Array)args;
            string ip = (string)argsArray.GetValue(0);
            string portString = (string)argsArray.GetValue(1);
            string id = (string)argsArray.GetValue(2);
            Int32.TryParse(portString, out int port);

            Client_Client client = (Client_Client)argsArray.GetValue(3);

            if(!client.Connect(ip, port, id))
            {
                client.Dispose();
                StartLoop();
            }
        }
    }
}
