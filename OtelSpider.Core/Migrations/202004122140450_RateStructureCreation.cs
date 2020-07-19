namespace OtelSpider.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RateStructureCreation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserPermissions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemUserID = c.String(maxLength: 128),
                        PermissionID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.SystemPermissions", t => t.PermissionID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.SystemUserID)
                .Index(t => t.SystemUserID)
                .Index(t => t.PermissionID);
            
            CreateTable(
                "dbo.SystemPermissions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PermissionName = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.RoomTypes", "RoomCapacity", c => c.Int(nullable: false));
            AddColumn("dbo.RoomTypes", "IsBase", c => c.Boolean(nullable: false));
            AddColumn("dbo.RoomTypes", "AdditionValue", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserPermissions", "SystemUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserPermissions", "PermissionID", "dbo.SystemPermissions");
            DropIndex("dbo.UserPermissions", new[] { "PermissionID" });
            DropIndex("dbo.UserPermissions", new[] { "SystemUserID" });
            DropColumn("dbo.RoomTypes", "AdditionValue");
            DropColumn("dbo.RoomTypes", "IsBase");
            DropColumn("dbo.RoomTypes", "RoomCapacity");
            DropTable("dbo.SystemPermissions");
            DropTable("dbo.UserPermissions");
        }
    }
}
