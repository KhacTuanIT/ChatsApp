namespace ChatsApp
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.cmsSetting = new MaterialSkin.Controls.MaterialContextMenuStrip();
            this.tmsiDeleteMessage = new System.Windows.Forms.ToolStripMenuItem();
            this.lblNameUser = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.button3 = new System.Windows.Forms.Button();
            this.materialSingleLineTextField1 = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnMinimize = new System.Windows.Forms.Button();
            this.btnScreen = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnOut = new System.Windows.Forms.Button();
            this.panelAside = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panelChatbox = new System.Windows.Forms.Panel();
            this.chatbox1 = new ChatsApp.chatbox();
            this.panel1.SuspendLayout();
            this.cmsSetting.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panel5.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panelAside.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panelChatbox.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(125)))), ((int)(((byte)(50)))));
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.lblNameUser);
            this.panel1.Controls.Add(this.pictureBox2);
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.panel2);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.BackgroundImage = global::ChatsApp.Properties.Resources.icons8_menu_horizontal_80;
            this.button1.ContextMenuStrip = this.cmsSetting;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // cmsSetting
            // 
            this.cmsSetting.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmsSetting.Depth = 0;
            this.cmsSetting.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmsSetting.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tmsiDeleteMessage});
            this.cmsSetting.MouseState = MaterialSkin.MouseState.HOVER;
            this.cmsSetting.Name = "cmsSetting";
            resources.ApplyResources(this.cmsSetting, "cmsSetting");
            // 
            // tmsiDeleteMessage
            // 
            this.tmsiDeleteMessage.Name = "tmsiDeleteMessage";
            resources.ApplyResources(this.tmsiDeleteMessage, "tmsiDeleteMessage");
            // 
            // lblNameUser
            // 
            resources.ApplyResources(this.lblNameUser, "lblNameUser");
            this.lblNameUser.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblNameUser.Name = "lblNameUser";
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImage = global::ChatsApp.Properties.Resources.icons8_chat_room_64;
            resources.ApplyResources(this.pictureBox2, "pictureBox2");
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.TabStop = false;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(245)))), ((int)(((byte)(233)))));
            this.panel5.Controls.Add(this.button3);
            this.panel5.Controls.Add(this.materialSingleLineTextField1);
            resources.ApplyResources(this.panel5, "panel5");
            this.panel5.Name = "panel5";
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Transparent;
            this.button3.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.button3, "button3");
            this.button3.Image = global::ChatsApp.Properties.Resources.ic_search_black_36dp;
            this.button3.Name = "button3";
            this.button3.UseVisualStyleBackColor = false;
            // 
            // materialSingleLineTextField1
            // 
            resources.ApplyResources(this.materialSingleLineTextField1, "materialSingleLineTextField1");
            this.materialSingleLineTextField1.Depth = 0;
            this.materialSingleLineTextField1.ForeColor = System.Drawing.Color.White;
            this.materialSingleLineTextField1.Hint = "Tìm kiếm...";
            this.materialSingleLineTextField1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialSingleLineTextField1.Name = "materialSingleLineTextField1";
            this.materialSingleLineTextField1.PasswordChar = '\0';
            this.materialSingleLineTextField1.SelectedText = "";
            this.materialSingleLineTextField1.SelectionLength = 0;
            this.materialSingleLineTextField1.SelectionStart = 0;
            this.materialSingleLineTextField1.UseSystemPasswordChar = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(94)))), ((int)(((byte)(32)))));
            this.panel2.Controls.Add(this.btnMinimize);
            this.panel2.Controls.Add(this.btnScreen);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Controls.Add(this.btnOut);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            this.panel2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel2_MouseDown);
            this.panel2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel2_MouseMove);
            this.panel2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel2_MouseUp);
            // 
            // btnMinimize
            // 
            resources.ApplyResources(this.btnMinimize, "btnMinimize");
            this.btnMinimize.BackgroundImage = global::ChatsApp.Properties.Resources.icons8_minimize_window_32px;
            this.btnMinimize.FlatAppearance.BorderSize = 0;
            this.btnMinimize.Name = "btnMinimize";
            this.btnMinimize.UseVisualStyleBackColor = true;
            this.btnMinimize.Click += new System.EventHandler(this.btnMinimize_Click);
            // 
            // btnScreen
            // 
            resources.ApplyResources(this.btnScreen, "btnScreen");
            this.btnScreen.BackgroundImage = global::ChatsApp.Properties.Resources.icons8_fit_to_width_26px;
            this.btnScreen.FlatAppearance.BorderSize = 0;
            this.btnScreen.Name = "btnScreen";
            this.btnScreen.UseVisualStyleBackColor = true;
            this.btnScreen.Click += new System.EventHandler(this.btnScreen_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Name = "label1";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::ChatsApp.Properties.Resources.icons8_weixin_64;
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // btnOut
            // 
            resources.ApplyResources(this.btnOut, "btnOut");
            this.btnOut.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(94)))), ((int)(((byte)(32)))));
            this.btnOut.FlatAppearance.BorderSize = 0;
            this.btnOut.Image = global::ChatsApp.Properties.Resources.ic_clear_black_36dp;
            this.btnOut.Name = "btnOut";
            this.btnOut.UseVisualStyleBackColor = true;
            this.btnOut.Click += new System.EventHandler(this.btnOut_Click);
            // 
            // panelAside
            // 
            this.panelAside.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelAside.Controls.Add(this.button2);
            resources.ApplyResources(this.panelAside, "panelAside");
            this.panelAside.Name = "panelAside";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.button2.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            resources.ApplyResources(this.button2, "button2");
            this.button2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(97)))), ((int)(((byte)(97)))), ((int)(((byte)(97)))));
            this.button2.Image = global::ChatsApp.Properties.Resources.icons8_communication_64;
            this.button2.Name = "button2";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.panelChatbox);
            resources.ApplyResources(this.panel4, "panel4");
            this.panel4.Name = "panel4";
            // 
            // panelChatbox
            // 
            this.panelChatbox.Controls.Add(this.chatbox1);
            resources.ApplyResources(this.panelChatbox, "panelChatbox");
            this.panelChatbox.Name = "panelChatbox";
            // 
            // chatbox1
            // 
            this.chatbox1.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.chatbox1, "chatbox1");
            this.chatbox1.Name = "chatbox1";
            // 
            // frmMain
            // 
            this.AllowDrop = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            resources.ApplyResources(this, "$this");
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panelAside);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmMain";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.cmsSetting.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panelAside.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panelChatbox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnOut;
        private System.Windows.Forms.Panel panelAside;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel panel4;
        private MaterialSkin.Controls.MaterialSingleLineTextField materialSingleLineTextField1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblNameUser;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panelChatbox;
        private System.Windows.Forms.Button btnScreen;
        private System.Windows.Forms.Button btnMinimize;
        private MaterialSkin.Controls.MaterialContextMenuStrip cmsSetting;
        private System.Windows.Forms.ToolStripMenuItem tmsiDeleteMessage;
        private chatbox chatbox1;
    }
}

