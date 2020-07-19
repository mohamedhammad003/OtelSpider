namespace OtelSpider.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AvailableRoomNights",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Year = c.Int(nullable: false),
                        January = c.Int(nullable: false),
                        February = c.Int(nullable: false),
                        March = c.Int(nullable: false),
                        April = c.Int(nullable: false),
                        May = c.Int(nullable: false),
                        June = c.Int(nullable: false),
                        July = c.Int(nullable: false),
                        August = c.Int(nullable: false),
                        September = c.Int(nullable: false),
                        October = c.Int(nullable: false),
                        November = c.Int(nullable: false),
                        December = c.Int(nullable: false),
                        HotelID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Hotels", t => t.HotelID, cascadeDelete: true)
                .Index(t => t.HotelID);
            
            CreateTable(
                "dbo.Hotels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 250),
                        WebURL = c.String(),
                        ParentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.HotelParents", t => t.ParentID, cascadeDelete: true)
                .Index(t => t.ParentID);
            
            CreateTable(
                "dbo.DailyReservations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        HotelID = c.Int(nullable: false),
                        OTAID = c.Int(nullable: false),
                        ReservationDay = c.DateTime(nullable: false),
                        MonthLetters = c.String(maxLength: 50),
                        RoomNights = c.Int(nullable: false),
                        GrossRoomRevenue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        GrossARR = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NetRoomRevenue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NetARR = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreatedByID = c.String(maxLength: 128),
                        UpdatedByID = c.String(maxLength: 128),
                        CreationDate = c.DateTime(nullable: false),
                        LastUpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedByID)
                .ForeignKey("dbo.Hotels", t => t.HotelID, cascadeDelete: true)
                .ForeignKey("dbo.OTAs", t => t.OTAID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedByID)
                .Index(t => t.HotelID)
                .Index(t => t.OTAID)
                .Index(t => t.CreatedByID)
                .Index(t => t.UpdatedByID);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(nullable: false, maxLength: 100),
                        LastName = c.String(maxLength: 100),
                        SystemRoleID = c.Int(nullable: false),
                        isAdmin = c.Boolean(nullable: false),
                        isActive = c.Boolean(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.MonthlyReservations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        HotelID = c.Int(nullable: false),
                        OTAID = c.Int(nullable: false),
                        Year = c.Int(nullable: false),
                        Month = c.Int(nullable: false),
                        MonthName = c.String(maxLength: 50),
                        RoomNights = c.Int(nullable: false),
                        GrossRoomRevenue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        GrossARR = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NetRoomRevenue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NetARR = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreatedByID = c.String(maxLength: 128),
                        UpdatedByID = c.String(maxLength: 128),
                        CreationDate = c.DateTime(nullable: false),
                        LastUpdateDate = c.DateTime(),
                        SystemUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedByID)
                .ForeignKey("dbo.Hotels", t => t.HotelID, cascadeDelete: true)
                .ForeignKey("dbo.OTAs", t => t.OTAID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedByID)
                .ForeignKey("dbo.AspNetUsers", t => t.SystemUser_Id)
                .Index(t => t.HotelID)
                .Index(t => t.OTAID)
                .Index(t => t.CreatedByID)
                .Index(t => t.UpdatedByID)
                .Index(t => t.SystemUser_Id);
            
            CreateTable(
                "dbo.OTAs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 250),
                        WebURL = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.UserHotels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.String(maxLength: 128),
                        HotelID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Hotels", t => t.HotelID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.UserID)
                .Index(t => t.HotelID);
            
            CreateTable(
                "dbo.HotelMarkets",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        HotelID = c.Int(nullable: false),
                        MarketID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Hotels", t => t.HotelID, cascadeDelete: true)
                .ForeignKey("dbo.Markets", t => t.MarketID, cascadeDelete: true)
                .Index(t => t.HotelID)
                .Index(t => t.MarketID);
            
            CreateTable(
                "dbo.Markets",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 150),
                        CurrencyID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Currencies", t => t.CurrencyID, cascadeDelete: true)
                .Index(t => t.CurrencyID);
            
            CreateTable(
                "dbo.Currencies",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                        CurrencyCode = c.String(maxLength: 5),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.HotelParents",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 250),
                        WebURL = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.SingleReservations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ReservationRefID = c.String(maxLength: 250),
                        VisitorName = c.String(maxLength: 250),
                        VisitorEmail = c.String(maxLength: 250),
                        PhoneNum = c.String(maxLength: 100),
                        Nationality = c.String(maxLength: 100),
                        CheckinDate = c.DateTime(nullable: false),
                        CheckoutDate = c.DateTime(),
                        NightsNum = c.Int(nullable: false),
                        PromoCode = c.String(maxLength: 250),
                        Discount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RoomPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ReservationType = c.Int(nullable: false),
                        RoomTypeID = c.Int(nullable: false),
                        HotelID = c.Int(nullable: false),
                        OTAID = c.Int(nullable: false),
                        MealPlanID = c.Int(nullable: false),
                        StatusID = c.Int(nullable: false),
                        CurrencyID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Currencies", t => t.CurrencyID)
                .ForeignKey("dbo.Hotels", t => t.HotelID, cascadeDelete: true)
                .ForeignKey("dbo.MealPlans", t => t.MealPlanID, cascadeDelete: true)
                .ForeignKey("dbo.OTAs", t => t.OTAID, cascadeDelete: true)
                .ForeignKey("dbo.RoomTypes", t => t.RoomTypeID, cascadeDelete: true)
                .ForeignKey("dbo.ReservationStatus", t => t.StatusID, cascadeDelete: true)
                .Index(t => t.RoomTypeID)
                .Index(t => t.HotelID)
                .Index(t => t.OTAID)
                .Index(t => t.MealPlanID)
                .Index(t => t.StatusID)
                .Index(t => t.CurrencyID);
            
            CreateTable(
                "dbo.MealPlans",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 250),
                        Apprev = c.String(maxLength: 10),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.RoomTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Type = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.HotelRoomTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        HotelID = c.Int(nullable: false),
                        RoomTypeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Hotels", t => t.HotelID, cascadeDelete: true)
                .ForeignKey("dbo.RoomTypes", t => t.RoomTypeID, cascadeDelete: true)
                .Index(t => t.HotelID)
                .Index(t => t.RoomTypeID);
            
            CreateTable(
                "dbo.ReservationStatus",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Status = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.DailyReservationLoggers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DailyReservationID = c.Int(nullable: false),
                        HotelID = c.Int(nullable: false),
                        OTAID = c.Int(nullable: false),
                        ReservationDay = c.DateTime(nullable: false),
                        MonthLetters = c.String(maxLength: 50),
                        RoomNights = c.Int(nullable: false),
                        GrossRoomRevenue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        GrossARR = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NetRoomRevenue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NetARR = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreatedByID = c.String(),
                        UpdatedByID = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.HotelBudgets",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Year = c.Int(nullable: false),
                        January = c.Decimal(nullable: false, precision: 18, scale: 2),
                        February = c.Decimal(nullable: false, precision: 18, scale: 2),
                        March = c.Decimal(nullable: false, precision: 18, scale: 2),
                        April = c.Decimal(nullable: false, precision: 18, scale: 2),
                        May = c.Decimal(nullable: false, precision: 18, scale: 2),
                        June = c.Decimal(nullable: false, precision: 18, scale: 2),
                        July = c.Decimal(nullable: false, precision: 18, scale: 2),
                        August = c.Decimal(nullable: false, precision: 18, scale: 2),
                        September = c.Decimal(nullable: false, precision: 18, scale: 2),
                        October = c.Decimal(nullable: false, precision: 18, scale: 2),
                        November = c.Decimal(nullable: false, precision: 18, scale: 2),
                        December = c.Decimal(nullable: false, precision: 18, scale: 2),
                        HotelID = c.Int(nullable: false),
                        CurrencyID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Currencies", t => t.CurrencyID, cascadeDelete: true)
                .ForeignKey("dbo.Hotels", t => t.HotelID, cascadeDelete: true)
                .Index(t => t.HotelID)
                .Index(t => t.CurrencyID);
            
            CreateTable(
                "dbo.HotelOTAs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        HotelID = c.Int(nullable: false),
                        OTAID = c.Int(nullable: false),
                        CommissionPercentage = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IncludeVAT = c.Boolean(nullable: false),
                        ExtranetURL = c.String(),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Hotels", t => t.HotelID, cascadeDelete: true)
                .ForeignKey("dbo.OTAs", t => t.OTAID, cascadeDelete: true)
                .Index(t => t.HotelID)
                .Index(t => t.OTAID);
            
            CreateTable(
                "dbo.MonthlyReservationLoggers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MonthlyReservationID = c.Int(nullable: false),
                        HotelID = c.Int(nullable: false),
                        OTAID = c.Int(nullable: false),
                        Year = c.Int(nullable: false),
                        Month = c.Int(nullable: false),
                        MonthLetters = c.String(maxLength: 50),
                        RoomNights = c.Int(nullable: false),
                        GrossRoomRevenue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        GrossARR = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NetRoomRevenue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NetARR = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreatedByID = c.String(),
                        UpdatedByID = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.RegisterationTokens",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Token = c.String(maxLength: 150),
                        RegisterLink = c.String(),
                        TokenExpires = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.SingleReservationLoggers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ReservationRefID = c.String(maxLength: 250),
                        VisitorName = c.String(maxLength: 250),
                        VisitorEmail = c.String(maxLength: 250),
                        PhoneNum = c.String(maxLength: 100),
                        Nationality = c.String(maxLength: 100),
                        CheckinDate = c.DateTime(nullable: false),
                        CheckoutDate = c.DateTime(),
                        NightsNum = c.Int(nullable: false),
                        PromoCode = c.String(maxLength: 250),
                        Discount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RoomPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ReservationType = c.Int(nullable: false),
                        RoomTypeID = c.Int(nullable: false),
                        HotelID = c.Int(nullable: false),
                        OTAID = c.Int(nullable: false),
                        MealPlanID = c.Int(nullable: false),
                        StatusID = c.Int(nullable: false),
                        CurrencyID = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.HotelOTAs", "OTAID", "dbo.OTAs");
            DropForeignKey("dbo.HotelOTAs", "HotelID", "dbo.Hotels");
            DropForeignKey("dbo.HotelBudgets", "HotelID", "dbo.Hotels");
            DropForeignKey("dbo.HotelBudgets", "CurrencyID", "dbo.Currencies");
            DropForeignKey("dbo.AvailableRoomNights", "HotelID", "dbo.Hotels");
            DropForeignKey("dbo.SingleReservations", "StatusID", "dbo.ReservationStatus");
            DropForeignKey("dbo.SingleReservations", "RoomTypeID", "dbo.RoomTypes");
            DropForeignKey("dbo.HotelRoomTypes", "RoomTypeID", "dbo.RoomTypes");
            DropForeignKey("dbo.HotelRoomTypes", "HotelID", "dbo.Hotels");
            DropForeignKey("dbo.SingleReservations", "OTAID", "dbo.OTAs");
            DropForeignKey("dbo.SingleReservations", "MealPlanID", "dbo.MealPlans");
            DropForeignKey("dbo.SingleReservations", "HotelID", "dbo.Hotels");
            DropForeignKey("dbo.SingleReservations", "CurrencyID", "dbo.Currencies");
            DropForeignKey("dbo.Hotels", "ParentID", "dbo.HotelParents");
            DropForeignKey("dbo.HotelMarkets", "MarketID", "dbo.Markets");
            DropForeignKey("dbo.Markets", "CurrencyID", "dbo.Currencies");
            DropForeignKey("dbo.HotelMarkets", "HotelID", "dbo.Hotels");
            DropForeignKey("dbo.DailyReservations", "UpdatedByID", "dbo.AspNetUsers");
            DropForeignKey("dbo.DailyReservations", "OTAID", "dbo.OTAs");
            DropForeignKey("dbo.DailyReservations", "HotelID", "dbo.Hotels");
            DropForeignKey("dbo.DailyReservations", "CreatedByID", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserHotels", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserHotels", "HotelID", "dbo.Hotels");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.MonthlyReservations", "SystemUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.MonthlyReservations", "UpdatedByID", "dbo.AspNetUsers");
            DropForeignKey("dbo.MonthlyReservations", "OTAID", "dbo.OTAs");
            DropForeignKey("dbo.MonthlyReservations", "HotelID", "dbo.Hotels");
            DropForeignKey("dbo.MonthlyReservations", "CreatedByID", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.HotelOTAs", new[] { "OTAID" });
            DropIndex("dbo.HotelOTAs", new[] { "HotelID" });
            DropIndex("dbo.HotelBudgets", new[] { "CurrencyID" });
            DropIndex("dbo.HotelBudgets", new[] { "HotelID" });
            DropIndex("dbo.HotelRoomTypes", new[] { "RoomTypeID" });
            DropIndex("dbo.HotelRoomTypes", new[] { "HotelID" });
            DropIndex("dbo.SingleReservations", new[] { "CurrencyID" });
            DropIndex("dbo.SingleReservations", new[] { "StatusID" });
            DropIndex("dbo.SingleReservations", new[] { "MealPlanID" });
            DropIndex("dbo.SingleReservations", new[] { "OTAID" });
            DropIndex("dbo.SingleReservations", new[] { "HotelID" });
            DropIndex("dbo.SingleReservations", new[] { "RoomTypeID" });
            DropIndex("dbo.Markets", new[] { "CurrencyID" });
            DropIndex("dbo.HotelMarkets", new[] { "MarketID" });
            DropIndex("dbo.HotelMarkets", new[] { "HotelID" });
            DropIndex("dbo.UserHotels", new[] { "HotelID" });
            DropIndex("dbo.UserHotels", new[] { "UserID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.MonthlyReservations", new[] { "SystemUser_Id" });
            DropIndex("dbo.MonthlyReservations", new[] { "UpdatedByID" });
            DropIndex("dbo.MonthlyReservations", new[] { "CreatedByID" });
            DropIndex("dbo.MonthlyReservations", new[] { "OTAID" });
            DropIndex("dbo.MonthlyReservations", new[] { "HotelID" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.DailyReservations", new[] { "UpdatedByID" });
            DropIndex("dbo.DailyReservations", new[] { "CreatedByID" });
            DropIndex("dbo.DailyReservations", new[] { "OTAID" });
            DropIndex("dbo.DailyReservations", new[] { "HotelID" });
            DropIndex("dbo.Hotels", new[] { "ParentID" });
            DropIndex("dbo.AvailableRoomNights", new[] { "HotelID" });
            DropTable("dbo.SingleReservationLoggers");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.RegisterationTokens");
            DropTable("dbo.MonthlyReservationLoggers");
            DropTable("dbo.HotelOTAs");
            DropTable("dbo.HotelBudgets");
            DropTable("dbo.DailyReservationLoggers");
            DropTable("dbo.ReservationStatus");
            DropTable("dbo.HotelRoomTypes");
            DropTable("dbo.RoomTypes");
            DropTable("dbo.MealPlans");
            DropTable("dbo.SingleReservations");
            DropTable("dbo.HotelParents");
            DropTable("dbo.Currencies");
            DropTable("dbo.Markets");
            DropTable("dbo.HotelMarkets");
            DropTable("dbo.UserHotels");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.OTAs");
            DropTable("dbo.MonthlyReservations");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.DailyReservations");
            DropTable("dbo.Hotels");
            DropTable("dbo.AvailableRoomNights");
        }
    }
}
