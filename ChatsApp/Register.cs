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
using System.Collections;
using System.Configuration;
using ChatObject;

namespace ChatsApp
{
    public partial class Register : UserControl
    {
        private string user = "user";
        private frmLogin _frmLogin = null;
        private ChatClientControl chatClient = null;
        private string hostAddress = "";
        private int iPort = 0;
        private Register _register = null;
        private bool isConnect = false;

        public Register(frmLogin _frmLogin, string user)
        {
            InitializeComponent();
            this.user = user;
            _register = this;
            this._frmLogin = _frmLogin;
        }

        private void linkRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (isConnect)
            {
                ChatHeaderObject header = new ChatHeaderObject()
                {
                    Header = Header.Quit,
                    SessionFrom = this.user
                };
                ChatPayloadObject payload = new ChatPayloadObject()
                {
                };
                ChatDataObject chatData = new ChatDataObject()
                {
                    Header = header,
                    Payload = payload
                };
                this.chatClient.sendDataObject(chatData);
            }
            Login login = new Login(this._frmLogin, this.user);
            login.Location = this.Location;
            login.Width = this.Width;
            login.Height = this.Height;
            login.Anchor = this.Anchor;
            panel1.Controls.Clear();
            panel1.Controls.Add(login);
            
        }

        public void getConfig()
        {
            this.hostAddress = ConfigurationManager.AppSettings["hostAddress"];
            this.iPort = Int32.Parse(ConfigurationManager.AppSettings["iPort"]);
        }

        public void Register_Click(object sender, EventArgs e)
        {
            this.btnRegister_Click(sender, e);
        }

        public void connect(string hostAddress, int iPort)
        {
            try
            {
                this.chatClient = new ChatClientControl();
                if (this.chatClient.connect(hostAddress, iPort))
                {
                    ChatJoinObject chatJoin = new ChatJoinObject();

                    chatJoin.Username = this.user;
                    this.chatClient.sendDataObject(chatJoin);
                }
            }
            catch (Exception)
            {
                lblWarning.Text = "Kết nối tới máy chủ thất bại!";
                isConnect = false;
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (!isConnect)
            {
                getConfig();
                connect(hostAddress, iPort);
                System.Threading.Thread.Sleep(200);
                isConnect = true;
            }
            string fullname = txtFullname.Text.Trim();
            string username = txtUsername.Text.Trim();
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();

            lblWarning.ForeColor = Color.Red;
            if ("".Equals(fullname) || "".Equals(username) || "".Equals(password) || "".Equals(email))
            {
                lblWarning.Text = "Vui lòng điền đầy đủ các trường";
            }
            else
            {
                sendRegister(fullname, username, password, email);
                if (chatClient != null)
                {
                    try
                    {
                        Object o = this.chatClient.recvDataObject();
                        if (o != null)
                        {
                            ChatDataObject chatData = (ChatDataObject)o;

                            if (chatData.Header.Header == Header.Register)
                            {
                                if (chatData.Payload.StatusCode == StatusCode.ExistEmail)
                                {
                                    this.lblWarning.Text = "Email đã tồn tại!";
                                    //invokeLabel(_register.lblWarning, "Email đã tồn tại");
                                }
                                else if (chatData.Payload.StatusCode == StatusCode.ExistUsername)
                                {
                                    this.lblWarning.Text = "Tài khoản đã tồn tại";
                                    //invokeLabel(_register.lblWarning, "Tên tài khoản đã tồn tại");
                                }
                                else if (chatData.Payload.StatusCode == StatusCode.MissField)
                                {
                                    this.lblWarning.Text = "Vui lòng điền đầy đủ các trường!";
                                    //invokeLabel(_register.lblWarning, "Vui lòng điền đầy đủ các trường!");
                                }
                                else if (chatData.Payload.StatusCode == StatusCode.CheckTrue)
                                {
                                    this.txtEmail.Text = "";
                                    this.txtFullname.Text = "";
                                    this.txtPassword.Text = "";
                                    this.txtUsername.Text = "";
                                    this.lblWarning.Text = "Đăng ký thành công";
                                    this.lblWarning.ForeColor = Color.Green;
                                    //invokeLabel(_register.lblWarning, "Đăng ký thành công!");
                                    chatData.Header.Header = Header.Quit;
                                    this.chatClient.sendDataObject(chatData);
                                    isConnect = false;
                                }
                            }
                        }
                    }
                    catch (Exception)
                    {
                        lblWarning.Text = "Kết nối tới máy chủ thất bại!";
                        //ChatHeaderObject header = new ChatHeaderObject()
                        //{
                        //    Header = Header.Quit,
                        //    SessionFrom = this.user
                        //};
                        //ChatPayloadObject payload = new ChatPayloadObject()
                        //{
                        //};
                        //ChatDataObject chatData = new ChatDataObject()
                        //{
                        //    Header = header,
                        //    Payload = payload
                        //};
                        //this.chatClient.sendDataObject(chatData);
                    }
                }
            }
        }

        private void invokeLabel(Label label, string message)
        {
            if (label.InvokeRequired)
            {
                label.Invoke(new Action(() =>
                {
                    label.Text = message;
                }));
            }
            else
            {
                label.Text = message;
            }
        }

        private void invokehideForm(Form form)
        {
            if (form.InvokeRequired)
            {
                form.Invoke(new Action(() =>
                {
                    form.Hide();
                }));
            }
            else
            {
                form.Hide();
            }
        }

        private void sendRegister(string fullname, string username, string password, string email)
        {
            ChatHeaderObject header = new ChatHeaderObject()
            {
                Header = Header.Register,
                SessionFrom = this.user
            };
            ChatPayloadObject payload = new ChatPayloadObject()
            {
                Fullname = fullname,
                Username = username,
                Password = password, 
                Email = email
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
            catch (Exception)
            {
                lblWarning.Text = "Kết nối tới máy chủ thất bại!";
                isConnect = false;
            }
        }

        private void txtFullname_KeyDown(object sender, KeyEventArgs e)
        {
            lblWarning.Text = "";
        }

        private void txtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            lblWarning.Text = "";
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!isConnect)
                {
                    getConfig();
                    connect(hostAddress, iPort);
                    System.Threading.Thread.Sleep(200);
                    isConnect = true;
                }
                btnRegister_Click(sender, e);
            }
        }

        private void txtEmail_KeyDown(object sender, KeyEventArgs e)
        {
            lblWarning.Text = "";
        }
    }
}
