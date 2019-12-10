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
            this.MainWindow = new Client.MonoGameWindow(packet, this.client);
            this.SuspendLayout();
            // 
            // monoGameWindow1
            // 
            this.MainWindow.Location = new System.Drawing.Point(0, 0);
            this.MainWindow.MouseHoverUpdatesOnly = false;
            this.MainWindow.Name = "monoGameWindow1";
            this.MainWindow.Size = new System.Drawing.Size(1440, 810);
            this.MainWindow.TabIndex = 0;
            this.MainWindow.Text = "monoGameWindow1";
            // 
            // GameWindow
            // 
            this.ClientSize = new System.Drawing.Size(1440, 810);
            this.Controls.Add(this.MainWindow);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "GameWindow";
            this.ResumeLayout(false);

        }

        #endregion

        private MonoGameWindow MainWindow;
    }
}