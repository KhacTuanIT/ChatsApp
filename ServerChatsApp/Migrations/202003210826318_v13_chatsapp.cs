namespace ServerChatsApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v13_chatsapp : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Messages", "MessageType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Messages", "MessageType", c => c.Int(nullable: false));
        }
    }
}
