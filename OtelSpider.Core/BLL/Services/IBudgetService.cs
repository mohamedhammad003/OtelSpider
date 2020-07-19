using OtelSpider.Core.DAL.Models;
using System.Collections.Generic;

namespace OtelSpider.Core.BLL.Services
{
    public interface IBudgetService
    {
        void Create(HotelBudget item);
        void Update(HotelBudget item);
        void Delete(int id);
        HotelBudget GetBudget(int id);
        HotelBudget GetYearBudgets(int hotelId, int year);
        IEnumerable<HotelBudget> GetHotelBudgets(int hotelId);
        decimal GetTotalAnnualBudget(int hotelId, int year);
        void SaveChanges();
    }
}
