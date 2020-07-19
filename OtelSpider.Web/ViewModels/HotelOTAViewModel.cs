using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OtelSpider.Web.ViewModels
{
    public class HotelOTAViewModel
    {
        public int ID { get; set; }
        public int HotelID { get; set; }
        [Display(Name = "Hotel")]
        public string HotelName { get; set; }
        public int OTAID { get; set; }
        [Display(Name = "OTA")]
        public string OTAName { get; set; }
        [Display(Name = "Commission Percentage")]
        public decimal CommissionPercentage { get; set; }
        [Display(Name = "Included VAT")]
        public bool IncludeVAT { get; set; }
        [Display(Name = "Included Service Charge")]
        public bool IncludeServiceCharge { get; set; }
        [Display(Name = "Included City Tax")]
        public bool IncludeCityTax { get; set; }
        [Display(Name = "Extranet URL")]
        public string ExtranetURL { get; set; }
        [Display(Name = "User Name")]
        public string UserName { get; set; }
    }
}