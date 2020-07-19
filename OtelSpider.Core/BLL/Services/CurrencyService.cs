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
    public class CurrencyService : ICurrencyService
    {

        private readonly ICurrencyRepository currencyRepository;
        private readonly IUnitOfWork unitOfWork;
        public CurrencyService(ICurrencyRepository currencyRepository, IUnitOfWork unitOfWork)
        {
            this.currencyRepository = currencyRepository;
            this.unitOfWork = unitOfWork;
        }
        public void Create(Currency item)
        {
            currencyRepository.Add(item);
        }
        public void Update(Currency item)
        {
            currencyRepository.Update(item);
        }
        public void Delete(int id)
        {
            var item = GetCurrency(id);
            currencyRepository.Delete(item);
        }
        public Currency GetCurrency(int id)
        {
            return currencyRepository.Get(c => c.ID == id);
        }
        public Currency GetCurrencyByName(string name)
        {
            return currencyRepository.Get(c => c.Name == name);
        }
        public Currency GetCurrencyByCode(string code)
        {
            return currencyRepository.Get(c => c.CurrencyCode == code);
        }
        public IEnumerable<Currency> GetCurrencies()
        {
            return currencyRepository.GetMany(c => true);
        }
        public void SaveChanges()
        {
            unitOfWork.Commit();
        }
    }
}
