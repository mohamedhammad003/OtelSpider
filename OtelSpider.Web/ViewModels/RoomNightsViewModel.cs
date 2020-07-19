using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OtelSpider.Web.ViewModels
{
    public class RoomNightsViewModel
    {
        public int ID { get; set; }
        public int Year { get; set; }
        [Display(Name ="Jan Room Nights")]
        public int January { get; set; }
        [Display(Name = "Feb Room Nights")]
        public int February { get; set; }
        [Display(Name = "Mar Room Nights")]
        public int March { get; set; }
        [Display(Name = "Apr Room Nights")]
        public int April { get; set; }
        [Display(Name = "May Room Nights")]
        public int May { get; set; }
        [Display(Name = "June Room Nights")]
        public int June { get; set; }
        [Display(Name = "July Room Nights")]
        public int July { get; set; }
        [Display(Name = "Aug Room Nights")]
        public int August { get; set; }
        [Display(Name = "Sept Room Nights")]
        public int September { get; set; }
        [Display(Name = "Oct Room Nights")]
        public int October { get; set; }
        [Display(Name = "Nov Room Nights")]
        public int November { get; set; }
        [Display(Name = "Dec Room Nights")]
        public int December { get; set; }
        public int HotelID { get; set; }
        public virtual HotelViewModel Hotel { get; set; }
    }
}