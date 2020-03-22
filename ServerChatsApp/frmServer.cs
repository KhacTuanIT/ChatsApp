using ChatObject;
using ServerChatsApp.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
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

        private List<string> userInRoom = null;

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
                    this.txtLog.Text = DateTime.Now + ": Server is starting!" + Environment.NewLine;
                    this.txtPort.Enabled = false;
                    statusRun = true;
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    this.lblState.Text = "Start failure, IP wrong!";
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
            List<MixUserInRoom> userInRooms = new List<MixUserInRoom>();
            bool messageChat = false;
            bool status = true;
            while (this.chatServerControl != null && this.chatServerControl.isConnected())
            {
                try
                {
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
                        else if (chatData.Header.Header == Header.Message)
                        {
                            messageChat = true;
                            if (chatData.Header.ChatType == ChatTypeMess.Message)
                            {
                                int id = getUserId(chatData.Header.SessionFrom);

                                if (id > 0)
                                {
                                    Model.Message message = new Model.Message()
                                    {
                                        ChatroomName = chatData.Header.SessionTo,
                                        Content = chatData.Payload.Data,
                                        UserId = id,
                                        Time = DateTime.Now
                                    };
                                    addMessageToDB(message);
                                }
                            }
                            
                            userInRooms = getUserInRooms(chatData.Header.SessionTo, chatData.Header.SessionFrom);
                        }
                        else if (chatData.Header.Header == Header.LoadMessage)
                        {
                            channel = (SocketChannel)this.mhSessionTable[chatData.Header.SessionFrom];
                            List<TempMessage> messages = GetTempMessages(chatData.Header.SessionTo);

                            chatData.Payload.Data = covertTempMessageToString(messages);
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
                        else if (chatData.Header.Header == Header.GetRoomName)
                        {
                            channel = (SocketChannel)this.mhSessionTable[chatData.Header.SessionFrom];
                            chatData.Payload.ChatroomName = GetRoomName(chatData.Payload.Fullname, chatData.Payload.Data);
                            if (checkUser(chatData.Payload.Data))
                            {
                                addUserToChat(chatData.Payload.ChatroomName, chatData.Payload.Fullname, chatData.Payload.Data);
                            }
                            data = DateTime.Now + ":Session from " + chatData.Header.SessionFrom + " get room name!";
                        }
                        else if (chatData.Header.Header == Header.CreateRoom)
                        {
                            channel = (SocketChannel)this.mhSessionTable[chatData.Header.SessionFrom];
                            if (createNewRoom(chatData.Payload.ChatroomName, chatData.Header.SessionFrom, chatData.Payload.Data))
                            {
                                chatData.Payload.StatusCode = StatusCode.CheckTrue;
                            }
                            else
                            {
                                chatData.Payload.StatusCode = StatusCode.CreateFail;
                            }
                        }
                        if (isQuit)
                        {
                            this.mhSessionTable.Remove(chatData.Header.SessionFrom);
                            isQuit = false;
                            channel.stopChannel();
                        }
                        if (messageChat)
                        { 
                            foreach (MixUserInRoom user in userInRooms)
                            {
                                try
                                {
                                    chatData.Header.SessionTo = user.Username;
                                    chatData.Payload.Fullname = user.Fullname;
                                    chatData.Payload.ChatroomName = user.RoomName;
                                    channel = (SocketChannel)this.mhSessionTable[user.Username];
                                    if (channel != null)
                                    {
                                        channel.sendData(chatData);
                                    }
                                }
                                catch (Exception)
                                {

                                }
                            }
                            messageChat = false;
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
                    Console.Write(e);
                }
                System.Threading.Thread.Sleep(300);
            }
        }

        private List<string> convertStringToList(string usersString)
        {
            List<string> users = usersString.Split(',').ToList();
            return users;
        }

        private List<int> getUsersId(string username, List<string> users)
        {
            List<int> usersId = new List<int>();
            var user = context.Users.Where(x => x.Username == username).FirstOrDefault();
            if (user != null) usersId.Add(user.Id);
            foreach (string fullname in users)
            {
                user = context.Users.Where(x => x.Fullname == fullname).FirstOrDefault();
                if (user != null) usersId.Add(user.Id);
            }
            if (usersId.Count > 0) return usersId;
            return null;
        }

        private bool addUserToChatroom(string roomName, string username, List<string> users)
        {
            List<int> usersId = getUsersId(username, users);
            int roomId = context.Chatrooms.Where(x => x.RoomName == roomName).FirstOrDefault().Id;
            if (usersId != null)
            {
                foreach (int userId in usersId)
                {
                    Chat chat = new Chat()
                    {
                        UserId = userId,
                        ChatRoomId = roomId
                    };
                    context.Chats.Add(chat);
                }
                context.SaveChanges();
                return true;
            }
            return false;
        }

        private bool createNewRoom(string roomName, string username, string usersString)
        {
            List<string> users = convertStringToList(usersString);
            var room = context.Chatrooms.Where(x => x.RoomName == roomName).FirstOrDefault();
            if (room != null) return false;
            Chatroom chatroom = new Chatroom()
            {
                RoomName = roomName,
                CheckCreate = "Create",
                Time = DateTime.Now
            };
            context.Chatrooms.Add(chatroom);
            context.SaveChanges();
            if (addUserToChatroom(roomName, username, users) == false) return false;
            return true;
        }

        private string covertTempMessageToString(List<TempMessage> messages)
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (messages.Count > 0)
            {
                foreach (TempMessage message in messages)
                {
                    stringBuilder.Append(message.toString() + ";");
                }
            }

            return stringBuilder.ToString();
        }

        private int getUserId(string username)
        {
            int id = context.Users.Where(x => x.Username == username).FirstOrDefault().Id;
            return id;
        }

        private void addMessageToDB(Model.Message message)
        {
            if (message != null)
            {
                context.Messages.Add(message);
                context.SaveChanges();
            }
        }

        private bool checkUser(string name)
        {
            var user = context.Users.Where(x => x.Fullname == name).FirstOrDefault();
            return (user == null) ? false : true;
        }

        private string GetRoomName(string fromUser, string toUser)
        {
            var room = context.Chatrooms.Where(x => x.RoomName == toUser).FirstOrDefault();
            if (room != null) return toUser;
            string roomName = fromUser + toUser;
            room = context.Chatrooms.Where(x => x.RoomName == roomName).FirstOrDefault();
            if (room != null) return roomName;
            roomName = toUser + fromUser;
            room = context.Chatrooms.Where(x => x.RoomName == roomName).FirstOrDefault();
            if (room != null) return roomName;
            Chatroom chatroom = new Chatroom() {
                RoomName = fromUser + toUser,
                Time = DateTime.Now
            };
            context.Chatrooms.Add(chatroom);
            context.SaveChanges();
            System.Threading.Thread.Sleep(100);
            return fromUser + toUser;
        }

        private void addUserToChat(string roomName, string fromUser, string toUser)
        {
            int roomId = context.Chatrooms.Where(x => x.RoomName == roomName).FirstOrDefault().Id;
            int fromId = context.Users.Where(x => x.Fullname == fromUser).FirstOrDefault().Id;
            int toId = context.Users.Where(x => x.Fullname == toUser).FirstOrDefault().Id;
            var chat = context.Chats.Where(x => x.ChatRoomId == roomId).Where(x => x.UserId == fromId).FirstOrDefault();

            if (chat == null) {
                Chat chatFrom = new Chat()
                {
                    ChatRoomId = roomId,
                    UserId = fromId
                };
                Chat chatTo = new Chat()
                {
                    ChatRoomId = roomId,
                    UserId = toId
                };
                context.Chats.Add(chatFrom);
                context.Chats.Add(chatTo);
                context.SaveChanges();
            }
        }

        private List<TempMessage> GetTempMessages(string roomName)
        {
            List<TempMessage> tempMessages = new List<TempMessage>();

            var messages = context.Messages.Where(x => x.ChatroomName == roomName).ToList();
            System.Threading.Thread.Sleep(200);
            tempMessages = getMess(messages);

            if (tempMessages.Count > 0) return tempMessages;
            return null;
        }

        private List<TempMessage> getMess(List<Model.Message> messages)
        {
            List<TempMessage> tempMessages = new List<TempMessage>();
            if (messages.Count > 0)
            {
                foreach (Model.Message message in messages)
                {
                    var user = context.Users.Where(x => x.Id == message.UserId).FirstOrDefault();
                    TempMessage temp = new TempMessage()
                    {
                        ChatType = message.ChatType,
                        Fullname = user.Fullname,
                        Message = message.Content,
                        Username = user.Username,
                        Time = message.Time.ToShortTimeString()
                    };
                    tempMessages.Add(temp);
                }
            }
            return tempMessages;
        }

        private List<MixUserInRoom> getUserInRooms(string roomName, string fromName)
        {
            List<MixUserInRoom> userInRooms = new List<MixUserInRoom>();
            var idRoom = context.Chatrooms.Where(x => x.RoomName == roomName).FirstOrDefault().Id;
            User fromUser = context.Users.Where(x => x.Username == fromName).FirstOrDefault();
            if (idRoom > 0)
            {
                var userIdInRooms = context.Chats.Where(x => x.ChatRoomId == idRoom);
                if (userIdInRooms != null)
                {
                    foreach (var userId in userIdInRooms)
                    {
                        var user = context.Users.Where(x => x.Id == userId.UserId).FirstOrDefault();
                        if (user != null && user.Username != fromName)
                        {
                            MixUserInRoom mixUser = new MixUserInRoom()
                            {
                                Username = user.Username,
                                Fullname = fromUser.Fullname,
                                RoomName = roomName
                            };
                            userInRooms.Add(mixUser);
                        }
                    }
                }
            }

            if (userInRooms.Count > 0) return userInRooms;
            return null;
        }

        private List<string> getAllUserInRoom(string roomName)
        {
            List<string> allUserInRoom;
            int roomId = context.Chatrooms.Where(x => x.RoomName == roomName).FirstOrDefault().Id;
            if (roomId <= 0) return null;
            var idUsers = context.Chats.Where(x => x.ChatRoomId == roomId);
            if (idUsers != null)
            {
                allUserInRoom = new List<string>();
                foreach (var user in idUsers)
                {
                    string username = context.Users.Where(x => x.Id == user.Id).FirstOrDefault().Username;
                    allUserInRoom.Add(username);
                }

                if (allUserInRoom.Count > 0) return allUserInRoom;
            }
            return null;
        }

        private string getAllUser()
        {
            Dictionary<string, string> dictUsers = new Dictionary<string, string>();
            var chatrooms = context.Chatrooms.Where(x => x.CheckCreate == "Create");
            var users = context.Users;
            foreach (var user in users)
            {
                dictUsers.Add(user.Fullname, user.Username);
            }
            foreach (var chatroom in chatrooms)
            {
                dictUsers.Add(chatroom.RoomName, "room");
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
            message.Content = filename;
            message.ChatType = ChatTypeMess.File;
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
            User user = null;
            try
            {
                user = context.Users.Where(x => x.Email == email).FirstOrDefault();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
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

        private void txtIPServer_Click(object sender, EventArgs e)
        {

        }
    }

    //public class TempMessage
    //{
    //    public string Username { get; set; }
    //    public string Fullname { get; set; }
    //    public string Message { get; set; }
    //    public string ChatType { get; set; }
    //    public string Time { get; set; }
    //}

    public class MixUserInRoom
    {
        public string RoomName { get; set; }
        public string Username { get; set; }
        public string Fullname { get; set; }
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
