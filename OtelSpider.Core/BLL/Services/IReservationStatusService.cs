using OtelSpider.Core.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelSpider.Core.BLL.Services
{
    public interface IReservationStatusService
    {
        void Create(ReservationStatus item);
        void Update(ReservationStatus item);
        void Delete(int id);
        ReservationStatus GetReservationStatus(int id);
        ReservationStatus GetReservationStatusByName(string name);
        IEnumerable<ReservationStatus> GetReservationStatuses();
        void SaveChanges();
    }
}
