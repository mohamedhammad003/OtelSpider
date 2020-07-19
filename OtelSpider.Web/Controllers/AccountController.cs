using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using OtelSpider.Core.DAL.Models;
using OtelSpider.Web.ViewModels;
using System.Threading.Tasks;
using Microsoft.Owin.Security;
using System.Web.Mvc;
using System.Web;
using OtelSpider.Core.BLL.Services;
using System;
using AutoMapper;
using OtelSpider.Web.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Security;

namespace OtelSpider.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly ISecurityUserService _securityUserService;
        private readonly ITokenService _tokenService;
        public AccountController(ISecurityUserService securityUserService, ITokenService tokenService)
            : this(new UserManager<SystemUser>(new UserStore<SystemUser>(new Core.DAL.Data.OtelSpiderContext())))
        {
            _securityUserService = securityUserService;
            _tokenService = tokenService;
        }

        public AccountController(UserManager<SystemUser> userManager)
        {
            UserManager = userManager;
        }

        public UserManager<SystemUser> UserManager { get; private set; }

        [AllowAnonymous]
        public ActionResult UnAuthorized()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindAsync(model.UserName, model.Password);
                if (user != null && user.isActive)
                {
                    var userHotels = Mapper.Map<List<Hotel>, List<HotelViewModel>>(_securityUserService.GetUserHotels(user.Id).ToList());
                    var permissions = Mapper.Map<List<SystemPermission>, List<PermissionViewModel>>(_securityUserService.GetUserPermissions(user.Id).ToList());
                    var userInfo = new CustomisedUserInfo
                    {
                        UserId = user.Id,
                        UserName = user.UserName,
                        Name = user.FirstName + " " + user.LastName,
                        Title = user.Title,
                        Hotels = userHotels,
                        UserRoles = _securityUserService.GetUserRoles(user.Id).ToList(),
                        UserPermissions = permissions.Select(x => x.PermissionName).ToList()
                    };
                    UserHelper.AddUserInfoToCookie(userInfo, HttpContext);
                    HttpContext.Application[OtelConstant.IsApplicationStart] = true;

                    ViewBag.IsAdmin = user.isAdmin;
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                }
            }

            return View(model);
        }

        [AllowAnonymous]
        public ActionResult Register(string token)
        {
            var tokenModel = _tokenService.GetRegisterationToken(token);
            if (tokenModel == null || tokenModel.TokenExpires < DateTime.Now)
            {
                return View("Error", "404 this page not found");
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new SystemUser() { FirstName = model.FirstName, LastName = model.LastName, Email = model.Email, UserName = model.UserName };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    AddErrors(result);
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Disassociate(string loginProvider, string providerKey)
        {
            ManageMessageId? message = null;
            IdentityResult result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return RedirectToAction("Manage", new { Message = message });
        }

        public ActionResult Manage(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";
            ViewBag.HasLocalPassword = HasPassword();
            ViewBag.ReturnUrl = Url.Action("Manage");
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Manage(ManageUserViewModel model)
        {
            bool hasPassword = HasPassword();
            ViewBag.HasLocalPassword = hasPassword;
            ViewBag.ReturnUrl = Url.Action("Manage");
            if (hasPassword)
            {
                if (ModelState.IsValid)
                {
                    IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
            }
            else
            {
                // User does not have a password so remove any validation errors caused by a missing OldPassword field
                ModelState state = ModelState["OldPassword"];
                if (state != null)
                {
                    state.Errors.Clear();
                }

                if (ModelState.IsValid)
                {
                    IdentityResult result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult LogOff()
        {
            //AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [ChildActionOnly]
        public ActionResult RemoveAccountList()
        {
            var linkedAccounts = UserManager.GetLogins(User.Identity.GetUserId());
            ViewBag.ShowRemoveButton = HasPassword() || linkedAccounts.Count > 1;
            return (ActionResult)PartialView("_RemoveAccountPartial", linkedAccounts);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && UserManager != null)
            {
                UserManager.Dispose();
                UserManager = null;
            }
            base.Dispose(disposing);
        }

        public ActionResult Error(string ErrorMessage)
        {
            return View("~/Views/Shared/Error.cshtml", ErrorMessage);
        }

        #region Helpers

        //private IAuthenticationManager AuthenticationManager
        //{
        //    get
        //    {
        //        return HttpContext.GetOwinContext().Authentication;
        //    }
        //}

        private async Task SignInAsync(SystemUser user, bool isPersistent)
        {
            var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            FormsAuthentication.SignOut();
            Session.Abandon();
            //AuthenticationManager.Register(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            Error
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        private class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri) : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

        }
        #endregion
    }
}