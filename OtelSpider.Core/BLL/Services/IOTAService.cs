using OtelSpider.Core.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelSpider.Core.BLL.Services
{
    public interface IOTAService
    {
        void CreateOTA(OTA item);
        void Update(OTA item);
        void DeleteOTA(int id);
        OTA GetOTA(int id);
        OTA GetOTAByName(string name);
        IEnumerable<OTA> GetOTAs();
        void SaveOTA();
    }
}
