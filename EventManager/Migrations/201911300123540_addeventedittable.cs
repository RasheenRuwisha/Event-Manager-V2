namespace EventManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addeventedittable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EventContacts", "UserEvent_Id", "dbo.UserEvents");
            DropIndex("dbo.EventContacts", new[] { "UserEvent_Id" });
            DropColumn("dbo.EventContacts", "EventId");
            RenameColumn(table: "dbo.EventContacts", name: "UserEvent_Id", newName: "EventId");
            DropPrimaryKey("dbo.UserEvents");
            AddColumn("dbo.UserEvents", "HasExceptions", c => c.String());
            AlterColumn("dbo.UserEvents", "eventid", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.EventContacts", "EventId", c => c.String(maxLength: 128));
            AlterColumn("dbo.EventContacts", "EventId", c => c.String(maxLength: 128));
            AddPrimaryKey("dbo.UserEvents", "eventid");
            CreateIndex("dbo.EventContacts", "EventId");
            AddForeignKey("dbo.EventContacts", "EventId", "dbo.UserEvents", "eventid");
            DropColumn("dbo.UserEvents", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserEvents", "Id", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.EventContacts", "EventId", "dbo.UserEvents");
            DropIndex("dbo.EventContacts", new[] { "EventId" });
            DropPrimaryKey("dbo.UserEvents");
            AlterColumn("dbo.EventContacts", "EventId", c => c.Int());
            AlterColumn("dbo.EventContacts", "EventId", c => c.String());
            AlterColumn("dbo.UserEvents", "eventid", c => c.String());
            DropColumn("dbo.UserEvents", "HasExceptions");
            AddPrimaryKey("dbo.UserEvents", "Id");
            RenameColumn(table: "dbo.EventContacts", name: "EventId", newName: "UserEvent_Id");
            AddColumn("dbo.EventContacts", "EventId", c => c.String());
            CreateIndex("dbo.EventContacts", "UserEvent_Id");
            AddForeignKey("dbo.EventContacts", "UserEvent_Id", "dbo.UserEvents", "Id");
        }
    }
}
