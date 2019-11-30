namespace EventManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtypecol : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserEvents", "type", c => c.String(maxLength: 15));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserEvents", "type");
        }
    }
}
