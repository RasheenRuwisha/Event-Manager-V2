namespace EventManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovePasswordMaxLength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserCredentials", "Password", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserCredentials", "Password", c => c.String(maxLength: 20));
        }
    }
}
