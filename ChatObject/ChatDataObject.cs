﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatObject
{
    [Serializable]
    public class ChatJoinObject
    {
        public string Username { get; set; }
    }

    [Serializable]
    public class ChatDataObject
    {
        public ChatHeaderObject Header { get; set; }
        public ChatPayloadObject Payload { get; set; }
    }

    [Serializable]
    public class ChatHeaderObject
    {
        public string SessionFrom { get; set; }
        public string SessionTo { get; set; }
        public ChatTypeMess ChatType { get; set; }
        public Header Header { get; set; }
    }

    [Serializable]
    public class ChatPayloadObject
    {
        public string Username { get; set; }
        public string Fullname { get; set; }
        public string Filename { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ChatroomName { get; set; }
        public StatusCode StatusCode { get; set; }
        public string Data { get; set; }
        public string MessageType { get; set; }
        public DateTime Time { get; set; }
        public List<TempMessage> TempMessages { get; set; }
    }

    public enum Header
    {
        Side, Message, Join, Quit, Download, Upload, Register, Login, Logout, Forget, Refresh, LoadMessage, GetRoomName, CreateRoom
    }

    public enum ChatTypeMess
    {
        Message, File
    }

    public enum StatusCode
    {
        ExistUsername,
        ExistEmail,
        PasswordWrong,
        CheckTrue,
        MissField,
        MissUsername,
        CreateFail
    }

    public class TempMessage
    {
        public string Username { get; set; }
        public string Fullname { get; set; }
        public string Message { get; set; }
        public ChatTypeMess ChatType { get; set; }
        public string Time { get; set; }
        public string toString()
        {
            return Username + "," + Fullname + "," + Message + "," + ChatType + "," + Time;
        }
    }
}
