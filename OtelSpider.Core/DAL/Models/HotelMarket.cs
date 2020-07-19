using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelSpider.Core.DAL.Models
{
    public class HotelMarket
    {
        public int ID { get; set; }
        [ForeignKey("Hotel")]
        public int HotelID { get; set; }
        [ForeignKey("Market")]
        public int MarketID { get; set; }
        public virtual Hotel Hotel { get; set; }
        public virtual Market Market { get; set; }
    }
}
