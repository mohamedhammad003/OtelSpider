using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OtelSpider.Web.ViewModels
{
    public class RateStructureReportVM
    {
        public int HotelID { get; set; }
        public int Year { get; set; }
        public RateStructureSettingVM rateStructureSettings { get; set; }
        public List<RSPeriodViewModel> RSPeriods { get; set; }
        public List<RoomTypeViewModel> RoomTypes { get; set; }
        public List<HotelMealPlanVM> hotelMealPlans { get; set; }
    }
}