using OtelSpider.Core.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelSpider.Core.BLL.Services
{
    public interface ISingleReservationService
    {

        void Create(SingleReservation item);
        void Update(SingleReservation item);
        SingleReservation GetSingleReservation(int id);
        SingleReservation GetSingleReservationByRefID(string refID);
        IEnumerable<SingleReservation> GetSingleReservations();
        IEnumerable<SingleReservation> GetSingleReservationsByHotel(int hotelId);
        IEnumerable<SingleReservation> GetSingleReservationsByOTA(int otaId);
        IEnumerable<SingleReservation> GetFilteredSingleReservation(int? hotelId, int? otaId, int? year, int? month);
        void SaveChanges();
    }
}
