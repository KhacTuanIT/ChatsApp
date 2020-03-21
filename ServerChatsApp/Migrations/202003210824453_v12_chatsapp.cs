namespace ServerChatsApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v12_chatsapp : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Messages", "MessageTypeId", "dbo.MessageTypes");
            DropIndex("dbo.Messages", new[] { "MessageTypeId" });
            AddColumn("dbo.Messages", "MessageType", c => c.Int(nullable: false));
            DropColumn("dbo.Messages", "MessageTypeId");
            DropTable("dbo.MessageTypes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.MessageTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Messages", "MessageTypeId", c => c.Int(nullable: false));
            DropColumn("dbo.Messages", "MessageType");
            CreateIndex("dbo.Messages", "MessageTypeId");
            AddForeignKey("dbo.Messages", "MessageTypeId", "dbo.MessageTypes", "Id", cascadeDelete: true);
        }
    }
}
