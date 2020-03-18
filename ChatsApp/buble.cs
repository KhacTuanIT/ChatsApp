﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatsApp
{
    public partial class buble : UserControl
    {
        public buble()
        {
                InitializeComponent();
        }

        public buble(string name, string message, string time, MessageType messageType)
        {
            InitializeComponent();
            lblUser.Text = name;
            lblMessage.Text = message;
            lblTime.Text = time;

            if (messageType.ToString() == "Out")
            {
                this.BackColor = Color.FromArgb(38, 166, 154);
            }
            else if (messageType.ToString() == "In")
            {
                this.BackColor = Color.Gray;
            }

            else if (messageType.ToString() == "OutFile")
            {
                lblMessage.ContextMenuStrip = cmsDownload;
                lblMessage.Font = new Font(lblMessage.Font.Name, lblMessage.Font.Size, FontStyle.Underline);
                lblMessage.ForeColor = Color.FromArgb(0, 96, 100);
                this.BackColor = Color.LightGray;
            }
            else
            {
                lblMessage.ContextMenuStrip = cmsDownload;
                lblMessage.Font = new Font(lblMessage.Font.Name, lblMessage.Font.Size, FontStyle.Underline);
                lblMessage.ForeColor = Color.FromArgb(0, 96, 100);
                this.BackColor = Color.LightGray;
            }
            SetHeight();
        }

        private void SetHeight()
        {
            Size maxSize = new Size(200, int.MaxValue);
            Graphics g = CreateGraphics();
            SizeF size = g.MeasureString(lblMessage.Text, lblMessage.Font, lblMessage.Width);

            lblMessage.Height = int.Parse(Math.Round(size.Height + 2, 0).ToString());
            lblTime.Top = lblMessage.Bottom + 10;
            panelBubble.Height = lblTime.Bottom + 10;
            this.Height = lblTime.Bottom + 10;
        }

        private void buble_Resize(object sender, EventArgs e)
        {
            SetHeight();
        }

        private void panelBubble_Resize(object sender, EventArgs e)
        {
            SetHeight();
        }
    }

    public enum MessageType
    {
        In,
        Out,
        InFile,
        OutFile

    }
}