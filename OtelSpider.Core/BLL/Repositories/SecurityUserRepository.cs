using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using OtelSpider.Core.DAL.Data;
using OtelSpider.Core.DAL.Models;
using OtelSpider.Core.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace OtelSpider.Core.BLL.Repositories
{
    public class SecurityUserRepository : RepositoryBase<SystemUser>, ISecurityUserRepository
    {
        private OtelSpiderContext context;
        private UserStore<SystemUser> userStore;
        private UserManager<SystemUser> userManager;
        public SecurityUserRepository(IDbFactory dbFactory) : base(dbFactory)
        {
            context = new OtelSpiderContext();
            userStore = new UserStore<SystemUser>(context);
            userManager = new UserManager<SystemUser>(userStore);
        }

        public IEnumerable<string> GetUserRoles(string userId)
        {
            return userManager.GetRoles(userId);
        }
        public IEnumerable<UserPermission> GetUserPermissions(string userId)
        {
            return DbContext.UserPermissions.Where(x=> x.SystemUserID == userId);
        }
        public IEnumerable<UserHotel> GetUserHotels(string systemUserID)
        {
            return DbContext.UserHotels.Where(x => x.UserID == systemUserID);
        }
    }
    public interface ISecurityUserRepository : IRepository<SystemUser>
    {
        IEnumerable<string> GetUserRoles(string userID);
        IEnumerable<UserPermission> GetUserPermissions(string userId);
        IEnumerable<UserHotel> GetUserHotels(string systemUserID);
    }
}
