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
    public class OTAService : IOTAService
    {
        private readonly IOTARepository OTARepository;
        private readonly IUnitOfWork unitOfWork;
        public OTAService(IOTARepository OTARepository, IUnitOfWork unitOfWork)
        {
            this.OTARepository = OTARepository;
            this.unitOfWork = unitOfWork;
        }
        public void CreateOTA(OTA item)
        {
            OTARepository.Add(item);
        }
        public void Update(OTA item)
        {
            OTARepository.Update(item);
        }
        public void DeleteOTA(int id)
        {
            var item = GetOTA(id);
            OTARepository.Delete(item);
        }
        public OTA GetOTA(int id)
        {
            return OTARepository.Get(x => x.ID == id);
        }
        public OTA GetOTAByName(string name)
        {
            return OTARepository.Get(x => x.Name == name);
        }

        public IEnumerable<OTA> GetOTAs()
        {
            return OTARepository.GetMany(r => true);
        }

        public void SaveOTA()
        {
            unitOfWork.Commit();
        }
    }
}
