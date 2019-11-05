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
            client.Dispose();
        }
    }
}
