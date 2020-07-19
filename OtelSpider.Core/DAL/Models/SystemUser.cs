using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelSpider.Core.DAL.Models
{
    public class SystemUser : IdentityUser
    {
        public SystemUser()
        {
            MonthlyReservations = new HashSet<MonthlyReservation>();
            UserHotels = new HashSet<UserHotel>();
            UserPermissions = new HashSet<UserPermission>();
            isActive = true;
        }
        [StringLength(100), Required(ErrorMessage = "First Name is Required.")]
        public string FirstName { get; set; }
        [StringLength(100)]
        public string LastName { get; set; }
        public string Title { get; set; }
        public string ProfilePic { get; set; }
        public int SystemRoleID { get; set; }

        [Display(Name = "Admin")]
        public bool isAdmin { get; set; }
        [Display(Name = "Active")]
        public bool isActive { get; set; }
        public virtual ICollection<MonthlyReservation> MonthlyReservations { get; set; }
        public virtual ICollection<UserHotel> UserHotels { get; set; }
        public virtual ICollection<UserPermission> UserPermissions { get; set; }
    }
}
