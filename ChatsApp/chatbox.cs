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
using ChatObject;

namespace ChatsApp
{
    public partial class chatbox : UserControl
    {
        private int curTop = 10;
        private ChatClientControl chatClient = null;
        private string roomName = "";
        buble oldBuble = new buble();
        private string username = "";
        private string messageType  = "";
        private string fullname = "";
        private ChatObject.ChatType type;

        public chatbox(ChatClientControl chatClient, string roomName, string  username, string messageType, string fullname)
        {
            InitializeComponent();
            this.chatClient = chatClient;
            this.roomName = roomName;
            this.username = username;
            this.messageType = messageType;
            this.fullname = fullname;
            oldBuble.Top = 0 - oldBuble.Height + 10;
            buble1.Visible = false;
            buble2.Visible = false;
        }
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
            if (type == ChatObject.ChatType.Message)
            {
                string mess = txtMessage.Text;
                if (mess != "" && mess != null)
                {
                    sendMessage(this.fullname, mess, DateTime.Now.ToShortTimeString());
                    ChatHeaderObject header = new ChatHeaderObject()
                    {
                        Header = Header.Message,
                        SessionFrom = this.username,
                        SessionTo = this.roomName,
                        ChatType = ChatObject.ChatType.Message
                    };
                    ChatPayloadObject payload = new ChatPayloadObject()
                    {
                        MessageType = this.messageType,
                        Data = mess,
                        Time = DateTime.Now,
                        Fullname = this.fullname
                    };
                    ChatDataObject chatData = new ChatDataObject()
                    {
                        Header = header,
                        Payload = payload
                    };
                    this.chatClient.sendDataObject(chatData);
                    txtMessage.Text = "";
                }
            }
            else if (type == ChatObject.ChatType.File)
            {

            }
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
            panel2.VerticalScroll.Value = panel2.VerticalScroll.Maximum;
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
            panel2.VerticalScroll.Value = panel2.VerticalScroll.Maximum;
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
            panel2.VerticalScroll.Value = panel2.VerticalScroll.Maximum;
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
            panel2.VerticalScroll.Value = panel2.VerticalScroll.Maximum;
        }

        private void txtMessage_KeyDown(object sender, KeyEventArgs e)
        {
            type = ChatObject.ChatType.Message;
            if (e.KeyCode == Keys.Enter)
            {
                button2_Click(sender, e);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            type = ChatObject.ChatType.File;
        }
    }
}
