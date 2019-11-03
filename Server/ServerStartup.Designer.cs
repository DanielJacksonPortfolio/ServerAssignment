namespace Server
{
    partial class ServerStartup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServerStartup));
            this.PortLabel = new System.Windows.Forms.Label();
            this.PortInput = new System.Windows.Forms.RichTextBox();
            this.ServerNameLabel = new System.Windows.Forms.Label();
            this.StartButton = new System.Windows.Forms.Button();
            this.ServerSettingsHeaderLabel = new System.Windows.Forms.Label();
            this.IPLabel = new System.Windows.Forms.Label();
            this.IPInput = new System.Windows.Forms.RichTextBox();
            this.UsernameInput = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // PortLabel
            // 
            this.PortLabel.AutoSize = true;
            this.PortLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.PortLabel.Location = new System.Drawing.Point(12, 132);
            this.PortLabel.Name = "PortLabel";
            this.PortLabel.Size = new System.Drawing.Size(103, 40);
            this.PortLabel.TabIndex = 17;
            this.PortLabel.Text = "Port: ";
            // 
            // PortInput
            // 
            this.PortInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.PortInput.Location = new System.Drawing.Point(267, 132);
            this.PortInput.Multiline = false;
            this.PortInput.Name = "PortInput";
            this.PortInput.Size = new System.Drawing.Size(732, 46);
            this.PortInput.TabIndex = 16;
            this.PortInput.Text = "4444";
            // 
            // ServerNameLabel
            // 
            this.ServerNameLabel.AutoSize = true;
            this.ServerNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.ServerNameLabel.Location = new System.Drawing.Point(10, 189);
            this.ServerNameLabel.Name = "ServerNameLabel";
            this.ServerNameLabel.Size = new System.Drawing.Size(249, 40);
            this.ServerNameLabel.TabIndex = 15;
            this.ServerNameLabel.Text = "Server Name: ";
            // 
            // StartButton
            // 
            this.StartButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.StartButton.Location = new System.Drawing.Point(1022, 189);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(138, 48);
            this.StartButton.TabIndex = 14;
            this.StartButton.Text = "Start";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // ServerSettingsHeaderLabel
            // 
            this.ServerSettingsHeaderLabel.AutoSize = true;
            this.ServerSettingsHeaderLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.ServerSettingsHeaderLabel.Location = new System.Drawing.Point(12, 12);
            this.ServerSettingsHeaderLabel.Name = "ServerSettingsHeaderLabel";
            this.ServerSettingsHeaderLabel.Size = new System.Drawing.Size(263, 40);
            this.ServerSettingsHeaderLabel.TabIndex = 12;
            this.ServerSettingsHeaderLabel.Text = "Server Settings";
            // 
            // IPLabel
            // 
            this.IPLabel.AutoSize = true;
            this.IPLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.IPLabel.Location = new System.Drawing.Point(10, 69);
            this.IPLabel.Name = "IPLabel";
            this.IPLabel.Size = new System.Drawing.Size(212, 40);
            this.IPLabel.TabIndex = 11;
            this.IPLabel.Text = "IP Address: ";
            // 
            // IPInput
            // 
            this.IPInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.IPInput.Location = new System.Drawing.Point(267, 69);
            this.IPInput.Multiline = false;
            this.IPInput.Name = "IPInput";
            this.IPInput.Size = new System.Drawing.Size(732, 46);
            this.IPInput.TabIndex = 10;
            this.IPInput.Text = "127.0.0.1";
            // 
            // UsernameInput
            // 
            this.UsernameInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.UsernameInput.Location = new System.Drawing.Point(267, 189);
            this.UsernameInput.Multiline = false;
            this.UsernameInput.Name = "UsernameInput";
            this.UsernameInput.Size = new System.Drawing.Size(732, 46);
            this.UsernameInput.TabIndex = 13;
            this.UsernameInput.Text = "General";
            // 
            // ServerStartup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1178, 251);
            this.Controls.Add(this.PortLabel);
            this.Controls.Add(this.PortInput);
            this.Controls.Add(this.ServerNameLabel);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.UsernameInput);
            this.Controls.Add(this.ServerSettingsHeaderLabel);
            this.Controls.Add(this.IPLabel);
            this.Controls.Add(this.IPInput);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ServerStartup";
            this.Text = "Server Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label PortLabel;
        private System.Windows.Forms.RichTextBox PortInput;
        private System.Windows.Forms.Label ServerNameLabel;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.Label ServerSettingsHeaderLabel;
        private System.Windows.Forms.Label IPLabel;
        private System.Windows.Forms.RichTextBox IPInput;
        private System.Windows.Forms.RichTextBox UsernameInput;
    }
}