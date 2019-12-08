namespace EventManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedMaxLengths : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Contacts", "Email", c => c.String(maxLength: 75));
            AlterColumn("dbo.Contacts", "Phone", c => c.String(maxLength: 10));
            AlterColumn("dbo.Users", "Phone", c => c.String(maxLength: 10));
            AlterColumn("dbo.UserEvents", "ParentId", c => c.String(maxLength: 10));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserEvents", "ParentId", c => c.String());
            AlterColumn("dbo.Users", "Phone", c => c.String(maxLength: 15));
            AlterColumn("dbo.Contacts", "Phone", c => c.String());
            AlterColumn("dbo.Contacts", "Email", c => c.String());
        }
    }
}
