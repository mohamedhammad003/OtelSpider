using OtelSpider.Core.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelSpider.Core.BLL.Services
{
    public interface IHotelOTAService
    {
        void Create(HotelOTA item);
        void Update(HotelOTA item);
        void Delete(int id);
        HotelOTA GetHotelOTA(int id);
        HotelOTA GetHotelOTA(int hotelId, int OTAId);
        IEnumerable<HotelOTA> GetHotelOTAs(int hotelId);
        IEnumerable<HotelOTA> GetHotelOTAs(List<int> hotelIds);
        void SaveChanges();
    }
}
