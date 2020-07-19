using OtelSpider.Core.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelSpider.Core.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        OtelSpiderContext dbContext;

        public OtelSpiderContext Init()
        {
            return dbContext ?? (dbContext = new OtelSpiderContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}
