namespace EventAttendance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class whatsapp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Members", "Whatsapp", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Members", "Whatsapp");
        }
    }
}
