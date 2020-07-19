using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OtelSpider.Web.ViewModels
{
    public class RSPeriodViewModel
    {
        public int ID { get; set; }
        [Display(Name ="Period Name")]
        public string PeriodName { get; set; }
        public int HotelID { get; set; }
        [Display(Name = "Room Supplement")]
        public decimal RoomSupplement { get; set; }
        [Display(Name = "Meal Supplement")]
        public decimal MealSupplement { get; set; }
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }
        [Display(Name = "Background Color")]
        public string BackGroundColor { get; set; }
        public virtual HotelViewModel Hotel { get; set; }
    }
}