namespace EventManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EventContacs : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        Contactid = c.String(nullable: false, maxLength: 128),
                        Email = c.String(),
                        Image = c.String(),
                        Name = c.String(maxLength: 50),
                        AddressLine1 = c.String(maxLength: 50),
                        AddressLine2 = c.String(maxLength: 50),
                        City = c.String(maxLength: 50),
                        State = c.String(maxLength: 50),
                        Zipcode = c.String(maxLength: 50),
                        Userid = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Contactid)
                .ForeignKey("dbo.Users", t => t.Userid)
                .Index(t => t.Userid);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        Email = c.String(),
                        Image = c.String(),
                        Username = c.String(),
                        Firstname = c.String(),
                        Lastname = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.UserEvents",
                c => new
                    {
                        eventid = c.String(nullable: false, maxLength: 128),
                        userid = c.String(maxLength: 128),
                        title = c.String(maxLength: 100),
                        description = c.String(maxLength: 500),
                        repeatType = c.String(),
                        AddressLine1 = c.String(maxLength: 50),
                        AddressLine2 = c.String(maxLength: 50),
                        City = c.String(maxLength: 50),
                        State = c.String(maxLength: 50),
                        Zipcode = c.String(maxLength: 50),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.eventid)
                .ForeignKey("dbo.Users", t => t.userid)
                .Index(t => t.userid);
            
            CreateTable(
                "dbo.EventContacts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        ContactId = c.String(maxLength: 128),
                        EventId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contacts", t => t.ContactId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .ForeignKey("dbo.UserEvents", t => t.EventId)
                .Index(t => t.UserId)
                .Index(t => t.ContactId)
                .Index(t => t.EventId);
            
            CreateTable(
                "dbo.EventDates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        UserEvent_eventid = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserEvents", t => t.UserEvent_eventid)
                .Index(t => t.UserEvent_eventid);
            
            CreateTable(
                "dbo.MonthStartEnds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MonthStart = c.DateTime(nullable: false),
                        MonthEnd = c.DateTime(nullable: false),
                        UserEvent_eventid = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserEvents", t => t.UserEvent_eventid)
                .Index(t => t.UserEvent_eventid);
            
            CreateTable(
                "dbo.WeekStartEnds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        WeekStart = c.DateTime(nullable: false),
                        WeekEnd = c.DateTime(nullable: false),
                        UserEvent_eventid = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserEvents", t => t.UserEvent_eventid)
                .Index(t => t.UserEvent_eventid);
            
            CreateTable(
                "dbo.UserCredentials",
                c => new
                    {
                        Email = c.String(nullable: false, maxLength: 128),
                        Username = c.String(),
                        Password = c.String(),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Email)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserCredentials", "UserId", "dbo.Users");
            DropForeignKey("dbo.WeekStartEnds", "UserEvent_eventid", "dbo.UserEvents");
            DropForeignKey("dbo.UserEvents", "userid", "dbo.Users");
            DropForeignKey("dbo.MonthStartEnds", "UserEvent_eventid", "dbo.UserEvents");
            DropForeignKey("dbo.EventDates", "UserEvent_eventid", "dbo.UserEvents");
            DropForeignKey("dbo.EventContacts", "EventId", "dbo.UserEvents");
            DropForeignKey("dbo.EventContacts", "UserId", "dbo.Users");
            DropForeignKey("dbo.EventContacts", "ContactId", "dbo.Contacts");
            DropForeignKey("dbo.Contacts", "Userid", "dbo.Users");
            DropIndex("dbo.UserCredentials", new[] { "UserId" });
            DropIndex("dbo.WeekStartEnds", new[] { "UserEvent_eventid" });
            DropIndex("dbo.MonthStartEnds", new[] { "UserEvent_eventid" });
            DropIndex("dbo.EventDates", new[] { "UserEvent_eventid" });
            DropIndex("dbo.EventContacts", new[] { "EventId" });
            DropIndex("dbo.EventContacts", new[] { "ContactId" });
            DropIndex("dbo.EventContacts", new[] { "UserId" });
            DropIndex("dbo.UserEvents", new[] { "userid" });
            DropIndex("dbo.Contacts", new[] { "Userid" });
            DropTable("dbo.UserCredentials");
            DropTable("dbo.WeekStartEnds");
            DropTable("dbo.MonthStartEnds");
            DropTable("dbo.EventDates");
            DropTable("dbo.EventContacts");
            DropTable("dbo.UserEvents");
            DropTable("dbo.Users");
            DropTable("dbo.Contacts");
        }
    }
}
