using PacketData;

namespace Client
{
    partial class GameWindow
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
        private void InitializeComponent(InitGamePacket packet)
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameWindow));
            this.MainWindow = new Client.MonoGameWindow(packet);
            this.SuspendLayout();
            // 
            // MainWindow
            // 
            this.MainWindow.Location = new System.Drawing.Point(0, 0);
            this.MainWindow.MouseHoverUpdatesOnly = false;
            this.MainWindow.Name = "MainWindow";
            this.MainWindow.Size = new System.Drawing.Size(2160, 1246);
            this.MainWindow.TabIndex = 0;
            this.MainWindow.Text = "Main Window";
            // 
            // GameWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2160, 1244);
            this.Controls.Add(this.MainWindow);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GameWindow";
            this.Text = "GameWindow";
            this.ResumeLayout(false);

        }

        #endregion

        private MonoGameWindow MainWindow;
    }
}