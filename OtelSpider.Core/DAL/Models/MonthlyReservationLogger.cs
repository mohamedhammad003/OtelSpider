using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OtelSpider.Core.DAL.Models
{
    public class MonthlyReservationLogger
    {
        [Key]
        public int ID { get; set; }
        public int MonthlyReservationID { get; set; }
        public int HotelID { get; set; }
        public int OTAID { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        [StringLength(50)]
        public string MonthLetters { get; set; }
        public int RoomNights { get; set; }
        public decimal GrossRoomRevenue { get; set; }
        public decimal GrossARR { get; set; }
        public decimal NetRoomRevenue { get; set; }
        public decimal NetARR { get; set; }
        public string CreatedByID { get; set; }
        public string UpdatedByID { get; set; }
    }
}
