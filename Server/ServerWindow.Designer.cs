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
            this.Submit = new System.Windows.Forms.Button();
            this.ServerLog = new System.Windows.Forms.RichTextBox();
            this.InputBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // Submit
            // 
            this.Submit.Location = new System.Drawing.Point(684, 389);
            this.Submit.Name = "Submit";
            this.Submit.Size = new System.Drawing.Size(104, 49);
            this.Submit.TabIndex = 0;
            this.Submit.Text = "Submit";
            this.Submit.UseVisualStyleBackColor = true;
            this.Submit.Click += new System.EventHandler(this.Submit_Click);
            // 
            // ServerLog
            // 
            this.ServerLog.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ServerLog.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ServerLog.ForeColor = System.Drawing.SystemColors.WindowText;
            this.ServerLog.Location = new System.Drawing.Point(12, 12);
            this.ServerLog.Name = "ServerLog";
            this.ServerLog.ReadOnly = true;
            this.ServerLog.Size = new System.Drawing.Size(776, 359);
            this.ServerLog.TabIndex = 1;
            this.ServerLog.Text = "";
            this.ServerLog.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.ServerLog_LinkClicked);
            // 
            // InputBox
            // 
            this.InputBox.Location = new System.Drawing.Point(12, 389);
            this.InputBox.Name = "InputBox";
            this.InputBox.Size = new System.Drawing.Size(666, 49);
            this.InputBox.TabIndex = 2;
            this.InputBox.Text = "";
            this.InputBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.InputBox_KeyDown);
            this.InputBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.InputBox_KeyUp);
            // 
            // ServerWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.InputBox);
            this.Controls.Add(this.ServerLog);
            this.Controls.Add(this.Submit);
            this.Name = "ServerWindow";
            this.Text = "ServerEngine V0.1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ServerWindow_FormClosed);
            this.Load += new System.EventHandler(this.ServerWindow_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Submit;
        private System.Windows.Forms.RichTextBox ServerLog;
        private System.Windows.Forms.RichTextBox InputBox;
    }
}

