namespace EventAttendance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Members", "Facebook", c => c.String());
            AddColumn("dbo.Members", "Insta", c => c.String());
            AddColumn("dbo.Members", "Twitter", c => c.String());
            AddColumn("dbo.Members", "Linkedin", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Members", "Linkedin");
            DropColumn("dbo.Members", "Twitter");
            DropColumn("dbo.Members", "Insta");
            DropColumn("dbo.Members", "Facebook");
        }
    }
}
