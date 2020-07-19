using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OtelSpider.Web.ViewModels
{
    public class AnnualReservationsViewModel
    {
        public int HotelID { get; set; }
        [Display(Name = "Hotel")]
        public string HotelName { get; set; }
        public int OTAID { get; set; }
        [Display(Name = "OTA")]
        public string OTAName { get; set; }
        public int Year { get; set; }
        [Display(Name = "Room Nights")]
        public int RoomNights { get; set; }
        [Display(Name = "Gross Room Revenue")]
        public decimal GrossRoomRevenue { get; set; }
        [Display(Name = "Groos ARR")]
        public decimal GrossARR { get; set; }
        [Display(Name = "Net Room Revenue")]
        public decimal NetRoomRevenue { get; set; }
        [Display(Name = "Net ARR")]
        public decimal NetARR { get; set; }
        [Display(Name ="%")]
        public decimal TargetPrecentage { get; set; }
        [Display(Name = "Budget")]
        public decimal TargetBudget { get; set; }
    }
}