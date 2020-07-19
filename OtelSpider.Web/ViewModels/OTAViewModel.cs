using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OtelSpider.Web.ViewModels
{
    public class OTAViewModel
    {
        public int ID { get; set; }
        [Display(Name = "OTA")]
        public string Name { get; set; }
        public string WebURL { get; set; }
        public virtual ICollection<MonthlyReservationVM> Reservations { get; set; }
    }
}
