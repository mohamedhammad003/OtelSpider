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
    public class ReservationStatusService : IReservationStatusService
    {

        private readonly IReservationStatusRepository reservationStatusRepository;
        private readonly IUnitOfWork unitOfWork;
        public ReservationStatusService(IReservationStatusRepository reservationStatusRepository, IUnitOfWork unitOfWork)
        {
            this.reservationStatusRepository = reservationStatusRepository;
            this.unitOfWork = unitOfWork;
        }
        public void Create(ReservationStatus item)
        {
            reservationStatusRepository.Add(item);
        }
        public void Update(ReservationStatus item)
        {
            reservationStatusRepository.Update(item);
        }
        public void Delete(int id)
        {
            var item = GetReservationStatus(id);
            reservationStatusRepository.Delete(item);
        }
        public ReservationStatus GetReservationStatus(int id)
        {
            return reservationStatusRepository.Get(m => m.ID == id);
        }
        public ReservationStatus GetReservationStatusByName(string name)
        {
            return reservationStatusRepository.Get(m => m.Status == name);
        }
        public IEnumerable<ReservationStatus> GetReservationStatuses()
        {
            return reservationStatusRepository.GetMany(r => true);
        }
        public void SaveChanges()
        {
            unitOfWork.Commit();
        }
    }
}
