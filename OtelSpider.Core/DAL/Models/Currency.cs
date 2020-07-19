using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelSpider.Core.DAL.Models
{
    public class Currency
    {
        public Currency()
        {
            Markets = new HashSet<Market>();
        }
        [Key]
        public int ID { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(5)]
        public string CurrencyCode { get; set; }
        public virtual ICollection<Market> Markets { get; set; }
    }
}
