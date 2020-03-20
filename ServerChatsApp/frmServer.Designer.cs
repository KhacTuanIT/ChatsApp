namespace ServerChatsApp
{
    partial class frmServer
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
            this.txtPort = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.btnStart = new MaterialSkin.Controls.MaterialFlatButton();
            this.lblState = new MaterialSkin.Controls.MaterialLabel();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.txtIPServer = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.SuspendLayout();
            // 
            // txtPort
            // 
            this.txtPort.Depth = 0;
            this.txtPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPort.Hint = "Port";
            this.txtPort.Location = new System.Drawing.Point(275, 12);
            this.txtPort.MouseState = MaterialSkin.MouseState.HOVER;
            this.txtPort.Name = "txtPort";
            this.txtPort.PasswordChar = '\0';
            this.txtPort.SelectedText = "";
            this.txtPort.SelectionLength = 0;
            this.txtPort.SelectionStart = 0;
            this.txtPort.Size = new System.Drawing.Size(197, 28);
            this.txtPort.TabIndex = 0;
            this.txtPort.UseSystemPasswordChar = false;
            this.txtPort.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPort_KeyDown);
            // 
            // btnStart
            // 
            this.btnStart.AutoSize = true;
            this.btnStart.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnStart.Depth = 0;
            this.btnStart.Location = new System.Drawing.Point(482, 4);
            this.btnStart.Margin = new System.Windows.Forms.Padding(7, 2, 7, 2);
            this.btnStart.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnStart.Name = "btnStart";
            this.btnStart.Primary = false;
            this.btnStart.Size = new System.Drawing.Size(66, 36);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // lblState
            // 
            this.lblState.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblState.AutoSize = true;
            this.lblState.Depth = 0;
            this.lblState.Font = new System.Drawing.Font("Roboto", 11F);
            this.lblState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblState.Location = new System.Drawing.Point(699, 9);
            this.lblState.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(0, 24);
            this.lblState.TabIndex = 2;
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(10, 60);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(821, 521);
            this.txtLog.TabIndex = 3;
            // 
            // txtIPServer
            // 
            this.txtIPServer.Depth = 0;
            this.txtIPServer.Hint = "Server IPAddress";
            this.txtIPServer.Location = new System.Drawing.Point(12, 12);
            this.txtIPServer.MouseState = MaterialSkin.MouseState.HOVER;
            this.txtIPServer.Name = "txtIPServer";
            this.txtIPServer.PasswordChar = '\0';
            this.txtIPServer.SelectedText = "";
            this.txtIPServer.SelectionLength = 0;
            this.txtIPServer.SelectionStart = 0;
            this.txtIPServer.Size = new System.Drawing.Size(257, 28);
            this.txtIPServer.TabIndex = 4;
            this.txtIPServer.Text = "192.168.1.6";
            this.txtIPServer.UseSystemPasswordChar = false;
            // 
            // frmServer
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(841, 587);
            this.Controls.Add(this.txtIPServer);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.lblState);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.txtPort);
            this.MaximizeBox = false;
            this.Name = "frmServer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ChatsApp Server";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MaterialSkin.Controls.MaterialSingleLineTextField txtPort;
        private MaterialSkin.Controls.MaterialFlatButton btnStart;
        private MaterialSkin.Controls.MaterialLabel lblState;
        private System.Windows.Forms.TextBox txtLog;
        private MaterialSkin.Controls.MaterialSingleLineTextField txtIPServer;
    }
}

