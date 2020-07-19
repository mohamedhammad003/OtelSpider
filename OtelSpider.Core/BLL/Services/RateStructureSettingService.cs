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
    public class RateStructureSettingService : IRateStructureSettingService
    {

        private readonly IRateStructureSettingRepository rateStructureSettingRepository;
        private readonly IUnitOfWork unitOfWork;
        public RateStructureSettingService(IRateStructureSettingRepository rateStructureSettingRepository, IUnitOfWork unitOfWork)
        {
            this.rateStructureSettingRepository = rateStructureSettingRepository;
            this.unitOfWork = unitOfWork;
        }
        public void Create(RateStructureSetting item)
        {
            rateStructureSettingRepository.Add(item);
        }
        public void Update(RateStructureSetting item)
        {
            rateStructureSettingRepository.Update(item);
        }
        public void Delete(int id) {
            var item = GetRateStructureSetting(id);
            rateStructureSettingRepository.Delete(item);
        }
        public RateStructureSetting GetRateStructureSetting(int id)
        {
            return rateStructureSettingRepository.Get(rs => rs.ID == id);
        }
        public RateStructureSetting GetHotelRateStructureSetting(int hotelId)
        {
            return rateStructureSettingRepository.Get(rs => rs.HotelID == hotelId);
        }
        public void SaveChanges()
        {
            unitOfWork.Commit();
        }
    }
}
