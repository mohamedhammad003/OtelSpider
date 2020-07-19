using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelSpider.Core.DAL.Models
{
    public class HotelBudget
    {
        [Key]
        public int ID { get; set; }
        public int Year { get; set; }
        public decimal January { get; set; }
        public decimal February { get; set; }
        public decimal March { get; set; }
        public decimal April { get; set; }
        public decimal May { get; set; }
        public decimal June { get; set; }
        public decimal July { get; set; }
        public decimal August { get; set; }
        public decimal September { get; set; }
        public decimal October { get; set; }
        public decimal November { get; set; }
        public decimal December { get; set; }
        [ForeignKey("Hotel")]
        public int HotelID { get; set; }
        [ForeignKey("Currency")]
        public int CurrencyID { get; set; }
        public virtual Hotel Hotel { get; set; }
        public virtual Currency Currency { get; set; }
    }
}
