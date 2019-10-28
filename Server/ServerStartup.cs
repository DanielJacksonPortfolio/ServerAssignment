using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Server
{
    public partial class ServerStartup : Form
    {
        public ServerStartup()
        {
            InitializeComponent();
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            object args = new object[3] { IPInput.Text, PortInput.Text, UsernameInput.Text };
            Thread t = new Thread(new ParameterizedThreadStart(Server_Main.StartServer));
            t.Start(args);
            this.Close();
        }
    }
}
