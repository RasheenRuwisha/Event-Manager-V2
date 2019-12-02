namespace EventManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateVaraibleNames : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Contacts", new[] { "Userid" });
            DropIndex("dbo.UserEvents", new[] { "userid" });
            CreateIndex("dbo.Contacts", "UserId");
            CreateIndex("dbo.UserEvents", "UserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.UserEvents", new[] { "UserId" });
            DropIndex("dbo.Contacts", new[] { "UserId" });
            CreateIndex("dbo.UserEvents", "userid");
            CreateIndex("dbo.Contacts", "Userid");
        }
    }
}
