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
using System.IO;

namespace ChatsApp
{
    public partial class chatbox : UserControl
    {
        private int curTop = 10;
        private ChatClientControl chatClient = null;
        public string roomName = "";
        buble oldBuble = new buble();
        private string username = "";
        private string messageType  = "";
        private string fullname = "";
        public string toUser = "";
        private ChatObject.ChatTypeMess type;
        private chatbox _box = null;

        public delegate void AddMessage(List<TempMessage> messages);
        public AddMessage addMessage;

        public delegate void AddOneMessage(TempMessage message);
        public AddOneMessage addOneMessage;
        
        public chatbox(ChatClientControl chatClient, string  username, string messageType, string fullname, string toUser)
        {
            InitializeComponent();
            _box = this;
            this.chatClient = chatClient;
            this.username = username;
            this.messageType = messageType;
            this.fullname = fullname;
            this.toUser = toUser;
            oldBuble.Top = 0 - oldBuble.Height + 10;
            buble1.Visible = false;
            buble2.Visible = false;
            getRoomName();
            addMessage = new AddMessage(AddMessageMethod);
            addOneMessage = new AddOneMessage(AddOneMessageMethod);
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

        private void getRoomName()
        {
            ChatHeaderObject header = new ChatHeaderObject()
            {
                SessionFrom = this.username,
                Header = Header.GetRoomName
            };

            ChatPayloadObject payload = new ChatPayloadObject()
            {
                Username = username,
                Fullname = this.fullname,
                Data = this.toUser
            };
            ChatDataObject chatData = new ChatDataObject()
            {
                Header = header,
                Payload = payload
            };
            try
            {
                this.chatClient.sendDataObject(chatData);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }

        
        private void buble1_Load(object sender, EventArgs e)
        {

        }

        private void buble2_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (type == ChatObject.ChatTypeMess.Message)
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
                        ChatType = type
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
            else if (type == ChatObject.ChatTypeMess.File)
            {
                MessageBox.Show("Chức năng đang phát triển!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //try
                //{
                //    byte[] dataFile = File.ReadAllBytes(txtMessage.Text.Trim());
                //    string dataTranfer = Convert.ToBase64String(dataFile);
                //    string filePath = txtMessage.Text.Trim();
                //    string[] temp = filePath.Split('\\');
                //    string filename = temp[temp.Length - 1];
                //    sendFile(this.fullname, filename, DateTime.Now.ToShortTimeString());
                //    ChatHeaderObject header = new ChatHeaderObject()
                //    {
                //        Header = Header.Upload,
                //        SessionFrom = this.username,
                //        SessionTo = this.roomName,
                //        ChatType = type
                //    };
                //    ChatPayloadObject payload = new ChatPayloadObject()
                //    {
                //        MessageType = this.messageType,
                //        Data = dataTranfer,
                //        Time = DateTime.Now,
                //        Filename = filename
                //    };
                //    ChatDataObject chatData = new ChatDataObject()
                //    {
                //        Header = header,
                //        Payload = payload
                //    };
                //    this.chatClient.sendDataObject(chatData);
                //    txtMessage.Text = "";
                //} 
                //catch (Exception)
                //{
                //    MessageBox.Show("Đường dẫn file sai", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //}
            }
        }

        private void AddOneMessageMethod(TempMessage message)
        {
            if (message != null)
            {
                if (message.ChatType == ChatObject.ChatTypeMess.Message)
                {
                    if (message.Username.Equals(username))
                    {
                        sendMessage(message.Fullname, message.Message, message.Time);
                    }
                    else
                    {
                        receiveMessage(message.Fullname, message.Message, message.Time);
                    }
                }
                else
                {
                    if (message.Username.Equals(username))
                    {
                        sendFile(message.Fullname, message.Message, message.Time);
                    }
                    else
                    {
                        receiveFile(message.Fullname, message.Message, message.Time);
                    }
                }
            }
        }

        private void AddMessageMethod(List<TempMessage> messages)
        {
            if (messages.Count > 0)
            {
                foreach (TempMessage message in messages)
                {
                    if (message.ChatType == ChatObject.ChatTypeMess.Message)
                    {
                        if (message.Username.Equals(username))
                        {
                            sendMessage(message.Fullname, message.Message, message.Time);
                        }
                        else
                        {
                            receiveMessage(message.Fullname, message.Message, message.Time);
                        }
                    }
                    else
                    {
                        if (message.Username.Equals(username))
                        {
                            sendFile(message.Fullname, message.Message, message.Time);
                        }
                        else
                        {
                            receiveFile(message.Fullname, message.Message, message.Time);
                        }
                    }
                }
            }
        }

        public void sendMessage(string name, string message, string time)
        {
            buble buble = new ChatsApp.buble(name, message, time, MessageType.Out, this.chatClient);
            buble.Location = buble2.Location;
            buble.Size = buble2.Size;
            buble.Anchor = buble2.Anchor;
            buble.Top = oldBuble.Bottom + 10;
            curTop = buble.Bottom + 10;

            panel2.Controls.Add(buble);

            oldBuble = buble;
            panel2.VerticalScroll.Value = panel2.VerticalScroll.Maximum;
        }

        public void receiveMessage(string name, string message, string time)
        {
            buble buble = new ChatsApp.buble(name, message, time, MessageType.In, this.chatClient);
            buble.Location = buble1.Location;
            buble.Size = buble1.Size;
            buble.Anchor = buble1.Anchor;
            buble.Top = oldBuble.Bottom + 10;
            curTop = buble.Bottom + 10;

            panel2.Controls.Add(buble);

            oldBuble = buble;
            panel2.VerticalScroll.Value = panel2.VerticalScroll.Maximum;
        }

        public void sendFile(string name, string title, string time)
        {
            buble buble = new ChatsApp.buble(name, title, time, MessageType.OutFile, this.chatClient);
            buble.Location = buble2.Location;
            buble.Size = buble2.Size;
            buble.Anchor = buble2.Anchor;
            buble.Top = oldBuble.Bottom + 10;
            curTop = buble.Bottom + 10;

            panel2.Controls.Add(buble);

            oldBuble = buble;
            panel2.VerticalScroll.Value = panel2.VerticalScroll.Maximum;
        }

        public void receiveFile(string name, string title, string time)
        {
            buble buble = new ChatsApp.buble(name, title, time, MessageType.InFile, this.chatClient);
            buble.Location = buble1.Location;
            buble.Size = buble1.Size;
            buble.Anchor = buble1.Anchor;
            buble.Top = oldBuble.Bottom + 10;
            curTop = buble.Bottom + 10;

            panel2.Controls.Add(buble);

            oldBuble = buble;
            panel2.VerticalScroll.Value = panel2.VerticalScroll.Maximum;
        }

        private void txtMessage_KeyDown(object sender, KeyEventArgs e)
        {
            type = ChatObject.ChatTypeMess.Message;
            if (e.KeyCode == Keys.Enter)
            {
                button2_Click(sender, e);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            type = ChatObject.ChatTypeMess.File;
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = @"C:\",
                Title = "Browse Text Files",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "txt",
                Filter = "txt files (*.txt)|*.txt",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtMessage.Text = openFileDialog1.FileName;
            }
        }
    }
}
