﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class ChatWindow : Form
    {
        delegate void UpdateServerLogDelegate(string message);
        delegate void CloseFormDelegate();
        UpdateServerLogDelegate updateServerLogDelegate;
        CloseFormDelegate closeFormDelegate;
        Client_Client client;
        public ChatWindow(Client_Client client)
        {
            InitializeComponent();
            updateServerLogDelegate = new UpdateServerLogDelegate(UpdateServerLog);
            closeFormDelegate = new CloseFormDelegate(CloseForm);
            this.client = client;
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
            client.ProcessMessage(InputBox.Text);
            InputBox.Text = "";
        }

        private void InputBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                client.ProcessMessage(InputBox.Text);
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
            client.Run();
        }

        private void ChatWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            client.Stop();
        }

        private void ServerLog_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.LinkText);
        }
    }
}