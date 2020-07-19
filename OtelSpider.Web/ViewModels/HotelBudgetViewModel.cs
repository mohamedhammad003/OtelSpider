using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OtelSpider.Web.ViewModels
{
    public class HotelBudgetViewModel
    {
        public BudgetViewModel Budget { get; set; }
        public RoomNightsViewModel RoomNights { get; set; }
    }
}