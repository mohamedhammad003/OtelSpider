using OtelSpider.Core.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelSpider.Core.BLL.Services
{
    public interface IRateStructurePeriodService
    {
        void Create(RateStructurePeriod item);
        void Update(RateStructurePeriod item);
        void Delete(int id);
        RateStructurePeriod GetRateStructurePeriod(int id);
        RateStructurePeriod GetRateStructurePeriodByDate(int hotelId,DateTime periodDate);
        IEnumerable<RateStructurePeriod> GetHotelRateStructurePeriods(int hotelId);
        IEnumerable<RateStructurePeriod> GetRateStructurePeriodsByYear(int hotelId, int year);
        void SaveChanges();
    }
}
