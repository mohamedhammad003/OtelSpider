using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelSpider.Core.DAL.Models
{
    public class DailyReservationLogger
    {
        [Key]
        public int ID { get; set; }
        public int DailyReservationID { get; set; }
        public int HotelID { get; set; }
        public int OTAID { get; set; }
        public DateTime ReservationDay { get; set; }
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
