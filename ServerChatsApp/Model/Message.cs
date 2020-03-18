﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerChatsApp.Model
{
    public class Message
    {
        public int Id { get; set; }
        public int ChatId { get; set; }
        public int MessageTypeId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Time { get; set; }

        public string ChatroomName { get; set; }
        public MessageType MessageType { get; set; }
    }
}