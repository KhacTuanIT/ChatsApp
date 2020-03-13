using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerChatsApp.Model
{
    public class Chat
    {
        public int Id { get; set; }
        public int ChatRoomId { get; set; }
        public int UserId { get; set; }
        public int Status { get; set; }

        public Chatroom Chatroom { get; set; }
    }
}
