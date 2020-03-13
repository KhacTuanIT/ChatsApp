 using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ServerChatsApp.Model;

namespace ChatsApp
{
    public partial class chatbox : UserControl
    {
        private int curTop = 10;

        buble oldBuble = new buble();
        public chatbox()
        {
            //if (!this.DesignMode)
            //{
                InitializeComponent();
            //}
            oldBuble.Top = 0 - oldBuble.Height + 10;
            buble1.Visible = false;
            buble2.Visible = false;
            
        }

        private void buble1_Load(object sender, EventArgs e)
        {

        }

        private void buble2_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            sendMessage("Tuan", "Building out a website or brochure based heavily on an image? With Pictaculous, you can simply upload that picture and the app will spit out a color palette that pairs perfectly with it.", "12:00");
            receiveMessage("R", "Hello", "12:00");
            sendFile("Tuan", "FIle", "12:00");
            receiveFile("R", "File", "12:00");
            panel2.VerticalScroll.Value = panel2.VerticalScroll.Maximum;
        }

        public void sendMessage(string name, string message, string time)
        {
            buble buble = new ChatsApp.buble(name, message, time, MessageType.Out);
            buble.Location = buble1.Location;
            buble.Size = buble1.Size;
            buble.Anchor = buble1.Anchor;
            buble.Top = oldBuble.Bottom + 10;
            curTop = buble.Bottom + 10;

            panel2.Controls.Add(buble);

            oldBuble = buble;
        }

        public void receiveMessage(string name, string message, string time)
        {
            buble buble = new ChatsApp.buble(name, message, time, MessageType.In);
            buble.Location = buble2.Location;
            buble.Size = buble2.Size;
            buble.Anchor = buble2.Anchor;
            buble.Top = oldBuble.Bottom + 10;
            curTop = buble.Bottom + 10;

            panel2.Controls.Add(buble);

            oldBuble = buble;
        }

        public void sendFile(string name, string title, string time)
        {
            buble buble = new ChatsApp.buble(name, title, time, MessageType.OutFile);
            buble.Location = buble1.Location;
            buble.Size = buble1.Size;
            buble.Anchor = buble1.Anchor;
            buble.Top = oldBuble.Bottom + 10;
            curTop = buble.Bottom + 10;

            panel2.Controls.Add(buble);

            oldBuble = buble;
        }

        public void receiveFile(string name, string title, string time)
        {
            buble buble = new ChatsApp.buble(name, title, time, MessageType.InFile);
            buble.Location = buble2.Location;
            buble.Size = buble2.Size;
            buble.Anchor = buble2.Anchor;
            buble.Top = oldBuble.Bottom + 10;
            curTop = buble.Bottom + 10;

            panel2.Controls.Add(buble);

            oldBuble = buble;
        }
    }
}
