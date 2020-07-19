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
    public class RateStructurePeriodService : IRateStructurePeriodService
    {
        private readonly IRateStructurePeriodRepository rateStructurePeriodRepository;
        private readonly IUnitOfWork unitOfWork;
        public RateStructurePeriodService(IRateStructurePeriodRepository rateStructurePeriodRepository, IUnitOfWork unitOfWork)
        {
            this.rateStructurePeriodRepository = rateStructurePeriodRepository;
            this.unitOfWork = unitOfWork;
        }
        public void Create(RateStructurePeriod item) 
        {
            rateStructurePeriodRepository.Add(item);
        }
        public void Update(RateStructurePeriod item) 
        {
            rateStructurePeriodRepository.Update(item);
        }
        public void Delete(int id) 
        {
            var item = GetRateStructurePeriod(id);
            rateStructurePeriodRepository.Delete(item);
        }
        public RateStructurePeriod GetRateStructurePeriod(int id)
        {
            return rateStructurePeriodRepository.Get(r => r.ID == id);
        }
        public RateStructurePeriod GetRateStructurePeriodByDate(int hotelId, DateTime periodDate)
        {
            return rateStructurePeriodRepository.Get(r => r.StartDate <= periodDate && r.EndDate >= periodDate && r.HotelID == hotelId);
        }
        public IEnumerable<RateStructurePeriod> GetHotelRateStructurePeriods(int hotelId)
        {
            return rateStructurePeriodRepository.GetMany(r => r.HotelID == hotelId);
        }

        public IEnumerable<RateStructurePeriod> GetRateStructurePeriodsByYear(int hotelId, int year)
        {
            return rateStructurePeriodRepository.GetMany(r => r.HotelID == hotelId && (r.StartDate.Year == year || r.EndDate.Year == year));
        }
        public void SaveChanges()
        {
            unitOfWork.Commit();
        }
    }
}
