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
using System.Threading;
using System.Configuration;
using System.Collections;

namespace ChatsApp
{
    public partial class Login : UserControl
    {
        private ChatClientControl chatClient = null;
        private string hostAddress = "";
        private int iPort = 0;
        private Login _login = null;
        private string user = "";
        private frmLogin _frmLogin = null;
        private bool isConnect = false;
        private Queue mqRequestQueue = null;

        public Login()
        {
            InitializeComponent();
        }
        public Login(frmLogin _frmLogin,string user)
        {
            InitializeComponent();
            this._frmLogin = _frmLogin;
            _login = this;
            this.user = user;
            this.mqRequestQueue = new Queue();
        }

        public void getConfig()
        {
            this.hostAddress = ConfigurationManager.AppSettings["hostAddress"];
            this.iPort = Int32.Parse(ConfigurationManager.AppSettings["iPort"]);
            //this.hostAddress = "127.0.0.1";
            //this.iPort = 9002;
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
            catch (Exception ex)
            {
                lblWarning.Text = "Kết nối tới máy chủ thất bại!";
                isConnect = false;
            }
        }

        private void runRecvData()
        {
            while (this.chatClient != null)
            {
                try
                {
                    Object o = this.chatClient.recvDataObject();
                    if (o != null)
                    {
                        ChatDataObject chatData = (ChatDataObject)o;

                        if (chatData.Header.Header == Header.Login)
                        {
                            if (chatData.Payload.StatusCode == StatusCode.MissUsername)
                            {
                                invokeLabel(_login.lblWarning, "Tài khoản không chính xác");
                            }
                            else if (chatData.Payload.StatusCode == StatusCode.PasswordWrong)
                            {
                                invokeLabel(_login.lblWarning, "Mật khẩu không chính xác");
                            }
                            else if (chatData.Payload.StatusCode == StatusCode.CheckTrue)
                            {
                                invokehideForm(_frmLogin);
                                frmMain main = new frmMain(chatData.Payload.Username, chatData.Payload.Fullname);
                                main.Show();
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                System.Threading.Thread.Sleep(200);
            }
        }

        private void invokeLabel (Label label, string message)
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

        private void linkRegister_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Register register = new Register(this._frmLogin, this.user);
            register.Location = this.Location;
            register.Width = this.Width;
            register.Height = this.Height;
            register.Anchor = this.Anchor;
            panel1.Controls.Clear();
            panel1.Controls.Add(register);
        }

        public void Login_Click(object sender, EventArgs e)
        {
            this.btnLogin_Click(sender, e);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (!isConnect)
            {
                getConfig();
                connect(hostAddress, iPort);
                Thread.Sleep(200);
                isConnect = true;
            }
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            lblWarning.Text = "";
            if ("".Equals(username) || "".Equals(password))
            {
                lblWarning.Text = "Chưa điền tài khoản/mật khẩu";
            }
            else
            {
                //ThreadStart threadStart = new ThreadStart(runRecvData);
                //Thread thread = new Thread(threadStart);

                //thread.IsBackground = true;
                //thread.Start();
                checkLogin(username, password);
                if (chatClient != null)
                {
                    try
                    {
                        Object o = this.chatClient.recvDataObject();
                        if (o != null)
                        {
                            ChatDataObject chatData = (ChatDataObject)o;

                            if (chatData.Header.Header == Header.Login)
                            {
                                if (chatData.Payload.StatusCode == StatusCode.MissUsername)
                                {
                                    invokeLabel(_login.lblWarning, "Tài khoản không chính xác");
                                }
                                else if (chatData.Payload.StatusCode == StatusCode.PasswordWrong)
                                {
                                    invokeLabel(_login.lblWarning, "Mật khẩu không chính xác");
                                }
                                else if (chatData.Payload.StatusCode == StatusCode.CheckTrue)
                                {
                                    chatData.Header.Header = Header.Quit;
                                    this.chatClient.sendDataObject(chatData);
                                    invokehideForm(_frmLogin);
                                    frmMain main = new frmMain(chatData.Payload.Username, chatData.Payload.Fullname);
                                    main.Show();
                                }
                            }
                        }
                    }
                    catch (Exception)
                    {
                        lblWarning.Text = "Kết nối tới máy chủ thất bại!";
                        isConnect = false;
                    }
                }
            }
        }

        private void checkLogin(string username, string password)
        {
            ChatHeaderObject header = new ChatHeaderObject()
            {
                SessionFrom = this.user,
                Header = Header.Login
            };

            ChatPayloadObject payload = new ChatPayloadObject()
            {
                Username = username,
                Password = password
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

        private void txtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            lblWarning.Text = "";
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            lblWarning.Text = "";
            if (e.KeyCode == Keys.Enter)
            {
                if (!isConnect)
                {
                    getConfig();
                    connect(hostAddress, iPort);
                    Thread.Sleep(200);
                    isConnect = true;
                }

                btnLogin_Click(sender, e);
            }
        }

        private void lblForget_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Login_Load(object sender, EventArgs e)
        {
            //connect(hostAddress, iPort);
        }
    }

}
