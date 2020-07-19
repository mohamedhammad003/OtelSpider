using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OtelSpider.Web.ViewModels
{
    public class HotelMealPlanVM
    {
        public int ID { get; set; }
        [Display(Name ="Meal Plan")]
        public int MealPlanID { get; set; }
        [Display(Name = "Meal Plan")]
        public string MealPlanName { get; set; }
        [Display(Name = "Abbrev")]
        public string Abbreviation { get; set; }
        [Display(Name = "Hotel")]
        public int HotelID { get; set; }
        [Display(Name = "Base Meal Plan")]
        public bool IsBase { get; set; }
        [Display(Name = "Meal Plan Supplement")]
        public int MealPlanSupplement { get; set; }
        [Display(Name = "Calculation Method")]
        public bool isSubstraction { get; set; }
        [Display(Name = "Per Person")]
        public bool isPerPerson { get; set; }
        [Display(Name = "Precentage / Amount")]
        public bool isPrecentage { get; set; }
        public virtual HotelViewModel Hotel { get; set; }
    }
}