using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelSpider.Core.DAL.Models
{
    public class RateStructurePeriod
    {
        [Key]
        public int ID { get; set; }
        public string PeriodName { get; set; }
        [ForeignKey("Hotel")]
        public int HotelID { get; set; }
        public decimal RoomSupplement { get; set; }
        public decimal MealSupplement { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string BackGroundColor { get; set; }
        public virtual Hotel Hotel { get; set; }
    }
}
