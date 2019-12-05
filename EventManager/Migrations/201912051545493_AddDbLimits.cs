namespace EventManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDbLimits : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Contacts", "UserId", "dbo.Users");
            DropForeignKey("dbo.EventContacts", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserEvents", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserCredentials", "UserId", "dbo.Users");
            DropIndex("dbo.Contacts", new[] { "UserId" });
            DropIndex("dbo.EventContacts", new[] { "UserId" });
            DropIndex("dbo.UserEvents", new[] { "UserId" });
            DropIndex("dbo.UserCredentials", new[] { "UserId" });
            DropPrimaryKey("dbo.Users");
            DropPrimaryKey("dbo.UserCredentials");
            AddColumn("dbo.Users", "Name", c => c.String(maxLength: 50));
            AddColumn("dbo.Users", "Phone", c => c.String(maxLength: 15));
            AlterColumn("dbo.Contacts", "UserId", c => c.String(maxLength: 10));
            AlterColumn("dbo.Users", "UserId", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.Users", "Email", c => c.String(maxLength: 75));
            AlterColumn("dbo.Users", "Username", c => c.String(maxLength: 30));
            AlterColumn("dbo.EventContacts", "UserId", c => c.String(maxLength: 10));
            AlterColumn("dbo.UserEvents", "UserId", c => c.String(maxLength: 10));
            AlterColumn("dbo.UserCredentials", "Email", c => c.String(nullable: false, maxLength: 75));
            AlterColumn("dbo.UserCredentials", "Username", c => c.String(maxLength: 30));
            AlterColumn("dbo.UserCredentials", "Password", c => c.String(maxLength: 20));
            AlterColumn("dbo.UserCredentials", "UserId", c => c.String(maxLength: 10));
            AddPrimaryKey("dbo.Users", "UserId");
            AddPrimaryKey("dbo.UserCredentials", "Email");
            CreateIndex("dbo.Contacts", "UserId");
            CreateIndex("dbo.EventContacts", "UserId");
            CreateIndex("dbo.UserEvents", "UserId");
            CreateIndex("dbo.UserCredentials", "UserId");
            AddForeignKey("dbo.Contacts", "UserId", "dbo.Users", "UserId");
            AddForeignKey("dbo.EventContacts", "UserId", "dbo.Users", "UserId");
            AddForeignKey("dbo.UserEvents", "UserId", "dbo.Users", "UserId");
            AddForeignKey("dbo.UserCredentials", "UserId", "dbo.Users", "UserId");
            DropColumn("dbo.Users", "Firstname");
            DropColumn("dbo.Users", "Lastname");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Lastname", c => c.String());
            AddColumn("dbo.Users", "Firstname", c => c.String());
            DropForeignKey("dbo.UserCredentials", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserEvents", "UserId", "dbo.Users");
            DropForeignKey("dbo.EventContacts", "UserId", "dbo.Users");
            DropForeignKey("dbo.Contacts", "UserId", "dbo.Users");
            DropIndex("dbo.UserCredentials", new[] { "UserId" });
            DropIndex("dbo.UserEvents", new[] { "UserId" });
            DropIndex("dbo.EventContacts", new[] { "UserId" });
            DropIndex("dbo.Contacts", new[] { "UserId" });
            DropPrimaryKey("dbo.UserCredentials");
            DropPrimaryKey("dbo.Users");
            AlterColumn("dbo.UserCredentials", "UserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.UserCredentials", "Password", c => c.String());
            AlterColumn("dbo.UserCredentials", "Username", c => c.String());
            AlterColumn("dbo.UserCredentials", "Email", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.UserEvents", "UserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.EventContacts", "UserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Users", "Username", c => c.String());
            AlterColumn("dbo.Users", "Email", c => c.String());
            AlterColumn("dbo.Users", "UserId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Contacts", "UserId", c => c.String(maxLength: 128));
            DropColumn("dbo.Users", "Phone");
            DropColumn("dbo.Users", "Name");
            AddPrimaryKey("dbo.UserCredentials", "Email");
            AddPrimaryKey("dbo.Users", "UserId");
            CreateIndex("dbo.UserCredentials", "UserId");
            CreateIndex("dbo.UserEvents", "UserId");
            CreateIndex("dbo.EventContacts", "UserId");
            CreateIndex("dbo.Contacts", "UserId");
            AddForeignKey("dbo.UserCredentials", "UserId", "dbo.Users", "UserId");
            AddForeignKey("dbo.UserEvents", "UserId", "dbo.Users", "UserId");
            AddForeignKey("dbo.EventContacts", "UserId", "dbo.Users", "UserId");
            AddForeignKey("dbo.Contacts", "UserId", "dbo.Users", "UserId");
        }
    }
}
