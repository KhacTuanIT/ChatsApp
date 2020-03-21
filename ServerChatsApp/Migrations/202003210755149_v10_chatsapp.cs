namespace ServerChatsApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v10_chatsapp : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Chatrooms", "ChatTypeId", "dbo.ChatTypes");
            DropIndex("dbo.Chatrooms", new[] { "ChatTypeId" });
            DropColumn("dbo.Chatrooms", "ChatTypeId");
            DropTable("dbo.ChatTypes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ChatTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TypeName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Chatrooms", "ChatTypeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Chatrooms", "ChatTypeId");
            AddForeignKey("dbo.Chatrooms", "ChatTypeId", "dbo.ChatTypes", "Id", cascadeDelete: true);
        }
    }
}
