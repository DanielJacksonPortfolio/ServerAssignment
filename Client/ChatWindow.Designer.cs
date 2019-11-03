namespace Client
{
    partial class ChatWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChatWindow));
            this.ServerLog = new System.Windows.Forms.RichTextBox();
            this.InputBox = new System.Windows.Forms.RichTextBox();
            this.Submit = new System.Windows.Forms.Button();
            this.ChatInputPanel = new System.Windows.Forms.Panel();
            this.ConnectionControlPanel = new System.Windows.Forms.Panel();
            this.RenameButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.UsernameInput = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.DisconnectButton = new System.Windows.Forms.Button();
            this.ConnectButton = new System.Windows.Forms.Button();
            this.LineBreak3 = new System.Windows.Forms.Panel();
            this.CurrentPortLabel = new System.Windows.Forms.Label();
            this.CurrentIPLabel = new System.Windows.Forms.Label();
            this.LineBreak2 = new System.Windows.Forms.Panel();
            this.LastConnectionLabel = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.LineBreak1 = new System.Windows.Forms.Panel();
            this.CurrentConnectionLabel = new System.Windows.Forms.Label();
            this.PortLabel2 = new System.Windows.Forms.Label();
            this.IPLabel2 = new System.Windows.Forms.Label();
            this.ConnectionDestinationLabel = new System.Windows.Forms.Label();
            this.PortLabel1 = new System.Windows.Forms.Label();
            this.IPLabel1 = new System.Windows.Forms.Label();
            this.PortInput = new System.Windows.Forms.RichTextBox();
            this.IPInput = new System.Windows.Forms.RichTextBox();
            this.LastPortLabel = new System.Windows.Forms.Label();
            this.LastIPLabel = new System.Windows.Forms.Label();
            this.ChatInputPanel.SuspendLayout();
            this.ConnectionControlPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // ServerLog
            // 
            this.ServerLog.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ServerLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ServerLog.Font = new System.Drawing.Font("Century Gothic", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ServerLog.ForeColor = System.Drawing.SystemColors.WindowText;
            this.ServerLog.Location = new System.Drawing.Point(10, 10);
            this.ServerLog.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.ServerLog.Name = "ServerLog";
            this.ServerLog.ReadOnly = true;
            this.ServerLog.Size = new System.Drawing.Size(1420, 930);
            this.ServerLog.TabIndex = 1;
            this.ServerLog.Text = "";
            this.ServerLog.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.ServerLog_LinkClicked);
            // 
            // InputBox
            // 
            this.InputBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.InputBox.Font = new System.Drawing.Font("Century Gothic", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InputBox.Location = new System.Drawing.Point(10, 10);
            this.InputBox.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.InputBox.Name = "InputBox";
            this.InputBox.Size = new System.Drawing.Size(1630, 110);
            this.InputBox.TabIndex = 2;
            this.InputBox.Text = "";
            this.InputBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.InputBox_KeyDown);
            this.InputBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.InputBox_KeyUp);
            // 
            // Submit
            // 
            this.Submit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.Submit.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.Submit.FlatAppearance.BorderSize = 0;
            this.Submit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Submit.ForeColor = System.Drawing.SystemColors.Control;
            this.Submit.Location = new System.Drawing.Point(1650, 10);
            this.Submit.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.Submit.Name = "Submit";
            this.Submit.Size = new System.Drawing.Size(258, 110);
            this.Submit.TabIndex = 0;
            this.Submit.Text = "Submit";
            this.Submit.UseVisualStyleBackColor = false;
            this.Submit.Click += new System.EventHandler(this.Submit_Click);
            // 
            // ChatInputPanel
            // 
            this.ChatInputPanel.Controls.Add(this.InputBox);
            this.ChatInputPanel.Controls.Add(this.Submit);
            this.ChatInputPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ChatInputPanel.Location = new System.Drawing.Point(0, 950);
            this.ChatInputPanel.Name = "ChatInputPanel";
            this.ChatInputPanel.Size = new System.Drawing.Size(1920, 130);
            this.ChatInputPanel.TabIndex = 3;
            // 
            // ConnectionControlPanel
            // 
            this.ConnectionControlPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.ConnectionControlPanel.Controls.Add(this.RenameButton);
            this.ConnectionControlPanel.Controls.Add(this.label1);
            this.ConnectionControlPanel.Controls.Add(this.UsernameInput);
            this.ConnectionControlPanel.Controls.Add(this.panel1);
            this.ConnectionControlPanel.Controls.Add(this.DisconnectButton);
            this.ConnectionControlPanel.Controls.Add(this.ConnectButton);
            this.ConnectionControlPanel.Controls.Add(this.LineBreak3);
            this.ConnectionControlPanel.Controls.Add(this.CurrentPortLabel);
            this.ConnectionControlPanel.Controls.Add(this.CurrentIPLabel);
            this.ConnectionControlPanel.Controls.Add(this.LineBreak2);
            this.ConnectionControlPanel.Controls.Add(this.LastConnectionLabel);
            this.ConnectionControlPanel.Controls.Add(this.label5);
            this.ConnectionControlPanel.Controls.Add(this.label6);
            this.ConnectionControlPanel.Controls.Add(this.LineBreak1);
            this.ConnectionControlPanel.Controls.Add(this.CurrentConnectionLabel);
            this.ConnectionControlPanel.Controls.Add(this.PortLabel2);
            this.ConnectionControlPanel.Controls.Add(this.IPLabel2);
            this.ConnectionControlPanel.Controls.Add(this.ConnectionDestinationLabel);
            this.ConnectionControlPanel.Controls.Add(this.PortLabel1);
            this.ConnectionControlPanel.Controls.Add(this.IPLabel1);
            this.ConnectionControlPanel.Controls.Add(this.PortInput);
            this.ConnectionControlPanel.Controls.Add(this.IPInput);
            this.ConnectionControlPanel.Controls.Add(this.LastPortLabel);
            this.ConnectionControlPanel.Controls.Add(this.LastIPLabel);
            this.ConnectionControlPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.ConnectionControlPanel.Location = new System.Drawing.Point(1440, 0);
            this.ConnectionControlPanel.Name = "ConnectionControlPanel";
            this.ConnectionControlPanel.Size = new System.Drawing.Size(480, 950);
            this.ConnectionControlPanel.TabIndex = 4;
            // 
            // RenameButton
            // 
            this.RenameButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.RenameButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.RenameButton.FlatAppearance.BorderSize = 0;
            this.RenameButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RenameButton.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.RenameButton.ForeColor = System.Drawing.SystemColors.Control;
            this.RenameButton.Location = new System.Drawing.Point(330, 537);
            this.RenameButton.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.RenameButton.Name = "RenameButton";
            this.RenameButton.Size = new System.Drawing.Size(130, 50);
            this.RenameButton.TabIndex = 32;
            this.RenameButton.Text = "Set Name";
            this.RenameButton.UseVisualStyleBackColor = false;
            this.RenameButton.Click += new System.EventHandler(this.RenameButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Maroon;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(98, 499);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Size = new System.Drawing.Size(145, 28);
            this.label1.TabIndex = 31;
            this.label1.Text = "USERNAME";
            // 
            // UsernameInput
            // 
            this.UsernameInput.BackColor = System.Drawing.Color.Black;
            this.UsernameInput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.UsernameInput.Font = new System.Drawing.Font("Century Gothic", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UsernameInput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(95)))), ((int)(((byte)(60)))));
            this.UsernameInput.Location = new System.Drawing.Point(20, 537);
            this.UsernameInput.Name = "UsernameInput";
            this.UsernameInput.Size = new System.Drawing.Size(300, 50);
            this.UsernameInput.TabIndex = 30;
            this.UsernameInput.Text = "Danny";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Location = new System.Drawing.Point(0, 602);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(480, 2);
            this.panel1.TabIndex = 29;
            // 
            // DisconnectButton
            // 
            this.DisconnectButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.DisconnectButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.DisconnectButton.FlatAppearance.BorderSize = 0;
            this.DisconnectButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DisconnectButton.ForeColor = System.Drawing.SystemColors.Control;
            this.DisconnectButton.Location = new System.Drawing.Point(20, 679);
            this.DisconnectButton.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.DisconnectButton.Name = "DisconnectButton";
            this.DisconnectButton.Size = new System.Drawing.Size(440, 50);
            this.DisconnectButton.TabIndex = 28;
            this.DisconnectButton.Text = "Disconnect";
            this.DisconnectButton.UseVisualStyleBackColor = false;
            this.DisconnectButton.Click += new System.EventHandler(this.DisconnectButton_Click);
            // 
            // ConnectButton
            // 
            this.ConnectButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.ConnectButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.ConnectButton.FlatAppearance.BorderSize = 0;
            this.ConnectButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ConnectButton.ForeColor = System.Drawing.SystemColors.Control;
            this.ConnectButton.Location = new System.Drawing.Point(20, 619);
            this.ConnectButton.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(440, 50);
            this.ConnectButton.TabIndex = 3;
            this.ConnectButton.Text = "Connect";
            this.ConnectButton.UseVisualStyleBackColor = false;
            this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // LineBreak3
            // 
            this.LineBreak3.BackColor = System.Drawing.Color.White;
            this.LineBreak3.Location = new System.Drawing.Point(0, 482);
            this.LineBreak3.Name = "LineBreak3";
            this.LineBreak3.Size = new System.Drawing.Size(480, 2);
            this.LineBreak3.TabIndex = 27;
            // 
            // CurrentPortLabel
            // 
            this.CurrentPortLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.CurrentPortLabel.Font = new System.Drawing.Font("DS-Digital", 25F, System.Drawing.FontStyle.Bold);
            this.CurrentPortLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(255)))), ((int)(((byte)(140)))));
            this.CurrentPortLabel.Location = new System.Drawing.Point(330, 221);
            this.CurrentPortLabel.Name = "CurrentPortLabel";
            this.CurrentPortLabel.Size = new System.Drawing.Size(130, 50);
            this.CurrentPortLabel.TabIndex = 24;
            this.CurrentPortLabel.Text = "4444";
            this.CurrentPortLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CurrentIPLabel
            // 
            this.CurrentIPLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.CurrentIPLabel.Font = new System.Drawing.Font("DS-Digital", 25F, System.Drawing.FontStyle.Bold);
            this.CurrentIPLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(255)))), ((int)(((byte)(140)))));
            this.CurrentIPLabel.Location = new System.Drawing.Point(20, 221);
            this.CurrentIPLabel.Name = "CurrentIPLabel";
            this.CurrentIPLabel.Size = new System.Drawing.Size(300, 50);
            this.CurrentIPLabel.TabIndex = 23;
            this.CurrentIPLabel.Text = "127.0.01";
            this.CurrentIPLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LineBreak2
            // 
            this.LineBreak2.BackColor = System.Drawing.Color.White;
            this.LineBreak2.Location = new System.Drawing.Point(0, 324);
            this.LineBreak2.Name = "LineBreak2";
            this.LineBreak2.Size = new System.Drawing.Size(480, 2);
            this.LineBreak2.TabIndex = 22;
            // 
            // LastConnectionLabel
            // 
            this.LastConnectionLabel.AutoSize = true;
            this.LastConnectionLabel.BackColor = System.Drawing.Color.Black;
            this.LastConnectionLabel.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LastConnectionLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.LastConnectionLabel.Location = new System.Drawing.Point(60, 439);
            this.LastConnectionLabel.Name = "LastConnectionLabel";
            this.LastConnectionLabel.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.LastConnectionLabel.Size = new System.Drawing.Size(361, 28);
            this.LastConnectionLabel.TabIndex = 21;
            this.LastConnectionLabel.Text = "LAST CONNECTION DEPARTED";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Maroon;
            this.label5.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.Control;
            this.label5.Location = new System.Drawing.Point(360, 341);
            this.label5.Name = "label5";
            this.label5.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label5.Size = new System.Drawing.Size(79, 28);
            this.label5.TabIndex = 20;
            this.label5.Text = "PORT";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Maroon;
            this.label6.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.Control;
            this.label6.Location = new System.Drawing.Point(51, 341);
            this.label6.Name = "label6";
            this.label6.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label6.Size = new System.Drawing.Size(257, 28);
            this.label6.TabIndex = 19;
            this.label6.Text = "INTERNET PROTOCOL";
            // 
            // LineBreak1
            // 
            this.LineBreak1.BackColor = System.Drawing.Color.White;
            this.LineBreak1.Location = new System.Drawing.Point(0, 166);
            this.LineBreak1.Name = "LineBreak1";
            this.LineBreak1.Size = new System.Drawing.Size(480, 2);
            this.LineBreak1.TabIndex = 10;
            // 
            // CurrentConnectionLabel
            // 
            this.CurrentConnectionLabel.AutoSize = true;
            this.CurrentConnectionLabel.BackColor = System.Drawing.Color.Black;
            this.CurrentConnectionLabel.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CurrentConnectionLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.CurrentConnectionLabel.Location = new System.Drawing.Point(94, 281);
            this.CurrentConnectionLabel.Name = "CurrentConnectionLabel";
            this.CurrentConnectionLabel.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.CurrentConnectionLabel.Size = new System.Drawing.Size(292, 28);
            this.CurrentConnectionLabel.TabIndex = 9;
            this.CurrentConnectionLabel.Text = "CURRENT CONNECTION";
            // 
            // PortLabel2
            // 
            this.PortLabel2.AutoSize = true;
            this.PortLabel2.BackColor = System.Drawing.Color.Maroon;
            this.PortLabel2.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PortLabel2.ForeColor = System.Drawing.SystemColors.Control;
            this.PortLabel2.Location = new System.Drawing.Point(360, 183);
            this.PortLabel2.Name = "PortLabel2";
            this.PortLabel2.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.PortLabel2.Size = new System.Drawing.Size(79, 28);
            this.PortLabel2.TabIndex = 8;
            this.PortLabel2.Text = "PORT";
            // 
            // IPLabel2
            // 
            this.IPLabel2.AutoSize = true;
            this.IPLabel2.BackColor = System.Drawing.Color.Maroon;
            this.IPLabel2.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IPLabel2.ForeColor = System.Drawing.SystemColors.Control;
            this.IPLabel2.Location = new System.Drawing.Point(51, 183);
            this.IPLabel2.Name = "IPLabel2";
            this.IPLabel2.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.IPLabel2.Size = new System.Drawing.Size(257, 28);
            this.IPLabel2.TabIndex = 7;
            this.IPLabel2.Text = "INTERNET PROTOCOL";
            // 
            // ConnectionDestinationLabel
            // 
            this.ConnectionDestinationLabel.AutoSize = true;
            this.ConnectionDestinationLabel.BackColor = System.Drawing.Color.Black;
            this.ConnectionDestinationLabel.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConnectionDestinationLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.ConnectionDestinationLabel.Location = new System.Drawing.Point(70, 123);
            this.ConnectionDestinationLabel.Name = "ConnectionDestinationLabel";
            this.ConnectionDestinationLabel.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.ConnectionDestinationLabel.Size = new System.Drawing.Size(339, 28);
            this.ConnectionDestinationLabel.TabIndex = 4;
            this.ConnectionDestinationLabel.Text = "CONNECTION DESTINATION";
            // 
            // PortLabel1
            // 
            this.PortLabel1.AutoSize = true;
            this.PortLabel1.BackColor = System.Drawing.Color.Maroon;
            this.PortLabel1.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PortLabel1.ForeColor = System.Drawing.SystemColors.Control;
            this.PortLabel1.Location = new System.Drawing.Point(360, 25);
            this.PortLabel1.Name = "PortLabel1";
            this.PortLabel1.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.PortLabel1.Size = new System.Drawing.Size(79, 28);
            this.PortLabel1.TabIndex = 3;
            this.PortLabel1.Text = "PORT";
            // 
            // IPLabel1
            // 
            this.IPLabel1.AutoSize = true;
            this.IPLabel1.BackColor = System.Drawing.Color.Maroon;
            this.IPLabel1.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IPLabel1.ForeColor = System.Drawing.SystemColors.Control;
            this.IPLabel1.Location = new System.Drawing.Point(51, 25);
            this.IPLabel1.Name = "IPLabel1";
            this.IPLabel1.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.IPLabel1.Size = new System.Drawing.Size(257, 28);
            this.IPLabel1.TabIndex = 2;
            this.IPLabel1.Text = "INTERNET PROTOCOL";
            // 
            // PortInput
            // 
            this.PortInput.BackColor = System.Drawing.Color.Black;
            this.PortInput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.PortInput.Font = new System.Drawing.Font("DS-Digital", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PortInput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(95)))), ((int)(((byte)(60)))));
            this.PortInput.Location = new System.Drawing.Point(330, 63);
            this.PortInput.Name = "PortInput";
            this.PortInput.Size = new System.Drawing.Size(130, 50);
            this.PortInput.TabIndex = 1;
            this.PortInput.Text = "4444";
            // 
            // IPInput
            // 
            this.IPInput.BackColor = System.Drawing.Color.Black;
            this.IPInput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.IPInput.Font = new System.Drawing.Font("DS-Digital", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IPInput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(95)))), ((int)(((byte)(60)))));
            this.IPInput.Location = new System.Drawing.Point(20, 63);
            this.IPInput.Name = "IPInput";
            this.IPInput.Size = new System.Drawing.Size(300, 50);
            this.IPInput.TabIndex = 0;
            this.IPInput.Text = "127.0.0.1";
            // 
            // LastPortLabel
            // 
            this.LastPortLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.LastPortLabel.Font = new System.Drawing.Font("DS-Digital", 25F, System.Drawing.FontStyle.Bold);
            this.LastPortLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(210)))), ((int)(((byte)(90)))));
            this.LastPortLabel.Location = new System.Drawing.Point(330, 379);
            this.LastPortLabel.Name = "LastPortLabel";
            this.LastPortLabel.Size = new System.Drawing.Size(130, 50);
            this.LastPortLabel.TabIndex = 26;
            this.LastPortLabel.Text = "4444";
            this.LastPortLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LastIPLabel
            // 
            this.LastIPLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.LastIPLabel.Font = new System.Drawing.Font("DS-Digital", 25F, System.Drawing.FontStyle.Bold);
            this.LastIPLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(210)))), ((int)(((byte)(90)))));
            this.LastIPLabel.Location = new System.Drawing.Point(20, 379);
            this.LastIPLabel.Name = "LastIPLabel";
            this.LastIPLabel.Size = new System.Drawing.Size(300, 50);
            this.LastIPLabel.TabIndex = 25;
            this.LastIPLabel.Text = "127.0.01";
            this.LastIPLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ChatWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
            this.ClientSize = new System.Drawing.Size(1920, 1080);
            this.Controls.Add(this.ConnectionControlPanel);
            this.Controls.Add(this.ChatInputPanel);
            this.Controls.Add(this.ServerLog);
            this.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.Name = "ChatWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ChatEngine V0.2";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ChatWindow_FormClosed);
            this.Load += new System.EventHandler(this.ChatWindow_Load);
            this.ChatInputPanel.ResumeLayout(false);
            this.ConnectionControlPanel.ResumeLayout(false);
            this.ConnectionControlPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox ServerLog;
        private System.Windows.Forms.RichTextBox InputBox;
        private System.Windows.Forms.Button Submit;
        private System.Windows.Forms.Panel ChatInputPanel;
        private System.Windows.Forms.Panel ConnectionControlPanel;
        private System.Windows.Forms.Label ConnectionDestinationLabel;
        private System.Windows.Forms.Label PortLabel1;
        private System.Windows.Forms.Label IPLabel1;
        private System.Windows.Forms.RichTextBox PortInput;
        private System.Windows.Forms.RichTextBox IPInput;
        private System.Windows.Forms.Panel LineBreak1;
        private System.Windows.Forms.Label CurrentConnectionLabel;
        private System.Windows.Forms.Label PortLabel2;
        private System.Windows.Forms.Label IPLabel2;
        private System.Windows.Forms.Panel LineBreak2;
        private System.Windows.Forms.Label LastConnectionLabel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label CurrentPortLabel;
        private System.Windows.Forms.Label CurrentIPLabel;
        private System.Windows.Forms.Label LastPortLabel;
        private System.Windows.Forms.Label LastIPLabel;
        private System.Windows.Forms.Panel LineBreak3;
        private System.Windows.Forms.Button ConnectButton;
        private System.Windows.Forms.Button DisconnectButton;
        private System.Windows.Forms.Button RenameButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox UsernameInput;
        private System.Windows.Forms.Panel panel1;
    }
}

