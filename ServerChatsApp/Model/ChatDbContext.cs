using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerChatsApp.Model
{
    public class ChatDbContext : DbContext
    {
        public ChatDbContext() : base("ChatRoom")
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Chatroom> Chatrooms { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Message> Messages { get; set; }
    }
}
