namespace EventManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedContactName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EventContacts", "ContactName", c => c.String());
            AlterColumn("dbo.UserEvents", "Zipcode", c => c.String(maxLength: 10));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserEvents", "Zipcode", c => c.String(maxLength: 50));
            DropColumn("dbo.EventContacts", "ContactName");
        }
    }
}
