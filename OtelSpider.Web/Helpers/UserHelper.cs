using OtelSpider.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace OtelSpider.Web.Helpers
{
    public class UserHelper
    {
        public static UserSettings Current
        {
            get { return GetUserCookie(); }
        }

        private static UserSettings GetUserCookie()
        {
            try
            {
                var model = new UserSettings();
                var userEncryptedData = HttpContext.Current.Request.Cookies[OtelConstant.UserDataCookie].Value;
                var userEncryptedRoles = HttpContext.Current.Request.Cookies[OtelConstant.UserRolesCookie].Value;
                var userEncryptedPermissions = HttpContext.Current.Request.Cookies[OtelConstant.UserPermissionsCookie].Value;
                var userDataJson = EncryptHelper.Decrypt(userEncryptedData.ToString(), ConfigurationHelper.EncryptionPassword);
                var userRolesJson = EncryptHelper.Decrypt(userEncryptedRoles.ToString(), ConfigurationHelper.EncryptionPassword);
                var userPermissionsJson = EncryptHelper.Decrypt(userEncryptedPermissions.ToString(), ConfigurationHelper.EncryptionPassword);
                var userModel = new JavaScriptSerializer().Deserialize<CustomisedUserInfo>(userDataJson);
                var rolesModel = new JavaScriptSerializer().Deserialize<List<string>>(userRolesJson);
                var permissionsModel = new JavaScriptSerializer().Deserialize<List<string>>(userPermissionsJson);
                model.UserInfo = userModel;
                model.Roles = rolesModel;
                model.Permissions = permissionsModel;
                return model;
            }
            catch
            {
                return null;
            }
        }
        public static bool IsAuthorized(string permissionName)
        {
            return Current.Permissions.FindIndex(x => x.Trim().ToLower() == permissionName.ToLower()) >= 0;
        }

        public static void RemoveUserCookies(HttpContextBase httpContext, bool IsHotelEnabled = false)
        {
            if (httpContext.Request.Cookies[OtelConstant.UserDataCookie] != null)
            {
                var newUserInfoCookie = httpContext.Request.Cookies[OtelConstant.UserDataCookie];
                newUserInfoCookie.Expires = DateTime.Now.AddYears(-1);
                httpContext.Response.Cookies.Add(newUserInfoCookie);
            }

            if (httpContext.Request.Cookies[OtelConstant.UserRolesCookie] != null)
            {
                var newUserRolesCookie = httpContext.Request.Cookies[OtelConstant.UserRolesCookie];
                newUserRolesCookie.Expires = DateTime.Now.AddYears(-1);
                httpContext.Response.Cookies.Add(newUserRolesCookie);
            }

            if (IsHotelEnabled)
            {
                if (httpContext.Request.Cookies[OtelConstant.HotelCookie] != null)
                {
                    var newCompanyBranchInfoCookie = new HttpCookie(OtelConstant.HotelCookie);
                    newCompanyBranchInfoCookie.Expires = DateTime.Now.AddYears(-1);
                    httpContext.Response.Cookies.Add(newCompanyBranchInfoCookie);
                }
            }
        }
        public static void AddUserInfoToCookie(CustomisedUserInfo model, HttpContextBase httpContext)
        {
            var userData = model;
            var userRoles = model.UserRoles;
            var userPermissions = model.UserPermissions;
            
            var userDataJson = new JavaScriptSerializer().Serialize(userData);
            var userRolesJson = new JavaScriptSerializer().Serialize(userRoles);
            var userPermissionsJson = new JavaScriptSerializer().Serialize(userPermissions);

            var userEncryptionData = EncryptHelper.Encrypt(userDataJson, ConfigurationHelper.EncryptionPassword);
            var userEncryptionRoles = EncryptHelper.Encrypt(userRolesJson, ConfigurationHelper.EncryptionPassword);
            var userEncryptionPermissions = EncryptHelper.Encrypt(userPermissionsJson, ConfigurationHelper.EncryptionPassword);

            if (httpContext.Request.Cookies[OtelConstant.UserDataCookie] != null)
            {
                httpContext.Request.Cookies[OtelConstant.UserDataCookie].Value = userEncryptionData;
            }

            if (httpContext.Request.Cookies[OtelConstant.UserRolesCookie] != null)
            {
                httpContext.Request.Cookies[OtelConstant.UserRolesCookie].Value = userEncryptionRoles;
            }
            else
            {
                var newUserInfoCookie = new HttpCookie(OtelConstant.UserDataCookie);
                newUserInfoCookie.Value = userEncryptionData;
                newUserInfoCookie.Expires = DateTime.Now.AddYears(1);
                httpContext.Response.Cookies.Add(newUserInfoCookie);

                var newUserRolesCookie = new HttpCookie(OtelConstant.UserRolesCookie);
                newUserRolesCookie.Value = userEncryptionRoles;
                newUserRolesCookie.Expires = DateTime.Now.AddDays(7);
                httpContext.Response.Cookies.Add(newUserRolesCookie);
            }

            if (httpContext.Request.Cookies[OtelConstant.UserPermissionsCookie] != null)
            {
                httpContext.Request.Cookies[OtelConstant.UserPermissionsCookie].Value = userEncryptionPermissions;
            }
            else
            {
                var newUserInfoCookie = new HttpCookie(OtelConstant.UserDataCookie);
                newUserInfoCookie.Value = userEncryptionData;
                newUserInfoCookie.Expires = DateTime.Now.AddDays(7);
                httpContext.Response.Cookies.Add(newUserInfoCookie);

                var newUserPermissionCookie = new HttpCookie(OtelConstant.UserPermissionsCookie);
                newUserPermissionCookie.Value = userEncryptionRoles;
                newUserPermissionCookie.Expires = DateTime.Now.AddDays(7);
                httpContext.Response.Cookies.Add(newUserPermissionCookie);
            }

            AddHotelCookie(new StaticCookie { HotelId = model.Hotels.Select(h => h.ID).ToList()}, httpContext);
        }

        private static int? CalculateGrandPrecision(int currencyPrecision)
        {
            var value = 1;
            for (int i = 0; i < currencyPrecision; i++)
            {
                value = value * 10;
            }
            return value;
        }

        public static void AddHotelCookie(StaticCookie companyBranchModel, HttpContextBase httpContext)
        {
            var companyBranchJson = new JavaScriptSerializer().Serialize(companyBranchModel);
            if (httpContext.Request.Cookies[OtelConstant.HotelCookie] != null)
            {
                httpContext.Request.Cookies[OtelConstant.HotelCookie].Value = companyBranchJson;
            }
            else
            {
                var newUserInfoCookie = new HttpCookie(OtelConstant.HotelCookie);
                newUserInfoCookie.Value = companyBranchJson;
                newUserInfoCookie.Expires = DateTime.Now.AddDays(7);
                httpContext.Response.Cookies.Add(newUserInfoCookie);
            }
        }
    }
    public class StaticCookie
    {
        public List<int> HotelId { get; set; }
    }
    public class UserSettings
    {
        public CustomisedUserInfo UserInfo { get; set; }
        public List<string> Roles { get; set; }
        public List<string> Permissions { get; set; }
    }
}