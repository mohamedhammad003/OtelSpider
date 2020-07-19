using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OtelSpider.Web.ViewModels
{
    public class UserPermissionViewModel
    {
        public int ID { get; set; }
        public string SystemUserID { get; set; }
        public int PermissionID { get; set; }
    }
}