namespace OtelSpider.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RateStructureUpdates : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RoomTypes", "Abbreviation", c => c.String());
            AddColumn("dbo.MealPlans", "Abbreviation", c => c.String(maxLength: 10));
            AddColumn("dbo.RateStructurePeriods", "BackGroundColor", c => c.String());
            DropColumn("dbo.MealPlans", "Apprev");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MealPlans", "Apprev", c => c.String(maxLength: 10));
            DropColumn("dbo.RateStructurePeriods", "BackGroundColor");
            DropColumn("dbo.MealPlans", "Abbreviation");
            DropColumn("dbo.RoomTypes", "Abbreviation");
        }
    }
}
