using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OtelSpider.Web.ViewModels
{
    public class RateStructureSettingVM
    {
        public int ID { get; set; }
        public int HotelID { get; set; }
        [Display(Name = "Base Room")]
        public int BaseRoomTypeID { get; set; }
        public decimal OnlinePrecentage { get; set; }
        [Display(Name ="Base Currency")]
        public int BaseCurrencyID { get; set; }
        [Display(Name = "Shown Currency")]
        public int ShownCurrencyID { get; set; }
        public decimal CurrencyConversionRate { get; set; }
        public virtual RoomTypeViewModel BaseRoomType { get; set; }
        public virtual HotelViewModel Hotel { get; set; }
        public virtual CurrencyViewModel BaseCurrency { get; set; }
        public virtual CurrencyViewModel ShownCurrency { get; set; }
    }
}