using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OtelSpider.Web.ViewModels
{
    public class SingleReservationViewModel
    {

        public IEnumerable<SingleReservationVM> singleReservations { get; set; }
        public IEnumerable<HotelViewModel> lstHotels { get; set; }
        public IEnumerable<OTAViewModel> lstOTAs { get; set; }
        public int SelectedMonth { get; set; }
        public int SelectedYear { get; set; }
    }
}