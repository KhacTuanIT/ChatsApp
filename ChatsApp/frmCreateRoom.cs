using ChatObject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatsApp
{
    public partial class frmCreateRoom : Form
    {
        private ChatClientControl chatClient = null;
        private string fullname = "";
        private string username = "";
        private List<string> listUser = null;

        public delegate void AddListBox(List<string> users);
        public AddListBox addListBox;

        public frmCreateRoom()
        {
            InitializeComponent();
        }
        public frmCreateRoom(ChatClientControl chatClient, string fullname, string username, List<string> users)
        {
            InitializeComponent();
            this.chatClient = chatClient;
            this.fullname = fullname;
            this.username = username;
            this.listUser = users;
            addListBox = new AddListBox(AddListBoxMethod);
        }

        private void addUser(List<string> users)
        {
            if (users.Count > 0)
            {
                lbUsers.Items.Clear();
                foreach (string user in users)
                {
                    lbUsers.Items.Add(user);
                }
            }
        }

        private void frmCreateRoom_Load(object sender, EventArgs e)
        {
            addUser(listUser);
        }

        private void AddListBoxMethod(List<string> users)
        {
            if (users.Count > 0)
            {
                lbUsers.Items.Clear();
                foreach (string user in users)
                {
                    if (!user.Equals(fullname))
                        lbUsers.Items.Add(user);
                }
            }
        } 

        private string convertUserListToString(List<string> users)
        {
            return string.Join(",", users);
        }

        private void btnCreateRoom_Click(object sender, EventArgs e)
        {
            string roomName = txtRoomName.Text;
            if (!"".Equals(roomName))
            {
                List<string> users = new List<string>();
                foreach (string user in lbUsers.CheckedItems)
                {
                    users.Add(user);
                }
                if (users.Count > 0)
                {
                    string usersString = convertUserListToString(users);
                    ChatHeaderObject header = new ChatHeaderObject()
                    {
                        SessionFrom = this.username,
                        Header = Header.CreateRoom
                    };
                    ChatPayloadObject payload = new ChatPayloadObject()
                    {
                        ChatroomName = roomName,
                        Data = usersString
                    };
                    ChatDataObject chatData = new ChatDataObject()
                    {
                        Header = header,
                        Payload = payload
                    };
                    this.chatClient.sendDataObject(chatData);
                    if (chatClient != null)
                    {
                        try
                        {
                            Object o = this.chatClient.recvDataObject();
                            if (o != null)
                            {
                                ChatDataObject chatDataRecv = (ChatDataObject)o;

                                if (chatDataRecv.Header.Header == Header.CreateRoom)
                                {
                                    if (chatDataRecv.Payload.StatusCode == StatusCode.CheckTrue)
                                    {
                                        lblWarning.Text = "Tạo phòng thành công!";
                                    }
                                    else
                                    {
                                        lblWarning.Text = "Tên phòng trùng/ chưa chọn thành viên!";
                                    }
                                }
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
                else
                {
                    lblWarning.Text = "Chưa chọn thành viên!";
                }
            }
            else
            {
                lblWarning.Text = "Chưa nhập tên phòng!";
            }
        }

        private void txtRoomName_KeyDown(object sender, KeyEventArgs e)
        {
            if(lblWarning.Text != "")
            {
                lblWarning.Text = "";
            }
        }
    }
}
