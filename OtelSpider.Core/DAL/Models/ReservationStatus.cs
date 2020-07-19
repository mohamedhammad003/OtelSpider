using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelSpider.Core.DAL.Models
{
    public class ReservationStatus
    {
        [Key]
        public int ID { get; set; }
        [StringLength(250)]
        public string Status { get; set; }
    }
}
