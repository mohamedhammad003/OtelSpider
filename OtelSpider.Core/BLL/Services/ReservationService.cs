using OtelSpider.Core.BLL.Repositories;
using OtelSpider.Core.DAL.Models;
using OtelSpider.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelSpider.Core.BLL.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository reservationRepository;
        private readonly IDailyReservationRepository dailyReservationRepository;
        private readonly IUnitOfWork unitOfWork;
        public ReservationService(IReservationRepository reservationRepository, IDailyReservationRepository dailyReservationRepository, IUnitOfWork unitOfWork)
        {
            this.reservationRepository = reservationRepository;
            this.dailyReservationRepository = dailyReservationRepository;
            this.unitOfWork = unitOfWork;
        }
        #region Monthly Reservations
        public void CreateMonthlyReservation(MonthlyReservation item)
        {
            reservationRepository.Add(item);
        }
        public void UpdateMonthlyReservation(MonthlyReservation item)
        {
            reservationRepository.Update(item);
        }
        public MonthlyReservation GetReservation(int id)
        {
            return reservationRepository.Get(x => x.ID == id);
        }

        public IEnumerable<MonthlyReservation> GetReservations()
        {
            return reservationRepository.GetMany(r=> true);
        }

        public IEnumerable<MonthlyReservation> GetReservationByHotel(int hotelId)
        {
            return reservationRepository.GetReservationsByHotel(hotelId);
        }
        

        public IEnumerable<MonthlyReservation> GetReservationByOTA(int otaId)
        {
            return reservationRepository.GetReservationsByOTA(otaId);
        }
        public IEnumerable<MonthlyReservation> GetFilteredReservation(int? hotelId, int? otaId, int? year, int? month)
        {
            return reservationRepository.GetMany(r => (!hotelId.HasValue || r.HotelID == hotelId.Value) && (!otaId.HasValue || r.OTAID == otaId.Value) 
                        && (!year.HasValue || r.Year == year.Value) && (!month.HasValue || r.Month == month.Value));
        }
        #endregion

        #region Daily Reservations
        public void CreateDailyReservation(DailyReservation item)
        {
            dailyReservationRepository.Add(item);
        }
        public void UpdateDailyReservation(DailyReservation item)
        {
            dailyReservationRepository.Update(item);
        }
        public DailyReservation GetDailyReservation(int id)
        {
            return dailyReservationRepository.Get(x => x.ID == id);
        }

        public IEnumerable<DailyReservation> GetDailyReservations()
        {
            return dailyReservationRepository.GetMany(r => true);
        }

        public IEnumerable<DailyReservation> GetDailyReservationByHotel(int hotelId)
        {
            return dailyReservationRepository.GetMany(d => d.HotelID == hotelId);
        }


        public IEnumerable<DailyReservation> GetDailyReservationByOTA(int otaId)
        {
            return dailyReservationRepository.GetMany(d => d.OTAID == otaId);
        }
        public IEnumerable<DailyReservation> GetFilteredDailyReservation(int? hotelId, int? otaId, int? year, int? month)
        {
            return dailyReservationRepository.GetMany(r => (!hotelId.HasValue || r.HotelID == hotelId.Value) && (!otaId.HasValue || r.OTAID == otaId.Value)
                        && (!year.HasValue || r.ReservationDay.Year == year.Value) && (!month.HasValue || r.ReservationDay.Month == month.Value));
        }
        public IEnumerable<DailyReservation> GetFilteredDailyReservation(int? hotelId, int? otaId, DateTime? reservationDay)
        {
            return dailyReservationRepository.GetMany(r => (!hotelId.HasValue || r.HotelID == hotelId.Value) && (!otaId.HasValue || r.OTAID == otaId.Value)
                        && (!reservationDay.HasValue || r.ReservationDay == reservationDay.Value));
        }
        #endregion
        public void SaveReservation()
        {
            unitOfWork.Commit();
        }
    }
}
