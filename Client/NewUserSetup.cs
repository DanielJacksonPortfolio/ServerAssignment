using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Client
{
    public partial class NewUserSetup : Form
    {
        public NewUserSetup()
        {
            InitializeComponent();
        }
        private void ConnectButton_Click(object sender, EventArgs e)
        {
            object args = new object[3]{ IPInput.Text, PortInput.Text, UsernameInput.Text };
            Thread t = new Thread(new ParameterizedThreadStart(Client_Main.ConnectToServer));
            t.Start(args);        
            this.Close();
        }
    }
}
