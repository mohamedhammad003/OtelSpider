using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OtelSpider.Core.DAL.Models
{
    public class Hotel
    {
        public Hotel()
        {
            OTAReservations = new HashSet<MonthlyReservation>();
            UserHotels = new HashSet<UserHotel>();
            HotelMarkets = new HashSet<HotelMarket>();
            DailyReservations = new HashSet<DailyReservation>();
            SingleReservations = new HashSet<SingleReservation>();
            RoomTypes = new HashSet<RoomType>();
        }
        [Key]
        public int ID { get; set; }
        [StringLength(250)]
        public string Name { get; set; }
        public string WebURL { get; set; }
        [ForeignKey("HotelParent")]
        public int ParentID { get; set; }
        public virtual HotelParent HotelParent { get; set; }
        public virtual ICollection<MonthlyReservation> OTAReservations { get; set; }
        public virtual ICollection<DailyReservation> DailyReservations { get; set; }
        public virtual ICollection<SingleReservation> SingleReservations { get; set; }
        public virtual ICollection<UserHotel> UserHotels { get; set; }
        public virtual ICollection<HotelMarket> HotelMarkets { get; set; }
        public virtual ICollection<RoomType> RoomTypes { get; set; }
    }
}
