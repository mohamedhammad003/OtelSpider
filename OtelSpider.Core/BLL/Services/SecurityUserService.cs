using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using OtelSpider.Core.BLL.Repositories;
using OtelSpider.Core.DAL.Data;
using OtelSpider.Core.DAL.Models;
using OtelSpider.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OtelSpider.Core.BLL.Services
{
    public class SecurityUserService : ISecurityUserService
    {
        private readonly ISecurityUserRepository securityUserRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly OtelSpiderContext context;
        private readonly UserManager<SystemUser> userManager;
        public SecurityUserService(ISecurityUserRepository securityUserRepository, IUnitOfWork unitOfWork)
        {
            this.securityUserRepository = securityUserRepository;
            this.unitOfWork = unitOfWork;
            context = new OtelSpiderContext();
            var userStore = new UserStore<SystemUser>(context);
            userManager = new UserManager<SystemUser>(userStore);
        }
        public void UpdateUser(SystemUser user)
        {

            if (user.isAdmin && !isAdmin(user.Id))
                userManager.AddToRole(user.Id, "AppAdmin");
            else if (!user.isAdmin && isAdmin(user.Id))
                userManager.RemoveFromRole(user.Id, "AppAdmin");
            userManager.Update(user);
        }
        public bool isAdmin(string userId)
        {
            return userManager.GetRoles(userId).Contains("AppAdmin");
        }
        public SystemUser LogInAndGetUsersInfo(string Password, string UserName)
        {
            var hashedPassword = new PasswordHasher().HashPassword(Password);
            return securityUserRepository.Get(x=> x.PasswordHash  == hashedPassword && x.UserName == UserName);
        }
        public SystemUser GetUserByID(string userID)
        {
            return securityUserRepository.Get(x => x.Id == userID);
        }
        public IEnumerable<SystemUser> GetUsers()
        {
            return securityUserRepository.GetMany(x => true);
        }
        public IEnumerable<string> GetUserRoles(string userID)
        {
            return securityUserRepository.GetUserRoles(userID);
        }
        public IEnumerable<SystemPermission> GetUserPermissions(string userID)
        {
            var userPermissions = securityUserRepository.GetUserPermissions(userID).ToList();
            return userPermissions.Select(x => x.Permission);
        }
        public IEnumerable<Hotel> GetUserHotels(string userID)
        {
            var userHotels = securityUserRepository.GetUserHotels(userID).ToList();
            return userHotels.Select(x => x.Hotel);
        }
        public IEnumerable<IdentityRole> GetRoles()
        {
            return context.Roles;
        }
    }
}
