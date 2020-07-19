using Microsoft.AspNet.Identity.EntityFramework;
using OtelSpider.Core.DAL.Models;
using System.Data.Entity;

namespace OtelSpider.Core.DAL.Data
{
    public class OtelSpiderContext : IdentityDbContext<SystemUser>
    {
        public OtelSpiderContext() : base("OtelSpiderConnectionString")
        {
            Database.SetInitializer<OtelSpiderContext>(new CreateDatabaseIfNotExists<OtelSpiderContext>());
        }
        public virtual void Commit()
        {
            base.SaveChanges();
        }
        #region Entities        
        public DbSet<AvailableRoomNights> AvailableRoomNights { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<DailyReservation> DailyReservations { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<HotelBudget> HotelBudgets { get; set; }
        public DbSet<HotelParent> HotelParents { get; set; }
        public DbSet<Market> Markets { get; set; }
        public DbSet<MealPlan> MealPlans { get; set; }
        public DbSet<MonthlyReservation> Reservations { get; set; }
        public DbSet<OccupancyFormula> occupancyFormulas { get; set; }
        public DbSet<OTA> OTAs { get; set; }
        public DbSet<RateStructurePeriod> rateStructurePeriods { get; set; }
        public DbSet<RateStructureSetting> RateStructureSettings { get; set; }
        public DbSet<RegisterationToken> RegisterationTokens { get; set; }
        public DbSet<ReservationStatus> ReservationStatuses { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<SingleReservation> SingleReservations { get; set; }
        public DbSet<SystemPermission> SystemPermissions { get; set; }
        public DbSet<UserHotel> UserHotels { get; set; }
        public DbSet<UserPermission> UserPermissions{ get; set; }
        //Bridges
        public DbSet<HotelMarket> HotelMarkets { get; set; }
        public DbSet<HotelMealPlan> HotelMealPlans { get; set; }
        public DbSet<HotelOTA> HotelOTAs { get; set; }
        public DbSet<RoomMealPlan> RoomMealPlans { get; set; }
        //Loggers
        public DbSet<MonthlyReservationLogger> MonthlyReservationLogger { get; set; }
        public DbSet<DailyReservationLogger> DailyReservationLogger { get; set; }
        public DbSet<SingleReservationLogger> SingleReservationLogger { get; set; }

        #endregion
    }
}
