namespace EventManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addrepeatduaration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserEvents", "RepeatDuration", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserEvents", "RepeatDuration");
        }
    }
}
