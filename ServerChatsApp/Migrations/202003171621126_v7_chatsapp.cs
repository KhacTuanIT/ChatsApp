namespace ServerChatsApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v7_chatsapp : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Messages", "ChatId", "dbo.Chats");
            DropIndex("dbo.Messages", new[] { "ChatId" });
            AddColumn("dbo.Messages", "ChatroomName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Messages", "ChatroomName");
            CreateIndex("dbo.Messages", "ChatId");
            AddForeignKey("dbo.Messages", "ChatId", "dbo.Chats", "Id", cascadeDelete: true);
        }
    }
}
