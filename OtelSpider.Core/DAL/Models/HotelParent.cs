using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OtelSpider.Core.DAL.Models
{
    public class HotelParent
    {
        public HotelParent()
        {
            Hotels = new HashSet<Hotel>();
        }
        [Key]
        public int ID { get; set; }
        [StringLength(250)]
        public string Name { get; set; }
        public string WebURL { get; set; }
        public virtual ICollection<Hotel> Hotels { get; set; }
    }
}
