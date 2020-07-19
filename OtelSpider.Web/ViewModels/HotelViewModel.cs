using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OtelSpider.Web.ViewModels
{
    public class HotelViewModel
    {
        public int ID { get; set; }
        [Display(Name = "Hotel")]
        public string Name { get; set; }
        public int ParentID { get; set; }
        public string WebURL { get; set; }

        public virtual ICollection<MonthlyReservationVM> Reservations { get; set; }
        public virtual HotelParentViewModel HotelParent { get; set; }
    }
}
