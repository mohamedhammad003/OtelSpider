namespace OtelSpider.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RegisterationToken : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.HotelRoomTypes", "HotelID", "dbo.Hotels");
            DropForeignKey("dbo.HotelRoomTypes", "RoomTypeID", "dbo.RoomTypes");
            DropIndex("dbo.HotelRoomTypes", new[] { "HotelID" });
            DropIndex("dbo.HotelRoomTypes", new[] { "RoomTypeID" });
            AddColumn("dbo.AspNetUsers", "Title", c => c.String());
            AddColumn("dbo.AspNetUsers", "ProfilePic", c => c.String());
            AddColumn("dbo.RoomTypes", "HotelID", c => c.Int(nullable: false));
            AddColumn("dbo.RegisterationTokens", "UserEmail", c => c.String(nullable: false));
            AddColumn("dbo.RegisterationTokens", "SystemRoleID", c => c.Int(nullable: false));
            CreateIndex("dbo.RoomTypes", "HotelID");
            AddForeignKey("dbo.RoomTypes", "HotelID", "dbo.Hotels", "ID");
            DropTable("dbo.HotelRoomTypes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.HotelRoomTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        HotelID = c.Int(nullable: false),
                        RoomTypeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            DropForeignKey("dbo.RoomTypes", "HotelID", "dbo.Hotels");
            DropIndex("dbo.RoomTypes", new[] { "HotelID" });
            DropColumn("dbo.RegisterationTokens", "SystemRoleID");
            DropColumn("dbo.RegisterationTokens", "UserEmail");
            DropColumn("dbo.RoomTypes", "HotelID");
            DropColumn("dbo.AspNetUsers", "ProfilePic");
            DropColumn("dbo.AspNetUsers", "Title");
            CreateIndex("dbo.HotelRoomTypes", "RoomTypeID");
            CreateIndex("dbo.HotelRoomTypes", "HotelID");
            AddForeignKey("dbo.HotelRoomTypes", "RoomTypeID", "dbo.RoomTypes", "ID", cascadeDelete: true);
            AddForeignKey("dbo.HotelRoomTypes", "HotelID", "dbo.Hotels", "ID", cascadeDelete: true);
        }
    }
}
