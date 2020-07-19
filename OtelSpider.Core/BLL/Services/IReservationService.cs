using OtelSpider.Core.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelSpider.Core.BLL.Services
{
    public interface IReservationService
    {
        void CreateMonthlyReservation(MonthlyReservation item);
        void UpdateMonthlyReservation(MonthlyReservation item);
        MonthlyReservation GetReservation(int id);
        IEnumerable<MonthlyReservation> GetReservations();
        IEnumerable<MonthlyReservation> GetReservationByHotel(int hotelId);
        IEnumerable<MonthlyReservation> GetReservationByOTA(int otaId);
        IEnumerable<MonthlyReservation> GetFilteredReservation(int? hotelId, int? otaId, int? year, int? month);

        #region Daily Reservations
        void CreateDailyReservation(DailyReservation item);
        void UpdateDailyReservation(DailyReservation item);
        DailyReservation GetDailyReservation(int id);

        IEnumerable<DailyReservation> GetDailyReservations();

        IEnumerable<DailyReservation> GetDailyReservationByHotel(int hotelId);


        IEnumerable<DailyReservation> GetDailyReservationByOTA(int otaId);
        IEnumerable<DailyReservation> GetFilteredDailyReservation(int? hotelId, int? otaId, int? year, int? month);
        IEnumerable<DailyReservation> GetFilteredDailyReservation(int? hotelId, int? otaId, DateTime? reservationDay);
        #endregion
        void SaveReservation();
    }
}
