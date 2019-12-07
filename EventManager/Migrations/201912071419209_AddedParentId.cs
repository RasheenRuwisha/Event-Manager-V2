namespace EventManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedParentId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserEvents", "ParentId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserEvents", "ParentId");
        }
    }
}
