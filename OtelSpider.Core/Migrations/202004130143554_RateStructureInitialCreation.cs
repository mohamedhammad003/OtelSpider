namespace OtelSpider.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RateStructureInitialCreation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RateStructurePeriods",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PeriodName = c.String(),
                        HotelID = c.Int(nullable: false),
                        RoomSupplement = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MealSupplement = c.Decimal(nullable: false, precision: 18, scale: 2),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Hotels", t => t.HotelID, cascadeDelete: true)
                .Index(t => t.HotelID);
            
            CreateTable(
                "dbo.RateStructureSettings",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        HotelID = c.Int(nullable: false),
                        BaseRoomTypeID = c.Int(nullable: false),
                        OnlinePrecentage = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BaseCurrencyID = c.Int(nullable: false),
                        ShownCurrencyID = c.Int(nullable: false),
                        CurrencyConversionRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Currencies", t => t.BaseCurrencyID, cascadeDelete: false)
                .ForeignKey("dbo.RoomTypes", t => t.BaseRoomTypeID, cascadeDelete: false)
                .ForeignKey("dbo.Hotels", t => t.HotelID, cascadeDelete: true)
                .ForeignKey("dbo.Currencies", t => t.ShownCurrencyID, cascadeDelete: false)
                .Index(t => t.HotelID)
                .Index(t => t.BaseRoomTypeID)
                .Index(t => t.BaseCurrencyID)
                .Index(t => t.ShownCurrencyID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RateStructureSettings", "ShownCurrencyID", "dbo.Currencies");
            DropForeignKey("dbo.RateStructureSettings", "HotelID", "dbo.Hotels");
            DropForeignKey("dbo.RateStructureSettings", "BaseRoomTypeID", "dbo.RoomTypes");
            DropForeignKey("dbo.RateStructureSettings", "BaseCurrencyID", "dbo.Currencies");
            DropForeignKey("dbo.RateStructurePeriods", "HotelID", "dbo.Hotels");
            DropIndex("dbo.RateStructureSettings", new[] { "ShownCurrencyID" });
            DropIndex("dbo.RateStructureSettings", new[] { "BaseCurrencyID" });
            DropIndex("dbo.RateStructureSettings", new[] { "BaseRoomTypeID" });
            DropIndex("dbo.RateStructureSettings", new[] { "HotelID" });
            DropIndex("dbo.RateStructurePeriods", new[] { "HotelID" });
            DropTable("dbo.RateStructureSettings");
            DropTable("dbo.RateStructurePeriods");
        }
    }
}
