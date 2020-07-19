using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelSpider.Core.DAL.Models
{
    public class OTA
    {
        public OTA()
        {
            Reservations = new HashSet<MonthlyReservation>();
        }
        [Key]
        public int ID { get; set; }
        [StringLength(250)]
        public string Name { get; set; }
        public string WebURL { get; set; }
        public virtual ICollection<MonthlyReservation> Reservations { get; set; }
    }
}
