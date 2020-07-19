using OtelSpider.Core.DAL.Models;
using OtelSpider.Core.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace OtelSpider.Core.BLL.Repositories
{
    public class HotelRepository : RepositoryBase<Hotel>, IHotelRepository
    {
        public HotelRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
        public IEnumerable<UserHotel> GetUserHotels(string systemUserID)
        {
            return DbContext.UserHotels.Where(x => x.UserID == systemUserID);
        }
        public IEnumerable<HotelMarket> GetHotelMarkets(int hotelID)
        {
            return DbContext.HotelMarkets.Where(x => x.HotelID == hotelID);
        }
        public IEnumerable<RoomType> GetHotelRoomTypes(int hotelID)
        {
            return DbContext.RoomTypes.Where(x => x.HotelID == hotelID);
        }
        public IEnumerable<HotelBudget> GetHotelBudgets(int hotelID)
        {
            return DbContext.HotelBudgets.Where(x => x.HotelID == hotelID);
        }
        public IEnumerable<HotelOTA> GetHotelOTAs(int hotelID)
        {
            return DbContext.HotelOTAs.Where(x => x.HotelID == hotelID);
        }
        public IEnumerable<HotelOTA> GetHotelsOTAs(List<int> hotelIds)
        {
            return DbContext.HotelOTAs.Where(x => hotelIds.Contains(x.HotelID));
        }
    }
    public interface IHotelRepository : IRepository<Hotel>
    {
        IEnumerable<UserHotel> GetUserHotels(string systemUserID);
        IEnumerable<HotelMarket> GetHotelMarkets(int hotelID);
        IEnumerable<RoomType> GetHotelRoomTypes(int hotelID);
        IEnumerable<HotelBudget> GetHotelBudgets(int hotelID);
        IEnumerable<HotelOTA> GetHotelOTAs(int hotelID);
        IEnumerable<HotelOTA> GetHotelsOTAs(List<int> hotelIds);
    }
}
