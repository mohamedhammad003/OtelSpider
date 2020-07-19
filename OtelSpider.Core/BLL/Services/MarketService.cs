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
    public class MarketService : IMarketService
    {
        private readonly IMarketRepository marketRepository;
        private readonly IUnitOfWork unitOfWork;
        public MarketService(IMarketRepository marketRepository, IUnitOfWork unitOfWork)
        {
            this.marketRepository = marketRepository;
            this.unitOfWork = unitOfWork;
        }
        public void Create(Market item)
        {
            marketRepository.Add(item);
        }
        public void Update(Market item)
        {
            marketRepository.Update(item);
        }
        public void Delete(int id)
        {
            var item = GetMarket(id);
            marketRepository.Delete(item);
        }
        public Market GetMarket(int id)
        {
            return marketRepository.Get(m => m.ID == id);
        }
        public Market GetMarketByName(string name)
        {
            return marketRepository.Get(m => m.Name == name);
        }
        public IEnumerable<Market> GetMarkets()
        {
            return marketRepository.GetMany(r => true);
        }
        public void SaveChanges()
        {
            unitOfWork.Commit();
        }
    }
}
