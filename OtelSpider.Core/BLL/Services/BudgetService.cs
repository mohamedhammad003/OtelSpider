using OtelSpider.Core.BLL.Repositories;
using OtelSpider.Core.DAL.Models;
using OtelSpider.Core.Infrastructure;
using System.Collections.Generic;

namespace OtelSpider.Core.BLL.Services
{
    public class BudgetService : IBudgetService
    {

        private readonly IBudgetRepository budgetRepository;
        private readonly IUnitOfWork unitOfWork;
        public BudgetService(IBudgetRepository budgetRepository, IUnitOfWork unitOfWork)
        {
            this.budgetRepository = budgetRepository;
            this.unitOfWork = unitOfWork;
        }
        public void Create(HotelBudget item)
        {
            budgetRepository.Add(item);
        }
        public void Update(HotelBudget item)
        {
            budgetRepository.Update(item);
        }
        public void Delete(int id)
        {
            var item = GetBudget(id);
            budgetRepository.Delete(item);
        }
        public HotelBudget GetBudget(int id)
        {
            return budgetRepository.Get(x => x.ID == id);
        }
        public HotelBudget GetYearBudgets(int hotelId, int year)
        {
            return budgetRepository.Get(x => x.HotelID == hotelId && x.Year == year);
        }
        public IEnumerable<HotelBudget> GetHotelBudgets(int hotelId)
        {
            return budgetRepository.GetMany(x => x.HotelID == hotelId );
        }
        public decimal GetTotalAnnualBudget(int hotelId, int year)
        {
            var annualBudget = budgetRepository.Get(x => x.HotelID == hotelId && x.Year == year);
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
