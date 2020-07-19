using OtelSpider.Core.Infrastructure;
using OtelSpider.Core.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelSpider.Core.BLL.Repositories
{
    public class TokenRepository : RepositoryBase<RegisterationToken>, ITokenRepository
    {
        public TokenRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
    public interface ITokenRepository : IRepository<RegisterationToken>
    {

    }
}
