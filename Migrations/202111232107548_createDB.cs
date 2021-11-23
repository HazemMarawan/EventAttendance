namespace EventAttendance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Organization = c.String(),
                        JobTitle = c.String(),
                        StreetAddress = c.String(),
                        Zip = c.String(),
                        City = c.String(),
                        CountryName = c.String(),
                        Phone = c.String(),
                        Mobile = c.String(),
                        Email = c.String(),
                        HomePage = c.String(),
                        Image = c.String(),
                        active = c.Int(),
                        created_by = c.Int(),
                        updated_by = c.Int(),
                        deleted_by = c.Int(),
                        created_at = c.DateTime(),
                        updated_at = c.DateTime(),
                        deleted_at = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        user_name = c.String(),
                        password = c.String(),
                        image = c.String(),
                        type = c.Int(),
                        active = c.Int(),
                        created_by = c.Int(),
                        created_at = c.DateTime(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
            DropTable("dbo.Members");
        }
    }
}
