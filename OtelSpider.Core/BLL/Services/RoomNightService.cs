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
    public class RoomNightService : IRoomNightService
    {

        private readonly IRoomNightRepository roomNightRepository;
        private readonly IUnitOfWork unitOfWork;
        public RoomNightService(IRoomNightRepository roomNightRepository, IUnitOfWork unitOfWork)
        {
            this.roomNightRepository = roomNightRepository;
            this.unitOfWork = unitOfWork;
        }
        public void Create(AvailableRoomNights item)
        {
            roomNightRepository.Add(item);
        }
        public void Update(AvailableRoomNights item)
        {
            roomNightRepository.Update(item);
        }
        public void Delete(int id)
        {
            var item = GetAvailableRoomNight(id);
            roomNightRepository.Delete(item);
        }
        public AvailableRoomNights GetAvailableRoomNight(int id)
        {
            return roomNightRepository.Get(x => x.ID == id);
        }
        public AvailableRoomNights GetYearAvailableRoomNight(int hotelId, int year)
        {
            return roomNightRepository.Get(x => x.HotelID == hotelId && x.Year == year);
        }
        public IEnumerable<AvailableRoomNights> GetHotelRoomNights(int hotelId)
        {
            return roomNightRepository.GetMany(x => x.HotelID == hotelId);
        }
        public int GetTotalAnnualRoomNights(int hotelId, int year)
        {
            var annualBudget = roomNightRepository.Get(x => x.HotelID == hotelId && x.Year == year);
            var total = annualBudget != null ? annualBudget.January + annualBudget.February + annualBudget.March + annualBudget.April + annualBudget.May
                + annualBudget.June + annualBudget.July + annualBudget.August + annualBudget.September + annualBudget.October
                + annualBudget.November + annualBudget.December : 0;

            return total;
        }
        public void SaveChanges()
        {
            unitOfWork.Commit();
        }
    }
}
