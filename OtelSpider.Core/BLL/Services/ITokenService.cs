using OtelSpider.Core.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelSpider.Core.BLL.Services
{
    public interface ITokenService
    {
        void Create(RegisterationToken token);
        void Delete(int id);
        RegisterationToken GetRegisterationToken(int id);
        RegisterationToken GetRegisterationToken(string token);
        RegisterationToken GetRegisterationTokenByEmail(string userEmail);
        IEnumerable<RegisterationToken> GetRegisterationTokens();
        void SaveChanges();
    }
}
