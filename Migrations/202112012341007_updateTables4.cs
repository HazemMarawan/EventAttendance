namespace EventAttendance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateTables4 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Members", name: "MemberId", newName: "Id");
            RenameIndex(table: "dbo.Members", name: "IX_MemberId", newName: "IX_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Members", name: "IX_Id", newName: "IX_MemberId");
            RenameColumn(table: "dbo.Members", name: "Id", newName: "MemberId");
        }
    }
}
