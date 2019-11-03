using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using PacketData;

namespace Client
{
    public partial class ChatWindow : Form
    {
        delegate void UpdateServerLogDelegate(string message);
        delegate void CloseFormDelegate();
        UpdateServerLogDelegate updateServerLogDelegate;
        CloseFormDelegate closeFormDelegate;
        Client_Client client;


        void ApplyLayoutChanges()
        {
            IPInput.SelectionAlignment = HorizontalAlignment.Center;
            PortInput.SelectionAlignment = HorizontalAlignment.Center;
            UsernameInput.SelectionAlignment = HorizontalAlignment.Center;
        }

        public ChatWindow()
        {
            InitializeComponent();
            ApplyLayoutChanges();
            updateServerLogDelegate = new UpdateServerLogDelegate(UpdateServerLog);
            closeFormDelegate = new CloseFormDelegate(CloseForm);
            InputBox.Select();
        }
       
         public void StartConnection()
        {
            client.Run();
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
        }
        public void InitializeClient(Client_Client client)
        {
            this.client = client;
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
            client.ProcessMessage(InputBox.Text, PacketType.CHAT_MESSAGE);
            InputBox.Text = "";
        }

        private void InputBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                client.ProcessMessage(InputBox.Text, PacketType.CHAT_MESSAGE);
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

        private void ChatWindow_Load(object sender, EventArgs e)
        {
        }

        private void ChatWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (client != null)
            {
                client.ProcessMessage("/kill", PacketType.CHAT_MESSAGE);
                client.Stop();
            }
        }

        private void ServerLog_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.LinkText);
        }

        public bool ValidateIPv4()
        {
            if (String.IsNullOrWhiteSpace(IPInput.Text))
            {
                return false;
            }

            string[] splitValues = IPInput.Text.Split('.');
            if (splitValues.Length != 4)
            {
                return false;
            }

            byte tempForParsing;

            return splitValues.All(r => byte.TryParse(r, out tempForParsing));
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            bool ipValid = false;
            bool portValid = false;
            bool usernameValid = false;

            if (ValidateIPv4())
                ipValid = true;
            else
                UpdateServerLog("Error: Invalid IP - "+IPInput.Text);

            Int32.TryParse(PortInput.Text, out int port);

            if (port > 0 && port < 65536)
                portValid = true;
            else
                UpdateServerLog("Error: Invalid Port - "+ PortInput.Text);

            if (UsernameInput.Text != "")
                usernameValid = true;
            else
                UpdateServerLog("Error: You must have at least 1 character in your username");

            if (ipValid && portValid && usernameValid)
            {
                object args = new object[4] { IPInput.Text, PortInput.Text, UsernameInput.Text, this.client };
                Thread t = new Thread(new ParameterizedThreadStart(client.Connect));
                t.Start(args);
            }
        }

        private void RenameButton_Click(object sender, EventArgs e)
        {
            if(client.IsConnected())
            {
                if (UsernameInput.Text != "")
                    client.ProcessMessage("/rename " + UsernameInput.Text, PacketType.CHAT_MESSAGE);
                else
                    UpdateServerLog("Error: You must have at least 1 character in your username");
            }
        }

        private void DisconnectButton_Click(object sender, EventArgs e)
        {

        }
    }
}
