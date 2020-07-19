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
    public class HotelOTAService : IHotelOTAService 
    {

        private readonly IHotelOTARepository hotelOTARepository;
        private readonly IUnitOfWork unitOfWork;
        public HotelOTAService(IHotelOTARepository hotelOTARepository, IUnitOfWork unitOfWork)
        {
            this.hotelOTARepository = hotelOTARepository;
            this.unitOfWork = unitOfWork;
        }
        public void Create(HotelOTA item)
        {
            hotelOTARepository.Add(item);
        }
        public void Update(HotelOTA item)
        {
            hotelOTARepository.Update(item);
        }
        public void Delete(int id)
        {
            var item = GetHotelOTA(id);
            hotelOTARepository.Delete(item);
        }
        public HotelOTA GetHotelOTA(int id)
        {
            return hotelOTARepository.Get(i => i.ID == id);
        }
        public HotelOTA GetHotelOTA(int hotelId, int OTAId)
        {
            return hotelOTARepository.Get(i => i.HotelID == hotelId && i.OTAID == OTAId);
        }
        public IEnumerable<HotelOTA> GetHotelOTAs(int hotelId)
        {
            return hotelOTARepository.GetMany(x => x.HotelID == hotelId);
        }
        public IEnumerable<HotelOTA> GetHotelOTAs(List<int> hotelIds)
        {
            return hotelOTARepository.GetMany(x => hotelIds.Contains(x.HotelID));
        }
        public void SaveChanges()
        {
            unitOfWork.Commit();
        }
    }
}
