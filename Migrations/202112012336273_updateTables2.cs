namespace EventAttendance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateTables2 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Members", name: "Id", newName: "MemberId");
            RenameIndex(table: "dbo.Members", name: "IX_Id", newName: "IX_MemberId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Members", name: "IX_MemberId", newName: "IX_Id");
            RenameColumn(table: "dbo.Members", name: "MemberId", newName: "Id");
        }
    }
}
