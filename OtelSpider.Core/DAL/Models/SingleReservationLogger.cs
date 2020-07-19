using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelSpider.Core.DAL.Models
{
    public class SingleReservationLogger
    {
        [Key]
        public int ID { get; set; }
        [StringLength(250)]
        public string ReservationRefID { get; set; }
        [StringLength(250)]
        public string VisitorName { get; set; }
        [StringLength(250)]
        public string VisitorEmail { get; set; }
        [StringLength(100)]
        public string PhoneNum { get; set; }
        [StringLength(100)]
        public string Nationality { get; set; }
        public DateTime CheckinDate { get; set; }
        public DateTime? CheckoutDate { get; set; }
        public int NightsNum { get; set; }
        [StringLength(250)]
        public string PromoCode { get; set; }
        public decimal Discount { get; set; }
        public decimal RoomPrice { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
        public int ReservationType { get; set; }
        public int RoomTypeID { get; set; }
        public int HotelID { get; set; }
        public int OTAID { get; set; }
        public int MealPlanID { get; set; }
        public int StatusID { get; set; }
        public int? CurrencyID { get; set; }
    }
}
