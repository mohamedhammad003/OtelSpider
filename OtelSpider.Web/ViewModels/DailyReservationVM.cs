using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OtelSpider.Web.ViewModels
{
    public class DailyReservationVM
    {
        public int ID { get; set; }
        public int HotelID { get; set; }
        public int OTAID { get; set; }
        public DateTime ReservationDay { get; set; }
        [Display(Name = "Room Nights")]
        public int RoomNights { get; set; }
        [Display(Name = "Gross Room Revenue")]
        public decimal GrossRoomRevenue { get; set; }
        [Display(Name = "Gross ARR")]
        public decimal GrossARR { get; set; }
        [Display(Name = "Net Room Revenue")]
        public decimal NetRoomRevenue { get; set; }
        [Display(Name = "Net ARR")]
        public decimal NetARR { get; set; }
        public string CreatedByID { get; set; }
        public string UpdatedByID { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public HotelViewModel Hotel { get; set; }
        public OTAViewModel OTA { get; set; }
    }
}