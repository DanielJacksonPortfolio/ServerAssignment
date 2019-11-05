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
        delegate void UpdateServerLogDelegate(string message, Color color);
        delegate void UpdateConnectionLabelsDelegate(bool connecting);
        delegate void CloseFormDelegate();
        UpdateServerLogDelegate updateServerLogDelegate;
        UpdateConnectionLabelsDelegate updateConnectionLabelsDelegate;
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
            updateConnectionLabelsDelegate = new UpdateConnectionLabelsDelegate(UpdateConnectionLabels);
            closeFormDelegate = new CloseFormDelegate(CloseForm);
            InputBox.Select();
        }
       
        public void StartConnection()
        {
            client.Run();
            UpdateConnectionLabels(true);
        }
        public void UpdateConnectionLabels(bool connecting)
        {
            try
            {
                if (ServerLog.InvokeRequired)
                {
                    Invoke(updateConnectionLabelsDelegate, connecting);
                }
                else
                {
                    if (connecting)
                    {
                        CurrentIPLabel.Text = IPInput.Text;
                        CurrentPortLabel.Text = PortInput.Text;
                    }
                    else
                    {
                        LastIPLabel.Text = CurrentIPLabel.Text;
                        LastPortLabel.Text = CurrentPortLabel.Text;
                        CurrentIPLabel.Text = "N/A";
                        CurrentPortLabel.Text = "N/A";
                    }
                }
            }
            catch (System.InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
            }

        }
        public void UpdateServerLog(string message, Color color )
        {
            try
            {
                if (ServerLog.InvokeRequired)
                {
                    Invoke(updateServerLogDelegate, message, color);
                }
                else
                {
                    ServerLog.SelectionStart = ServerLog.TextLength;
                    ServerLog.SelectionLength = 0;
                    ServerLog.SelectionColor = color;
                    ServerLog.AppendText(message + "\n");
                    ServerLog.SelectionColor = ServerLog.ForeColor;

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
        public void InitializeClient(Client_Client client)
        {
            this.client = client;
            UpdateServerLog("Welcome to my chat window. You can connect to a server by inputing your desired IP and Port into the 'Connection Destination' boxes. Choose a username and click connect\n-------------------------------------------------------------------------------------------------------------------------------------------------------------",Color.Fuchsia);
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

        bool ValidateIPv4(string ip)
        {
            if (String.IsNullOrWhiteSpace(ip))
            {
                return false;
            }

            string[] splitValues = ip.Split('.');
            if (splitValues.Length != 4)
            {
                return false;
            }

            byte tempForParsing;

            return splitValues.All(r => byte.TryParse(r, out tempForParsing));
        }

        public void ProcessConnect(string ipIn = "NONE", string portIn = "NONE", string usernameIn = "NONE")
        {
            if (!client.IsConnected())
            {
                bool ipValid = false;
                bool portValid = false;
                bool usernameValid = false;
                if (ipIn == "NONE")
                {
                    ipIn = IPInput.Text;
                }
                if (ValidateIPv4(ipIn))
                    ipValid = true;
                else
                    UpdateServerLog("Error: Invalid IP - " + ipIn, Color.DarkRed);


                if (portIn == "NONE")
                {
                    portIn = PortInput.Text;
                }

                Int32.TryParse(portIn, out int port);

                if (port > 0 && port < 65536)
                    portValid = true;
                else
                    UpdateServerLog("Error: Invalid Port - " + portIn, Color.DarkRed);


                if (usernameIn == "NONE")
                {
                    usernameIn = UsernameInput.Text;
                }

                if (usernameIn != "")
                    usernameValid = true;
                else
                    UpdateServerLog("Error: You must have at least 1 character in your username", Color.DarkRed);

                if (ipValid && portValid && usernameValid)
                {
                    object args = new object[4] { ipIn, port, usernameIn, this.client };
                    Thread t = new Thread(new ParameterizedThreadStart(client.Connect));
                    t.Start(args);
                }
            }
            else
            {
                UpdateServerLog("Error: Already connected to a server", Color.DarkRed);
            }
        }
       
        private void ConnectButton_Click(object sender, EventArgs e)
        {
            ProcessConnect();
        }

        private void RenameButton_Click(object sender, EventArgs e)
        {
            if(client.IsConnected())
            {
                if (UsernameInput.Text != "")
                    client.ProcessMessage("/rename " + UsernameInput.Text, PacketType.CHAT_MESSAGE);
                else
                    UpdateServerLog("Error: You must have at least 1 character in your username", Color.DarkRed);
            }
        }

        private void DisconnectButton_Click(object sender, EventArgs e)
        {
            if (client.IsConnected())
            {
                client.ProcessMessage("/disconnect",PacketType.CHAT_MESSAGE);
            }
            else
            {
                UpdateServerLog("Error: You aren't connected to a server", Color.DarkRed);
            }
        }
    }
}
