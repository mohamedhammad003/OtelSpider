using OtelSpider.Core.DAL.Models;
using OtelSpider.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelSpider.Core.BLL.Repositories
{
    public class HotelMealPlanRepository : RepositoryBase<HotelMealPlan>, IHotelMealPlanRepository
    {
        public HotelMealPlanRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
    public interface IHotelMealPlanRepository : IRepository<HotelMealPlan>
    {

    }
}
