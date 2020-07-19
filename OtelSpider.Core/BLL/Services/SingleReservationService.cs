using OtelSpider.Core.BLL.Repositories;
using OtelSpider.Core.DAL.Models;
using OtelSpider.Core.Infrastructure;
using System.Collections.Generic;

namespace OtelSpider.Core.BLL.Services
{
    public class SingleReservationService : ISingleReservationService
    {
        private readonly ISingleReservationRepository singleReservationRepository;
        private readonly IUnitOfWork unitOfWork;
        public SingleReservationService(ISingleReservationRepository singleReservationRepository, IUnitOfWork unitOfWork)
        {
            this.singleReservationRepository = singleReservationRepository;
            this.unitOfWork = unitOfWork;
        }
        public void Create(SingleReservation item)
        {
            singleReservationRepository.Add(item);
        }
        public void Update(SingleReservation item)
        {
            singleReservationRepository.Update(item);
        }
        public SingleReservation GetSingleReservation(int id)
        {
            return singleReservationRepository.Get(x => x.ID == id);
        }
        public SingleReservation GetSingleReservationByRefID(string refID)
        {
            return singleReservationRepository.Get(x => x.ReservationRefID == refID);
        }
        public IEnumerable<SingleReservation> GetSingleReservations()
        {
            return singleReservationRepository.GetMany(r=> true);
        }

        public IEnumerable<SingleReservation> GetSingleReservationsByHotel(int hotelId)
        {
            return singleReservationRepository.GetMany(r => r.HotelID == hotelId);
        }
        

        public IEnumerable<SingleReservation> GetSingleReservationsByOTA(int otaId)
        {
            return singleReservationRepository.GetMany(r => r.OTAID == otaId);
        }
        public IEnumerable<SingleReservation> GetFilteredSingleReservation(int? hotelId, int? otaId, int? year, int? month)
        {
            return singleReservationRepository.GetMany(r => (!hotelId.HasValue || r.HotelID == hotelId.Value) && (!otaId.HasValue || r.OTAID == otaId.Value) 
                        && (!year.HasValue || r.CheckinDate.Year == year.Value) && (!month.HasValue || r.CheckinDate.Month == month.Value));
        }
        public void SaveChanges()
        {
            unitOfWork.Commit();
        }
    }
}
