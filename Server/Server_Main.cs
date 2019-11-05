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

            ServerWindow serverWindow = new ServerWindow();
            Server_Server server = new Server_Server(serverWindow);
            Application.Run(serverWindow);
            server.Dispose();
        }
    }
}
