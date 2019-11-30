namespace EventManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removedInheritance : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.UserEvents", "Discriminator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserEvents", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
    }
}
