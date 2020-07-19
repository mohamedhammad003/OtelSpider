using OtelSpider.Core.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelSpider.Core.BLL.Services
{
    public interface IRateStructureSettingService
    {
        void Create(RateStructureSetting item);
        void Update(RateStructureSetting item);
        void Delete(int id);
        RateStructureSetting GetRateStructureSetting(int id);
        RateStructureSetting GetHotelRateStructureSetting(int hotelId);
        void SaveChanges();
    }
}
