using OtelSpider.Core.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelSpider.Core.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        OtelSpiderContext Init();
    }
}
