namespace Server
{
    partial class ServerWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServerWindow));
            this.ChatInputPanel = new System.Windows.Forms.Panel();
            this.InputBox = new System.Windows.Forms.RichTextBox();
            this.Submit = new System.Windows.Forms.Button();
            this.ConnectionControlPanel = new System.Windows.Forms.Panel();
            this.RenameButton = new System.Windows.Forms.Button();
            this.ServerNameLabel = new System.Windows.Forms.Label();
            this.ServerNameInput = new System.Windows.Forms.RichTextBox();
            this.LineBreak4 = new System.Windows.Forms.Panel();
            this.StopButton = new System.Windows.Forms.Button();
            this.ListenButton = new System.Windows.Forms.Button();
            this.LineBreak3 = new System.Windows.Forms.Panel();
            this.CurrentPortLabel = new System.Windows.Forms.Label();
            this.CurrentIPLabel = new System.Windows.Forms.Label();
            this.LineBreak2 = new System.Windows.Forms.Panel();
            this.LastConnectionLabel = new System.Windows.Forms.Label();
            this.PortLabel3 = new System.Windows.Forms.Label();
            this.IPLabel3 = new System.Windows.Forms.Label();
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.ServerLog = new System.Windows.Forms.RichTextBox();
            this.ChatInputPanel.SuspendLayout();
            this.ConnectionControlPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ChatInputPanel
            // 
            this.ChatInputPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.ChatInputPanel.Controls.Add(this.InputBox);
            this.ChatInputPanel.Controls.Add(this.Submit);
            this.ChatInputPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ChatInputPanel.Location = new System.Drawing.Point(0, 621);
            this.ChatInputPanel.Margin = new System.Windows.Forms.Padding(2);
            this.ChatInputPanel.Name = "ChatInputPanel";
            this.ChatInputPanel.Size = new System.Drawing.Size(1284, 90);
            this.ChatInputPanel.TabIndex = 6;
            // 
            // InputBox
            // 
            this.InputBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.InputBox.Font = new System.Drawing.Font("Century Gothic", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InputBox.Location = new System.Drawing.Point(10, 10);
            this.InputBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.InputBox.Name = "InputBox";
            this.InputBox.Size = new System.Drawing.Size(1095, 70);
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
            this.Submit.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.Submit.ForeColor = System.Drawing.SystemColors.Control;
            this.Submit.Location = new System.Drawing.Point(1115, 10);
            this.Submit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Submit.Name = "Submit";
            this.Submit.Size = new System.Drawing.Size(159, 70);
            this.Submit.TabIndex = 0;
            this.Submit.Text = "Submit";
            this.Submit.UseVisualStyleBackColor = false;
            this.Submit.Click += new System.EventHandler(this.Submit_Click);
            // 
            // ConnectionControlPanel
            // 
            this.ConnectionControlPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.ConnectionControlPanel.Controls.Add(this.RenameButton);
            this.ConnectionControlPanel.Controls.Add(this.ServerNameLabel);
            this.ConnectionControlPanel.Controls.Add(this.ServerNameInput);
            this.ConnectionControlPanel.Controls.Add(this.LineBreak4);
            this.ConnectionControlPanel.Controls.Add(this.StopButton);
            this.ConnectionControlPanel.Controls.Add(this.ListenButton);
            this.ConnectionControlPanel.Controls.Add(this.LineBreak3);
            this.ConnectionControlPanel.Controls.Add(this.CurrentPortLabel);
            this.ConnectionControlPanel.Controls.Add(this.CurrentIPLabel);
            this.ConnectionControlPanel.Controls.Add(this.LineBreak2);
            this.ConnectionControlPanel.Controls.Add(this.LastConnectionLabel);
            this.ConnectionControlPanel.Controls.Add(this.PortLabel3);
            this.ConnectionControlPanel.Controls.Add(this.IPLabel3);
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
            this.ConnectionControlPanel.Location = new System.Drawing.Point(964, 0);
            this.ConnectionControlPanel.Margin = new System.Windows.Forms.Padding(2);
            this.ConnectionControlPanel.Name = "ConnectionControlPanel";
            this.ConnectionControlPanel.Size = new System.Drawing.Size(320, 621);
            this.ConnectionControlPanel.TabIndex = 8;
            // 
            // RenameButton
            // 
            this.RenameButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.RenameButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.RenameButton.FlatAppearance.BorderSize = 0;
            this.RenameButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RenameButton.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.RenameButton.ForeColor = System.Drawing.SystemColors.Control;
            this.RenameButton.Location = new System.Drawing.Point(220, 346);
            this.RenameButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.RenameButton.Name = "RenameButton";
            this.RenameButton.Size = new System.Drawing.Size(90, 35);
            this.RenameButton.TabIndex = 32;
            this.RenameButton.Text = "Set Name";
            this.RenameButton.UseVisualStyleBackColor = false;
            this.RenameButton.Click += new System.EventHandler(this.RenameButton_Click);
            // 
            // ServerNameLabel
            // 
            this.ServerNameLabel.AutoSize = true;
            this.ServerNameLabel.BackColor = System.Drawing.Color.Maroon;
            this.ServerNameLabel.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ServerNameLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.ServerNameLabel.Location = new System.Drawing.Point(62, 322);
            this.ServerNameLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.ServerNameLabel.Name = "ServerNameLabel";
            this.ServerNameLabel.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.ServerNameLabel.Size = new System.Drawing.Size(118, 19);
            this.ServerNameLabel.TabIndex = 31;
            this.ServerNameLabel.Text = "SERVER NAME";
            // 
            // ServerNameInput
            // 
            this.ServerNameInput.BackColor = System.Drawing.Color.Black;
            this.ServerNameInput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ServerNameInput.Font = new System.Drawing.Font("Century Gothic", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ServerNameInput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(95)))), ((int)(((byte)(60)))));
            this.ServerNameInput.Location = new System.Drawing.Point(13, 346);
            this.ServerNameInput.Margin = new System.Windows.Forms.Padding(2);
            this.ServerNameInput.Name = "ServerNameInput";
            this.ServerNameInput.Size = new System.Drawing.Size(200, 35);
            this.ServerNameInput.TabIndex = 30;
            this.ServerNameInput.Text = "General";
            // 
            // LineBreak4
            // 
            this.LineBreak4.BackColor = System.Drawing.Color.White;
            this.LineBreak4.Location = new System.Drawing.Point(0, 515);
            this.LineBreak4.Margin = new System.Windows.Forms.Padding(2);
            this.LineBreak4.Name = "LineBreak4";
            this.LineBreak4.Size = new System.Drawing.Size(320, 1);
            this.LineBreak4.TabIndex = 29;
            // 
            // StopButton
            // 
            this.StopButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.StopButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.StopButton.FlatAppearance.BorderSize = 0;
            this.StopButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.StopButton.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.StopButton.ForeColor = System.Drawing.SystemColors.Control;
            this.StopButton.Location = new System.Drawing.Point(10, 572);
            this.StopButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(300, 35);
            this.StopButton.TabIndex = 28;
            this.StopButton.Text = "Stop";
            this.StopButton.UseVisualStyleBackColor = false;
            this.StopButton.Click += new System.EventHandler(this.DisconnectButton_Click);
            // 
            // ListenButton
            // 
            this.ListenButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.ListenButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.ListenButton.FlatAppearance.BorderSize = 0;
            this.ListenButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ListenButton.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.ListenButton.ForeColor = System.Drawing.SystemColors.Control;
            this.ListenButton.Location = new System.Drawing.Point(10, 527);
            this.ListenButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ListenButton.Name = "ListenButton";
            this.ListenButton.Size = new System.Drawing.Size(300, 35);
            this.ListenButton.TabIndex = 3;
            this.ListenButton.Text = "Start";
            this.ListenButton.UseVisualStyleBackColor = false;
            this.ListenButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // LineBreak3
            // 
            this.LineBreak3.BackColor = System.Drawing.Color.White;
            this.LineBreak3.Location = new System.Drawing.Point(0, 311);
            this.LineBreak3.Margin = new System.Windows.Forms.Padding(2);
            this.LineBreak3.Name = "LineBreak3";
            this.LineBreak3.Size = new System.Drawing.Size(320, 1);
            this.LineBreak3.TabIndex = 27;
            // 
            // CurrentPortLabel
            // 
            this.CurrentPortLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.CurrentPortLabel.Font = new System.Drawing.Font("DS-Digital", 25F, System.Drawing.FontStyle.Bold);
            this.CurrentPortLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(255)))), ((int)(((byte)(140)))));
            this.CurrentPortLabel.Location = new System.Drawing.Point(220, 138);
            this.CurrentPortLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.CurrentPortLabel.Name = "CurrentPortLabel";
            this.CurrentPortLabel.Size = new System.Drawing.Size(90, 35);
            this.CurrentPortLabel.TabIndex = 24;
            this.CurrentPortLabel.Text = "N/A";
            this.CurrentPortLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CurrentIPLabel
            // 
            this.CurrentIPLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.CurrentIPLabel.Font = new System.Drawing.Font("DS-Digital", 25F, System.Drawing.FontStyle.Bold);
            this.CurrentIPLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(255)))), ((int)(((byte)(140)))));
            this.CurrentIPLabel.Location = new System.Drawing.Point(10, 138);
            this.CurrentIPLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.CurrentIPLabel.Name = "CurrentIPLabel";
            this.CurrentIPLabel.Size = new System.Drawing.Size(200, 35);
            this.CurrentIPLabel.TabIndex = 23;
            this.CurrentIPLabel.Text = "N/A";
            this.CurrentIPLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LineBreak2
            // 
            this.LineBreak2.BackColor = System.Drawing.Color.White;
            this.LineBreak2.Location = new System.Drawing.Point(0, 207);
            this.LineBreak2.Margin = new System.Windows.Forms.Padding(2);
            this.LineBreak2.Name = "LineBreak2";
            this.LineBreak2.Size = new System.Drawing.Size(320, 1);
            this.LineBreak2.TabIndex = 22;
            // 
            // LastConnectionLabel
            // 
            this.LastConnectionLabel.AutoSize = true;
            this.LastConnectionLabel.BackColor = System.Drawing.Color.Black;
            this.LastConnectionLabel.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LastConnectionLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.LastConnectionLabel.Location = new System.Drawing.Point(40, 282);
            this.LastConnectionLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LastConnectionLabel.Name = "LastConnectionLabel";
            this.LastConnectionLabel.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.LastConnectionLabel.Size = new System.Drawing.Size(237, 19);
            this.LastConnectionLabel.TabIndex = 21;
            this.LastConnectionLabel.Text = "LAST CONNECTION DEPARTED";
            // 
            // PortLabel3
            // 
            this.PortLabel3.AutoSize = true;
            this.PortLabel3.BackColor = System.Drawing.Color.Maroon;
            this.PortLabel3.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PortLabel3.ForeColor = System.Drawing.SystemColors.Control;
            this.PortLabel3.Location = new System.Drawing.Point(240, 218);
            this.PortLabel3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.PortLabel3.Name = "PortLabel3";
            this.PortLabel3.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.PortLabel3.Size = new System.Drawing.Size(53, 19);
            this.PortLabel3.TabIndex = 20;
            this.PortLabel3.Text = "PORT";
            // 
            // IPLabel3
            // 
            this.IPLabel3.AutoSize = true;
            this.IPLabel3.BackColor = System.Drawing.Color.Maroon;
            this.IPLabel3.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IPLabel3.ForeColor = System.Drawing.SystemColors.Control;
            this.IPLabel3.Location = new System.Drawing.Point(26, 218);
            this.IPLabel3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.IPLabel3.Name = "IPLabel3";
            this.IPLabel3.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.IPLabel3.Size = new System.Drawing.Size(169, 19);
            this.IPLabel3.TabIndex = 19;
            this.IPLabel3.Text = "INTERNET PROTOCOL";
            // 
            // LineBreak1
            // 
            this.LineBreak1.BackColor = System.Drawing.Color.White;
            this.LineBreak1.Location = new System.Drawing.Point(0, 103);
            this.LineBreak1.Margin = new System.Windows.Forms.Padding(2);
            this.LineBreak1.Name = "LineBreak1";
            this.LineBreak1.Size = new System.Drawing.Size(320, 1);
            this.LineBreak1.TabIndex = 10;
            // 
            // CurrentConnectionLabel
            // 
            this.CurrentConnectionLabel.AutoSize = true;
            this.CurrentConnectionLabel.BackColor = System.Drawing.Color.Black;
            this.CurrentConnectionLabel.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CurrentConnectionLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.CurrentConnectionLabel.Location = new System.Drawing.Point(63, 178);
            this.CurrentConnectionLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.CurrentConnectionLabel.Name = "CurrentConnectionLabel";
            this.CurrentConnectionLabel.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.CurrentConnectionLabel.Size = new System.Drawing.Size(191, 19);
            this.CurrentConnectionLabel.TabIndex = 9;
            this.CurrentConnectionLabel.Text = "CURRENT CONNECTION";
            // 
            // PortLabel2
            // 
            this.PortLabel2.AutoSize = true;
            this.PortLabel2.BackColor = System.Drawing.Color.Maroon;
            this.PortLabel2.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PortLabel2.ForeColor = System.Drawing.SystemColors.Control;
            this.PortLabel2.Location = new System.Drawing.Point(240, 114);
            this.PortLabel2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.PortLabel2.Name = "PortLabel2";
            this.PortLabel2.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.PortLabel2.Size = new System.Drawing.Size(53, 19);
            this.PortLabel2.TabIndex = 8;
            this.PortLabel2.Text = "PORT";
            // 
            // IPLabel2
            // 
            this.IPLabel2.AutoSize = true;
            this.IPLabel2.BackColor = System.Drawing.Color.Maroon;
            this.IPLabel2.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IPLabel2.ForeColor = System.Drawing.SystemColors.Control;
            this.IPLabel2.Location = new System.Drawing.Point(26, 114);
            this.IPLabel2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.IPLabel2.Name = "IPLabel2";
            this.IPLabel2.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.IPLabel2.Size = new System.Drawing.Size(169, 19);
            this.IPLabel2.TabIndex = 7;
            this.IPLabel2.Text = "INTERNET PROTOCOL";
            // 
            // ConnectionDestinationLabel
            // 
            this.ConnectionDestinationLabel.AutoSize = true;
            this.ConnectionDestinationLabel.BackColor = System.Drawing.Color.Black;
            this.ConnectionDestinationLabel.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConnectionDestinationLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.ConnectionDestinationLabel.Location = new System.Drawing.Point(49, 74);
            this.ConnectionDestinationLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.ConnectionDestinationLabel.Name = "ConnectionDestinationLabel";
            this.ConnectionDestinationLabel.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.ConnectionDestinationLabel.Size = new System.Drawing.Size(222, 19);
            this.ConnectionDestinationLabel.TabIndex = 4;
            this.ConnectionDestinationLabel.Text = "CONNECTION DESTINATION";
            // 
            // PortLabel1
            // 
            this.PortLabel1.AutoSize = true;
            this.PortLabel1.BackColor = System.Drawing.Color.Maroon;
            this.PortLabel1.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PortLabel1.ForeColor = System.Drawing.SystemColors.Control;
            this.PortLabel1.Location = new System.Drawing.Point(240, 10);
            this.PortLabel1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.PortLabel1.Name = "PortLabel1";
            this.PortLabel1.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.PortLabel1.Size = new System.Drawing.Size(53, 19);
            this.PortLabel1.TabIndex = 3;
            this.PortLabel1.Text = "PORT";
            // 
            // IPLabel1
            // 
            this.IPLabel1.AutoSize = true;
            this.IPLabel1.BackColor = System.Drawing.Color.Maroon;
            this.IPLabel1.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IPLabel1.ForeColor = System.Drawing.SystemColors.Control;
            this.IPLabel1.Location = new System.Drawing.Point(26, 10);
            this.IPLabel1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.IPLabel1.Name = "IPLabel1";
            this.IPLabel1.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.IPLabel1.Size = new System.Drawing.Size(169, 19);
            this.IPLabel1.TabIndex = 2;
            this.IPLabel1.Text = "INTERNET PROTOCOL";
            // 
            // PortInput
            // 
            this.PortInput.BackColor = System.Drawing.Color.Black;
            this.PortInput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.PortInput.Font = new System.Drawing.Font("DS-Digital", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PortInput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(95)))), ((int)(((byte)(60)))));
            this.PortInput.Location = new System.Drawing.Point(220, 34);
            this.PortInput.Margin = new System.Windows.Forms.Padding(2);
            this.PortInput.Name = "PortInput";
            this.PortInput.Size = new System.Drawing.Size(90, 35);
            this.PortInput.TabIndex = 1;
            this.PortInput.Text = "4444";
            // 
            // IPInput
            // 
            this.IPInput.BackColor = System.Drawing.Color.Black;
            this.IPInput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.IPInput.Font = new System.Drawing.Font("DS-Digital", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IPInput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(95)))), ((int)(((byte)(60)))));
            this.IPInput.Location = new System.Drawing.Point(10, 34);
            this.IPInput.Margin = new System.Windows.Forms.Padding(2);
            this.IPInput.Name = "IPInput";
            this.IPInput.Size = new System.Drawing.Size(200, 35);
            this.IPInput.TabIndex = 0;
            this.IPInput.Text = "127.0.0.1";
            // 
            // LastPortLabel
            // 
            this.LastPortLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.LastPortLabel.Font = new System.Drawing.Font("DS-Digital", 25F, System.Drawing.FontStyle.Bold);
            this.LastPortLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(210)))), ((int)(((byte)(90)))));
            this.LastPortLabel.Location = new System.Drawing.Point(220, 242);
            this.LastPortLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LastPortLabel.Name = "LastPortLabel";
            this.LastPortLabel.Size = new System.Drawing.Size(90, 35);
            this.LastPortLabel.TabIndex = 26;
            this.LastPortLabel.Text = "N/A";
            this.LastPortLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LastIPLabel
            // 
            this.LastIPLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.LastIPLabel.Font = new System.Drawing.Font("DS-Digital", 25F, System.Drawing.FontStyle.Bold);
            this.LastIPLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(210)))), ((int)(((byte)(90)))));
            this.LastIPLabel.Location = new System.Drawing.Point(10, 242);
            this.LastIPLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LastIPLabel.Name = "LastIPLabel";
            this.LastIPLabel.Size = new System.Drawing.Size(200, 35);
            this.LastIPLabel.TabIndex = 25;
            this.LastIPLabel.Text = "N/A";
            this.LastIPLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ServerLog);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(965, 621);
            this.panel1.TabIndex = 9;
            // 
            // ServerLog
            // 
            this.ServerLog.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ServerLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ServerLog.Font = new System.Drawing.Font("Century Gothic", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ServerLog.ForeColor = System.Drawing.SystemColors.WindowText;
            this.ServerLog.Location = new System.Drawing.Point(10, 10);
            this.ServerLog.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ServerLog.Name = "ServerLog";
            this.ServerLog.ReadOnly = true;
            this.ServerLog.Size = new System.Drawing.Size(945, 601);
            this.ServerLog.TabIndex = 6;
            this.ServerLog.Text = "";
            // 
            // ServerWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
            this.ClientSize = new System.Drawing.Size(1284, 711);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ConnectionControlPanel);
            this.Controls.Add(this.ChatInputPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ServerWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ServerEngine V0.2.1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ServerWindow_FormClosed);
            this.Load += new System.EventHandler(this.ServerWindow_Load);
            this.ChatInputPanel.ResumeLayout(false);
            this.ConnectionControlPanel.ResumeLayout(false);
            this.ConnectionControlPanel.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel ChatInputPanel;
        private System.Windows.Forms.RichTextBox InputBox;
        private System.Windows.Forms.Button Submit;
        private System.Windows.Forms.Panel ConnectionControlPanel;
        private System.Windows.Forms.Button RenameButton;
        private System.Windows.Forms.Label ServerNameLabel;
        private System.Windows.Forms.RichTextBox ServerNameInput;
        private System.Windows.Forms.Panel LineBreak4;
        private System.Windows.Forms.Button StopButton;
        private System.Windows.Forms.Button ListenButton;
        private System.Windows.Forms.Panel LineBreak3;
        private System.Windows.Forms.Label CurrentPortLabel;
        private System.Windows.Forms.Label CurrentIPLabel;
        private System.Windows.Forms.Panel LineBreak2;
        private System.Windows.Forms.Label LastConnectionLabel;
        private System.Windows.Forms.Label PortLabel3;
        private System.Windows.Forms.Label IPLabel3;
        private System.Windows.Forms.Panel LineBreak1;
        private System.Windows.Forms.Label CurrentConnectionLabel;
        private System.Windows.Forms.Label PortLabel2;
        private System.Windows.Forms.Label IPLabel2;
        private System.Windows.Forms.Label ConnectionDestinationLabel;
        private System.Windows.Forms.Label PortLabel1;
        private System.Windows.Forms.Label IPLabel1;
        private System.Windows.Forms.RichTextBox PortInput;
        private System.Windows.Forms.RichTextBox IPInput;
        private System.Windows.Forms.Label LastPortLabel;
        private System.Windows.Forms.Label LastIPLabel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RichTextBox ServerLog;
    }
}

