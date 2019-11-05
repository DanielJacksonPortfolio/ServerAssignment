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

namespace Server
{
    public partial class ServerWindow : Form
    {
        delegate void UpdateServerLogDelegate(string message, Color color);
        delegate void UpdateConnectionLabelsDelegate(bool connecting);
        delegate void CloseFormDelegate();
        UpdateServerLogDelegate updateServerLogDelegate;
        UpdateConnectionLabelsDelegate updateConnectionLabelsDelegate;
        CloseFormDelegate closeFormDelegate;
        Server_Server server;
        void ApplyLayoutChanges()
        {
            IPInput.SelectionAlignment = HorizontalAlignment.Center;
            PortInput.SelectionAlignment = HorizontalAlignment.Center;
            ServerNameInput.SelectionAlignment = HorizontalAlignment.Center;
        }

        public ServerWindow()//Server_Server server
        {
            InitializeComponent();
            ApplyLayoutChanges();
            updateServerLogDelegate = new UpdateServerLogDelegate(UpdateServerLog);
            updateConnectionLabelsDelegate = new UpdateConnectionLabelsDelegate(UpdateConnectionLabels);
            closeFormDelegate = new CloseFormDelegate(CloseForm);
            //this.server = server;
            InputBox.Select();
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

        public void UpdateServerLog(string message, Color color)
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

        public void InitializeServer(Server_Server server)
        {
            this.server = server;
            UpdateServerLog("Welcome to the server window. You can listen by inputing your desired IP and Port into the 'Connection Destination' boxes. Choose a server name and click connect\n-------------------------------------------------------------------------------------------------------------------------------------------------------------",Color.FromArgb(80,240,60));
        }
        public void StartConnection()
        {
            server.Run();
            UpdateConnectionLabels(true);
        }

        private void ServerWindow_Load(object sender, EventArgs e)
        {
        }

        private void ServerWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (server != null)
            {
                server.CloseServer();
            }
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
                UpdateServerLog("Error: There must have at least 1 character in the servers name", Color.DarkRed);
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

        public void ProcessConnect(string ipIn = "NONE", string portIn = "NONE", string serverNameIn = "NONE")
        {
            bool ipValid = false;
            bool portValid = false;
            bool serverNameValid = false;
            if (ipIn == "NONE")
            {
                ipIn = IPInput.Text;
            }
            if (ValidateIPv4(ipIn))
                ipValid = true;
            else
                UpdateServerLog("Error: Invalid IP - " + ipIn,Color.FromArgb(230,20,40));


            if (portIn == "NONE")
            {
                portIn = PortInput.Text;
            }

            Int32.TryParse(portIn, out int port);

            if (port > 0 && port < 65536)
                portValid = true;
            else
                UpdateServerLog("Error: Invalid Port - " + portIn, Color.FromArgb(230, 20, 40));


            if (serverNameIn == "NONE")
            {
                serverNameIn = ServerNameInput.Text;
            }

            if (serverNameIn != "")
                serverNameValid = true;
            else
                UpdateServerLog("Error: You must have at least 1 character in your username", Color.FromArgb(230, 20, 40));

            if (ipValid && portValid && serverNameValid)
            {
                //UpdateServerLog("Valid Connection");
                object args = new object[4] { ipIn, port, serverNameIn, this.server };
                Thread t = new Thread(new ParameterizedThreadStart(server.Connect));
                t.Start(args);
            }
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            ProcessConnect();
        }

        private void DisconnectButton_Click(object sender, EventArgs e)
        {
            if (server.IsConnected())
            {
                server.CloseServer();
            }
            else
            {
                UpdateServerLog("Error: You aren't connected to a server", Color.FromArgb(230, 20, 40));
            }
        }
    }
}
