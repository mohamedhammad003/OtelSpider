using OtelSpider.Core.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelSpider.Core.BLL.Services
{
    public interface ICurrencyService
    {
        void Create(Currency item);
        void Update(Currency item);
        void Delete(int id);
        Currency GetCurrency(int id);
        Currency GetCurrencyByName(string name);
        Currency GetCurrencyByCode(string code);
        IEnumerable<Currency> GetCurrencies();
        void SaveChanges();
    }
}
