namespace EventManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateeventtable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EventDates", "UserEvent_eventid", "dbo.UserEvents");
            DropForeignKey("dbo.MonthStartEnds", "UserEvent_eventid", "dbo.UserEvents");
            DropForeignKey("dbo.WeekStartEnds", "UserEvent_eventid", "dbo.UserEvents");
            DropIndex("dbo.EventDates", new[] { "UserEvent_eventid" });
            DropIndex("dbo.MonthStartEnds", new[] { "UserEvent_eventid" });
            DropIndex("dbo.WeekStartEnds", new[] { "UserEvent_eventid" });
            AddColumn("dbo.UserEvents", "StartDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.UserEvents", "EndDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.UserEvents", "RepeatCount", c => c.Int(nullable: false));
            AddColumn("dbo.UserEvents", "RepeatTill", c => c.DateTime(nullable: false));
            DropTable("dbo.EventDates");
            DropTable("dbo.MonthStartEnds");
            DropTable("dbo.WeekStartEnds");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.WeekStartEnds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        WeekStart = c.DateTime(nullable: false),
                        WeekEnd = c.DateTime(nullable: false),
                        UserEvent_eventid = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MonthStartEnds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MonthStart = c.DateTime(nullable: false),
                        MonthEnd = c.DateTime(nullable: false),
                        UserEvent_eventid = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EventDates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        UserEvent_eventid = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.UserEvents", "RepeatTill");
            DropColumn("dbo.UserEvents", "RepeatCount");
            DropColumn("dbo.UserEvents", "EndDate");
            DropColumn("dbo.UserEvents", "StartDate");
            CreateIndex("dbo.WeekStartEnds", "UserEvent_eventid");
            CreateIndex("dbo.MonthStartEnds", "UserEvent_eventid");
            CreateIndex("dbo.EventDates", "UserEvent_eventid");
            AddForeignKey("dbo.WeekStartEnds", "UserEvent_eventid", "dbo.UserEvents", "eventid");
            AddForeignKey("dbo.MonthStartEnds", "UserEvent_eventid", "dbo.UserEvents", "eventid");
            AddForeignKey("dbo.EventDates", "UserEvent_eventid", "dbo.UserEvents", "eventid");
        }
    }
}
