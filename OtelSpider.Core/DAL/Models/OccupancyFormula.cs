using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelSpider.Core.DAL.Models
{
    public class OccupancyFormula
    {
        [Key]
        public int ID { get; set; }
        public int RoomTypeID { get; set; }
        public int Capacity { get; set; }
        public bool IsBase { get; set; }
        public decimal AdditionalValue { get; set; }
        public bool isSubstraction { get; set; }
        public bool isPrecentage { get; set; }
    }
}
