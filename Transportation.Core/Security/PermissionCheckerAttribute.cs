using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transportation.Core.Services;

namespace Transportation.Core.Security
{
    public class PermissionCheckerAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private int _permissionId = 0;
        private IAccountService _accountService;

        public PermissionCheckerAttribute(int permissionId)
        {
            _permissionId = permissionId;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                _accountService =
                    (IAccountService)context.HttpContext.RequestServices.GetService(typeof(IAccountService));

                string userName = context.HttpContext.User.Identity.Name;

                if (!_accountService.CheckUserPermissions(_permissionId, userName))
                {
                    context.Result = new RedirectResult("/");
                }
            }
            else
            {
                context.Result = new RedirectResult("/Login" + "?ReturnPath=" + context.HttpContext.Request.Path);
            }
        }
    }
}
