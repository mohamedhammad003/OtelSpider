using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OtelSpider.Web.ViewModels
{
    public class HotelParentViewModel
    {
        public int ID { get; set; }
        [Display(Name ="Parent Hotel")]
        public string Name { get; set; }
        public string WebURL { get; set; }
        public virtual ICollection<HotelViewModel> Hotels { get; set; }
    }
}