namespace ServerChatsApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v3_chatsapp : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "Chat_Id", "dbo.Chats");
            DropIndex("dbo.Users", new[] { "Chat_Id" });
            DropColumn("dbo.Users", "Chat_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Chat_Id", c => c.Int());
            CreateIndex("dbo.Users", "Chat_Id");
            AddForeignKey("dbo.Users", "Chat_Id", "dbo.Chats", "Id");
        }
    }
}
