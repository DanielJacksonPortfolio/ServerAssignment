namespace Client
{
    partial class NewUserSetup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewUserSetup));
            this.IPInput = new System.Windows.Forms.RichTextBox();
            this.IPLabel = new System.Windows.Forms.Label();
            this.NewUserHeaderLabel = new System.Windows.Forms.Label();
            this.UsernameLabel = new System.Windows.Forms.Label();
            this.ConnectButton = new System.Windows.Forms.Button();
            this.UsernameInput = new System.Windows.Forms.RichTextBox();
            this.PortLabel = new System.Windows.Forms.Label();
            this.PortInput = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // IPInput
            // 
            this.IPInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.IPInput.Location = new System.Drawing.Point(161, 43);
            this.IPInput.Margin = new System.Windows.Forms.Padding(2);
            this.IPInput.Multiline = false;
            this.IPInput.Name = "IPInput";
            this.IPInput.Size = new System.Drawing.Size(489, 31);
            this.IPInput.TabIndex = 0;
            this.IPInput.Text = "127.0.0.1";
            // 
            // IPLabel
            // 
            this.IPLabel.AutoSize = true;
            this.IPLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.IPLabel.Location = new System.Drawing.Point(7, 43);
            this.IPLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.IPLabel.Name = "IPLabel";
            this.IPLabel.Size = new System.Drawing.Size(142, 29);
            this.IPLabel.TabIndex = 2;
            this.IPLabel.Text = "IP Address: ";
            // 
            // NewUserHeaderLabel
            // 
            this.NewUserHeaderLabel.AutoSize = true;
            this.NewUserHeaderLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.NewUserHeaderLabel.Location = new System.Drawing.Point(8, 6);
            this.NewUserHeaderLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.NewUserHeaderLabel.Name = "NewUserHeaderLabel";
            this.NewUserHeaderLabel.Size = new System.Drawing.Size(121, 29);
            this.NewUserHeaderLabel.TabIndex = 3;
            this.NewUserHeaderLabel.Text = "Welcome!";
            // 
            // UsernameLabel
            // 
            this.UsernameLabel.AutoSize = true;
            this.UsernameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.UsernameLabel.Location = new System.Drawing.Point(7, 120);
            this.UsernameLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.UsernameLabel.Name = "UsernameLabel";
            this.UsernameLabel.Size = new System.Drawing.Size(136, 29);
            this.UsernameLabel.TabIndex = 6;
            this.UsernameLabel.Text = "Username: ";
            // 
            // ConnectButton
            // 
            this.ConnectButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.ConnectButton.Location = new System.Drawing.Point(664, 120);
            this.ConnectButton.Margin = new System.Windows.Forms.Padding(2);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(92, 31);
            this.ConnectButton.TabIndex = 5;
            this.ConnectButton.Text = "Connect";
            this.ConnectButton.UseVisualStyleBackColor = true;
            this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // UsernameInput
            // 
            this.UsernameInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.UsernameInput.Location = new System.Drawing.Point(161, 120);
            this.UsernameInput.Margin = new System.Windows.Forms.Padding(2);
            this.UsernameInput.Multiline = false;
            this.UsernameInput.Name = "UsernameInput";
            this.UsernameInput.Size = new System.Drawing.Size(489, 31);
            this.UsernameInput.TabIndex = 4;
            this.UsernameInput.Text = "Danny";
            // 
            // PortLabel
            // 
            this.PortLabel.AutoSize = true;
            this.PortLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.PortLabel.Location = new System.Drawing.Point(8, 83);
            this.PortLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.PortLabel.Name = "PortLabel";
            this.PortLabel.Size = new System.Drawing.Size(69, 29);
            this.PortLabel.TabIndex = 9;
            this.PortLabel.Text = "Port: ";
            // 
            // PortInput
            // 
            this.PortInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.PortInput.Location = new System.Drawing.Point(161, 83);
            this.PortInput.Margin = new System.Windows.Forms.Padding(2);
            this.PortInput.Multiline = false;
            this.PortInput.Name = "PortInput";
            this.PortInput.Size = new System.Drawing.Size(489, 31);
            this.PortInput.TabIndex = 7;
            this.PortInput.Text = "4440";
            // 
            // NewUserSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(773, 168);
            this.Controls.Add(this.PortLabel);
            this.Controls.Add(this.PortInput);
            this.Controls.Add(this.UsernameLabel);
            this.Controls.Add(this.ConnectButton);
            this.Controls.Add(this.UsernameInput);
            this.Controls.Add(this.NewUserHeaderLabel);
            this.Controls.Add(this.IPLabel);
            this.Controls.Add(this.IPInput);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "NewUserSetup";
            this.Text = "Connect To Server";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox IPInput;
        private System.Windows.Forms.Label IPLabel;
        private System.Windows.Forms.Label NewUserHeaderLabel;
        private System.Windows.Forms.Label UsernameLabel;
        private System.Windows.Forms.Button ConnectButton;
        private System.Windows.Forms.RichTextBox UsernameInput;
        private System.Windows.Forms.Label PortLabel;
        private System.Windows.Forms.RichTextBox PortInput;
    }
}