namespace Game
{
    partial class GameForm
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
            this.monoGameWindow1 = new Game.MonoGameWindow();
            this.SuspendLayout();
            // 
            // monoGameWindow1
            // 
            this.monoGameWindow1.ForeColor = System.Drawing.Color.Turquoise;
            this.monoGameWindow1.Location = new System.Drawing.Point(0, 0);
            this.monoGameWindow1.MouseHoverUpdatesOnly = false;
            this.monoGameWindow1.Name = "monoGameWindow1";
            this.monoGameWindow1.Size = new System.Drawing.Size(1440, 810);
            this.monoGameWindow1.TabIndex = 0;
            this.monoGameWindow1.Text = "monoGameWindow1";
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1440, 810);
            this.Controls.Add(this.monoGameWindow1);
            this.Cursor = System.Windows.Forms.Cursors.Cross;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "GameForm";
            this.Text = "GameForm";
            this.ResumeLayout(false);

        }

        #endregion

        private MonoGameWindow monoGameWindow1;
    }
}