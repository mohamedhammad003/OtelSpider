using OtelSpider.Core.DAL.Models;
using OtelSpider.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelSpider.Core.BLL.Repositories
{
    public class ReservationRepository : RepositoryBase<MonthlyReservation>, IReservationRepository
    {
        public ReservationRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
        public IEnumerable<MonthlyReservation> GetReservationsByHotel(int hotelId)
        {
            return base.DbContext.Reservations.Where(x => x.HotelID == hotelId);
        }
        public IEnumerable<MonthlyReservation> GetReservationsByOTA(int otaId)
        {
            return base.DbContext.Reservations.Where(x => x.OTAID == otaId);
        }
    }
    public interface IReservationRepository : IRepository<MonthlyReservation>
    {
        IEnumerable<MonthlyReservation> GetReservationsByHotel(int hotelId);
        IEnumerable<MonthlyReservation> GetReservationsByOTA(int otaId);
    }
}
