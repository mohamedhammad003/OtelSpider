namespace OtelSpider.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RateStructureSupplements : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HotelMealPlans",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MealPlanID = c.Int(nullable: false),
                        HotelID = c.Int(nullable: false),
                        IsBase = c.Boolean(nullable: false),
                        MealPlanSupplement = c.Int(nullable: false),
                        isSubstraction = c.Boolean(nullable: false),
                        isPerPerson = c.Boolean(nullable: false),
                        isPrecentage = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Hotels", t => t.HotelID, cascadeDelete: true)
                .ForeignKey("dbo.MealPlans", t => t.MealPlanID, cascadeDelete: true)
                .Index(t => t.MealPlanID)
                .Index(t => t.HotelID);
            
            AddColumn("dbo.RoomTypes", "MinCapacity", c => c.Int(nullable: false));
            AddColumn("dbo.RoomTypes", "OccupancyRate", c => c.Boolean(nullable: false));
            AddColumn("dbo.RoomTypes", "isPerPerson", c => c.Boolean(nullable: false));
            AddColumn("dbo.RoomTypes", "isSubstraction", c => c.Boolean(nullable: false));
            AddColumn("dbo.RoomTypes", "isPrecentage", c => c.Boolean(nullable: false));
            AddColumn("dbo.HotelOTAs", "IncludeServiceCharge", c => c.Boolean(nullable: false));
            AddColumn("dbo.HotelOTAs", "IncludeCityTax", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HotelMealPlans", "MealPlanID", "dbo.MealPlans");
            DropForeignKey("dbo.HotelMealPlans", "HotelID", "dbo.Hotels");
            DropIndex("dbo.HotelMealPlans", new[] { "HotelID" });
            DropIndex("dbo.HotelMealPlans", new[] { "MealPlanID" });
            DropColumn("dbo.HotelOTAs", "IncludeCityTax");
            DropColumn("dbo.HotelOTAs", "IncludeServiceCharge");
            DropColumn("dbo.RoomTypes", "isPrecentage");
            DropColumn("dbo.RoomTypes", "isSubstraction");
            DropColumn("dbo.RoomTypes", "isPerPerson");
            DropColumn("dbo.RoomTypes", "OccupancyRate");
            DropColumn("dbo.RoomTypes", "MinCapacity");
            DropTable("dbo.HotelMealPlans");
        }
    }
}
