namespace EventManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedeventtable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EventContacts", "EventId", "dbo.UserEvents");
            DropIndex("dbo.EventContacts", new[] { "EventId" });
            DropPrimaryKey("dbo.UserEvents");
            AddColumn("dbo.UserEvents", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.EventContacts", "UserEvent_Id", c => c.Int());
            AlterColumn("dbo.UserEvents", "eventid", c => c.String());
            AlterColumn("dbo.EventContacts", "EventId", c => c.String());
            AddPrimaryKey("dbo.UserEvents", "Id");
            CreateIndex("dbo.EventContacts", "UserEvent_Id");
            AddForeignKey("dbo.EventContacts", "UserEvent_Id", "dbo.UserEvents", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EventContacts", "UserEvent_Id", "dbo.UserEvents");
            DropIndex("dbo.EventContacts", new[] { "UserEvent_Id" });
            DropPrimaryKey("dbo.UserEvents");
            AlterColumn("dbo.EventContacts", "EventId", c => c.String(maxLength: 128));
            AlterColumn("dbo.UserEvents", "eventid", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.EventContacts", "UserEvent_Id");
            DropColumn("dbo.UserEvents", "Id");
            AddPrimaryKey("dbo.UserEvents", "eventid");
            CreateIndex("dbo.EventContacts", "EventId");
            AddForeignKey("dbo.EventContacts", "EventId", "dbo.UserEvents", "eventid");
        }
    }
}
