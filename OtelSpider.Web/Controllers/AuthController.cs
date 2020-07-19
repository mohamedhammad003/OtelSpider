using AutoMapper;
using OtelSpider.Core.BLL.Services;
using OtelSpider.Core.DAL.Models;
using OtelSpider.Web.ActionFilter;
using OtelSpider.Web.Helpers;
using OtelSpider.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace OtelSpider.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly ISecurityUserService _securityUserService;
        private readonly ITokenService _tokenService;

        public AuthController(ISecurityUserService securityUserService, ITokenService tokenService)
        {
            _securityUserService = securityUserService;
            _tokenService = tokenService;
        }
        [AuthorizeRole(PermissionName = "SuperAdmin")]
        [HttpGet]
        public ActionResult UsersList()
        {
            var lstUsers = _securityUserService.GetUsers().Where(u => u.Id != UserHelper.Current.UserInfo.UserId);
            var lstUsersVM = Mapper.Map<IEnumerable<SystemUser>, IEnumerable<UserViewModel>>(lstUsers);
            return View(lstUsersVM);
        }
        [AuthorizeRole(PermissionName = "SuperAdmin")]
        [HttpGet]
        public ActionResult Tokens()
        {
            var lstTokens = _tokenService.GetRegisterationTokens();
            var lstTokensVM = Mapper.Map<IEnumerable<RegisterationToken>, IEnumerable<TokenViewModel>>(lstTokens);
            return View(lstTokensVM);
        }
        [AuthorizeRole(PermissionName = "SuperAdmin")]
        [HttpGet]
        public ActionResult CreateToken()
        {
            var lstRoles = _securityUserService.GetRoles().ToList();
            ViewBag.lstRoles = new SelectList(lstRoles.Select(x =>
              new SelectListItem() { Text = x.Name.ToString(), Value = x.Id.ToString() }), "Value", "Text"); 
            return View();
        }
        [AuthorizeRole(PermissionName = "SuperAdmin")]
        [HttpPost]
        public ActionResult CreateToken(TokenViewModel tokenViewModel)
        {
            var domainName = System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties().DomainName;
            var tokenValue = Guid.NewGuid().ToString();
            var token = new RegisterationToken
            {
                Token = tokenValue,
                TokenExpires = DateTime.Now.AddDays(1),
                RegisterLink = domainName+"/Account/Register?token=" + tokenValue,
                UserEmail = tokenViewModel.UserEmail,
                SystemRoleID = tokenViewModel.SystemRoleID
            };
            _tokenService.Create(token);
            _tokenService.SaveChanges();
            return View("Tokens");
        }
        [AuthorizeRole(PermissionName = "SuperAdmin")]
        [HttpGet]
        public ActionResult Edit(string Id)
        {
            var user = _securityUserService.GetUserByID(Id);
            var userVM = Mapper.Map<SystemUser, UserViewModel> (user);

            return View(userVM);
        }
        [HttpPost]
        public ActionResult Edit(UserViewModel user)
        {
            var userModel = Mapper.Map<UserViewModel, SystemUser>(user);
            _securityUserService.UpdateUser(userModel);
            
            return RedirectToAction("UsersList");
        }
    }
}