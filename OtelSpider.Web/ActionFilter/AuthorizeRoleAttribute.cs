using OtelSpider.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OtelSpider.Web.ActionFilter
{
    public class AuthorizeRoleAttribute : ActionFilterAttribute
    {
        public string PermissionName { get; set; }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var httpContext = context.HttpContext;

            if (httpContext.Application[OtelConstant.IsApplicationStart] == null)
            {
                UserHelper.RemoveUserCookies(httpContext, true);
                NavigateToLoginView(context, httpContext);
                return;
            }

            if (httpContext.Request.Cookies[OtelConstant.UserDataCookie] == null)
            {
                NavigateToLoginView(context, httpContext);
                return;
            }

            if (!UserHelper.IsAuthorized(this.PermissionName))
            {
                NavigateToUnAuthorizedView(context, httpContext);
                return;
            }
            base.OnActionExecuting(context);
        }



        private void NavigateToUnAuthorizedView(ActionExecutingContext context, HttpContextBase httpContext)
        {
            var urlHelper = new UrlHelper(context.RequestContext);
            var url = urlHelper.Action("UnAuthorized", "Account");
            httpContext.Response.Redirect(url, true);
        }

        private static void NavigateToLoginView(ActionExecutingContext context, System.Web.HttpContextBase httpContext)
        {
            var redirectOnSuccess = httpContext.Request.Url.AbsolutePath;
            var urlHelper = new UrlHelper(context.RequestContext);
            var url = urlHelper.Action("Login", "Account", new { RedirectOnSuccess = redirectOnSuccess });
            context.Result = new RedirectToRouteResult(
                new RouteValueDictionary
                {
                    { "controller", "Account" },
                    { "action", "Login" }
                });
        }
    }
}