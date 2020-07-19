using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OtelSpider.Web.ViewModels
{
    public class CustomisedUserInfo
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public List<string> UserPermissions { get; set; }
        public List<string> UserRoles { get; set; }
        public List<HotelViewModel> Hotels { get; set; }

    }
}