using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelSpider.Core.DAL.Models
{
    public class RoomMealPlan
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("RoomType")]
        public int RoomTypeID { get; set; }
        [ForeignKey("MealPlan")]
        public int MealPlanID { get; set; }
        public bool IsBase { get; set; }
        public virtual RoomType RoomType { get; set; }
        public virtual MealPlan MealPlan { get; set; }
    }
}
