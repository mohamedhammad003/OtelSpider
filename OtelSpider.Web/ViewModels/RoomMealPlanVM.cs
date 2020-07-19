using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OtelSpider.Web.ViewModels
{
    public class RoomMealPlanVM
    {
        public int ID { get; set; }
        public int RoomTypeID { get; set; }
        public int MealPlanID { get; set; }
        public string MealPlanName { get; set; }
        public bool IsBase { get; set; }
        public virtual RoomTypeViewModel RoomType { get; set; }
    }
}