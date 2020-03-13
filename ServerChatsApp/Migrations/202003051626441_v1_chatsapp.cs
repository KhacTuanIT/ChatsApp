namespace ServerChatsApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v1_chatsapp : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Chatrooms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ChatTypeId = c.Int(nullable: false),
                        RoomName = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ChatTypes", t => t.ChatTypeId, cascadeDelete: true)
                .Index(t => t.ChatTypeId);
            
            CreateTable(
                "dbo.Chats",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ChatRoomId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Chatrooms", t => t.ChatRoomId, cascadeDelete: true)
                .Index(t => t.ChatRoomId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Fullname = c.String(),
                        Username = c.String(),
                        Password = c.String(),
                        Time = c.DateTime(nullable: false),
                        Chat_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Chats", t => t.Chat_Id)
                .Index(t => t.Chat_Id);
            
            CreateTable(
                "dbo.ChatTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TypeName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ChatId = c.Int(nullable: false),
                        MessageTypeId = c.Int(nullable: false),
                        Title = c.String(),
                        Content = c.String(),
                        Time = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Chats", t => t.ChatId, cascadeDelete: true)
                .ForeignKey("dbo.MessageTypes", t => t.MessageTypeId, cascadeDelete: true)
                .Index(t => t.ChatId)
                .Index(t => t.MessageTypeId);
            
            CreateTable(
                "dbo.MessageTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Messages", "MessageTypeId", "dbo.MessageTypes");
            DropForeignKey("dbo.Messages", "ChatId", "dbo.Chats");
            DropForeignKey("dbo.Chatrooms", "ChatTypeId", "dbo.ChatTypes");
            DropForeignKey("dbo.Users", "Chat_Id", "dbo.Chats");
            DropForeignKey("dbo.Chats", "ChatRoomId", "dbo.Chatrooms");
            DropIndex("dbo.Messages", new[] { "MessageTypeId" });
            DropIndex("dbo.Messages", new[] { "ChatId" });
            DropIndex("dbo.Users", new[] { "Chat_Id" });
            DropIndex("dbo.Chats", new[] { "ChatRoomId" });
            DropIndex("dbo.Chatrooms", new[] { "ChatTypeId" });
            DropTable("dbo.MessageTypes");
            DropTable("dbo.Messages");
            DropTable("dbo.ChatTypes");
            DropTable("dbo.Users");
            DropTable("dbo.Chats");
            DropTable("dbo.Chatrooms");
        }
    }
}
