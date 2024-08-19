

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
namespace BTL_Finance.Authrization
{
    public class CustomAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string[] _roles;

        public CustomAuthorizeAttribute(string roles)
        {
            _roles = roles.Split(',').Select(r => r.Trim()).ToArray();
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            if (!user.Identity.IsAuthenticated || !_roles.Any(role => user.IsInRole(role)))
            {
                context.Result = new RedirectToActionResult("Unauthorized", "Home", null);
            }
        }
    }
}