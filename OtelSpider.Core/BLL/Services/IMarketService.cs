using OtelSpider.Core.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelSpider.Core.BLL.Services
{
    public interface IMarketService
    {
        void Create(Market item);
        void Update(Market item);
        void Delete(int id);
        Market GetMarket(int id);
        Market GetMarketByName(string name);
        IEnumerable<Market> GetMarkets();
        void SaveChanges();
    }
}
