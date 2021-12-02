namespace EventAttendance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateTables : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Members");
            AddColumn("dbo.Members", "CreatedBy", c => c.Int());
            AddColumn("dbo.Members", "UpdatedBy", c => c.Int());
            AddColumn("dbo.Members", "DeletedBy", c => c.Int());
            AddColumn("dbo.Members", "CreatedAt", c => c.DateTime());
            AddColumn("dbo.Members", "UpdatedAt", c => c.DateTime());
            AddColumn("dbo.Members", "DeleteAt", c => c.DateTime());
            AddColumn("dbo.Users", "Username", c => c.String());
            AddColumn("dbo.Users", "CreatedBy", c => c.Int());
            AddColumn("dbo.Users", "CreatedAt", c => c.DateTime());
            AlterColumn("dbo.Members", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Members", "Id");
            CreateIndex("dbo.Members", "Id");
            AddForeignKey("dbo.Members", "Id", "dbo.Users", "Id");
            DropColumn("dbo.Members", "Image");
            DropColumn("dbo.Members", "created_by");
            DropColumn("dbo.Members", "updated_by");
            DropColumn("dbo.Members", "deleted_by");
            DropColumn("dbo.Members", "created_at");
            DropColumn("dbo.Members", "updated_at");
            DropColumn("dbo.Members", "deleted_at");
            DropColumn("dbo.Users", "user_name");
            DropColumn("dbo.Users", "created_by");
            DropColumn("dbo.Users", "created_at");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "created_at", c => c.DateTime());
            AddColumn("dbo.Users", "created_by", c => c.Int());
            AddColumn("dbo.Users", "user_name", c => c.String());
            AddColumn("dbo.Members", "deleted_at", c => c.DateTime());
            AddColumn("dbo.Members", "updated_at", c => c.DateTime());
            AddColumn("dbo.Members", "created_at", c => c.DateTime());
            AddColumn("dbo.Members", "deleted_by", c => c.Int());
            AddColumn("dbo.Members", "updated_by", c => c.Int());
            AddColumn("dbo.Members", "created_by", c => c.Int());
            AddColumn("dbo.Members", "Image", c => c.String());
            DropForeignKey("dbo.Members", "Id", "dbo.Users");
            DropIndex("dbo.Members", new[] { "Id" });
            DropPrimaryKey("dbo.Members");
            AlterColumn("dbo.Members", "Id", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.Users", "CreatedAt");
            DropColumn("dbo.Users", "CreatedBy");
            DropColumn("dbo.Users", "Username");
            DropColumn("dbo.Members", "DeleteAt");
            DropColumn("dbo.Members", "UpdatedAt");
            DropColumn("dbo.Members", "CreatedAt");
            DropColumn("dbo.Members", "DeletedBy");
            DropColumn("dbo.Members", "UpdatedBy");
            DropColumn("dbo.Members", "CreatedBy");
            AddPrimaryKey("dbo.Members", "Id");
        }
    }
}
