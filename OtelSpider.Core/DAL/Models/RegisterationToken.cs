using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelSpider.Core.DAL.Models
{
    public class RegisterationToken
    {
        [Key]
        public int ID { get; set; }
        [StringLength(150)]
        public string Token { get; set; }
        [Required]
        public string UserEmail { get; set; }
        public int SystemRoleID { get; set; }
        public string RegisterLink { get; set; }
        public DateTime TokenExpires { get; set; }
    }
}
