using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    public partial class ServerWindow : Form
    {
        delegate void UpdateServerLogDelegate(string message);
        delegate void CloseFormDelegate();
        UpdateServerLogDelegate updateServerLogDelegate;
        CloseFormDelegate closeFormDelegate;
        Server_Server server;
        void ApplyLayoutChanges()
        {
            IPInput.SelectionAlignment = HorizontalAlignment.Center;
            PortInput.SelectionAlignment = HorizontalAlignment.Center;
            ServerNameInput.SelectionAlignment = HorizontalAlignment.Center;
        }

        public ServerWindow(Server_Server server)
        {
            InitializeComponent();
            ApplyLayoutChanges();
            updateServerLogDelegate = new UpdateServerLogDelegate(UpdateServerLog);
            closeFormDelegate = new CloseFormDelegate(CloseForm);
            this.server = server;
            InputBox.Select();
        }
        public void UpdateServerLog(string message)
        {
            try
            {
                if (ServerLog.InvokeRequired)
                {
                    Invoke(updateServerLogDelegate, message);
                }
                else
                {
                    ServerLog.Text += message += "\n";
                    ServerLog.SelectionStart = ServerLog.Text.Length;
                    ServerLog.ScrollToCaret();
                }
            }
            catch (System.InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (InvalidAsynchronousStateException e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void CloseForm()
        {
            try
            {
                if (this.InvokeRequired)
                {
                    Invoke(closeFormDelegate);
                }
                else
                {
                    this.Close();
                }
            }
            catch (System.InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
            }

        }

        private void Submit_Click(object sender, EventArgs e)
        {
            server.ProcessCommand(Message_Origins.SERVER, InputBox.Text);
            InputBox.Text = "";
        }

        private void InputBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                server.ProcessCommand(Message_Origins.SERVER, InputBox.Text);
                InputBox.Text = "";
            }
        }

        private void InputBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                InputBox.Text = "";
            }
        }

        private void ServerWindow_Load(object sender, EventArgs e)
        {
            server.Run();
        }

        private void ServerWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            server.CloseServer();
        }

        private void ServerLog_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.LinkText);
        }

        private void RenameButton_Click(object sender, EventArgs e)
        {
            if (ServerNameInput.Text != "")
                server.ProcessCommand(Message_Origins.SERVER, "/rename_server " + ServerNameInput.Text);
            else
                server.ProcessCommand(Message_Origins.SERVER, "/log Error: There must have at least 1 character in the servers name");
         }

        private void ConnectButton_Click(object sender, EventArgs e)
        {

        }
    }
}
