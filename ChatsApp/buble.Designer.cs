namespace ChatsApp
{
    partial class buble
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelBubble = new System.Windows.Forms.Panel();
            this.lblUser = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.cmsDownload = new MaterialSkin.Controls.MaterialContextMenuStrip();
            this.tsmiDownload = new System.Windows.Forms.ToolStripMenuItem();
            this.panelBubble.SuspendLayout();
            this.cmsDownload.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelBubble
            // 
            this.panelBubble.AllowDrop = true;
            this.panelBubble.AutoSize = true;
            this.panelBubble.Controls.Add(this.lblUser);
            this.panelBubble.Controls.Add(this.lblTime);
            this.panelBubble.Controls.Add(this.lblMessage);
            this.panelBubble.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBubble.Location = new System.Drawing.Point(0, 0);
            this.panelBubble.Name = "panelBubble";
            this.panelBubble.Size = new System.Drawing.Size(751, 80);
            this.panelBubble.TabIndex = 0;
            this.panelBubble.Resize += new System.EventHandler(this.panelBubble_Resize);
            // 
            // lblUser
            // 
            this.lblUser.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUser.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lblUser.Location = new System.Drawing.Point(10, 53);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(129, 25);
            this.lblUser.TabIndex = 4;
            this.lblUser.Text = "Khac Tuan";
            this.lblUser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTime
            // 
            this.lblTime.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lblTime.Location = new System.Drawing.Point(593, 53);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(148, 25);
            this.lblTime.TabIndex = 3;
            this.lblTime.Text = "00:00 AM";
            this.lblTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblMessage
            // 
            this.lblMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lblMessage.Location = new System.Drawing.Point(13, 2);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(728, 51);
            this.lblMessage.TabIndex = 2;
            this.lblMessage.Text = "Building out a website or brochure based heavily on an image? With Pictaculous, y" +
    "ou can simply upload that picture and the app will spit out a color palette that" +
    " pairs perfectly with it.";
            this.lblMessage.UseCompatibleTextRendering = true;
            // 
            // cmsDownload
            // 
            this.cmsDownload.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmsDownload.Depth = 0;
            this.cmsDownload.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmsDownload.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiDownload});
            this.cmsDownload.MouseState = MaterialSkin.MouseState.HOVER;
            this.cmsDownload.Name = "materialContextMenuStrip1";
            this.cmsDownload.Size = new System.Drawing.Size(148, 30);
            // 
            // tsmiDownload
            // 
            this.tsmiDownload.Image = global::ChatsApp.Properties.Resources.icons8_download_100;
            this.tsmiDownload.Name = "tsmiDownload";
            this.tsmiDownload.Size = new System.Drawing.Size(147, 26);
            this.tsmiDownload.Text = "Tải xuống";
            // 
            // buble
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(166)))), ((int)(((byte)(154)))));
            this.Controls.Add(this.panelBubble);
            this.DoubleBuffered = true;
            this.Name = "buble";
            this.Size = new System.Drawing.Size(751, 80);
            this.Resize += new System.EventHandler(this.buble_Resize);
            this.panelBubble.ResumeLayout(false);
            this.cmsDownload.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelBubble;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Label lblMessage;
        private MaterialSkin.Controls.MaterialContextMenuStrip cmsDownload;
        private System.Windows.Forms.ToolStripMenuItem tsmiDownload;
        private System.Windows.Forms.Label lblUser;
    }
}
