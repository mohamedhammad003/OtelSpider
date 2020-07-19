using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelSpider.Core.DAL.Models
{
    public class Market
    {
        public Market()
        {
            HotelMarkets = new HashSet<HotelMarket>();
        }
        [Key]
        public int ID { get; set; }
        [StringLength(150)]
        public string Name { get; set; }
        [ForeignKey("Currency")]
        public int CurrencyID { get; set; }
        public Currency Currency { get; set; }
        public ICollection<HotelMarket> HotelMarkets { get; set; } 
    }
}
