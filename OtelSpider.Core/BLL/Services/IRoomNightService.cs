using OtelSpider.Core.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelSpider.Core.BLL.Services
{
    public interface IRoomNightService
    {

        void Create(AvailableRoomNights item);
        void Update(AvailableRoomNights item);
        void Delete(int id);
        AvailableRoomNights GetAvailableRoomNight(int id);
        AvailableRoomNights GetYearAvailableRoomNight(int hotelId, int year);
        IEnumerable<AvailableRoomNights> GetHotelRoomNights(int hotelId);
        int GetTotalAnnualRoomNights(int hotelId, int year);
        void SaveChanges();
    }
}
