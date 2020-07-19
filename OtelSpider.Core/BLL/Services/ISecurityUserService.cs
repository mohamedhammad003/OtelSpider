using Microsoft.AspNet.Identity.EntityFramework;
using OtelSpider.Core.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelSpider.Core.BLL.Services
{
    public interface ISecurityUserService
    {
        void UpdateUser(SystemUser user);
        SystemUser LogInAndGetUsersInfo(string Password, string UserName);
        SystemUser GetUserByID(string userID);
        IEnumerable<SystemUser> GetUsers();
        IEnumerable<string> GetUserRoles(string userID);
        IEnumerable<SystemPermission> GetUserPermissions(string userID);
        IEnumerable<Hotel> GetUserHotels(string userID);
        IEnumerable<IdentityRole> GetRoles();
    }
}
