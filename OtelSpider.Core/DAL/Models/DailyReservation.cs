using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelSpider.Core.DAL.Models
{
    public class DailyReservation
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("Hotel")]
        public int HotelID { get; set; }
        [ForeignKey("OTA")]
        public int OTAID { get; set; }
        public DateTime ReservationDay { get; set; }
        [StringLength(50)]
        public string MonthLetters { get; set; }
        public int RoomNights { get; set; }
        public decimal GrossRoomRevenue { get; set; }
        public decimal GrossARR { get; set; }
        public decimal NetRoomRevenue { get; set; }
        public decimal NetARR { get; set; }
        [ForeignKey("Creator")]
        public string CreatedByID { get; set; }
        [ForeignKey("UpdatedBy")]
        public string UpdatedByID { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public virtual Hotel Hotel { get; set; }
        public virtual OTA OTA { get; set; }
        public virtual SystemUser Creator { get; set; }
        public virtual SystemUser UpdatedBy { get; set; }
    }
}
