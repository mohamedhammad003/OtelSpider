using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelSpider.Core.DAL.Models
{
    public class SystemPermission
    {
        public SystemPermission()
        {
            UserPermissions = new HashSet<UserPermission>();
        }
        [Key]
        public int ID { get; set; }
        public string PermissionName { get; set; }
        public virtual ICollection<UserPermission> UserPermissions { get; set; }
    }
}
