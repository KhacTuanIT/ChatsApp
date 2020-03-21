namespace ServerChatsApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v9_chatsapp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Messages", "ChatType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Messages", "ChatType");
        }
    }
}
