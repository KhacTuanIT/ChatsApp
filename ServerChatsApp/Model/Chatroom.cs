using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerChatsApp.Model
{
    public class Chatroom
    {
        public int Id { get; set; }
        public string RoomName { get; set; }
        public DateTime Time { get; set; }
        public string CheckCreate { get; set; }
        public ICollection<Chat> Chats { get; set; }
    }
}
