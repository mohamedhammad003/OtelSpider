namespace OtelSpider.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateOccupancyFormula_RoomMealPlan : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OccupancyFormulas",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RoomTypeID = c.Int(nullable: false),
                        Capacity = c.Int(nullable: false),
                        IsBase = c.Boolean(nullable: false),
                        AdditionalValue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        isSubstraction = c.Boolean(nullable: false),
                        isPrecentage = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.RoomTypes", t => t.RoomTypeID, cascadeDelete: true)
                .Index(t => t.RoomTypeID);
            
            CreateTable(
                "dbo.RoomMealPlans",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RoomTypeID = c.Int(nullable: false),
                        MealPlanID = c.Int(nullable: false),
                        IsBase = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.MealPlans", t => t.MealPlanID, cascadeDelete: true)
                .ForeignKey("dbo.RoomTypes", t => t.RoomTypeID, cascadeDelete: true)
                .Index(t => t.RoomTypeID)
                .Index(t => t.MealPlanID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RoomMealPlans", "RoomTypeID", "dbo.RoomTypes");
            DropForeignKey("dbo.RoomMealPlans", "MealPlanID", "dbo.MealPlans");
            DropForeignKey("dbo.OccupancyFormulas", "RoomTypeID", "dbo.RoomTypes");
            DropIndex("dbo.RoomMealPlans", new[] { "MealPlanID" });
            DropIndex("dbo.RoomMealPlans", new[] { "RoomTypeID" });
            DropIndex("dbo.OccupancyFormulas", new[] { "RoomTypeID" });
            DropTable("dbo.RoomMealPlans");
            DropTable("dbo.OccupancyFormulas");
        }
    }
}
