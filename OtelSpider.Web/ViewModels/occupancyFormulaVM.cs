using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OtelSpider.Web.ViewModels
{
    public class occupancyFormulaVM
    {
        public int ID { get; set; }
        public int RoomTypeID { get; set; }
        public int Capacity { get; set; }
        public bool IsBase { get; set; }
        [Display(Name = "Addition value")]
        public decimal AdditionalValue { get; set; }
        [Display(Name = "Calculation Method")]
        public bool isSubstraction { get; set; }
        [Display(Name = "Precentage / Amount")]
        public bool isPrecentage { get; set; }
    }
}