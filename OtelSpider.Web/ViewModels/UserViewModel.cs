using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OtelSpider.Web.ViewModels
{
    public class UserViewModel
    {
        public string ID { get; set; }
        [StringLength(100), Required(ErrorMessage = "First Name is Required.")]
        public string FirstName { get; set; }
        [StringLength(100)]
        public string LastName { get; set; }
        public int SystemRoleID { get; set; }
        public string Title { get; set; }
        [Display(Name = "Admin")]
        public bool isAdmin { get; set; }
        [Display(Name = "Active")]
        public bool isActive { get; set; }
    }
}
