namespace ChatsApp
{
    partial class frmCreateRoom
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
            this.txtRoomName = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.btnCreateRoom = new System.Windows.Forms.Button();
            this.lbUsers = new System.Windows.Forms.CheckedListBox();
            this.lblWarning = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtRoomName
            // 
            this.txtRoomName.Depth = 0;
            this.txtRoomName.Hint = "Tên phòng";
            this.txtRoomName.Location = new System.Drawing.Point(28, 26);
            this.txtRoomName.MouseState = MaterialSkin.MouseState.HOVER;
            this.txtRoomName.Name = "txtRoomName";
            this.txtRoomName.PasswordChar = '\0';
            this.txtRoomName.SelectedText = "";
            this.txtRoomName.SelectionLength = 0;
            this.txtRoomName.SelectionStart = 0;
            this.txtRoomName.Size = new System.Drawing.Size(349, 28);
            this.txtRoomName.TabIndex = 0;
            this.txtRoomName.UseSystemPasswordChar = false;
            this.txtRoomName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRoomName_KeyDown);
            // 
            // btnCreateRoom
            // 
            this.btnCreateRoom.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(94)))), ((int)(((byte)(32)))));
            this.btnCreateRoom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreateRoom.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreateRoom.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(94)))), ((int)(((byte)(32)))));
            this.btnCreateRoom.Location = new System.Drawing.Point(239, 310);
            this.btnCreateRoom.Name = "btnCreateRoom";
            this.btnCreateRoom.Size = new System.Drawing.Size(138, 40);
            this.btnCreateRoom.TabIndex = 2;
            this.btnCreateRoom.Text = "Tạo phòng";
            this.btnCreateRoom.UseVisualStyleBackColor = true;
            this.btnCreateRoom.Click += new System.EventHandler(this.btnCreateRoom_Click);
            // 
            // lbUsers
            // 
            this.lbUsers.FormattingEnabled = true;
            this.lbUsers.Location = new System.Drawing.Point(28, 60);
            this.lbUsers.Name = "lbUsers";
            this.lbUsers.Size = new System.Drawing.Size(349, 225);
            this.lbUsers.TabIndex = 3;
            // 
            // lblWarning
            // 
            this.lblWarning.AutoSize = true;
            this.lblWarning.ForeColor = System.Drawing.Color.Red;
            this.lblWarning.Location = new System.Drawing.Point(25, 288);
            this.lblWarning.Name = "lblWarning";
            this.lblWarning.Size = new System.Drawing.Size(0, 17);
            this.lblWarning.TabIndex = 4;
            // 
            // frmCreateRoom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(405, 362);
            this.Controls.Add(this.lblWarning);
            this.Controls.Add(this.lbUsers);
            this.Controls.Add(this.btnCreateRoom);
            this.Controls.Add(this.txtRoomName);
            this.Name = "frmCreateRoom";
            this.Text = "Tạo phòng";
            this.Load += new System.EventHandler(this.frmCreateRoom_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MaterialSkin.Controls.MaterialSingleLineTextField txtRoomName;
        private System.Windows.Forms.Button btnCreateRoom;
        private System.Windows.Forms.CheckedListBox lbUsers;
        private System.Windows.Forms.Label lblWarning;
    }
}