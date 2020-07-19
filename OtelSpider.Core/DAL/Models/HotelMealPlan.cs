using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelSpider.Core.DAL.Models
{
    public class HotelMealPlan
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("MealPlan")]
        public int MealPlanID { get; set; }
        [ForeignKey("Hotel")]
        public int HotelID { get; set; }
        public bool IsBase { get; set; }
        public int MealPlanSupplement { get; set; }
        public bool isSubstraction { get; set; }
        public bool isPerPerson { get; set; }
        public bool isPrecentage { get; set; }
        public virtual Hotel Hotel { get; set; }
        public virtual MealPlan MealPlan { get; set; }
    }
}
