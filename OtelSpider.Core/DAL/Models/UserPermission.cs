using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelSpider.Core.DAL.Models
{
    public class UserPermission
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("SystemUser")]
        public string SystemUserID { get; set; }
        [ForeignKey("Permission")]
        public int PermissionID { get; set; }
        public virtual SystemUser SystemUser { get; set; }
        public virtual SystemPermission Permission { get; set; }
    }
}
