using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OtelSpider.Web.ViewModels
{
    public class BudgetViewModel
    {
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
        public int HotelID { get; set; }
        public int CurrencyID { get; set; }
        public virtual HotelViewModel Hotel { get; set; }
        public virtual CurrencyViewModel Currency { get; set; }
    }
}