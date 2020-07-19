using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelSpider.Core.DAL.Models
{
    public class UserHotel
    {
        public int ID { get; set; }
        [ForeignKey("User")]
        public string UserID { get; set; }
        [ForeignKey("Hotel")]
        public int HotelID { get; set; }
        public virtual SystemUser User { get; set; }
        public virtual Hotel Hotel { get; set; }
    }
}
