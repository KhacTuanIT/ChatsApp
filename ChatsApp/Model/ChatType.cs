using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerChatsApp.Model
{
    public class ChatType
    {
        public int Id { get; set; }
        public string TypeName { get; set; }
        public ICollection<Chatroom> Chatrooms { get; set; }
    }
}
