using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OtelSpider.Web.ViewModels
{
    public class RoomTypeViewModel
    {
        public int ID { get; set; }
        [Display(Name ="Room Type")]
        public string Type { get; set; }
        public int HotelID { get; set; }
        [Display(Name = "Min Capacity"), Range(1,5)]
        public int MinCapacity { get; set; }
        [Display(Name = "Room Capacity"), Range(1, 5)]
        public int RoomCapacity { get; set; }
        [Display(Name = "Base Room")]
        public bool IsBase { get; set; }
        [Display(Name = "Room Supplement")]
        public decimal AdditionValue { get; set; }
        [Display(Name = "Abbrev")]
        public string Abbreviation { get; set; }
        [Display(Name = "Occupancy Rate")]
        public bool OccupancyRate { get; set; }
        [Display(Name = "Per Person")]
        public bool isPerPerson { get; set; }
        [Display(Name = "Calculation Method")]
        public bool isSubstraction { get; set; }
        [Display(Name = "Precentage / Amount")]
        public bool isPrecentage { get; set; }
        public virtual HotelViewModel Hotel { get; set; }

        public virtual ICollection<occupancyFormulaVM> OccupancyFormulas { get; set; }
        public virtual ICollection<RoomMealPlanVM> RoomMealPlans { get; set; }
    }
}