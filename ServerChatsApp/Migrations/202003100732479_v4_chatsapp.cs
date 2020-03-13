namespace ServerChatsApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v4_chatsapp : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Messages", "UserId", "dbo.Users");
            DropIndex("dbo.Messages", new[] { "UserId" });
            AddColumn("dbo.Chatrooms", "Time", c => c.DateTime(nullable: false));
            DropColumn("dbo.Messages", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Messages", "UserId", c => c.Int(nullable: false));
            DropColumn("dbo.Chatrooms", "Time");
            CreateIndex("dbo.Messages", "UserId");
            AddForeignKey("dbo.Messages", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
    }
}
