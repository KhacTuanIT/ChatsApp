using ChatObject;
using ServerChatsApp.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServerChatsApp
{
    public partial class frmServer : Form
    {
        private string appFolder = "";

        private ChatServerControl chatServerControl = null;
        private Hashtable mhSessionTable = null;
        private Queue mqRequestQueue = null;
        private ChatDbContext context = null;
        private bool statusRun = false;
        private bool isQuit = false;
        public frmServer()
        {
            InitializeComponent();
            this.mhSessionTable = new Hashtable();
            this.mqRequestQueue = new Queue();
            context = new ChatDbContext();
            createDir();
        }

        public void createDir()
        {
            string myDocument = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            this.appFolder = myDocument + "\\ChatsApp";
            if (!Directory.Exists(appFolder))
            {
                Directory.CreateDirectory(appFolder);
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (statusRun == false)
            {
                MessageBox.Show("OK");

                try
                {
                    int iPort = Convert.ToInt32(this.txtPort.Text);
                    IPAddress iPAddress = IPAddress.Parse(this.txtIPServer.Text);
                    this.chatServerControl = new ChatServerControl();
                    this.chatServerControl.open(iPAddress, iPort);

                    ThreadStart threadStart = new ThreadStart(runServer);
                    Thread thread = new Thread(threadStart);

                    thread.IsBackground = true;
                    thread.Start();

                    threadStart = new ThreadStart(runRecvData);
                    thread = new Thread(threadStart);

                    thread.IsBackground = true;
                    thread.Start();

                    this.lblState.Text = "Server is Starting!";
                    this.btnStart.Text = "Stop";
                    this.txtPort.Enabled = false;
                    statusRun = true;
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                }
            }
            else
            {
                try
                {
                    this.chatServerControl.close();
                    this.chatServerControl = null;

                    this.lblState.Text = "Server is Stopped!";
                    this.btnStart.Text = "Start";
                    this.txtPort.Enabled = true;
                    statusRun = false;
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                }
            }
        }

        private void runServer()
        {
            while (this.chatServerControl != null && this.chatServerControl.isConnected())
            {
                try
                {
                    Socket socketChannel = this.chatServerControl.acceptChannel();
                    System.Threading.Thread.Sleep(200);

                    SocketChannel channel = new SocketChannel(this.txtLog, this.mqRequestQueue);
                    ChatJoinObject chatJoinObjcet = (ChatJoinObject)channel.recvDataObject(socketChannel);

                    channel.startChannel(socketChannel);
                    channel.logMonitor(DateTime.Now + " -Username: " + chatJoinObjcet.Username + " is connected! \r\n");
                    this.mhSessionTable.Add(chatJoinObjcet.Username, channel);
                }
                catch (Exception e)
                {
                    Console.Write(e.Message);
                }
                System.Threading.Thread.Sleep(300);
            }
        }

        private void runRecvData()
        {
            while (this.chatServerControl != null && this.chatServerControl.isConnected())
            {
                try
                {
                    bool status = true;
                    Object o = this.mqRequestQueue.Dequeue();
                    string data = "";
                    string username = "";
                    if (o != null)
                    {
                        ChatDataObject chatData = (ChatDataObject)o;
                        SocketChannel channel = null;
                        if (chatData.Header.SessionTo == null) channel = (SocketChannel)this.mhSessionTable[chatData.Header.SessionFrom];
                        else channel = (SocketChannel)this.mhSessionTable[chatData.Header.SessionTo];
                        if (chatData.Header.Header == Header.Download)
                        {
                            channel = (SocketChannel)this.mhSessionTable[chatData.Header.SessionFrom];
                            string fileName = chatData.Payload.Filename;
                            byte[] dataFile = File.ReadAllBytes(appFolder + "\\" + fileName);
                            string dataTranfer = Convert.ToBase64String(dataFile);
                            chatData.Payload.Data = dataTranfer;
                            data = DateTime.Now + ":Session from " + chatData.Header.SessionFrom + " download file " + fileName;
                        }
                        else if (chatData.Header.Header == Header.Upload)
                        {
                            string fileName = chatData.Payload.Filename;
                            int chatId = int.Parse(chatData.Header.SessionTo);
                            int messageTypeId = int.Parse(chatData.Payload.MessageType);
                            byte[] dataFile = Convert.FromBase64String(chatData.Payload.Data);
                            File.WriteAllBytes(appFolder + "\\" + fileName , dataFile);
                            uploadFile(chatId, messageTypeId, fileName);
                            data = DateTime.Now + ":Session from " + chatData.Header.SessionFrom + " upload file " + fileName;
                        }
                        else if(chatData.Header.Header == Header.Register)
                        {
                            username = chatData.Payload.Username;
                            string email = chatData.Payload.Email;
                            string password = chatData.Payload.Password;
                            string fullname = chatData.Payload.Fullname;
                            channel = (SocketChannel)this.mhSessionTable[chatData.Header.SessionFrom];
                            if(checkExistEmail(email))
                            {
                                status = false;
                                chatData.Payload.StatusCode = StatusCode.ExistEmail;
                                data = DateTime.Now + ":Session from " + chatData.Header.SessionFrom + " register failure. Exist email error!";
                            }
                            else if (checkExistUsername(username))
                            {
                                status = false;
                                chatData.Payload.StatusCode = StatusCode.ExistUsername;
                                data = DateTime.Now + ":Session from " + chatData.Header.SessionFrom + " register failure. Exist username error!";
                            }
                            else
                            {
                                if (checkField(fullname, username, email, password))
                                {
                                    status = false;
                                    chatData.Payload.StatusCode = StatusCode.MissField;
                                    data = DateTime.Now + ":Session from " + chatData.Header.SessionFrom + " register failure. Miss fields error!";
                                }
                                else
                                {
                                    chatData.Payload.StatusCode = StatusCode.CheckTrue;
                                    addUser(fullname, username, email, password);
                                    data = DateTime.Now + ":Session from " + chatData.Header.SessionFrom + " register successfully!";
                                }
                            }
                        }
                        else if (chatData.Header.Header == Header.Login)
                        {
                            username = chatData.Payload.Username;
                            string password = chatData.Payload.Password;
                            channel = (SocketChannel)this.mhSessionTable[chatData.Header.SessionFrom];
                            if (!checkExistUsername(username))
                            {
                                status = false;
                                chatData.Payload.StatusCode = StatusCode.MissUsername;
                                data = DateTime.Now + ":Session from " + chatData.Header.SessionFrom + " login failure. Miss username error!";
                            }
                            else
                            {
                                if (!checkPassword(username, password))
                                {
                                    chatData.Payload.StatusCode = StatusCode.PasswordWrong;
                                    data = DateTime.Now + ":Session from " + chatData.Header.SessionFrom + " login failure. Password wrong error!";
                                }
                                else
                                {
                                    User user = getUser(username);
                                    chatData.Payload.Password = null;
                                    chatData.Payload.Fullname = user.Fullname;
                                    chatData.Payload.StatusCode = StatusCode.CheckTrue;
                                    data = DateTime.Now + ":Session from " + chatData.Header.SessionFrom + " username: " + chatData.Payload.Username + " login successfully!";
                                }
                            }
                        }
                        else if (chatData.Header.Header == Header.Quit)
                        {
                            channel = (SocketChannel)this.mhSessionTable[chatData.Header.SessionFrom];
                            chatData.Payload.StatusCode = StatusCode.CheckTrue;
                            data = DateTime.Now + ":Session from " + chatData.Header.SessionFrom + " quit!";
                            isQuit = true;
                        }
                        else if (chatData.Header.Header == Header.Refresh)
                        {
                            channel = (SocketChannel)this.mhSessionTable[chatData.Header.SessionFrom];
                            string allUser = getAllUser();
                            chatData.Payload.Data = allUser;
                            data = DateTime.Now + ":Session from " + chatData.Header.SessionFrom + " get all user!";
                        }

                        if (isQuit)
                        {
                            this.mhSessionTable.Remove(chatData.Header.SessionFrom);
                            isQuit = false;
                            channel.stopChannel();
                        }
                        else
                        {
                            channel.sendData(chatData);
                            channel.logMonitor(data);
                        }
                        if (status == false)
                        {
                            if (username != "")
                            {
                                this.mhSessionTable.Remove(username);
                                channel.stopChannel();
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.Write(e.Message);
                }
                System.Threading.Thread.Sleep(300);
            }
        }

        private string getAllUser()
        {
            Dictionary<string, string> dictUsers = new Dictionary<string, string>();
            var users = context.Users;
            foreach (var user in users)
            {
                dictUsers.Add(user.Fullname, user.Username);
            }
            string allUser = string.Join(";", dictUsers.Select(x => x.Key + "=" + x.Value).ToArray());
            return allUser;
        }

        /// <summary>
        /// Save file from user to system.
        /// </summary>
        /// <param name="chatId"></param>
        /// <param name="messageTypeId"></param>
        /// <param name="filename"></param>
        private void uploadFile(int chatId, int messageTypeId, string filename)
        {
            Model.Message message = new Model.Message();
            message.ChatId = chatId;
            message.MessageTypeId = messageTypeId;
            message.Content = filename;
            message.Time = DateTime.Now;
            context.Messages.Add(message);
            context.SaveChanges();
        }

        public User getUser(string username)
        {
            User user = null;
            user = context.Users.Where(x => x.Username == username).FirstOrDefault();
            return user;
        }

        /// <summary>
        /// Add new account to system.
        /// </summary>
        /// <param name="fullname"></param>
        /// <param name="username"></param>
        /// <param name="email"></param>
        /// <param name="password"></param>
        private void addUser(string fullname, string username, string email, string password)
        {
            User user = new User()
            {
                Username = username,
                Email = email,
                Password = password,
                Fullname = fullname,
                Time = DateTime.Now
            };
            this.context.Users.Add(user);
            this.context.SaveChanges();
        }

        /// <summary>
        /// Check all field wwhen register new account from system (Fields: Username, Email, Password).
        /// </summary>
        /// <param name="fullname"></param>
        /// <param name="username"></param>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns>return true when have any field empty</returns>
        public bool checkField(string fullname, string username, string email, string password)
        {
            bool result = false;
            if ("".Equals(fullname) | "".Equals(username) | "".Equals(email) | "".Equals(password)) result = true;
            return result;
        }

        /// <summary>
        /// Check email when user request to system.
        /// </summary>
        /// <param name="email"></param>
        /// <returns>true if exist email(Param email)</returns>
        private bool checkExistEmail(string email)
        {
            bool result = false;
            var user = context.Users.Where(x => x.Email == email).FirstOrDefault();
            if (user != null) result = true;
            return result;
        }

        /// <summary>
        /// Check username in system.
        /// </summary>
        /// <param name="username"></param>
        /// <returns>true if exist username(Param username)</returns>
        private bool checkExistUsername(string username)
        {
            bool result = false;
            var user = context.Users.Where(x => x.Username == username).FirstOrDefault();
            if (user != null) result = true;
            return result;
        }

        /// <summary>
        /// Check password for username.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>true if password right.</returns>
        private bool checkPassword(string username, string password)
        {
            var user = context.Users.Where(x => x.Username == username).FirstOrDefault();
            if (password.Equals(user.Password)) return true;
            return false;
        }

        /// <summary>
        /// Enter KeyDown event, action start button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPort_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnStart_Click(sender, e);
            }
        }
    }

    /// <summary>
    /// Class SocketChannel, is a client socket. Use to system send message for user via stream in this socket.
    /// </summary>
    public class SocketChannel : SocketChannelControl
    {
        private TextBox textBox = null;
        private Queue mqRequestQueue = null;

        public SocketChannel(TextBox textBox, Queue queue)
        {
            this.textBox = textBox;
            this.mqRequestQueue = queue;
        }

        public override void eventRecvData(object o)
        {
            ChatDataObject chatDataObject = (ChatDataObject)o;
            String data = DateTime.Now + ":Session from " + chatDataObject.Header.SessionFrom + " with header: " + chatDataObject.Header.Header;

            invokeTextBox(this.textBox, data + "\r\n");
            this.mqRequestQueue.Enqueue(o);
        }

        public void logMonitor(string content)
        {
            invokeTextBox(this.textBox, content + "\r\n");
        }

        private void invokeTextBox(TextBox t, string s)
        {
            if (t.InvokeRequired)
            {
                t.Invoke(new Action<TextBox, string>(invokeTextBox), new Object[] { t, s });
            }
            else
            {
                t.AppendText(s);
            }
        }
    }
}
