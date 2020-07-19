using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelSpider.Core.DAL.Models
{
    public class RateStructureSetting
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("Hotel")]
        public int HotelID { get; set; }
        [ForeignKey("BaseRoomType")]
        public int BaseRoomTypeID { get; set; }
        public decimal OnlinePrecentage { get; set; }
        [ForeignKey("BaseCurrency")]
        public int BaseCurrencyID { get; set; }
        [ForeignKey("ShownCurrency")]
        public int ShownCurrencyID { get; set; }
        public decimal CurrencyConversionRate { get; set; }
        public virtual RoomType BaseRoomType { get; set; }
        public virtual Hotel Hotel { get; set; }
        public virtual Currency BaseCurrency { get; set; }
        public virtual Currency ShownCurrency { get; set; }
    }
}
