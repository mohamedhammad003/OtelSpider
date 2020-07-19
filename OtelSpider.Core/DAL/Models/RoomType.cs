using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelSpider.Core.DAL.Models
{
    public class RoomType
    {
        public RoomType()
        {
            OccupancyFormulas = new HashSet<OccupancyFormula>();
            RoomMealPlans = new HashSet<RoomMealPlan>();
        }
        [Key]
        public int ID { get; set; }
        [StringLength(250)]
        public string Type { get; set; }
        [ForeignKey("Hotel")]
        public int HotelID { get; set; }
        public int RoomCapacity { get; set; }
        public int MinCapacity { get; set; }
        public bool IsBase { get; set; }
        public decimal AdditionValue { get; set; }
        public string Abbreviation { get; set; }
        public bool OccupancyRate { get; set; }
        public bool isPerPerson { get; set; }
        public bool isSubstraction { get; set; }
        public bool isPrecentage { get; set; }
        public virtual Hotel Hotel { get; set; }
        public virtual ICollection<OccupancyFormula> OccupancyFormulas { get; set; }
        public virtual ICollection<RoomMealPlan> RoomMealPlans { get; set; }
    }
}
