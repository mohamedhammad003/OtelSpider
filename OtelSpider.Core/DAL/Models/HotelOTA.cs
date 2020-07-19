using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelSpider.Core.DAL.Models
{
    public class HotelOTA
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("Hotel")]
        public int HotelID { get; set; }
        [ForeignKey("OTA")]
        public int OTAID { get; set; }
        public decimal CommissionPercentage { get; set; }
        public bool IncludeVAT { get; set; }
        public bool IncludeServiceCharge { get; set; }
        public bool IncludeCityTax { get; set; }
        public string ExtranetURL { get; set; }
        public string UserName { get; set; }
        public virtual Hotel Hotel { get; set; }
        public virtual OTA OTA { get; set; }
    }
}
