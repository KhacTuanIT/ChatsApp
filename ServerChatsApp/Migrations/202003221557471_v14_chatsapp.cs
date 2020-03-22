namespace ServerChatsApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v14_chatsapp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Chatrooms", "CheckCreate", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Chatrooms", "CheckCreate");
        }
    }
}
