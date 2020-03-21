namespace ServerChatsApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v11_chatsapp : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Messages", "ChatId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Messages", "ChatId", c => c.Int(nullable: false));
        }
    }
}
