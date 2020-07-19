using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OtelSpider.Web.ViewModels
{
    public class SingleReservationVM
    {
        public int ID { get; set; }
        [Display(Name = "Reference ID")]
        public string ReservationRefID { get; set; }
        [Display(Name = "Guest Name")]
        public string VisitorName { get; set; }
        [Display(Name = "Guest Email")]
        public string VisitorEmail { get; set; }
        [Display(Name = "Phone Number")]
        public string PhoneNum { get; set; }
        [Display(Name = "Nationality")]
        public string Nationality { get; set; }
        [Display(Name = "Arrival Date")]
        public DateTime CheckinDate { get; set; }
        [Display(Name = "Departure Date")]
        public DateTime? CheckoutDate { get; set; }
        [Display(Name = "Room Nights")]
        public int NightsNum { get; set; }
        [Display(Name = "Promo Code")]
        public string PromoCode { get; set; }
        [Display(Name = "Discount %")]
        public decimal Discount { get; set; }
        [Display(Name = "Rate Per Night")]
        public decimal RoomPrice { get; set; }
        [Display(Name = "Total Rate")]
        public decimal Price { get; set; }
        [Display(Name = "Total")]
        public decimal Total { get; set; }
        [Display(Name = "Cancellation Policy")]
        public int ReservationType { get; set; }
        [Display(Name = "Room Type ID")]
        public int RoomTypeID { get; set; }
        [Display(Name = "Hotel ID")]
        public int HotelID { get; set; }
        [Display(Name = "OTA ID")]
        public int OTAID { get; set; }
        [Display(Name = "Meal Plan ID")]
        public int MealPlanID { get; set; }
        [Display(Name = "Meal Plan")]
        public string MealPlanName { get; set; }
        [Display(Name = "Status ID")]
        public int StatusID { get; set; }
        [Display(Name = "Status")]
        public string Status { get; set; }
        [Display(Name = "Currency ID")]
        public int? CurrencyID { get; set; }
        [Display(Name = "Currency")]
        public string CurrencyCode { get; set; }
        public virtual RoomTypeViewModel RoomType { get; set; }
        public virtual HotelViewModel Hotel { get; set; }
        public virtual OTAViewModel OTA { get; set; }
    }
}