using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelSpider.Core.DAL.Models
{
    public class SingleReservation
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
        [ForeignKey("Hotel")]
        public int HotelID { get; set; }
        [ForeignKey("OTA")]
        public int OTAID { get; set; }
        public int MealPlanID { get; set; }
        public int StatusID { get; set; }
        [ForeignKey("Currency")]
        public int? CurrencyID { get; set; }
        public virtual RoomType RoomType { get; set; }
        public virtual MealPlan MealPlan { get; set; }
        public virtual Hotel Hotel { get; set; }
        public virtual OTA OTA { get; set; }
        public virtual ReservationStatus Status { get; set; }
        public virtual Currency Currency { get; set; }

    }
}
